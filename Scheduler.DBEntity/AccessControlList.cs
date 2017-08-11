using Scheduler.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.DBEntity
{
    public class AccessControlList : EntityBase
    {
        public List<AccessControlEntry> Entries { get; private set; }

        public AccessControlList()
        {
            Entries = new List<AccessControlEntry>();
        }

        public AccessControlList(AccessControlList acl) : this()
        {
            foreach (AccessControlEntry entry in acl.Entries)
            {
                Entries.Add(new AccessControlEntry(entry));
            }
        }

        public void MergeEntry(AccessControlEntry entry)
        {
            AccessControlEntry existing = Entries.FirstOrDefault(a => a.Id == entry.Id);
            if (existing == null)
                Entries.Add(entry);
            else existing.Merge(entry);
        }

        public void MergeList(List<AccessControlEntry> entries)
        {
            if (entries == null) return;
            foreach (AccessControlEntry accountEntry in entries)
            {
                this.MergeEntry(accountEntry);
            }
        }

        public void GrantAllEntries(Permission permission)
        {
            foreach (AccessControlEntry entry in Entries)
            {
                entry.Grant(permission, permission, permission, permission);
            }
        }
    }
}
