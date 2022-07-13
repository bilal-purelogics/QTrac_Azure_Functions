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

                IQueuedCustomer customer = new IQueuedCustomer()
                {
                    id = entry.id,
                    serviceId = entry.serviceId,
                    queueId = entry.queueId,
                    customerState = CustomerStateInQueue.WAITING
                };

                var queue = await context.CallActivityAsync<IQueue>("Queue-ReadWaitingCustomerQueue", entry);
                IQueue queueResult = new IQueue();

                if (queue != null)
                {
                    var queuelist = queue.customers.ToList();
                    queuelist.Add(customer);
                    var queueArray = queuelist.ToArray();
                    queue.customers = queueArray;
                    queueResult = await context.CallActivityAsync<IQueue>("Queue-UpdateWaitingCustomerQueue", queue);

                    // Call Sub Orchestration
                    var updateQueue = await UpdateServingTime(context, entry.kioskRequest, queue);
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

                    // Call Sub Orchestration
                    var updateQueue = await UpdateServingTime(context, entry.kioskRequest, queue);
                }

                queueUpdated = true;
            }

        }
        private static async Task<IQueue> UpdateServingTime([OrchestrationTrigger] IDurableOrchestrationContext context, KioskRequest kioskRequest, IQueue queue)
        {
            if (kioskRequest.workflow?.incomingWorkflowEstimateWaitSettings?.allowCalculateEstimateWaitTime == false)
            {
                return queue;
            }

            var calculationStartTimeInMilliseconds = (DateTime.Now.ToUniversalTime() - new DateTime(1970, 1, 1));
            var idleTimeBetweenServicesInMilliseconds = kioskRequest.workflow.incomingWorkflowEstimateWaitSettings.idleTimeBetweenServices * 60 * 1000;
            var workflowId = kioskRequest.workflow.workFlowId;

            Agent[] allOnlineAgents = null;
            allOnlineAgents = await context.CallActivityAsync<Agent[]>("Queue-GetAllOnlineAgentInBranch", queue);

            if (!(allOnlineAgents == null && allOnlineAgents.Length > 0))
            {
                Agent agent = new Agent { agentId = "", companyId = kioskRequest.companyId, queueIds = null, busyTillTimeInMilliseconds = 0, customersInServing = null, customerToBeServed = null };
                allOnlineAgents.Append(agent);
            }

            var AllInServingCustomers = await context.CallActivityAsync<KioskRequest[]>("Queue-GetAllAgentsServings", queue);
            queue.estimatedWaitRangesSettings.estimateWaitSettings = kioskRequest.workflow.incomingWorkflowEstimateWaitSettings.estimateWaitSettings;

            foreach (var agent in allOnlineAgents)
            {
                agent.customersInServing = (KioskRequest[])AllInServingCustomers.Where(x => x.servingAgentId == agent.agentId);

                agent.busyTillTimeInMilliseconds = Convert.ToInt32(calculationStartTimeInMilliseconds);
                foreach (var customer in agent.customersInServing)
                {
                    agent.busyTillTimeInMilliseconds = agent.busyTillTimeInMilliseconds + QueueManager.GetServiceTimeInMilliseconds(kioskRequest.workflow);
                }
            }

            int index = 0;

            while (QueueManager.GetCustomerCount(queue) > index)
            {
                if (allOnlineAgents.Length == 0)
                {
                    break;
                }

                foreach (var agent in allOnlineAgents.ToList())
                {
                    var customer = queue.customers[index++];

                    if (customer == null)
                    {
                        break;
                    }

                    var ExpectedServeTime = agent.busyTillTimeInMilliseconds + idleTimeBetweenServicesInMilliseconds;
                    var breakEndTime = QueueManager.GetBreakEndTimeIfThere(ExpectedServeTime);

                    if (breakEndTime != 0)
                    {
                        ExpectedServeTime += breakEndTime;
                    }

                    customer.estimateWaitTimeISOString = (ExpectedServeTime.ToString("yyyy-MM-dd'T'HH:mm:ss zzz"));
                    var ServiceTimeInMilliseconds = QueueManager.GetServiceTimeInMilliseconds(kioskRequest.workflow);
                    agent.busyTillTimeInMilliseconds = ExpectedServeTime + ServiceTimeInMilliseconds;
                }
            }

            IQueue updatedQueue = new IQueue();
            updatedQueue = await context.CallActivityAsync<IQueue>("Queue-UpdateWaitingCustomerQueue", queue);

            if (updatedQueue.estimatedWaitRangesSettings != null)
            {
                updatedQueue.estimatedWaitRangesSettings.allowCalculateEstimateWaitTime = kioskRequest.workflow.incomingWorkflowEstimateWaitSettings.allowCalculateEstimateWaitTime;
            }

            return updatedQueue;
        }
    }
}