using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Business.Entity
{
    public class SchedulerIntegrationAddressDto
    {
        public String Address { get; set; }

        public String Address1 { get; set; }

        public String City { get; set; }

        public String State { get; set; }

        public String ZipCode { get; set; }

        public String Country { get; set; }
    }

    public class SchedulerIntegrationAddressesDto : DtoBase
    {
        public IList<SchedulerIntegrationAddressDto> Entries { get; set; }

        public SchedulerIntegrationAddressesDto()
        {
            Entries = new List<SchedulerIntegrationAddressDto>();
        }
    }
}
