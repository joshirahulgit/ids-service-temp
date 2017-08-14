using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.DBEntity
{
    public class Surgeon : EntityBase
    {
        public int Type { get; set; }
        public string Name { get; set; }
    }
}
