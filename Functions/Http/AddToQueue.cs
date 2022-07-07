using System.Net.Http;
using System.Threading.Tasks;
using LAVI.QueueManager;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;


namespace Lavi.QueueManager
{
    public static partial class AddToQueue
    {

        [FunctionName("Queue-AddToQueueTrigger")]
        public static async Task<HttpResponseMessage> AddToQueueTrigger(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "QueueManager/AddToQueueTrigger")] HttpRequestMessage req,
        [DurableClient] IDurableOrchestrationClient client,
        ILogger log)
        {

            var data = await req.Content.ReadAsAsync<CustomerRequest>();
            await client.StartNewAsync("Queue-ReadWriteQueueDocument", new CustomerRequest()
            {
                id = data.id,
                branchId = data.branchId,
                workflowId = data.workflowId,
                serviceId = data.serviceId,
                queueId = data.queueId
            });

            return req.CreateResponse(System.Net.HttpStatusCode.Accepted);
        }

    }
}