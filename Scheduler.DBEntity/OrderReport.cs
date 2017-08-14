using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.DBEntity
{
    public class OrderReport : EntityBase
    {
        public string JobId { get; set; }
        public string FilePath { get; set; }
        public string Account { get; set; }
        public string Html { get; set; }
    }
}
