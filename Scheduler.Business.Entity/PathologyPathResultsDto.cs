using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Business.Entity
{
    public class PathologyPathResultsDto
    {
        public long Id { get; set; }

        public string Code { get; set; }

        public string Description { get; set; }

        public override string ToString()
        {
            return Description;
        }
    }

    public class PathologyPathsResultsDto : DtoBase
    {
        public IList<PathologyPathResultsDto> Entries { get; set; }

        public PathologyPathsResultsDto()
        {
            Entries = new List<PathologyPathResultsDto>();
        }
    }
}
