using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.DBEntity
{
    public class Area : EntityBase
    {
        public virtual String Country { get; set; }
        public virtual String State { get; set; }
        public virtual String City { get; set; }

        public virtual List<ResourceLocation> Locations { get; set; }

        public Area()
        {

        }

        public Area(String country, String state, String city)
        {
            this.Id = -1;
            this.Country = country;
            this.State = state;
            this.City = city;
        }
    }
}
