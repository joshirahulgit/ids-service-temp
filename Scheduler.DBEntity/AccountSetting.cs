using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.DBEntity
{
    public class AccountSetting : EntityBase
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public string Application { get; set; }
        public string CreateUser { get; set; }
        public string UpdateUser { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsActive { get; set; }

        public AccountSetting()
        {
        }

        public AccountSetting(AccountSetting accountSetting)
        {
            Name = accountSetting.Name;
            Value = accountSetting.Value;
            Application = accountSetting.Application;
            CreateDate = accountSetting.CreateDate;
            CreateUser = accountSetting.CreateUser;
            UpdateDate = accountSetting.UpdateDate;
            UpdateUser = accountSetting.UpdateUser;
            IsActive = accountSetting.IsActive;
        }
    }

    public class AccountSettingCollection : EntityBase
    {
        public AccountSettingCollection()
        {
            Items = new List<AccountSetting>();
        }

        public List<AccountSetting> Items { get; set; }

        public AccountSetting GetByName(string name)
        {
            return Items.FirstOrDefault(s => s.Name == name);
        }

        public int? GetInt32ByName(string name)
        {
            AccountSetting setting = Items.FirstOrDefault(s => s.Name == name && s.IsActive);
            if (setting != null)
                return Convert.ToInt32(setting.Value);
            return null;
        }

        public bool? GetBoolByName(string name)
        {
            AccountSetting setting = Items.FirstOrDefault(s => s.Name == name && s.IsActive);
            if (setting != null)
                return Convert.ToBoolean(setting.Value);
            return null;
        }

        public DateTime? GetDateTimeByName(string name)
        {
            AccountSetting setting = Items.FirstOrDefault(s => s.Name == name && s.IsActive);
            if (setting != null)
                return Convert.ToDateTime(setting.Value);
            return null;
        }

        public string GetStringByName(string name)
        {
            AccountSetting setting = Items.FirstOrDefault(s => s.Name == name && s.IsActive);
            if (setting != null)
                return setting.Value;
            return null;
        }

        //public bool DefaultPayerStateToPatientState { get { return GetBoolByName(AccountSettingsNames.DefaultPayerStateToPatientState) ?? true; } }
        //public bool PayerRelationshipMandatory { get { return GetBoolByName(AccountSettingsNames.PayerRelationshipMandatory) ?? false; } }
        //public bool DefaultConsentDateToAppointmentDate { get { return GetBoolByName(AccountSettingsNames.DefaultConsentDateToAppointmentDate) ?? false; } }
        //public bool ShowAppointmentPaymentDetails { get { return GetBoolByName(AccountSettingsNames.ShowAppointmentPaymentDetails) ?? false; } }
        //public bool Reports_PatientDetails_EHRSectionVisible { get { return GetBoolByName(AccountSettingsNames.Reports_PatientDetails_EHRSectionVisible) ?? true; } }
        //public bool Reports_PatientDetails_SSNFirstNumbersVisible { get { return GetBoolByName(AccountSettingsNames.Reports_PatientDetails_SSNFirstNumbersVisible) ?? false; } }
        //public bool Reports_PatientDetails_ExternalIdVisible { get { return GetBoolByName(AccountSettingsNames.Reports_PatientDetails_ExternalIdVisible) ?? true; } }
        //public int Reports_PatientDetails_FontSize { get { return GetInt32ByName(AccountSettingsNames.Reports_PatientDetails_FontSize) ?? 14; } }
        //public int Reports_Comments_FontSize { get { return GetInt32ByName(AccountSettingsNames.Reports_Comments_FontSize) ?? 14; } }
        //public int Reports_RoomSchedule_FontSize { get { return GetInt32ByName(AccountSettingsNames.Reports_RoomSchedule_FontSize) ?? 10; } }
        //public string Reports_PatientDetails_FontFamily { get { return GetStringByName(AccountSettingsNames.Reports_PatientDetails_FontFamily) ?? "Verdana"; } }
        //public string Reports_Comments_FontFamily { get { return GetStringByName(AccountSettingsNames.Reports_Comments_FontFamily) ?? "Verdana"; } }
        //public string Reports_Payments_FontFamily { get { return GetStringByName(AccountSettingsNames.Reports_Payments_FontFamily) ?? "Verdana"; } }
        //public string Reports_RoomSchedule_FontFamily { get { return GetStringByName(AccountSettingsNames.Reports_RoomSchedule_FontFamily) ?? "Verdana"; } }
        //public string Reports_RoomSchedule_HeaderFontSize { get { return GetStringByName(AccountSettingsNames.Reports_RoomSchedule_HeaderFontSize) ?? "12"; } }
        //public string Reports_Payments_HeaderFontSize { get { return GetStringByName(AccountSettingsNames.Reports_Payments_HeaderFontSize) ?? "24"; } }
        //public string Reports_Comments_HeaderFontSize { get { return GetStringByName(AccountSettingsNames.Reports_Comments_HeaderFontSize) ?? "16"; } }
        //public string Reports_PatientDetails_HeaderFontSize { get { return GetStringByName(AccountSettingsNames.Reports_PatientDetails_HeaderFontSize) ?? "16"; } }
        //public int ConsentDateUpdateTriggeredOnStatus { get { return GetInt32ByName(AccountSettingsNames.ConsentDateUpdateTriggeredOnStatus) ?? 0; } }
        //public int IsServicesRestrictedByResource { get { return GetInt32ByName(AccountSettingsNames.IsServicesRestrictedByResource) ?? 0; } }
        //public bool IsCodeCategoryRestrictedByModality { get { return GetBoolByName(AccountSettingsNames.IsCodeCategoryRestrictedByModality) ?? false; } }
        //public string VisitReasonName { get { return GetStringByName(AccountSettingsNames.VisitReasonName) ?? "Visit Reason"; } }
        //public bool GroupCommentsByAppointment { get { return GetBoolByName(AccountSettingsNames.GroupCommentsByAppointment) ?? true; } }
        //public bool SendEmailMeetingRequestForPoA { get { return GetBoolByName(AccountSettingsNames.SendEmailMeetingRequestForPoA) ?? false; } }
        //public bool IsProviderMandatory { get { return GetBoolByName(AccountSettingsNames.IsProviderMandatory) ?? false; } }
        //public bool IsAlertCommentsActive { get { return GetBoolByName(AccountSettingsNames.IsAlertCommentsActive) ?? false; } }
        //public bool UpdateAllNewToArrive { get { return GetBoolByName(AccountSettingsNames.UpdateAllNewToArrive) ?? false; } }
        //public bool AutoFormatPatientNameToUpperСase { get { return GetBoolByName(AccountSettingsNames.AutoFormatPatientNameToUpperСase) ?? false; } }
        //public bool EnableRescheduleReason { get { return GetBoolByName(AccountSettingsNames.EnableRescheduleReason) ?? false; } }
        //public bool EnableEhrTasksInScheduler { get { return GetBoolByName(AccountSettingsNames.EnableEhrTasksInScheduler) ?? false; } }
        //public bool EnableLikeSearchForPatientMrn { get { return GetBoolByName(AccountSettingsNames.EnableLikeSearchForPatientMrn) ?? false; } }
        //public bool EnableEmailValidation { get { return GetBoolByName(AccountSettingsNames.EnableEmailValidation) ?? false; } }
        //public bool EnableCombinedSearchFields { get { return GetBoolByName(AccountSettingsNames.EnableCombinedSearchFields) ?? false; } }
        //public bool WarnOnFooterCancel { get { return GetBoolByName(AccountSettingsNames.WarnOnFooterCancel) ?? false; } }
        //public bool IsScheduleFeeEnabled { get { return GetBoolByName(AccountSettingsNames.IsScheduleFeeEnabled) ?? false; } }
        //public bool ResetPayersOnFutureAppointments { get { return GetBoolByName(AccountSettingsNames.ResetPayersOnFutureAppointments) ?? false; } }
        //public bool AllowToScheduleForDifferentDOS { get { return GetBoolByName(AccountSettingsNames.AllowToScheduleForDifferentDOS) ?? false; } }
        //public bool IsScheduledPendingInCalendar { get { return GetBoolByName(AccountSettingsNames.IsScheduledPendingInCalendar) ?? false; } }
        //public bool IsMedicalHxTabVisible { get { return GetBoolByName(AccountSettingsNames.IsMedicalHxTabVisible) ?? false; } }
        //public bool IsPatientMruFromScheduledOnly { get { return GetBoolByName(AccountSettingsNames.IsPatientMruFromScheduledOnly) ?? false; } }
        //public bool IsPsvGridRowActive { get { return GetBoolByName(AccountSettingsNames.IsPsvGridRowActive) ?? false; } }
        //public bool IsAutoFillUniqueSearchResult { get { return GetBoolByName(AccountSettingsNames.IsAutoFillUniqueSearchResult) ?? false; } }
        //public bool IsPaymentPostingValidated { get { return GetBoolByName(AccountSettingsNames.IsPaymentPostingValidated) ?? false; } }
        //public bool VisitProceduresAlwaysCreateOrder { get { return GetBoolByName(AccountSettingsNames.VisitProceduresAlwaysCreateOrder) ?? false; } }
        //public int LimitNumericCharsCountInPhoneMobileField { get { return GetInt32ByName(AccountSettingsNames.LimitNumericCharsCountInPhoneMobileField) ?? 20; } }
        //public int PatientInformationDefaultCursorToTabIndex { get { return GetInt32ByName(AccountSettingsNames.PatientInformationDefaultCursorToTabIndex) ?? 0; } }
        //public bool EnableMailValidation { get { return GetBoolByName(AccountSettingsNames.EnableMailValidation) ?? false; } }
        //public bool ShowPatientOptOutOptions { get { return GetBoolByName(AccountSettingsNames.ShowPatientOptOutOptions) ?? false; } }
        //public bool AutoPopNewPayerExternalId { get { return GetBoolByName(AccountSettingsNames.AutoPopNewPayerExternalId) ?? false; } }
        //public bool SetRescheduleStatusIfDateChanged { get { return GetBoolByName(AccountSettingsNames.SetRescheduleStatusIfDateChanged) ?? false; } }
        //public bool RemoveOrdersOnCancelStatus { get { return GetBoolByName(AccountSettingsNames.RemoveOrdersOnCancelStatus) ?? false; } }
        //public bool AllowEditBlockedAppointment { get { return GetBoolByName(AccountSettingsNames.AllowEditBlockedAppointment) ?? false; } }
        //public bool IsAutoAssignFirstDefaultReferring { get { return GetBoolByName(AccountSettingsNames.IsAutoAssignFirstDefaultReferring) ?? false; } }
        //public bool IsAutoAssignMostRecentReferring { get { return GetBoolByName(AccountSettingsNames.IsAutoAssignMostRecentReferring) ?? false; } }
    }
}
