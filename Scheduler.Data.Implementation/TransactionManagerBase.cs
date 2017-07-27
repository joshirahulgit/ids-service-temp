using Scheduler.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Data.Implementation
{
    internal abstract class TransactionManagerBase : ITransactionManager
    {
        protected bool IsInTranx;

        public RepositoryLocator Locator { get; set; }

        #region ITransactionManager Members

        public TResult ExecuteReadOnlyCommand<TResult>(Func<RepositoryLocator, TResult> command) where TResult : class//TODO: Not sure about DTOResponse, Common.Messages.IDtoResponseWrapper
        {
            try
            {
                var result = command.Invoke(Locator);
                CheckForWarnings(result);
                return result;
            }
            catch (SchedulerException exception)
            {
                var type = typeof(TResult);
                //TODO: Not sure about DTOResponse
                //var instance = Activator.CreateInstance(type, true) as IDtoResponseWrapper;
                //if (instance != null) instance.Response.AddSchedulerException(exception);
                //return instance as TResult;
                return null;
            }
            catch
            {
                throw;
            }
        }

        public TResult ExecuteCommand<TResult>(Func<RepositoryLocator, TResult> command) where TResult : class//TODO: Not sure about DTOResponse, Common.Messages.IDtoResponseWrapper
        {
            Exception occuredException = null;
            TResult result = null;
            try
            {
                BeginTransaction();
                result = command.Invoke(Locator);
                CommitTransaction();
                CheckForWarnings(result);
                return result;
            }
            catch (SchedulerException exception)
            {
                occuredException = exception;
                if (IsInTranx)
                    Rollback();
                var type = typeof(TResult);
                result = Activator.CreateInstance(type, true) as TResult;
                //TODO: Not sure about DTOResponse
                //if (result != null) result.Response.AddSchedulerException(exception);
                //return result as TResult;
                return null;
            }
            catch (Exception e)
            {
                occuredException = e;
                throw;
            }
            finally
            {
                try
                {
                    AuditAction(command, occuredException, result);
                }
                catch { }
            }
        }

        private void AuditAction<TResult>(Func<RepositoryLocator, TResult> command, Exception raisedException, TResult actionResult) where TResult : class//TODO: Not sure about DTOResponse, IDtoResponseWrapper
        {
            //TODO: Resolving of caller method name might be optimized!!! (Just found quick solution and implemented it) 
            StackTrace stackTrace = new StackTrace(false);
            if (stackTrace.FrameCount < 3)
                return;

            String methodName = stackTrace.GetFrame(3).GetMethod().Name;

            RepositoryLocator locator = this.Locator;
            FieldInfo[] additionalParamsInfo = command.Target.GetType().GetFields();
            if (additionalParamsInfo.Length == 0)
                return;

            FieldInfo targetType = null;
            foreach (FieldInfo item in additionalParamsInfo)
                if (item.GetValue(command.Target) != null)
                    //TODO: Not sure about ServiceBase now.
                    //if (item.GetValue(command.Target).GetType().IsSubclassOf(typeof(ServiceBase)))
                    targetType = item;

            if (targetType != null)
            {
                //TODO: Not sure about ServiceBase.
                //ServiceBase service = targetType.GetValue(command.Target) as ServiceBase;

                ////            ServiceBase service = additionalParamsInfo[0].GetValue(command.Target) as ServiceBase;
                //if (service == null) return;
                object[] additionalParams = new object[additionalParamsInfo.Length - 1];

                for (int i = 1; i < additionalParamsInfo.Length; i++)
                {
                    additionalParams[i - 1] = additionalParamsInfo[i].GetValue(command.Target);
                }

                if (raisedException != null)
                {
                    if (raisedException.GetType().Name != "SchedulerException")
                    {
                        //TODO: uncomment next 3 lines once audit Repo is added.
                        //  locator.AuditRepository.CreateUnhandledException(
                        //new UnhandledExceptionEntry(Container.RequestContext.UserName, raisedException.Message,
                        //    raisedException.StackTrace, DateTime.Now, methodName));
                        //                    locator.AuditRepository.Create(new AuditEntry(null,
                        //                        string.Format("Attempt to call \"{2}\" failed with message {0}:'{1}'", raisedException.GetType().Name, raisedException.Message, methodName)));
                    }
                }
                else
                {
                    //TODO: uncomment this block when AuditEntry is added.
                    //List<AuditEntry> actionDescription = service.GetCommandDescription(methodName, raisedException,
                    //    locator,
                    //                                                               actionResult, additionalParams);
                    //if (actionDescription != null)
                    //{
                    //    foreach (AuditEntry auditEntry in actionDescription)
                    //    {
                    //        if (auditEntry != null)
                    //        {
                    //            locator.AuditRepository.Create(auditEntry);
                    //        }
                    //    }
                    //}
                }
            }
        }
        private void CheckForWarnings<TResult>(TResult result)
        {
            //TODO: uncomment this block when DTOResponseWrapper is decided.
            //var response = result as IDtoResponseWrapper;
            //if (response == null)
            //    return;
            //var notifier = Container.RequestContext.Notifier;
            //if (notifier.HasWarnings)
            //    response.Response.AddSchedulerWarnings(notifier.RetrieveWarnings());
        }

        public virtual void BeginTransaction()
        {
            this.IsInTranx = true;
            return;
        }

        public virtual void CommitTransaction()
        {
            this.IsInTranx = false;
            return;
        }

        public virtual void Rollback()
        {
            this.IsInTranx = false;
            return;
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected bool IsDisposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing) return;
            if (!IsDisposed && IsInTranx)
                Rollback();
            Locator = null;
            IsDisposed = true;
        }

        #endregion
    }
}
