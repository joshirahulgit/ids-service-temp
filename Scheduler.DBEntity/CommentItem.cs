using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.DBEntity
{
    public class CommentItem : EntityBase
    {
        public CommentType Type { get; set; }
        public String Text { get; set; }
        public DateTime Time { get; set; }
        public String Creator { get; set; }


        //public static CommentItemDto Convert2Dto(CommentItem comment)
        //{
        //    CommentItemDto dto = new CommentItemDto();

        //    dto.Id = comment.Id;
        //    dto.Creator = comment.Creator;
        //    dto.Text = comment.Text;
        //    dto.Time = comment.Time;
        //    dto.Type = new Common.DataTransferObjects.Types.CommentTypeDto();
        //    if (comment.Type != null)
        //    {
        //        dto.Type.TypeId = comment.Type.Id;
        //        dto.Type.TypeName = comment.Type.Name;
        //    }
        //    return dto;
        //}
    }
}
