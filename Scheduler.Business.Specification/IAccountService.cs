using Scheduler.Business.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Business.Specification
{
    public interface IAccountService
    {
        AccountEnumsDto GetAccountEnumsByType(string type);

        AccountEnumsDto InsertUpdateAccountEnum(AccountEnumsDto accEnums);
    }
}
