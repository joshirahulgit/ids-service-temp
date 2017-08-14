using Scheduler.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.DBEntity
{
    public class User : EntityBase
    {
        public List<long> Roles { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Account> Accounts { get; set; }
        public TimeSpan DefaultAppointmentLength { get; set; }
        public bool IsSuperAdmin { get; set; }
        public bool IsIDSUser { get; set; }
        public List<UserProfile> Profiles { get; set; }
        public UserProfile CurrentProfile { get; set; }

        public User()
        {
            Accounts = new List<Account>();
            Profiles = new List<UserProfile>();
            CurrentProfile = new UserProfile();
            DefaultAppointmentLength = TimeSpan.FromMinutes(15);
            Roles = new List<long>();
        }

        //public UserProfile SaveProfile(RepositoryLocator locator, UserProfile newProfile)
        //{
        //    this.Profiles.Add(newProfile);
        //    newProfile.SetUserId((int)this.Id);
        //    if (newProfile.Id == 0)
        //    {
        //        return locator.UserRepository.AddProfileToUserNew(newProfile);
        //        //return locator.UserRepository.AddProfileToUser(this, newProfile);
        //    }

        //    return locator.UserRepository.UpdateProfileNew(newProfile);
        //    //return locator.UserRepository.UpdateProfile(this,newProfile);
        //}

        //public void Update(RepositoryLocator locator, User updatedUser)
        //{
        //    this.Login = updatedUser.Login;
        //    this.FirstName = updatedUser.FirstName;
        //    this.LastName = updatedUser.LastName;
        //    this.Password = updatedUser.Password;
        //    this.Accounts = updatedUser.Accounts;
        //    this.DefaultAppointmentLength = updatedUser.DefaultAppointmentLength;
        //    this.IsSuperAdmin = updatedUser.IsSuperAdmin;

        //    List<Account> newAccounts = new List<Account>(updatedUser.Accounts.Count);
        //    List<UserProfile> newProfiles = new List<UserProfile>(updatedUser.Profiles.Count);

        //    foreach (Account a in updatedUser.Accounts)
        //        newAccounts.Add(a);

        //    //            foreach (UserProfile p in updatedUser.Profiles)
        //    //                newProfiles.Add(p);

        //    this.CurrentProfile = updatedUser.CurrentProfile;

        //    this.Accounts = newAccounts;
        //    //this.Profiles = newProfiles;

        //    locator.UserRepository.Update(this);
        //}

        //public void Delete(RepositoryLocator locator)
        //{
        //    //Here we can perform before delete actions 
        //    locator.UserRepository.Remove(this);
        //}

        //public void Create(RepositoryLocator locator)
        //{
        //    //Here we can perform before create actions 
        //    locator.UserRepository.Create(this);
        //}

        public void SetRoles(List<long> userRoles)
        {
            Roles = userRoles;
        }

        public void SetPersonalInfo(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }


    }

    public class UserProfileComment : IEquatable<UserProfileComment>
    {
        public int Id { get; set; }

        public List<CommentType> PredefinedCommentTypes { get; set; }

        public DateTime? TimeFrom { get; set; }

        public DateTime? TimeTo { get; set; }

        public bool IsAppointmentOnly { get; set; }

        public UserProfileComment()
        {
            PredefinedCommentTypes = new List<CommentType>();
        }

        public bool Equals(UserProfileComment other)
        {
            if (Object.ReferenceEquals(other, null)) return false;
            if (Object.ReferenceEquals(this, other)) return true;
            return Eq(other);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            UserProfileComment comment = obj as UserProfileComment;
            if (comment == null)
            {
                return false;
            }
            return Eq(comment);
        }

        private bool Eq(UserProfileComment comment)
        {
            bool baseEq =
                TimeFrom == comment.TimeFrom &&
                TimeTo == comment.TimeTo &&
                IsAppointmentOnly == comment.IsAppointmentOnly;
            if (this.PredefinedCommentTypes == null && comment.PredefinedCommentTypes == null)
            {
                return baseEq;
            }
            else
            {
                if (this.PredefinedCommentTypes != null && comment.PredefinedCommentTypes != null)
                {
                    return baseEq && !this.PredefinedCommentTypes.Except(comment.PredefinedCommentTypes).Any();
                }
                else
                {
                    return baseEq;
                }
            }
        }

        public override int GetHashCode()
        {
            int hashResult = TimeFrom.GetHashCode();
            hashResult ^= TimeTo.GetHashCode();
            hashResult ^= IsAppointmentOnly.GetHashCode();
            if (PredefinedCommentTypes != null)
            {
                foreach (CommentType predefinedCommentType in PredefinedCommentTypes)
                {
                    hashResult ^= predefinedCommentType.GetHashCode();
                }
            }
            return hashResult;
        }
    }

    public class UserProfile
    {
        public UserProfile()
        {
            this.Columns = new List<ColumnConfig>();
            TabsConfiguraton = new Dictionary<int, bool>();
        }

        public class ColumnConfig : IEquatable<ColumnConfig>
        {
            public int Id { get; set; }
            public int Size { get; set; }
            public string Name { get; set; }
            public int Order { get; set; }
            public string Sort { get; set; }

            public ColumnConfig()
            {

            }

            public ColumnConfig(int size, string name, int order, string sort) : this()
            {
                Size = size;
                Name = name;
                Order = order;
                Sort = sort;
            }

            public bool Equals(ColumnConfig other)
            {
                if (Object.ReferenceEquals(other, null)) return false;
                if (Object.ReferenceEquals(this, other)) return true;
                return Eq(other);
            }

            public override bool Equals(object obj)
            {
                if (obj == null)
                {
                    return false;
                }
                ColumnConfig cc = obj as ColumnConfig;
                if (cc == null)
                {
                    return false;
                }
                return Eq(cc);
            }

            public override int GetHashCode()
            {
                //Get hash code for the Name field if it is not null.
                int hashName = Name == null ? 0 : Name.GetHashCode();
                int hashSort = Sort == null ? 0 : Sort.GetHashCode();
                int hashSize = Size.GetHashCode();
                int hashOrder = Order.GetHashCode();

                return hashName ^ hashSort ^ hashSize ^ hashOrder;
            }

            private bool Eq(ColumnConfig cc)
            {
                return
                  Size == cc.Size &&
                  Name == cc.Name &&
                  Order == cc.Order &&
                  Sort == cc.Sort;
            }
        }

        public int Id { get; set; }
        public long AccountID { get; set; }
        public int UserId { get; set; }
        public int? RoleId { get; set; }
        public string RoleName { get; set; }
        public String Filters { get; set; }
        public String ProfileName { get; set; }
        public ScheduleMode ScheduleMode { get; set; }
        public int DefaultViewMode { get; set; }
        public List<ColumnConfig> Columns { get; set; }
        public int PrintOption { get; set; }
        public CalendarDisplayMode CalendarDisplayMode { get; set; }
        public double FirstVisibleHour { get; set; }
        public double LastVisibleHour { get; set; }
        public int PatientInfoVisitIndex { get; set; }
        public int CaptionMode { get; set; }
        public Dictionary<int, bool> TabsConfiguraton { get; set; }

        public bool IsSummaryPageEnabled { get; set; }
        public bool? IsExportedSuccessfully { get; set; }
        public bool IsDefault { get; set; }

        public UserProfileComment CommentDefaults { get; set; }

        public void SetId(int newId)
        {
            Id = newId;
        }

        public void SetName(string name)
        {
            ProfileName = name;
        }

        public void SetUserId(int userId)
        {
            this.UserId = userId;
        }

        public void SetRoleId(int roleId)
        {
            this.RoleId = roleId;
        }

        public void SetAsDefault()
        {
            this.IsDefault = true;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            var eqProfile = obj as UserProfile;
            if (eqProfile == null)
            {
                return false;
            }
            if (
                //Id                      != eqProfile.Id ||
                //AccountID               != eqProfile.AccountID ||
                //UserId                  != eqProfile.UserId ||
                Filters != eqProfile.Filters ||
                ProfileName != eqProfile.ProfileName ||
                ScheduleMode != eqProfile.ScheduleMode ||
                DefaultViewMode != eqProfile.DefaultViewMode ||
                PrintOption != eqProfile.PrintOption ||
                CalendarDisplayMode != eqProfile.CalendarDisplayMode ||
                Math.Abs(FirstVisibleHour - eqProfile.FirstVisibleHour) > 0 ||
                Math.Abs(LastVisibleHour - eqProfile.LastVisibleHour) > 0 ||
                PatientInfoVisitIndex != eqProfile.PatientInfoVisitIndex ||
                CaptionMode != eqProfile.CaptionMode ||
                IsSummaryPageEnabled != eqProfile.IsSummaryPageEnabled
                )
            {
                return false;
            }
            if (this.Columns != null && eqProfile.Columns != null)
            {
                if (this.Columns.Except(eqProfile.Columns).Any())
                {
                    return false;
                }
            }
            else
            {
                if ((this.Columns == null && eqProfile.Columns != null) || (this.Columns != null && eqProfile.Columns == null))
                {
                    return false;
                }
            }
            if (!this.CommentDefaults.Equals(eqProfile.CommentDefaults))
            {
                return false;
            }
            if (TabsConfiguraton != null && eqProfile.TabsConfiguraton != null)
            {
                if (TabsConfiguraton.Except(eqProfile.TabsConfiguraton).Any())
                {
                    return false;
                }
            }
            else
            {
                if ((TabsConfiguraton != null && eqProfile.TabsConfiguraton != null) || (TabsConfiguraton != null && eqProfile.TabsConfiguraton != null))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
