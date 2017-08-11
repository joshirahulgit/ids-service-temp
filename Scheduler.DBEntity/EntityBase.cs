using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scheduler.DBEntity
{
    public abstract class EntityBase : IEntity
    {
        public virtual long Id { get; set; }

        //TODO: This method is converting DBEntities to DTO, this makes DBEntities anf DTO coupled. This can be done via association in future. -By RJ
        //public ReflectableMobileType ReflectedConvert2Mobile<ReflectableMobileType>() where ReflectableMobileType : DtoBase
        //{
        //    var mobileProps = typeof(ReflectableMobileType).GetProperties();
        //    var businessProps = this.GetType().GetProperties();
        //    ReflectableMobileType result = Activator.CreateInstance(typeof(ReflectableMobileType)) as ReflectableMobileType;
        //    foreach (var businessProp in businessProps)
        //    {
        //        PropertyInfo mobileProp = mobileProps.FirstOrDefault(
        //            p => p.Name == businessProp.Name && p.PropertyType == businessProp.PropertyType && p.CanWrite);
        //        if (mobileProp != null)
        //        {
        //            mobileProp.SetValue(result, businessProp.GetValue(this, null), null);
        //        }
        //    }
        //    return result;
        //}

    }
}
