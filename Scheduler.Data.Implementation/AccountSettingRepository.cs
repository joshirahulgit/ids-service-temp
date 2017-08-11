using Scheduler.Core;
using Scheduler.Data.Specification;
using Scheduler.DBEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Data.Implementation
{
    class AccountSettingRepository : DatabaseRepository, IAccountSettingRepository
    {
        public const string ApplicationName = "Scheduler.SL";

        public AccountSettingRepository(SchedulerDatabaseConnection dbConnection) : base(dbConnection)
        {
        }

        public AccountSetting GetById(long id)
        {
            SetConnection2Account();
            string sql = @"
SELECT [id]
      ,[Name]
      ,[Value]
      ,[Application]
      ,[IsActive]
      ,[CreateDate]
      ,[CreateUserLogin]
      ,[UpdateDate]
      ,[UpdateUserLogin]
  FROM [AccountSettings]  (NOLOCK) 
where Id = @id
";
            AccountSetting result = null;
            using (IDataReader reader = ExecuteReader(sql, id))
            {
                using (SafeDataReader sr = new SafeDataReader(reader))
                {
                    if (sr.Read())
                    {
                        result = (sr.ToAccountSetting());
                    }
                }
            }
            return result;
        }

        public AccountSetting GetByName(string name)
        {
            SetConnection2Account();
            string sql = @"
SELECT [id]
      ,[Name]
      ,[Value]
      ,[Application]
      ,[IsActive]
      ,[CreateDate]
      ,[CreateUserLogin]
      ,[UpdateDate]
      ,[UpdateUserLogin]
  FROM [AccountSettings]  (NOLOCK) 
where Application = @app and isActive = 1 and Name = @n
";
            AccountSetting result = null;
            using (IDataReader reader = ExecuteReader(sql, ApplicationName, name))
            {
                using (SafeDataReader sr = new SafeDataReader(reader))
                {
                    if (sr.Read())
                    {
                        result = (sr.ToAccountSetting());
                    }
                }
            }
            return result;
        }

        public AccountSetting Create(AccountSetting entity)
        {
            SetConnection2Account();
            string sql = @"
insert into AccountSettings ([Name], [Value], [Application], [IsActive], [CreateUserLogin])
values (@n, @v, @a, @ia, @u);
select SCOPE_IDENTITY();
";
            string user = entity.CreateUser;
            if (string.IsNullOrEmpty(user)) user = GlobalContext.RequestContext.UserName;
            var id = Convert.ToInt64(ExecuteScalar(sql,
                entity.Name,
                entity.Value,
                ApplicationName,
                entity.IsActive,
                user));
            SetNewId(typeof(AccountSetting), entity, id);
            return entity;
        }

        public void Remove(AccountSetting entity)
        {
            SetConnection2Account();
            string sql = string.Format(@"update AccountSettings 
set IsActive = 0,
    UpdateUserLogin = @u,
    UpdateDate = dateadd(minute, {0}, getdate())
where id = @id", GetTimeZonesDiff);
            string user = entity.UpdateUser;
            if (string.IsNullOrEmpty(user))
                user = GlobalContext.RequestContext.UserName;
            ExecuteNonQuery(sql, user, entity.Id);
        }

        public void Update(AccountSetting entity)
        {
            SetConnection2Account();
            string sql = string.Format(@"update AccountSettings 
set 
    Name = @n,
    Value = @v,
    IsActive = @ia,
    UpdateUserLogin = @u,
    UpdateDate = dateadd(minute, {0}, getdate())
where id = @id", GetTimeZonesDiff);
            string user = entity.UpdateUser;
            if (string.IsNullOrEmpty(user))
                user = GlobalContext.RequestContext.UserName;
            ExecuteNonQuery(sql, entity.Name, entity.Value, entity.IsActive, user, entity.Id);
        }

        private List<AccountSetting> DoGetAll()
        {
            string sql = @"
SELECT [id]
      ,[Name]
      ,[Value]
      ,[Application]
      ,[IsActive]
      ,[CreateDate]
      ,[CreateUserLogin]
      ,[UpdateDate]
      ,[UpdateUserLogin]
  FROM [AccountSettings]  (NOLOCK) 
where Application = @app --and isActive = 1
";
            List<AccountSetting> result = new List<AccountSetting>();
            using (IDataReader reader = ExecuteReader(sql, ApplicationName))
            {
                using (SafeDataReader sr = new SafeDataReader(reader))
                {
                    while (sr.Read())
                    {
                        result.Add(sr.ToAccountSetting());
                    }
                }
            }
            return result;
        }

        public List<AccountSetting> GetAll()
        {
            SetConnection2Account();
            return DoGetAll();
        }

        public List<AccountSetting> GetAll(long accountId)
        {
            SetConnection2Account(accountId);
            return DoGetAll();
        }

    }
}
