using Scheduler.Business.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scheduler.Business.Entity;
using Scheduler.Data.Implementation;
using Scheduler.DBEntity;
using Scheduler.Core;

namespace Scheduler.Business.Implementation
{
    internal class AccountService : ServiceBase, IAccountService
    {
        private static object _orderIdCreationLock = new object();
        static object lockObj = new object();

        private static Dictionary<long, AccountCacheItem> _accountCache = new Dictionary<long, AccountCacheItem>();


        public AccountEnumsDto GetAccountEnumsByType(string type)
        {
            return ExecuteReadOnlyCommand(locator => GetAccountEnumsByTypeCommand(locator, type));
        }

        public AccountEnumsDto InsertUpdateAccountEnum(AccountEnumsDto accEnums)
        {
            return ExecuteCommand(locator => InsertUpdateAccountEnumCommand(locator, accEnums));
        }

        private AccountEnumsDto InsertUpdateAccountEnumCommand(RepositoryLocator locator, AccountEnumsDto accEnums)
        {
            AccountEnumsDto result = new AccountEnumsDto();
            List<AccountEnum> accountEnums = new List<AccountEnum>();
            foreach (AccountEnumDto accountEnumDto in accEnums.AccountEnums)
                accountEnums.Add(accountEnumDto.ToDbEntity());

            List<AccountEnum> listAccountEnum = locator.AccountRepository.InsertUpdateAccountEnum(accountEnums);
            foreach (AccountEnum accountEnum in listAccountEnum)
            {
                result.AccountEnums.Add(accountEnum.ToDto());
            }
            return result;
        }

        private AccountEnumsDto GetAccountEnumsByTypeCommand(RepositoryLocator locator, string type)
        {
            List<AccountEnum> codes = locator.AccountRepository.GetAccountEnumsByType(type);
            AccountEnumsDto result = new AccountEnumsDto();
            foreach (AccountEnum code in codes)
            {
                result.AccountEnums.Add(code.ToDto());
            }
            return result;
        }

        public AppointmentResourcePhysicianDto GetDefaultProvider(string patientRecordNumber, string patientLocation, bool userIsDictator)
        {
            throw new NotImplementedException();
        }

        public AuditEntriesDto FindAudit(AuditRequestDto request)
        {
            throw new NotImplementedException();
        }

        public PhysicianSpecialitiesDto UpdateReferralSpecialities(PhysicianSpecialitiesDto request)
        {
            throw new NotImplementedException();
        }

        public VolumeUnitsDto UpdateVolumeUnits(VolumeUnitsDto request)
        {
            throw new NotImplementedException();
        }

        public CommentTypesDto UpdateCommentTypes(CommentTypesDto request)
        {
            throw new NotImplementedException();
        }

        public TechCompleteSuggestionsListDto UpdateTechCompleteSuggestions(TechCompleteSuggestionsListDto request)
        {
            throw new NotImplementedException();
        }

        public AppointmentStatusesDto UpdateAppointmentStatuses(AppointmentStatusesDto request)
        {
            throw new NotImplementedException();
        }

        public AccountGenerateIDconfigsDto GetAccountIdConfigurations()
        {
            throw new NotImplementedException();
        }

        public AuthorizationAlertsDto GetAuthorizationAlerts()
        {
            throw new NotImplementedException();
        }

        public AuthorizationAlertsDto UpdateAuthorizationAlerts(AuthorizationAlertsDto alerts)
        {
            throw new NotImplementedException();
        }

        public AccountGenerateIDconfigsDto UpdateAccountIdConfigurations(AccountGenerateIDconfigsDto idConfigs)
        {
            throw new NotImplementedException();
        }

        public void UpdatePhysicianType(PhysicianTypeDto updatedType)
        {
            throw new NotImplementedException();
        }

        public void RemoveCustomPayer(int payerId)
        {
            throw new NotImplementedException();
        }

        public CustomPayerDto CreatePayer(CustomPayerDto payer)
        {
            throw new NotImplementedException();
        }

        public AccountDto LoadAccount(long accountID)
        {
            throw new NotImplementedException();
        }

        public AccountDto CreateAccount(AccountDto newAccount)
        {
            throw new NotImplementedException();
        }

        public CustomPayersDto GetPayerSuggestionList(string searchString, string payerState, PayerSearchMode mode)
        {
            throw new NotImplementedException();
        }

        public void RemoveAccount(AccountDto account)
        {
            throw new NotImplementedException();
        }

        public AccountDto UpdateAccount(long accountId, AccountDto updatedAccount)
        {
            throw new NotImplementedException();
        }

        public AccountsDto GetAllAccounts()
        {
            throw new NotImplementedException();
        }

        public ReferralsDto FindReferrals(string searchKey)
        {
            throw new NotImplementedException();
        }

        public ReferralDto CreateReferring(ReferralsDto newReferral, bool ignoreWarning)
        {
            throw new NotImplementedException();
        }

        public ReferringNoteDto CreateReferringNote(string referralId, ReferringNoteDto note)
        {
            throw new NotImplementedException();
        }

        public ReferringNotesDto GetReferringNotes(string referralId)
        {
            throw new NotImplementedException();
        }

        public ReferringNotesBatchDto GetReferralNotesBatch(string[] referralIds)
        {
            throw new NotImplementedException();
        }

        public OrderTransformParametersDto FindTransformParameters(string searchKey)
        {
            throw new NotImplementedException();
        }

        public string GetOrderId(string locationId)
        {
            throw new NotImplementedException();
        }

        public ReferralDto UpdateReferring(ReferralsDto updatedReferral)
        {
            throw new NotImplementedException();
        }

        public string CheckExsistProcedure(List<ProcedureDto> procedures, DateTime appointEndDate, long ptientId, long appointmentID)
        {
            throw new NotImplementedException();
        }

        public void UpdateCustomPayer(CustomPayerDto customPayer)
        {
            throw new NotImplementedException();
        }

        public CustomPayersDto FindCustomPayers(CustomPayerDto customPayer, PayerSearchMode mode)
        {
            throw new NotImplementedException();
        }

        public ReferralDto GetReferralByReferringId(string referringId)
        {
            throw new NotImplementedException();
        }

        public PaymentFeesDto GetPaymentFees(List<int> locationIds, List<int> codeReferenceIds)
        {
            throw new NotImplementedException();
        }

        public ReferralsDto FindReferral(string firstName, string lastName, string npiNum, string taxId, string phone, string zip, string group, bool includeDeleted)
        {
            throw new NotImplementedException();
        }

        public CrosswalkPayersDto FindPayersCrosswalk()
        {
            throw new NotImplementedException();
        }

        public CrosswalkPayersDto DeletePayerCrosswalk(int crosswalkId)
        {
            throw new NotImplementedException();
        }

        public CrosswalkPayersDto UpdatePayerCrosswalk(CrosswalkPayerDto crosswalk)
        {
            throw new NotImplementedException();
        }

        public CrosswalkPayersDto CreatePayerCrosswalk(CrosswalkPayerDto crosswalk)
        {
            throw new NotImplementedException();
        }

        public void UpdateFiltersConfiguration(AccountDto acct)
        {
            throw new NotImplementedException();
        }

        public UnhandledExceptionEntrysDto FindExceptions(ExceptionRequestDto req)
        {
            throw new NotImplementedException();
        }

        public void LogUnhandledException(UnhandledExceptionEntryDto req)
        {
            throw new NotImplementedException();
        }

        public void UpdateAccountSettings(AccountSettingCollectionDto req)
        {
            throw new NotImplementedException();
        }

        public UserProfilesDto LoadAllProfiles(int userId)
        {
            throw new NotImplementedException();
        }

        public UserProfilesDto LoadAllProfilesByParams(int? userId, List<long> roleIds)
        {
            throw new NotImplementedException();
        }

        public UserProfilesDto LoadProfileList(bool loadDetails)
        {
            throw new NotImplementedException();
        }

        public UserProfileDto LoadProfile(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateUserDefaultProfile(int userId, int profileId)
        {
            throw new NotImplementedException();
        }

        public void DeleteUserProfile(int profileId)
        {
            throw new NotImplementedException();
        }

        public void UpdateNotificationSlots(NotificationSlotsDto slots)
        {
            throw new NotImplementedException();
        }

        public NotificationSlotCommentsDto LoadNotificationSlotComments()
        {
            throw new NotImplementedException();
        }

        public SchedulerIntegrationAddressesDto FindAddress(string pattern)
        {
            throw new NotImplementedException();
        }

        public bool AllowAppointmentForOutofNetworkPrimaryPayer(long schedulerLocationId, string patientMrn)
        {
            throw new NotImplementedException();
        }

        public NotificationSlotsCptDto FindNotificationSlotsCptsById(int notificationSlotId)
        {
            throw new NotImplementedException();
        }

        public void UpdateNotificationSlotsCpts(NotificationSlotsCptDto slotCpts)
        {
            throw new NotImplementedException();
        }

        public void DeleteNotificationSlot(NotificationSlotsDto slot)
        {
            throw new NotImplementedException();
        }

        public PdfFileDto ConvertHtmlToPdf(byte[] html)
        {
            throw new NotImplementedException();
        }

        public bool EsquaredSendEmail(long appointmentId, string email, string subject, string htmlBody)
        {
            throw new NotImplementedException();
        }

        public class AccountCacheItem
        {
            public AccountCacheItem(DateTime createTime, Account account)
            {
                CreateTime = createTime;
                Account = account;
            }

            public DateTime CreateTime { get; set; }
            public DateTime UpdateTime { get; set; }
            public Account Account { get; set; }
        }

        public static Dictionary<long, AccountCacheItem> AccountCache
        {
            get { return _accountCache; }
        }

        
    }
}
