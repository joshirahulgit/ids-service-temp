using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Business.Entity
{
    public class NotificationSlotDto : DtoBase
    {
        public int Id { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public DateTime? EndDate { get; set; }

        public int ModalityId { get; set; }

        public string Comment { get; set; }

        public string DayOfWeek { get; set; }

        public string Color { get; set; }

        public bool IsActive { get; set; }

        public List<string> AllowedCptCodes { get; set; }

        public string ModalityName { get; set; }

        public NotificationSlotDto Clone()
        {
            NotificationSlotDto newObject = new NotificationSlotDto();
            newObject.Id = this.Id;
            newObject.StartDate = this.StartDate;
            newObject.StartTime = this.StartTime;
            newObject.EndTime = this.EndTime;
            newObject.EndDate = this.EndDate;
            newObject.ModalityId = this.ModalityId;
            newObject.Comment = this.Comment;
            newObject.DayOfWeek = this.DayOfWeek;
            newObject.Color = this.Color;
            newObject.IsActive = this.IsActive;
            newObject.AllowedCptCodes = this.AllowedCptCodes;
            newObject.ModalityName = this.ModalityName;
            return newObject;
        }
    }


    public class NotificationSlotsDto : DtoBase
    {
        public NotificationSlotsDto()
        {
            Notifications = new List<NotificationSlotDto>();
        }

        public List<NotificationSlotDto> Notifications { get; set; }
    }


    public class NotificationSlotCptDto : DtoBase
    {
        public int Id { get; set; }

        public int NotificationSlotId { get; set; }

        public string CptCode { get; set; }

        public bool IsActive { get; set; }
    }

    public class NotificationSlotsCptDto : DtoBase
    {
        public NotificationSlotsCptDto()
        {
            Items = new List<NotificationSlotCptDto>();
        }

        public List<NotificationSlotCptDto> Items { get; set; }
    }
}
