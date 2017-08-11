using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Business.Entity
{
    public class RaceDto //: DtoBase
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public string HL7Code { get; set; }


        public string Text
        {
            get { return Description; }
        }

        public override string ToString()
        {
            return Text;
        }
    }

    public class RacesDto //: DtoBase
    {
        public RacesDto()
        {
            Races = new List<RaceDto>();
        }

        public IList<RaceDto> Races { get; set; }
    }
}
