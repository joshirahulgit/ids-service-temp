using Scheduler.Core;
using Scheduler.Data.Implementation;
using Scheduler.Data.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Business.Implementation
{
    public class ServiceBase
    {

        private ITransactionFactory tFactory;

        public ServiceBase()
        {
            string cs = GlobalContext.ApplicationSetting.CS;
            tFactory = new TransactionFactory(cs);
            //TODO: Varify with Sunil and think for proper place for these codes. 
            //Previous comment by RJ: Need to have request context to call this method
            using (ITransactionManager manager = tFactory.CreateManager())
            {
                var res = manager.ExecuteReadOnlyCommand<IAccountRepository>(Handler);
                //var accounts = res.FindAllAccounts();
            }
        }

        private static IAccountRepository Handler(RepositoryLocator arg)
        {
            var accounts = arg.AccountRepository.FindAllAccounts();
            return arg.AccountRepository;
        }

        protected TResult ExecuteCommand<TResult>(Func<RepositoryLocator, TResult> command)
            where TResult : class//TODO: Not sure how to deal with it, IDtoResponseWrapper
        {
            //TODO: Not sure now about it Container.RequestContext = RequestContext; 
            using (ITransactionManager manager = tFactory.CreateManager())
            {
                return manager.ExecuteCommand(command);
            }
        }

        protected TResult ExecuteReadOnlyCommand<TResult>(Func<RepositoryLocator, TResult> command)
            where TResult : class//TODO: Not sure how to deal with, IDtoResponseWrapper
        {
            //Container.RequestContext = RequestContext;//TODO: Not sure how to deal it.
            using (ITransactionManager manager = tFactory.CreateManager())
            {
                return manager.ExecuteReadOnlyCommand(command);
            }
        }

        //TODO: Not sured how to deal it.
        //public virtual List<AuditEntry> GetCommandDescription(String methodName, Exception raisedException, RepositoryLocator locator, IDtoResponseWrapper actionResult, params object[] methodParams)
        //{
        //    return new List<AuditEntry>();
        //}

        //TODO: How to deal it.
        //public RequestContext RequestContext = new RequestContext();
    }
}
