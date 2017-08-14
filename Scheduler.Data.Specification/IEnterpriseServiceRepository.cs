using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Data.Specification
{
    public interface IEnterpriseServiceRepository : IRepository
    {
        string GetDecryptedURLQueryParameters(string accountName, string userId, string qpa, string qpb, string qpp, string cipdt);
        //CP: Fix, TODO: Not sure how to handle WCF eligibility report and what exactly this is doing.
        //string RunEligibilityVerification(WS.Request[] arrRequests);
        //BatchStatus IsBatchVerificationComplete(string processId);
        string GetLastRequestedDictator(string account, string patientRecordNumber);
    }
}
