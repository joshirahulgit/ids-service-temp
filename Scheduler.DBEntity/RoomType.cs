using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.DBEntity
{
    public class RoomType : EntityType
    {
        public RoomType()
        {
        }
        public RoomType(long id)
            : this()
        {
            this.Id = id;
        }

        public RoomType(long id, String typeName)
            : this(id)
        {
            this.Name = typeName;
        }
    }
}
