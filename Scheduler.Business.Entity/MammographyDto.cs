using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Business.Entity
{
    public class MammographyDto : DtoBase
    {
        public MammographyDto()
        {
            RightBreast = new PathologyDto();
            LeftBreast = new PathologyDto();
            Tumors = new List<TumorDto>();
        }

        public MammographyDto(MammographyDto m)
        {
            Id = m.Id;
            AppointmentID = m.AppointmentID;
            Laterality = m.Laterality;
            BreastDensity = m.BreastDensity;
            SurgeonName = m.SurgeonName;
            BIRADcode = m.BIRADcode;
            NotifiedBy = m.NotifiedBy;
            NotifiedDate = m.NotifiedDate;
            LayletterSentDate = m.LayletterSentDate;
            FollowUpDate = m.FollowUpDate;
            Tumors = new List<TumorDto>(m.Tumors.Select(t => new TumorDto(t)));
            IsDiscordant = m.IsDiscordant;
            IsPregnant = m.IsPregnant;

            HasImplants = m.HasImplants;

            LeftBreast = new PathologyDto(m.LeftBreast);
            RightBreast = new PathologyDto(m.RightBreast);

            Procedures = m.Procedures;

            IsDeleted = m.IsDeleted;
        }

        public long Id { get; set; }

        public long AppointmentID { get; set; }

        public string Laterality { get; set; }

        public string BreastDensity { get; set; }

        public string SurgeonName { get; set; }

        public string BIRADcode { get; set; }

        public string NotifiedBy { get; set; }

        public DateTime? NotifiedDate { get; set; }

        public DateTime? LayletterSentDate { get; set; }

        public DateTime? FollowUpDate { get; set; }

        public List<TumorDto> Tumors { get; set; }

        public bool? IsDiscordant { get; set; }

        public bool? IsPregnant { get; set; }

        public bool? HasImplants { get; set; }

        public PathologyDto LeftBreast { get; set; }

        public PathologyDto RightBreast { get; set; }

        public string Procedures { get; set; }

        public bool IsDeleted { get; set; }
    }
}
