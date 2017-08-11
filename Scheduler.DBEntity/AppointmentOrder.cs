using Scheduler.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.DBEntity
{
    public class AppointmentOrder : EntityBase
    {
        public string JobID { get; set; }
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

        //public void Remove(RepositoryLocator locator)
        //{
        //    locator.AccountRepository.RemoveOrder(this.Id);
        //}

        //public void UpdateAppointmentItemLinking(RepositoryLocator locator, String newAppointmentItemId, long? newAppointmentItemType)
        //{
        //    this.AppointmentItemId = newAppointmentItemId;
        //    this.AppointmentItemType = newAppointmentItemType;
        //    locator.AccountRepository.UpdateOrderLinkedItem(this.Id, this.AppointmentItemType, this.AppointmentItemId);
        //}

        //public static AppointmentOrder ExtractFromDto(AppointmentOrderDto dto)
        //{
        //    AppointmentOrder result = new AppointmentOrder();
        //    result.AccountName = dto.AccountName;
        //    result.AppointmentId = dto.AppointmentId;
        //    result.AppointmentItemId = dto.AppointmentItemId;
        //    result.AppointmentItemType = dto.AppointmentItemType;
        //    result.CC = dto.CC;
        //    result.CPTCode = dto.CPTCode;
        //    result.Dictator = dto.Dictator;
        //    result.DOS = dto.DOS;
        //    result.ExamDescription = dto.ExamDescription;
        //    result.Id = dto.ID;
        //    result.Location = dto.Location;
        //    result.Modality = dto.Modality;
        //    result.OrderId = dto.OrderId;
        //    result.PatientId = dto.PatientId;
        //    result.PhysicianId = dto.PhysicianId;
        //    result.Priority = dto.Priority;
        //    result.Reason = dto.Reason;
        //    result.RecurringSeriesID = dto.RecurringSeriesID;
        //    result.WorktypeDescription = dto.WorktypeDescription;
        //    return result;
        //}

        

        //public static AppointmentOrderDto Convert2Dto(AppointmentOrder order)
        //{
        //    AppointmentOrderDto result = new AppointmentOrderDto();
        //    result.ID = order.Id;
        //    result.AccountName = order.AccountName;
        //    result.AppointmentId = order.AppointmentId;
        //    result.AppointmentItemId = order.AppointmentItemId;
        //    result.AppointmentItemType = order.AppointmentItemType;
        //    result.CC = order.CC;
        //    result.CPTCode = order.CPTCode;
        //    result.Dictator = order.Dictator;
        //    result.DOS = order.DOS;
        //    result.ExamDescription = order.ExamDescription;
        //    result.Location = order.Location;
        //    result.Modality = order.Modality;
        //    result.MultipleOrderId = order.MultipleOrderId;
        //    result.OrderId = order.OrderId;
        //    result.PatientId = order.PatientId;
        //    result.PhysicianId = order.PhysicianId;
        //    result.Priority = order.Priority;
        //    result.Reason = order.Reason;
        //    result.RecurringSeriesID = order.RecurringSeriesID;
        //    result.WorktypeDescription = order.WorktypeDescription;
        //    result.JobID = order.JobID;
        //    return result;
        //}

        public void MapToAppointment(Appointment appointment)
        {
            this.AppointmentItemType = null;
            this.AppointmentItemId = string.Empty;
            this.AppointmentId = appointment.Id;
        }

        public void MapToProcedure(long appointmentId, Procedure procedure)
        {
            this.AppointmentItemType = (long)AppointmentOrderItemType.Procedure;
            this.AppointmentItemId = procedure.GlobalId;
            this.AppointmentId = appointmentId;
        }

        public void MapToRoom(long appointmentId, AppointmentResourceModality room)
        {
            this.AppointmentItemType = (long)AppointmentOrderItemType.Room;
            this.AppointmentItemId = room.Id.ToString();
            this.AppointmentId = appointmentId;
        }
    }
}
