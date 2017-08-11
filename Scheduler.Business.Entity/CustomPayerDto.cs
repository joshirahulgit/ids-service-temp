using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Business.Entity
{
    public class CustomPayerDto : DtoBase
    {
        public CustomPayerDto()
        {
            Addresses = new List<AddressDto>();
        }

        public long PayerId { get; set; }

        public String Address { get; set; }

        public string Address2 { get; set; }

        public String Phone { get; set; }

        public String Name { get; set; }

        public String WebSite { get; set; }

        public String City { get; set; }

        public String State { get; set; }

        public String Zip { get; set; }

        public String Fax { get; set; }

        public bool IsGlobal { get; set; }

        public bool? IsEligible { get; set; }

        public string ClientCode { get; set; }

        public String VendorPayerId { get; set; }

        public int PayerAddressId { get; set; }

        public string Address1Ext
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                foreach (AddressDto address in Addresses)
                {
                    sb.Append(address.Address1);
                    sb.AppendLine();
                }

                if (sb.Length > 0)
                    sb.Remove(sb.Length - Environment.NewLine.Length, Environment.NewLine.Length);

                return sb.ToString();
            }
        }

        public string Address2Ext
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                foreach (AddressDto address in Addresses)
                {
                    sb.Append(address.Address2);
                    sb.AppendLine();
                }

                if (sb.Length > 0)
                    sb.Remove(sb.Length - Environment.NewLine.Length, Environment.NewLine.Length);

                return sb.ToString();
            }
        }


        public string CityExt
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                foreach (AddressDto address in Addresses)
                {
                    sb.Append(address.City);
                    sb.AppendLine();
                }

                if (sb.Length > 0)
                    sb.Remove(sb.Length - Environment.NewLine.Length, Environment.NewLine.Length);

                return sb.ToString();
            }
        }


        public string StateExt
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                foreach (AddressDto address in Addresses)
                {
                    sb.Append(address.State);
                    sb.AppendLine();
                }

                if (sb.Length > 0)
                    sb.Remove(sb.Length - Environment.NewLine.Length, Environment.NewLine.Length);

                return sb.ToString();
            }
        }


        public string ZipExt
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                foreach (AddressDto address in Addresses)
                {
                    sb.Append(address.ZipCode);
                    sb.AppendLine();
                }

                if (sb.Length > 0)
                    sb.Remove(sb.Length - Environment.NewLine.Length, Environment.NewLine.Length);

                return sb.ToString();
            }
        }

        public String ExternalId { get; set; }

        public List<AddressDto> Addresses { get; set; }

        public String DisplayName
        {
            get
            {
                return GetDisplayName();
            }
        }

        private string GetDisplayName()
        {
            StringBuilder result = new StringBuilder();
            result.Append(Name);

            if (!string.IsNullOrEmpty(Address))
                result.AppendFormat(" ~ {0}", Address);

            if (!string.IsNullOrEmpty(Address2))
                result.AppendFormat(" {0}", Address2);

            if (!string.IsNullOrEmpty(City))
                result.AppendFormat(" ~ {0}", City);

            if (!string.IsNullOrEmpty(State))
                result.AppendFormat(" ~ {0}", State);

            if (!string.IsNullOrEmpty(Zip))
                result.AppendFormat(" ~ {0}", Zip);

            return result.ToString();
        }
        //        public CustomPayerDto(CustomPayerDto source)
        //        {
        //            Address  = source.Address;
        //            Address2  = source.Address2;
        //            Phone  = source.Phone;
        //            Name  = source.Name;
        //            WebSite  = source.WebSite;
        //            City  = source.City;
        //            State  = source.State;
        //            Zip  = source.Zip;
        //            Fax  = source.Fax;
        //            IsGlobal = source.IsGlobal;
        //        }

    }

    public class CustomPayersDto : DtoBase
    {
        public CustomPayersDto()
        {
            this.Payers = new List<CustomPayerDto>();
        }

        public List<CustomPayerDto> Payers { get; set; }
    }
}
