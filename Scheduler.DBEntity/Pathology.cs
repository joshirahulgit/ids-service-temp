using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.DBEntity
{
    public class Pathology : EntityBase
    {
        public List<PathologyDetail> Pathologies { get; set; }
        public List<PathologyDiagnosis> Diagnoses { get; set; }

        public Pathology()
        {
            Pathologies = new List<PathologyDetail>();
            Diagnoses = new List<PathologyDiagnosis>();
        }

        //public static Pathology ExtractFromDto(PathologyDto breast)
        //{
        //    Pathology p = new Pathology();
        //    p.Id = breast.Id;
        //    foreach (PathologyDiagnosisDto dto in breast.Diagnoses)
        //        p.Diagnoses.Add(PathologyDiagnosis.ExtractFromDto(dto));

        //    foreach (PathologyDetailDto dto in breast.Pathologies)
        //    {
        //        p.Pathologies.Add(PathologyDetail.ExtractFromDto(dto));
        //    }
        //    return p;
        //}

        protected bool Equals(Pathology other)
        {
            throw new NotImplementedException();
            //return !Common.Utils.Helpers.ListsDiffer(Pathologies, other.Pathologies) && !Common.Utils.Helpers.ListsDiffer(Diagnoses, other.Diagnoses);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Pathology)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Pathologies != null ? Pathologies.GetHashCode() : 0) * 397) ^ (Diagnoses != null ? Diagnoses.GetHashCode() : 0);
            }
        }

        //public static PathologyDto Convert2Dto(Pathology p)
        //{
        //    PathologyDto r = new PathologyDto();
        //    r.Id = p.Id;
        //    foreach (var dto in p.Diagnoses)
        //        r.Diagnoses.Add(PathologyDiagnosis.Convert2Dto(dto));

        //    foreach (var dto in p.Pathologies)
        //    {
        //        r.Pathologies.Add(PathologyDetail.Convert2Dto(dto));
        //    }
        //    return r;
        //}

        public void SetId(int id)
        {
            Id = id;
        }
    }
}
