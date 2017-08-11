using Scheduler.Core;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.DBEntity
{
    public class AppointmentResourceTime : AppointmentResource
    {
        public virtual DateTime StartTime { get; set; }
        public virtual DateTime EndTime { get; set; }

        //public override AppointmentResource Create(RepositoryLocator locator)
        //{
        //    //Data validation might be implemen
        //    return locator.ResourceRepository.Create(this);
        //}

        public void SetDateTime(DateTime startDate, DateTime endTime)
        {
            StartTime = startDate;
            EndTime = endTime;
        }

        public override string DisplayText { get { return string.Format("{0} ({1})", StartTime, new DateTime((EndTime - StartTime).Ticks).ToString("HH\\:mm")); } }

        public override AppointmentResource Clear()
        {
            return new AppointmentResourceTime()
            {
                Id = this.Id,
                ResourceType = this.ResourceType,
                Account = new Account(this.Account.Id),
                StartTime = this.StartTime,
                EndTime = this.EndTime
            };
        }

        public double AppointmentRangeSeconds
        {
            get
            {
                return (this.EndTime - this.StartTime).TotalSeconds;
            }
        }

        public static AppointmentResourceTime Infinity
        {
            get
            {
                return new AppointmentResourceTime()
                {
                    StartTime = DateTime.MinValue,
                    EndTime = DateTime.MaxValue,
                    Id = Environment.TickCount,
                    ResourceType = new AppointmentResourceType((long)ResourceTypes.Time)
                };
            }
        }

        public bool IsSpecified
        {
            get { return (StartTime != DateTime.MinValue) && (EndTime != DateTime.MinValue); }
        }

        public void ExtendRange(long minutesAmount)
        {
            this.EndTime = this.EndTime.AddMinutes(minutesAmount);
        }

        //public static AppointmentResourceTime ExtractFromDto(AppointmentResourceTimeDto dto)
        //{
        //    AppointmentResourceTime result = new AppointmentResourceTime();
        //    result.Account = new Account(dto.AccountId);
        //    if (dto.StartTime <= dto.EndTime)
        //    {
        //        result.EndTime = dto.EndTime;
        //        result.StartTime = dto.StartTime;
        //    }
        //    else
        //        throw new SchedulerException(SchedulerExceptionType.TimeResourceIsWrong, "Appointment start time is bigger than end time");

        //    result.Id = dto.Id;
        //    result.ResourceType = new AppointmentResourceType(dto.TypeId);

        //    return result;
        //}

        public override bool IsValid(AppointmentResource resource)
        {
            AppointmentResourceTime resourceTime = (resource as AppointmentResourceTime);
            if (resourceTime == null)
                return true;
            else
            {
                bool notMatch = (resourceTime.StartTime < this.StartTime && resourceTime.EndTime <= this.StartTime) || (resourceTime.StartTime >= this.EndTime && resourceTime.EndTime > this.EndTime);
                return !notMatch;
            }
        }

        //public static AppointmentResourceTime ExtractFromReader(System.Data.IDataReader reader, long accountID)
        //{
        //    using (SafeDataReader sr = new SafeDataReader(reader))
        //    {
        //        AppointmentResourceTime res = new AppointmentResourceTime();

        //        res.ResourceType = new AppointmentResourceType((long)ResourceTypes.Time);
        //        res.Account = new Account(accountID);
        //        res.Id = sr.GetInt64("TimeResourceID");
        //        res.StartTime = sr.GetDateTime("StartTime");
        //        if (res.StartTime == SqlDateTime.MinValue) res.StartTime = DateTime.MinValue;
        //        res.EndTime = sr.GetDateTime("EndTime");
        //        if (res.EndTime == SqlDateTime.MinValue) res.EndTime = DateTime.MinValue;

        //        return res;
        //    }
        //}

        public void InitTime()
        {
            if (StartTime == DateTime.MinValue)
                StartTime = SqlDateTime.MinValue.Value;

            if (EndTime == DateTime.MinValue)
                EndTime = SqlDateTime.MinValue.Value;
        }
    }
}
