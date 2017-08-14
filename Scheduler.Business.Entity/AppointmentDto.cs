using Scheduler.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Business.Entity
{
    public class AppointmentDto : DtoBase
    {
        private string _defaultOrderPriority;
        private long _appointmentId;
        private List<string> _appointmentProcedureWithLocationAlerts = new List<string>();
        private long _statusId;


        public long AppointmentID
        {
            get { return _appointmentId; }
            set { _appointmentId = value; }
        }

        public String Number { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public List<AppointmentResourceDto> Resources { get; set; }

        public long StatusID
        {
            get { return _statusId; }
            set { _statusId = value; }
        }

        public long AccountID { get; set; }

        public VisitDto Visit { get; set; }

        public List<PatientCommentDto> Comments { get; set; }

        public long? LocationId { get; set; }

        public OrderCreateParametersSetDto OrderCreationParams { get; set; }

        public bool UseAttachedParamsForOrderCreation { get; set; }

        public bool IsJustCreated { set; get; }

        public string Worktype { get; set; }

        public string DefaultOrderPriority
        {
            get { return _defaultOrderPriority; }
            set
            {
                _defaultOrderPriority = value;
                //OnPropertyChanged(nameof(DefaultOrderPriority));
            }
        }

        public string PendingReasonCode { get; set; }

        public string PendingReasonText { get; set; }

        public string CreateBy { get; set; }

        public DateTime? CreatedOn { get; set; }

        public string LastModifiedBy { get; set; }

        public DateTime? LastModifiedOn { get; set; }

        public String Comment { get; set; }

        public String TechName { get; set; }

        public String TechUserId { get; set; }

        public bool IsAuthorizationAlert { get; set; }

        public int? PatientVisitId { get; set; }

        public bool IsLocked { get; set; }

        public string IsLockedBy { get; set; }

        public List<AppointmentResourceModalityDto> AffectedVirtualRooms { get; set; }

        public long BaseAppointmentId { get; set; }


        public long GroupedAppointmentId { get; set; }

        public bool IsAssociationToParentRequired { get; set; }

        public List<string> AppointmentProcedureWithLocationAlerts
        {
            get { return _appointmentProcedureWithLocationAlerts; }
            set { _appointmentProcedureWithLocationAlerts = value; }
        }

        public List<PaymentFeeDto> PaymentFees { get; set; }

        public int? GroupId { get; set; }

        public AppointmentDto(AppointmentDto app)
            : this()
        {
            this.IsLocked = app.IsLocked;
            this.IsLockedBy = app.IsLockedBy;
            this.IsJustCreated = app.IsJustCreated;
            this.AppointmentID = app.AppointmentID;
            this.Number = app.Number;
            this.Name = app.Name;
            this.Description = app.Description;
            this.StatusID = app.StatusID;
            this.AccountID = app.AccountID;
            this.PatientVisitId = app.PatientVisitId;
            this.LocationId = app.LocationId;
            this.DefaultOrderPriority = app.DefaultOrderPriority;
            this.PendingReasonCode = app.PendingReasonCode;
            this.PendingReasonText = app.PendingReasonText;
            this.Visit = app.Visit == null ? new VisitDto() : app.Visit.Clone() as VisitDto;
            this.CreateBy = app.CreateBy;
            this.CreatedOn = app.CreatedOn;
            this.LastModifiedBy = app.LastModifiedBy;
            this.LastModifiedOn = app.LastModifiedOn;
            this.Comment = app.Comment;
            this.RescheduleFromHistory = app.RescheduleFromHistory;
            this.RescheduleFromPending = app.RescheduleFromPending;
            this.ScheduleFromPending = app.ScheduleFromPending;
            this.IsOverheadApplied = app.IsOverheadApplied;
            this.IsAuthorizationAlert = app.IsAuthorizationAlert;
            this.AffectedVirtualRooms.AddRange(app.AffectedVirtualRooms);
            this.AppointmentProcedureWithLocationAlerts.AddRange(app.AppointmentProcedureWithLocationAlerts);
            this.PaymentFees = app.PaymentFees;
            this.GroupId = app.GroupId;
            this.RequestedTimeRange = app.RequestedTimeRange;

            foreach (AppointmentResourceDto resource in app.Resources)
                this.Resources.Add(resource.Clone() as AppointmentResourceDto);

            if (app.Comments != null)
            {
                if (this.Comments == null) this.Comments = new List<PatientCommentDto>();
                else this.Comments.Clear();
                foreach (PatientCommentDto comment in app.Comments)
                    this.Comments.Add(new PatientCommentDto(comment));
            }
        }

        public AppointmentDto(long id)
            : this()
        {
            this.AppointmentID = id;
        }

        public AppointmentDto()
        {
            StatusID = 1;//(int)Scheduler.ClientModel.AppointmentStatuses.New;
            this.Resources = new List<AppointmentResourceDto>();
            this.Visit = new VisitDto();
            this.OrderCreationParams = new OrderCreateParametersSetDto();
            AffectedVirtualRooms = new List<AppointmentResourceModalityDto>();
            AppointmentProcedureWithLocationAlerts = new List<string>();
            PaymentFees = new List<PaymentFeeDto>();
        }

        public DateTime SortingStartTime
        {
            get
            {
                return new DateTime(2010, 1, 1, StartDate.Hour, StartDate.Minute, StartDate.Second);
            }
        }

        public string SortingLocation { get; set; }

        public string SortingStatus { get; set; }

        
        public DateTime SortingEndTime
        {
            get
            {
                return new DateTime(2010, 1, 1, EndDate.Hour, EndDate.Minute, EndDate.Second);
            }
        }

        public DateTime? StartDateSafe
        {
            get
            {

                foreach (AppointmentResourceDto res in Resources)
                    if (res is AppointmentResourceTimeDto)
                        return (res as AppointmentResourceTimeDto).StartTime;

                return null;
            }
        }

        public DateTime? EndDateSafe
        {
            get
            {

                foreach (AppointmentResourceDto res in Resources)
                    if (res is AppointmentResourceTimeDto)
                        return (res as AppointmentResourceTimeDto).EndTime;

                return null;
            }
        }

        public DateTime StartDate
        {
            get
            {
                foreach (AppointmentResourceDto res in Resources)
                    if (res is AppointmentResourceTimeDto)
                        return (res as AppointmentResourceTimeDto).StartTime;
                throw new Exception("Invalid appointment: no time resource is specified.");
            }
        }

        public DateTime EndDate
        {
            get
            {
                foreach (AppointmentResourceDto res in Resources)
                    if (res is AppointmentResourceTimeDto)
                        return (res as AppointmentResourceTimeDto).EndTime;
                throw new Exception("Invalid appointment: no time resource is specified.");
            }
        }

        public List<AppointmentResourcePhysicianDto> Physicians
        {
            get { return AppointmentResourceDto.FindAll<AppointmentResourcePhysicianDto>(Resources); }
        }

        public AppointmentResourcePhysicianDto Physician
        {
            get
            {
                foreach (AppointmentResourceDto res in Resources)
                    if (res is AppointmentResourcePhysicianDto)
                        return (res as AppointmentResourcePhysicianDto);

                return null;
            }
        }

        public List<AppointmentResourcePatientDto> Patients
        {
            get
            {
                return AppointmentResourceDto.FindAll<AppointmentResourcePatientDto>(Resources);
            }
        }

        public AppointmentResourcePatientDto Patient
        {
            get
            {
                foreach (AppointmentResourceDto res in Resources)
                    if (res is AppointmentResourcePatientDto)
                        return (res as AppointmentResourcePatientDto);

                return null;
            }
            set
            {
                AppointmentResourcePatientDto currentPat = this.Patient;
                if (currentPat != null)
                    this.Resources.Remove(currentPat);

                if (value != null)
                    this.Resources.Add(value);
            }
        }

        public AppointmentResourceModalityDto Modality
        {
            get
            {
                foreach (AppointmentResourceDto res in Resources)
                    if (res is AppointmentResourceModalityDto)
                        return (res as AppointmentResourceModalityDto);

                return null;
            }
        }

        public List<AppointmentResourceModalityDto> Modalities
        {
            get
            {
                return AppointmentResourceDto.FindAll<AppointmentResourceModalityDto>(Resources);
            }
        }

        public bool IsComplete
        {
            get
            {
                bool flPatient = false, flRoom = false, flTime = false, flPhysician = false;
                foreach (AppointmentResourceDto resource in Resources)
                {
                    if (resource is AppointmentResourceTimeDto) flTime = true;
                    if (resource is AppointmentResourceModalityDto) flRoom = true;
                    if (resource is AppointmentResourcePatientDto) flPatient = true;
                    if (resource is AppointmentResourcePhysicianDto) flPhysician = true;

                }

                return flPatient && flRoom && flTime && flPhysician;

            }
        }

        public bool IsCompleted
        {
            get
            {
                return StatusID == (long)AppointmentStatuses.Complete || StatusID == (long)AppointmentStatuses.TechComplete;
            }
        }

        public ReferralDto Referral
        {
            get
            {
                if (Visit?.Referrals == null)
                {
                    return null;
                }
                if (Visit.Referrals.Count == 0)
                {
                    return null;
                }
                return Visit.Referrals[0];
            }
        }

        public AppointmentOrderDto Order
        {
            get
            {
                if (Visit?.Orders == null)
                {
                    return null;
                }
                if (Visit.Orders.Count == 0)
                {
                    return null;
                }
                return Visit.Orders[0];
            }
        }

        public string CalendarText
        {
            get
            {
                string text = string.Empty;
                if (this.Patient != null)
                    text = this.Patient.FullName;
                else if (StatusID != (long)AppointmentStatuses.Blocked && AppointmentID > 0)
                    text = string.Format("Reserved by {0} @ {1}", CreateBy, CreatedOn);

                return text;
            }
        }

        public override string ToString()
        {
            string ret = string.Format("({0}) ", ((AppointmentStatuses)StatusID).ToString());
            ret += string.Join("/", Resources.Select(a => a.DisplayText).ToArray());
            return ret;
        }

        public string TextForAudit
        {
            get
            {
                TimeSpan ts = EndDate - StartDate;
                return string.Format("AppId: {0}, Status: {1}, Date: {2}, Time: {3} ({4}:{5})", AppointmentID, ((AppointmentStatuses)StatusID).ToString(),
                    StartDate.ToShortDateString(), StartDate.ToShortTimeString(), ts.Hours, ts.Minutes);
            }
        }

        public TimeSpan Length
        {
            get { return EndDate - StartDate; }
        }

        public string ProceduresText
        {
            get
            {
                if (Visit == null) return string.Empty;
                if (Visit.Procedures == null) return string.Empty;
                return string.Join(Environment.NewLine,
                                   Visit.Procedures.Select(p => string.Format("({0}) {1}", p.Code, p.ShortDescription)).ToArray());
            }
        }

        public string ProcedureNamesText
        {
            get
            {
                if (Visit == null) return string.Empty;
                if (Visit.Procedures == null) return string.Empty;
                return string.Join(Environment.NewLine,
                                   Visit.Procedures.Select(p => string.Format("{0}", p.ShortDescription)).ToArray());
            }
        }

        public string ProcedureCodesText
        {
            get
            {
                if (Visit == null) return string.Empty;
                if (Visit.Procedures == null) return string.Empty;
                return string.Join(", ", Visit.Procedures.Select(p => string.Format("({0})", p.Code)).ToArray());
            }
        }

        public bool RescheduleFromHistory { get; set; }

        public bool RescheduleFromPending { get; set; }
        public bool IsOverheadApplied { get; set; }
        public bool ScheduleFromPending { get; set; }

        public TimeSpan StartTime
        {
            get
            {
                return this.StartDateSafe.Value.TimeOfDay;
            }
        }

        public TimeSpan EndTime
        {
            get
            {
                return this.EndDateSafe.Value.TimeOfDay;
            }
        }
        public string RequestedTimeRange { get; set; }

        public bool IsReservation(string currentUser)
        {
            return Patient == null && CreateBy != currentUser &&
                (StatusID != (long)AppointmentStatuses.Blocked &&
                StatusID != (long)AppointmentStatuses.Available);
        }

        public void ClearResourcesExceptPatient()
        {
            if (this.Patient != null)
            {
                AppointmentResourcePatientDto copy = new AppointmentResourcePatientDto(this.Patient);
                this.Resources.Clear();
                this.Resources.Add(copy);
            }
            else
                this.Resources.Clear();
        }

        public static AppointmentDto FindById(List<AppointmentDto> appointments, long id)
        {
            foreach (AppointmentDto appointment in appointments)
            {
                if (appointment.AppointmentID == id) return appointment;
            }
            return null;
        }

        public bool StatusBelongsTo(List<AppointmentStatusDto> statuses)
        {
            if (statuses == null)
                return false;

            return statuses.Count(s => s.StatusID == this.StatusID) > 0;
        }

        public bool ContainsResourcesById(List<AppointmentResourceDto> resources)
        {
            foreach (AppointmentResourceDto resource in resources)
            {
                AppointmentResourceDto dumb;
                if (!AppointmentResourceDto.TryFindById(this.Resources, resource, out dumb))
                    return false;
            }
            return true;
        }

        public void SetStartTime(DateTime minValue)
        {
            foreach (AppointmentResourceDto res in Resources)
                if (res is AppointmentResourceTimeDto)
                    (res as AppointmentResourceTimeDto).StartTime = minValue;
        }

        public void SetEndTime(DateTime minValue)
        {
            foreach (AppointmentResourceDto res in Resources)
                if (res is AppointmentResourceTimeDto)
                    (res as AppointmentResourceTimeDto).EndTime = minValue;
        }

        public void RemovePatient()
        {
            var p = this.Patient;
            if (p != null)
                this.Resources.Remove(p);
        }
        
        private int GetAllowedApptCountBasedOnVirtualRoom(DayOfWeek day)
        {
            int res = int.MaxValue;
            if (this.Modality == null && this.Modality.SchedulerModalityVirtualRoom == null)
                return res;

            var schedulerModalityVirtualRoomDto = this.Modality.SchedulerModalityVirtualRoom;
            if (schedulerModalityVirtualRoomDto != null)
                res = schedulerModalityVirtualRoomDto.GetExamCountForDay(day);

            return res;
        }

        public static List<AppointmentResourceModalityDto> GetAffectedVirtualRooms(List<AppointmentResourceModalityDto> rooms, List<AppointmentResourceModalityDto> allRooms)
        {
            var vgroups = rooms.Where(a => a.VirtualRoomId.HasValue).Select(a => a.VirtualRoomId).Distinct().ToList();
            var result = allRooms.Where(a => vgroups.Contains(a.VirtualRoomId) && !(rooms.Any(r => r.Id == a.Id))).ToList();
            return result;
        }

        //public event PropertyChangedEventHandler PropertyChanged;

        //protected virtual void OnPropertyChanged(string propertyName)
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}
    }

    //public class AppointmentSetDto :List<AppointmentDto>{ }

    public class AppointmentsDto : DtoBase
    {
        public AppointmentsDto()
        {
            Appointments = new List<AppointmentDto>();
        }

        public IList<AppointmentDto> Appointments { get; set; }

        public int AppointmentsCount { get; set; }

        public bool ContainsAppointment(AppointmentDto app)
        {
            if (this.Appointments == null)
                return false;

            foreach (AppointmentDto a in this.Appointments)
                if (a.AppointmentID == app.AppointmentID)
                    return true;

            return false;
        }
    }

    public class AppointmentRequestDto : DtoBase
    {
        public List<AppointmentResourceDto> RequestResources { get; set; }
        public List<AppointmentStatusDto> StatusFilter { get; set; }
        public Dictionary<string, string> Sorter { get; set; }
    }


    public class AppointmentInsuranceInfoDto : DtoBase
    {
        public AppointmentInsuranceInfoDto()
        {
            InitialInsuranceInfo = new List<string>();
            ProceduresInsuranceInfo = new List<string>();
        }

        public List<string> InitialInsuranceInfo { get; set; }

        public List<string> ProceduresInsuranceInfo { get; set; }
    }
}
