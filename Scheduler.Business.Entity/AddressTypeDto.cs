using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Business.Entity
{
    public class AddressTypeDto : EntityTypeDto
    {
        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string County { get; set; }

        public string InternationalProvince { get; set; }

        public string ZipCode { get; set; }

        public string POBox { get; set; }

        public string Country { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Fax { get; set; }

        public AddressTypeDto()
        {
        }

        public AddressTypeDto(AddressTypeDto addressType) : this()
        {
            this.Address1 = addressType.Address1;
            this.Address2 = addressType.Address2;
            this.City = addressType.City;
            this.State = addressType.State;
            this.County = addressType.County;
            this.InternationalProvince = addressType.InternationalProvince;
            this.ZipCode = addressType.ZipCode;
            this.POBox = addressType.POBox;
            this.Country = addressType.Country;
            this.Email = addressType.Email;
            this.Phone = addressType.Phone;
            this.Fax = addressType.Fax;
        }

    }
}
