using Scheduler.DBEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Data.Implementation
{
    //To Desc: This class is created to support existing code and also maintain to SOC. -By RJ

    internal static class SafeDataReaderExt
    {
        public static AccountEnum ToAccountEnum(this SafeDataReader sr)
        {
            AccountEnum r = null;
            //r.Id = sr.GetInt64("ID");

            //By RJ: Next 4 lines added to support exiting encapsulation of ID field in Account Enum outside the class.
            if (sr.ContainsColumn("id"))
                r = new AccountEnum(sr.GetInt32("id"));
            else
                r = new AccountEnum();


            if (sr.ContainsColumn("Name"))
                r.Name = sr.GetNullableString("Name");
            if (sr.ContainsColumn("Value"))
                r.Value = sr.GetNullableString("Value");
            if (sr.ContainsColumn("IsVisible"))
                r.IsVisible = sr.GetBoolean("IsVisible");
            if (sr.ContainsColumn("EnumType"))
                r.EnumType = sr.GetNullableString("EnumType");
            if (sr.ContainsColumn("IsDefault"))
                r.IsDefault = sr.GetBoolean("IsDefault");
            if (sr.ContainsColumn("UserCanEdit"))
                r.UserCanEdit = sr.GetBoolean("UserCanEdit");
            if (sr.ContainsColumn("UserCanDelete"))
                r.UserCanDelete = sr.GetBoolean("UserCanDelete");

            return r;
        }
    }
}
