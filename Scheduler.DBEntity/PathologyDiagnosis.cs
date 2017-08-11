using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.DBEntity
{
    public class PathologyDiagnosis : EntityBase
    {
        public string Code { get; set; }
        public string ShortDesc { get; set; }
        public string CodeType { get; set; }
        public bool IsDeleted { get; set; }

        //public static PathologyDiagnosis ExtractFromDto(PathologyDiagnosisDto diag)
        //{
        //    PathologyDiagnosis r = new PathologyDiagnosis();
        //    r.Id = diag.Id;
        //    r.Code = diag.Code;
        //    r.CodeType = diag.CodeType;
        //    r.IsDeleted = diag.IsDeleted;
        //    r.ShortDesc = diag.ShortDesc;
        //    return r;
        //}

        protected bool Equals(PathologyDiagnosis other)
        {
            return string.Equals(Code, other.Code) && string.Equals(ShortDesc, other.ShortDesc) && string.Equals(CodeType, other.CodeType) && IsDeleted.Equals(other.IsDeleted);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((PathologyDiagnosis)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = (Code != null ? Code.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (ShortDesc != null ? ShortDesc.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (CodeType != null ? CodeType.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ IsDeleted.GetHashCode();
                return hashCode;
            }
        }

        //public static PathologyDiagnosisDto Convert2Dto(PathologyDiagnosis dto)
        //{
        //    PathologyDiagnosisDto r = new PathologyDiagnosisDto();

        //    r.Id = dto.Id;
        //    r.Code = dto.Code;
        //    r.CodeType = dto.CodeType;
        //    r.IsDeleted = dto.IsDeleted;
        //    r.ShortDesc = dto.ShortDesc;
        //    return r;
        //}

        //public void Create(RepositoryLocator locator, long mpId)
        //{
        //    Id = locator.MammographyRepository.CreatePathologyDiagnosis(mpId, this);
        //}
    }
}
