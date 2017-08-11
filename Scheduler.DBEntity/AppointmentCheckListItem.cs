using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.DBEntity
{
    public class AppointmentCheckListItem : EntityBase
    {
        public string Name { get; set; }
        public TaskTemplate Template { get; set; }

        //public static AppointmentCheckListItemDto Convert2Dto(AppointmentCheckListItem item)
        //{
        //    return new AppointmentCheckListItemDto()
        //    {
        //        Id = item.Id,
        //        Name = item.Name,
        //        Template = item.Template == null ? null : TaskTemplate.Convert2Dto(item.Template)
        //    };
        //}


        //public static AppointmentCheckListItem ExtractFromDto(AppointmentCheckListItemDto item)
        //{
        //    return new AppointmentCheckListItem()
        //    {
        //        Id = item.Id,
        //        Name = item.Name,
        //        Template = item.Template == null ? null : TaskTemplate.ExtractFromDto(item.Template)
        //    };
        //}

    }
}
