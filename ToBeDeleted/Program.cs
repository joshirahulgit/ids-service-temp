using Scheduler.Business.Implementation;
using Scheduler.Business.Specification;
using Scheduler.Core;
using System;

namespace ToBeDeleted
{
    class Program
    {
        private static String cs = @"Server=172.31.25.70;uid=sa;pwd=stagingsa;Trusted_Connection=False;Application Name=MachinesLocalDebuggingApplication;";

        static void Main(string[] args)
        {
            IRequestContext rc = new RequestContext("gdeleon", "!password!", 92749, "solismammotest", null, "", "");
            IApplicationSetting appSet = new ApplicationSetting(100, 4, cs);
            GlobalContext.Add(rc);
            GlobalContext.Add(appSet);

            IAccountService accSer = ServiceFactory<IAccountService>.CreateNew();

            var hcpcsEnums = accSer.GetAccountEnumsByType("HCPCS");

            //ITransactionFactory tFactory = new TransactionFactory(cs);

            ////Need to have request context to call this method
            //using (ITransactionManager manager = tFactory.CreateManager())
            //{
            //    var res = manager.ExecuteReadOnlyCommand<IAccountRepository>(Handler);
            //    //var accounts = res.FindAllAccounts();
            //}
        }

        

        //void checkDataImplementation()
        //{

        //}
    }
}
