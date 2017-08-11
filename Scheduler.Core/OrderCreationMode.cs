using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Core
{
    public enum OrderCreationMode
    {
        NotSpecified = 0,
        ManualOneOne = 1,
        AutoOneOne = 2,
        AutoOneMany = 3,
        ManualOneMany = 5
    }
}
