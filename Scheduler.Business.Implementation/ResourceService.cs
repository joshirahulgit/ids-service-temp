using Scheduler.Business.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scheduler.Business.Entity;
using Scheduler.Core;

namespace Scheduler.Business.Implementation
{
    public class ResourceService : ServiceBase, IResourceService
    {
        public void ActivateAuthorizationUnit(int itemCount, long appointmentId, string comment, PatientAuthorizationDto authObj)
        {
            throw new NotImplementedException();
        }

        public PatientAuthorizationsDto AddPatientAuthorization(PatientAuthorizationDto authObj)
        {
            throw new NotImplementedException();
        }

        public PatientContactDto AddPatientContact(long patientID, PatientContactDto contact)
        {
            throw new NotImplementedException();
        }

        public AppointmentResourcePatientsDto AddPatientMrnToMruList(long patientIntId, string patientreq)
        {
            throw new NotImplementedException();
        }

        public void AssociateCommentsWithAppointment(List<PatientCommentDto> comments)
        {
            throw new NotImplementedException();
        }

        public AppointmentResourcePatientsDto CheckExistPatients(AppointmentResourcePatientDto patient)
        {
            throw new NotImplementedException();
        }

        public ProgressDto CheckVerificationProgress(string verificationProcessId)
        {
            throw new NotImplementedException();
        }

        public void ContactPatient(long patientId, ContactPatientMethods method, string message)
        {
            throw new NotImplementedException();
        }

        public PatientGuarantorDto CreateGuarantor(PatientGuarantorDto guarantor)
        {
            throw new NotImplementedException();
        }

        public PatientInvoiceDto CreateInvoice(PatientInvoiceDto invoice)
        {
            throw new NotImplementedException();
        }

        public PatientPaymentsDto CreateMultiplePayments(PatientPaymentsDto PaymentCollection)
        {
            throw new NotImplementedException();
        }

        public PayerDto CreateNewPayer(PayerDto payer)
        {
            throw new NotImplementedException();
        }

        public PatientCommentDto CreatePatientComment(long patientId, PatientCommentDto comment)
        {
            throw new NotImplementedException();
        }

        public void CreatePatientComments(Dictionary<long, PatientCommentDto> comments)
        {
            throw new NotImplementedException();
        }

        public void CreatePatientMultipleComments(long patientId, List<PatientCommentDto> comments)
        {
            throw new NotImplementedException();
        }

        public PatientPaymentDto CreatePayment(PatientPaymentDto Payment)
        {
            throw new NotImplementedException();
        }

        public AppointmentResourceDto CreateResource(AppointmentResourceDto newResource)
        {
            throw new NotImplementedException();
        }

        public void DeactivateAuthorizationUnit(long patientId, long appointmentId, UsedAuthorizationDto authObj)
        {
            throw new NotImplementedException();
        }

        public void DeleteComment(int commentId)
        {
            throw new NotImplementedException();
        }

        public void DeletePatientContact(long contactId)
        {
            throw new NotImplementedException();
        }

        public void DeleteReferralComment(int commentId)
        {
            throw new NotImplementedException();
        }

        public void DeleteResource(long accountID, long resourceID, long typeID)
        {
            throw new NotImplementedException();
        }

        public SchedulerModalityVirtualRoomDto DeleteSchedulerModalityVirtualRoom(SchedulerModalityVirtualRoomDto item)
        {
            throw new NotImplementedException();
        }

        public PathologyPathsResultsDto FilterPathResults(string filter)
        {
            throw new NotImplementedException();
        }

        public AppointmentResourceModalitiesDto FindAllModalities()
        {
            throw new NotImplementedException();
        }

        public SchedulerModalityVirtualRoomsDto FindAllSchedulerModalityVirtualRooms()
        {
            throw new NotImplementedException();
        }

        public GeocodingsDto FindLocationDetailsByZipCode(AddressDto address)
        {
            throw new NotImplementedException();
        }

        public AppointmentResourcePatientDto FindPatient(long patientId)
        {
            throw new NotImplementedException();
        }

        public AppointmentResourcePatientsDto FindPatientById(AppointmentResourcePatientDto patient)
        {
            throw new NotImplementedException();
        }

        public AppointmentResourcePatientsDto FindPatientByMrn(long accountID, AppointmentResourcePatientDto patient)
        {
            throw new NotImplementedException();
        }

        public AppointmentResourcePatientsDto FindPatients(long accountID, AppointmentResourcePatientDto patient)
        {
            throw new NotImplementedException();
        }

        public AppointmentResourcePatientsDto FindPatientsWithLocations(long accountID, AppointmentResourcePatientDto patient, List<int> locations)
        {
            throw new NotImplementedException();
        }

        public TasksDto FindPatientTasks(int patientId)
        {
            throw new NotImplementedException();
        }

        public string GenerateOrderId(int? location)
        {
            throw new NotImplementedException();
        }

        public string GeneratePatientMrn(int? location)
        {
            throw new NotImplementedException();
        }

        public PatientGuarantorsDto GetAllGuarantors(string recordNumber, long? appointmentId)
        {
            throw new NotImplementedException();
        }

        public PatientGuarantorsDto GetAllGuarantorsByPatientId(int patientId)
        {
            throw new NotImplementedException();
        }

        public PatientAuthorizationsDto GetAuthorizationHistory(long patientId)
        {
            throw new NotImplementedException();
        }

        public PatientGuarantorDto GetGuarantor(int guarantorId)
        {
            throw new NotImplementedException();
        }

        public PatientInvoicesDto GetInvoices(long patientId)
        {
            throw new NotImplementedException();
        }

        public PayersDto GetLocalPayers()
        {
            throw new NotImplementedException();
        }

        public PatientAdditionalDataDto GetPatientAdditionalData(long patientId)
        {
            throw new NotImplementedException();
        }

        public PatientCommentsDto GetPatientComments(long patientID)
        {
            throw new NotImplementedException();
        }

        public CommentTypesDto GetPatientCommentTypes()
        {
            throw new NotImplementedException();
        }

        public PatientContactDto GetPatientContact(long contactId)
        {
            throw new NotImplementedException();
        }

        public PatientContactsDto GetPatientContacts(long patientID)
        {
            throw new NotImplementedException();
        }

        public PatientFamilyHistoryProblemsDto GetPatientFamilyHistoryProblems(long patientId, string patientMrn)
        {
            throw new NotImplementedException();
        }

        public PayersDto GetPatientPayers(long patientID)
        {
            throw new NotImplementedException();
        }

        public PayersDto GetPatientPayersByMRN(string patientMRN)
        {
            throw new NotImplementedException();
        }

        public PatientPaymentsDto GetPatientPayments(long patientID, long accountID)
        {
            throw new NotImplementedException();
        }

        public PatientVisitHistoriesDto GetPatientVisitHistories(long patientID)
        {
            throw new NotImplementedException();
        }

        public ReferralsDto GetReferralsHint(string patientMrn)
        {
            throw new NotImplementedException();
        }

        public AppointmentResourceDto GetResourceById(long id, long typeID)
        {
            throw new NotImplementedException();
        }

        public AppointmentSourcesDto GetResourcesByType(long accountID, long typeID)
        {
            throw new NotImplementedException();
        }

        public SurgeonsDto GetSurgeonWhos(int type, string filter)
        {
            throw new NotImplementedException();
        }

        public UsedAuthorizationsDto GetUsedAuthorizations(long patientId, long appointmentId)
        {
            throw new NotImplementedException();
        }

        public string GetVersionOfServer()
        {
            throw new NotImplementedException();
        }

        public AppointmentResourcePatientsDto LoadLastTenPatients()
        {
            throw new NotImplementedException();
        }

        public AppointmentResourcePatientDto LoadPatientResources(AppointmentResourcePatientDto patient)
        {
            throw new NotImplementedException();
        }

        public AppointmentResourcePatientsDto LoadPatientsResources(AppointmentResourcePatientsDto patient)
        {
            throw new NotImplementedException();
        }

        public void NotifyPatient(long patientId, ContactPatientMethods method, string message)
        {
            throw new NotImplementedException();
        }

        public void RemovePatientAuthorization(long authId, string comment)
        {
            throw new NotImplementedException();
        }

        public void RemovePayer(long patientId, long payerId)
        {
            throw new NotImplementedException();
        }

        public void RemoveReservedAuthorization(long patientId, PatientAuthorizationDto authObj)
        {
            throw new NotImplementedException();
        }

        public AppointmentResourcePatientDto RequestNewPatient(long accountID, int? location)
        {
            throw new NotImplementedException();
        }

        public string RequestNewPayerExternalId(long accountID, int? location)
        {
            throw new NotImplementedException();
        }

        public string RequestNewReferringId(long accountID, int? location)
        {
            throw new NotImplementedException();
        }

        public PayerDto.PayerStatusDto RequestPayerStatus(PayerDto payer, string recordNum)
        {
            throw new NotImplementedException();
        }

        public string ResetPatientInsuranceFutureAppointments(long patIntId)
        {
            throw new NotImplementedException();
        }

        public PatientIdentifiersDto SaveMultipleIdentifiers(List<PatientIdentifierDto> multipleIdentifiers)
        {
            throw new NotImplementedException();
        }

        public bool SavePatientDemographics(AppointmentResourcePatientDto pat)
        {
            throw new NotImplementedException();
        }

        public SchedulerModalityVirtualRoomDto SaveSchedulerModalityVirtualRoom(SchedulerModalityVirtualRoomDto item)
        {
            throw new NotImplementedException();
        }

        public void UpdateGuarantor(PatientGuarantorDto guarantor)
        {
            throw new NotImplementedException();
        }

        public PatientAuthorizationsDto UpdatePatientAuthorization(PatientAuthorizationDto authObj)
        {
            throw new NotImplementedException();
        }

        public PatientContactDto UpdatePatientContact(PatientContactDto contact)
        {
            throw new NotImplementedException();
        }

        public PayersDto UpdatePatientPayers(AppointmentResourcePatientDto patientId, List<PayerDto> newPayers)
        {
            throw new NotImplementedException();
        }

        public PayerDto UpdatePayer(PayerDto updatedPayer)
        {
            throw new NotImplementedException();
        }

        public AppointmentResourceDto UpdateResource(long resourceId, AppointmentResourceDto updatedResource)
        {
            throw new NotImplementedException();
        }

        public PayerDto VerifyInsurance(PayerDto insurance, long patientId)
        {
            throw new NotImplementedException();
        }

        public string VerifyPayersBatch(Dictionary<long, int> patientsWithProviderDICNPI)
        {
            throw new NotImplementedException();
        }
    }
}
