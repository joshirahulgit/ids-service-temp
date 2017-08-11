using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.DBEntity
{
    public class AppointmentResourcePhysician : AppointmentResource
    {
        public virtual String FirstName { get; set; }
        public virtual String LastName { get; set; }
        public virtual String MiddleName { get; set; }
        public virtual String Tag { get; set; }
        public virtual int? TypeId { get; set; }
        public virtual String NPINo { get; set; }
        public String Fax { get; set; }
        public String AbbadoxDictatorId { get; set; }
        public String Phone { get; set; }
        public virtual PhysicianSpecializationType Specialization { get; set; }
        public virtual bool IsAssigned2Scheduler { get; set; }

        public String Email { get; set; }
        public String SendTo { get; set; }
        public String LocationId { get; set; }
        public String Color { get; set; }
        public String UserId { get; set; }
        public bool EmailCopy { get; set; }
        public virtual WorkingSchedule WorkingSchedule { get; set; }

        public AppointmentResourcePhysician()
        {
            WorkingSchedule = new WorkingSchedule();
        }

        public override string DisplayText { get { return (LastName + ", " + FirstName).Trim(); } }

        public override AppointmentResource Clear()
        {
            return new AppointmentResourcePhysician() { Id = this.Id, ResourceType = this.ResourceType, Account = new Account(this.Account.Id), WorkingSchedule = new WorkingSchedule() };
        }

        //public override AppointmentResource Create(RepositoryLocator locator)
        //{
        //    //Here we can do some validation
        //    return locator.ResourceRepository.Create(this);
        //}

        //public override void Delete(RepositoryLocator locator)
        //{
        //    //Here we can do some validation
        //    locator.ResourceRepository.Remove(this);
        //}

        //public override AppointmentResource Update(RepositoryLocator locator, AppointmentResource updatedResource)
        //{
        //    AppointmentResourcePhysician updatedPhysician = updatedResource as AppointmentResourcePhysician;
        //    if (updatedPhysician == null)
        //        return null;

        //    //Here we can do some validation
        //    //this.FirstName      = updatedPhysician.FirstName;
        //    //this.LastName       = updatedPhysician.LastName;
        //    //this.NPINo          = updatedPhysician.NPINo;
        //    //this.Fax            = updatedPhysician.Fax;
        //    //this.Phone          = updatedPhysician.Phone;
        //    //this.Specialization = updatedPhysician.Specialization;
        //    this.Tag = updatedPhysician.Tag;
        //    this.TypeId = updatedPhysician.TypeId;
        //    this.LocationId = updatedPhysician.LocationId;
        //    this.Color = updatedPhysician.Color;
        //    this.WorkingSchedule = updatedPhysician.WorkingSchedule;

        //    locator.ResourceRepository.Update(this);

        //    return this;
        //}

        //public static AppointmentResourcePhysician ExtractFromDto(AppointmentResourcePhysicianDto dto)
        //{
        //    AppointmentResourcePhysician result = new AppointmentResourcePhysician();
        //    if (dto == null) return null;

        //    result.Account = new Account(dto.AccountId);
        //    result.Fax = dto.Fax;
        //    result.AbbadoxDictatorId = dto.AbbadoxDictatorId;
        //    result.FirstName = dto.FirstName;
        //    result.Id = dto.Id;
        //    result.LastName = dto.LastName;
        //    result.MiddleName = dto.MiddleName;
        //    result.Tag = dto.Tag;
        //    result.TypeId = dto.PhysTypeId;
        //    result.NPINo = dto.NPINo;
        //    result.Phone = dto.Phone;
        //    result.ResourceType = new AppointmentResourceType(dto.TypeId);
        //    result.Specialization = new PhysicianSpecializationType(dto.SpecializationId);
        //    result.Email = dto.Email;
        //    result.SendTo = dto.SendTo;
        //    result.EmailCopy = dto.EmailCopy;
        //    result.LocationId = dto.LocationId.HasValue ? dto.LocationId.ToString() : null;
        //    result.Color = dto.Color;
        //    result.IsAssigned2Scheduler = dto.IsAssigned2Scheduler;
        //    result.UserId = dto.UserId;
        //    result.WorkingSchedule = WorkingSchedule.ExtractFromDto(dto.WorkingSchedule);

        //    return result;
        //}

        //public static AppointmentResourcePhysician Create(RepositoryLocator locator, AppointmentResourcePhysicianDto operation)
        //{
        //    return null;
        //}
    }
}