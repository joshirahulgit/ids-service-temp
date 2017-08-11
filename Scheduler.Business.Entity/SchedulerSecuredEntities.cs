using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Business.Entity
{
    public enum SchedulerSecuredEntities
    {
        // ReSharper disable InconsistentNaming
        
        Admin = 1,
        
        AdminAccount = 2,
        
        AdminWorkCalendar = 3,
        
        AdminUserManagement = 4,
        
        AdminAccountGeneral = 5,
        
        AdminAccountProviderRoles = 6,
        
        AdminAccountProviders = 7,
        
        AdminAccountModalities = 8,
        
        AdminAccountAppointmentStatuses = 9,
        
        AdminAccountCommentTypes = 10,
        
        AdminAccountVolumeUnits = 11,
        
        AdminAccountReferralSpecialities = 12,
        
        AdminAccountIdsConfiguration = 13,
        
        AdminAccountAudit = 14,
        
        AdminAccountAuthorization = 15,
        
        AdminAccountTechCompleteSuggestionList = 16,
        
        AdminAccountHCPCSList = 17,
        
        AdminAccountHeardOfUsList = 18,
        
        AdminAccountPriorityList = 19,
        
        AdminAccountScheduledByList = 20,
        
        AdminAccountAuthorizationUserStatus = 21,
        
        AdminAccountProcedures = 22,
        
        UserManagementSecureACLs = 23,
        
        ProcedureAddToFavorites = 24,
        
        AdminAccountDiagnoses = 25,
        
        AdminAilmentList = 26,
        
        CompletedAppointments = 27,
        
        TestData = 28,
        
        SchedulerAdminAccountSettings = 29,
        
        SchedulerAdminAccountReferralGroups = 30,
        
        SchedulerAdminAccountDiagnosisFlags = 31,
        
        SchedulerAdminAccountCreditCardTypes = 32,
        
        SchedulerAdminAccountPaymentStatuses = 33,
        
        SchedulerAdminAccountPayerCrosswalk = 34,
        
        SchedulerAdminAccountFilters = 35,
        
        SchedulerAdminAccountMammoBirads = 36,
        
        SchedulerAdminAccountMammoBreastDensity = 37,
        
        SchedulerAdminAccountMammoLaterality = 38,
        
        SchedulerAdminAccountMammoNodalStatuses = 39,
        
        SchedulerAdminAccountMammoTumorSizes = 40,
        
        SchedulerAdminAccountMammoBiopsyTypes = 41,
        
        SchedulerPatientComments = 42,
        
        SchedulerReferringNote = 43,
        
        SchedulerExamDuration = 44,
        
        SchedulerReferring = 45,
        
        SchedulerDoubleBooking = 46,
        
        SchedulerAllowUserToChangeModalityType = 47,
        
        SchedulerDemographicsTab = 48,
        
        SchedulerMammographyTab = 49,
        
        SchedulerPayerInformationTab = 50,
        
        SchedulerAuthorizationTab = 51,
        
        SchedulerVisitHistoryTab = 52,
        
        SchedulerGuarantorTab = 53,
        
        SchedulerPaymentsTab = 54,
        
        SchedulerContactsTab = 55,
        
        SchedulerAppointmentChecklistTab = 56,
        
        SchedulerReferralTab = 57,
        
        SchedulerCptTab = 58,
        
        SchedulerVisitInformationTab = 59,
        
        PatientCommentsTab = 60,
        
        SchedulerOverrideNotificationSlots = 61,
        
        SchedulerAdminNotificationSlots = 62
    }
}
