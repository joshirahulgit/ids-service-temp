using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Core
{
    public struct TimeResourceRequest
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public long PatientID { get; set; }

        public TimeResourceRequest(long patientId)
            : this(DateTime.MinValue, DateTime.MaxValue)
        {
            this.PatientID = patientId;
        }

        public TimeResourceRequest(DateTime start, DateTime end)
            : this()
        {
            this.StartDate = start;
            this.EndDate = end;
        }

        public static TimeResourceRequest InitFromObject(object o)
        {
            TimeResourceRequest result = new TimeResourceRequest();

            DateTime[] arr = o as DateTime[];

            if (arr != null && arr.Length == 2)
            {
                result.StartDate = arr[0];
                result.EndDate = arr[1];
            }
            return result;
        }
    }
}
