using System;
using System.Linq;
using System.Threading.Tasks;
using LAVI.QueueManager;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;


namespace Lavi.QueueManager
{
    public static partial class Queue_Activity_UpdateQueue
    {
        [FunctionName("Queue-UpdateWaitingCustomerQueue")]
        public static async Task<IQueue> UpdateWaitingCustomerQueue(
        [ActivityTrigger] IDurableActivityContext context,
        [CosmosDB(
        databaseName: "COSMOS_DATABASE",
        collectionName: "LATCH_TRIGGER_ITEMS_CONTAINER",
        ConnectionStringSetting = "COSMOS_CONNECTION_STRING"
        )] DocumentClient client,
        ILogger log)
        {
            
            var entry = context.GetInput<IQueue>();

            var dbName = Environment.GetEnvironmentVariable("COSMOS_DATABASE", EnvironmentVariableTarget.Process);
            var dbContainer = Environment.GetEnvironmentVariable("COSMOS_WAITING_CUSTOMERS_QUEUES_CONTAINER", EnvironmentVariableTarget.Process);

            if (entry.pk != null && entry.pk != "null")
            {
                var options = new RequestOptions() { PartitionKey = new PartitionKey(entry.pk) };
                await client.UpsertDocumentAsync(UriFactory.CreateDocumentCollectionUri(dbName, dbContainer).ToString(), entry, options);
            }

            return entry;
        }
    }
}