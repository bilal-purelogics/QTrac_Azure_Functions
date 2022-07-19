using System;
using System.Collections.Generic;
using System.Linq;
using LAVI.QueueManager;

namespace Lavi.QueueManager
{

    public static partial class QueueManager
    {
        public static int GetServiceTimeInMilliseconds(Workflow workflow, string serviceId)
        {
            var averageWaitTime = workflow.services.ToList().Where(x => x.ServiceId == serviceId).Select(x => x.averageWaitTime);
            
            //if (averageWaitTime != null)
            //{
            //    return Convert.ToInt32(averageWaitTime) * 60 * 100;
            //}
            return 0;
        }

        public static double GetBreakEndTimeIfThere(double expectedServeTime)
        {
            return 0;
        }
        public static Agent MapUserToAgent(UserModel user, UserRole userRoles)
        {
            Agent agent = null;
            if (user.IsOverride != null)
            {
                agent = new Agent { agentId = user.Id, companyId = user.companyId, busyTillTimeInMilliseconds = 0, customersInServing = null, queueIds = userRoles.agentTemplates[0].Queues };
            }
            else
            {
                agent = new Agent { agentId = user.Id, companyId = user.companyId, busyTillTimeInMilliseconds = 0, customersInServing = null, queueIds = userRoles.agentTemplates[0].Queues };
            }
            return agent;
        }
        public static int GetCustomerCount(IQueue queue)
        {

            if (queue.customers.Length <= 0)
            {
                return 0;
            }
            return queue.customers.Length;
        }
    }
}