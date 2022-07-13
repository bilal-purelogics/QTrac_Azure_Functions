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
        [FunctionName("Queue-GetAllAgentsServings")]
        public static async Task<KioskRequest []> GetAllAgentsServings(
        [ActivityTrigger] CustomerRequest inputValues,
        [CosmosDB(
        databaseName: "COSMOS_DATABASE",
        collectionName: "LATCH_TRIGGER_ITEMS_CONTAINER",
        ConnectionStringSetting = "COSMOS_CONNECTION_STRING"
        )] DocumentClient client,
        ILogger log)
        {
            var option = new FeedOptions { PartitionKey = new PartitionKey(inputValues.branchId) };
            var dbName = Environment.GetEnvironmentVariable("COSMOS_DATABASE", EnvironmentVariableTarget.Process);

            var dbContainer = Environment.GetEnvironmentVariable("COSMOS_COMPANY_CONFIGURATIONS_CONTAINER", EnvironmentVariableTarget.Process);

            var users = client.CreateDocumentQuery<UserModel>(UriFactory.CreateDocumentCollectionUri(dbName, dbContainer), option)
                .Where(f => f.pk == inputValues.id && f.type=="user" && f.isOnlineAsAgent==true && f.agentDeskSettings.branchId ==inputValues.branchId).AsEnumerable();

            //Need to implement logic yet
            KioskRequest [] customerRequest=null;
            return customerRequest;
        }

    }
}