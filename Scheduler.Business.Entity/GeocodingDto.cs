using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Business.Entity
{
    public class GeocodingDto : DtoBase
    {
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }

    public class GeocodingsDto : DtoBase
    {
        public List<GeocodingDto> Entries { get; set; }

        public GeocodingsDto()
        {
            Entries = new List<GeocodingDto>();
        }

        public GeocodingsDto(List<GeocodingDto> entries)
        {
            Entries = entries;
        }
    }
}
