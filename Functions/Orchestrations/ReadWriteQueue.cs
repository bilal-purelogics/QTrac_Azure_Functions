using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LAVI.QueueManager;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Lavi.QueueManager
{
    public static partial class ReadWriteQueue
    {

        [FunctionName("Queue-ReadWriteQueueDocument")]
        public static async Task<IQueue> ReadWriteQueueDocument(
        [OrchestrationTrigger] IDurableOrchestrationContext context,
        ILogger log)
        {
            bool queueUpdated = false;
            IQueue updateQueue = new IQueue();
            
            while (!queueUpdated)
            {
                try
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
                        updateQueue = await UpdateServingTime(context, entry, queue);

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
                        updateQueue = await UpdateServingTime(context, entry, queue);

                    }

                    await context.CallActivityAsync<IQueue>("activity-trigger-signalr-on-update-queue", updateQueue);

                    queueUpdated = true;

                }
                catch (Exception e)
                {
                    log.LogError(e, "ReadWriteProcessor");
                    queueUpdated = false;
                    updateQueue=null;
                }
            }

            return updateQueue;
        }

        private static async Task<IQueue> UpdateServingTime([OrchestrationTrigger] IDurableOrchestrationContext context, CustomerRequest customerRequest, IQueue queue)
        {
            if (customerRequest.workflow?.estimateWaitSettings?.allowCalculateEstimateWaitTime == false)
            {
                return queue;
            }


            double calculationStartTimeInMilliseconds = Convert.ToInt64((DateTime.Now.ToUniversalTime() - new DateTime(1970, 1, 1)).TotalMilliseconds);
            var idleTimeBetweenServicesInMilliseconds = customerRequest.workflow.estimateWaitSettings.idleTimeBetweenServices * 60 * 1000;
            var workflowId = customerRequest.workflow.WorkFlowId;

            var allOnlineAgents = new List<Agent>();
            allOnlineAgents = await context.CallActivityAsync<List<Agent>>("Queue-GetAllOnlineAgentInBranch", customerRequest);

            if (allOnlineAgents == null)
            {
                Agent agent = new Agent { agentId = "", companyId = customerRequest.workflow.companyId, queueIds = null, busyTillTimeInMilliseconds = 0, customersInServing = null, customerToBeServed = null };

                allOnlineAgents.Append(agent);
            }

            var AllInServingCustomers = await context.CallActivityAsync<List<KioskRequest>>("Queue-GetAllAgentsServings", customerRequest);
            //queue.estimatedWaitRangesSettings.estimateWaitSettings = customerRequest.workflow.estimateWaitSettings.customRangesForTimeDisplayMessages[0];

            foreach (var agent in allOnlineAgents)
            {
                if (agent.customersInServing != null)
                {
                    agent.customersInServing = (KioskRequest[])AllInServingCustomers.Where(x => x.servingAgentId == agent.agentId);

                    foreach (var customer in agent.customersInServing)
                    {
                        
                        agent.busyTillTimeInMilliseconds = agent.busyTillTimeInMilliseconds + QueueManager.GetServiceTimeInMilliseconds(customerRequest.workflow,customer.serviceId);

                    }
                }

                agent.busyTillTimeInMilliseconds = Convert.ToDouble(calculationStartTimeInMilliseconds);

            }


            int index = 0;

            while (QueueManager.GetCustomerCount(queue) > index)
            {
                if (allOnlineAgents == null)
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
                    customer.estimateWaitTimeISOString = new DateTime(1970, 1, 1).AddMilliseconds(ExpectedServeTime).ToString();

                    var ServiceTimeInMilliseconds = QueueManager.GetServiceTimeInMilliseconds(customerRequest.workflow,customer.serviceId);
                    agent.busyTillTimeInMilliseconds = ExpectedServeTime + ServiceTimeInMilliseconds;

                }
            }

            IQueue updatedQueue = new IQueue();
            updatedQueue = await context.CallActivityAsync<IQueue>("Queue-UpdateWaitingCustomerQueue", queue);

            if (updatedQueue.estimatedWaitRangesSettings != null)
            {
                updatedQueue.estimatedWaitRangesSettings.allowCalculateEstimateWaitTime = customerRequest.workflow.estimateWaitSettings.allowCalculateEstimateWaitTime;
            }
            return updatedQueue;
        }
    }
}