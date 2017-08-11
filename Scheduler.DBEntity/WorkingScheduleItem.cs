using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.DBEntity
{
    public class WorkingScheduleItem : EntityBase
    {

        public WorkingScheduleItem()
        { }

        public WorkingScheduleItem(DateTime start, DateTime end)
            : this()
        {
            StartTime = start;
            EndTime = end;
        }

        public long ModalityId { get; set; }
        public String WeekDay { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime? BreakFrom { get; set; }
        public DateTime? BreakTo { get; set; }
        public bool IsActive { get; set; }

        //public static WorkingScheduleItem ExtractFromDto(WorkingScheduleItemDto op)
        //{
        //    WorkingScheduleItem result = new WorkingScheduleItem();
        //    result.Id = op.Id;
        //    result.ModalityId = op.ModalityId;
        //    result.WeekDay = op.WeekDay;
        //    result.StartTime = op.StartTime;
        //    result.EndTime = op.EndTime;
        //    result.BreakFrom = op.BreakFrom;
        //    result.BreakTo = op.BreakTo;
        //    result.IsActive = op.IsActive;
        //    return result;
        //}

        //public static WorkingScheduleItemDto Convert2Dto(WorkingScheduleItem p)
        //{
        //    WorkingScheduleItemDto result = new WorkingScheduleItemDto();
        //    result.Id = p.Id;
        //    result.ModalityId = p.ModalityId;
        //    result.WeekDay = p.WeekDay;
        //    result.StartTime = p.StartTime;
        //    result.EndTime = p.EndTime;
        //    result.BreakFrom = p.BreakFrom;
        //    result.BreakTo = p.BreakTo;
        //    result.IsActive = p.IsActive;
        //    return result;
        //}
    }

    public class WorkingSchedule : EntityBase
    {
        public List<WorkingScheduleItem> Items { get; private set; }
        public List<Holiday> Holidays { get; private set; }

        public WorkingSchedule()
        {
            Items = new List<WorkingScheduleItem>();
            Holidays = new List<Holiday>();
        }

        public WorkingSchedule(List<WorkingScheduleItem> list)
        {
            Items = list;
        }

        //public static WorkingScheduleDto Convert2Dto(WorkingSchedule p)
        //{
        //    WorkingScheduleDto result = new WorkingScheduleDto();
        //    foreach (WorkingScheduleItem item in p.Items)
        //    {
        //        result.Items.Add(WorkingScheduleItem.Convert2Dto(item));
        //    }
        //    foreach (Holiday item in p.Holidays)
        //    {
        //        result.Holidays.Add(Holiday.Convert2Dto(item));
        //    }

        //    return result;
        //}

        //public static WorkingSchedule ExtractFromDto(WorkingScheduleDto op)
        //{
        //    WorkingSchedule result = new WorkingSchedule();
        //    foreach (WorkingScheduleItemDto item in op.Items)
        //    {
        //        result.Items.Add(WorkingScheduleItem.ExtractFromDto(item));
        //    }
        //    foreach (HolidayDto hDto in op.Holidays)
        //    {
        //        result.Holidays.Add(Holiday.ExtractFromDto(hDto));
        //    }
        //    return result;
        //}

        private WorkingScheduleItem GetScheduleForHoliday(DateTime day)
        {
            WorkingScheduleItem holiday = Items.FirstOrDefault(h => h.WeekDay == day.Date.ToString("yyyy-MM-dd"));
            return holiday;
        }

        public WorkingScheduleItem GetScheduleForDay(DateTime day)
        {
            WorkingScheduleItem sched = GetScheduleForHoliday(day);
            if (sched == null)
                sched = GetScheduleForDay(day.DayOfWeek);
            return sched;
        }

        public WorkingScheduleItem GetScheduleForDay(DayOfWeek weekDay)
        {
            return GetScheduleForDay(Enum.GetName(typeof(DayOfWeek), weekDay));
        }

        public WorkingScheduleItem GetScheduleForDay(string weekDay)
        {
            var item = Items.FirstOrDefault(s => s.WeekDay.ToLower() == weekDay.ToLower());
            return item;
        }


    }
}
