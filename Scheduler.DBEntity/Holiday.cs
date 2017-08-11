using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.DBEntity
{
    public class Holiday : EntityBase
    {
        public String Name { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool Repeat { get; set; }

        //internal static Holiday ExtractFromDto(Common.DataTransferObjects.HolidayDto hDto)
        //{
        //    Holiday result = new Holiday();
        //    result.Id = hDto.Id;
        //    result.Repeat = hDto.Repeat;
        //    result.EndTime = hDto.EndTime;
        //    result.Name = hDto.Name;
        //    result.StartTime = hDto.StartTime;
        //    return result;
        //}

        //public static HolidayDto Convert2Dto(Holiday h)
        //{
        //    return new HolidayDto(h.Id, h.Name, h.StartTime, h.EndTime, h.Repeat);
        //}

        public bool Covers(DateTime date)
        {
            DateTime low = StartTime, hi = EndTime;
            if (this.Repeat)
            {
                low = new DateTime(date.Year, StartTime.Month, StartTime.Day);
                hi = new DateTime(date.Year, EndTime.Month, EndTime.Day);
            }

            return low <= date && date <= hi;
        }
    }
}
