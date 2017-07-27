using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scheduler.DBEntity
{
    public class Account : EntityBase
    {

        public String Name { get; private set; }
        public List<DayOfWeek> WorkingDays { get; private set; }
        public int StartWorkingHour { get; private set; }
        public int StartWorkingMinute { get; private set; }
        public int EndWorkingHour { get; private set; }
        public int EndWorkingMinute { get; private set; }
        public int HourDivisionSegment { get; private set; }
        public int DefaultViewMode { get; private set; }
        public int ScheduleMode { get; private set; }
        public bool? ViewPatientsInValidLocationsOnly { get; private set; }
        public bool AccessValidLocationsOnly { get; private set; }

        public int StartWeekOn { get; private set; }
        public bool MRNReadOnly { get; private set; }
        public bool IsMammographyActive { get; private set; }
        public int NumberOfVisibleHours { get; private set; }



        public List<AccountEnum> VisitCategories { get; set; }
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
        public List<AccountEnum> EnumAuthorizationUserStatuses { get; set; }
        public List<AccountEnum> AllGenders { get; set; }
        public List<AccountEnum> AllEthnicity { get; set; }
        public List<AccountEnum> AllSmoking { get; set; }
        public List<AccountEnum> AllPatientStatuses { get; set; }
        public List<AccountEnum> AllSpecialNeeds { get; set; }
        public List<AccountEnum> AllRelationships { get; set; }

        public Account()
        {
            VisitCategories = new List<AccountEnum>();
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
            AllGenders = new List<AccountEnum>();
            AllEthnicity = new List<AccountEnum>();
            AllSmoking = new List<AccountEnum>();
            AllPatientStatuses = new List<AccountEnum>();
            AllSpecialNeeds = new List<AccountEnum>();
            AllRelationships = new List<AccountEnum>();
            EnumAuthorizationUserStatuses = new List<AccountEnum>();
            EnumPatientAilment = new List<AccountEnum>();
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

        //TODO: No DTO allowed in this project. Should be moved to other project. -By RJ
        //public static Account ExtractFromDto(AccountDto dto)
        //{
        //}

        //TODO: No DTO here. Should be moved to other project and taken care via association object. -By RJ
        //public static AccountDto Convert2Dto(Account account)
        //{
        //}
    }
}
