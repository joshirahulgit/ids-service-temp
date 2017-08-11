using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Core
{
    public enum AppointmentStatuses
    {
        New = 1,
        Arrived = 2,
        Complete = 3,
        Cancel = 4,
        NoShow = 5,
        Blocked = 6,
        Available = 7,
        Rescheduled = 8,
        TechComplete = 9,
        CheckIn = 10,
        Pending = 11,
    }
}
