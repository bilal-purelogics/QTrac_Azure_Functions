using System.Net.Http;
using System.Threading.Tasks;
using LAVI.QueueManager;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;


namespace Lavi.QueueManager
{
    public static partial class AddOrUpdateQueueTrigger
    {

        [FunctionName("Queue-AddOrUpdateQueueTrigger")]
        public static async Task<HttpResponseMessage> AddToQueueTrigger(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "QueueManager/AddOrUpdateQueueTrigger")] HttpRequestMessage req,
        [DurableClient] IDurableOrchestrationClient client,
        ILogger log)
        {

            var data = await req.Content.ReadAsAsync<CustomerRequest>();

            var instanceId = await client.StartNewAsync("Queue-ReadWriteQueueDocument", data);

            return await client.WaitForCompletionOrCreateCheckStatusResponseAsync(req, instanceId, System.TimeSpan.MaxValue);
        }

    }
}