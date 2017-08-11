using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.DBEntity
{
    public class UserRole : EntityBase
    {
        public AccessControlList Acl { get; private set; }
        public string Name { get; private set; }
        public bool CanBeDeleted { get; set; }

        public UserRole()
        {
            Acl = new AccessControlList();
        }

        public UserRole(long id, string name) : this()
        {
            Id = id;
            Name = name;
        }

        //public static UserRoleDto Convert2Dto(UserRole role)
        //{
        //    UserRoleDto result = new UserRoleDto(AccessControlList.Convert2Dto(role.Acl), role.Name, role.Id, role.CanBeDeleted);
        //    return result;
        //}

        //public static UserRole ExtractFromDto(UserRoleDto role)
        //{
        //    UserRole result = new UserRole();
        //    result.Id = role.Id;
        //    result.CanBeDeleted = role.CanBeDeleted;
        //    result.Name = role.Name;
        //    result.Acl = AccessControlList.ExtractFromDto(role.Acl);
        //    return result;
        //}

        public void AssignId(int newId)
        {
            Id = newId;
        }

        //public void Update(RepositoryLocator locator, long accountId, UserRole newRole)
        //{
        //    if (newRole.Name != Name)
        //    {
        //        Name = newRole.Name;
        //        locator.UserRepository.UpdateUserRole(accountId, this);
        //    }
        //    foreach (AccessControlEntry entry in this.Acl.Entries)
        //    {
        //        if (newRole.Acl.Entries.Count(a => a.Id == entry.Id) == 0)
        //            locator.UserRepository.RemoveEntryFromRole(accountId, this.Id, entry.Id);
        //    }
        //    foreach (AccessControlEntry entry in newRole.Acl.Entries)
        //    {
        //        if (this.Acl.Entries.Count(a => a.Id == entry.Id) == 0)
        //            locator.UserRepository.InsertEntryToRole(accountId, this.Id, entry);
        //        else if (entry.DiffersFrom(this.Acl.Entries.FirstOrDefault(a => a.Id == entry.Id)))
        //            locator.UserRepository.UpdateEntryInRole(accountId, this.Id, entry);
        //    }
        //    this.Acl.Entries.Clear();
        //    this.Acl.Entries.AddRange(newRole.Acl.Entries);
        //}

        public void SetName(string name)
        {
            this.Name = name;
        }
    }
}
