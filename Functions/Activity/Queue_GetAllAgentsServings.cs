using System;
using System.Collections.Generic;
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
        public static async Task<List<KioskRequest>> GetAllAgentsServings(
        [ActivityTrigger] CustomerRequest customerReq,
        [CosmosDB(
        databaseName: "COSMOS_DATABASE",
        collectionName: "LATCH_TRIGGER_ITEMS_CONTAINER",
        ConnectionStringSetting = "COSMOS_CONNECTION_STRING"
        )] DocumentClient client,
        ILogger log)
        {
            
            var option = new FeedOptions { PartitionKey = new PartitionKey(customerReq.workflow.companyId) };
            var dbName = Environment.GetEnvironmentVariable("COSMOS_DATABASE", EnvironmentVariableTarget.Process);

            var dbContainer = Environment.GetEnvironmentVariable("COSMOS_WAITING_CUSTOMERS_QUEUES_CONTAINER", EnvironmentVariableTarget.Process);

            var customers = client.CreateDocumentQuery<KioskRequest>(UriFactory.CreateDocumentCollectionUri(dbName, dbContainer), option)
                .Where(f => f.customerState == CustomerState.SERVING && f.type == "kiosk-request" && f.isDeleted == false && f.servingAgentId == "5275e255-e0a6-4c0c-a930-c27440d8c4bb").AsEnumerable();
            
            var customersList = customers.ToList();

            var servingCustomersList = new List<KioskRequest>();

            foreach (var customer in customersList)
            {
                servingCustomersList.Add(customer);
            }

            return servingCustomersList;
        }

    }
}