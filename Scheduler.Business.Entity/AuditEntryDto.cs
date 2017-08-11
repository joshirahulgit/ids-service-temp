using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Business.Entity
{
    public class AuditEntryDto //: DtoBase
    {
        public long? AppointmentId { get; set; }
        public String Location { get; set; }
        public String UserId { get; set; }
        public String AuditMsg { get; set; }
        public String UserName { get; set; }
        public String Printer { get; set; }
        public DateTime? Date { get; set; }
        public String ComputerName { get; set; }
        public String Destination { get; set; }
        public long? UserActivityId { get; set; }
        public String EntityId { get; set; }
        public String EntityName { get; set; }
        public String ActionType { get; set; }

        public String ShortDateShortTimeStr
        {
            get { return Date.HasValue ? Date.Value.ToShortDateString() + " " + Date.Value.ToLongTimeString() : String.Empty; }
        }
    }

    public class AuditEntriesDto //: DtoBase
    {
        public List<AuditEntryDto> Entries { get; set; }

        public AuditEntriesDto()
        {
            Entries = new List<AuditEntryDto>();
        }

        public AuditEntriesDto(List<AuditEntryDto> entries)
        {
            Entries = entries;
        }
    }
}
