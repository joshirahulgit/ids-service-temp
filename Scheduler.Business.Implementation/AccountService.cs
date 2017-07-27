using Scheduler.Business.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scheduler.Business.Entity;
using Scheduler.Data.Implementation;
using Scheduler.DBEntity;

namespace Scheduler.Business.Implementation
{
    internal class AccountService : ServiceBase, IAccountService
    {
        public AccountEnumsDto GetAccountEnumsByType(string type)
        {
            return ExecuteReadOnlyCommand(locator => GetAccountEnumsByTypeCommand(locator, type));
        }

        public AccountEnumsDto InsertUpdateAccountEnum(AccountEnumsDto accEnums)
        {
            return ExecuteCommand(locator => InsertUpdateAccountEnumCommand(locator, accEnums));
        }

        private AccountEnumsDto InsertUpdateAccountEnumCommand(RepositoryLocator locator, AccountEnumsDto accEnums)
        {
            AccountEnumsDto result = new AccountEnumsDto();
            List<AccountEnum> accountEnums = new List<AccountEnum>();
            foreach (AccountEnumDto accountEnumDto in accEnums.AccountEnums)
                accountEnums.Add(accountEnumDto.ToAccountEnum());

            List<AccountEnum> listAccountEnum = locator.AccountRepository.InsertUpdateAccountEnum(accountEnums);
            foreach (AccountEnum accountEnum in listAccountEnum)
            {
                result.AccountEnums.Add(accountEnum.ToAccountEnumDto());
            }
            return result;
        }

        private AccountEnumsDto GetAccountEnumsByTypeCommand(RepositoryLocator locator, string type)
        {
            List<AccountEnum> codes = locator.AccountRepository.GetAccountEnumsByType(type);
            AccountEnumsDto result = new AccountEnumsDto();
            foreach (AccountEnum code in codes)
            {
                result.AccountEnums.Add(code.ToAccountEnumDto());
            }
            return result;
        }
    }
}
