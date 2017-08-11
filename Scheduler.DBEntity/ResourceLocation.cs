using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.DBEntity
{
    public class ResourceLocation : EntityBase
    {
        public ResourceLocation()
        {

        }

        public ResourceLocation(long id)
            : this()
        {
            this.Id = id;
        }

        public virtual String LocationName { get; set; }
        public virtual String AbbadoxLocation { get; set; }
        public virtual String Address { get; set; }
        public virtual Area Area { get; set; }
        public string Zip { get; set; }
        public bool IsForceStateMatch { get; set; }
        public string LocationAlert { get; set; }
        public string PathToImage { get; set; }

        //internal static ResourceLocation ExtractFromDto(ResourceLocationDto l)
        //{
        //    ResourceLocation res = new ResourceLocation();
        //    res.Address = l.Address;
        //    res.Area = Area.ExtractFromDto(new ResourceAreaDto() { Id = l.AreaID });
        //    res.Id = l.Id;
        //    res.LocationName = l.Text;
        //    res.AbbadoxLocation = l.AbbadoxLocation;
        //    res.Area.City = l.City;
        //    res.Area.State = l.State;
        //    res.Zip = l.Zip;
        //    res.PathToImage = l.PathToImage;
        //    return res;
        //}

        //public static ResourceLocationDto Convert2Dto(ResourceLocation location)
        //{
        //    ResourceLocationDto dto = new ResourceLocationDto();
        //    dto.Id = location.Id;
        //    dto.Address = location.Address;
        //    dto.AreaID = location.Area == null ? -1 : location.Area.Id;
        //    dto.Name = location.LocationName;
        //    dto.City = location.Area == null ? string.Empty : location.Area.City;
        //    dto.State = location.Area == null ? string.Empty : location.Area.State;
        //    dto.Zip = location.Zip;
        //    dto.IsForceStateMatch = location.IsForceStateMatch;
        //    dto.LocationAlert = location.LocationAlert;
        //    dto.PathToImage = location.PathToImage;
        //    return dto;
        //}
    }
}
