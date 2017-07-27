using Scheduler.Core;
using System;

namespace Scheduler.Application
{
    internal class RequestContext : IRequestContext
    {
        private string account;
        private long accountId;
        private string clientTimeZone;
        private string password;
        private object sessionToken;
        private string userComputerIP;
        private string userName;

        public string Account => account;

        public long AccountId => accountId;

        public string ClientTimeZone => clientTimeZone;

        public string Password => password;

        public object SessionToken => sessionToken;

        public string UserComputerIP => userComputerIP;

        public string UserName => userName;

        internal RequestContext(String user, String pwd, long accountId, String account, object sessionToken, string userComputerIP, string clientTimeZone)
        {
            this.userName = user;
            this.password = pwd;
            this.accountId = accountId;
            this.account = account;
            this.sessionToken = sessionToken;
            this.userComputerIP = userComputerIP;
            this.clientTimeZone = clientTimeZone;
        }
    }
}
