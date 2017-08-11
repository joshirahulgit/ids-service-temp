using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Business.Entity
{
    public class PdfFileDto : DtoBase
    {
        public byte[] PdfFileBytes { get; set; }
    }

    public class PdfFilesDto : DtoBase
    {
        public List<PdfFileDto>[] PdfFiles { get; set; }
    }
}
