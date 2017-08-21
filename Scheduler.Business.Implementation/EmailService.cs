using Scheduler.Business.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scheduler.Core;

namespace Scheduler.Business.Implementation
{
    public class EmailService : ServiceBase, IEmailService
    {
        public string GetEmailHTMLTemplate(EmailTemplate template)
        {
            throw new NotImplementedException();
        }

        public byte[] GetEmailRTFTemplate(EmailTemplate template)
        {
            throw new NotImplementedException();
        }

        public void SendEmail(List<string> to, List<string> copies, string from, string subject, string body, Dictionary<string, byte[]> attachmentBinaries)
        {
            throw new NotImplementedException();
        }
    }
}
