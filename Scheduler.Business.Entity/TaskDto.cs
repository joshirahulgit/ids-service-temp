using Scheduler.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Business.Entity
{
    public class TaskDto : DtoBase
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public EhrTaskStatus Status { get; set; }

        public string Description { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int Complete { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? CreatedDate { get; set; }

    }


    public class TasksDto : DtoBase
    {
        public List<TaskDto> Set { get; set; }

        public TasksDto()
        {
            this.Set = new List<TaskDto>();
        }

        public void Add(TaskDto p)
        {
            this.Set.Add(p);
        }

        public void AddRange(TasksDto set)
        {
            this.Set.AddRange(set.Set);
        }
    }
}
