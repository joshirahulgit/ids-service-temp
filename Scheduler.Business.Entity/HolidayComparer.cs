using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Business.Entity
{
    public class HolidayComparer : IEqualityComparer<HolidayDto>
    {
        private static HolidayComparer _instance;

        public static HolidayComparer Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new HolidayComparer();

                return _instance;
            }
        }

        #region IEqualityComparer<HolidayDto> Members

        public bool Equals(HolidayDto x, HolidayDto y)
        {
            return x.Repeat == y.Repeat && x.EndTime == y.EndTime && x.Id == y.Id && x.Name == y.Name && x.StartTime == y.StartTime;
        }

        public int GetHashCode(HolidayDto obj)
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    public class HolidayDto //: DtoBase
    {
        public HolidayDto()
        {
        }

        public HolidayDto(long id, string name, DateTime start, DateTime end, bool allDay)
        {
            this.Id = id;
            this.Name = name;
            this.StartTime = start;
            this.EndTime = end;
            this.Repeat = allDay;
        }

        public String Name { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool Repeat { get; set; }
        public long Id { get; set; }
    }
}
