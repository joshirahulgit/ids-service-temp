using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Business.Entity
{
    public class UnhandledExceptionEntryDto : DtoBase
    {
        public string UserLogin { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public DateTime ExceptionDate { get; set; }
        public string MethodName { get; set; }

        public string ExceptionDateString
        {
            get { return ExceptionDate.ToShortDateString(); }
        }
    }


    public class UnhandledExceptionEntrysDto : DtoBase
    {
        public IList<UnhandledExceptionEntryDto> Entries { get; set; }

        public UnhandledExceptionEntrysDto()
        {
            Entries = new List<UnhandledExceptionEntryDto>();
        }
    }
}
