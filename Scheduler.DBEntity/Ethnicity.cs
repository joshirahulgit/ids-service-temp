using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.DBEntity
{
    public class Ethnicity : EntityBase
    {
        public new int Id { get; set; }
        public string Description { get; set; }

        //public static EthnicityDto Convert2Dto(Ethnicity eth)
        //{
        //    EthnicityDto r = new EthnicityDto();
        //    r.Description = eth.Description;
        //    r.Id = eth.Id;
        //    return r;
        //}

        //public static Ethnicity ExtractFromDto(EthnicityDto eth)
        //{
        //    Ethnicity r = new Ethnicity();
        //    r.Description = eth.Description;
        //    r.Id = eth.Id;
        //    return r;
        //}
    }
}
