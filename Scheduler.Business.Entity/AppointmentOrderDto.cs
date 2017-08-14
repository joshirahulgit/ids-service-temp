using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Business.Entity
{
    public class AppointmentOrderDto : DtoBase
    {
        public string JobID { get; set; }
        public long ID { get; set; }
        public long AppointmentId { get; set; }
        public long? AppointmentItemType { get; set; }
        public String AppointmentItemId { get; set; }
        public String PatientId { get; set; }
        public String Location { get; set; }
        public String CPTCode { get; set; }
        public String WorktypeDescription { get; set; }
        public String ExamDescription { get; set; }
        public String PhysicianId { get; set; }
        public String Reason { get; set; }
        public String Dictator { get; set; }
        public String Priority { get; set; }
        public String MultipleOrderId { get; set; }
        public String OrderId { get; set; }
        public DateTime? DOS { get; set; }
        public String AccountName { get; set; }
        public String RecurringSeriesID { get; set; }
        public String CC { get; set; }
        public String Modality { get; set; }


        public AppointmentOrderDto()
        {

        }

        public AppointmentOrderDto(AppointmentOrderDto order)
        {
            this.ID = order.ID;
            this.AccountName = order.AccountName;
            this.AppointmentId = order.AppointmentId;
            this.AppointmentItemId = order.AppointmentItemId;
            this.AppointmentItemType = order.AppointmentItemType;
            this.CC = order.CC;
            this.CPTCode = order.CPTCode;
            this.Dictator = order.Dictator;
            this.DOS = order.DOS;
            this.ExamDescription = order.ExamDescription;
            this.Location = order.Location;
            this.Modality = order.Modality;
            this.MultipleOrderId = order.MultipleOrderId;
            this.OrderId = order.OrderId;
            this.PatientId = order.PatientId;
            this.PhysicianId = order.PhysicianId;
            this.Priority = order.Priority;
            this.Reason = order.Reason;
            this.RecurringSeriesID = order.RecurringSeriesID;
            this.WorktypeDescription = order.WorktypeDescription;
        }
    }
}
