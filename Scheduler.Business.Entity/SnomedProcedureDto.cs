using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Business.Entity
{
    public class SnomedProcedureDto : DtoBase
    {
        public string SnomedCode { get; set; }

        public string ShortDescription { get; set; }

        public string MediumDescription { get; set; }

        public string LongDescription { get; set; }

        public bool IsEncounterCode { get; set; }

        public long Id { get; set; }
    }

    public class SnomedsDto : DtoBase
    {
        public SnomedsDto()
        {
            this.SnomedProcedures = new List<SnomedProcedureDto>();
        }

        public IList<SnomedProcedureDto> SnomedProcedures { get; set; }
    }
}
