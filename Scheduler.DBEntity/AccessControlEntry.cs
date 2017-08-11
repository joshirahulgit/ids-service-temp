using Scheduler.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.DBEntity
{
    public class AccessControlEntry : EntityBase
    {

        public string Name { get; set; }
        public Permission Create { get; set; }
        public Permission Update { get; set; }
        public Permission Read { get; set; }
        public Permission Delete { get; set; }
        public bool IsInherit { get; set; }

        public AccessControlEntry(AccessControlEntry entry)
        {
            Id = entry.Id;
            Name = entry.Name;
            Create = entry.Create;
            Update = entry.Update;
            Read = entry.Read;
            Delete = entry.Delete;
        }

        public AccessControlEntry()
        {
        }

        public void Merge(AccessControlEntry entry)
        {
            this.Create = MergePermission(this.Create, entry.Create);
            this.Read = MergePermission(this.Read, entry.Read);
            this.Update = MergePermission(this.Update, entry.Update);
            this.Delete = MergePermission(this.Delete, entry.Delete);
        }

        private Permission MergePermission(Permission a, Permission b)
        {
            if (a == Permission.Denied || b == Permission.Denied) return Permission.Denied;//Deny superseeds all
            if (a == Permission.Allowed || b == Permission.Allowed) return Permission.Allowed;//no denied left, Allowed is more then undefined
            return Permission.Undefined;
        }

        public override string ToString()
        {
            List<string> permissions = new List<string>(4);
            if (Create != Permission.Undefined) permissions.Add(Create == Permission.Allowed ? "Create" : "Can't create");
            if (Read != Permission.Undefined) permissions.Add(Read == Permission.Allowed ? "Read" : "Can't read");
            if (Update != Permission.Undefined) permissions.Add(Update == Permission.Allowed ? "Update" : "Can't update");
            if (Delete != Permission.Undefined) permissions.Add(Delete == Permission.Allowed ? "Delete" : "Can't delete");
            return string.Format("{0} {1} ({2})", Id, Name, string.Join(",", permissions));
        }

        public bool DiffersFrom(AccessControlEntry entry)
        {
            return (entry == null) || (Id != entry.Id) || (Read != entry.Read) || (Create != entry.Create) || (Update != entry.Update) || (Delete != entry.Delete);
        }

        public void Grant(Permission create, Permission read, Permission update, Permission delete)
        {
            Create = create;
            Read = read;
            Update = update;
            Delete = delete;
        }

        public static AccessControlEntry Inherit(AccessControlEntry entry)
        {
            AccessControlEntry newEntry = new AccessControlEntry(entry);
            newEntry.IsInherit = true;
            return newEntry;
        }
    }
}
