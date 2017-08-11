using Scheduler.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Business.Entity
{
    public class AccessControlEntryDto //: DtoBase
    {
        public string Name { get; set; }
        public Permission Create { get; set; }
        public Permission Update { get; set; }
        public Permission Read { get; set; }
        public Permission Delete { get; set; }

        //2014-05-30. Grety mod. This trick allows to avoid exceptions serializing Enum values which are not defined in C#. 
        //This happens when new SecuredEntites are read from database by old version running over same DB
        public SchedulerSecuredEntities Id { get { return (SchedulerSecuredEntities)LongId; } set { LongId = (long)value; } }
        public long LongId { get; set; }

        public bool IsInherit { get; set; }

        public AccessControlEntryDto()
        {
        }

        public AccessControlEntryDto(AccessControlEntryDto entry)
        {
            Name = entry.Name;
            Id = entry.Id;
            Create = entry.Create;
            Read = entry.Read;
            Update = entry.Update;
            Delete = entry.Delete;
            IsInherit = entry.IsInherit;
        }

        public override string ToString()
        {
            List<string> permissions = new List<string>(4);
            if (Create != Permission.Undefined) permissions.Add(Create == Permission.Allowed ? "Create" : "Can't create");
            if (Read != Permission.Undefined) permissions.Add(Read == Permission.Allowed ? "Read" : "Can't read");
            if (Update != Permission.Undefined) permissions.Add(Update == Permission.Allowed ? "Update" : "Can't update");
            if (Delete != Permission.Undefined) permissions.Add(Delete == Permission.Allowed ? "Delete" : "Can't delete");
            return string.Format("{0} ({1})", Id, string.Join(",", permissions.ToArray()));
        }

        public bool DiffersFrom(AccessControlEntryDto entry)
        {
            return (entry == null) || (Id != entry.Id) || (Read != entry.Read) || (Create != entry.Create) || (Update != entry.Update) || (Delete != entry.Delete) || (IsInherit != entry.IsInherit);
        }
    }
}
