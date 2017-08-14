using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Business.Entity
{
    public class MammographyHistoryDto
    {
        public long? AppointmentId { get; set; }

        public DateTime DOS { get; set; }

        public string BiradCode { get; set; }

        public string BreastDensity { get; set; }

        public string Type { get; set; }

        public string ProcedureCodes { get; set; }

        public DateTime? LayLetterSent { get; set; }

        //        [DataMember(Name = "I")]
        public string LayLetterSentString
        {
            get { return LayLetterSent == null ? "" : LayLetterSent.Value.ToShortDateString(); }
        }

        public DateTime? FollowUpDate { get; set; }
        public string FollowUpDateString
        {
            get { return FollowUpDate == null ? "" : FollowUpDate.Value.ToShortDateString(); }
        }
        public string MammogramType { get; set; }

        public string Procedures { get; set; }
    }


    public class MammographyHistoriesDto : DtoBase
    {
        public List<MammographyHistoryDto> Entries { get; set; }

        public MammographyHistoriesDto()
        {
            Entries = new List<MammographyHistoryDto>();
        }
    }
}
