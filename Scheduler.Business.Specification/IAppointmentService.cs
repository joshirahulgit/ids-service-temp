using Scheduler.Business.Entity;
using Scheduler.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Business.Specification
{
    public interface IAppointmentService : IContract
    {
        void AttachGuarantorsToApopintments(List<long> guarantorIds, List<long> apptIds);

        void DeleteReservations(List<AppointmentDto> appointments);

        String UpdateAppointmnetCPTCode(long appointmentId, AppointmentOrderItemType cptObject, String currentGlobalId, String newGlobalId);

        void RemoveOrdersFromAppoinment(long appointemntId, List<long> orderIds);

        AppointmentsDto FindAppointments(List<AppointmentResourceDto> requestResources, List<AppointmentStatusDto> statusFilter, Dictionary<string, string> sorter, AppointmentSearchModeType mode);

        AppointmentsDto FindAppointmentsEx(List<AppointmentResourceDto> requestResources, List<AppointmentStatusDto> statusFilter, Dictionary<string, string> sorter, AppointmentSearchModeType mode);

        AppointmentsDto FilterAppointments(AppointmentResourcePatientDto patient, ReferralDto referral, AppointmentResourcePhysicianDto dictator, DateTime startTime, DateTime endTime);

        AppointmentsDto FindPendingAppointments(List<long> referringIds, List<long> roomIds, string payerSearchPattern, List<string> authStatus, List<AppointmentResourceTimeDto> dateRange, List<string> reasons, Dictionary<string, string> sorter, int pageNumber, bool includeReferringRequest);

        AppointmentsDto FindPendingPatientAppointments(List<long> referringIds, List<long> roomIds, string payerSearchPattern, List<string> authStatus, List<AppointmentResourceTimeDto> dateRange, List<string> reasons, AppointmentResourcePatientDto patientParams, Dictionary<string, string> sorter, int pageNumber, bool includeReferringRequest);

        void ManageReferringPhysicianCreatedApp(int requestAppointmentId, int schedulerAppointmentId);

        void ManageRequestAppointmentsCPTs(int? requestAppointmentId, AppointmentDto appointment);

        AppointmentsDto FindAppointmentsBatch(List<AppointmentRequestDto> request);

        NotificationSlotsDto FindNotificationSlots(List<AppointmentRequestDto> request);

        AppointmentsDto FindFreeResources(List<AppointmentResourceDto> requestResources, Dictionary<string, string> sorter);

        void DeleteAppointments(List<long> appIds);

        AppointmentsDto CreateAppointments(AppointmentsDto appointments, bool ignoreWarning);

        AppointmentDto UpdateAppointment(AppointmentDto appointment);

        AppointmentsDto RescheduleAppointments(AppointmentsDto appointments, bool ignoreWarnings);

        AppointmentsDto UpdatePendingAppointments(AppointmentsDto appointments, bool ignoreWarnings);

        AppointmentsDto UpdateAppointmentStatuses(List<AppointmentDto> appointments, AppointmentStatusDto newStatus, bool ignoreWarnings);

        AppointmentsDto AttachVisitAndResourceToAppointments(AppointmentsDto appointments, AppointmentResourceDto resource, VisitDto visit, bool ignoreWarnings);

        AppointmentDto ReadAppointmentById(long appointmentId);

        RequestAppointmentDto ReadRequestAppointmentForId(long appointmentId);

        RequestAppointmentsDto ReadRequestAppointmentForIds(List<long> appointmentIds);

        bool UpdateRequestAppointment(AppointmentResourcePatientDto patient, RequestAppointmentDto reqAppt, PatientCommentDto comment, PatientAuthorizationDto authorizationWallet);

        AppointmentsDto LoadVisitByAppointment(AppointmentDto appointmentId, bool loadPatDetails);

        TaskTemplatesDto GetTaskTemplates(long appId);

        AppointmentCheckListValuesDto GetAppoinmentCheckListValues(long? appId);

        TasksDto GetAppoinmentCheckListValuesTasks(List<int> taskIds);

        AppointmentsDto LoadVisitsBatch(AppointmentsDto appointmentId);

        AppointmentsDto LoadAbbreviatedVisitByAppointment(AppointmentDto app);

        void UpdateAppointmentPendingReason(AppointmentsDto appointments);

        void LockAppointment(int appId);

        void UnLockAppointment(int appId);

        bool CheckAppointmentForLock(int appId);

        AppointmentsDto GetAppointmentDetailsByOrderId(string orderid);

        AppointmentsDto ReadVisitHistoryByAppointment(AppointmentDto appId, bool loadPatDetails);

        void AuditViewAppointment(AppointmentDto app);

        AppointmentsDto RescheduleVisitAppointments(AppointmentsDto appointments, bool ignoreWarnings);

        void UpdatePrepInstructions(List<int> appIds);

        void LinkTaskAndCheckListValue(int taskId, long valueId);

        bool SaveAppointmentCpts(long appId, ProceduresDto procs, DiagnosesDto diags);

        bool SaveAppointmentVisitInformation(AppointmentDto app);

        bool SaveAppointmentChecklist(long appId, List<AppointmentCheckListValueDto> checklist);

        AppointmentsDto FindAppointmentAutoCreateParams(AppointmentDto app);

        AppointmentsDto LoadProceduresFees(List<AppointmentDto> appointments);

        AppointmentInsuranceInfoDto FindAppointmentInsurances(int appointmentId);

        bool UpdateAppointmentReferrals(long apptId, List<ReferralDto> referrings);

        bool CreateGroupedExams(AppointmentsDto apps);

        bool DoUnGroupeExams(AppointmentsDto apps);

    }
}
