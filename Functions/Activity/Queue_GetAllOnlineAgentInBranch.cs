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
        public static async Task<List<Agent>> GetAllOnlineAgentInBranch(
        [ActivityTrigger] CustomerRequest inputValues,
        [CosmosDB(
        databaseName: "COSMOS_DATABASE",
        collectionName: "COSMOS_COMPANY_CONFIGURATIONS_CONTAINER",
        ConnectionStringSetting = "COSMOS_CONNECTION_STRING"
        )] DocumentClient client,
        ILogger log)
        {
            var option = new FeedOptions { PartitionKey = new PartitionKey(inputValues.workflow.companyId) };
            var dbName = Environment.GetEnvironmentVariable("COSMOS_DATABASE", EnvironmentVariableTarget.Process);

            var dbContainer = Environment.GetEnvironmentVariable("COSMOS_COMPANY_CONFIGURATIONS_CONTAINER", EnvironmentVariableTarget.Process);

            var users = client.CreateDocumentQuery<UserModel>(UriFactory.CreateDocumentCollectionUri(dbName, dbContainer), option)
                .Where(f => f.pk == inputValues.workflow.companyId && f.Type == "user" && f.isOnlineAsAgent == true && f.AgentDeskSettings.branchId == inputValues.branchId).AsEnumerable();


            var userList = users.ToList();


            var rolesToFetchIfAny = userList.Select(user => user.RoleId).ToArray();


            var UserRoles = client.CreateDocumentQuery<UserRole>(UriFactory.CreateDocumentCollectionUri(dbName, dbContainer), option)
                .Where(f => f.pk == inputValues.workflow.companyId && f.type == "UserRole" && rolesToFetchIfAny.Contains(f.roleId)).AsEnumerable();

            var UserRolesList = UserRoles.FirstOrDefault();

            var agentList = new List<Agent>();
            foreach (var item in userList)
            {
                agentList.Add(QueueManager.MapUserToAgent(item, UserRolesList));

            }

            return agentList;

        }


    }
}