using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Business.Entity
{
    public class PatientContactDto : DtoBase
    {
        public long ID { get; set; }

        public long PatientId { get; set; }

        public string ContactTypeId { get; set; }

        public string ContactTypeName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName
        {
            get { return (FirstName + " " + LastName).Trim(); }
        }
        public override string ToString()
        {
            return FullName + " " + ContactTypeName;
        }
        public string RelationshipId { get; set; }

        public string RelationshipName { get; set; }

        public string CType
        {
            get
            {
                if (!string.IsNullOrEmpty(ContactTypeId))
                {
                    if (ContactTypeId == "Other")
                    {
                        return ContactTypeName;
                    }
                    return ContactTypeId;
                }
                return string.Empty;
            }
        }
        public string RelationShip
        {
            get
            {
                if (!string.IsNullOrEmpty(RelationshipId))
                {
                    if (RelationshipId == "Other")
                    {
                        return RelationshipName;
                    }
                    return RelationshipId;
                }

                return string.Empty;
            }
        }

        public string PatientPhones
        {
            get
            {
                StringBuilder sb = new StringBuilder();

                if (!string.IsNullOrEmpty(Phone))
                    sb.AppendLine("T: " + Phone);

                if (!string.IsNullOrEmpty(Mobile))
                    sb.AppendLine("M: " + Mobile);

                return sb.ToString().Trim();
            }
        }

        public string Phone { get; set; }

        public string Mobile { get; set; }

        public string Email { get; set; }

        public string Fax { get; set; }

        public string Address { get; set; }

        public string EmployerName { get; set; }

        public string EmploymentAddress { get; set; }

        public string EmployerPhone { get; set; }

        public string Comment { get; set; }

        public string Address2 { get; set; }

        public string City { get; set; }

        public string EmploymentAddress2 { get; set; }

        public string EmploymentCity { get; set; }

        public string EmploymentState { get; set; }

        public string EmploymentZip { get; set; }

        public string State { get; set; }

        public string Zip { get; set; }

        public bool IsAuthorizedDelegate { get; set; }

        public string IsAuthorizedDelegateYN
        {
            get { return IsAuthorizedDelegate ? "Yes" : "No"; }
            set { }
        }

        public DateTime CreateDate { get; set; }

        public DateTime? ModifyDate { get; set; }

        public string TooltipText
        {
            get
            {
                StringBuilder sb = new StringBuilder();

                if (!string.IsNullOrEmpty(this.FullName))
                    sb.AppendLine(this.FullName);

                if (!string.IsNullOrEmpty(this.CType))
                    sb.AppendLine("Type: " + this.CType);

                if (!string.IsNullOrEmpty(this.RelationShip))
                    sb.AppendLine("Relation: " + this.RelationShip);

                if (!string.IsNullOrEmpty(this.PatientPhones))
                    sb.AppendLine(this.PatientPhones);

                if (!string.IsNullOrEmpty(this.Fax))
                    sb.AppendLine("F: " + this.Fax);

                if (!string.IsNullOrEmpty(this.Email))
                    sb.AppendLine(this.Email);

                string address = AddressDto.ConstructTwoLineAddress(Address, Address2, City, State, Zip);
                if (!string.IsNullOrEmpty(address))
                    sb.AppendLine(address);

                //employer address
                address = AddressDto.ConstructTwoLineAddress(EmploymentAddress, EmploymentAddress2, EmploymentCity, EmploymentState, EmploymentZip);

                if (!string.IsNullOrEmpty(address) || !string.IsNullOrEmpty(this.EmployerName))
                    sb.AppendLine("Employment: " + this.EmployerName);

                if (!string.IsNullOrEmpty(address))
                    sb.AppendLine(address);

                if (!string.IsNullOrEmpty(this.Comment))
                    sb.AppendLine(this.Comment);

                return sb.ToString().Trim();
            }
        }
    }

    public class PatientContactsDto : DtoBase
    {
        public PatientContactsDto()
        {
            PatientContacts = new List<PatientContactDto>();
        }

        public IList<PatientContactDto> PatientContacts { get; set; }
    }
}
