using Scheduler.Business.Entity;
using Scheduler.Business.Specification;
using Scheduler.Data.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Business.Implementation
{
    public class AuthenticationService : ServiceBase, IAuthenticationService
    {
        public static object _locker = new object();
        class AuthenticationCacheItem
        {
            public DateTime CreateTime { get; set; }
            public UserDto User { get; set; }
        }
        private static Dictionary<string, AuthenticationCacheItem> _authenticatedCache = new Dictionary<string, AuthenticationCacheItem>();

        public void CopyAccessRole(int roleId, string name, List<int> accIds)
        {
            throw new NotImplementedException();
        }

        public UserRolesDto UpdateUserRoleList(UserRolesDto roles)
        {
            throw new NotImplementedException();
        }

        public UserDto CreateUser(UserDto newUser)
        {
            throw new NotImplementedException();
        }

        public void RemoveUser(UserDto user)
        {
            throw new NotImplementedException();
        }

        public UserDto UpdateUser(long userId, UserDto updatedUser)
        {
            throw new NotImplementedException();
        }

        public UserDto Login(string login, string password)
        {
            throw new NotImplementedException();
        }

        public UserDto LoginSecure(List<KeyValuePair<string, string>> loginParams)
        {
            throw new NotImplementedException();
        }

        public void Logout(UserDto user, object sessionToken)
        {
            throw new NotImplementedException();
        }

        public UsersDto GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public UserProfileDto SaveUserProfile(long userId, UserProfileDto profile)
        {
            throw new NotImplementedException();
        }

        public UserProfilesDto CopyProfiles(List<int> profileIds, int userId)
        {
            throw new NotImplementedException();
        }

        public UserProfilesDto CreateRolesProfiles(List<UserProfileDto> profiles, int roleId)
        {
            throw new NotImplementedException();
        }

        public void RemoveUserProfile(long userId, long accountId)
        {
            throw new NotImplementedException();
        }

        public long ResolveIdByAccountName(string accountName, string userName)
        {
            throw new NotImplementedException();
        }

        public string ResolveNameByAccountId(long accountID)
        {
            throw new NotImplementedException();
        }

        public UsersDto GetAllUsersByName(string name)
        {
            throw new NotImplementedException();
        }
    }
}
