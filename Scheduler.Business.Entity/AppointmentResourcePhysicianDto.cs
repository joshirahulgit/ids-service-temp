using Scheduler.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Business.Entity
{
    public class AppointmentResourcePhysicianDto : AppointmentResourceDto
    {
        public String FirstName { get; set; }

        public String LastName { get; set; }

        public override string DisplayText
        {
            get { return FullName; }
        }

        public String NPINo { get; set; }

        public String UserId { get; set; }

        public long SpecializationId { get; set; }

        public string Phone { get; set; }

        public string Fax { get; set; }

        public String Email { get; set; }

        public String SendTo { get; set; }

        public bool EmailCopy { get; set; }

        public String MiddleName { get; set; }

        private string _tag;

        public virtual String Tag
        {
            get
            {
                return string.IsNullOrEmpty(_tag) ?
                    string.IsNullOrEmpty(FirstName) && string.IsNullOrEmpty(LastName) ? null :
                        (LastName + ", " + FirstName).Trim()
                    : _tag;
            }
            set { _tag = value; }
        }

        public virtual int? PhysTypeId { get; set; }

        public virtual bool IsAssigned2Scheduler { get; set; }

        public String FullName { get { return (LastName + ", " + FirstName).Trim(); } }

        public long? LocationId { get; set; }

        public string AbbadoxDictatorId { get; set; }

        public string Color { get; set; }

        public WorkingScheduleDto WorkingSchedule { get; set; }

        public AppointmentResourcePhysicianDto()
            : base((long)ResourceTypes.Physician)
        {
            WorkingSchedule = new WorkingScheduleDto();
        }

        public AppointmentResourcePhysicianDto(long id, long accountId)
            : this()
        {
            this.Id = id;
            this.AccountId = accountId;
        }


        public override string ToString()
        {
            return FullName;
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        public bool Equals(AppointmentResourcePhysicianDto other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.Id == this.Id;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(AppointmentResourcePhysicianDto)) return false;
            return Equals((AppointmentResourcePhysicianDto)obj);
        }

        protected override void DoCopy(AppointmentResourceDto dest)
        {
            throw new NotImplementedException();
        }

        protected override AppointmentResourceDto DoClone()
        {
            return this.MemberwiseClone() as AppointmentResourceDto;
        }
    }
}
