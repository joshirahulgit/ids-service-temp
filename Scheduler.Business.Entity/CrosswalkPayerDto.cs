using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Business.Entity
{
    public class CrosswalkPayerDto : DtoBase
    {
        public CustomPayerDto LocalPayer { get; set; }
        public CustomPayerDto GlobalPayer { get; set; }
        public int Id { get; set; }
    }


    public class CrosswalkPayersDto : DtoBase
    {
        public CrosswalkPayersDto()
        {
            this.Payers = new List<CrosswalkPayerDto>();
        }

        public IList<CrosswalkPayerDto> Payers { get; set; }
    }
}
