using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Business.Entity
{
    public class AccountEnumsDto //: DtoBase TODO: Not sure about Dto Base
    {
        public AccountEnumsDto()
        {
            this.AccountEnums = new List<AccountEnumDto>();
        }

        public List<AccountEnumDto> AccountEnums { get; set; }

    }
}
