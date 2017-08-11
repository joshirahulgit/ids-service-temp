using Scheduler.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scheduler.DBEntity
{
    public class Account : EntityBase
    {
        public AccessControlList AvailableAccessControlEntities { get; set; }
        public List<UserRole> AvailableRoles { get; set; }
        public String Name { get; set; }
        public List<DayOfWeek> WorkingDays { get; set; }
        public int StartWorkingHour { get; set; }
        public int StartWorkingMinute { get; set; }
        public int EndWorkingHour { get; set; }
        public int EndWorkingMinute { get; set; }
        public int HourDivisionSegment { get; set; }
        public int DefaultViewMode { get; set; }
        public int ScheduleMode { get; set; }
        public bool? ViewPatientsInValidLocationsOnly { get; set; }
        public bool AccessValidLocationsOnly { get; set; }

        public int StartWeekOn { get; set; }
        public bool MRNReadOnly { get; set; }
        public bool IsMammographyActive { get; set; }
        public int NumberOfVisibleHours { get; set; }

        public List<AppointmentResourceType> ResourceTypes { get; set; }
        public List<ModalityType> ModalityTypes { get; set; }
        public List<Area> ResouceAreas { get; set; }
        public List<ResourceLocation> ResourceLocations { get; set; }
        public List<AppointmentStatus> AppointmentStatuses { get; set; }
        public List<CommentType> CommentTypes { get; set; }
        public List<TechCompleteSuggestionList> TechCompleteSuggestionList { get; set; }
        public List<PhysicianType> PhysicianTypes { get; set; }
        public List<AuthorizationAlert> AutorizationAlerts { get; set; }


        public Dictionary<String, String> AvailablePayers { get; set; }
        public Dictionary<String, String> AvailableProviders { get; set; }
        public Dictionary<String, String> AvailableInsRelationships { get; set; }
        public Dictionary<String, String> AvailableLanguages { get; set; }

        public List<CodeCategory> CodeCategories { get; set; }

        public List<ProcedureType> AvailableProcedures { get; set; }
        public List<Diagnosis> AvailableDiangnosises { get; set; }
        public Dictionary<long, string> ColorsConfiguration { get; set; }

        public Dictionary<String, Dictionary<String, String>> WorkTypes { get; set; }

        public bool AccountProceduresAreAvailable { get; set; }
        public bool AccountDiagnosesAreAvailable { get; set; }

        public String State { get; set; }
        public String LogoUrl { get; set; }
        public String Address { get; set; }
        public String Address2 { get; set; }
        public String City { get; set; }
        public String ZipCode { get; set; }
        public String Phone { get; set; }

        //Automatic order creation support 
        public OrderCreationMode OrderCreationMode { get; set; }
        public ProcedureExpansionMode ProcedureExpansionMode { get; set; }
        public PayersSearchMode PayersSearchMode { get; set; }
        public List<int> OrderCreationTrigger { get; set; }
        public List<int> VisitCreationTrigger { get; set; }
        public String WorkTypeSourceTable { get; set; }
        public String WorkTypeSourceColumn { get; set; }
        public String MapVisitTypeFrom { get; set; }
        public String SendEmailFromAddress { get; set; }
        public List<OrderCreationParameter> OrderCreationParameters { get; set; }

        public bool IsSchedulerAdmin { get; set; }
        public bool IsWorkWithPatientVisitAllowed { get; set; }
        public bool IsDictator { get; set; }
        public bool IsAdmin { get; set; }
        public bool HasAccessToScheduler { get; set; }
        public bool IsCrmEnabled { get; set; }

        public bool IsLocationFilterVis { get; set; }
        public bool IsModalityFilterVis { get; set; }
        public bool IsRoomFilterVis { get; set; }
        public bool IsRoleFilterVis { get; set; }
        public bool IsProviderFilterVis { get; set; }
        public bool IsApptStatusFilterVis { get; set; }
        public bool IsDaysFilterVis { get; set; }
        public bool IsPhyGroupVis { get; set; }
        public bool IsWtGroupVis { get; set; }
        public bool IsReferralRequired { get; set; }
        public bool IsPaymentsEnabled { get; set; }
        public bool IsProcessPaymentsEnabled { get; set; }
        public bool IsBillingNoteRequired { get; set; }
        public bool IsCreateOrderRequired { get; set; }
        public bool IsVisitReasonRequired { get; set; }
        public bool IsPendingEnabled { get; set; }
        public bool PreselectProcedureTypes { get; set; }
        public bool PatientCategoryRequired { get; set; }
        public bool IsProcedureGlobalSearchEnabled { get; set; }
        public bool IsWarningMessagesEnabled { get; set; }
        public bool IsCommentForBlockingRequired { get; set; }
        public bool IsPatientDOBMandatory { get; set; }
        //        public List<PhysicianSpeciality> ReferralSpecialities { get; set; }
        public List<VolumeUnit> VolumeUnits { get; private set; }
        public List<AccountEnum> VisitCategories { get; set; }
        public Dictionary<string, string> UsaStates { get; set; }
        public List<AccountGenerateIDconfig> GenerateIDconfigs { get; set; }
        public List<AccountEnum> HCPCScodes { get; set; }
        public List<AccountEnum> EnumsScheduledBy { get; set; }
        public List<AccountEnum> EnumsHeardOfUs { get; set; }
        public List<AccountEnum> EnumPriority { get; set; }
        public List<AccountEnum> EnumPatientAilment { get; set; }
        public List<AccountEnum> GuarantorRelationShip { get; set; }
        public List<AccountEnum> EnumPendingReason { get; set; }
        public List<AccountEnum> EnumEmploymentStatus { get; set; }
        public List<AccountEnum> EnumDiagnosisFlags { get; set; }
        public List<AccountEnum> EnumReferralSpecialities { get; set; }
        public List<AccountEnum> EnumReferralGroups { get; set; }
        public List<AccountEnum> EnumCreditCardTypes { get; set; }
        public List<AccountEnum> EnumPaymentStatuses { get; set; }
        public List<AccountEnum> EnumMaritalStatus { get; set; }
        public List<AccountEnum> EnumContactType { get; set; }
        public List<AccountEnum> EnumContactRelation { get; set; }
        public List<AccountEnum> EnumFiltersConfiguration { get; set; }
        public List<AccountEnum> EnumMammoLaterality { get; set; }
        public List<AccountEnum> EnumMammoMammogramType { get; set; }
        public List<AccountEnum> EnumMammoMammogramSubType { get; set; }
        public List<AccountEnum> EnumMammoNodalStatus { get; set; }
        public List<AccountEnum> EnumMammoTumorSize { get; set; }
        public List<AccountEnum> EnumMammoBiopsyType { get; set; }
        public List<AccountEnum> EnumMammoBirads { get; set; }
        public List<AccountEnum> EnumMammoBreastDensity { get; set; }
        public List<AccountEnum> EnumTestResultStatus { get; set; }
        public WorkingSchedule WorkingSchedule { get; set; }
        public List<AccountEnum> EnumAuthorizationUserStatuses { get; set; }
        public List<AccountEnum> EnumMultipleIdentifierSource { get; set; }
        public List<AccountEnum> EnumPatientCommentTransferredTo { get; set; }

        public List<AccountEnum> AllGenders { get; set; }
        public List<Race> AllRaces { get; set; }
        public List<Ethnicity> AllEthnicity { get; set; }
        public List<AccountEnum> AllSmoking { get; set; }
        public List<AccountEnum> AllPatientStatuses { get; set; }
        public List<AccountEnum> AllSpecialNeeds { get; set; }
        public List<AccountEnum> AllRelationships { get; set; }
        public List<AccountEnum> AllEhrSystems { get; set; }
        public List<MarketingRep> AllMarketingReps { get; set; }

        public List<NotificationSlot> NotificationSlot { get; set; }


        public AccountSettingCollection AccountSettings { get; set; }
        public bool IsScheduleAppointmentByEstimationSlots { get; set; }
        public bool IsStateOfServiceEnabled { get; set; }
        public bool IsProcedureRequired { get; set; }

        public List<AppointmentCheckListItem> AppointmentCheckListItems { get; set; }

        public Account()
        {
            ResourceTypes = new List<AppointmentResourceType>();
            ModalityTypes = new List<ModalityType>();
            ResouceAreas = new List<Area>();
            ResourceLocations = new List<ResourceLocation>();
            AppointmentStatuses = new List<AppointmentStatus>();
            AvailableProcedures = new List<ProcedureType>();
            AvailableDiangnosises = new List<Diagnosis>();
            CommentTypes = new List<CommentType>();
            ColorsConfiguration = new Dictionary<long, string>();
            AvailablePayers = new Dictionary<String, String>();
            PayerStates = new Dictionary<String, String>();
            AvailableProviders = new Dictionary<string, string>();
            AvailableInsRelationships = new Dictionary<string, string>();
            AvailableLanguages = new Dictionary<String, String>();
            UsaStates = new Dictionary<String, String>();
            WorkTypes = new Dictionary<string, Dictionary<string, string>>();
            CodeCategories = new List<CodeCategory>();
            PhysicianTypes = new List<PhysicianType>();
            OrderCreationParameters = new List<OrderCreationParameter>();
            //            ReferralSpecialities       = new List<PhysicianSpeciality>();
            VolumeUnits = new List<VolumeUnit>();
            GenerateIDconfigs = new List<AccountGenerateIDconfig>();
            VisitCategories = new List<AccountEnum>();
            AutorizationAlerts = new List<AuthorizationAlert>();
            TechCompleteSuggestionList = new List<TechCompleteSuggestionList>();
            HCPCScodes = new List<AccountEnum>();
            EnumsHeardOfUs = new List<AccountEnum>();
            EnumsScheduledBy = new List<AccountEnum>();
            EnumMaritalStatus = new List<AccountEnum>();
            EnumContactRelation = new List<AccountEnum>();
            EnumContactType = new List<AccountEnum>();
            GuarantorRelationShip = new List<AccountEnum>();
            EnumPriority = new List<AccountEnum>();
            EnumPendingReason = new List<AccountEnum>();
            EnumEmploymentStatus = new List<AccountEnum>();
            EnumDiagnosisFlags = new List<AccountEnum>();
            EnumReferralGroups = new List<AccountEnum>();
            EnumPaymentStatuses = new List<AccountEnum>();
            EnumCreditCardTypes = new List<AccountEnum>();
            EnumReferralSpecialities = new List<AccountEnum>();
            EnumFiltersConfiguration = new List<AccountEnum>();
            EnumMammoLaterality = new List<AccountEnum>();
            EnumMammoMammogramType = new List<AccountEnum>();
            EnumMammoMammogramSubType = new List<AccountEnum>();
            EnumMammoNodalStatus = new List<AccountEnum>();
            EnumMammoTumorSize = new List<AccountEnum>();
            EnumMammoBiopsyType = new List<AccountEnum>();
            EnumMammoBirads = new List<AccountEnum>();
            EnumMammoBreastDensity = new List<AccountEnum>();
            EnumTestResultStatus = new List<AccountEnum>();

            OrderCreationTrigger = new List<int>();
            VisitCreationTrigger = new List<int>();
            AllGenders = new List<AccountEnum>();
            AllRaces = new List<Race>();
            AllEthnicity = new List<Ethnicity>();
            AllSmoking = new List<AccountEnum>();
            AllPatientStatuses = new List<AccountEnum>();
            AllSpecialNeeds = new List<AccountEnum>();
            AllRelationships = new List<AccountEnum>();
            AllEhrSystems = new List<AccountEnum>();
            AllMarketingReps = new List<MarketingRep>();

            WorkingSchedule = new WorkingSchedule();
            EnumAuthorizationUserStatuses = new List<AccountEnum>();
            EnumMultipleIdentifierSource = new List<AccountEnum>();
            EnumPatientCommentTransferredTo = new List<AccountEnum>();
            AccountSettings = new AccountSettingCollection();
            EnumPatientAilment = new List<AccountEnum>();
            AvailableRoles = new List<UserRole>();
            AvailableAccessControlEntities = new AccessControlList();
            NotificationSlot = new List<NotificationSlot>();
            AppointmentCheckListItems = new List<AppointmentCheckListItem>();
        }

        public Account(long id, String accountName)
            : this()
        {
            this.Id = id;
            this.Name = accountName;
        }

        public Account(long id)
            : this(id, String.Empty)
        {

        }

        public Account(string accountName)
            : this(-1, accountName)
        {

        }

        public void SetAccountSourcesAvailability(object proceduresExist, object diagnosesExist)
        {
            int proceduresCount = Convert.ToInt32(proceduresExist);
            int diagnosisCount = Convert.ToInt32(diagnosesExist);

            this.AccountDiagnosesAreAvailable = diagnosisCount > 0;
            this.AccountProceduresAreAvailable = proceduresCount > 0;
        }

        public Dictionary<string, string> PayerStates { get; private set; }


        //internal void Create(Repository.RepositoryLocator locator)
        //{
        //    //Here we perform before create actions
        //    locator.AccountRepository.Create(this);
        //}

        //internal void Delete(Repository.RepositoryLocator locator)
        //{
        //    //Here we perform before delete actions
        //    locator.AccountRepository.Remove(this);
        //}

        //internal void Update(Repository.RepositoryLocator locator, Account account)
        //{
        //    this.ViewPatientsInValidLocationsOnly = account.ViewPatientsInValidLocationsOnly;
        //    this.Name = account.Name;
        //    this.WorkingDays = account.WorkingDays;
        //    this.AvailableRoles = account.AvailableRoles;
        //    this.AvailableAccessControlEntities = account.AvailableAccessControlEntities;
        //    this.StartWorkingHour = account.StartWorkingHour;
        //    this.StartWorkingMinute = account.StartWorkingMinute;
        //    this.EndWorkingHour = account.EndWorkingHour;
        //    this.EndWorkingMinute = account.EndWorkingMinute;
        //    this.HourDivisionSegment = account.HourDivisionSegment;
        //    this.DefaultViewMode = account.DefaultViewMode;
        //    this.ProcedureExpansionMode = account.ProcedureExpansionMode;
        //    this.PayersSearchMode = account.PayersSearchMode;
        //    this.ScheduleMode = account.ScheduleMode;
        //    this.OrderCreationMode = account.OrderCreationMode;
        //    this.OrderCreationTrigger = account.OrderCreationTrigger;
        //    this.VisitCreationTrigger = account.VisitCreationTrigger;
        //    this.SendEmailFromAddress = account.SendEmailFromAddress;

        //    this.ResourceTypes = account.ResourceTypes;
        //    this.ModalityTypes = account.ModalityTypes;
        //    this.ResouceAreas = account.ResouceAreas;
        //    this.ResourceLocations = account.ResourceLocations;
        //    this.AppointmentStatuses = account.AppointmentStatuses;
        //    this.CommentTypes = account.CommentTypes;
        //    this.AvailableProcedures = account.AvailableProcedures;
        //    this.AvailableDiangnosises = account.AvailableDiangnosises;
        //    this.ColorsConfiguration = account.ColorsConfiguration;
        //    this.StartWeekOn = account.StartWeekOn;
        //    this.NumberOfVisibleHours = account.NumberOfVisibleHours;
        //    this.WorkTypes = account.WorkTypes;

        //    this.IsScheduleAppointmentByEstimationSlots = account.IsScheduleAppointmentByEstimationSlots;
        //    this.IsStateOfServiceEnabled = account.IsStateOfServiceEnabled;
        //    this.IsProcedureRequired = account.IsProcedureRequired;
        //    this.IsLocationFilterVis = account.IsLocationFilterVis;
        //    this.IsModalityFilterVis = account.IsModalityFilterVis;
        //    this.IsRoomFilterVis = account.IsRoomFilterVis;
        //    this.IsRoleFilterVis = account.IsRoleFilterVis;
        //    this.IsProviderFilterVis = account.IsProviderFilterVis;
        //    this.IsApptStatusFilterVis = account.IsApptStatusFilterVis;
        //    this.IsDaysFilterVis = account.IsDaysFilterVis;
        //    this.IsPhyGroupVis = account.IsPhyGroupVis;
        //    this.IsPendingEnabled = account.IsPendingEnabled;
        //    this.IsWtGroupVis = account.IsWtGroupVis;
        //    this.IsReferralRequired = account.IsReferralRequired;
        //    this.IsPaymentsEnabled = account.IsPaymentsEnabled;
        //    this.IsProcessPaymentsEnabled = account.IsProcessPaymentsEnabled;
        //    this.IsBillingNoteRequired = account.IsBillingNoteRequired;
        //    this.IsCreateOrderRequired = account.IsCreateOrderRequired;
        //    this.IsVisitReasonRequired = account.IsVisitReasonRequired;
        //    this.PreselectProcedureTypes = account.PreselectProcedureTypes;
        //    this.PatientCategoryRequired = account.PatientCategoryRequired;
        //    this.IsProcedureGlobalSearchEnabled = account.IsProcedureGlobalSearchEnabled;
        //    this.IsCommentForBlockingRequired = account.IsCommentForBlockingRequired;
        //    this.IsPatientDOBMandatory = account.IsPatientDOBMandatory;
        //    this.IsWarningMessagesEnabled = account.IsWarningMessagesEnabled;
        //    this.MRNReadOnly = account.MRNReadOnly;
        //    this.IsMammographyActive = account.IsMammographyActive;

        //    this.WorkingSchedule.Items.Clear();
        //    this.WorkingSchedule.Items.AddRange(account.WorkingSchedule.Items);
        //    this.WorkingSchedule.Holidays.Clear();
        //    this.WorkingSchedule.Holidays.AddRange(account.WorkingSchedule.Holidays);

        //    this.OrderCreationParameters.Clear();
        //    this.OrderCreationParameters.AddRange(account.OrderCreationParameters);

        //    locator.AccountRepository.Update(this);
        //}

        public void InitDefaultData()
        {
            ResourceTypes.Clear();
            ResourceTypes.AddRange(AppointmentResourceType.GetList());

            // AppointmentStatuses.Clear();
            //AppointmentStatuses.AddRange(AppointmentStatus.GetList());

            //     CommentTypes.Clear();
            //  CommentTypes.AddRange(CommentType.GetList());
        }

        public void AllowFullAccess()
        {
            IsSchedulerAdmin = HasAccessToScheduler = true;
        }

        //CP: Fix
        //public void LoadCommentTypes(IDataReader reader)
        //{
        //    CommentTypes.Clear();
        //    using (SafeDataReader sr = new SafeDataReader(reader))
        //    {
        //        while (sr.Read())
        //        {
        //            //CommentTypes.Add(new CommentType(sr.GetInt32("Id"), sr.GetString("DisplayName"),
        //            //                                 sr.GetBoolean("IsVisible"), sr.GetBoolean("IsSystem")));
        //            CommentTypes.Add(CommentType.ExtractFromReader(sr));
        //        }
        //    }
        //}

        //CP: Fix
        //public void LoadTechCompleteSuggestionList(IDataReader reader)
        //{
        //    TechCompleteSuggestionList.Clear();
        //    using (SafeDataReader sr = new SafeDataReader(reader))
        //    {
        //        while (sr.Read())
        //            TechCompleteSuggestionList.Add(new TechCompleteSuggestionList(sr.GetInt64("Id"),
        //                                                                          sr.GetString("DisplayName"),
        //                                                                          sr.GetBoolean("IsVisible")));
        //    }
        //}

        //CP: Fix
        //public void LoadAppointmentStatuses(IDataReader reader)
        //{
        //    AppointmentStatuses.Clear();
        //    int sortIndex = 0;
        //    using (SafeDataReader sr = new SafeDataReader(reader))
        //    {
        //        while (sr.Read())
        //        {
        //            //Grety mod. 2013-01-25. Prevent Pending status from being visible anywhere in the system.
        //            int id = sr.GetInt32("Id");
        //            bool isVisible = sr.GetBoolean("IsVisible");
        //            //                    if(id ==(long) Scheduler.Common.Enums.AppointmentStatuses.Pending)
        //            //                        isVisible = false;
        //            AppointmentStatuses.Add(new AppointmentStatus(id, sr.GetString("DisplayName"),
        //                                                          sr.GetString("AppliedDisplayName"),
        //                                                          isVisible, sortIndex++, sr.GetBoolean("IsSystemStatus"), sr.GetString("Color")));
        //        }
        //    }
        //}

        public void LoadVolumeUnits(List<VolumeUnit> volumeUnits)
        {
            VolumeUnits = volumeUnits;
        }

        public void UpdateName(string accountName)
        {
            Name = accountName;
        }

        public void LoadAuthorizationAlerts(List<AuthorizationAlert> authAlerts)
        {
            AutorizationAlerts = authAlerts;
        }
        //
        //        public void LoadReferralSpecialities(List<PhysicianSpeciality> rSpecialities)
        //        {
        //            ReferralSpecialities = rSpecialities;
        //        }


        public void LoadAvailableRoles(List<UserRole> userRoles)
        {
            AvailableRoles = userRoles;
        }

        public void LoadAvailableAccessControlEntities(AccessControlList entities)
        {
            AvailableAccessControlEntities = entities;
        }

        public void LoadAccountSettings(AccountSettingCollection accountSettings)
        {
            AccountSettings = accountSettings;
        }

        public void LoadNotificationSlots(List<NotificationSlot> slots)
        {
            NotificationSlot = slots;
        }

        public void LoadOrderCreationTriggers(List<int> getOrderCreationTriggers)
        {
            OrderCreationTrigger = getOrderCreationTriggers;
        }

        public void LoadVisitCreationTriggers(List<int> getVisitCreationTriggers)
        {
            VisitCreationTrigger = getVisitCreationTriggers;
        }

        public void LoadAllMarketingReps(List<MarketingRep> getAllMarketingReps)
        {
            this.AllMarketingReps = getAllMarketingReps;
        }
    }
}
