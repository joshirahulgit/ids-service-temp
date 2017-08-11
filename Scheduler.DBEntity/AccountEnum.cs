using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scheduler.DBEntity
{
    public class AccountEnum : EntityBase
    {
        private string _name = "";
        private string _value = "";
        private string _enumType = "";

        private bool _isVisible = true;
        private bool _isDefault;
        private bool _userCanEdit = true;
        private bool _userCanDelete = true;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public bool IsVisible
        {
            get { return _isVisible; }
            set { _isVisible = value; }
        }

        public string EnumType
        {
            get { return _enumType; }
            set { _enumType = value; }
        }

        public bool UserCanEdit
        {
            get { return _userCanEdit; }
            set { _userCanEdit = value; }
        }

        public bool UserCanDelete
        {
            get { return _userCanDelete; }
            set { _userCanDelete = value; }
        }

        //public string DefaultDbName
        //{
        //    get { return _defaultDbName; }
        //    set { _defaultDbName = value; }
        //}

        public bool IsDefault
        {
            get { return _isDefault; }
            set { _isDefault = value; }
        }

        public AccountEnum() { }

        public AccountEnum(long id) : this()
        {
            this.Id = id;
        }
    }
}
