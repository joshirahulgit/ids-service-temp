using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Business.Entity
{
    public class WorkingScheduleItemDto //: DtoBase
    {
        public WorkingScheduleItemDto()
        {
        }

        public WorkingScheduleItemDto(WorkingScheduleItemDto item)
        {
            Id = item.Id;
            ModalityId = item.Id;
            WeekDay = item.WeekDay;
            StartTime = item.StartTime;
            EndTime = item.EndTime;
            BreakFrom = item.BreakFrom;
            BreakTo = item.BreakTo;
            IsActive = item.IsActive;
        }

        public long Id { get; set; }
        public long ModalityId { get; set; }
        public String WeekDay { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime? BreakFrom { get; set; }
        public DateTime? BreakTo { get; set; }

        public bool IsActive { get; set; }


        public override string ToString()
        {

            return (this.BreakFrom.HasValue && this.BreakTo.HasValue)
                ?
                String.Format("[{9}] {0} ({1}:{2}-{3}:{4}; {5}:{6}-{7}:{8};)", WeekDay, StartTime.Hour, StartTime.Minute, BreakFrom.Value.Hour, BreakFrom.Value.Minute,
                    BreakTo.Value.Hour, BreakTo.Value.Minute, EndTime.Hour, EndTime.Minute, ModalityId)
                :
                String.Format("[{5}] {0} ({1}:{2}-{3}:{4})", WeekDay, StartTime.Hour, StartTime.Minute, EndTime.Hour, EndTime.Minute, ModalityId);

        }

        public bool DiffersFrom(WorkingScheduleItemDto item)
        {
            return item.IsActive != IsActive ||
                   item.StartTime != StartTime ||
                   item.EndTime != EndTime ||
                   item.BreakTo != BreakTo ||
                   item.BreakFrom != BreakFrom ||
                   item.ModalityId != ModalityId ||
                   item.WeekDay != WeekDay;
        }

        public List<KeyValuePair<long, long>> GetOverlappingRanges(WorkingScheduleItemDto[] schedules)
        {
            List<KeyValuePair<long, long>> overlappingRanges = this.GetRanges();
            foreach (WorkingScheduleItemDto schedule in schedules)
            {
                List<KeyValuePair<long, long>> nextIterationResult = new List<KeyValuePair<long, long>>();
                List<KeyValuePair<long, long>> overlapTestRanges = schedule.GetRanges();
                for (int index = 0; index < overlappingRanges.Count; index++)
                {
                    KeyValuePair<long, long> overlappingRange = overlappingRanges[index];
                    foreach (KeyValuePair<long, long> overlapTestRange in overlapTestRanges)
                    {
                        long start = Math.Max(overlappingRange.Key, overlapTestRange.Key);
                        long end = Math.Min(overlappingRange.Value, overlapTestRange.Value);

                        if (start < end) //overlapping result is non-zero interval
                        {
                            nextIterationResult.Add(new KeyValuePair<long, long>(start, end));
                        }
                    }
                }
                overlappingRanges = nextIterationResult;
            }
            return overlappingRanges;
        }

        private List<KeyValuePair<long, long>> GetRanges()
        {
            var ranges = new List<KeyValuePair<long, long>>();
            if (BreakFrom.HasValue && BreakTo.HasValue)
            {
                ranges.Add(new KeyValuePair<long, long>(StartTime.TimeOfDay.Ticks, BreakFrom.Value.TimeOfDay.Ticks));
                ranges.Add(new KeyValuePair<long, long>(BreakTo.Value.TimeOfDay.Ticks, EndTime.TimeOfDay.Ticks));
            }
            else ranges.Add(new KeyValuePair<long, long>(StartTime.TimeOfDay.Ticks, EndTime.TimeOfDay.Ticks));
            return ranges;
        }

        public bool Contains(WorkingScheduleItemDto schedule)
        {
            long smallStart = schedule.StartTime.Hour * 60 + schedule.StartTime.Minute;
            long smallEnd = schedule.EndTime.Hour * 60 + schedule.EndTime.Minute;
            long start = this.StartTime.Hour * 60 + this.StartTime.Minute;
            long end = this.EndTime.Hour * 60 + this.EndTime.Minute;
            if (this.BreakFrom.HasValue && this.BreakTo.HasValue)
            {
                long breakStart = this.BreakFrom.Value.Hour * 60 + this.BreakFrom.Value.Minute;
                long breakEnd = this.BreakTo.Value.Hour * 60 + this.BreakTo.Value.Minute;
                return (start <= smallStart && breakStart >= smallEnd) || (breakEnd <= smallStart && end >= smallEnd); ;
            }
            else return (start <= smallStart && end >= smallEnd);
        }
    }

    public class WorkingScheduleDto //: DtoBase
    {
        public List<WorkingScheduleItemDto> Items { get; set; }
        public List<HolidayDto> Holidays { get; set; }

        public WorkingScheduleDto()
        {
            Items = new List<WorkingScheduleItemDto>();
            Holidays = new List<HolidayDto>();
        }

        public WorkingScheduleDto(WorkingScheduleDto schedule) : this(schedule, false)
        {
        }

        public WorkingScheduleDto(WorkingScheduleDto schedule, bool onlyActive) : this()
        {
            foreach (WorkingScheduleItemDto item in schedule.Items)
            {
                if (!onlyActive || item.IsActive)
                    Items.Add(new WorkingScheduleItemDto(item));
            }
        }

        public WorkingScheduleItemDto GetScheduleForDay(DateTime day)
        {
            WorkingScheduleItemDto sched = GetScheduleForHoliday(day);
            if (sched == null)
                sched = GetScheduleForDay(day.DayOfWeek);
            if (sched != null)
            {
                sched = new WorkingScheduleItemDto(sched);
                sched.StartTime = day.Date + sched.StartTime.TimeOfDay;
                sched.EndTime = day.Date + sched.EndTime.TimeOfDay;
            }
            return sched;
        }

        private WorkingScheduleItemDto GetScheduleForHoliday(DateTime day)
        {
            WorkingScheduleItemDto holiday = Items.FirstOrDefault(h => h.WeekDay == day.Date.ToString("yyyy-MM-dd"));
            return holiday;
        }

        public WorkingScheduleItemDto GetScheduleForDay(DayOfWeek weekDay)
        {
            return GetScheduleForDay(Enum.GetName(typeof(DayOfWeek), weekDay));
        }

        public WorkingScheduleItemDto GetScheduleForDay(string weekDay)
        {
            var item = Items.FirstOrDefault(s => s.WeekDay.ToLower() == weekDay.ToLower());
            return item;
        }

        public override string ToString()
        {
            if (Items.Count == 0)
                return "<empty>";
            List<WorkingScheduleItemDto> items = new List<WorkingScheduleItemDto>();
            foreach (WorkingScheduleItemDto realItem in Items)
            {
                WorkingScheduleItemDto item = items.FirstOrDefault(i => i.StartTime == realItem.StartTime && i.EndTime == realItem.EndTime);
                if (item != null)
                    item.WeekDay = item.WeekDay + "," + realItem.WeekDay.Substring(0, 3);
                else items.Add(new WorkingScheduleItemDto()
                {
                    StartTime = realItem.StartTime,
                    EndTime = realItem.EndTime,
                    WeekDay = realItem.WeekDay.Substring(0, 3),
                });
            }

            return String.Join(";", items.Select(i => String.Format("{0} ({1}:{2}-{3}:{4})",
                i.WeekDay, i.StartTime.Hour, i.StartTime.Minute, i.EndTime.Hour, i.EndTime.Minute)).ToArray());
        }

        public bool DiffersFrom(WorkingScheduleDto workingSchedule)
        {
            if (workingSchedule == null) return true;
            if (workingSchedule.Items != null || this.Items != null)
            {
                if (workingSchedule.Items == null) return true;
                if (Items.Count != workingSchedule.Items.Count) return true;

                foreach (WorkingScheduleItemDto thisItem in Items)
                {
                    WorkingScheduleItemDto item = workingSchedule.GetScheduleForDay(thisItem.WeekDay);
                    if (item == null) return true;
                    if (item.DiffersFrom(thisItem)) return true;
                }
            }
            if (this.Holidays.Count != workingSchedule.Holidays.Count)
                return true;
            foreach (HolidayDto holiday in workingSchedule.Holidays)
            {
                if (!this.Holidays.Contains<HolidayDto>(holiday, HolidayComparer.Instance))
                    return true;
            }

            return false;
        }

        public bool IsHoliday(DateTime day)
        {
            foreach (HolidayDto holiday in Holidays)
            {
                DateTime start, end;
                if (holiday.Repeat)
                {
                    start = new DateTime(day.Year, holiday.StartTime.Month, holiday.StartTime.Day);
                    end = new DateTime(day.Year, holiday.EndTime.Month, holiday.EndTime.Day).AddDays(1).AddSeconds(-1);
                }
                else
                {
                    start = holiday.StartTime;
                    end = holiday.EndTime;
                }
                if (start.Date <= day.Date && end.Date >= day.Date)
                    return true;
            }
            return false;
        }

        public bool IsWeekEnd(DateTime day)
        {
            return GetScheduleForDay(day) == null;
        }
    }
}
