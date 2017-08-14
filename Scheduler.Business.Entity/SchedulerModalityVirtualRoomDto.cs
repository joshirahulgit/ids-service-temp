using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Business.Entity
{
    public class SchedulerModalityVirtualRoomDto : DtoBase
    {
        public long Id { get; set; }
        public string Name { get; set; }
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

        public bool IsLinked { get; set; }

        public int GetExamCountForDay(DayOfWeek day)
        {
            int res = int.MaxValue;

            switch (day)
            {
                case DayOfWeek.Sunday:
                    res = this.AllowedExamCountSun;
                    break;
                case DayOfWeek.Monday:
                    res = this.AllowedExamCountMon;
                    break;
                case DayOfWeek.Tuesday:
                    res = this.AllowedExamCountTue;
                    break;
                case DayOfWeek.Wednesday:
                    res = this.AllowedExamCountWed;
                    break;
                case DayOfWeek.Thursday:
                    res = this.AllowedExamCountThu;
                    break;
                case DayOfWeek.Friday:
                    res = this.AllowedExamCountFri;
                    break;
                case DayOfWeek.Saturday:
                    res = this.AllowedExamCountSat;
                    break;
                default:
                    res = this.AllowedExamCount;
                    break;
            }

            //When day count is less than one apply default count.
            if (res < 1)
                res = this.AllowedExamCount;

            //When default count is also less than, it should discard the virtual room check. So set int max value. 
            if (res < 1)
                res = int.MaxValue;

            return res;
        }

        public override bool Equals(object obj)
        {
            bool res = false;

            if (obj != null && obj is SchedulerModalityVirtualRoomDto)
                res = (obj as SchedulerModalityVirtualRoomDto).Id == this.Id;

            return res;
        }
    }

    public class SchedulerModalityVirtualRoomsDto : DtoBase
    {
        public IList<SchedulerModalityVirtualRoomDto> Items { get; set; }

        public SchedulerModalityVirtualRoomsDto(List<SchedulerModalityVirtualRoomDto> items)
        {
            this.Items = items;
        }
    }
}
