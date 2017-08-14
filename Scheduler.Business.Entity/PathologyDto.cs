using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Business.Entity
{
    public class PathologyDto
    {
        public PathologyDto()
        {
            Pathologies = new List<PathologyDetailDto>();
            Diagnoses = new List<PathologyDiagnosisDto>();
        }

        public PathologyDto(PathologyDto p)
        {
            Id = p.Id;
            Pathologies = new List<PathologyDetailDto>(p.Pathologies.Select(d => new PathologyDetailDto(d)));
            Diagnoses = new List<PathologyDiagnosisDto>(p.Diagnoses.Select(d => new PathologyDiagnosisDto(d)));
        }

        public long Id { get; set; }

        public List<PathologyDetailDto> Pathologies { get; set; }

        public List<PathologyDiagnosisDto> Diagnoses { get; set; }
    }
}
