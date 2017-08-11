using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Business.Entity
{
    public class PhysicianSpecialityDto : DtoBase
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public bool IsVisible { get; set; }

        public bool IsDeleted { get; set; }
    }

    public class PhysicianSpecialitiesDto : DtoBase
    {
        public PhysicianSpecialitiesDto()
        {
            this.PhysicianSpecialities = new List<PhysicianSpecialityDto>();
        }

        public IList<PhysicianSpecialityDto> PhysicianSpecialities { get; set; }
    }
}
