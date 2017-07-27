using Scheduler.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Data.Implementation
{
    internal class SchedulerDatabaseConnection
    {
        private SqlConnection _connection;
        private SqlTransaction _transaction;
        private static int _currentVersion = -1;

        private static List<string> verifiedDatabases = new List<string>();
        private static List<string> absentDatabases = new List<string>();
        private static object syncLocker = new object();
        private string _connectionString;

        private const string GLOBAL_DATABASE_NAME = "Global";
        private const string INSURANCE_DB_NAME = "InsuranceVerification";
        private const string ESQUARED_DB_NAME = "esquared";
        private const string DB_SCHEMA_VER_TABLE = "SchedulerSchemaVersion";
        private const string SCRIPTS_FOLDER_NAME = "DBPatches";

        public string CurrentlySetDBName
        {
            get { return _connection != null ? _connection.Database : string.Empty; }
        }

        private static int CurrentVersion
        {
            get
            {
                if (_currentVersion < 0)
                {
                    object[] obj = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyInformationalVersionAttribute), false);
                    AssemblyInformationalVersionAttribute att = obj[0] as AssemblyInformationalVersionAttribute;
                    _currentVersion = Convert.ToInt32(att.InformationalVersion);
                }

                return _currentVersion;
            }
        }

        public string ConnectionString
        {
            get { return _connectionString; }
            set { _connectionString = value; }
        }

        public SchedulerDatabaseConnection(string connStr)
        {
            _connectionString = connStr;
            _connection = new SqlConnection(connStr);
        }

        public void Connect2Global()
        {
            if (_connection.Database != GLOBAL_DATABASE_NAME)
                ChangeDatabase(GLOBAL_DATABASE_NAME);
        }

        public void Conncect2Insurance()
        {
            if (_connection.Database != INSURANCE_DB_NAME)
                ChangeDatabase(INSURANCE_DB_NAME);
        }

        public SqlConnection Connection
        {
            get { return _connection; }
            set { _connection = value; }
        }

        public void Connect2Account(string accountName)
        {
            if (_connection.Database != accountName)
            {
                ChangeDatabase(accountName);

                String path = AppDomain.CurrentDomain.RelativeSearchPath;
                if (String.IsNullOrEmpty(path))
                {
                    path = AppDomain.CurrentDomain.BaseDirectory;
                }
                //TODO: by RJ, How to handle this part. Should start versioning from same place?
                //SyncDatabase(CurrentVersion, Path.Combine(path, SCRIPTS_FOLDER_NAME));
            }
        }

        public void Connect2Esquared()
        {
            if (_connection.Database != ESQUARED_DB_NAME)
                ChangeDatabase(ESQUARED_DB_NAME);
        }


        public void ChangeDatabase(string databaseName)
        {
            if (absentDatabases.Contains(databaseName))
                throw new Exception(String.Format("Database '{0}' does not exist. Make sure that the name is entered correctly.", databaseName));
            try
            {
                _connection.ChangeDatabase(databaseName);
            }
            catch
            {
                absentDatabases.Add(databaseName);
                throw;
            }
        }

        private void CommitChanges()
        {
            Commit();
            BeginTransaction();
        }

        private void SyncDatabase(int requredVersion, string path2scripts)
        {
            lock (syncLocker)
            {
                if (verifiedDatabases.Contains(_connection.Database))
                    return;

                int? currentVersion = GetDatabaseVersion();

                bool exceptionRaised = false;

                try
                {
                    if (!currentVersion.HasValue)
                        currentVersion = 0;

                    if (currentVersion.Value >= requredVersion || currentVersion.Value == -1)
                        return;

                    int maxAge = GlobalContext.ApplicationSetting.DBAgeInPatchesApplicableForAutoPatching;
                    if (requredVersion - currentVersion.Value > maxAge)
                        throw new SchedulerException(SchedulerExceptionType.DbPatchesAreNotAvailable,
                            string.Format("Database is more then {0} patch(es) old. Please contact your administrator.", maxAge));

                    ApplyPatches(GetRequiredScripts(currentVersion, requredVersion, path2scripts));
                    CommitChanges();
                }
                catch
                {
                    exceptionRaised = true;
                    throw;
                }
                finally
                {
                    if (!exceptionRaised)
                        verifiedDatabases.Add(_connection.Database);
                }
            }
        }

        private void ApplyPatch(String patchSQL)
        {
            String[] sqlBlocks = patchSQL.Split(new string[] { Environment.NewLine + "GO" }, StringSplitOptions.None);
            using (DbCommand cmd = CreateCommand())
            {
                foreach (String sqlBlock in sqlBlocks)
                {
                    if (String.IsNullOrEmpty(sqlBlock))
                        continue;
                    cmd.CommandText = sqlBlock;
                    cmd.CommandTimeout = 0;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void ApplyPatches(List<String> patches)
        {
            string cpatch = string.Empty;
            bool transactionIsAlreadyStarted = _transaction != null;

            if (!transactionIsAlreadyStarted)
                BeginTransaction();
            try
            {
                foreach (string patch in patches)
                {
                    cpatch = patch;
                    if (!string.IsNullOrEmpty(patch))
                    {
                        System.Diagnostics.Debug.Write(patch.Substring(0, 33));
                        ApplyPatch(patch);
                    }
                }
                if (!transactionIsAlreadyStarted)
                    Commit();
            }
            catch
            {
                //if (!transactionIsAlreadyStarted)
                Rollback();
                //TODO: Need to think about it.
                //throw new SchedulerException(SchedulerExceptionType.StorageSyncronizationFailed, "Database syncronization failed - " + cpatch.Substring(28, 5));
            }
        }

        private List<String> GetRequiredScripts(int? currenVersion, int requiredVersion, string path)
        {
            List<String> result = new List<string>();

            List<String> filePaths = Directory.GetFiles(path).ToList();

            int tempVersion = 0;

            foreach (string filePath in filePaths)
            {
                tempVersion = Convert.ToInt32(Path.GetFileNameWithoutExtension(filePath).Substring(5));

                if (tempVersion > currenVersion && tempVersion <= requiredVersion)
                    result.Add(File.ReadAllText(filePath));
            }

            if (result.Count < requiredVersion - currenVersion)
                throw new SchedulerException(SchedulerExceptionType.DbPatchesAreNotAvailable, "Database can not be updated because not all patches exist");

            return result;
        }

        private int? GetDatabaseVersion()
        {
            //Get current database version 
            int? dbVersion = null;
            string sql = String.Concat("SELECT dbVersion FROM ", DB_SCHEMA_VER_TABLE);
            try
            {
                using (DbCommand cmd = CreateCommand())
                {
                    cmd.CommandText = sql;
                    object dbRes = cmd.ExecuteScalar();

                    if (dbRes != null && dbRes != DBNull.Value)
                        dbVersion = Convert.ToInt32(dbRes);
                }
            }
            catch
            {
            }
            return dbVersion;
        }


        public SqlBulkCopy InitCopier()
        {
            if (_transaction == null)
                return new SqlBulkCopy(_connection as SqlConnection);
            else
                return new SqlBulkCopy(_connection as SqlConnection,
                                       SqlBulkCopyOptions.Default,
                                       _transaction);
        }

        public SqlCommand CreateCommand()
        {
            SqlCommand cmd = _connection.CreateCommand();
            if (_transaction != null)
                cmd.Transaction = _transaction;
            return cmd;
        }

        public bool IsInTransaction
        {
            get { return _transaction != null; }
        }

        public void Open()
        {
            _connection.Open();
        }

        public void Commit()
        {
            if (IsInTransaction)
            {
                if (_transaction.Connection != null)
                    _transaction.Commit();
                _transaction.Dispose();
                _transaction = null;
            }
        }

        public void Rollback()
        {
            if (IsInTransaction)
            {
                _transaction.Rollback();
                _transaction.Dispose();
                _transaction = null;
            }
        }

        public void Close()
        {
            _connection.Close();
        }

        public DbTransaction BeginTransaction()
        {
            if (_transaction != null)
                throw new NotImplementedException("Nested transactions are not supported");

            _transaction = _connection.BeginTransaction();

            return _transaction;
        }

        public ConnectionState State
        {
            get { return _connection.State; }
        }
    }
}
