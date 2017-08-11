using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Business.Entity
{
    public class UserRoleDto //: DtoBase, System.ComponentModel.INotifyPropertyChanged
    {
        public long Id { get; set; }

        private string _name;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                Notify("Name");
            }
        }

        public string ToolTip
        {
            get
            {
                if (!CanBeDeleted) return "This role is in use.";
                return null;
            }
        }

        public AccessControlListDto Acl { get; set; }

        public bool CanBeDeleted { get; set; }


        public UserRoleDto()
        {
            Acl = new AccessControlListDto();
        }

        public UserRoleDto(UserRoleDto role)
        {
            Name = role.Name;
            CanBeDeleted = role.CanBeDeleted;
            Id = role.Id;
            Acl = new AccessControlListDto(role.Acl);
        }

        public UserRoleDto(AccessControlListDto acl, string name, long id, bool canDelete)
        {
            Acl = acl;
            Name = name;
            Id = id;
            CanBeDeleted = canDelete;
        }


        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        void Notify(string propName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propName));
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }

    public class UserRolesDto //: DtoBase
    {
        public List<UserRoleDto> Roles { get; set; }
    }
}
