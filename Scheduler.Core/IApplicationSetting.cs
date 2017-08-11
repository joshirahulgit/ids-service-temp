using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Core
{
    public interface IApplicationSetting
    {

        int E2SourceApplicationId { get; }
        int DBAgeInPatchesApplicableForAutoPatching { get; }
        string CS { get; }
        string CSRO { get; }
    }
}
