using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Business.Entity
{
    public class RequestAppointmentDto : DtoBase
    {
        public long Id { get; set; }

        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String MiddleName { get; set; }
        public String Address1 { get; set; }
        public String Address2 { get; set; }
        public String ZipCode { get; set; }
        public String City { get; set; }
        public String State { get; set; }
        public String Phone { get; set; }
        public String SSN { get; set; }
        public DateTime Dob { get; set; }
        public String Gender { get; set; }
        public String Email { get; set; }
        public String RecordNumber { get; set; }//For MRN
        public string Mobile { get; set; }
        public bool HasAttachments { get; set; }
        public string AuthorizationNo { get; set; }
        public string InsuranceCompanyName { get; set; }
        public string PolicyNumber { get; set; }
        public string RequestedTimeRange { get; set; }
        public DateTime RequestedDate { get; set; }
        public DateTime RequestedDateTime { get; set; }
        public bool IsLinkingCompleted { get; set; }

        public string GetFullName()
        {
            return string.Format("{0} {1}", this.FirstName, this.LastName);
        }

        public string GetAllContactNumbers()
        {
            bool hasOne = false;
            string no = string.Empty;
            if (!string.IsNullOrEmpty(this.Phone))
            {
                no = no + this.Phone;
                hasOne = true;
            }
            if (!string.IsNullOrEmpty(this.Mobile))
            {
                if (hasOne)
                {
                    no += ", ";
                }
                no += this.Mobile;
            }
            return no;
        }

    }

    public class RequestAppointmentsDto : DtoBase
    {
        public List<RequestAppointmentDto> Items { get; set; }
    }
}
