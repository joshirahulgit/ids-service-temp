using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.DBEntity
{
    public class OrderTransformParameter : EntityBase
    {
        public int SchedulerConfigID { get; set; }
        public String MapFieldValue { get; set; }
        public String AccountWtValue { get; set; }
        public String MapFieldGroup { get; set; }
        public bool IsGroupPrompt { get; set; }
        public int? OverrideCreationMode { get; set; }

        //public static OrderTransformParameterDto Convert2Dto(OrderTransformParameter p)
        //{
        //    OrderTransformParameterDto result = new OrderTransformParameterDto();
        //    result.TransformId = (int)p.Id;
        //    result.SchedulerConfigID = p.SchedulerConfigID;
        //    result.MapFieldValue = p.MapFieldValue;
        //    result.AccountWtValue = p.AccountWtValue;
        //    result.MapFieldGroup = p.MapFieldGroup;
        //    result.IsGroupPrompt = p.IsGroupPrompt;
        //    result.OverrideCreationMode = p.OverrideCreationMode;
        //    return result;
        //}

    }
}
