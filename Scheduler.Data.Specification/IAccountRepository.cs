using Scheduler.Core;
using Scheduler.DBEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scheduler.Data.Specification
{
    public interface IAccountRepository : IRepository, ICanGetById<Account, long>, ICanCreate<Account>, ICanRemove<Account>, ICanUpdate<Account>
    {
        List<Account> FindAllAccounts();
        String GetEmailTemplate(int emailType);
        Dictionary<String, String> GetApplicationConfig(string appName, string accountName, string userName);

        //Referrals section
        List<Referral> FindReferrals(String searchString);
        Referral CreateReferral(Referral newReferral);
        Referral GetReferralById(long refId);
        Referral UpdateReferral(Referral updatedReferring);

        ReferringNote CreateReferringNote(string referringID, ReferringNote note);
        List<ReferringNote> GetReferringNotes(string referringID);
        string CheckExistReferral(Referral referral);

        //=======================ORDERS CREATION RELATED SECTION=======================================
        //String CreateOrder(OrderCreateParametersDto orderCreateParameters);
        String CreateOrderEx(long appId, int? appItemType, String appItemId, OrderCreationValues values);
        void RemoveOrder(long orderId);
        void ChangeOrderMapping(long orderId, long newAppId, long? newAppItemType, String newAppItemId);
        void UpdateOrderScheduleWhenNecessary(long orderId);
        AppointmentOrder UpdateOrder(AppointmentOrder order);

        List<String> FindRefTypesForOrderPhysicianID(string accountName);
        List<String> FindRefTypesForOrderСС(string accountName);
        String FindWorkTypeDescription(string accountName, string workType, long locationId);
        //==============================================================================================

        void UpdateOrderLinkedItem(long orderId, long? newAppointmentItemType, String newAppointmentItemId);

        WorkingSchedule GetAccountWorkingSchedule();

        List<Procedure> GetProcedureSuggestionList(String searchString, int categoryFilter, CPTCodeSearchMode mode, bool exactMatch);
        List<Procedure> GetLocalProcedureAdminList();
        List<Diagnosis> GetLocalDiagnosesAdminList();
        List<CPTModifier> GetModifierSuggestionList(String searchString);
        List<Diagnosis> GetDiagnosisSuggestionListCommand(String searchString, int categoryFilter, CPTCodeSearchMode mode);
        void UpdatePhysicianType(PhysicianType type);
        Procedure AddProcedure2LocalStorage(Procedure procedure);
        Diagnosis AddDiagnosis2LocalStorage(Diagnosis diagnosis);
        List<OrderTransformParameter> FindOrderTransformParams(String mapFieldValue, long accountId);
        OrderTransformParameter GetOrderTransformParamForProcCode(String mapFieldValue, long accountId);
        //        String                        GetWTValueByCptCode(String cptCode,long accountId);
        void UpdateProcedureGuid(String oldGuid, String newGuid);
        void UpdateDiagnosisGuid(String oldGuid, String newGuid);
        int ReserveNewSeqForId(long idGeneratorId);
        AccountGenerateIDconfig GetIdGenerator(string locationId, IdGenerationTypeName typeName);
        bool CheckIfNewIdExistsInAccount(string newId, string idType);
        AccountGenerateIDconfig UpdateAccountIdConfiguration(AccountGenerateIDconfig updatedType);
        List<AccountGenerateIDconfig> FindAllGenerateIdConfiguration();
        AccountGenerateIDconfig CreateAccountIdConfiguration(AccountGenerateIDconfig created);
        void DeleteAccountIdConfiguration(AccountGenerateIDconfig genId);
        long ResolveIdByAccountName(string accountName, string user);
        string ResolveNameByAccountId(long accountID);
        List<AuthorizationAlert> GetAuthorizationAlerts();
        List<AuthorizationAlert> UpdateAuthorizationAlerts(List<AuthorizationAlert> serverAlerts);
        List<AppointmentStatus> UpdateAppointmentStatuses(List<AppointmentStatus> appointmentStatuses);
        List<CommentType> UpdateCommentTypes(List<CommentType> commentTypes);
        List<SnomedProcedure> GetSnomedSuggestionList(string searchString);
        List<VolumeUnit> UpdateVolumeUnits(List<VolumeUnit> serverObj);
        List<PhysicianSpeciality> UpdateReferralSpecialities(List<PhysicianSpeciality> phBusiness);
        AppointmentResourcePhysician GetDefaultProvider(string patientLocation, bool userIsDictator);
        //string CheckExistProcedure(List<ProcedureDto> procedures, DateTime appointEndDate, long patientId, long appointmentID);
        string GetAppointmentStatusById(long newStatusId);
        List<TechCompleteSuggestionList> UpdateTechCompleteSuggestionList(List<TechCompleteSuggestionList> toS);
        List<AccountEnum> InsertUpdateAccountEnum(List<AccountEnum> accEnums);
        void UpdateOrderForRoom(long orderId, long appointmentId, AppointmentResourceModality room);
        void UpdateOrderForProcedure(long orderId, long appointmentId, Procedure procedure);
        Diagnosis GetDiagnosisById(long id);
        Procedure GetProcedureById(long id);
        void DeactivateLocalCpt(long id);
        List<AccountEnum> GetAccountEnumsByType(string type);
        AppointmentResourcePhysician GetProviderBySignature(string dictatorSignature);
        Referral GetReferralByReferringId(string toString);
        List<CrosswalkPayer> GetCrossWalkPayers();
        void DeletePayerCrosswalk(int crosswalkId);
        void UpdatePayerCrosswalk(CrosswalkPayer crosswalk);
        void CreateCrosswalk(CrosswalkPayer crosswalk);
        void UpdateFiltersConfiguration(Account acct);
        int CreatePatientVisit(Appointment appointment);
        void MarkVisitAsSchedulerCreated(long id, int paitentVisitid);
        int AttachProcedureToPatientVisit(int patientVisitId, Procedure procedure);
        int GetPatientVisitIdByAppointmentId(long appId);
        int AttachDiagnosisToPatientVisit(int patientVisitId, Diagnosis diagnosis);
        void RemoveExistingCptsInPatientVisit(int patientVisitId);
        List<Procedure> GetPatientVisitProceduresBytVisitId(int patientVisitId);
        List<Diagnosis> GetPatientVisitDiagnosesBytVisitId(int patientVisitId);
        bool CheckIfPaitentVivistExists(long id);
        List<NotificationSlot> FindAllNotificationSlots(bool initiateConnection);
        void SendMessageToE2(string accountName, string key, int evtTypId, string additionalInfo);
        List<Procedure> GetProcedureSuggestionListWithRoom(string searchString, int category, CPTCodeSearchMode mode, bool exactMatch, long? roomId);
        void UpdateNotificationSlots(List<NotificationSlot> sts);
        void DeleteNotificationSlots(List<NotificationSlot> sts);
        void GetAdditionalUserInformation(IRequestContext rc);
        void AttachVitalsToPatientVisit(int patientVisitId, Visit visit);
        void UpdateVitalsToPatientVisit(int patientVisitId, Visit visit);
        List<CommentType> GetCommentTypes();
        List<PaymentFee> GetPaymentFees(List<int> locationIds, List<int> codeReferenceIds, DateTime? date);
        bool AllowAppointmentForOutofNetworkPrimaryPayer(long schedulerLocationId, string patientMrn);
        void CreatePatientMruEntry(int userIdInt, string userName, long id, string patientreq);
        List<NotificationSlotCpt> FindNotificationSlotsCptsById(int notificationSlotId);
        void UpdateNotificationSlotsCpts(List<NotificationSlotCpt> slots);
        void UpdateEhrMaritalStatus(bool p0, int patientVisitId);
        void UpdateEhrProvider(AppointmentResourcePhysician phys, int patientVisitId);
        void UpdateEhrLocation(long? locationId, int patientVisitId);
        void UpdateEhrChiefComplaint(string visitVisitReason, int patientVisitId);
        void UpdateEhrIsPregnant(bool visitIsPregnant, int patientVisitId);
        bool EsquaredSendEmail(long appointmentId, string email, string subject, string htmlBody);
        List<NotificationSlotComment> GetNotificationSlotComments();
    }
}
