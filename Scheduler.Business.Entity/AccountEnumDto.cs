using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Business.Entity
{
    public class AccountEnumDto //: DtoBase TODO: Not sure about Dto Base
    {
        private string _name = "";
        private string _value = "";
        private bool _isVisible = true;
        private string _enumType = "";
        private bool _isDefault;
        private bool _userCanEdit = true;
        private bool _userCanDelete = true;

        public long Id { get; set; }

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

        public bool IsDefault
        {
            get { return _isDefault; }
            set { _isDefault = value; }
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

        public string Text
        {
            get { return Value; }
        }

        public override string ToString()
        {
            return Value;
        }
    }
}
