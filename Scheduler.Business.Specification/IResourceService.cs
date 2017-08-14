using Scheduler.Business.Entity;
using Scheduler.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Business.Specification
{
    public interface IResourceService : IContract
    {
        ProgressDto CheckVerificationProgress(string verificationProcessId);

        PatientIdentifiersDto SaveMultipleIdentifiers(List<PatientIdentifierDto> multipleIdentifiers);

        String VerifyPayersBatch(Dictionary<long, int> patientsWithProviderDICNPI);

        AppointmentResourcePatientsDto FindPatients(long accountID, AppointmentResourcePatientDto patient);

        UsedAuthorizationsDto GetUsedAuthorizations(long patientId, long appointmentId);

        String GetVersionOfServer();

        void DeactivateAuthorizationUnit(long patientId, long appointmentId, UsedAuthorizationDto authObj);

        void ActivateAuthorizationUnit(int itemCount, long appointmentId, string comment,
                                             PatientAuthorizationDto authObj);

        void RemoveReservedAuthorization(long patientId, PatientAuthorizationDto authObj);

        void DeleteComment(int commentId);

        void DeleteReferralComment(int commentId);

        PatientAuthorizationsDto AddPatientAuthorization(PatientAuthorizationDto authObj);

        PatientAuthorizationsDto UpdatePatientAuthorization(PatientAuthorizationDto authObj);

        PatientAuthorizationsDto GetAuthorizationHistory(long patientId);

        PayersDto UpdatePatientPayers(AppointmentResourcePatientDto patientId, List<PayerDto> newPayers);

        PatientVisitHistoriesDto GetPatientVisitHistories(long patientID);

        AppointmentResourcePatientsDto FindPatientsWithLocations(long accountID, AppointmentResourcePatientDto patient,
                                                                 List<int> locations);

        AppointmentResourcePatientDto FindPatient(long patientId);

        AppointmentResourcePatientsDto CheckExistPatients(AppointmentResourcePatientDto patient);

        void NotifyPatient(long patientId, ContactPatientMethods method, string message);

        void ContactPatient(long patientId, ContactPatientMethods method, string message);

        PatientGuarantorDto CreateGuarantor(PatientGuarantorDto guarantor);

        void UpdateGuarantor(PatientGuarantorDto guarantor);

        PatientGuarantorsDto GetAllGuarantors(string recordNumber, long? appointmentId);

        PatientGuarantorsDto GetAllGuarantorsByPatientId(int patientId);

        PatientGuarantorDto GetGuarantor(int guarantorId);

        PatientCommentDto CreatePatientComment(long patientId, PatientCommentDto comment);

        void CreatePatientComments(Dictionary<long, PatientCommentDto> comments);

        void CreatePatientMultipleComments(long patientId, List<PatientCommentDto> comments);

        AppointmentResourceDto CreateResource(AppointmentResourceDto newResource);

        PayerDto CreateNewPayer(PayerDto payer);

        AppointmentResourceDto GetResourceById(long id, long typeID);

        AppointmentSourcesDto GetResourcesByType(long accountID, long typeID);

        PatientCommentsDto GetPatientComments(long patientID);

        PayersDto GetPatientPayers(long patientID);

        PayersDto GetPatientPayersByMRN(string patientMRN);

        void RemovePayer(long patientId, long payerId);

        PayerDto UpdatePayer(PayerDto updatedPayer);

        AppointmentResourceDto UpdateResource(long resourceId, AppointmentResourceDto updatedResource);

        void DeleteResource(long accountID, long resourceID, long typeID);

        AppointmentResourcePatientDto RequestNewPatient(long accountID, int? location);

        String RequestNewPayerExternalId(long accountID, int? location);

        String RequestNewReferringId(long accountID, int? location);

        String GeneratePatientMrn(int? location);

        String GenerateOrderId(int? location);

        PayerDto VerifyInsurance(PayerDto insurance, long patientId);

        PayerDto.PayerStatusDto RequestPayerStatus(PayerDto payer, string recordNum);

        PatientPaymentsDto CreateMultiplePayments(PatientPaymentsDto PaymentCollection);

        PatientPaymentDto CreatePayment(PatientPaymentDto Payment);

        PatientInvoiceDto CreateInvoice(PatientInvoiceDto invoice);

        PatientInvoicesDto GetInvoices(long patientId);

        PatientPaymentsDto GetPatientPayments(long patientID, long accountID);

        PatientContactsDto GetPatientContacts(long patientID);

        PatientContactDto GetPatientContact(long contactId);

        PatientContactDto UpdatePatientContact(PatientContactDto contact);

        void DeletePatientContact(long contactId);

        PatientContactDto AddPatientContact(long patientID, PatientContactDto contact);

        void RemovePatientAuthorization(long authId, string comment);

        PayersDto GetLocalPayers();


        ReferralsDto GetReferralsHint(string patientMrn);

        AppointmentResourcePatientDto LoadPatientResources(AppointmentResourcePatientDto patient);

        AppointmentResourcePatientsDto LoadPatientsResources(AppointmentResourcePatientsDto patient);

        SurgeonsDto GetSurgeonWhos(int type, string filter);

        PathologyPathsResultsDto FilterPathResults(string filter);

        AppointmentResourceModalitiesDto FindAllModalities();

        SchedulerModalityVirtualRoomsDto FindAllSchedulerModalityVirtualRooms();

        SchedulerModalityVirtualRoomDto SaveSchedulerModalityVirtualRoom(SchedulerModalityVirtualRoomDto item);

        SchedulerModalityVirtualRoomDto DeleteSchedulerModalityVirtualRoom(SchedulerModalityVirtualRoomDto item);

        CommentTypesDto GetPatientCommentTypes();

        TasksDto FindPatientTasks(int patientId);

        AppointmentResourcePatientsDto LoadLastTenPatients();

        AppointmentResourcePatientsDto FindPatientById(AppointmentResourcePatientDto patient);

        AppointmentResourcePatientsDto FindPatientByMrn(long accountID, AppointmentResourcePatientDto patient);

        GeocodingsDto FindLocationDetailsByZipCode(AddressDto address);

        void AssociateCommentsWithAppointment(List<PatientCommentDto> comments);

        bool SavePatientDemographics(AppointmentResourcePatientDto pat);

        PatientAdditionalDataDto GetPatientAdditionalData(long patientId);

        AppointmentResourcePatientsDto AddPatientMrnToMruList(long patientIntId, string patientreq);

        String ResetPatientInsuranceFutureAppointments(long patIntId);

        PatientFamilyHistoryProblemsDto GetPatientFamilyHistoryProblems(long patientId, string patientMrn);
    }
}
