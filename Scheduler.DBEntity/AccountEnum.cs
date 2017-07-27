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

        //TODO: No DTO Code can be here -By RJ
        //public static AccountEnum ExtractFromReader(SafeDataReader sr)
        //{
        //    AccountEnum r = new AccountEnum();
        //    //r.Id = sr.GetInt64("ID");
        //    if (sr.ContainsColumn("id"))
        //        r.Id = sr.GetInt32("id");
        //    if (sr.ContainsColumn("Name"))
        //        r.Name = sr.GetNullableString("Name");
        //    if (sr.ContainsColumn("Value"))
        //        r.Value = sr.GetNullableString("Value");
        //    if (sr.ContainsColumn("IsVisible"))
        //        r.IsVisible = sr.GetBoolean("IsVisible");
        //    if (sr.ContainsColumn("EnumType"))
        //        r.EnumType = sr.GetNullableString("EnumType");
        //    if (sr.ContainsColumn("IsDefault"))
        //        r.IsDefault = sr.GetBoolean("IsDefault");
        //    if (sr.ContainsColumn("UserCanEdit"))
        //        r.UserCanEdit = sr.GetBoolean("UserCanEdit");
        //    if (sr.ContainsColumn("UserCanDelete"))
        //        r.UserCanDelete = sr.GetBoolean("UserCanDelete");

        //    return r;
        //}

        //TODO: No DTO Code can be here -By RJ
        //public static AccountEnumDto Convert2Dto(AccountEnum acEnum)
        //{
        //    AccountEnumDto dto = new AccountEnumDto();
        //    dto.Id = acEnum.Id;
        //    dto.Name = acEnum.Name;
        //    dto.Value = acEnum.Value;
        //    dto.IsVisible = acEnum.IsVisible;
        //    dto.EnumType = acEnum.EnumType;
        //    dto.IsDefault = acEnum.IsDefault;
        //    dto.UserCanEdit = acEnum.UserCanEdit;
        //    dto.UserCanDelete = acEnum.UserCanDelete;
        //    return dto;
        //}


        //TODO: No DTO code can be here. -By RJ
        //internal static AccountEnum ExtractFromDto(AccountEnumDto acEnumDto)
        //{
        //    AccountEnum res = new AccountEnum();
        //    res.Id = acEnumDto.Id;
        //    res.Name = acEnumDto.Name;
        //    res.Value = acEnumDto.Value;
        //    res.IsVisible = acEnumDto.IsVisible;
        //    res.EnumType = acEnumDto.EnumType;
        //    res.IsDefault = acEnumDto.IsDefault;
        //    res.UserCanEdit = acEnumDto.UserCanEdit;
        //    res.UserCanDelete = acEnumDto.UserCanDelete;
        //    return res;

        //}
    }
}
