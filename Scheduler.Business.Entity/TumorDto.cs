using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Business.Entity
{
    public class TumorDto
    {
        public TumorDto()
        {
        }

        public TumorDto(TumorDto t)
        {
            Id = t.Id;
            Laterality = t.Laterality;
            NodalStatus = t.NodalStatus;
            TumorSize = t.TumorSize;
            BiopsyType = t.BiopsyType;
            IsDeleted = t.IsDeleted;
        }

        public long Id { get; set; }

        public string Laterality { get; set; }

        public string NodalStatus { get; set; }

        public string TumorSize { get; set; }

        public string BiopsyType { get; set; }

        public bool IsDeleted { get; set; }

    }
}
