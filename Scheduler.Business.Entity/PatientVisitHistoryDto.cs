using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Business.Entity
{
    public class PatientVisitHistoryDto : DtoBase
    {
        private DateTime _startDate = DateTime.MinValue;
        private DateTime _endDate = DateTime.MinValue;
        //        private string _firstName = String.Empty;
        //        private string _lastName = String.Empty;
        private string _visitReason = String.Empty;
        private int _modalityId;
        private string _modalityName;
        private long _status = 0;
        private string _locationName;
        private int _locationId;
        private string _pendingReason;
        private long _patientId;
        private string _pendingReasonCode;


        public PatientVisitHistoryDto()
        {
            //            this.Procedures             = new List<ProcedureDto>();
            //            this.Diagnoses              = new List<DiagnosisDto>();
            //            this.UsedAuthorizations     = new List<UsedAuthorizationDto>();
            //            this.Referrals              = new List<ReferralDto>();
            this.Providers = new List<AppointmentResourcePhysicianDto>();
            this.Patient = new AppointmentResourcePatientDto();
            this.OrderReports = new List<OrderReportDto>();
            this.Visit = new VisitDto();
        }

        public bool IsSelected { get; set; }

        public long PatientVisitHistoryID { get; set; }

        public DateTime StartDate
        {
            get { return _startDate; }
            set { _startDate = value; }
        }

        public string DOS
        {
            get { return _startDate.ToShortDateString() + " " + _startDate.ToShortTimeString(); }
        }

        public DateTime EndDate
        {
            get { return _endDate; }
            set { _endDate = value; }
        }
        /*
                public string PatientFirst
                {
                    get { return _firstName; }
                }

                public string PatientName 
                {
                    get { return String.Format("{0} {1}", _firstName, _lastName); }
                }
                [DataMember(Name = "F")]
                public string FirstName
                {
                    get { return _firstName; }
                    set { _firstName = value; }
                }
                [DataMember(Name = "G")]
                public string LastName
                {
                    get { return _lastName; }
                    set { _lastName = value; }
                }*/

        public string VisitReason
        {
            get { return _visitReason; }
            set { _visitReason = value; }
        }


        public int ModalityId
        {
            get { return _modalityId; }
            set { _modalityId = value; }
        }

        public string ModalityName
        {
            get { return _modalityName; }
            set { _modalityName = value; }
        }

        public string PendingReason
        {
            get { return _pendingReason; }
            set { _pendingReason = value; }
        }

        public string PendingReasonCode
        {
            get { return _pendingReasonCode; }
            set { _pendingReasonCode = value; }
        }

        public long PatientId
        {
            get { return _patientId; }
            set { _patientId = value; }
        }

        public string LocationName
        {
            get { return _locationName; }
            set { _locationName = value; }
        }

        public int LocationId
        {
            get { return _locationId; }
            set { _locationId = value; }
        }


        //        [DataMember(Name = "L")]
        //        public List<ProcedureDto> Procedures { get; set; }

        //        [DataMember(Name = "M")]
        //        public List<DiagnosisDto> Diagnoses { get; set; }

        //        [DataMember(Name = "N")]
        //        public List<UsedAuthorizationDto> UsedAuthorizations { get; set; }

        //        [DataMember(Name = "O")]
        //        public List<ReferralDto> Referrals { get; set; }

        public VisitDto Visit { get; set; }

        public List<AppointmentResourcePhysicianDto> Providers { get; set; }

        public AppointmentResourcePatientDto Patient { get; set; }

        public long Status
        {
            get { return _status; }
            set { _status = value; }
        }

        public List<OrderReportDto> OrderReports { get; set; }

        #region ICloneable Members

        public object Clone()
        {
            PatientVisitHistoryDto res = new PatientVisitHistoryDto();
            //
            //            if (this.Referrals != null)
            //            {
            //                foreach (ReferralDto dto in Referrals)
            //                {
            //                    res.Referrals.Add(new ReferralDto(dto) { Speciality = dto.Speciality });
            //                }
            //            }
            //
            //            foreach (ProcedureDto pt in this.Procedures)
            //                res.Procedures.Add(new ProcedureDto(pt));
            //
            //            foreach (DiagnosisDto d in this.Diagnoses)
            //                res.Diagnoses.Add(new DiagnosisDto(d));

            //            foreach (UsedAuthorizationDto auth in this.UsedAuthorizations)
            //                res.UsedAuthorizations.Add(new UsedAuthorizationDto(auth));

            foreach (OrderReportDto orderReportDto in this.OrderReports)
                res.OrderReports.Add(new OrderReportDto(orderReportDto));

            res.StartDate = this.StartDate;
            res.EndDate = this.EndDate;
            //            res.FirstName   = this.FirstName;
            //            res.LastName    = this.LastName;
            res.Patient = this.Patient;
            res.VisitReason = this.VisitReason;

            res.PatientVisitHistoryID = this.PatientVisitHistoryID;

            return res;
        }

        #endregion
    }
    public class PatientVisitHistoriesDto : DtoBase
    {
        public PatientVisitHistoriesDto()
        {
            this.VisitHistories = new List<PatientVisitHistoryDto>();
        }

        public List<PatientVisitHistoryDto> VisitHistories { get; set; }
    }
}
