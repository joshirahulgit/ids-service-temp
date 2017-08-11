using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.DBEntity
{
    public class CodeCategory : EntityType
    {
        public CodeCategory Parent { get; set; }
        public bool IsActive { get; set; }

        public CodeCategory()
        {
        }

        public CodeCategory(int id) : this()
        {
            this.Id = id;
        }

    }
}
