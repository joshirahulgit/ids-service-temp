using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Business.Entity
{
    public class OrderTransformParameterDto : DtoBase
    {
        public int SchedulerConfigID { get; set; }

        public String MapFieldValue { get; set; }

        public String AccountWtValue { get; set; }

        public String MapFieldGroup { get; set; }

        public bool IsGroupPrompt { get; set; }

        public int? OverrideCreationMode { get; set; }

        public int TransformId { get; set; }
    }

    public class OrderTransformParametersDto : DtoBase
    {
        public OrderTransformParametersDto()
        {
            this.Params = new List<OrderTransformParameterDto>();
        }


        public IList<OrderTransformParameterDto> Params { get; set; }
    }
}
