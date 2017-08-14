using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.DBEntity
{
    public class UnhandledExceptionEntry : EntityBase
    {
        public UnhandledExceptionEntry(string userLogin, string message, string stackTrace, DateTime exceptionDate, string methodName)
        {
            UserLogin = userLogin;
            Message = message;
            StackTrace = stackTrace;
            ExceptionDate = exceptionDate;
            MethodName = methodName;
        }

        public UnhandledExceptionEntry()
        {
        }

        public string UserLogin { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public DateTime ExceptionDate { get; set; }
        public string MethodName { get; set; }
    }
}
