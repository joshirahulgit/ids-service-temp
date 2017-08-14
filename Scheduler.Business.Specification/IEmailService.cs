using Scheduler.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Business.Specification
{
    public interface IEmailService : IContract
    {
        String GetEmailHTMLTemplate(EmailTemplate template);

        byte[] GetEmailRTFTemplate(EmailTemplate template);

        void SendEmail(List<String> to, List<String> copies, String from, String subject, String body, Dictionary<string, byte[]> attachmentBinaries);
    }
}
