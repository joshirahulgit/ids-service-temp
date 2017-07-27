using Scheduler.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Data.Implementation
{
    internal abstract class DatabaseRepository
    {
        public ReadOnlyCollection<TimeZoneInfo> AllZones
        {
            get { return _allZones; }
            set { _allZones = value; }
        }

        protected SchedulerDatabaseConnection _databaseConnection;
        private ConnectionMode _currentMode;
        private readonly IDictionary<Type, MethodInfo> _setters;
        ReadOnlyCollection<TimeZoneInfo> _allZones = TimeZoneInfo.GetSystemTimeZones();

        protected const int UNIQUE_IDX_VIOLATION_ERROR_CODE = 2627;
        protected const string DESCENDING_SORT_STR = "DESC";

        private static Dictionary<long, String> accountNames;
        private static object namesLocker;

        protected MethodInfo GetSetter(Type type)
        {
            if (_setters.ContainsKey(type)) return _setters[type];
            var setter = type.GetProperty("Id").GetSetMethod(true);
            _setters.Add(type, setter);
            return setter;
        }

        static DatabaseRepository()
        {
            accountNames = new Dictionary<long, string>();
            namesLocker = new object();
        }

        public string CurrentlySetDBName
        {
            get { return _databaseConnection.CurrentlySetDBName; }
        }

        protected ConnectionMode CurrentConnectionMode
        {
            get { return _currentMode; }
        }

        protected void SetNewId(Type t, object instance, long id)
        {
            var setter = GetSetter(t);
            setter.Invoke(instance, new object[] { id });
        }

        protected void SetNewId(Type t, object instance, int id)
        {
            var setter = GetSetter(t);
            setter.Invoke(instance, new object[] { id });
        }


        public DatabaseRepository(SchedulerDatabaseConnection dbConnection)
        {
            _databaseConnection = dbConnection;
            _currentMode = ConnectionMode.Global;
            _setters = new Dictionary<Type, MethodInfo>();
        }

        protected object[] AddInSQLClause<T>(StringBuilder sql, IEnumerable<T> values, params object[] sqlParams)
        {
            List<object> result = new List<object>();
            foreach (T val in values)
            {
                if (result.Count > 0)
                    sql.Append(" , ");
                else
                    sql.Append("IN ( ");

                result.Add(val);
                sql.Append("@sip").Append(result.Count);
            }
            if (result.Count > 0)
                sql.Append(" ) ");

            if (sqlParams == null)
            {
                return result.ToArray();
            }
            else
            {
                List<object> incomeParams = new List<object>(sqlParams);
                incomeParams.AddRange(result);
                return incomeParams.ToArray();
            }
        }

        private SqlCommand InitDatabaseCommand(string sql, params object[] param)
        {
            return InitDatabaseCommand(sql, false, param);
        }

        public int GetTimeZonesDiff
        {
            get
            {
                try
                {
                    TimeZoneInfo cZone = _allZones.FirstOrDefault(p => p.StandardName == GlobalContext.RequestContext.ClientTimeZone);//C: by RJ container is replaced with GlobalContext
                    if (cZone != null)
                    {
                        return ((int)(TimeZoneInfo.ConvertTime(DateTime.Now, cZone) - DateTime.Now).TotalMinutes);
                    }
                    //                    DateTime clTime = TimeZoneInfo.ConvertTime(DateTime.Now, clTimezone);
                    return 0;
                }
                catch
                {
                    return 0;
                }
            }
        }

        private SqlCommand InitDatabaseCommand(string sql, bool doNotPrepareParams, params object[] param)
        {
            SqlCommand cmd = _databaseConnection.CreateCommand();

            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;

            if (doNotPrepareParams)
                return cmd;

            string[] sqlParts = sql.Split('@');

            if (sqlParts.Length > 1)
            {
                List<String> paramNames = new List<string>();

                for (int i = 1; i < sqlParts.Length; i++)
                {
                    if (sqlParts[i].Length == 0 || sqlParts[i].ToUpper() == "IDENTITY")
                        continue;

                    int firstSpaceIndex = sqlParts[i].IndexOfAny(new char[] { ' ', '(', ')', ',', '\r', '\n' });

                    if (firstSpaceIndex < 0)
                    {
                        if (i == sqlParts.Length - 1)
                            firstSpaceIndex = sqlParts[i].Length;
                        else
                            continue;
                    }
                    String sqlChunk = sqlParts[i].Substring(0, firstSpaceIndex);
                    paramNames.Add(String.Concat("@", sqlChunk));
                }

                List<String> noDupesParams = paramNames.Distinct().ToList();

                if (noDupesParams.Count != param.Length)
                    throw new ArgumentException("Number of specified params doesn't match with passed sql query");

                for (int i = 0; i < noDupesParams.Count; i++)
                    cmd.Parameters.Add(CreateParameter(noDupesParams[i], param[i]));
            }

            return cmd;
        }

        private IDataParameter CreateParameter(string paramName, object paramValue)
        {
            if (paramValue == null)
                paramValue = DBNull.Value;

            SqlParameter param = new SqlParameter(paramName, paramValue);
            return param;
        }

        protected void SetConnection2Insurance()
        {
            _databaseConnection.Conncect2Insurance();
            _currentMode = ConnectionMode.Insurance;
        }

        protected void SetConnection2Global()
        {
            _databaseConnection.Connect2Global();
            _currentMode = ConnectionMode.Global;
        }

        protected void SetConnection2Esquared()
        {
            _databaseConnection.Connect2Esquared();

            _currentMode = ConnectionMode.Esquared;
        }

        protected bool IsCodeCategoryRestrictedByModality()
        {
            bool hasRestriction = Convert.ToBoolean(ExecuteScalar(
                        "SELECT as1.Value FROM AccountSettings as1 WHERE as1.Application='scheduler.sl' AND as1.Name='IsCodeCategoryRestrictedByModality' AND as1.IsActive=1"));

            return hasRestriction;
        }

        protected void BackUpSchedulerResourceDurationOverrideByProcedures()
        {
            string sql = string.Format(@"INSERT INTO SchedulerResourceDurationOverrideByProcedures_Audit (SchedulerModalityId, CodeReferenceId, ActualDuration, SedationTime,
AddLeadTime, IsActive, CreateUser, Version,CreateDate)
	SELECT
		srdobp.SchedulerModalityId,
		srdobp.CodeReferenceId,
		srdobp.ActualDuration,
		srdobp.SedationTime,
		srdobp.AddLeadTime,
		srdobp.IsActive,
		@createUser 'CreateUser',
		(SELECT
			COALESCE(MAX([Version]), 0) + 1
		FROM SchedulerResourceDurationOverrideByProcedures_Audit),
        dateadd(minute, {0}, getdate())
	FROM SchedulerResourceDurationOverrideByProcedures srdobp", GetTimeZonesDiff);
            int id = Convert.ToInt32(ExecuteScalar("SELECT u.ID FROM global.dbo.Users u where u.UserId=@userId", GlobalContext.RequestContext.UserName)); //C: by RJ Container is replaced with GlobalContext
            ExecuteNonQuery(sql, id);
        }

        protected void BackupCodeCategoryRestrictions()
        {
            const string sql =
        @"	INSERT INTO SchedulerModalities2SchedulerCodeCategories_Audit (SchedulerModalityId, CodeCategoryId, Version, CreateUser)
	SELECT  smscc.SchedulerModalityId,smscc.CodeCategoryId, (SELECT
			COALESCE(MAX([Version]), 0) + 1
		FROM SchedulerModalities2SchedulerCodeCategories_Audit
		), @createUser 'CreateUser' FROM SchedulerModalities2SchedulerCodeCategories smscc";

            ExecuteNonQuery(sql, GlobalContext.RequestContext.UserName);
        }

        protected void BackupNotificationSlotEntries()
        {
            const string sql =
                @"INSERT INTO SchedulerNotificationSlots_Audit (DayOfWeek, StartDate, StartTime, EndTime, ModalityId,
Comment, IsActive, Color, EndDate, Version, CreateUser)
	SELECT
		sns.DayOfWeek,sns.StartDate, sns.StartTime, sns.EndTime,sns.ModalityId,sns.Comment, sns.IsActive, sns.Color,sns.EndDate,(SELECT
			COALESCE(MAX([Version]), 0) + 1
		FROM SchedulerNotificationSlots_Audit
		),
		@createUser 'CreateUser'
	FROM SchedulerNotificationSlots sns";

            ExecuteNonQuery(sql, GlobalContext.RequestContext.UserName);
        }

        protected void BackupSchedulerAppointmentOrders(long orderId)
        {
            const string sql =
                @"INSERT INTO SchedulerAppointmentOrders_Audit (SchedulerOrderId, AppointmentId, AppointmentItemType, AppointmentItemId, PatientId,
LocationName, CPTCode, WorktypeDescription, ExamDescription, PhysicianId, Reason, Dictator, Priority, MultipleOrderId, OrderId,
DOS, AccountName, RecurringSeriesID, CC, Modality, Version, CreateUser)
	SELECT
		sao.SchedulerOrderId,
		sao.AppointmentId,
		sao.AppointmentItemType,
		sao.AppointmentItemId,
		sao.PatientId,
		sao.LocationName,
		sao.CPTCode,
		sao.WorktypeDescription,
		sao.ExamDescription,
		sao.PhysicianId,
		sao.Reason,
		sao.Dictator,
		sao.Priority,
		sao.MultipleOrderId,
		sao.OrderId,
		sao.DOS,
		sao.AccountName,
		sao.RecurringSeriesID,
		sao.CC,
		sao.Modality,
		(SELECT
			COALESCE(MAX([Version]), 0) + 1
		FROM SchedulerAppointmentOrders_Audit
		WHERE sao.SchedulerOrderId = @orderId)
		'Version',
		@createUser 'CreateUser'
	FROM SchedulerAppointmentOrders sao 
	WHERE sao.SchedulerOrderId = @orderId";

            ExecuteNonQuery(sql, orderId, GlobalContext.RequestContext.UserName);
        }

        protected void SendInsuranceMessageToE2(string patientMrn)
        {
            object res = null;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(_databaseConnection.ConnectionString))
                {
                    if (sqlConnection.State != ConnectionState.Open) sqlConnection.Open();
                    sqlConnection.ChangeDatabase(GlobalContext.RequestContext.Account);

                    using (SqlCommand sqlCommand = new SqlCommand())
                    {
                        sqlCommand.Connection = sqlConnection;
                        sqlCommand.CommandType = CommandType.Text;
                        sqlCommand.CommandText = "SELECT TOP 1 pat.AutoCount FROM PatientInfo pat WITH (NOLOCK) WHERE pat.Req = @mrn";
                        sqlCommand.Parameters.AddWithValue("@mrn", patientMrn);
                        res = sqlCommand.ExecuteScalar();
                    }
                }
            }
            catch (Exception)
            {
                // dont care if it fails
            }
            if (res != null)
                SendInsuranceMessageToE2(Convert.ToInt64(res));
        }

        protected void SendInsuranceMessageToE2(long patientId)
        {
            try
            {
                int E2SourceApplicationId = GlobalContext.ApplicationSetting.E2SourceApplicationId;
                using (SqlConnection sqlConnection = new SqlConnection(_databaseConnection.ConnectionString))
                {
                    if (sqlConnection.State != ConnectionState.Open) sqlConnection.Open();
                    sqlConnection.ChangeDatabase("esquared");

                    using (SqlCommand sqlCommand = new SqlCommand())
                    {
                        sqlCommand.Connection = sqlConnection;
                        sqlCommand.CommandType = CommandType.Text;
                        sqlCommand.CommandText = String.Format(@" INSERT INTO events 
                        (AccountName, JobId, EventTypeID, SourceApplicationID, CreateDateTime, LastUpdateDateTime, CurrentStateID)
                        Values(@accName,@jobid,@eventtId,@sourceAppid,dateadd(minute, {0}, getdate()),dateadd(minute, {0}, getdate()),@currentStateid)",
                            GetTimeZonesDiff);
                        sqlCommand.Parameters.AddWithValue("@accName", GlobalContext.RequestContext.Account);
                        sqlCommand.Parameters.AddWithValue("@eventtId", 420);
                        sqlCommand.Parameters.AddWithValue("@sourceAppid", E2SourceApplicationId);
                        sqlCommand.Parameters.AddWithValue("@currentStateid", 1);
                        sqlCommand.Parameters.AddWithValue("@jobid", patientId);
                        sqlCommand.ExecuteNonQuery();
                        sqlConnection.Close();
                    }
                }
            }
            catch (Exception)
            {
                // dont care if it fails
            }
        }

        protected void InsertBulkData<T>(string tableName, Dictionary<string, Type> columns, List<T> data, Action<DataRow, T> fillRowWithData)
        {
            DataTable table = new DataTable();
            foreach (KeyValuePair<string, Type> column in columns)
                table.Columns.Add(column.Key, column.Value);

            foreach (T item in data)
            {
                DataRow newRow = table.NewRow();
                fillRowWithData(newRow, item);
                table.Rows.Add(newRow);
            }

            using (SqlBulkCopy copier = ExecuteBulkCopy())
            {
                copier.DestinationTableName = tableName;
                copier.BatchSize = data.Count;
                copier.WriteToServer(table);
            }
        }

        public string ResolveNameByAccountId()
        {
            return ResolveNameByAccountId(this.CurrentAccountId);
        }

        public string ResolveNameByAccountId(long accountID)
        {
            lock (namesLocker)
            {
                String accountName = null;
                if (accountNames.ContainsKey(accountID))
                    accountName = accountNames[accountID];
                else
                {
                    SetConnection2Global();
                    accountName = ExecuteScalar("SELECT Account+'-'+UserId FROM ValidAccounts (NOLOCK) where ID = @aID", accountID) as String;
                    accountNames.Add(accountID, accountName);
                }
                if (accountName != null)
                    return accountName.Split('-')[0];
                return null;
            }
        }

        public long ResolveIdByAccountName(string accountName, string userName)
        {
            lock (namesLocker)
            {
                long accountID = -1;
                string auName = accountName + "-" + userName;
                if (accountNames.ContainsValue(auName))
                {
                    KeyValuePair<long, string> acc = accountNames.FirstOrDefault(a => a.Value == auName);
                    accountID = acc.Key;
                }
                else
                {
                    SetConnection2Global();
                    accountID = Convert.ToInt32(ExecuteScalar("SELECT ID FROM ValidAccounts (NOLOCK) where Account = @ac and UserId = @user",
                        accountName, userName));
                    if (accountID == 0)
                    {
                        throw new SchedulerException(SchedulerExceptionType.AccountIdByAccountNameAndUserNameNotFound);
                    }
                    else
                    {
                        accountNames.Add(accountID, auName);
                    }
                }
                return accountID;
            }
        }

        protected void SetConnection2Account(long accountID)
        {
            SetConnection2Account(ResolveNameByAccountId(accountID));
        }

        protected long CurrentAccountId
        {
            get
            {
                return GlobalContext.RequestContext.AccountId;
            }
        }

        protected string CurrentAccountName
        {
            get
            {
                return GlobalContext.RequestContext.Account;
            }
        }

        protected void SetConnection2Account()
        {
            SetConnection2Account(this.CurrentAccountId);
        }

        protected void SetConnection2Account(String accountName)
        {
            _databaseConnection.Connect2Account(accountName);
            _currentMode = ConnectionMode.Account;
        }


        protected void ExecuteNonQuery(string sql, params object[] param)
        {
            ExecuteNonQuery(sql, false, param);
        }

        protected void ExecuteNonQuery(string sql, bool parameterLessQuery, params object[] param)
        {
            using (SqlCommand cmd = InitDatabaseCommand(sql, parameterLessQuery, param))
            {
                cmd.ExecuteNonQuery();
            }
        }

        protected object ExecuteScalar(string sql, params object[] param)
        {
            using (SqlCommand cmd = InitDatabaseCommand(sql, param))
            {
                return cmd.ExecuteScalar();
            }
        }

        protected SqlBulkCopy ExecuteBulkCopy()
        {
            return _databaseConnection.InitCopier();
        }

        protected IDataReader ExecuteReader(string sql, params object[] param)
        {
            using (SqlCommand cmd = InitDatabaseCommand(sql, param))
            {
                return cmd.ExecuteReader();
            }
        }

        protected bool DetectDescQuery(string sortOrder)
        {
            return sortOrder == DESCENDING_SORT_STR;
        }

        protected SafeDataReader ExecuteSafeReader(string sql, params object[] param)
        {
            return new SafeDataReader(ExecuteReader(sql, param));
        }

        public object ToDbParam(DateTime? dateTime)
        {
            if (dateTime.HasValue)
            {
                DateTime d = dateTime.Value;
                if (d < SqlDateTime.MinValue.Value)
                    d = SqlDateTime.MinValue.Value.Date;
                if (d > SqlDateTime.MaxValue.Value)
                    d = SqlDateTime.MaxValue.Value.Date;

                return d;
            }
            else
                return DBNull.Value;
        }
    }

    public enum ConnectionMode
    {
        Global,
        Account,
        Insurance,
        Esquared
    }
}
