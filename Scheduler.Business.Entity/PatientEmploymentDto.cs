using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Business.Entity
{
    public class PatientEmploymentDto : DtoBase
    {
        public PatientEmploymentDto()
        {
            Address = new AddressDto();
        }

        public long Id { get; set; }

        public long PatientId { get; set; }

        public AddressDto Address { get; set; }

        public string EmploymentStatus { get; set; }

        public string EmployerName { get; set; }
    }


    public class PatientEmploymentsDto : DtoBase
    {
        public PatientEmploymentsDto()
        {
            PatientEmployments = new List<PatientEmploymentDto>();
        }

        public IList<PatientEmploymentDto> PatientEmployments { get; set; }
    }
}
