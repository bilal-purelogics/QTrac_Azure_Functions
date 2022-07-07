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
        public static Agent MapUserToAgent(UserModel users, IEnumerable<UserRole> userRoles)
        {
            Agent a = new Agent();
            return a;
        }
    }
}