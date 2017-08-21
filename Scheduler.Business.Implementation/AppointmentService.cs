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
    public class AppointmentService : ServiceBase, IAppointmentService
    {
        public void AttachGuarantorsToApopintments(List<long> guarantorIds, List<long> apptIds)
        {
            throw new NotImplementedException();
        }

        public AppointmentsDto AttachVisitAndResourceToAppointments(AppointmentsDto appointments, AppointmentResourceDto resource, VisitDto visit, bool ignoreWarnings)
        {
            throw new NotImplementedException();
        }

        public void AuditViewAppointment(AppointmentDto app)
        {
            throw new NotImplementedException();
        }

        public bool CheckAppointmentForLock(int appId)
        {
            throw new NotImplementedException();
        }

        public AppointmentsDto CreateAppointments(AppointmentsDto appointments, bool ignoreWarning)
        {
            throw new NotImplementedException();
        }

        public bool CreateGroupedExams(AppointmentsDto apps)
        {
            throw new NotImplementedException();
        }

        public void DeleteAppointments(List<long> appIds)
        {
            throw new NotImplementedException();
        }

        public void DeleteReservations(List<AppointmentDto> appointments)
        {
            throw new NotImplementedException();
        }

        public bool DoUnGroupeExams(AppointmentsDto apps)
        {
            throw new NotImplementedException();
        }

        public AppointmentsDto FilterAppointments(AppointmentResourcePatientDto patient, ReferralDto referral, AppointmentResourcePhysicianDto dictator, DateTime startTime, DateTime endTime)
        {
            throw new NotImplementedException();
        }

        public AppointmentsDto FindAppointmentAutoCreateParams(AppointmentDto app)
        {
            throw new NotImplementedException();
        }

        public AppointmentInsuranceInfoDto FindAppointmentInsurances(int appointmentId)
        {
            throw new NotImplementedException();
        }

        public AppointmentsDto FindAppointments(List<AppointmentResourceDto> requestResources, List<AppointmentStatusDto> statusFilter, Dictionary<string, string> sorter, AppointmentSearchModeType mode)
        {
            throw new NotImplementedException();
        }

        public AppointmentsDto FindAppointmentsBatch(List<AppointmentRequestDto> request)
        {
            throw new NotImplementedException();
        }

        public AppointmentsDto FindAppointmentsEx(List<AppointmentResourceDto> requestResources, List<AppointmentStatusDto> statusFilter, Dictionary<string, string> sorter, AppointmentSearchModeType mode)
        {
            throw new NotImplementedException();
        }

        public AppointmentsDto FindFreeResources(List<AppointmentResourceDto> requestResources, Dictionary<string, string> sorter)
        {
            throw new NotImplementedException();
        }

        public NotificationSlotsDto FindNotificationSlots(List<AppointmentRequestDto> request)
        {
            throw new NotImplementedException();
        }

        public AppointmentsDto FindPendingAppointments(List<long> referringIds, List<long> roomIds, string payerSearchPattern, List<string> authStatus, List<AppointmentResourceTimeDto> dateRange, List<string> reasons, Dictionary<string, string> sorter, int pageNumber, bool includeReferringRequest)
        {
            throw new NotImplementedException();
        }

        public AppointmentsDto FindPendingPatientAppointments(List<long> referringIds, List<long> roomIds, string payerSearchPattern, List<string> authStatus, List<AppointmentResourceTimeDto> dateRange, List<string> reasons, AppointmentResourcePatientDto patientParams, Dictionary<string, string> sorter, int pageNumber, bool includeReferringRequest)
        {
            throw new NotImplementedException();
        }

        public AppointmentCheckListValuesDto GetAppoinmentCheckListValues(long? appId)
        {
            throw new NotImplementedException();
        }

        public TasksDto GetAppoinmentCheckListValuesTasks(List<int> taskIds)
        {
            throw new NotImplementedException();
        }

        public AppointmentsDto GetAppointmentDetailsByOrderId(string orderid)
        {
            throw new NotImplementedException();
        }

        public TaskTemplatesDto GetTaskTemplates(long appId)
        {
            throw new NotImplementedException();
        }

        public void LinkTaskAndCheckListValue(int taskId, long valueId)
        {
            throw new NotImplementedException();
        }

        public AppointmentsDto LoadAbbreviatedVisitByAppointment(AppointmentDto app)
        {
            throw new NotImplementedException();
        }

        public AppointmentsDto LoadProceduresFees(List<AppointmentDto> appointments)
        {
            throw new NotImplementedException();
        }

        public AppointmentsDto LoadVisitByAppointment(AppointmentDto appointmentId, bool loadPatDetails)
        {
            throw new NotImplementedException();
        }

        public AppointmentsDto LoadVisitsBatch(AppointmentsDto appointmentId)
        {
            throw new NotImplementedException();
        }

        public void LockAppointment(int appId)
        {
            throw new NotImplementedException();
        }

        public void ManageReferringPhysicianCreatedApp(int requestAppointmentId, int schedulerAppointmentId)
        {
            throw new NotImplementedException();
        }

        public void ManageRequestAppointmentsCPTs(int? requestAppointmentId, AppointmentDto appointment)
        {
            throw new NotImplementedException();
        }

        public AppointmentDto ReadAppointmentById(long appointmentId)
        {
            throw new NotImplementedException();
        }

        public RequestAppointmentDto ReadRequestAppointmentForId(long appointmentId)
        {
            throw new NotImplementedException();
        }

        public RequestAppointmentsDto ReadRequestAppointmentForIds(List<long> appointmentIds)
        {
            throw new NotImplementedException();
        }

        public AppointmentsDto ReadVisitHistoryByAppointment(AppointmentDto appId, bool loadPatDetails)
        {
            throw new NotImplementedException();
        }

        public void RemoveOrdersFromAppoinment(long appointemntId, List<long> orderIds)
        {
            throw new NotImplementedException();
        }

        public AppointmentsDto RescheduleAppointments(AppointmentsDto appointments, bool ignoreWarnings)
        {
            throw new NotImplementedException();
        }

        public AppointmentsDto RescheduleVisitAppointments(AppointmentsDto appointments, bool ignoreWarnings)
        {
            throw new NotImplementedException();
        }

        public bool SaveAppointmentChecklist(long appId, List<AppointmentCheckListValueDto> checklist)
        {
            throw new NotImplementedException();
        }

        public bool SaveAppointmentCpts(long appId, ProceduresDto procs, DiagnosesDto diags)
        {
            throw new NotImplementedException();
        }

        public bool SaveAppointmentVisitInformation(AppointmentDto app)
        {
            throw new NotImplementedException();
        }

        public void UnLockAppointment(int appId)
        {
            throw new NotImplementedException();
        }

        public AppointmentDto UpdateAppointment(AppointmentDto appointment)
        {
            throw new NotImplementedException();
        }

        public void UpdateAppointmentPendingReason(AppointmentsDto appointments)
        {
            throw new NotImplementedException();
        }

        public bool UpdateAppointmentReferrals(long apptId, List<ReferralDto> referrings)
        {
            throw new NotImplementedException();
        }

        public AppointmentsDto UpdateAppointmentStatuses(List<AppointmentDto> appointments, AppointmentStatusDto newStatus, bool ignoreWarnings)
        {
            throw new NotImplementedException();
        }

        public string UpdateAppointmnetCPTCode(long appointmentId, AppointmentOrderItemType cptObject, string currentGlobalId, string newGlobalId)
        {
            throw new NotImplementedException();
        }

        public AppointmentsDto UpdatePendingAppointments(AppointmentsDto appointments, bool ignoreWarnings)
        {
            throw new NotImplementedException();
        }

        public void UpdatePrepInstructions(List<int> appIds)
        {
            throw new NotImplementedException();
        }

        public bool UpdateRequestAppointment(AppointmentResourcePatientDto patient, RequestAppointmentDto reqAppt, PatientCommentDto comment, PatientAuthorizationDto authorizationWallet)
        {
            throw new NotImplementedException();
        }
    }
}
