using Scheduler.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Data.Implementation
{
    //By RJ: DatabaseTransactionManagerFactory is now Transaction Factory
    public class TransactionFactory : ITransactionFactory
    {

        private string _connectionString;

        public TransactionFactory(String connectionString)
        {
            _connectionString = connectionString;
        }

        #region ITransactionFactory Members

        public ITransactionManager CreateManager()
        {
            SchedulerDatabaseConnection connection = new SchedulerDatabaseConnection(_connectionString);

            if (connection.State != System.Data.ConnectionState.Open)
            {
                try
                {
                    connection.Open();
                }
                catch (Exception e)
                {
                    throw new SchedulerException(SchedulerExceptionType.DatabaseConnectionFailed, e.Message);
                }
            }
            return new TransactionManager(connection);
        }

        #endregion
    }
}
