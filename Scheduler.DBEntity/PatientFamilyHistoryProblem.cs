using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.DBEntity
{
    public class PatientFamilyHistoryProblem : EntityBase
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
    }
}
