using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Business.Specification
{
    public interface IEnterpriseService : IContract
    {
        String GetDecryptedURLQueryParameters(string accountName, string userId, string qpa, string qpb, string qpp, string cipdt);
    }
}
