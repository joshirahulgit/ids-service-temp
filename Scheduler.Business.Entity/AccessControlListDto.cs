using Scheduler.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Business.Entity
{
    public class AccessControlListDto //: DtoBase
    {
        public List<AccessControlEntryDto> Entries { get; set; }

        public AccessControlListDto()
        {
            Entries = new List<AccessControlEntryDto>();
        }

        public AccessControlListDto(AccessControlListDto acl) : this()
        {
            foreach (AccessControlEntryDto item in acl.Entries)
                Entries.Add(new AccessControlEntryDto(item));
        }

        public bool CanRead(SchedulerSecuredEntities se)
        {
            AccessControlEntryDto entry = Entries.FirstOrDefault(a => a.Id == se);
            if (entry == null) return false;
            return entry.Read == Permission.Allowed;
        }

        public bool CanCreate(SchedulerSecuredEntities se)
        {
            AccessControlEntryDto entry = Entries.FirstOrDefault(a => a.Id == se);
            if (entry == null) return false;
            return entry.Create == Permission.Allowed;
        }

        public bool CanUpdate(SchedulerSecuredEntities se)
        {
            AccessControlEntryDto entry = Entries.FirstOrDefault(a => a.Id == se);
            if (entry == null) return false;
            return entry.Update == Permission.Allowed;
        }

        public bool CanDelete(SchedulerSecuredEntities se)
        {
            AccessControlEntryDto entry = Entries.FirstOrDefault(a => a.Id == se);
            if (entry == null) return false;
            return entry.Delete == Permission.Allowed;
        }

        public bool DiffersFrom(AccessControlListDto list)
        {
            if (list.Entries.Count != this.Entries.Count) return true;
            foreach (AccessControlEntryDto thisEntry in Entries)
            {
                AccessControlEntryDto entry = list.Entries.FirstOrDefault(e => e.Id == thisEntry.Id);
                if (entry == null) return true;
                if (thisEntry.DiffersFrom(entry)) return true;
            }
            return false;
        }

        public void GrantAllEntries(Permission permission)
        {
            foreach (AccessControlEntryDto entry in Entries)
            {
                entry.Create = permission;
                entry.Read = permission;
                entry.Update = permission;
                entry.Delete = permission;
            }
        }
    }
}
