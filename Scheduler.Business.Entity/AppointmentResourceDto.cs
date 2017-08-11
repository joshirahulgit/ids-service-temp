using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Business.Entity
{
    public class AppointmentResourceDto //: DtoBase
    {
        public long Id { get; set; }

        public long TypeId { get; set; }

        public long AccountId { get; set; }


        public virtual string DisplayText
        {
            get { return ToString(); }
        }

        public AppointmentResourceDto()
        {

        }

        public AppointmentResourceDto(long typeID)
        {
            this.TypeId = typeID;
        }

        public virtual bool IsValid(AppointmentResourceDto resource)
        {
            return this.Id == resource.Id;
        }

        protected virtual AppointmentResourceDto DoClone()
        {
            throw new NotImplementedException();
        }

        protected virtual void DoCopy(AppointmentResourceDto dest)
        {
            throw new NotImplementedException();
        }

        public static bool TryFindById(List<AppointmentResourceDto> resources, AppointmentResourceDto resource, out AppointmentResourceDto found)
        {
            found = null;
            foreach (AppointmentResourceDto res in resources)
            {
                if (resource.GetType() == res.GetType() && res.Id == resource.Id)
                {
                    found = res;
                    return true;
                }
            }
            return false;
        }


        public static bool TryFind<T>(List<AppointmentResourceDto> resources, out T mod) where T : class
        {
            mod = null;
            foreach (AppointmentResourceDto res in resources)
            {
                if (res is T)
                {
                    mod = res as T;
                    return true;
                }
            }
            return false;
        }

        public static List<T> FindAll<T>(List<AppointmentResourceDto> resources) where T : class
        {
            List<T> ret = new List<T>();
            foreach (AppointmentResourceDto res in resources)
            {
                if (res is T)
                {
                    ret.Add(res as T);
                }
            }
            return ret;
        }

        #region ICloneable Members

        public object Clone()
        {
            return DoClone();
        }

        public void CopyTo(AppointmentResourceDto reg)
        {
            DoCopy(reg);
        }

        #endregion
    }

    public class AppointmentSourcesDto //: DtoBase
    {
        public IList<AppointmentResourceDto> Sources { get; set; }
    }

    public class ResourceIdComparer : IEqualityComparer<AppointmentResourceDto>
    {
        private static ResourceIdComparer _instance;

        public static ResourceIdComparer Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ResourceIdComparer();

                return _instance;
            }
        }

        #region IEqualityComparer<HolidayDto> Members

        public bool Equals(AppointmentResourceDto x, AppointmentResourceDto y)
        {
            return x.GetType() == y.GetType() && x.Id == y.Id && x.AccountId == y.AccountId;
        }

        public int GetHashCode(AppointmentResourceDto obj)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
