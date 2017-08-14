using Scheduler.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Business.Entity
{
    public class AppointmentResourceTimeDto : AppointmentResourceDto
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public override string DisplayText
        {
            get { return string.Format("{0} ({1})", StartTime, new DateTime((EndTime - StartTime).Ticks).ToString("HH\\:mm")); }
        }

        public AppointmentResourceTimeDto() : base((long)ResourceTypes.Time)
        {
        }

        private AppointmentResourceTimeDto(long id, long accountId) : this()
        {
            this.Id = id;
            this.AccountId = accountId;
        }

        public AppointmentResourceTimeDto(DateTime startTime, DateTime endTime) : this()
        {
            StartTime = startTime;
            EndTime = endTime;
        }

        public AppointmentResourceTimeDto(long accountId, DateTime startTime, DateTime endTime)
            : this(startTime, endTime)
        {
            this.AccountId = accountId;
        }


        public override bool IsValid(AppointmentResourceDto resource)
        {
            AppointmentResourceTimeDto resourceTime = (resource as AppointmentResourceTimeDto);
            if (resourceTime == null)
                return true;
            else
            {
                bool notMatch = (resourceTime.StartTime < this.StartTime && resourceTime.EndTime <= this.StartTime) || (resourceTime.StartTime >= this.EndTime && resourceTime.EndTime > this.EndTime);
                return !notMatch;
            }
        }

        public override string ToString()
        {
            string start = StartTime.ToString("HH:mm");
            string end = EndTime.ToString("HH:mm");
            string range = new DateTime((EndTime - StartTime).Ticks).ToString("HH\\:mm");
            string endDate = (EndTime.Date != StartTime.Date) ? (EndTime.ToShortDateString() + " ") : string.Empty;
            return string.Format("{4} {3} {0} - {5}{1} ({2})", start, end, range, Id, StartTime.ToShortDateString(), endDate);
        }

        protected override AppointmentResourceDto DoClone()
        {
            return this.MemberwiseClone() as AppointmentResourceDto;
        }

        protected override void DoCopy(AppointmentResourceDto dest)
        {
            throw new NotImplementedException();
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool operator ==(AppointmentResourceTimeDto left, AppointmentResourceTimeDto right)
        {
            if (ReferenceEquals(left, null))
                if (ReferenceEquals(right, null)) return true;
                else return false;
            else if (ReferenceEquals(right, null)) return false;
            if (ReferenceEquals(left, right)) return true;

            return (left.StartTime == right.StartTime) && (left.EndTime == right.EndTime);
        }

        public static bool operator !=(AppointmentResourceTimeDto left, AppointmentResourceTimeDto right)
        {
            return !(left == right);
        }

        public bool IsStartTimeShiftValid(TimeSpan shift)
        {
            DateTime tmp = StartTime + shift;
            return (tmp.Date == StartTime.Date) && (tmp < EndTime);
        }

        public bool IsEndTimeShiftValid(TimeSpan shift)
        {
            DateTime tmp = EndTime + shift;
            return (tmp.Date == EndTime.Date) && (StartTime < tmp);
        }
    }
}
