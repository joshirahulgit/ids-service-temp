using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.DBEntity
{
    public class SnomedProcedure : EntityBase
    {
        public string SnomedCode { get; set; }
        public string ShortDescription { get; set; }
        public string MediumDescription { get; set; }
        public string LongDescription { get; set; }
        public bool IsEncounterCode { get; set; }

        public SnomedProcedure()
        {
        }

        public SnomedProcedure(string snomedCode, string shortDescription, string mediumDescription, string longDescription, bool isEncounterCode)
        {
            SnomedCode = snomedCode;
            ShortDescription = shortDescription;
            MediumDescription = mediumDescription;
            LongDescription = longDescription;
            IsEncounterCode = isEncounterCode;
        }

        //public static SnomedProcedureDto Convert2Dto(SnomedProcedure procedure)
        //{
        //    SnomedProcedureDto r = new SnomedProcedureDto();
        //    r.Id = procedure.Id;
        //    r.IsEncounterCode = procedure.IsEncounterCode;
        //    r.LongDescription = procedure.LongDescription;
        //    r.MediumDescription = procedure.MediumDescription;
        //    r.ShortDescription = procedure.ShortDescription;
        //    r.SnomedCode = procedure.SnomedCode;
        //    return r;
        //}
    }
}
