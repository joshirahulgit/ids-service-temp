using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Business.Entity
{
    public enum ReservationViewMode
    {
        Calendar,
        ListView,
    }

    public enum ScheduleMode
    {
        FromDate,
        FromPatient,
        SchedulePending,
    }

    public class AccountDto //: DtoBase
    {
        //private ResourceLocationDto _defaultLocation;


        public string Name
        {
            get { return AccountName; }
        }
        #region Data Members 

        //public List<NotificationSlotDto> NotificationSlots { get; set; }


        public long Id { get; set; }

        public bool MRNReadOnly { get; set; }
        public bool IsMammographyActive { get; set; }

        public String AccountName { get; set; }


        public String State { get; set; }
        /*style part*/
        public String LogoUrl { get; set; }

        public String Address { get; set; }

        public String Address2 { get; set; }

        public String City { get; set; }

        public String ZipCode { get; set; }

        public String Phone { get; set; }

        public string Text
        {
            get { return AccountName; }
        }

        //public List<AppointmentResourceTypeDto> Types { get; set; }

        //        [DataMember(Name = "WD")]
        //        public List<DayOfWeek> WorkingDays { get; set; }

        //        [DataMember(Name = "D")]
        //        public DayTime StartWorkingTime { get; set; }
        //
        //        [DataMember(Name = "E")]
        //        public DayTime FinishWorkingTime { get; set; }

        public int HourDivisionSegment { get; set; }

        //public List<PhysicianTypeDto> PhysicianTypes { get; set; }

        public ReservationViewMode DefaultViewMode { get; set; }

        public ScheduleMode ScheduleMode { get; set; }

        //public List<AppointmentResourceTypeDto> ResourceTypes { get; set; }

        //public List<ModalityTypeDto> ModalityTypes { get; set; }

        //public List<TechCompleteSuggestionListDto> TechCompleteSuggestionList { get; set; }

        //public List<ResourceAreaDto> ResouceAreas { get; set; }

        //public List<ResourceLocationDto> ResourceLocations { get; set; }

        //public List<AppointmentStatusDto> AppointmentStatuses { get; set; }

        //public List<AppointmentResourceDto> PreCachedResourses { get; set; }

        //public List<AppointmentResourceDto> UnlinkedResourses { get; set; }

        //public List<CommentTypeDto> CommentTypes { get; set; }

        public Dictionary<long, string> ColorsConfiguration { get; set; }

        public Dictionary<string, string> AvailablePayers { get; set; }

        //public PhysicianSpecialitiesDto ReferralSpecialities { get; set; }

        //public VolumeUnitsDto VolumeUnits { get; set; }

        public AccountEnumsDto VisitCategories { get; set; }

        public Dictionary<string, string> AvailableProviders { get; set; }

        public Dictionary<string, string> AvailableInsRelationships { get; set; }

        //public PrintOptions DefaultPrintOption { get; set; }

        public Dictionary<String, String> AvailableLanguages { get; set; }

        public string PrintPatientDetailsPage { get; set; }

        public string PrintRoomSchedulePage { get; set; }

        public string PrintPrepNotePage { get; set; }

        public string PrintCommentsPage { get; set; }

        public string PrintPaymentPage { get; set; }

        public string PrintInvoicePage { get; set; }

        public string OrderReportPage { get; set; }

        public int StartWeekOn { get; set; }

        public int NumberOfVisibleHours { get; set; }

        public Dictionary<string, Dictionary<string, string>> WorkTypes { get; set; }

        public bool AccountProceduresAreAvailable { get; set; }

        public bool AccountDiagnosesAreAvailable { get; set; }

        //public List<CodeCategoryDto> CodeCategories { get; set; }

        //public UserProfileDto CurrentProfile { get; set; }

        public List<UserProfileDto> AllProfiles { get; set; }

        //Automatic order creation support 
        //public OrderCreationMode OrderCreationMode { get; set; }

        public List<int> OrderCreationTrigger { get; set; }

        public List<int> VisitCreationTrigger { get; set; }

        public String WorkTypeSourceTable { get; set; }

        public String WorkTypeSourceColumn { get; set; }

        //public List<OrderCreationParameterDto> OrderCreationParameters { get; set; }

        public bool HasAccessToScheduler { get; set; }

        public bool IsCrmEnabled { get; set; }

        public bool IsSchedulerAdmin { get; set; }

        public bool IsWorkWithPatientVisitAllowed { get; set; }

        //public ResourceLocationDto DefaultLocation
        //{
        //    get
        //    {
        //        if (ResourceLocations.Count > 0)
        //            return ResourceLocations[0];
        //        return _defaultLocation;
        //    }
        //    set { _defaultLocation = value; }
        //}

        public bool IsLocationFilterVis { get; set; }

        public bool IsModalityFilterVis { get; set; }

        public bool IsRoomFilterVis { get; set; }

        public bool IsRoleFilterVis { get; set; }

        public bool IsProviderFilterVis { get; set; }

        public bool IsApptStatusFilterVis { get; set; }

        public bool IsDaysFilterVis { get; set; }

        public bool IsPhyGroupVis { get; set; }

        public bool IsWtGroupVis { get; set; }


        public bool IsPaymentsEnabled { get; set; }

        public bool IsProcessPaymentsEnabled { get; set; }
        //public List<MarketingRepDto> AllMarketingReps { get; set; }


        public AccountSettingCollectionDto AccountSettings { get; set; }

        //[DataMember(Name = "IRR")]
        //public bool? IsReferralRequired
        //{
        //    get
        //    {
        //        var referringParam = OrderCreationParameters.FirstOrDefault(a => a.ParamName == "PhysicianId");
        //        if (referringParam == null) return null;
        //        return referringParam.IsRequired;
        //    }
        //    set
        //    {
        //        var referringParam = OrderCreationParameters.FirstOrDefault(a => a.ParamName == "PhysicianId");
        //        if (referringParam != null && value.HasValue)
        //        {
        //            referringParam.IsRequired = value.Value;
        //            referringParam.DefaultValue = value.Value ? null : "Unknown";
        //        }
        //    }
        //}

        public bool? ViewPatientsInValidLocationsOnly { get; set; }

        public bool IsBillingNoteRequired { get; set; }

        public bool IsCreateOrderRequired { get; set; }

        public bool IsPendingEnabled { get; set; }

        public bool IsVisitReasonRequired { get; set; }

        //public ProcedureExpansionMode ProcedureExpansionMode { get; set; }

        //public PayersSearchMode PayersSearchMode { get; set; }

        public bool PreselectProcedureTypes { get; set; }

        public Dictionary<string, string> PayerStates { get; set; }

        public Dictionary<string, string> UsaStates { get; set; }

        //public List<AccountGenerateIDconfigDto> GenerateIDconfigs { get; set; }

        //public List<AuthorizationAlertDto> AutorizationAlerts { get; set; }

        public bool PatientCategoryRequired { get; set; }

        public bool IsProcedureGlobalSearchEnabled { get; set; }


        public string SendEmailFromAddress { get; set; }

        public bool IsDictator { get; set; }

        public bool IsAdmin { get; set; }

        public AccountEnumsDto HCPCSCodes { get; set; }
        public AccountEnumsDto EnumsScheduledBy { get; set; }
        public AccountEnumsDto EnumsPatientAilment { get; set; }
        public AccountEnumsDto EnumsHeardOfUs { get; set; }
        public AccountEnumsDto EnumsMaritalStatus { get; set; }
        public AccountEnumsDto EnumContactRelation { get; set; }
        public AccountEnumsDto EnumFiltersConfiguration { get; set; }
        public AccountEnumsDto EnumContactType { get; set; }
        public AccountEnumsDto GuarantorRelationShip { get; set; }
        public AccountEnumsDto EnumPriority { get; set; }
        public AccountEnumsDto EnumPendingReason { get; set; }
        public AccountEnumsDto EnumEmploymentStatus { get; set; }
        public AccountEnumsDto EnumDiagnosisFlags { get; set; }
        public AccountEnumsDto EnumReferralGroups { get; set; }
        public AccountEnumsDto EnumPaymentStatuses { get; set; }
        public AccountEnumsDto EnumCreditCardTypes { get; set; }
        public AccountEnumsDto EnumReferralSpecialities { get; set; }
        //public WorkingScheduleDto WorkingSchedule { get; set; }
        public AccountEnumsDto EnumAuthorizationUserStatuses { get; set; }
        public AccountEnumsDto EnumMultipleIdentifierSource { get; set; }
        public AccountEnumsDto EnumPatientCommentTransferredTo { get; set; }
        public bool IsWarningMessagesEnabled { get; set; }
        public bool IsCommentForBlockingRequired { get; set; }
        public bool IsPatientDOBMandatory { get; set; }
        //public AccessControlListDto CurrentAcl { get; set; }
        public List<UserRoleDto> AvailableRoles { get; set; }
        //public AccessControlListDto AvailableAccessControlEntities { get; set; }
        public bool IsScheduleAppointmentByEstimationSlots { get; set; }

        public bool IsStateOfServiceEnabled { get; set; }

        public bool IsProcedureRequired { get; set; }

        public AccountEnumsDto EnumMammoLaterality { get; set; }

        public AccountEnumsDto EnumMammoMammogramType { get; set; }

        public AccountEnumsDto EnumMammoMammogramSubType { get; set; }

        public AccountEnumsDto EnumMammoNodalStatus { get; set; }

        public AccountEnumsDto EnumMammoTumorSize { get; set; }

        public AccountEnumsDto EnumMammoBiopsyType { get; set; }

        public AccountEnumsDto EnumMammoBirads { get; set; }

        public AccountEnumsDto EnumMammoBreastDensity { get; set; }

        public AccountEnumsDto EnumTestResultStatus { get; set; }


        public AccountEnumsDto AllGenders { get; set; }

        public RacesDto AllRaces { get; set; }

        public EthnicitiesDto AllEthnicity { get; set; }

        public AccountEnumsDto AllSmoking { get; set; }

        public AccountEnumsDto AllPatientStatuses { get; set; }

        public AccountEnumsDto AllSpecialNeeds { get; set; }

        public AccountEnumsDto AllRelationships { get; set; }

        public AccountEnumsDto AllEhrSystems { get; set; }

        //public List<AppointmentCheckListItemDto> AppointmentCheckListItems { get; set; }
        //        public WorkingScheduleDto WorkingScheduleComplete
        //        {
        //            get
        //            {
        //                WorkingScheduleDto result = new WorkingScheduleDto (WorkingSchedule, true);
        //                for (DayOfWeek i = DayOfWeek.Sunday; i <= DayOfWeek.Saturday; i++)
        //                    if(result.GetScheduleForDay(i) == null)
        //                        result.Items.Add(new WorkingScheduleItemDto()
        //                                             {
        //                                                 WeekDay = Enum.GetName(typeof(DayOfWeek), i),
        //                                                 StartTime = StartWorkingTime.GetDateTime(DateTime.Now),
        //                                                 EndTime = FinishWorkingTime.GetDateTime(DateTime.Now),
        //                                             });
        //                return result;
        //            }
        //        }

        #endregion

        #region Nested Classes 

        public struct DayTime
        {
            private int _hours;
            private int _minutes;


            public DayTime(int hours, int minutes)
            {
                _hours = hours;
                _minutes = minutes;
            }

            public int Hours
            {
                get { return _hours; }
                set { _hours = value; }
            }

            public int Minutes
            {
                get { return _minutes; }
                set { _minutes = value; }
            }

            public override string ToString()
            {
                return string.Format("{0}:{1}", _hours, _minutes);
            }

            public DateTime GetDateTime(DateTime date)
            {
                return new DateTime(date.Year, date.Month, date.Day, _hours, _minutes, 0);
            }

            public static DayTime operator -(DayTime lt, DayTime rt)
            {
                DayTime tmp = lt;

                tmp._hours = lt._hours - rt._hours;
                tmp._minutes = lt._minutes - rt._minutes;

                while (tmp._minutes < 0)
                {
                    tmp._hours -= 1;
                    tmp._minutes += 60;
                }

                while (tmp._hours < 0) tmp._hours += 24;

                return tmp;
            }

            public static DayTime operator +(DayTime lt, DayTime rt)
            {
                DayTime tmp = lt;

                tmp._hours = lt._hours + rt._hours;
                tmp._minutes = lt._minutes + rt._minutes;

                while (tmp._minutes >= 60)
                {
                    tmp._hours += 1;
                    tmp._minutes -= 60;
                }

                while (tmp._hours >= 24) tmp._hours -= 24;

                return tmp;
            }

        }

        #endregion

        #region Construction

        public AccountDto()
        {
            this.ColorsConfiguration = new Dictionary<long, string>();
            this.AllProfiles = new List<UserProfileDto>();
            //this.Types = new List<AppointmentResourceTypeDto>();
            //            this.WorkingDays = new List<DayOfWeek>();

            //this.ResourceTypes = new List<AppointmentResourceTypeDto>();
            //this.ModalityTypes = new List<ModalityTypeDto>();
            //this.TechCompleteSuggestionList = new List<TechCompleteSuggestionListDto>();
            //this.ResouceAreas = new List<ResourceAreaDto>();
            //this.ResourceLocations = new List<ResourceLocationDto>();
            //this.AppointmentStatuses = new List<AppointmentStatusDto>();
            //this.PhysicianTypes = new List<PhysicianTypeDto>();

            //this.PreCachedResourses = new List<AppointmentResourceDto>();
            //this.UnlinkedResourses = new List<AppointmentResourceDto>();

            //this.CommentTypes = new List<CommentTypeDto>();
            this.AvailablePayers = new Dictionary<string, string>();
            this.PayerStates = new Dictionary<string, string>();
            this.UsaStates = new Dictionary<string, string>();
            this.AvailableProviders = new Dictionary<string, string>();
            //this.ReferralSpecialities = new PhysicianSpecialitiesDto();
            //this.VolumeUnits = new VolumeUnitsDto();
            this.VisitCategories = new AccountEnumsDto();
            this.AvailableInsRelationships = new Dictionary<string, string>();
            this.AvailableLanguages = new Dictionary<String, String>();
            this.WorkTypes = new Dictionary<string, Dictionary<string, string>>();
            //this.CodeCategories = new List<CodeCategoryDto>();
            //this.OrderCreationParameters = new List<OrderCreationParameterDto>();

            this.PrintPatientDetailsPage = "prPatientDetails.aspx";
            this.PrintRoomSchedulePage = "prRoomSchedule.aspx";
            this.PrintPrepNotePage = "prPrepNote.aspx";
            this.PrintCommentsPage = "prComments.aspx";
            this.PrintPaymentPage = "prPayment.aspx";
            this.PrintInvoicePage = "prInvoice.aspx";
            this.OrderReportPage = "ReportViewer.aspx";
            //this.GenerateIDconfigs = new List<AccountGenerateIDconfigDto>();
            //this.AutorizationAlerts = new List<AuthorizationAlertDto>();
            this.HCPCSCodes = new AccountEnumsDto();
            this.EnumsHeardOfUs = new AccountEnumsDto();
            this.EnumsScheduledBy = new AccountEnumsDto();
            this.GuarantorRelationShip = new AccountEnumsDto();
            this.EnumsMaritalStatus = new AccountEnumsDto();
            this.EnumPriority = new AccountEnumsDto();
            this.EnumPendingReason = new AccountEnumsDto();
            //this.WorkingSchedule = new WorkingScheduleDto();
            this.EnumAuthorizationUserStatuses = new AccountEnumsDto();
            this.EnumMultipleIdentifierSource = new AccountEnumsDto();
            this.EnumPatientCommentTransferredTo = new AccountEnumsDto();
            this.AccountSettings = new AccountSettingCollectionDto();
            this.EnumsPatientAilment = new AccountEnumsDto();
            this.EnumContactRelation = new AccountEnumsDto();
            this.EnumFiltersConfiguration = new AccountEnumsDto();
            this.EnumEmploymentStatus = new AccountEnumsDto();
            this.EnumDiagnosisFlags = new AccountEnumsDto();
            this.EnumReferralSpecialities = new AccountEnumsDto();
            this.EnumReferralGroups = new AccountEnumsDto();
            this.EnumPaymentStatuses = new AccountEnumsDto();
            this.EnumCreditCardTypes = new AccountEnumsDto();
            this.EnumMammoLaterality = new AccountEnumsDto();
            this.EnumMammoMammogramType = new AccountEnumsDto();
            this.EnumMammoMammogramSubType = new AccountEnumsDto();
            this.EnumMammoNodalStatus = new AccountEnumsDto();
            this.EnumMammoTumorSize = new AccountEnumsDto();
            this.EnumMammoBiopsyType = new AccountEnumsDto();
            this.EnumMammoBirads = new AccountEnumsDto();
            this.EnumMammoBreastDensity = new AccountEnumsDto();
            this.EnumTestResultStatus = new AccountEnumsDto();
            this.EnumContactType = new AccountEnumsDto();

            OrderCreationTrigger = new List<int>();
            //AllMarketingReps = new List<MarketingRepDto>();
            VisitCreationTrigger = new List<int>();
            AllGenders = new AccountEnumsDto();
            AllRaces = new RacesDto();
            AllEthnicity = new EthnicitiesDto();
            AllSmoking = new AccountEnumsDto();
            AllPatientStatuses = new AccountEnumsDto();
            AllSpecialNeeds = new AccountEnumsDto();
            AllRelationships = new AccountEnumsDto();
            AllEhrSystems = new AccountEnumsDto();

            //this.CurrentAcl = new AccessControlListDto();
            this.AvailableRoles = new List<UserRoleDto>();
            //this.AvailableAccessControlEntities = new AccessControlListDto();
            //this.NotificationSlots = new List<NotificationSlotDto>();
            //this.AppointmentCheckListItems = new List<AppointmentCheckListItemDto>();
        }

        #endregion

        public override string ToString()
        {
            return this.AccountName;
        }

        public void InitColor(long statusId, string color)
        {
            if (ColorsConfiguration.ContainsKey(statusId))
                ColorsConfiguration[statusId] = color;
            else
                ColorsConfiguration.Add(statusId, color);
        }

        public bool ApplyConfigChanges(AccountDto acc)
        {
            bool requireUpdate = false;
            //            this.WorkingDays.Count != acc.WorkingDays.Count;
            //
            //            foreach (DayOfWeek d in acc.WorkingDays)
            //            {
            //                if (!this.WorkingDays.Contains(d))
            //                    requireUpdate = true;
            //            }

            //if (WorkingSchedule.DiffersFrom(acc.WorkingSchedule))
            //{
            //    WorkingSchedule.Items.Clear();
            //    WorkingSchedule.Items.AddRange(acc.WorkingSchedule.Items);
            //    WorkingSchedule.Holidays.Clear();
            //    WorkingSchedule.Holidays.AddRange(acc.WorkingSchedule.Holidays);
            //    requireUpdate = true;
            //}

            //if (this.OrderCreationMode != acc.OrderCreationMode)
            //{
            //    this.OrderCreationMode = acc.OrderCreationMode;
            //    requireUpdate = true;
            //}

            if (this.MRNReadOnly != acc.MRNReadOnly)
            {
                this.MRNReadOnly = acc.MRNReadOnly;
                requireUpdate = true;
            }

            if (this.IsMammographyActive != acc.IsMammographyActive)
            {
                this.IsMammographyActive = acc.IsMammographyActive;
                requireUpdate = true;
            }

            if (this.IsScheduleAppointmentByEstimationSlots != acc.IsScheduleAppointmentByEstimationSlots)
            {
                this.IsScheduleAppointmentByEstimationSlots = acc.IsScheduleAppointmentByEstimationSlots;
                requireUpdate = true;
            }
            if (this.IsStateOfServiceEnabled != acc.IsStateOfServiceEnabled)
            {
                this.IsStateOfServiceEnabled = acc.IsStateOfServiceEnabled;
                requireUpdate = true;
            }

            if (this.IsProcedureRequired != acc.IsProcedureRequired)
            {
                this.IsProcedureRequired = acc.IsProcedureRequired;
                requireUpdate = true;
            }

            if (this.IsPaymentsEnabled != acc.IsPaymentsEnabled)
            {
                this.IsPaymentsEnabled = acc.IsPaymentsEnabled;
                requireUpdate = true;
            }
            if (this.IsProcessPaymentsEnabled != acc.IsProcessPaymentsEnabled)
            {
                this.IsProcessPaymentsEnabled = acc.IsProcessPaymentsEnabled;
                requireUpdate = true;
            }

            if (this.SendEmailFromAddress != acc.SendEmailFromAddress)
            {
                this.SendEmailFromAddress = acc.SendEmailFromAddress;
                requireUpdate = true;
            }

            //if (this.ProcedureExpansionMode != acc.ProcedureExpansionMode)
            //{
            //    this.ProcedureExpansionMode = acc.ProcedureExpansionMode;
            //    requireUpdate = true;
            //}


            //if (this.PayersSearchMode != acc.PayersSearchMode)
            //{
            //    this.PayersSearchMode = acc.PayersSearchMode;
            //    requireUpdate = true;
            //}


            if (this.PreselectProcedureTypes != acc.PreselectProcedureTypes)
            {
                this.PreselectProcedureTypes = acc.PreselectProcedureTypes;
                requireUpdate = true;
            }

            if (this.IsBillingNoteRequired != acc.IsBillingNoteRequired)
            {
                this.IsBillingNoteRequired = acc.IsBillingNoteRequired;
                requireUpdate = true;
            }

            if (this.IsCreateOrderRequired != acc.IsCreateOrderRequired)
            {
                this.IsCreateOrderRequired = acc.IsCreateOrderRequired;
                requireUpdate = true;
            }

            if (this.IsPendingEnabled != acc.IsPendingEnabled)
            {
                this.IsPendingEnabled = acc.IsPendingEnabled;
                requireUpdate = true;
            }


            if (this.PatientCategoryRequired != acc.PatientCategoryRequired)
            {
                this.PatientCategoryRequired = acc.PatientCategoryRequired;
                requireUpdate = true;
            }

            if (this.IsVisitReasonRequired != acc.IsVisitReasonRequired)
            {
                this.IsVisitReasonRequired = acc.IsVisitReasonRequired;
                requireUpdate = true;
            }

            //if (this.IsReferralRequired != acc.IsReferralRequired)
            //{
            //    this.IsReferralRequired = acc.IsReferralRequired;
            //    requireUpdate = true;
            //}

            if (this.IsProcedureGlobalSearchEnabled != acc.IsProcedureGlobalSearchEnabled)
            {
                this.IsProcedureGlobalSearchEnabled = acc.IsProcedureGlobalSearchEnabled;
                requireUpdate = true;
            }

            if (this.ViewPatientsInValidLocationsOnly != acc.ViewPatientsInValidLocationsOnly)
            {
                this.ViewPatientsInValidLocationsOnly = acc.ViewPatientsInValidLocationsOnly;
                requireUpdate = true;
            }

            if (this.OrderCreationTrigger != acc.OrderCreationTrigger)
            {
                this.OrderCreationTrigger = acc.OrderCreationTrigger;
                requireUpdate = true;
            }

            if (this.VisitCreationTrigger != acc.VisitCreationTrigger)
            {
                this.VisitCreationTrigger = acc.VisitCreationTrigger;
                requireUpdate = true;
            }

            if (this.StartWeekOn != acc.StartWeekOn)
            {
                this.StartWeekOn = acc.StartWeekOn;
                requireUpdate = true;
            }

            //                if (!this.StartWorkingTime.Equals(acc.StartWorkingTime))
            //                {
            //                    this.StartWorkingTime = acc.StartWorkingTime;
            //                    requireUpdate = true;
            //                }
            //                if (!this.FinishWorkingTime.Equals(acc.FinishWorkingTime))
            //                {
            //                    this.FinishWorkingTime = acc.FinishWorkingTime;
            //                    requireUpdate = true;
            //                }
            //
            if (this.NumberOfVisibleHours != acc.NumberOfVisibleHours)
            {
                this.NumberOfVisibleHours = acc.NumberOfVisibleHours;
                requireUpdate = true;
            }

            if (this.DefaultViewMode != acc.DefaultViewMode)
            {
                this.DefaultViewMode = acc.DefaultViewMode;
                requireUpdate = true;
            }

            if (this.ScheduleMode != acc.ScheduleMode)
            {
                this.ScheduleMode = acc.ScheduleMode;
                requireUpdate = true;
            }

            if (this.IsWarningMessagesEnabled != acc.IsWarningMessagesEnabled)
            {
                this.IsWarningMessagesEnabled = acc.IsWarningMessagesEnabled;
                requireUpdate = true;
            }

            if (this.IsCommentForBlockingRequired != acc.IsCommentForBlockingRequired)
            {
                this.IsCommentForBlockingRequired = acc.IsCommentForBlockingRequired;
                requireUpdate = true;
            }

            if (this.IsPatientDOBMandatory != acc.IsPatientDOBMandatory)
            {
                this.IsPatientDOBMandatory = acc.IsPatientDOBMandatory;
                requireUpdate = true;
            }

            return requireUpdate;
        }
    }

    public class AccountsDto //: DtoBase
    {
        public AccountsDto()
        {
            this.Accounts = new List<AccountDto>();
        }

        public IList<AccountDto> Accounts { get; set; }
    }
}
