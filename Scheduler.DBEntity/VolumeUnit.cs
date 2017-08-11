using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.DBEntity
{
    public class VolumeUnit : EntityBase
    {
        public VolumeUnit()
        {
        }

        public VolumeUnit(long id, string displayName, bool isVisible)
        {
            Id = id;
            DisplayName = displayName;
            IsVisible = isVisible;
        }

        public string DisplayName { get; private set; }
        public bool IsVisible { get; private set; }
        public bool IsDeleted { get; private set; }

        //public static VolumeUnitDto Convert2Dto(VolumeUnit unit)
        //{
        //    VolumeUnitDto r = new VolumeUnitDto();
        //    r.Id = unit.Id;
        //    r.DisplayName = unit.DisplayName;
        //    r.IsVisible = unit.IsVisible;
        //    r.IsDeleted = unit.IsDeleted;
        //    return r;
        //}

        //public static VolumeUnit ExtractFromDto(VolumeUnitDto unit)
        //{
        //    VolumeUnit r = new VolumeUnit();
        //    r.Id = unit.Id;
        //    r.DisplayName = unit.DisplayName;
        //    r.IsVisible = unit.IsVisible;
        //    r.IsDeleted = unit.IsDeleted;
        //    return r;
        //}
    }
}
