using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.DBEntity
{
    public class Referral : EntityBase
    {
        public Referral()
        {
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Type { get; set; }
        public string ReferralId { get; set; }
        public string Speciality { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public bool? IsFaxingEnabled { get; set; }
        public bool IsEmailEnabled { get; set; }
        public bool IsAutoPrintEnabled { get; set; }
        public string NPI { get; set; }
        public string TaxId { get; set; }

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
        public string PrimaryMarketerId { get; set; }
        public string EhrSystem { get; set; }
        public string WebSite { get; set; }
        public string CompanyName { get; set; }

        public List<ReferringNote> ReferringNotes { get; set; }
        //        public List<MarketingRep> MarketingReps { get; set; } 

        //internal static Referral ExtractFromDto(ReferralDto dto)
        //{
        //    if (dto == null)
        //        return null;

        //    Referral result = new Referral();
        //    result.IsFaxingEnabled = dto.IsFaxingEnabled;
        //    result.IsAutoPrintEnabled = dto.IsAutoPrintEnabled;
        //    result.Fax = dto.Fax?.Trim();
        //    result.FirstName = dto.FirstName?.Trim();
        //    result.Id = dto.Id;
        //    result.LastName = dto.LastName?.Trim();
        //    result.Phone = dto.Phone?.Trim();
        //    result.Email = dto.Email?.Trim();
        //    result.Type = dto.Type?.Trim();
        //    result.ReferralId = dto.ReferralId;
        //    result.Speciality = dto.Speciality;
        //    result.Address = dto.Address?.Trim();
        //    result.Address2 = dto.Address2?.Trim();
        //    result.City = dto.City?.Trim();
        //    result.State = dto.State?.Trim();
        //    result.ZipCode = dto.ZipCode?.Trim();

        //    result.IsEmailEnabled = dto.IsEmailEnabled;
        //    result.NPI = dto.NPI?.Trim();
        //    result.TaxId = dto.TaxId?.Trim();

        //    result.Group = dto.Group?.Trim();
        //    result.FirstLanguage = dto.FirstLanguage;
        //    result.SecondLanguage = dto.SecondLanguage;
        //    result.Country = dto.Country;
        //    result.OfficePhone = dto.OfficePhone;
        //    result.OfficeFax = dto.OfficeFax;
        //    result.MobilePhone = dto.MobilePhone;
        //    result.RefINOutStatus = dto.RefINOutStatus;
        //    result.IsActive = dto.IsActive;
        //    result.SSN = dto.SSN;

        //    result.MiddleName = dto.MiddleName;
        //    result.Signature = dto.Signature;
        //    result.Credentials = dto.Credentials;
        //    result.ExternalID = dto.ExternalID;

        //    result.EhrSystem = dto.EhrSystem;
        //    result.WebSite = dto.WebSite;
        //    result.PrimaryMarketerId = dto.PrimaryMarketerId;
        //    result.CompanyName = dto.CompanyName;

        //    /*
        //                foreach (MarketingRepDto marketingRep in dto.MarketingReps)
        //                    result.MarketingReps.Add(MarketingRep.ExtractFromDto(marketingRep));
        //    */

        //    return result;
        //}

        //public static List<CommentItem> ExtractReferringNotesFromReader(SafeDataReader sr)
        //{
        //    /* List<CommentItem> rn = new List<CommentItem>();
        //     rn.Add(CommentItem.ExtractFromReader(sr));
        //     return rn;*/

        //    return null;
        //}

        

        public void TrimNames()
        {
            if (this.FirstName != null)
                this.FirstName = this.FirstName.Trim();

            if (this.LastName != null)
                this.LastName = this.LastName.Trim();
        }

        //public static ReferralDto Convert2Dto(Referral referral)
        //{
        //    if (referral == null) return null;
        //    var refs = new ReferralDto(
        //        referral.Id,
        //        referral.FirstName,
        //        referral.LastName,
        //        referral.Phone,
        //        referral.Fax,
        //        referral.Email,
        //        string.IsNullOrEmpty(referral.Type) ? "Unknown" : referral.Type,
        //        referral.ReferralId)
        //    {
        //        Speciality = referral.Speciality,
        //        NPI = referral.NPI,
        //        TaxId = referral.TaxId,
        //        IsFaxingEnabled = referral.IsFaxingEnabled ?? false,
        //        IsEmailEnabled = referral.IsEmailEnabled,
        //        IsAutoPrintEnabled = referral.IsAutoPrintEnabled,
        //        Address = referral.Address,
        //        Address2 = referral.Address2,
        //        City = referral.City,
        //        State = referral.State,
        //        ZipCode = referral.ZipCode,

        //        Group = referral.Group,
        //        FirstLanguage = referral.FirstLanguage,
        //        SecondLanguage = referral.SecondLanguage,
        //        Country = referral.Country,
        //        OfficePhone = referral.OfficePhone,
        //        OfficeFax = referral.OfficeFax,
        //        MobilePhone = referral.MobilePhone,
        //        RefINOutStatus = referral.RefINOutStatus,
        //        IsActive = referral.IsActive,
        //        SSN = referral.SSN,
        //        CompanyName = referral.CompanyName,
        //        PrimaryMarketerId = referral.PrimaryMarketerId,
        //        WebSite = referral.WebSite,
        //        EhrSystem = referral.EhrSystem
        //    };

        //    refs.ReferringNotes = new ReferringNotesDto();
        //    if (referral.ReferringNotes != null)
        //        foreach (ReferringNote note in referral.ReferringNotes)
        //            refs.ReferringNotes.Notes.Add(ReferringNote.Convert2Dto(note));


        //    refs.MiddleName = referral.MiddleName;
        //    refs.Signature = referral.Signature;
        //    refs.Credentials = referral.Credentials;
        //    refs.ExternalID = referral.ExternalID;

        //    return refs;
        //}
        public bool CompareTo(Referral r)
        {
            if (r == null)
                return false;
            return r.ReferralId == ReferralId;
        }

        public void SetNewReferringId(string newReferringId)
        {
            ReferralId = newReferringId;
        }

        public void SetMarketerDetails(SchedulerIntegration data)
        {
            if (data == null)
                return;
            this.PrimaryMarketerId = data.PrimaryMarketerId;
            this.CompanyName = data.CompanyName;
            this.EhrSystem = data.EhrSystem;
            this.WebSite = data.WebSite;
            this.Credentials = data.Credentials;
            this.Speciality = data.SpecArea;
        }
    }
}
