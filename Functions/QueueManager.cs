using System.Collections.Generic;
using LAVI.QueueManager;

namespace Lavi.QueueManager
{
 
    public static partial class QueueManager
    {
        public static int GetServiceTimeInMilliseconds(Workflow workflow)
        {
            return workflow.services.averageWaitTime * 60 * 100;
        }

        public static int GetBreakEndTimeIfThere(int expectedServeTime)
        {
            return 0;
        }
        public static Agent MapUserToAgent(UserModel user, IEnumerable<UserRole> userRoles)
        {
            Agent agent=null;
            if(user.isOverride)
            {
                agent=new Agent{agentId=user.id,companyId=user.companyId,busyTillTimeInMilliseconds=0,customersInServing=null,queueIds=null};
            }
            else
            {
                agent=new Agent{agentId=user.id,companyId=user.companyId,busyTillTimeInMilliseconds=0,customersInServing=null,queueIds=null};
            }
            return agent;
        }
        public static int GetCustomerCount(IQueue queue){
            
            if (!(queue==null && queue.customers==null && queue.customers.Length > 0)) {
              return 0;
            }
            return queue.customers.Length;
        }
    }
}