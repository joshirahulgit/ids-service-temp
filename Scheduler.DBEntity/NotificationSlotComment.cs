using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.DBEntity
{
    public class NotificationSlotComment : EntityBase
    {
        public new int Id { get; set; }
        public string Comment { get; set; }
        public string Color { get; set; }
        public bool IsActive { get; set; }

        //public static NotificationSlotCommentDto ToDto(this NotificationSlotComment entity)
        //{
        //    NotificationSlotCommentDto dto = new NotificationSlotCommentDto();
        //    dto.Id = entity.Id;
        //    dto.Comment = entity.Comment;
        //    dto.Color = entity.Color;
        //    dto.IsActive = entity.IsActive;
        //    return dto;
        //}
    }
}
