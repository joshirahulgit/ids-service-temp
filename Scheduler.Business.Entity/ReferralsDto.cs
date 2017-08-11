using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Business.Entity
{
    public class ReferralsDto : DtoBase
    {
        public ReferralsDto()
        {
            this.Referrals = new List<ReferralDto>();
        }

        public IList<ReferralDto> Referrals { get; set; }
    }

    public class ReferralDto : DtoBase
    {
        public ReferralDto()
        {
        }

        public ReferralDto(long id, string firstName, string lastName, string phone, string fax, string email, string type, string refId)
        {
            //TODO: Verify with Sunil these changes
            this.Id = id;
            this.FirstName = firstName;// firstName; 
            this.LastName = lastName;// lastName;
            this.Phone = phone;
            this.Fax = fax;
            this.Email = email;
            this.Type = type;
            this.ReferralId = refId;
        }

        public ReferralDto(ReferralDto referral)
        {
            this.Id = referral.Id;
            this.FirstName = referral.FirstName;
            this.LastName = referral.LastName;
            this.Phone = referral.Phone;
            this.Fax = referral.Fax;
            this.Email = referral.Email;
            this.Type = referral.Type;
            this.ReferralId = referral.ReferralId;
            this.IsFaxingEnabled = referral.IsFaxingEnabled;
            this.IsAutoPrintEnabled = referral.IsAutoPrintEnabled;
            this.IsEmailEnabled = referral.IsEmailEnabled;
            this.NPI = referral.NPI;
            this.TaxId = referral.TaxId;
            this.Address = referral.Address;
            this.Address2 = referral.Address2;
            this.City = referral.City;
            this.State = referral.State;
            this.ZipCode = referral.ZipCode;
            this.Group = referral.Group;
        }


        public string GroupName { get; set; }

        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string ReferralId { get; set; }

        public string Speciality { get; set; }

        public string Group { get; set; }

        public string SSN { get; set; }

        public string FirstLanguage { get; set; }

        public string SecondLanguage { get; set; }

        public string Country { get; set; }

        public string OfficePhone { get; set; }

        public string OfficeFax { get; set; }

        public string MobilePhone { get; set; }

        public string RefINOutStatus { get; set; }

        public bool IsActive { get; set; }

        public string Signature { get; set; }

        public string MiddleName { get; set; }

        public string Credentials { get; set; }

        public string ExternalID { get; set; }
        /*
                [DataMember(Name = "MARE")]
                public  List<MarketingRepDto> MarketingReps { get; set; }
        */

        public string PrimaryMarketerId { get; set; }

        public string EhrSystem { get; set; }

        public string WebSite { get; set; }

        public string CompanyName { get; set; }


        public string DisplayName
        {
            get { return (FirstName + " " + LastName).Trim(); }
        }

        public string DisplayType
        {
            get
            {
                if (Type.ToUpper() == "R")
                    return "Referring Physician";
                else
                    return Type;
            }
        }

        public string Email { get; set; }

        public string Type { get; set; }

        public string Address { get; set; }

        public string Address2 { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string ZipCode { get; set; }

        public bool IsFaxingEnabled { get; set; }

        public bool IsEmailEnabled { get; set; }

        public bool IsAutoPrintEnabled { get; set; }

        public string NPI { get; set; }

        public string TaxId { get; set; }

        public ReferringNotesDto ReferringNotes { get; set; }

    }
}
