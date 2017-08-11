using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Business.Entity
{
    public class NotificationSlotCommentDto
    {
        public int Id { get; set; }

        public string Comment { get; set; }

        public string Color { get; set; }

        public bool IsActive { get; set; }
    }

    public class NotificationSlotCommentsDto : DtoBase
    {
        public NotificationSlotCommentsDto()
        {
            this.NotificationSlotComments = new List<NotificationSlotCommentDto>();
        }

        public List<NotificationSlotCommentDto> NotificationSlotComments { get; set; }
    }
}
