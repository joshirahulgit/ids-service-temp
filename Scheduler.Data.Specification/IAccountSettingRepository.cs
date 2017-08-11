using Scheduler.DBEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Data.Specification
{
    public interface IAccountSettingRepository : IRepository,
        ICanGetById<AccountSetting, long>,
        ICanCreate<AccountSetting>,
        ICanRemove<AccountSetting>,
        ICanUpdate<AccountSetting>,
        ICanGetAll<AccountSetting>
    {
        AccountSetting GetByName(string name);
        List<AccountSetting> GetAll(long accountId);
    }
}
