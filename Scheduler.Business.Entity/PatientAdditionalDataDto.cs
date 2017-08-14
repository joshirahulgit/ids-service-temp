using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Business.Entity
{
    public class PatientAdditionalDataDto : DtoBase
    {
        public long Id { get; set; }

        public string PatientId { get; set; }

        public bool? HasBeenPregnant { get; set; }

        public bool HasBeenPregnantYes
        {
            get { return HasBeenPregnant == true; }
            set { HasBeenPregnant = value; }
        }
        public bool HasBeenPregnantNo
        {
            get { return HasBeenPregnant == false; }
            set { HasBeenPregnant = value; }
        }

        public int? AgeFirstMenstruation { get; set; }

        public int? AgeAtMenopause { get; set; }

        public int? AgeWhenOvariesRemoved { get; set; }

        public string MenopauseStatus { get; set; }

        public DateTime? DateOfLastMammogram { get; set; }

        public bool? FirstScreeningMammography { get; set; }

        public bool? FirstScreeningMammographyYes
        {
            get { return FirstScreeningMammography == true; }
            set { FirstScreeningMammography = value; }
        }
        public bool? FirstScreeningMammographyNo
        {
            get { return FirstScreeningMammography == false; }
            set { FirstScreeningMammography = value; }
        }

        public string LocationX { get; set; }

        public int? NoOfBirths { get; set; }

        public int? AgeAtFirstBirth { get; set; }

        public string GeneticTestResults { get; set; }
    }
}
