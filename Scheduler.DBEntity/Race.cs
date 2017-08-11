using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.DBEntity
{
    public class Race : EntityBase
    {
        public new int Id { get; set; }
        public string Description { get; set; }
        public string HL7Code { get; set; }

        //public static Race ExtractFromDto(RaceDto race)
        //{
        //    Race r = new Race();
        //    r.Description = race.Description;
        //    r.Id = race.Id;
        //    r.HL7Code = race.HL7Code;
        //    return r;
        //}

        //public static RaceDto Convert2Dto(Race item)
        //{
        //    RaceDto r = new RaceDto();
        //    r.Id = item.Id;
        //    r.Description = item.Description;
        //    r.HL7Code = item.HL7Code;

        //    return r;
        //}

        
    }
}
