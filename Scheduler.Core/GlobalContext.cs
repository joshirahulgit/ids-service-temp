using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Core
{
    public class GlobalContext
    {
        [ThreadStatic]
        private static IRequestContext _requestContext;

        private static IApplicationSetting _applicationSetting;

        public static IRequestContext RequestContext => _requestContext;

        public static IApplicationSetting ApplicationSetting => _applicationSetting;

        public static void Add(object context)
        {
            if (context is IRequestContext)
                _requestContext = context as IRequestContext;

            if (context is IApplicationSetting)
                _applicationSetting = context as IApplicationSetting;
        }
    }
}
