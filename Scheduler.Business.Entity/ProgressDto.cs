using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Business.Entity
{
    public class ProgressDto : DtoBase
    {
        public long Count { get; set; }

        public long TotalCount { get; set; }

        public string Text { get; set; }

    }
}
