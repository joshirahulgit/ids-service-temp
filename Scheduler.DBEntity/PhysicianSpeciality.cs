using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.DBEntity
{
    public class PhysicianSpeciality : EntityBase
    {
        public string Name { get; set; }
        public bool IsVisible { get; set; }
        public bool IsDeleted { get; set; }

        //public static PhysicianSpeciality ExtractFromDto(PhysicianSpecialityDto physicianSpecialityDto)
        //{
        //    PhysicianSpeciality r = new PhysicianSpeciality();
        //    r.Id = physicianSpecialityDto.Id;
        //    r.IsVisible = physicianSpecialityDto.IsVisible;
        //    r.Name = physicianSpecialityDto.Name;
        //    r.IsDeleted = physicianSpecialityDto.IsDeleted;
        //    return r;
        //}

        //public static PhysicianSpecialityDto Convert2Dto(PhysicianSpeciality speciality)
        //{
        //    PhysicianSpecialityDto r = new PhysicianSpecialityDto();

        //    r.Id = speciality.Id;
        //    r.IsVisible = speciality.IsVisible;
        //    r.Name = speciality.Name;
        //    r.IsDeleted = speciality.IsDeleted;
        //    return r;
        //}
    }
}
