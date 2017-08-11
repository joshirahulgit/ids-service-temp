using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Business.Entity
{
    public class ResourceDurationDto : DtoBase
    {
        public long Id { get; set; }
        public int ActualDuration { get; set; }
        public int SedationTime { get; set; }
        public int AdditionalLeadTime { get; set; }
        public int IncrementTime { get; set; }
        public int DecrementTime { get; set; }
    }
}
