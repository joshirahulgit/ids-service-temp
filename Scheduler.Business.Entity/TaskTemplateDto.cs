using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Business.Entity
{
    public class TaskTemplateDto : DtoBase
    {
        public long Id { get; set; }

        public string Name { get; set; }

        //just for back compatibility
        public List<TaskDto> Tasks { get; set; }
        public TaskDto DeletedTask => null;
        public TaskDto PendingTask => null;
        public TaskDto InProgressTask => null;
        public TaskDto CompletedTask => null;
    }

    public class TaskTemplatesDto : DtoBase
    {
        public List<TaskTemplateDto> Templates { get; set; }
    }
}
