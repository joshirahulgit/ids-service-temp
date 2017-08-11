using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.DBEntity
{
    public class AppointmentCheckListValue : EntityBase
    {
        public long AppointmentId { get; set; }

        public AppointmentCheckListItem Item { get; set; }

        public bool Value { get; set; }

        public List<Task> Tasks { get; set; }

        public AppointmentCheckListValue()
        {
            Tasks = new List<Task>();
        }

        //public static AppointmentCheckListValueDto Convert2Dto(AppointmentCheckListValue item)
        //{
        //    return new AppointmentCheckListValueDto()
        //    {
        //        Id = item.Id,
        //        AppointmentId = item.AppointmentId,
        //        Item = item.Item == null ? null : AppointmentCheckListItem.Convert2Dto(item.Item),
        //        Value = item.Value,
        //        Tasks = item.Tasks?.Select(t => Task.Convert2Dto(t)).ToList()
        //    };
        //}

        //public static AppointmentCheckListValue ExtractFromDto(AppointmentCheckListValueDto item)
        //{
        //    return new AppointmentCheckListValue()
        //    {
        //        Id = item.Id,
        //        AppointmentId = item.AppointmentId,
        //        Item = item.Item == null ? null : AppointmentCheckListItem.ExtractFromDto(item.Item),
        //        Value = item.Value,
        //        Tasks = item.Tasks?.Select(t => Task.ExtractFromDto(t)).ToList()
        //    };
        //}

        public void SetAppointmentId(long appointmentId)
        {
            AppointmentId = appointmentId;
        }
    }
}
