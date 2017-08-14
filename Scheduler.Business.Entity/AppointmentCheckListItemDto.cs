using Scheduler.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Business.Entity
{
    public class AppointmentCheckListItemDto : DtoBase
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public TaskTemplateDto Template { get; set; }
    }

    public class AppointmentCheckListValueDto : DtoBase
    {
        public AppointmentCheckListValueDto()
        {
            Tasks = new List<TaskDto>();
        }

        public AppointmentCheckListValueDto(AppointmentCheckListItemDto item) : this()
        {
            this.Item = item;
        }

        public long Id { get; set; }

        public long AppointmentId { get; set; }

        public AppointmentCheckListItemDto Item { get; set; }

        public bool Value { get; set; }

        public List<TaskDto> Tasks { get; set; }

        public TaskDto DeletedTask
        {
            get { return Select(t => t.IsDeleted); }
        }

        public TaskDto PendingTask
        {
            get { return Select(t => !t.IsDeleted && t.Status == EhrTaskStatus.Pending); }
        }

        public TaskDto InProgressTask
        {
            get { return Select(t => !t.IsDeleted && t.Status == EhrTaskStatus.InProgress); }
        }

        public TaskDto CompletedTask
        {
            get { return Select(t => !t.IsDeleted && t.Status == EhrTaskStatus.Completed); }
        }

        private TaskDto Select(Func<TaskDto, bool> func)
        {
            if (Tasks == null)
            {
                return null;
            }
            var tasks = Tasks.Where(func).ToList();
            if (tasks.Count == 0)
            {
                return null;
            }
            else
            {
                return tasks.OrderByDescending(t => t.CreatedDate).First();
            }
        }
    }

    public class AppointmentCheckListValuesDto : DtoBase
    {
        public List<AppointmentCheckListValueDto> Values { get; set; }
    }
}
