using Scheduler.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.DBEntity
{
    public class AppointmentResourceType : EntityBase
    {
        public AppointmentResourceType()
        {
            AppointmentSourceSet = new List<AppointmentResource>();
        }

        public AppointmentResourceType(long typeID)
            : this()
        {
            this.Id = typeID;
        }


        public AppointmentResourceType(long typeID, string typeName, bool filterable)
            : this(typeID)
        {
            this.TypeName = typeName;
            this.Filterable = filterable;
        }

        //public static AppointmentResourceType ExtractFromDto(AppointmentResourceTypeDto dto)
        //{
        //    AppointmentResourceType result = new AppointmentResourceType();
        //    result.Id = dto.Id;
        //    result.TypeName = dto.TypeName;
        //    result.Filterable = dto.Filterable;
        //    result.Account = new Account(dto.AccountId);
        //    return result;
        //}

        public virtual String TypeName { get; private set; }
        public virtual Account Account { get; private set; }
        public virtual List<AppointmentResource> AppointmentSourceSet { get; set; }
        public virtual Boolean Filterable { get; private set; }

        public static List<AppointmentResourceType> GetList()
        {
            List<AppointmentResourceType> list = new List<AppointmentResourceType>();

            list.Add(new AppointmentResourceType((long)ResourceTypes.Time, "Time", false));
            list.Add(new AppointmentResourceType((long)ResourceTypes.Room, "Modality", true));
            list.Add(new AppointmentResourceType((long)ResourceTypes.Physician, "Physician", true));
            list.Add(new AppointmentResourceType((long)ResourceTypes.Patient, "Patient", false));

            return list;
        }
    }
}
