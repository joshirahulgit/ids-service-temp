using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.DBEntity
{
    public class NotificationSlot : EntityBase
    {
        public NotificationSlot()
        {
            AllowedCptCodes = new List<string>();
        }

        public new int Id { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime? EndDate { get; set; }
        public int ModalityId { get; set; }
        public string Comment { get; set; }
        public string DayOfWeek { get; set; }
        public bool IsActive { get; set; }

        public List<string> AllowedCptCodes { get; set; }

        public string Color { get; set; }

        //public static NotificationSlot ExtractFromDto(NotificationSlotDto s)
        //{
        //    NotificationSlot slot = new NotificationSlot();
        //    slot.EndDate = s.EndDate;
        //    slot.Color = s.Color;
        //    slot.Comment = s.Comment;
        //    slot.DayOfWeek = s.DayOfWeek;
        //    slot.EndTime = s.EndTime;
        //    slot.ModalityId = s.ModalityId;
        //    slot.StartDate = s.StartDate;
        //    slot.StartTime = s.StartTime;
        //    slot.Id = s.Id;
        //    slot.IsActive = s.IsActive;

        //    return slot;
        //}

        public void AttachAllowedCpt(string cptCode)
        {
            AllowedCptCodes.Add(cptCode);
        }
    }

    public class NotificationSlotCpt : EntityBase
    {
        public NotificationSlotCpt(int id, int notificationSlotId, string cptCode, bool isActive)
        {
            Id = id;
            NotificationSlotId = notificationSlotId;
            CptCode = cptCode;
            IsActive = isActive;
        }

        public NotificationSlotCpt()
        {
        }

        public int Id { get; set; }

        public int NotificationSlotId { get; set; }

        public string CptCode { get; set; }

        public bool IsActive { get; set; }

        //public static NotificationSlotCpt ExtractFromDto(NotificationSlotCptDto dto)
        //{
        //    NotificationSlotCpt s = new NotificationSlotCpt();
        //    s.Id = dto.Id;
        //    s.NotificationSlotId = dto.NotificationSlotId;
        //    s.CptCode = dto.CptCode;
        //    s.IsActive = dto.IsActive;

        //    return s;
        //}
    }
}
