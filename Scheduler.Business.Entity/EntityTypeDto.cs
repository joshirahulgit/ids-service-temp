using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Business.Entity
{
    public class EntityTypeDto //: DtoBase
    {
        public long TypeId { get; set; }

        public string TypeName { get; set; }

        public bool IsVisible { get; set; }

        public bool IsSystem { get; set; }

        public string Text
        {
            get { return TypeName; }
        }

        public override string ToString()
        {
            return Text;
        }
    }
}
