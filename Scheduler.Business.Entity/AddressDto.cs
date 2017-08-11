using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Business.Entity
{
    public class AddressDto : DtoBase
    {
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Mobile { get; set; }
        public long Id { get; set; }
        public bool IsInternational { get; set; }
        public int EntityStatus { get; set; }
        public string Email { get; set; }



        public AddressDto()
        {
            Address1 =
            Address2 =
            City =
            State =
            ZipCode =
            Country =
            Phone =
            Fax =
            Mobile =
            Email = string.Empty;
        }

        public AddressDto(AddressDto address)
        {
            //Sunil: was throwing an error since was address null
            if (address == null) return;
            Id = address.Id;
            Address1 = address.Address1;
            Address2 = address.Address2;
            City = address.City;
            State = address.State;
            ZipCode = address.ZipCode;
            Country = address.Country;
            Phone = address.Phone;
            Fax = address.Fax;
            Mobile = address.Mobile;
            IsInternational = address.IsInternational;
            EntityStatus = address.EntityStatus;
            Email = address.Email;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            if (IsInternational)
                sb.Append("International, ");
            if (!string.IsNullOrEmpty(Address1))
                sb.Append(Address1 + ", ");
            if (!string.IsNullOrEmpty(Address2))
                sb.Append(Address2 + ", ");
            if (!string.IsNullOrEmpty(City))
                sb.Append(City + ", ");
            if (!string.IsNullOrEmpty(State))
                sb.Append(State + ", ");
            if (!string.IsNullOrEmpty(ZipCode))
                sb.Append(ZipCode + ", ");
            if (!string.IsNullOrEmpty(Country))
                sb.Append(Country + ", ");
            if (!string.IsNullOrEmpty(Phone))
                sb.Append(Phone + ", ");
            if (!string.IsNullOrEmpty(Fax))
                sb.Append(Fax + ", ");
            if (!string.IsNullOrEmpty(Email))
                sb.Append(Email + ", ");
            string ret = sb.ToString();
            if (ret.Length > 2 && ret[ret.Length - 2] == ',')
                ret = ret.Remove(ret.Length - 2);
            return ret;
        }

        public static string ConstructTwoLineAddress(string addr1, string addr2, string city, string state, string zip)
        {
            string result = addr1 ?? string.Empty;
            if (!string.IsNullOrEmpty(addr2))
                result += (string.IsNullOrEmpty(result) ? "" : ", ") + addr2;

            string result2 = state ?? string.Empty;

            if (!string.IsNullOrEmpty(zip))
                result2 += (string.IsNullOrEmpty(result2) ? "" : " ") + zip;

            if (!string.IsNullOrEmpty(city))
                result2 = city + (string.IsNullOrEmpty(result2) ? "" : ", ") + result2;

            result = (result.Trim() + Environment.NewLine + result2.Trim()).Trim();
            return result;
        }
    }
}
