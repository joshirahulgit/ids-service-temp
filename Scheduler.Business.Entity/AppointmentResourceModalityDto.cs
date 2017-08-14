using Scheduler.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Business.Entity
{
    public class AppointmentResourceModalityDto : AppointmentResourceDto
    {
        public String RoomName { get; set; }
        public long RoomTypeId { get; set; }
        public long ModalityTypeId { get; set; }
        public String AccessionNumber { get; set; }
        public TimeSpan Estimate { get; set; }
        public long LocationID { get; set; }
        public WorkingScheduleDto WorkingSchedule { get; set; }
        public int? VirtualRoomId { get; set; }
        public SchedulerModalityVirtualRoomDto SchedulerModalityVirtualRoom { get; set; }
        public bool IsMammographyResource { get; set; }

        public bool IsOnlineRoom { get; set; }

        public bool CreateOrder { get; set; }

        public bool CreateEncounter { get; set; }

        public bool IsActive { get; set; }

        public string LocationName { get; set; }
        public string ModalityType { get; set; }

        public AppointmentResourceModalityDto()
            : base((long)ResourceTypes.Room)
        {
            WorkingSchedule = new WorkingScheduleDto();
        }
        public string Text
        {
            get { return RoomName; }
        }

        public AppointmentResourceModalityDto(long id, long accountId)
            : this()
        {
            this.Id = id;
            this.AccountId = accountId;
        }

        protected override AppointmentResourceDto DoClone()
        {
            return this.MemberwiseClone() as AppointmentResourceDto;
        }

        public override string DisplayText
        {
            get { return RoomName; }
        }

        public string VirtualGroupColor
        {
            get
            {
                string c = "#00FFFFFF";
                if (this.VirtualRoomId.HasValue)
                {
                    var t = this.VirtualRoomId.Value.ToString().GetHashCode();
                    var tt = MD5Core.GetHash(this.VirtualRoomId.Value.ToString());
                    c = string.Format("#{0:X2}{1:X2}{2:X2}{3:X2}", 255, tt[0], tt[1], tt[2]);
                }

                return c;
            }
            //set { _virtualGroupColor = value; }
        }

        protected override void DoCopy(AppointmentResourceDto dest)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return RoomName;
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        public bool Equals(AppointmentResourceModalityDto other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.Id == this.Id && other.RoomTypeId == this.RoomTypeId;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(AppointmentResourceModalityDto)) return false;
            return Equals((AppointmentResourceModalityDto)obj);
        }
    }

    public class AppointmentResourceModalitiesDto : DtoBase
    {
        public AppointmentResourceModalitiesDto()
        {
            Modalities = new List<AppointmentResourceModalityDto>();
        }

        public IList<AppointmentResourceModalityDto> Modalities { get; set; }
    }
}
