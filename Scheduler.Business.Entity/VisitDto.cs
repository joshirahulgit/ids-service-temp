using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Business.Entity
{
    public class VisitDto : DtoBase
    {
        public VisitDto()
        {
            this.Procedures = new List<ProcedureDto>();
            this.Diagnoses = new List<DiagnosisDto>();
            this.Referrals = new List<ReferralDto>();
            this.Orders = new List<AppointmentOrderDto>();
            this.UsedAuthorizations = new List<UsedAuthorizationDto>();
            this.AttachedGuarantors = new List<long>();
            this.Mammography = new MammographyDto();
            this.MammographyHistory = new MammographyHistoriesDto();
            this.SpecialNeed = new List<string>();
            this.AppointmentCheckListItems = new List<AppointmentCheckListValueDto>();
            this.QuestionnaireCollection = new Dictionary<string, bool>();
            VisitID = -1;
        }

        public long VisitID { get; set; }

        public List<ProcedureDto> Procedures { get; set; }

        public List<DiagnosisDto> Diagnoses { get; set; }

        public List<ReferralDto> Referrals { get; set; }

        public float PatientHeight { get; set; }

        public float PatientWeight { get; set; }

        public String Smoking { get; set; }

        public List<String> SpecialNeed { get; set; }

        public String BPFrom { get; set; }

        public String BPTo { get; set; }

        public String VisitReason { get; set; }

        public String PatientCategory { get; set; }

        public String PatientAilment { get; set; }

        public DateTime? DOI { get; set; }

        public bool HasImplants { get; set; }

        public bool IsPregnant { get; set; }

        public String StateOfAccident { get; set; }

        public bool NoAuthRequired { get; set; }

        public String NoAuthRequiredComment { get; set; }


        public List<AppointmentOrderDto> Orders { get; set; }

        public List<UsedAuthorizationDto> UsedAuthorizations { get; set; }

        public String ReferringNotes { get; set; }

        public String EnumScheduledBy { get; set; }

        public List<long> AttachedGuarantors { get; set; }

        public MammographyDto Mammography { get; set; }

        public MammographyHistoriesDto MammographyHistory { get; set; }

        public string PlaceOfService { get; set; }

        public int ImageCount { get; set; }

        public int SeriesCount { get; set; }

        public bool HasOnlineAppointment { get; set; }

        public int? OnlineAppointmentId { get; set; }

        public List<AppointmentCheckListValueDto> AppointmentCheckListItems { get; set; }

        public string EhrTransitionGuid { get; set; }

        public Dictionary<string, bool> QuestionnaireCollection { get; set; }

        #region ICloneable Members

        public object Clone()
        {
            VisitDto res = new VisitDto();

            if (this.Referrals != null)
            {
                foreach (ReferralDto dto in Referrals)
                {
                    res.Referrals.Add(new ReferralDto(dto) { Speciality = dto.Speciality });
                }
            }

            foreach (ProcedureDto pt in this.Procedures)
                res.Procedures.Add(new ProcedureDto(pt));

            foreach (DiagnosisDto d in this.Diagnoses)
                res.Diagnoses.Add(new DiagnosisDto(d));

            foreach (AppointmentOrderDto order in this.Orders)
                res.Orders.Add(new AppointmentOrderDto(order));

            foreach (UsedAuthorizationDto auth in this.UsedAuthorizations)
                res.UsedAuthorizations.Add(new UsedAuthorizationDto(auth));

            res.PatientHeight = this.PatientHeight;
            res.PatientWeight = this.PatientWeight;
            res.BPFrom = this.BPFrom;
            res.BPTo = this.BPTo;
            res.Smoking = this.Smoking;
            res.VisitReason = this.VisitReason;
            res.PatientCategory = this.PatientCategory;
            res.PatientAilment = this.PatientAilment;
            res.DOI = this.DOI;
            res.HasImplants = this.HasImplants;
            res.IsPregnant = this.IsPregnant;
            res.StateOfAccident = this.StateOfAccident;
            res.NoAuthRequired = this.NoAuthRequired;
            res.NoAuthRequiredComment = this.NoAuthRequiredComment;
            res.HasOnlineAppointment = this.HasOnlineAppointment;
            res.ReferringNotes = this.ReferringNotes;
            res.EnumScheduledBy = this.EnumScheduledBy;
            res.SeriesCount = this.SeriesCount;
            res.ImageCount = this.ImageCount;
            res.SpecialNeed = new List<string>();
            var specialNeed = this.SpecialNeed;
            if (specialNeed != null)
                res.SpecialNeed.AddRange(specialNeed);

            res.VisitID = this.VisitID;
            res.EhrTransitionGuid = this.EhrTransitionGuid;
            res.Mammography = new MammographyDto(this.Mammography);
            res.AppointmentCheckListItems = AppointmentCheckListItems?.Select(s => s).ToList();
            foreach (KeyValuePair<string, bool> pair in QuestionnaireCollection)
                res.QuestionnaireCollection.Add(pair.Key, pair.Value);
            return res;
        }

        #endregion

        public double CalculateBMI()
        {
            return CalculateBMI(this.PatientWeight, this.PatientHeight);
        }

        public static double CalculateBMI(double weightLbs, double heightIn)
        {
            if (heightIn != 0)
                return (703 * (double)weightLbs / (heightIn * heightIn));
            return 0;
        }
    }
}
