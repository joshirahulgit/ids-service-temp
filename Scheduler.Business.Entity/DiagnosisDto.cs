using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Business.Entity
{
    public class DiagnosisDto : CptBaseDto
    {
        public string Chronic { get; set; }
        public bool IsChronic { get; set; }

        public string StringCategory { get; set; }

        public DiagnosisDto(DiagnosisDto dto)
            : base(dto)
        {
        }

        public DiagnosisDto(long id, string code, string shortDescription, string mediumDescription, string longDescription, string globalId, bool isGlobal, string alertText, DateTime? onsetDate, string flag)
            : base(id, code, shortDescription, mediumDescription, longDescription, globalId, isGlobal)
        {
            AlertText = alertText;
            OnsetDate = onsetDate;
            Flag = flag;
        }
    }

    public class DiagnosesDto : DtoBase
    {
        public DiagnosesDto()
        {
            Diagnoses = new List<DiagnosisDto>();
        }

        public DiagnosesDto(IList<DiagnosisDto> diagnoses)
        {
            Diagnoses = diagnoses;
        }

        public IList<DiagnosisDto> Diagnoses { get; set; }

        public bool ContainsDiagnosis(DiagnosisDto diag)
        {
            if (this.Diagnoses == null)
                return false;

            foreach (DiagnosisDto a in this.Diagnoses)
                if (a.Id == diag.Id)
                    return true;

            return false;
        }

        public static bool Differ(List<DiagnosisDto> da, List<DiagnosisDto> db)
        {

            bool differ = da.Count != db.Count;
            if (!differ)
                foreach (DiagnosisDto a in da)
                {
                    differ = db.Any(b => b.Code == a.Code && b.ShortDescription == a.ShortDescription && a.IsGlobal == b.IsGlobal);
                    if (differ)
                        break;
                }
            return differ;
        }
    }
}
