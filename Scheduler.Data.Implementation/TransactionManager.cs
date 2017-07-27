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
    internal class TransactionManager : TransactionManagerBase
    {
        private SchedulerDatabaseConnection _databaseConnection;
        private uint _runningTransactions;


        public TransactionManager(SchedulerDatabaseConnection dbConnection)
        {
            _databaseConnection = dbConnection;
            _runningTransactions = 0;
            Locator = new RepositoryLocator(_databaseConnection);
        }

        public override void BeginTransaction()
        {
            base.BeginTransaction();

            if (_runningTransactions == 0)
                _databaseConnection.BeginTransaction();

            _runningTransactions++;
        }

        public override void CommitTransaction()
        {
            base.CommitTransaction();

            if (--_runningTransactions == 0)
                _databaseConnection.Commit();
        }

        public override void Rollback()
        {
            base.Rollback();

            if (--_runningTransactions == 0)
                _databaseConnection.Rollback();
        }

        protected override void Dispose(bool disposing)
        {
            if (!disposing)
                return;
            // free managed resources
            if (!IsDisposed && IsInTranx)
            {
                Rollback();
            }
            Close();
            Locator = null;
            IsDisposed = true;
        }

        private void Close()
        {
            if (_databaseConnection == null)
                return;
            if (_databaseConnection.State == System.Data.ConnectionState.Closed)
                return;
            _databaseConnection.Close();
        }
    }
}
