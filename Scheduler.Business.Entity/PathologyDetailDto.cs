using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Business.Entity
{
    public class PathologyDetailDto
    {
        public PathologyDetailDto()
        {
        }

        public PathologyDetailDto(PathologyDetailDto d)
        {
            Id = d.Id;
            PathResult = d.PathResult;
            PathDetails = d.PathDetails;
        }

        public long Id { get; set; }

        public string PathResult { get; set; }

        public string ResultStatus { get; set; }

        public string PathDetails { get; set; }
    }


    public class PathologyDetailsDto : DtoBase
    {
        public List<PathologyDetailDto> Entries { get; set; }

        public PathologyDetailsDto()
        {
            Entries = new List<PathologyDetailDto>();
        }
    }
}
