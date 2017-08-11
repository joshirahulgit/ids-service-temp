using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.DBEntity
{
    public class PatientGuarantor : EntityBase
    {
        public string PatientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Suffix { get; set; }
        public DateTime DOB { get; set; }
        public string Sex { get; set; }
        public string SSN { get; set; }
        public string RelationshipToPatient { get; set; }
        public string RelationshipToPatientDescription { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Email { get; set; }
        public string HomePhone { get; set; }
        public string EmploymentName { get; set; }
        public string EmploymentAddress { get; set; }
        public string EmploymentAddress2 { get; set; }
        public string EmploymentCity { get; set; }
        public string EmploymentState { get; set; }
        public string EmploymentZip { get; set; }
        public string EmploymentPhone { get; set; }
        public string ContactReason { get; set; }
        public long AppointmentId { get; set; }

        

        //public static PatientGuarantor ExtractFromDto(PatientGuarantorDto guarantor)
        //{
        //    PatientGuarantor r = new PatientGuarantor();
        //    r.Id = guarantor.Id;
        //    r.AppointmentId = guarantor.AppointmentId;
        //    r.FirstName = guarantor.FirstName;
        //    r.LastName = guarantor.LastName;
        //    r.Address = guarantor.Address;
        //    r.Address2 = guarantor.Address2;
        //    r.City = guarantor.City;
        //    r.Country = guarantor.Country;
        //    r.DOB = guarantor.DOB;
        //    r.HomePhone = guarantor.HomePhone;
        //    r.MiddleName = guarantor.MiddleName;
        //    r.PatientId = guarantor.PatientId;
        //    r.RelationshipToPatient = guarantor.RelationshipToPatient;
        //    //for possible future compatibility
        //    r.RelationshipToPatientDescription = guarantor.RelationshipToPatientDescription;
        //    r.Sex = guarantor.Sex;
        //    r.SSN = guarantor.SSN;
        //    r.State = guarantor.State;
        //    r.Suffix = guarantor.Suffix;
        //    r.Zip = guarantor.Zip;
        //    r.Email = guarantor.Email;

        //    r.EmploymentName = guarantor.EmploymentName;
        //    r.EmploymentAddress = guarantor.EmploymentAddress;
        //    r.EmploymentAddress2 = guarantor.EmploymentAddress2;
        //    r.EmploymentCity = guarantor.EmploymentCity;
        //    r.EmploymentState = guarantor.EmploymentState;
        //    r.EmploymentZip = guarantor.EmploymentZip;
        //    r.EmploymentPhone = guarantor.EmploymentPhone;
        //    r.ContactReason = guarantor.ContactReason;

        //    return r;
        //}

        //public static PatientGuarantorDto Convert2Dto(PatientGuarantor guarantor)
        //{
        //    PatientGuarantorDto r = new PatientGuarantorDto();
        //    r.Id = guarantor.Id;
        //    r.AppointmentId = guarantor.AppointmentId;
        //    r.FirstName = guarantor.FirstName;
        //    r.LastName = guarantor.LastName;
        //    r.Address = guarantor.Address;
        //    r.Address2 = guarantor.Address2;
        //    r.City = guarantor.City;
        //    r.Country = guarantor.Country;
        //    r.DOB = guarantor.DOB;
        //    r.HomePhone = guarantor.HomePhone;
        //    r.MiddleName = guarantor.MiddleName;
        //    r.PatientId = guarantor.PatientId;
        //    r.RelationshipToPatient = guarantor.RelationshipToPatient;
        //    r.RelationshipToPatientDescription = guarantor.RelationshipToPatientDescription;
        //    r.Sex = guarantor.Sex;
        //    r.SSN = guarantor.SSN;
        //    r.State = guarantor.State;
        //    r.Suffix = guarantor.Suffix;
        //    r.Email = guarantor.Email;
        //    r.Zip = guarantor.Zip;
        //    r.EmploymentName = guarantor.EmploymentName;
        //    r.EmploymentAddress = guarantor.EmploymentAddress;
        //    r.EmploymentAddress2 = guarantor.EmploymentAddress2;
        //    r.EmploymentCity = guarantor.EmploymentCity;
        //    r.EmploymentState = guarantor.EmploymentState;
        //    r.EmploymentZip = guarantor.EmploymentZip;
        //    r.EmploymentPhone = guarantor.EmploymentPhone;
        //    r.ContactReason = guarantor.ContactReason;

        //    return r;
        //}
    }
}
