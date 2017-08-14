using Scheduler.Core;
using Scheduler.DBEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Data.Specification
{
    public interface IResourceRepository : IRepository, ICanRemove<AppointmentResource>, ICanCreate<AppointmentResource>, ICanUpdate<AppointmentResource>, ICanGetById<AppointmentResource, ResourceID>
    {
        ModalityType GetModalityTypeById(long typeID);
        ResourceLocation GetLocationById(long locationID);
        AppointmentResourceType GetTypeById(long typeID);
        RoomType GetRoomTypeById(long typeID);
        List<AppointmentResource> GetResourcesByType(long accountID, long typeId);
        List<AppointmentResource> GetTimeResources(List<TimeResourceRequest> ranges, long accountID);
        List<AppointmentResourcePatient> FindPatients(long accountID, AppointmentResourcePatient patient);
        List<AppointmentResourcePatient> FindPatientsWithLocation(long accountID, AppointmentResourcePatient patient, List<int> locations);
        Payer CreatePayer(long patientID, string patientNumber, Payer payer);
        Payer VerifyPayer(long patientId, Payer payer);
        Payer UpdatePayer(long patientId, string patientNumber, Payer payer);
        void RemovePayer(long patientId, string patientNumber, Payer payer);
        PatientComment CreateComment(long patientID, PatientComment comment, bool b);
        List<PatientComment> GetPatientComments(long patientID, long accountID);
        void RemovePatientCommentsByAppointmentId(long accountID, long apptId);
        AppointmentResourcePatient RequestNewPatient(long accountID, int location);
        Payer.PayerStatus GetPayerStatus(Payer payer, String recordNo);
        void ContactPatientByCall(long patientId, string message);
        void ContactPatientByEmail(long patientId, string message);
        void ContactPatientByMail(long patientId, string message);
        void ContactPatientBySMS(long patientId, string message);
        void NotifyPatientByCall(long patientId, string message);
        void NotifyPatientByMail(long patientId, string message);
        void NotifyPatientByEmail(long patientId, string message);
        void NotifyPatientBySMS(long patientId, string message);
        int CreateGuarantor(PatientGuarantor guarantor);
        void UpdateGuarantor(PatientGuarantor guarantor);
        List<AppointmentResourcePatient> CheckExistPatients(AppointmentResourcePatient patient);

        List<PatientGuarantor> GetAllGuarantors(string recordNumber, long? appointmentId);
        List<CustomPayer> GetPayersSuggestionList(string searchString, string payerState, PayerSearchMode mode);
        CustomPayer CreateCustomPayer(CustomPayer customPayer);
        void UpdateCustomPayer(CustomPayer customPayer);
        List<CustomPayer> FindCustomPayers(CustomPayer customPayer, PayerSearchMode mode);
        void RemoveCustomPayer(int payerId);
        Address CreatePatientAdditionalAddress(long patientId, Address address);
        void RemovePatientAdditionalAddress(Address address);
        void UpdatePatientAdditionalAddress(Address address);
        List<PatientAuthorization> GetAllPatientAuthorizations(long patientId);
        PatientAuthorization AddPatientAuthorization(PatientAuthorization authObj);
        PatientAuthorization UpdatePatientAuthorization(PatientAuthorization authObj);
        void RemoveReservedAuthorization(long patientId, PatientAuthorization authObj);
        void DeactivateAuthorizationUnit(long patientId, long appointmentId, long authId);
        void DeactivateAuthorizationUnit(long authId);
        long ActivateAuthorizationUnit(int itemsCount, long appointmentId, string comment, PatientAuthorization item);
        List<UsedAuthorization> GetUsedAuthorizations(long patientId, long appointmentId);
        PatientAuthorization GetPatientAuthById(long id);
        List<Payer> GetPatientInsurances(string patientMRN);
        List<PatientVisitHistory> GetPatientVisitHistories(long patientID);

        void DeleteComment(int commentId);
        void DeleteReferralComment(int commentId);

        PatientPaymentCollection CreateMultiplePayments(PatientPaymentCollection PaymentCollection);
        PatientPayment CreatePayment(PatientPayment Payment, bool processPayment);
        List<PatientPayment> GetPatientPayments(long patientID, long accountID);
        void RemovePatientPaymentsByAppointmentId(long accountID, long apptId);
        void DeletePayment(int PaymentId);

        PatientGuarantor GetGuarantor(int guarantorId);
        List<PatientContact> GetPatientContacts(long patientId);
        PatientContact UpdatePatientContact(PatientContact contact);
        void DeletePatientContact(long contactId);
        PatientContact AddPatientContact(long patientId, PatientContact contact);
        PatientContact GetPatientContactById(long contactId);
        void RemovePatientAuthorization(long authId, string comment);
        PatientEmployment CreatePatientEmployment(long patientId, PatientEmployment patientEmployment);
        void UpdatePatientEmployment(PatientEmployment patientEmployment);
        void PreCache(Dictionary<ResourceID, AppointmentResource> resourcesCache, ResourceTypes resType, List<long> resourceIds);
        List<Referral> FindReferral(string firstName, string lastName, string npiNum, string taxId, string phone, string zip, string group, bool includeDeleted);
        List<PatientGuarantor> GetAllGuarantors(int patientId);
        List<Payer> GetLocalPayers();
        List<string> GetReferralsHintIds(string patientMrn);
        Referral GetReferralByReferralId(string refId);
        AppointmentResourcePatient LoadPatientResources(AppointmentResourcePatient patient);
        void AttachGuarantorToAppointment(long guarantorId, long apptId);
        void DetachGuarantorFromAppointment(long guarantorId, long apptId);
        List<long> GetAttachedGuarantors(long apptId);
        void LoadPatientComments(AppointmentResourcePatient patient);
        List<AppointmentResource> GetAllModalities();
        AppointmentResourcePatient LoadPendingPhysicianPatientDetails(long l);
        List<SchedulerImage> GetPatientImages(string mrn);
        List<Race> ReadPatientRaces(long id);
        //        void CleanSchedulerPatientInsurancesSortOrderTable(long value);
        List<AppointmentResource> GetByResourceIds(List<ResourceID> ids);
        Payer GetPatientInsuranceById(int i);
        List<Task> FindPatientTasks(int patientId);
        List<TaskTemplate> GetTemplates();
        List<AppointmentResourcePatient> LoadLastTenPatients();
        void LoadPatientInsuranceInfo(List<Appointment> foundResults);
        PatientIdentifier AddMultipleIdentifiers(PatientIdentifier pi);
        void UpdateMultipleIdentifiers(PatientIdentifier pi);
        AppointmentResourcePatient FindPatientByMrn(long accountId, string patientreq);
        List<AppointmentCheckListValue> GetAppoinmentCheckListValues(long? appId);
        void AssociateCommentsWithAppointment(List<PatientComment> patComments);
        List<PatientIdentifier> GetPatientMultipleIdentifiers(long patientId);
        void AttachCptsToAppointment(long appId, List<Procedure> sProcs, List<Diagnosis> sDiags, bool initiateConnection);

        PatientAdditionalData GetPatientAdditionalData(long patientId);
        Task GetTaskById(int taskId);
        string ResetPatientInsuranceFutureAppointments(long patIntId);

        List<PatientFamilyHistoryProblem> GetPatientFamilyHistoryProblems(long patientId, string patientMrn);

        void UpdatePatientSelfPay(AppointmentResourcePatient appointmentResourcePatient);

        List<SchedulerModalityVirtualRoom> GetAllModalityVirtualRooms();

        SchedulerModalityVirtualRoom SaveModalityVirtualRoom(SchedulerModalityVirtualRoom vrg);
        SchedulerModalityVirtualRoom DeleteModalityVirtualRoom(SchedulerModalityVirtualRoom schedulerModalityVirtualRoom);
        int CreateInvoice(PatientInvoice invoice);
        List<PatientInvoice> GetInvoices(long patientId);
    }
}
