using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.DBEntity
{
    public class AppointmentResourceModality : AppointmentResource
    {
        public AppointmentResourceModality()
        {
            WorkingSchedule = new WorkingSchedule();
        }

        public String RoomName { get; set; }
        public RoomType RoomType { get; set; }
        public ModalityType ModalityType { get; set; }
        public String AccessionNumber { get; set; }
        public TimeSpan Estimate { get; set; }
        public ResourceLocation Location { get; set; }
        public WorkingSchedule WorkingSchedule { get; set; }
        public int? VirtualRoomId { get; set; }
        public SchedulerModalityVirtualRoom SchedulerModalityVirtualRoom { get; set; }
        public bool IsMammographyResource { get; set; }
        public bool IsOnlineRoom { get; set; }
        public bool CreateOrder { get; set; }
        public bool CreateEncounter { get; set; }
        public bool IsActive { get; set; }

        public override string DisplayText
        {
            get { return RoomName; }
        }

        //public override AppointmentResource Create(RepositoryLocator locator)
        //{
        //    //Here we can perform before crate actions
        //    return locator.ResourceRepository.Create(this);
        //}

        //public override void Delete(RepositoryLocator locator)
        //{
        //    //Here we can do before delete actions
        //    locator.ResourceRepository.Remove(this);
        //}

        //public override AppointmentResource Update(RepositoryLocator locator, AppointmentResource updatedResource)
        //{
        //    AppointmentResourceModality updatedModality = updatedResource as AppointmentResourceModality;
        //    if (updatedModality == null)
        //        return null;

        //    this.RoomName = updatedModality.RoomName;
        //    this.RoomType = updatedModality.RoomType;
        //    this.ModalityType = updatedModality.ModalityType;
        //    this.AccessionNumber = updatedModality.AccessionNumber;
        //    this.Estimate = updatedModality.Estimate;
        //    this.Location = updatedModality.Location;
        //    this.WorkingSchedule = updatedModality.WorkingSchedule;
        //    this.VirtualRoomId = updatedModality.VirtualRoomId;
        //    this.SchedulerModalityVirtualRoom = updatedModality.SchedulerModalityVirtualRoom;
        //    this.IsMammographyResource = updatedModality.IsMammographyResource;
        //    this.IsActive = updatedModality.IsActive;
        //    this.IsOnlineRoom = updatedModality.IsOnlineRoom;
        //    this.CreateOrder = updatedModality.CreateOrder;
        //    this.CreateEncounter = updatedModality.CreateEncounter;

        //    locator.ResourceRepository.Update(this);

        //    return this;
        //}

        public override AppointmentResource Clear()
        {
            return new AppointmentResourceModality()
            {
                Id = this.Id,
                ResourceType = this.ResourceType,
                Account = new Account(this.Account.Id),
                Location = new ResourceLocation(this.Location.Id),
                WorkingSchedule = new WorkingSchedule(),
                RoomType = new RoomType(this.RoomType.Id),
                ModalityType = new ModalityType(this.ModalityType.Id),
                AccessionNumber = string.Empty,
                RoomName = string.Empty,
                Estimate = new TimeSpan(0),
                VirtualRoomId = null,
                SchedulerModalityVirtualRoom = null,
                IsMammographyResource = false,
                IsOnlineRoom = false,
                CreateEncounter = false,
                CreateOrder = false,
                IsActive = false
            };
        }

        //public static AppointmentResourceModality ExtractFromDto(AppointmentResourceModalityDto dto)
        //{
        //    AppointmentResourceModality res = new AppointmentResourceModality();
        //    res.AccessionNumber = dto.AccessionNumber;
        //    res.Account = new Account(dto.AccountId);
        //    res.Estimate = dto.Estimate;
        //    res.Id = dto.Id;
        //    res.Location = new ResourceLocation(dto.LocationID);
        //    res.WorkingSchedule = WorkingSchedule.ExtractFromDto(dto.WorkingSchedule);
        //    res.ModalityType = new ModalityType(dto.ModalityTypeId);
        //    res.ResourceType = new AppointmentResourceType(dto.TypeId);
        //    res.RoomName = dto.RoomName;
        //    res.RoomType = new RoomType(dto.RoomTypeId);
        //    res.VirtualRoomId = dto.VirtualRoomId;
        //    res.SchedulerModalityVirtualRoom = dto.SchedulerModalityVirtualRoom.ToSchedulerModalityVirtualRoom();
        //    res.IsMammographyResource = dto.IsMammographyResource;
        //    res.IsOnlineRoom = dto.IsOnlineRoom;
        //    res.CreateEncounter = dto.CreateEncounter;
        //    res.CreateOrder = dto.CreateOrder;
        //    res.IsActive = dto.IsActive;
        //    return res;
        //}

        public void SetResourceTypeAndId(AppointmentResourceType appointmentResourceType, long modalityId)
        {
            this.ResourceType = appointmentResourceType;
            Id = modalityId;
        }

        public void SetRoomType(RoomType rp)
        {
            this.RoomType = new RoomType(rp.Id, rp.Name);
        }

        public void SetCreateOrder(bool createOrder)
        {
            CreateOrder = createOrder;
        }

        public void SetModalityType(long modalityTypeId)
        {
            this.ModalityType = new ModalityType(modalityTypeId);
        }

        public void SetLocation(long parameterLocationId)
        {
            this.Location = new ResourceLocation(parameterLocationId);
        }
    }
}
