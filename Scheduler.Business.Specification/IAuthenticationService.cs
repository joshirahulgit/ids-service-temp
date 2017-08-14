using Scheduler.Business.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Business.Specification
{
    public interface IAuthenticationService : IContract
    {
        void CopyAccessRole(int roleId, string name, List<int> accIds);

        UserRolesDto UpdateUserRoleList(UserRolesDto roles);

        UserDto CreateUser(UserDto newUser);

        void RemoveUser(UserDto user);

        UserDto UpdateUser(long userId, UserDto updatedUser);

        UserDto Login(string login, string password);

        UserDto LoginSecure(List<KeyValuePair<string, string>> loginParams);

        void Logout(UserDto user, object sessionToken);

        UsersDto GetAllUsers();

        UserProfileDto SaveUserProfile(long userId, UserProfileDto profile);

        UserProfilesDto CopyProfiles(List<int> profileIds, int userId);

        UserProfilesDto CreateRolesProfiles(List<UserProfileDto> profiles, int roleId);

        void RemoveUserProfile(long userId, long accountId);

        long ResolveIdByAccountName(string accountName, string userName);//internal service method
        String ResolveNameByAccountId(long accountID);

        UsersDto GetAllUsersByName(string name);
    }
}
