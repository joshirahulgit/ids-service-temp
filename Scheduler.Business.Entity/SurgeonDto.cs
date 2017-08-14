using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Business.Entity
{
    public class SurgeonDto
    {
        public long Id { get; set; }


        public int Type { get; set; }

        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }


    public class SurgeonsDto : DtoBase
    {
        public List<SurgeonDto> Entries { get; set; }

        public SurgeonsDto()
        {
            Entries = new List<SurgeonDto>();
        }

        public SurgeonsDto(List<SurgeonDto> entries)
        {
            Entries = entries;
        }
    }
}
