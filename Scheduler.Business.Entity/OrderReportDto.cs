using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Business.Entity
{
    public class OrderReportDto : DtoBase
    {
        public OrderReportDto()
        {

        }

        public OrderReportDto(OrderReportDto orderReportDto)
        {
            this.Account = orderReportDto.Account;
            this.FilePath = orderReportDto.FilePath;
            this.JobId = orderReportDto.JobId;
        }

        public string JobId { get; set; }

        public string FilePath { get; set; }

        public string Account { get; set; }

        public string Html { get; set; }
    }
}
