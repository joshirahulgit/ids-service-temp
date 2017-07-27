using Scheduler.Business.Specification;
using System;
using System.Collections.Generic;

namespace Scheduler.Business.Implementation
{
    public class ServiceFactory<TService>
    {
        private static IDictionary<Type, Type> services;

        static ServiceFactory()
        {
            services = new Dictionary<Type, Type>();
            services.Add(typeof(IAccountService), typeof(AccountService));
        }

        public static TService CreateNew()
        {
            Type t = typeof(TService);
            return (TService)Activator.CreateInstance(services[t], false);
        }
    }
}
