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
    public static partial class Queue_Activity_ReadQueue
    {
        [FunctionName("Queue-ReadWaitingCustomerQueue")]
        public static async Task<IQueue> GetWaitingCustomerQueue(
        [ActivityTrigger] CustomerRequest customerReq,
        [CosmosDB(
        databaseName: "COSMOS_DATABASE",
        collectionName: "LATCH_TRIGGER_ITEMS_CONTAINER",
        ConnectionStringSetting = "COSMOS_CONNECTION_STRING"
        )] DocumentClient client,
        ILogger log)
        {
            var option = new FeedOptions { PartitionKey = new PartitionKey(customerReq.branchId) };
            var dbName = Environment.GetEnvironmentVariable("COSMOS_DATABASE", EnvironmentVariableTarget.Process);

            var dbContainer = Environment.GetEnvironmentVariable("COSMOS_WAITING_CUSTOMERS_QUEUES_CONTAINER", EnvironmentVariableTarget.Process);

            var items = client.CreateDocumentQuery<IQueue>(UriFactory.CreateDocumentCollectionUri(dbName, dbContainer), option)
                .Where(f => f.pk == customerReq.branchId && f.type == "queue" && f.id == customerReq.id).AsEnumerable();

            var data = items.FirstOrDefault();

            return data;
        }

    }
}