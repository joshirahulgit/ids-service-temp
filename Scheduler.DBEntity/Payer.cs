using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.DBEntity
{
    public class Payer : EntityBase
    {
        public class PayerStatus
        {
            public PayerStatus(String info, bool isComplete, bool isSuccessful, String userText, DateTime? verificationDate, String lastRequestDictatorKey, String lastRequestDictatorValue, String validationReqId)
            {
                this.AdditionalInfo = info;
                this.IsComplete = isComplete;
                this.IsSuccessful = isSuccessful;
                this.UserText = userText;
                this.VerificationDateTime = verificationDate;
                this.LastRequestedDictatorKey = lastRequestDictatorKey;
                this.LastRequestedDictatorValue = lastRequestDictatorValue;
                this.ValidationRequestID = validationReqId;
            }

            //public static PayerStatus ExtractFromDto(PayerDto.PayerStatusDto dto)
            //{
            //    return dto == null ? null :
            //        new PayerStatus(dto.AdditionalInfo,
            //                        dto.IsComplete,
            //                        dto.IsSuccessful,
            //                        dto.UserText,
            //                        dto.VerificationDateTime,
            //                        dto.LastRequestedDictatorKey,
            //                        dto.LastRequestedDictatorValue,
            //                        dto.ValidationRequestID);
            //}

            public string AdditionalInfo { get; private set; }
            public bool IsComplete { get; private set; }
            public bool IsSuccessful { get; private set; }
            public string UserText { get; private set; }
            public DateTime? VerificationDateTime { get; private set; }
            public String LastRequestedDictatorKey { get; private set; }
            public String LastRequestedDictatorValue { get; private set; }
            public String ValidationRequestID { get; private set; }
        }

        public UInt32 Level { get; set; }
        public String Address { get; set; }
        public String State { get; set; }
        public String Phone { get; set; }
        public String PolicyNumber { get; set; }
        public String ProductName { get; set; }
        public String Provider { get; set; }
        public String InsuredFirstName { get; set; }
        public DateTime InsuredDOB { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public String RelationShip { get; set; }
        public String Fax { get; set; }
        public String GroupNumber { get; set; }
        public String NPINumber { get; set; }
        public String LastName { get; set; }
        public long PayerId { get; set; }
        public String Gender { get; set; }
        public String Comment { get; set; }
        public String PayerName { get; set; }
        public AppointmentResourcePatient Patient { get; set; }
        public int EntityStatus { get; set; }
        public PayerStatus VerificationStatus { get; set; }
        public bool IsGlobal { get; set; }
        public string WebSite { get; set; }
        public string Address2 { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        //for quicklook
        public bool? IsEligible { get; set; }
        public String VendorPayerId { get; set; }
        public int PiId { get; set; }
        public String InsuredCity { get; set; }
        public String InsuredState { get; set; }
        public String InsuredZip { get; set; }
        public String InsuredAddress { get; set; }
        public String InsuredPhone { get; set; }
        public String InsuredEmploymentName { get; set; }
        public String InsuredEmploymentAddress { get; set; }
        public int PayerAddressId { get; set; }
        public bool IsDeleted { get; set; }

        public void SetPiId(int pId)
        {
            PiId = pId;
        }
        //public Payer Create(RepositoryLocator locator, long patientId, String patientNumber)
        //{
        //    Payer newPayer = locator.ResourceRepository.CreatePayer(patientId, patientNumber, this);
        //    newPayer.EntityStatus = (int)Scheduler.Common.Enums.EntityStatus.NotModified;
        //    return newPayer;
        //}

        //public Payer Update(RepositoryLocator locator, Payer updatedPayer, long patientId, string patientNumber)
        //{
        //    this.Address = updatedPayer.Address;
        //    this.EntityStatus = (int)Scheduler.Common.Enums.EntityStatus.NotModified;
        //    this.Fax = updatedPayer.Fax;
        //    this.Gender = updatedPayer.Gender;
        //    this.GroupNumber = updatedPayer.GroupNumber;
        //    this.InsuredDOB = updatedPayer.InsuredDOB;
        //    this.InsuredFirstName = updatedPayer.InsuredFirstName;
        //    this.LastName = updatedPayer.LastName;
        //    this.Level = updatedPayer.Level;
        //    this.NPINumber = updatedPayer.NPINumber;
        //    this.Patient = updatedPayer.Patient;
        //    this.PayerName = updatedPayer.PayerName;
        //    this.Phone = updatedPayer.Phone;
        //    this.PolicyNumber = updatedPayer.PolicyNumber;
        //    this.ProductName = updatedPayer.ProductName;
        //    this.Provider = updatedPayer.Provider;
        //    this.RelationShip = updatedPayer.RelationShip;
        //    this.IsGlobal = updatedPayer.IsGlobal;
        //    this.WebSite = updatedPayer.WebSite;
        //    this.State = updatedPayer.State;
        //    this.PayerId = updatedPayer.PayerId;
        //    this.Address2 = updatedPayer.Address2;
        //    this.ZipCode = updatedPayer.ZipCode;
        //    this.City = updatedPayer.City;

        //    this.IsEligible = updatedPayer.IsEligible;
        //    this.VendorPayerId = updatedPayer.VendorPayerId;

        //    this.InsuredCity = updatedPayer.InsuredCity;
        //    this.InsuredState = updatedPayer.InsuredState;
        //    this.InsuredZip = updatedPayer.InsuredZip;
        //    this.InsuredAddress = updatedPayer.InsuredAddress;
        //    this.InsuredPhone = updatedPayer.InsuredPhone;
        //    this.InsuredEmploymentName = updatedPayer.InsuredEmploymentName;
        //    this.InsuredEmploymentAddress = updatedPayer.InsuredEmploymentAddress;
        //    this.PayerAddressId = updatedPayer.PayerAddressId;

        //    Payer updated = locator.ResourceRepository.UpdatePayer(patientId, patientNumber, this);
        //    return updated;
        //}

        //public void Delete(RepositoryLocator locator, long patientId, string patientNumber)
        //{
        //    locator.ResourceRepository.RemovePayer(patientId, patientNumber, this);
        //}

        //internal static Payer ExtractFromDto(Common.DataTransferObjects.Appointment.PayerDto p)
        //{
        //    Payer res = new Payer();
        //    res.State = p.State;
        //    res.Address = p.Address;
        //    res.Fax = p.Fax;
        //    res.Gender = p.Gender;
        //    res.GroupNumber = p.GroupNumber;
        //    res.Id = p.InsuranceId;
        //    res.InsuredDOB = p.InsuredDOB;
        //    res.InsuredFirstName = p.InsuredFirstName;
        //    res.LastName = p.LastName;
        //    res.NPINumber = p.NPINumber;
        //    res.Phone = p.Phone;
        //    res.PolicyNumber = p.PolicyNumber;
        //    res.ProductName = p.ProductName;
        //    res.Provider = p.Provider;
        //    res.RelationShip = p.RelationShip;
        //    res.PayerName = p.PayerName;
        //    res.Level = p.LevelIndex;
        //    res.EntityStatus = (int)p.Status;
        //    res.VerificationStatus = PayerStatus.ExtractFromDto(p.VerificationStatus);
        //    res.IsGlobal = p.IsGlobal;
        //    res.WebSite = p.WebSite;
        //    res.PayerId = p.PayerId;
        //    res.Address2 = p.Address2;
        //    res.ZipCode = p.ZipCode;
        //    res.City = p.City;
        //    res.Comment = p.Comment;
        //    res.ExpirationDate = p.ExpirationDate;

        //    res.IsEligible = p.IsEligible;
        //    res.VendorPayerId = p.VendorPayerId;
        //    res.PiId = p.PiId;

        //    res.InsuredCity = p.InsuredCity;
        //    res.InsuredState = p.InsuredState;
        //    res.InsuredZip = p.InsuredZip;
        //    res.InsuredAddress = p.InsuredAddress;
        //    res.InsuredPhone = p.InsuredPhone;
        //    res.InsuredEmploymentName = p.InsuredEmploymentName;
        //    res.InsuredEmploymentAddress = p.InsuredEmploymentAddress;
        //    res.PayerAddressId = p.PayerAddressId;
        //    res.IsDeleted = p.IsDeleted;

        //    return res;
        //}

        public void SetVerificationInfo(PayerStatus status)
        {
            if (status != null)
                this.VerificationStatus = status;
        }

        public void SetLevel(int index)
        {
            Level = (uint)index;
        }

        public void SetPayerName(string payerName)
        {
            this.PayerName = payerName;
        }
    }
}
