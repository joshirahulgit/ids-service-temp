using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Business.Entity
{
    public class UserDto //: DtoBase
    {
        //[DataMember(Name = "A")]
        public long Id { get; set; }

        //[DataMember(Name = "B")]
        public string Login { get; set; }

        //[DataMember(Name = "FName")]
        public string FirstName { get; set; }

        //[DataMember(Name = "LName")]
        public string LastName { get; set; }

        //[DataMember(Name = "C")]
        public string Password { get; set; }

        //[DataMember(Name = "D")]
        public List<AccountDto> Accounts { get; set; }

        //[DataMember(Name = "E")]
        public TimeSpan DefaultAppointmentLength { get; set; }

        //[DataMember(Name = "G")]
        public List<UserProfileDto> Profiles { get; set; }

        //[DataMember(Name = "CP")]
        public UserProfileDto CurrentProfile { get; set; }

        //[DataMember(Name = "H")]
        public bool IsSuperAdmin { get; set; }

        //[DataMember(Name = "IsIDSUser")]
        public bool IsIDSUser { get; set; }

        public string RolesString { get; set; }

        //[DataMember(Name = "I")]
        public List<long> Roles { get; set; }

        public UserDto()
        {
            Accounts = new List<AccountDto>();
            Profiles = new List<UserProfileDto>();
            DefaultAppointmentLength = TimeSpan.FromMinutes(15);
            CurrentProfile = new UserProfileDto();
            Roles = new List<long>();
        }

        public UserDto Clone()
        {
            return DoClone();
        }

        protected UserDto DoClone()
        {
            return this.MemberwiseClone() as UserDto;
        }

        public UserAccessRightToAccount GetAccessRightToAccount(string currentAccountName)
        {
            foreach (AccountDto account in Accounts)
            {
                if (account.AccountName.ToLowerInvariant() == currentAccountName.ToLowerInvariant())
                {
                    if (IsSuperAdmin) return UserAccessRightToAccount.SchedulerAdmin;
                    if (account.IsSchedulerAdmin) return UserAccessRightToAccount.SchedulerAdmin;
                    if (account.HasAccessToScheduler) return UserAccessRightToAccount.RegularUser;
                    return UserAccessRightToAccount.NoAccess;
                }
            }
            return UserAccessRightToAccount.NotDefined;
        }
    }

    public class UsersDto //: DtoBase
    {
        public UsersDto()
        {
            this.Users = new List<UserDto>();
        }

        public IList<UserDto> Users { get; set; }
    }

    public class UserProfileDto //: DtoBase
    {
        public class ColumnConfigDto //: DtoBase
        {
            public int Id { get; set; }
            public int Size { get; set; }
            public String Name { get; set; }
            public int Order { get; set; }
            public string Sort { get; set; }

            public override string ToString()
            {
                return Name;
            }
        }

        public UserProfileDto()
        {
            this.Columns = new List<ColumnConfigDto>();
            this.TabsConfiguration = new Dictionary<int, bool>();
        }


        public UserProfileCommentDto CommentDefaults { get; set; }

        public int Id { get; set; }
        public long AccountID { get; set; }
        public int UserId { get; set; }
        public int? RoleId { get; set; }
        public String Filters { get; set; }
        public ScheduleMode ScheduleMode { get; set; }
        public int DefaultViewMode { get; set; }
        public List<ColumnConfigDto> Columns { get; set; }
        //public PrintOptions PrintOption { get; set; }
        //public CalendarDisplayMode CalendarDisplayMode { get; set; }
        public double FirstVisibleHour { get; set; }
        public double LastVisibleHour { get; set; }
        public int PatientInfoVisitIndex { get; set; }
        public int CaptionMode { get; set; }

        public string ProfileName { get; set; }

        public Dictionary<int, bool> TabsConfiguration { get; set; }

        public bool IsSummaryPageEnabled { get; set; }

        public bool? IsExportedSuccessfully { get; set; }

        public bool IsDefault { get; set; }

        public string RoleName { get; set; }

        public bool IsRoleProfile
        {
            get { return RoleId.HasValue; }
        }

        public string ProfileNameExt
        {
            get { return ProfileName + (string.IsNullOrEmpty(RoleName) ? string.Empty : string.Format(" ({0})", RoleName)); }
        }

        public override string ToString()
        {
            return ProfileName;
        }
    }


    public class UserProfileCommentDto //: DtoBase
    {
        public UserProfileCommentDto()
        {
            PredefinedCommentTypes = new List<CommentTypeDto>();
        }

        public List<CommentTypeDto> PredefinedCommentTypes { get; set; }

        public DateTime? TimeFrom { get; set; }

        public DateTime? TimeTo { get; set; }

        public bool IsAppointmentOnly { get; set; }
    }


    public class UserProfilesDto// : DtoBase
    {
        public UserProfilesDto()
        {
            this.Values = new List<UserProfileDto>();
        }

        public IList<UserProfileDto> Values { get; set; }
    }
}
