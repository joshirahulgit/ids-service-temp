using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.DBEntity
{
    public class ModalityType : EntityType
    {
        public int ModalityId { get; set; }
        public SchedulerImage Icon { get; set; }
        public bool AllowComparision { get; set; }
        public int LocationId { get; set; }
        public string LocationCode { get; set; }

        public ModalityType()
        {
        }

        public ModalityType(long id)
            : this()
        {
            this.Id = id;
        }

        public ModalityType(long id, string name)
            : this(id)
        {
            this.Name = name;
        }

        //internal static ModalityType ExtractFromDto(Common.DataTransferObjects.Types.ModalityTypeDto m)
        //{
        //    ModalityType res = new ModalityType();
        //    res.Id = m.TypeId;
        //    res.AllowComparision = m.AllowComparision;
        //    res.Name = m.TypeName;
        //    res.Icon = SchedulerImage.ExtractFromDto(m.Icon);
        //    res.LocationId = m.LocationId;
        //    res.LocationCode = m.LocationCode;
        //    res.AllowComparision = m.AllowComparision;
        //    return res;
        //}
    }
}
