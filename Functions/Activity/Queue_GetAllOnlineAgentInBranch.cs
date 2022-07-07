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
        [FunctionName("Queue-GetAllOnlineAgentInBranch")]
        public static async Task<Agent []> GetAllOnlineAgentInBranch(
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

            var rolesToFetchIfAny = users.Where(user => !user.isOverride).Select(user=>user.roleId);


            var UserRoles = client.CreateDocumentQuery<UserRole>(UriFactory.CreateDocumentCollectionUri(dbName, dbContainer), option)
                .Where(f => f.type=="UserRole" && f.roleId.Equals(rolesToFetchIfAny)).AsEnumerable();
            
            var userList= users.ToList();
            foreach(var item in userList)
            {
              QueueManager.MapUserToAgent(item,UserRoles);
            }

            //Need to implement logic yet
            Agent[] agents=null;
            return  agents;

        }


    }
}