using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.DBEntity
{
    public class ExceptionRequest : EntityBase
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Message { get; set; }
        public string User { get; set; }
    }
}
