using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Business.Entity
{
    public class PatientFamilyHistoryProblemDto : DtoBase
    {
        public int PfhpId { get; set; }

        public int PfmId { get; set; }

        public long PatientIntId { get; set; }

        public string RelnTypCode { get; set; }

        public string Relationship { get; set; }

        public string RelnStatusCode { get; set; }

        public string RelnStatus { get; set; }

        public int? AgeDeceased { get; set; }

        public int? AgeDiagnosed { get; set; }

        public int PmplId { get; set; }

        public string MedicalProblem { get; set; }

        public PatientFamilyHistoryProblemDto()
        { }

        public PatientFamilyHistoryProblemDto(PatientFamilyHistoryProblemDto o)
        {
            PfhpId = o.PfhpId;
            PfmId = o.PfmId;
            AgeDeceased = o.AgeDeceased;
            AgeDiagnosed = o.AgeDiagnosed;
            MedicalProblem = o.MedicalProblem;
            PatientIntId = o.PatientIntId;
            PmplId = o.PmplId;
            Relationship = o.Relationship;
            RelnStatus = o.RelnStatus;
            RelnStatusCode = o.RelnStatusCode;
            RelnTypCode = o.RelnTypCode;
        }
    }

    public class PatientFamilyHistoryProblemsDto : DtoBase
    {
        public List<PatientFamilyHistoryProblemDto> Problems { get; set; }
    }
}
