using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.DBEntity
{
    public class Address : EntityBase
    {
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public bool IsInternational { get; set; }
        public int EntityStatus { get; set; }

        public Address(int id, string address1, string address2, string city, string state, string zipCode,
            string country, string phone, string fax, string email, string mobile) : this(address1, address2, city, state, zipCode, country, phone,
            fax, email, mobile, false)
        {
            Id = id;
        }


        public Address(string address1, string address2, string city, string state, string zipCode, string country, string phone, string fax, string email, string mobile, bool isInternational)
        {
            Address1 = address1;
            Address2 = address2;
            City = city;
            State = state;
            ZipCode = zipCode;
            Country = country;
            Phone = phone;
            Fax = fax;
            Email = email;
            Mobile = mobile;
            IsInternational = isInternational;
            EntityStatus = (int)Core.EntityStatus.NotModified;
        }

        public Address()
        {
            EntityStatus = (int)Core.EntityStatus.NotModified;
        }

        //public Address Create(RepositoryLocator locator, long patientId)
        //{
        //    Address address = locator.ResourceRepository.CreatePatientAdditionalAddress(patientId, this);
        //    address.EntityStatus = (int)Scheduler.Common.Enums.EntityStatus.NotModified;
        //    return address;
        //}

        //public void Delete(RepositoryLocator locator, long patientId)
        //{
        //    locator.ResourceRepository.RemovePatientAdditionalAddress(this);
        //}        

        //public Address Update(RepositoryLocator locator, Address address, long patientId)
        //{
        //    this.Address1 = address.Address1;
        //    this.Address2 = address.Address2;
        //    this.City = address.City;
        //    this.Country = address.Country;
        //    this.Fax = address.Fax;
        //    this.Mobile = address.Mobile;
        //    this.Phone = address.Phone;
        //    this.State = address.State;
        //    this.ZipCode = address.ZipCode;
        //    this.Email = address.Email;
        //    this.IsInternational = address.IsInternational;
        //    this.EntityStatus = address.EntityStatus;
        //    locator.ResourceRepository.UpdatePatientAdditionalAddress(this);
        //    return this;
        //}
    }
}
