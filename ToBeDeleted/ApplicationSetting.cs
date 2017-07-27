using Scheduler.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToBeDeleted
{
    internal class ApplicationSetting : IApplicationSetting
    {
        private int _DBAgeInPatchesApplicableForAutoPatching;
        private int _E2SourceApplicationId;
        private string _CS;

        public int DBAgeInPatchesApplicableForAutoPatching => _DBAgeInPatchesApplicableForAutoPatching;

        public int E2SourceApplicationId => _E2SourceApplicationId;

        public string CS => _CS;

        internal ApplicationSetting(int dBAgeInPatchesApplicableForAutoPatching, int e2SourceApplicationId ,string cs)
        {
            this._DBAgeInPatchesApplicableForAutoPatching = dBAgeInPatchesApplicableForAutoPatching;
            this._E2SourceApplicationId = e2SourceApplicationId;
            this._CS = cs;
        }
    }
}
