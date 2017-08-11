using Scheduler.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.DBEntity
{
    public class PatientComment : EntityBase
    {
        public CommentType Type { get; set; }
        public String Text { get; set; }
        public DateTime Time { get; set; }
        public String Creator { get; set; }
        public String TechnicianName { get; set; }
        public String LocationTransferredTo { get; set; }
        public bool IsAlert { get; set; }
        public long? CommentedEntityId { get; set; }
        public CommentedEntityTypes CommentedEntityType { get; set; }
        public int? AccountEnumId { get; set; }

        public void SetCommentDetails(string comment, DateTime time, string creator, CommentedEntityTypes entityType,
            CommentType commentType, int? accountEnumId = null)
        {
            Text = comment;
            Time = time;
            Creator = creator;
            CommentedEntityType = entityType;
            Type = commentType;
            AccountEnumId = accountEnumId;
        }

        //internal static PatientComment ExtractFromDto(PatientCommentDto c)
        //{
        //    PatientComment result = new PatientComment();
        //    result.Creator = c.Creator;
        //    result.Id = c.CommentID;
        //    result.Text = c.Text;
        //    result.Time = c.Time;
        //    result.Type = CommentType.ExtractFromDto(c.Type);
        //    result.CommentedEntityId = c.CommentedEntityId;
        //    result.CommentedEntityType = c.CommentedEntityType;
        //    result.TechnicianName = c.TechnicianName;
        //    result.LocationTransferredTo = c.LocationTransferredTo;
        //    result.IsAlert = c.IsAlert;
        //    result.AccountEnumId = c.AccountEnumId;
        //    return result;
        //}

        //public static PatientCommentDto Convert2Dto(PatientComment comment)
        //{
        //    PatientCommentDto dto = new PatientCommentDto();

        //    dto.CommentID = comment.Id;
        //    dto.Creator = comment.Creator;
        //    dto.Text = comment.Text;
        //    dto.LocationTransferredTo = comment.LocationTransferredTo;
        //    dto.Time = comment.Time;
        //    dto.Type = new CommentTypeDto();
        //    dto.Type.TypeId = comment.Type.Id;
        //    dto.Type.TypeName = comment.Type.Name;
        //    dto.CommentedEntityId = comment.CommentedEntityId;
        //    dto.CommentedEntityType = comment.CommentedEntityType;
        //    dto.IsAlert = comment.IsAlert;
        //    dto.AccountEnumId = comment.AccountEnumId;
        //    return dto;
        //}
    }
}
