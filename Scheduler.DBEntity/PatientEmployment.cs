using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.DBEntity
{
    public class PatientEmployment : EntityBase
    {
        public Address Address { get; set; }
        public string EmploymentStatus { get; set; }
        public string EmployerName { get; set; }
        public long PatientId { get; set; }

        //public static PatientEmployment ExtractFromDto(PatientEmploymentDto dto)
        //{
        //    PatientEmployment p = new PatientEmployment();
        //    p.Address = Address.ExtractFromDto(dto.Address);
        //    p.EmployerName = dto.EmployerName;
        //    p.EmploymentStatus = dto.EmploymentStatus;
        //    p.Id = dto.Id;
        //    p.PatientId = dto.PatientId;
        //    return p;
        //}

        //public static PatientEmploymentDto Convert2Dto(PatientEmployment patient)
        //{
        //    PatientEmploymentDto p = new PatientEmploymentDto();
        //    p.Address = Address.ConvertToDto(patient.Address);
        //    p.EmployerName = patient.EmployerName;
        //    p.EmploymentStatus = patient.EmploymentStatus;
        //    p.Id = patient.Id;
        //    p.PatientId = patient.PatientId;
        //    return p;
        //}

        //public PatientEmployment Create(RepositoryLocator locator, long patientId)
        //{
        //    PatientEmployment employment = locator.ResourceRepository.CreatePatientEmployment(patientId, this);
        //    return employment;
        //}

        //public PatientEmployment Update(RepositoryLocator locator, PatientEmployment employment, long id)
        //{
        //    this.Address = employment.Address;
        //    this.EmployerName = employment.EmployerName;
        //    this.EmploymentStatus = employment.EmploymentStatus;
        //    locator.ResourceRepository.UpdatePatientEmployment(this);
        //    return this;
        //}
    }
}
