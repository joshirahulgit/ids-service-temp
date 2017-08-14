using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Business.Entity
{
    public class PatientGuarantorDto : DtoBase
    {
        public PatientGuarantorDto()
        {
        }

        public long Id { get; set; }

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

        public string Email { get; set; }

        public string TextForAudit
        {
            get
            {
                return String.Format("Name={0} {1}, Gender={2}, DOB={3}, Phone={4}",
                                     FirstName,
                                     LastName,
                                     Sex,
                                     DOB.ToShortDateString(),
                                     HomePhone);
            }
        }

        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }

        public string FullAddress
        {
            get
            {
                string result = string.Empty;
                result += string.IsNullOrEmpty(Address) ? string.Empty : Address + ", ";
                result += string.IsNullOrEmpty(Address2) ? string.Empty : Address2 + ", ";
                result += string.IsNullOrEmpty(City) ? string.Empty : City + ", ";
                result += string.IsNullOrEmpty(State) ? string.Empty : State + ", ";
                result += string.IsNullOrEmpty(Zip) ? string.Empty : Zip + ", ";
                return string.IsNullOrEmpty(result) ? result : result.Substring(0, result.Length - 1);
            }
        }

    }

    public class PatientGuarantorsDto : DtoBase
    {
        public IList<PatientGuarantorDto> PatientGuarantors { get; set; }
    }
}
