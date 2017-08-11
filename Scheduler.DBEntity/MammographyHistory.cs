using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.DBEntity
{
    public class MammographyHistory : EntityBase
    {
        public DateTime DOS { get; set; }
        public string BiradCode { get; set; }
        public string BreastDensity { get; set; }
        public string Type { get; set; }
        public string ProcedureCodes { get; set; }
        public DateTime? LayLetterSent { get; set; }
        public DateTime? FollowUpDate { get; set; }
        public long? AppointmentId { get; set; }
        public string MammogramType { get; set; }
        public string Procedures { get; set; }

        //public static MammographyHistory ExtractFromDto(MammographyHistoryDto m)
        //{
        //    MammographyHistory r = new MammographyHistory();

        //    r.DOS = m.DOS;
        //    r.BiradCode = m.BiradCode;
        //    r.BreastDensity = m.BreastDensity;
        //    r.Type = m.Type;
        //    r.ProcedureCodes = m.ProcedureCodes;
        //    r.Procedures = m.Procedures;
        //    r.LayLetterSent = m.LayLetterSent;
        //    r.FollowUpDate = m.FollowUpDate;

        //    return r;
        //}

        //public static MammographyHistoryDto Convert2Dto(MammographyHistory m)
        //{
        //    MammographyHistoryDto r = new MammographyHistoryDto();

        //    r.AppointmentId = m.AppointmentId;
        //    r.DOS = m.DOS;
        //    r.BiradCode = m.BiradCode;
        //    r.BreastDensity = m.BreastDensity;
        //    r.MammogramType = m.MammogramType;
        //    r.Type = m.Type;
        //    r.ProcedureCodes = m.ProcedureCodes;
        //    r.Procedures = m.Procedures;
        //    r.LayLetterSent = m.LayLetterSent;
        //    r.FollowUpDate = m.FollowUpDate;

        //    return r;
        //}

    }
}
