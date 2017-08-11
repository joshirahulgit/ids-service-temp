using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.DBEntity
{
    public class PhysicianSpecializationType : EntityType
    {
        public PhysicianSpecializationType()
        {
        }

        public PhysicianSpecializationType(long id)
            : this()
        {
            this.Id = id;
        }
    }
}
