using Scheduler.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.DBEntity
{
    public class AuditEntry : EntityBase
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

        #region Construction

        public AuditEntry()
        {
        }

        private AuditEntry(long appointmentId, String location,
                    String auditMgs, String printer,
                    String destination, long? userActivityId)
            : this()
        {
            this.AppointmentId = appointmentId;
            this.Location = location;
            this.UserId = GlobalContext.RequestContext.UserName;
            this.AuditMsg = auditMgs;
            this.UserName = GlobalContext.RequestContext.UserName;
            this.Printer = printer;
            this.Date = DateTime.Now;
            this.ComputerName = GlobalContext.RequestContext.UserComputerIP;
            this.Destination = destination;
            this.UserActivityId = userActivityId;
        }

        private AuditEntry(int? appointmentId, String location,
                    String auditMgs, String printer,
                    String destination, long? userActivityId) : this()
        {
            this.AppointmentId = appointmentId;
            this.Location = location;
            this.UserId = GlobalContext.RequestContext.UserName;
            this.AuditMsg = auditMgs;
            this.UserName = GlobalContext.RequestContext.UserName;
            this.Printer = printer;
            this.Date = DateTime.Now;
            this.ComputerName = GlobalContext.RequestContext.UserComputerIP;
            this.Destination = destination;
            this.UserActivityId = userActivityId;
        }

        public AuditEntry(int auditEntryId, AuditEntry entry) : this()
        {
            Id = auditEntryId;
            this.AppointmentId = entry.AppointmentId;
            this.Location = entry.Location;
            this.UserId = entry.UserId;
            this.AuditMsg = entry.AuditMsg;
            this.UserName = entry.UserName;
            this.Printer = entry.Printer;
            this.Date = entry.Date;
            this.ComputerName = entry.ComputerName;
            this.Destination = entry.Destination;
            this.UserActivityId = entry.UserActivityId;
        }

        public AuditEntry(int? appointmentId, String auditMsg)
            : this(appointmentId, null, auditMsg, null, null, null)
        {
        }

        public AuditEntry(long? appointmentId, string auditMsg, string entityId,
            String entityName, String actionType)
            : this()
        {
            AppointmentId = appointmentId;
            EntityId = entityId;
            EntityName = entityName;
            ActionType = actionType;
            AuditMsg = auditMsg;
            UserId = GlobalContext.RequestContext.UserName;
            UserName = GlobalContext.RequestContext.UserName;
            //            Date = DateTime.Now;
            ComputerName = GlobalContext.RequestContext.UserComputerIP;
        }

        #endregion

        public AuditEntry BusinessClone()
        {
            return new AuditEntry();
        }

        //public static AuditEntry CreateAuditEntryItem(AppointmentDto a, string formatString, params object[] args)
        //{
        //    return CreateAuditEntryItem((int?)a.AppointmentID, formatString, args);
        //}
        //public static AuditEntry CreateAuditEntryItem(AppointmentDto a, string entityId, string actionType, string entityName, string formatString, params object[] args)
        //{
        //    AuditEntry auditEntry = CreateAuditEntryItem(a == null ? null : (int?)a.AppointmentID, formatString, args);
        //    auditEntry.EntityId = entityId;
        //    auditEntry.EntityName = entityName;
        //    auditEntry.ActionType = actionType;
        //    return auditEntry;
        //}

        public static AuditEntry CreateAuditEntryItem(int? aId, string formatString, params object[] args)
        {
            try
            {
                string auditMsg = string.Format(formatString, args);
                return new AuditEntry(aId, auditMsg);
            }
            catch
            {
                return null;
            }
        }
    }
}
