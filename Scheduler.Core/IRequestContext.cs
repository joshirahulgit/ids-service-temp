using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Core
{
    public interface IRequestContext //TODO by RJ, not sure about this, probably we dont need it here.: IExtension<OperationContext>
    {
        //IWarningsNotifier Notifier { get; }
        String UserName { get; }
        String Password { get; }
        String Account { get; }
        long AccountId { get; }
        Object SessionToken { get; }
        string UserComputerIP { get; }
        string ClientTimeZone { get; }
    }
}
