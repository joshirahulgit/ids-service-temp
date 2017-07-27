using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Data.Implementation
{
    public interface IWarningsNotifier
    {
        //TODO: Not Sure now -By RJ
        //void AddWarnings(SchedulerWarningType warningType, String message);
        bool HasWarnings { get; }
        //TODO: Not sure now -By RJ
        //IEnumerable<SchedulerWarning> RetrieveWarnings();
    }
}
