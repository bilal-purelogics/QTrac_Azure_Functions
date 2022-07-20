using System.Threading.Tasks;
using LAVI.QueueManager;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;

namespace Lavi.QueueManager
{
    public static partial class SendSignalRNotification
    {
        [FunctionName("activity-trigger-signalr-on-update-queue")]
        public static async Task<dynamic> SendNotification(
        [ActivityTrigger] IQueue request,
        [SignalR(HubName = "chat")] IAsyncCollector<SignalRMessage> signalRMessages)
        {

            var documents = request;

            await signalRMessages.AddAsync(new SignalRMessage
            {
                //the message will only be sent to this user ID
                UserId = documents.id,
                Target = "QUEUE_UPDATED",
                Arguments = new[] { documents }
            });

            return new OkObjectResult("Notification Sent Successfully!");
        }
    }
}