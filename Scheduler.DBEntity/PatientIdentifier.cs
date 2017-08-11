using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.DBEntity
{
    public class PatientIdentifier : EntityBase
    {
        public string Identifier { get; set; }
        public string Source { get; set; }


        public int? Sequence { get; set; }
        public long PatientId { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateUser { get; set; }

        public string ExtIdentifierSource { get; set; }
        public string ExtIdentifierSourceId { get; set; }

        public PatientIdentifier()
        {
        }

        //public static PatientIdentifierDto Convert2Dto(PatientIdentifier patientIdentifier)
        //{
        //    return new PatientIdentifierDto
        //    {
        //        Id = (int)patientIdentifier.Id,
        //        PatientId = patientIdentifier.PatientId,
        //        Identifier = patientIdentifier.Identifier,
        //        Source = patientIdentifier.Source,
        //        ExtIdentifierSourceId = patientIdentifier.ExtIdentifierSourceId,
        //        ExtIdentifierSource = patientIdentifier.ExtIdentifierSource,
        //        Sequence = patientIdentifier.Sequence,
        //        IsDeleted = patientIdentifier.IsDeleted,
        //        IsActive = patientIdentifier.IsActive,
        //        CreateDate = patientIdentifier.CreateDate,
        //        CreateUser = patientIdentifier.CreateUser

        //    };
        //}

        //internal static PatientIdentifier ExtractFromDto(PatientIdentifierDto patientIdentifier)
        //{
        //    return new PatientIdentifier
        //    {
        //        Id = patientIdentifier.Id,
        //        PatientId = patientIdentifier.PatientId,
        //        Identifier = patientIdentifier.Identifier,
        //        Source = patientIdentifier.Source,
        //        ExtIdentifierSource = patientIdentifier.ExtIdentifierSource,
        //        ExtIdentifierSourceId = patientIdentifier.ExtIdentifierSourceId,
        //        Sequence = patientIdentifier.Sequence,
        //        IsDeleted = patientIdentifier.IsDeleted,
        //        IsActive = patientIdentifier.IsActive,
        //        CreateDate = patientIdentifier.CreateDate,
        //        CreateUser = patientIdentifier.CreateUser

        //    };
        //}
    }
}
