using Scheduler.Data.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scheduler.DBEntity;
using System.Data;
using Scheduler.Core;

namespace Scheduler.Data.Implementation
{
    internal class AccountRepository : DatabaseRepository, IAccountRepository
    {
        public AccountRepository(SchedulerDatabaseConnection dbConnection) : base(dbConnection)
        {
        }

        public List<Account> FindAllAccounts()
        {
            SetConnection2Global();

            //List<long> accountIds  = new List<long>();
            List<Account> accounts = new List<Account>();
            String sql = @"select id,Account from ValidAccounts (nolock) where UserId = @user /*and Admin = 1*/ AND Account in (SELECT name FROM sys.databases (NOLOCK))";
            using (IDataReader reader = ExecuteReader(sql, GlobalContext.RequestContext.UserName)) //C: By RJ Container has to be replaced with GlobalContext
            {
                using (SafeDataReader sr = new SafeDataReader(reader))
                {
                    while (sr.Read())
                    {
                        accounts.Add(new Account(Convert.ToInt64(sr.GetDecimal(0)), sr.GetNullableString(1)));
                    }
                }
            }
            return accounts;
        }

        public List<AccountEnum> GetAccountEnumsByType(string enumType)
        {
            SetConnection2Account();

            return LoadAccountEnumsByType(enumType);
        }

        public List<AccountEnum> InsertUpdateAccountEnum(List<AccountEnum> accEnums)
        {
            SetConnection2Account();
            String sql;
            foreach (AccountEnum accEnum in accEnums)
            {
                if (accEnum.Id == -1)
                {
                    sql = @"
INSERT INTO [AccountEnums]
           ([Name]
           ,[Value]
           ,[EnumType]
           ,[IsVisible]
           ,[UserCanEdit]
           ,[UserCanDelete])
     VALUES
           (@Name
           ,@Value
           ,@EnumType
           ,@IsVisible
           ,@UserCanEdit
           ,@UserCanDelete)";
                    ExecuteNonQuery(sql, accEnum.Name, accEnum.Value, accEnum.EnumType, accEnum.IsVisible, accEnum.UserCanEdit, accEnum.UserCanDelete);
                }
                else
                {
                    sql =
                        @"
UPDATE [AccountEnums]
   SET [Name] = @Name
      ,[Value] = @Value
      ,[IsVisible] = @IsVisible
      ,[UserCanEdit] = @UserCanEdit
      ,[UserCanDelete] = @UserCanDelete
 WHERE Id = @Id";
                    ExecuteNonQuery(sql, accEnum.Name, accEnum.Value, accEnum.IsVisible, accEnum.UserCanEdit, accEnum.UserCanDelete, accEnum.Id);

                }
                if (accEnum.IsDefault)
                {
                    sql =
                        @"
If((Select COUNT (aed.EnumType) from [AccountEnumDefaults] aed where aed.EnumType = @EnumType) = 0)
BEGIN
INSERT INTO [AccountEnumDefaults]
           ([EnumType]
           ,[Name])
     VALUES
           (@EnumType
           ,@Name)
END
ELSE
BEGIN
UPDATE [AccountEnumDefaults]
   SET [Name] = @Name
 WHERE EnumType = @EnumType
END
";
                    ExecuteNonQuery(sql, accEnum.EnumType, accEnum.Name);

                }
            }

            List<AccountEnum> result = new List<AccountEnum>();

            sql = @"SELECT  ae.[id],ae.[Name],ae.[Value],ae.[EnumType],ae.[IsVisible], cast(case ISNULL(aed.[Name], 0) when '0' then 0 else 1 end as bit) as IsDefault,
ae.UserCanEdit, ae.UserCanDelete
FROM [AccountEnums] (NOLOCK) ae LEFT JOIN AccountEnumDefaults (NOLOCK) aed on
ae.EnumType = aed.EnumType and aed.Name = ae.Name
                    WHERE ae.[IsVisible]=1 AND ae.EnumType =@enumType order by Name asc";

            using (IDataReader reader = ExecuteReader(sql, accEnums[0].EnumType))
            {
                using (SafeDataReader sr = new SafeDataReader(reader))
                {
                    while (sr.Read())
                    {
                        //C: ToAccountEnum shold be used modified by RJ
                        result.Add(sr.ToAccountEnum());
                    }
                }
            }
            return result;

        }

        private List<AccountEnum> LoadAccountEnumsByType(string enumType)
        {
            List<AccountEnum> result = new List<AccountEnum>();

            String sql =
                @"SELECT  ae.[id],ae.[Name],ae.[Value],ae.[EnumType],ae.[IsVisible], cast(case ISNULL(aed.[Name], 0) when '0' then 0 else 1 end as bit) as IsDefault,
ae.UserCanEdit, ae.UserCanDelete
FROM [AccountEnums] (NOLOCK) ae LEFT JOIN AccountEnumDefaults (NOLOCK) aed on
ae.EnumType = aed.EnumType and aed.Name = ae.Name
WHERE [IsVisible]=1 AND ae.EnumType =@enumType ORDER BY [Value] ASC";

            using (IDataReader reader = ExecuteReader(sql, enumType))
            {
                using (SafeDataReader sr = new SafeDataReader(reader))
                {
                    while (sr.Read())
                    {
                        //By RJ: Replaced below commented line with extension call
                        //result.Add(AccountEnum.ExtractFromReader(sr));
                        result.Add(sr.ToAccountEnum());
                    }
                }
            }
            return result;
        }
    }
}
