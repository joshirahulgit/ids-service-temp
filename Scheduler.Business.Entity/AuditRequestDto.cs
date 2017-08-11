using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Business.Entity
{
    public class AuditRequestDto : DtoBase
    {
        public AuditRequestDto()
        {
            StartTime = new DateTime();
            EndTime = new DateTime();
        }

        public AuditRequestDto(int? appointmentId, int patientId)
            : this()
        {
            AppointmentId = appointmentId;
            PatientId = patientId;
        }

        public long EntityId { get; set; }

        public string EntityName { get; set; }

        public string ActionType { get; set; }

        public string Message { get; set; }

        public string User { get; set; }

        public string SortOrder { get; set; }

        public string SortKey { get; set; }

        public DateTime? EndTime { get; set; }

        public DateTime? StartTime { get; set; }

        public int? AppointmentId { get; set; }

        public int PatientId { get; set; }
    }
}
