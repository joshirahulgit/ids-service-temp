using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.DBEntity
{
    public class AppointmentStatus : EntityBase
    {
        public AppointmentStatus()
        {
            AllowedTransition = new List<AppointmentStatusTransition>();
        }

        public AppointmentStatus(long ID)
            : this()
        {
            AllowedTransition = new List<AppointmentStatusTransition>();
            base.Id = ID;
        }

        public AppointmentStatus(long ID, string statusName, string applied, bool isVisible, int sortIndex, bool isSystemStatus, string color)
            : this(ID)
        {
            this.StatusName = statusName;
            this.AppliedStatusName = applied;
            this.IsVisible = isVisible;
            this.IsSystemStatus = isSystemStatus;
            this.SortIndex = sortIndex;
            this.Color = color;
        }

        public List<AppointmentStatusTransition> AllowedTransition { get; set; }

        public virtual String StatusName { get; set; }
        public virtual String AppliedStatusName { get; set; }
        public virtual String Color { get; set; }
        public virtual bool IsVisible { get; set; }
        public virtual bool IsSystemStatus { get; set; }
        public virtual bool IsDeleted { get; set; }
        public virtual int SortIndex { get; set; }

        //internal static AppointmentStatus ExtractFromDto(Common.DataTransferObjects.Appointment.AppointmentStatusDto s)
        //{
        //    AppointmentStatus result = new AppointmentStatus();
        //    result.Id = s.StatusID;
        //    result.StatusName = s.StatusName;
        //    result.Color = s.Color;
        //    result.AppliedStatusName = s.AppliedStatusName;
        //    result.IsDeleted = s.IsDeleted;
        //    result.IsVisible = s.IsVisible;
        //    result.IsSystemStatus = s.IsSystemStatus;
        //    result.SortIndex = s.SortIndex;
        //    foreach (AppointmentStatusTransitionDto dto in s.AllowedTransition)
        //        result.AllowedTransition.Add(AppointmentStatusTransition.Extract2Dto(dto));

        //    //            result.AllowedTransition.AddRange(s.AllowedTransition);
        //    return result;
        //}

        //        public static List<AppointmentStatus> GetList()
        //        {
        //            List<AppointmentStatus> statuses = new List<AppointmentStatus>();
        //            statuses.Add(new AppointmentStatus((long)AppointmentStatuses.New, "New", "New"));
        //            statuses.Add(new AppointmentStatus((long)AppointmentStatuses.Arrived, "Arrive", "Arrived"));
        //            statuses.Add(new AppointmentStatus((long)AppointmentStatuses.Complete, "Complete", "Completed"));
        //            statuses.Add(new AppointmentStatus((long)AppointmentStatuses.Cancel, "Cancel", "Cancelled"));
        //            statuses.Add(new AppointmentStatus((long)AppointmentStatuses.NoShow, "No Show", "No Show"));
        //            statuses.Add(new AppointmentStatus((long)AppointmentStatuses.Blocked, "Block", "Blocked"));
        //            statuses.Add(new AppointmentStatus((long)AppointmentStatuses.Available, "Available", "Available"));
        //            statuses.Add(new AppointmentStatus((long)AppointmentStatuses.Rescheduled, "Reschedule", "Rescheduled"));
        //            statuses.Add(new AppointmentStatus((long)AppointmentStatuses.TechComplete, "Tech complete", "Tech completed"));
        //            return statuses;
        //        }

        

        //public static AppointmentStatusDto Convert2Dto(AppointmentStatus s)
        //{
        //    AppointmentStatusDto result = new AppointmentStatusDto();
        //    result.StatusID = s.Id;
        //    result.StatusName = s.StatusName;
        //    result.Color = s.Color;
        //    result.AppliedStatusName = s.AppliedStatusName;
        //    result.IsVisible = s.IsVisible;
        //    result.IsDeleted = s.IsDeleted;
        //    result.IsSystemStatus = s.IsSystemStatus;
        //    result.SortIndex = s.SortIndex;
        //    foreach (AppointmentStatusTransition transition in s.AllowedTransition)
        //        result.AllowedTransition.Add(AppointmentStatusTransition.Convert2Dto(transition));
        //    //            result.AllowedTransition.AddRange(s.AllowedTransition);
        //    return result;
        //}
    }


    public class AppointmentStatusTransition
    {
        public AppointmentStatusTransition(int statusId, bool patientViewSpecific)
        {
            StatusId = statusId;
            PatientViewSpecific = patientViewSpecific;
        }

        //public static AppointmentStatusTransition Extract2Dto(AppointmentStatusTransitionDto tr)
        //{
        //    return new AppointmentStatusTransition(tr.StatusId, tr.PatientViewSpecific);
        //}

        //public static AppointmentStatusTransitionDto Convert2Dto(AppointmentStatusTransition tr)
        //{
        //    return new AppointmentStatusTransitionDto(tr.StatusId, tr.PatientViewSpecific);
        //}

        public int StatusId { get; private set; }
        public bool PatientViewSpecific { get; private set; }
    }
}
