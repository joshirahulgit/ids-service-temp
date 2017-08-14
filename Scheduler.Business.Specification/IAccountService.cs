using Scheduler.Business.Entity;
using Scheduler.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Business.Specification
{
    public interface IAccountService : IContract
    {
        AppointmentResourcePhysicianDto GetDefaultProvider(string patientRecordNumber, string patientLocation, bool userIsDictator);

        AuditEntriesDto FindAudit(AuditRequestDto request);

        PhysicianSpecialitiesDto UpdateReferralSpecialities(PhysicianSpecialitiesDto request);

        VolumeUnitsDto UpdateVolumeUnits(VolumeUnitsDto request);

        CommentTypesDto UpdateCommentTypes(CommentTypesDto request);

        TechCompleteSuggestionsListDto UpdateTechCompleteSuggestions(TechCompleteSuggestionsListDto request);

        AppointmentStatusesDto UpdateAppointmentStatuses(AppointmentStatusesDto request);

        AccountGenerateIDconfigsDto GetAccountIdConfigurations();

        AuthorizationAlertsDto GetAuthorizationAlerts();

        AuthorizationAlertsDto UpdateAuthorizationAlerts(AuthorizationAlertsDto alerts);

        AccountGenerateIDconfigsDto UpdateAccountIdConfigurations(AccountGenerateIDconfigsDto idConfigs);

        void UpdatePhysicianType(PhysicianTypeDto updatedType);

        void RemoveCustomPayer(int payerId);

        CustomPayerDto CreatePayer(CustomPayerDto payer);

        AccountDto LoadAccount(long accountID);

        AccountDto CreateAccount(AccountDto newAccount);

        CustomPayersDto GetPayerSuggestionList(String searchString, string payerState, PayerSearchMode mode);

        void RemoveAccount(AccountDto account);

        AccountDto UpdateAccount(long accountId, AccountDto updatedAccount);

        AccountsDto GetAllAccounts();

        ReferralsDto FindReferrals(string searchKey);

        ReferralDto CreateReferring(ReferralsDto newReferral, bool ignoreWarning);

        ReferringNoteDto CreateReferringNote(string referralId, ReferringNoteDto note);

        ReferringNotesDto GetReferringNotes(string referralId);

        ReferringNotesBatchDto GetReferralNotesBatch(string[] referralIds);

        OrderTransformParametersDto FindTransformParameters(string searchKey);

        String GetOrderId(string locationId);

        ReferralDto UpdateReferring(ReferralsDto updatedReferral);

        String CheckExsistProcedure(List<ProcedureDto> procedures, DateTime appointEndDate, long ptientId, long appointmentID);

        AccountEnumsDto GetAccountEnumsByType(string type);

        AccountEnumsDto InsertUpdateAccountEnum(AccountEnumsDto accEnums);

        void UpdateCustomPayer(CustomPayerDto customPayer);

        CustomPayersDto FindCustomPayers(CustomPayerDto customPayer, PayerSearchMode mode);

        ReferralDto GetReferralByReferringId(string referringId);

        PaymentFeesDto GetPaymentFees(List<int> locationIds, List<int> codeReferenceIds);

        ReferralsDto FindReferral(string firstName, string lastName, string npiNum, string taxId, string phone, string zip, string group, bool includeDeleted);

        CrosswalkPayersDto FindPayersCrosswalk();

        CrosswalkPayersDto DeletePayerCrosswalk(int crosswalkId);

        CrosswalkPayersDto UpdatePayerCrosswalk(CrosswalkPayerDto crosswalk);

        CrosswalkPayersDto CreatePayerCrosswalk(CrosswalkPayerDto crosswalk);

        void UpdateFiltersConfiguration(AccountDto acct);

        UnhandledExceptionEntrysDto FindExceptions(ExceptionRequestDto req);

        void LogUnhandledException(UnhandledExceptionEntryDto req);

        void UpdateAccountSettings(AccountSettingCollectionDto req);

        UserProfilesDto LoadAllProfiles(int userId);

        UserProfilesDto LoadAllProfilesByParams(int? userId, List<long> roleIds);

        UserProfilesDto LoadProfileList(bool loadDetails);

        UserProfileDto LoadProfile(int id);

        void UpdateUserDefaultProfile(int userId, int profileId);

        void DeleteUserProfile(int profileId);

        void UpdateNotificationSlots(NotificationSlotsDto slots);

        NotificationSlotCommentsDto LoadNotificationSlotComments();

        SchedulerIntegrationAddressesDto FindAddress(string pattern);

        bool AllowAppointmentForOutofNetworkPrimaryPayer(long schedulerLocationId, string patientMrn);

        NotificationSlotsCptDto FindNotificationSlotsCptsById(int notificationSlotId);

        void UpdateNotificationSlotsCpts(NotificationSlotsCptDto slotCpts);

        void DeleteNotificationSlot(NotificationSlotsDto slot);

        PdfFileDto ConvertHtmlToPdf(byte[] html);

        bool EsquaredSendEmail(long appointmentId, string email, string subject, string htmlBody);
    }
}
