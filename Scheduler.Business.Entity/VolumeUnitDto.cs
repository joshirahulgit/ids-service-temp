using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Business.Entity
{
    public class VolumeUnitDto : DtoBase
    {
        public long Id { get; set; }

        public String DisplayName { get; set; }

        public Boolean IsVisible { get; set; }

        public Boolean IsDeleted { get; set; }

    }

    public class VolumeUnitsDto : DtoBase
    {
        public VolumeUnitsDto()
        {
            this.VolumeUnits = new List<VolumeUnitDto>();
        }

        public IList<VolumeUnitDto> VolumeUnits { get; set; }
    }
}
