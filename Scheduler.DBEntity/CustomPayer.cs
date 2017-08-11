using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.DBEntity
{
    public class CustomPayer : EntityBase
    {
        public CustomPayer()
        {
            Addresses = new List<Address>();
        }

        public String Address { get; set; }
        public String Address2 { get; set; }
        public String Phone { get; set; }
        public String Name { get; set; }
        public String WebSite { get; set; }
        public String City { get; set; }
        public String State { get; set; }
        public String Zip { get; set; }
        public String Fax { get; set; }
        public bool IsGlobal { get; set; }
        public bool? IsEligible { get; set; }
        public String VendorPayerId { get; set; }
        public String ClientCode { get; set; }
        public String ExternalId { get; set; }
        public int PayerAddressId { get; set; }
        public List<Address> Addresses { get; set; }

        //public CustomPayer Create(RepositoryLocator locator)
        //{
        //    CustomPayer newPayer = locator.ResourceRepository.CreateCustomPayer(this);
        //    return newPayer;
        //}

        //internal static CustomPayer ExtractFromDto(Common.DataTransferObjects.Appointment.CustomPayerDto p)
        //{
        //    CustomPayer res = new CustomPayer();
        //    res.Id = p.PayerId;
        //    res.Address = p.Address;
        //    res.Address2 = p.Address2;
        //    res.ClientCode = p.ClientCode;
        //    res.Name = p.Name;
        //    res.Fax = p.Fax;
        //    res.City = p.City;
        //    res.State = p.State;
        //    res.Phone = p.Phone;
        //    res.Zip = p.Zip;
        //    res.WebSite = p.WebSite;
        //    res.IsGlobal = p.IsGlobal;
        //    res.IsEligible = p.IsEligible;
        //    res.VendorPayerId = p.VendorPayerId;
        //    res.ExternalId = p.ExternalId;
        //    res.PayerAddressId = p.PayerAddressId;

        //    foreach (AddressDto address in p.Addresses)
        //        res.Addresses.Add(Entities.Appointment.Address.ExtractFromDto(address));

        //    return res;
        //}

        //public static CustomPayerDto Convert2Dto(CustomPayer business)
        //{
        //    CustomPayerDto result = new CustomPayerDto();
        //    result.Address = business.Address;
        //    result.Address2 = business.Address2;
        //    result.City = business.City;
        //    result.Fax = business.Fax;
        //    result.Name = business.Name;
        //    result.PayerId = business.Id;
        //    result.Phone = business.Phone;
        //    result.State = business.State;
        //    result.WebSite = business.WebSite;
        //    result.Zip = business.Zip;
        //    result.IsGlobal = business.IsGlobal;
        //    result.IsEligible = business.IsEligible;
        //    result.VendorPayerId = business.VendorPayerId;
        //    result.ClientCode = business.ClientCode;
        //    result.ExternalId = business.ExternalId;
        //    result.PayerAddressId = business.PayerAddressId;
        //    foreach (Address item in business.Addresses)
        //        result.Addresses.Add(Entities.Appointment.Address.ConvertToDto(item));

        //    return result;
        //}

        public override string ToString()
        {
            return string.Format("{0} {1} ~ {2}", Id, Name ?? string.Empty, State ?? string.Empty);
        }
    }
}
