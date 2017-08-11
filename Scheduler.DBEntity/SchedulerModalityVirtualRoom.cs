using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.DBEntity
{
    public class SchedulerModalityVirtualRoom : EntityBase
    {

        public string Name { set; get; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public int AllowedExamCount { get; set; }

        public int AllowedExamCountMon { get; set; }

        public int AllowedExamCountTue { get; set; }

        public int AllowedExamCountWed { get; set; }

        public int AllowedExamCountThu { get; set; }

        public int AllowedExamCountFri { get; set; }

        public int AllowedExamCountSat { get; set; }

        public int AllowedExamCountSun { get; set; }

        public bool IsLinked { get; set; } = true;//Default is true, inorder to prevent any non required delete.

        public SchedulerModalityVirtualRoom(long id)
        {
            this.Id = id;
        }

        public void SetId(long id)
        {
            this.Id = id;
        }
    }

    public static class SchedulerModalityVirtualRoomExt
    {
        //public static SchedulerModalityVirtualRoom ToSchedulerModalityVirtualRoom(this SchedulerModalityVirtualRoomDto dto)
        //{
        //    if (dto == null)
        //        return null;

        //    var entity = new SchedulerModalityVirtualRoom(dto.Id);
        //    entity._name = dto.Name;
        //    entity._description = dto.Description;
        //    entity._isActive = dto.IsActive;
        //    entity._allowedExamCount = dto.AllowedExamCount;
        //    entity._allowedExamCountSun = dto.AllowedExamCountSun;
        //    entity._allowedExamCountMon = dto.AllowedExamCountMon;
        //    entity._allowedExamCountTue = dto.AllowedExamCountTue;
        //    entity._allowedExamCountWed = dto.AllowedExamCountWed;
        //    entity._allowedExamCountThu = dto.AllowedExamCountThu;
        //    entity._allowedExamCountFri = dto.AllowedExamCountFri;
        //    entity._allowedExamCountSat = dto.AllowedExamCountSat;
        //    entity._isLinked = dto.IsLinked;
        //    return entity;
        //}

        //public static SchedulerModalityVirtualRoomDto ToSchedulerModalityVirtualRoomDto(this SchedulerModalityVirtualRoom smvr)
        //{
        //    if (smvr == null)
        //        return null;

        //    var dto = new SchedulerModalityVirtualRoomDto();
        //    dto.Id = smvr.Id;
        //    dto.Name = smvr.Name;
        //    dto.Description = smvr.Description;
        //    dto.IsActive = smvr.IsActive;
        //    dto.AllowedExamCount = smvr.AllowedExamCount;
        //    dto.AllowedExamCountSun = smvr.AllowedExamCountSun;
        //    dto.AllowedExamCountMon = smvr.AllowedExamCountMon;
        //    dto.AllowedExamCountTue = smvr.AllowedExamCountTue;
        //    dto.AllowedExamCountWed = smvr.AllowedExamCountWed;
        //    dto.AllowedExamCountThu = smvr.AllowedExamCountThu;
        //    dto.AllowedExamCountFri = smvr.AllowedExamCountFri;
        //    dto.AllowedExamCountSat = smvr.AllowedExamCountSat;
        //    dto.IsLinked = smvr.IsLikned;
        //    return dto;
        //}
    }
}
