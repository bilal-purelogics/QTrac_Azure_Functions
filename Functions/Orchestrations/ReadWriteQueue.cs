using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LAVI.QueueManager;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;

namespace Lavi.QueueManager
{
    public static partial class ReadWriteQueue
    {

        [FunctionName("Queue-ReadWriteQueueDocument")]
        public static async Task ReadWriteQueueDocument(
        [OrchestrationTrigger] IDurableOrchestrationContext context,
        ILogger log)
        {
            bool queueUpdated = false;
            while (!queueUpdated)
            {
                CustomerRequest entry = context.GetInput<CustomerRequest>();
                entry.id = entry.branchId + "_" + entry.workflowId + "_Queue";
                IQueuedCustomer customer = new IQueuedCustomer() { id = entry.id, serviceId = entry.serviceId, queueId = entry.queueId, customerState = CustomerStateInQueue.WAITING };
                var queue = await context.CallActivityAsync<IQueue>("Queue-ReadWaitingCustomerQueue", entry);
                IQueue queueResult = new IQueue();
                if (queue != null)
                {
                    var queuelist = queue.customers.ToList();
                    queuelist.Add(customer);
                    var queueArray = queuelist.ToArray();
                    queue.customers = queueArray;
                    queueResult = await context.CallActivityAsync<IQueue>("Queue-UpdateWaitingCustomerQueue", queue);
                    
                    // Call Update Serving Time Function
                    //UpdateServingTime(context,kioskRequest,queue);
                }
                else
                {
                    List<IQueuedCustomer> emptyQueue = new List<IQueuedCustomer>();
                    emptyQueue.Add(customer);
                    queueResult.pk = entry.branchId;
                    queueResult.id = entry.id;
                    queueResult.type = entry.DocumentType;
                    queueResult.customers = emptyQueue.ToArray();
                    queueResult = await context.CallActivityAsync<IQueue>("Queue-UpdateWaitingCustomerQueue", queueResult);
                    
                    // Call Update Serving Time Function
                    //UpdateServingTime(context,kioskRequest,queue);
                }
                queueUpdated = true;
            }          

        }
        private static async Task<IQueue> UpdateServingTime([OrchestrationTrigger] IDurableOrchestrationContext context,KioskRequest kioskRequest,IQueue queue)
        {
            var calculationStartTimeInMilliseconds = (DateTime.Now.ToUniversalTime() - new DateTime(1970, 1, 1));                    
            var idleTimeBetweenServicesInMilliseconds =kioskRequest.workflow.incomingWorkflowEstimateWaitSettings.idleTimeBetweenServices * 60 * 1000;
            var workflowId = kioskRequest.workflow.workFlowId;

            Agent [] AllOnlineAgents = null;
            AllOnlineAgents = await context.CallActivityAsync<Agent[]>("Queue-GetAllOnlineAgentInBranch", queue);

            if(AllOnlineAgents==null)
            {
                var emptyAgent=new Agent{agentId="",companyId="",queueIds=null,busyTillTimeInMilliseconds=0,customersInServing=null,customerToBeServed=null};
                AllOnlineAgents.Append(emptyAgent);
            }

            var AllInServingCustomers=await context.CallActivityAsync<CustomerRequest[]>("Queue-GetAllAgentsServings", queue);

            
            foreach(var agent in AllOnlineAgents.ToList())
            {
                var ExpectedServeTime = agent.busyTillTimeInMilliseconds + idleTimeBetweenServicesInMilliseconds;
                var breakEndTime = QueueManager.GetBreakEndTimeIfThere(ExpectedServeTime);

                if (breakEndTime !=0) {
                  ExpectedServeTime += breakEndTime;
                }

                var ServiceTimeInMilliseconds = QueueManager.GetServiceTimeInMilliseconds(kioskRequest.workflow);
                agent.busyTillTimeInMilliseconds = ExpectedServeTime + ServiceTimeInMilliseconds;
            }

            IQueue queueResult = new IQueue();
            return queueResult = await context.CallActivityAsync<IQueue>("Queue-UpdateWaitingCustomerQueue", queue);
        }      
    }
}