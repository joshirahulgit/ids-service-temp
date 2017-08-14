using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.DBEntity
{
    public class PatientVisitHistory : EntityBase
    {
        private DateTime _startDate = DateTime.MinValue;
        private DateTime _endDate = DateTime.MinValue;
        private long _appointmentId;
        private long _patientIntId;
        //        private string _firstName   = String.Empty;
        //        private string _lastName    = String.Empty;
        private string _visitReason = String.Empty;
        private long _status = 0;
        private string _pendingReason;
        private string _pendingReasonCode;


        public PatientVisitHistory()
        {
            //            this.Procedures         = new List<Procedure>();
            //            this.Diagnosises        = new List<Diagnosis>();
            this.UsedAuthorizations = new List<UsedAuthorization>();
            //            this.Referrals          = new List<Referral>();
            this.Providers = new List<AppointmentResourcePhysician>();
            this.OrderReports = new List<OrderReport>();
            this.Visit = new Visit();
        }

        public long PatientVisitHistoryID { get; set; }

        public AppointmentResourcePatient Patient { get; set; }

        //        public List<Procedure> Procedures       { get; private set; }
        //        public List<Diagnosis> Diagnosises      { get; private set; }
        public List<UsedAuthorization> UsedAuthorizations { get; private set; }
        //        public List<Referral> Referrals         { get; private set; }
        public List<AppointmentResourcePhysician> Providers { get; private set; }
        public List<OrderReport> OrderReports { get; private set; }

        public Visit Visit { get; set; }

        public long Status
        {
            get { return _status; }
            set { _status = value; }
        }

        public DateTime StartDate
        {
            get { return _startDate; }
            set { _startDate = value; }
        }

        public int ModalityId { get; set; }
        public string ModalityName { get; set; }

        public int LocationId { get; set; }
        public string LocationName { get; set; }

        public DateTime EndDate
        {
            get { return _endDate; }
            set { _endDate = value; }
        }

        public long AppointmentId
        {
            get { return _appointmentId; }
            set { _appointmentId = value; }
        }

        public long PatientIntId
        {
            get { return _patientIntId; }
            set { _patientIntId = value; }
        }

        //        public string PatientName 
        //        {
        //            get { return String.Format("{0} {1}", _firstName, _lastName); }
        //        }
        //
        //        public string FirstName
        //        {
        //            get { return _firstName; }
        //            set { _firstName = value; }
        //        }
        //
        //        public string LastName
        //        {
        //            get { return _lastName; }
        //            set { _lastName = value; }
        //        }


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

        public string VisitReason
        {
            get { return _visitReason; }
            set { _visitReason = value; }
        } 

    }
}
