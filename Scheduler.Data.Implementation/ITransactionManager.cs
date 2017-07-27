using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Data.Implementation
{
    public interface ITransactionManager : IDisposable
    {
        TResult ExecuteCommand<TResult>(Func<RepositoryLocator, TResult> command)
           where TResult : class;//TODO: Not sure about it now. By RJ: //, IDtoResponseWrapper;

        TResult ExecuteReadOnlyCommand<TResult>(Func<RepositoryLocator, TResult> command)
          where TResult : class;//TODO: Not sure about it now. By RJ: //, IDtoResponseWrapper;

        void BeginTransaction();
        void CommitTransaction();
        void Rollback();
    }
}
