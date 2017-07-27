using Scheduler.DBEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scheduler.Data.Specification
{
    public interface IAccountRepository : IRepository
    {
        List<Account> FindAllAccounts();

        List<AccountEnum> InsertUpdateAccountEnum(List<AccountEnum> accEnums);

        List<AccountEnum> GetAccountEnumsByType(string type);
    }
}
