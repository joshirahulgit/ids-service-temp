using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Business.Entity
{
    public enum UserAccessRightToAccount
    {
        NotDefined = -1,
        NoAccess = 0,
        RegularUser = 1,
        SchedulerAdmin = 2,
    }
}
