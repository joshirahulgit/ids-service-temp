using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.DBEntity
{
    public class PatientAdditionalData : EntityBase
    {
        public string PatientId { get; set; }
        public bool? HasBeenPregnant { get; set; }
        public int? AgeFirstMenstruation { get; set; }
        public int? AgeAtMenopause { get; set; }
        public int? AgeWhenOvariesRemoved { get; set; }
        public string MenopauseStatus { get; set; }
        public DateTime? DateOfLastMammogram { get; set; }
        public bool? FirstScreeningMammography { get; set; }
        public string LocationX { get; set; }
        public int? NoOfBirths { get; set; }
        public int? AgeAtFirstBirth { get; set; }
        public string GeneticTestResults { get; set; }
    }
}
