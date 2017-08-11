using Scheduler.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Business.Entity
{
    public class PayerDto : DtoBase
    {
        public class PayerStatusDto : DtoBase
        {
            public string AdditionalInfo { get; set; }
            public bool IsComplete { get; set; }
            public bool IsSuccessful { get; set; }
            public string UserText { get; set; }
            public DateTime? VerificationDateTime { get; set; }
            public String LastRequestedDictatorKey { get; set; }
            public String LastRequestedDictatorValue { get; set; }
            public String ValidationRequestID { get; set; }
        }

        public long PayerId { get; set; }

        public String Address { get; set; }

        public String Phone { get; set; }

        public String PolicyNumber { get; set; }

        public String ProductName { get; set; }

        public String Provider { get; set; }

        public String InsuredFirstName { get; set; }

        public DateTime InsuredDOB { get; set; }

        public DateTime? ExpirationDate { get; set; }

        public String Comment { get; set; }

        public String RelationShip { get; set; }

        public String Fax { get; set; }

        public String GroupNumber { get; set; }

        public String NPINumber { get; set; }

        public String LastName { get; set; }

        public String Gender { get; set; }

        public Int64 PatientId { get; set; }

        public String PayerName { get; set; }

        public UInt32 LevelIndex { get; set; }

        public EntityStatus Status { get; set; }

        public PayerStatusDto VerificationStatus { get; set; }

        public bool IsGlobal { get; set; }

        public string WebSite { get; set; }

        public string State { get; set; }

        public long InsuranceId { get; set; }

        public string Address2 { get; set; }

        public string ZipCode { get; set; }

        public string City { get; set; }

        public bool? IsEligible { get; set; }

        public String VendorPayerId { get; set; }

        public int PiId { get; set; }

        public String InsuredCity { get; set; }

        public String InsuredState { get; set; }

        public String InsuredZip { get; set; }

        public String InsuredAddress { get; set; }

        public int PayerAddressId { get; set; }

        public String InsuredPhone { get; set; }

        public String InsuredEmploymentName { get; set; }

        public bool IsDeleted { get; set; }

        public String InsuredEmploymentAddress { get; set; }

        public string InsuranceInfo
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                if (LevelIndex == 1)
                    sb.Append("(P)");

                if (LevelIndex == 2)
                    sb.Append("(S)");

                if (LevelIndex == 3)
                    sb.Append("(T)");

                sb.Append(" " + PayerName);
                return sb.ToString();
            }
        }

        public PayerDto()
        {
            this.Status = EntityStatus.NotModified;
            this.VerificationStatus = new PayerStatusDto();
        }

        public PayerDto(PayerDto sourceP)
        {
            this.Address = sourceP.Address;
            this.Phone = sourceP.Phone;
            this.PolicyNumber = sourceP.PolicyNumber;
            this.ProductName = sourceP.ProductName;
            this.Provider = sourceP.Provider;
            this.InsuredFirstName = sourceP.InsuredFirstName;
            this.InsuredDOB = sourceP.InsuredDOB;
            this.RelationShip = sourceP.RelationShip;
            this.Fax = sourceP.Fax;
            this.GroupNumber = sourceP.GroupNumber;
            this.NPINumber = sourceP.NPINumber;
            this.LastName = sourceP.LastName;
            this.Gender = sourceP.Gender;
            this.PatientId = sourceP.PatientId;
            this.PayerAddressId = sourceP.PayerAddressId;
            this.PayerId = sourceP.PayerId;
            this.PayerName = sourceP.PayerName;
            this.LevelIndex = sourceP.LevelIndex;
            this.WebSite = sourceP.WebSite;
            this.State = sourceP.State;
            this.InsuranceId = sourceP.InsuranceId;
            this.Address2 = sourceP.Address2;
            this.ZipCode = sourceP.ZipCode;
            this.City = sourceP.City;
            this.IsGlobal = sourceP.IsGlobal;
            this.Status = sourceP.Status;

            if (sourceP.VerificationStatus != null)
            {
                this.VerificationStatus = new PayerStatusDto();
                this.VerificationStatus.AdditionalInfo = sourceP.VerificationStatus.AdditionalInfo;
                this.VerificationStatus.IsComplete = sourceP.VerificationStatus.IsComplete;
                this.VerificationStatus.IsSuccessful = sourceP.VerificationStatus.IsSuccessful;
                this.VerificationStatus.UserText = sourceP.VerificationStatus.UserText;
                this.VerificationStatus.VerificationDateTime = sourceP.VerificationStatus.VerificationDateTime;
                this.VerificationStatus.LastRequestedDictatorKey = sourceP.VerificationStatus.LastRequestedDictatorKey;
                this.VerificationStatus.LastRequestedDictatorValue = sourceP.VerificationStatus.LastRequestedDictatorValue;
                this.VerificationStatus.ValidationRequestID = sourceP.VerificationStatus.ValidationRequestID;
            }

            this.IsEligible = sourceP.IsEligible;
            this.VendorPayerId = sourceP.VendorPayerId;
            this.PiId = sourceP.PiId;

            this.InsuredAddress = sourceP.InsuredAddress;
            this.InsuredPhone = sourceP.InsuredPhone;
            this.InsuredEmploymentAddress = sourceP.InsuredEmploymentAddress;
            this.InsuredEmploymentName = sourceP.InsuredEmploymentName;
            this.IsDeleted = sourceP.IsDeleted;
        }
    }

    public class PayersDto : DtoBase
    {
        public PayersDto()
        {
            this.Payers = new List<PayerDto>();
        }

        public List<PayerDto> Payers { get; set; }
    }
}
