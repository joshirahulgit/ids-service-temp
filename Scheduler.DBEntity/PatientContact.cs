using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.DBEntity
{
    public class PatientContact : EntityBase
    {
        public long PatientId { get; set; }
        public string ContactTypeId { get; set; }
        public string ContactTypeName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string RelationshipId { get; set; }
        public string RelationshipName { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Fax { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string EmployerName { get; set; }
        public string EmploymentAddress { get; set; }
        public string EmploymentAddress2 { get; set; }
        public string EmploymentCity { get; set; }
        public string EmploymentState { get; set; }
        public string EmploymentZip { get; set; }
        public string EmployerPhone { get; set; }
        public string Comment { get; set; }
        public bool IsAuthorizedDelegate { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? ModifyDate { get; set; }

        public void InitializeId(int id)
        {
            if (Id != 0)
                throw new InvalidOperationException(string.Format("Contact {0} {1}, id={2} cannot be re-initialized with ID ({3})", this.FirstName, LastName, Id, id));
            Id = id;
        }

    }
}
