using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.DBEntity
{
    public class Tumor : EntityBase
    {
        public string Laterality { get; set; }
        public string NodalStatus { get; set; }
        public string TumorSize { get; set; }
        public string BiopsyType { get; set; }
        public bool IsDeleted { get; set; }

        protected bool Equals(Tumor other)
        {
            return string.Equals(Laterality, other.Laterality) && string.Equals(NodalStatus, other.NodalStatus) &&
                   string.Equals(TumorSize, other.TumorSize) &&
                   string.Equals(BiopsyType, other.BiopsyType) && IsDeleted.Equals(other.IsDeleted);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Tumor)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = (NodalStatus != null ? NodalStatus.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Laterality != null ? Laterality.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (TumorSize != null ? TumorSize.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (BiopsyType != null ? BiopsyType.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ IsDeleted.GetHashCode();
                return hashCode;
            }
        }

        //public static Tumor ExtractFromDto(TumorDto dto)
        //{
        //    Tumor t = new Tumor();
        //    t.Id = dto.Id;
        //    t.BiopsyType = dto.BiopsyType;
        //    t.Laterality = dto.Laterality;
        //    t.NodalStatus = dto.NodalStatus;
        //    t.TumorSize = dto.TumorSize;
        //    return t;
        //}

        //public static TumorDto Convert2Dto(Tumor dto)
        //{
        //    TumorDto t = new TumorDto();
        //    t.Id = dto.Id;
        //    t.BiopsyType = dto.BiopsyType;
        //    t.Laterality = dto.Laterality;
        //    t.NodalStatus = dto.NodalStatus;
        //    t.TumorSize = dto.TumorSize;
        //    return t;
        //}
    }
}
