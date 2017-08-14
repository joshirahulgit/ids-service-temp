using Scheduler.DBEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Data.Specification
{
    public interface IAuditRepository : IRepository, ICanCreate<AuditEntry>
    {
        List<AuditEntry> FindAuditRecords(AuditRequest request);
        void CreateUnhandledException(UnhandledExceptionEntry unhandledExceptionEntry);
        List<UnhandledExceptionEntry> FindUnhandledExceptions(ExceptionRequest exceptionRequest);
        void LogUnhandledException(UnhandledExceptionEntry req);
    }
}
