using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Business.Entity
{
    public class EthnicityDto //: DtoBase
    {
        public int Id { get; set; }
        public string Description { get; set; }
    }


    public class EthnicitiesDto //: DtoBase
    {
        public EthnicitiesDto()
        {
            this.Entities = new List<EthnicityDto>();
        }

        public List<EthnicityDto> Entities { get; set; }

    }
}
