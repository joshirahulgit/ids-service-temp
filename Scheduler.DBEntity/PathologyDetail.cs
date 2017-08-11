using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.DBEntity
{
    public class PathologyDetail : EntityBase
    {
        public string PathResult { get; set; }
        public string ResultStatus { get; set; }
        public string PathDetails { get; set; }

        //public static PathologyDetail ExtractFromDto(PathologyDetailDto dto)
        //{
        //    PathologyDetail r = new PathologyDetail();
        //    r.Id = dto.Id;
        //    r.PathDetails = dto.PathDetails;
        //    r.PathResult = dto.PathResult;
        //    r.ResultStatus = dto.ResultStatus;
        //    return r;
        //}

        protected bool Equals(PathologyDetail other)
        {
            return string.Equals(PathResult, other.PathResult) && string.Equals(ResultStatus, other.ResultStatus) && string.Equals(PathDetails, other.PathDetails);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((PathologyDetail)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hc = (PathResult != null ? PathResult.GetHashCode() : 0);
                hc = (hc * 397) ^ (ResultStatus != null ? ResultStatus.GetHashCode() : 0);
                hc = (hc * 397) ^ (PathDetails != null ? PathDetails.GetHashCode() : 0);
                return hc;
            }
        }

        //public static PathologyDetailDto Convert2Dto(PathologyDetail dto)
        //{
        //    PathologyDetailDto r = new PathologyDetailDto();
        //    r.Id = dto.Id;
        //    r.PathDetails = dto.PathDetails;
        //    r.PathResult = dto.PathResult;
        //    r.ResultStatus = dto.ResultStatus;
        //    return r;
        //}

        //public void Create(RepositoryLocator locator, long mpId)
        //{
        //    Id = locator.MammographyRepository.CreatePathologyResult(mpId, this);

        //}
    }
}
