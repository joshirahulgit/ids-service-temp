using Scheduler.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Application
{
    internal class ApplicationSetting : IApplicationSetting
    {
        private int _DBAgeInPatchesApplicableForAutoPatching;
        private int _E2SourceApplicationId;
        private string _CS;
        private string _CSRO;

        public int DBAgeInPatchesApplicableForAutoPatching => _DBAgeInPatchesApplicableForAutoPatching;

        public int E2SourceApplicationId => _E2SourceApplicationId;

        public string CS => _CS;

        public string CSRO => _CSRO;

        internal ApplicationSetting(int dBAgeInPatchesApplicableForAutoPatching, int e2SourceApplicationId, string cs, string csro)
        {
            this._DBAgeInPatchesApplicableForAutoPatching = dBAgeInPatchesApplicableForAutoPatching;
            this._E2SourceApplicationId = e2SourceApplicationId;
            this._CS = cs;
            this._CSRO = csro;
        }
    }
}
