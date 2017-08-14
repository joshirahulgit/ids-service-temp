using Scheduler.DBEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Data.Specification
{
    public interface IUserRepository : IRepository, ICanGetById<User, long>, ICanCreate<User>, ICanRemove<User>, ICanUpdate<User>
    {
        User FindUser(string user, string password);
        List<User> FindAllUsers(List<Account> availableAccounts);
        List<User> FindAllUsers(long accountId);
        AccessControlList LoadUserAcl(long accountId);
        List<long> FindUserRoles(long accountId);
        List<long> FindUserRolesByUserId(int userId, string account);
        void RemoveRolesFromUser(long accountId, List<long> roleIds);
        void AddRolesToUser(long accountId, List<long> roleIds);
        AccessControlList GetAllSecuredEntities();
        List<UserRole> GetAllUserRoles(long accountId);
        void RemoveUserRole(long accountId, long roleId);
        UserRole InsertUserRole(long accountId, UserRole role);
        void UpdateUserRole(long accountId, UserRole role);
        void RemoveEntryFromRole(long accountId, long roleId, long entryId);
        void InsertEntryToRole(long accountId, long roleId, AccessControlEntry entry);
        void UpdateEntryInRole(long accountId, long id, AccessControlEntry entry);
        UserRole GetUserRoleById(long accId, int roleId);
        UserRole GetUserRoleByName(long accId, string name);
        User GetUserByLogin(string userName);
        UserProfile AddProfileToUserXML(User user, UserProfile newProfile);
        UserProfile AddProfileToUserNew(UserProfile newProfile);
        UserProfile LoadUserProfileXML(string user, long accountId);
        UserProfile LoadDefaultUserProfileNew(string userName, long accountId);
        UserProfile LoadDefaultUserProfileNew(long userId, long accountId);
        UserProfile LoadUserProfileNew(int id);
        UserProfile UpdateProfileXML(User user, UserProfile profile);
        UserProfile UpdateProfileNew(UserProfile profile);
        List<UserProfile> LoadUserProfilesXML(string userName, long accountId);
        List<UserProfile> LoadUserProfilesXML(int userName);
        List<UserProfile> LoadUserProfilesNew(int? userId, List<long> roleIds, long accountId, bool loadDetails);
        List<UserProfile> LoadUserProfilesNew(string userName, List<long> roleIds, long accountId, bool loadDetails);
        void AssignDefaultProfileXML(int userId, int profileId);
        void AssignDefaultProfileNew(int userId, int profileId);
        void RemoveAllUserProfileXML(long accountId, long userId);
        void RemoveAllUserProfileNew(long accountId, long userId);
        void DeleteUserProfileXML(int profileId);
        void DeleteUserProfileNew(int profileId);
        List<User> FindAllUsersByName(string name, long requestContextAccountId);
    }
}
