using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.DBEntity
{
    public class TaskTemplate : EntityBase
    {
        public string Name { get; set; }

        //public static TaskTemplateDto Convert2Dto(TaskTemplate template)
        //{
        //    return new TaskTemplateDto()
        //    {
        //        Id = template.Id,
        //        Name = template.Name,
        //    };
        //}

        //public static TaskTemplate ExtractFromDto(TaskTemplateDto template)
        //{
        //    return new TaskTemplate()
        //    {
        //        Id = template.Id,
        //        Name = template.Name,
        //    };
        //}
    }
}
