using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Business.Entity
{
    public class PatientIdentifierDto : DtoBase
    {
        public int Id { get; set; }

        public string Identifier { get; set; }


        public string Source { get; set; }

        public string ExtIdentifierSource { get; set; }

        public string ExtIdentifierSourceId { get; set; }

        public int? Sequence { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsActive { get; set; }

        public long PatientId { get; set; }

        public DateTime CreateDate { get; set; }

        public string CreateUser { get; set; }

        public PatientIdentifierDto Copy()
        {
            return new PatientIdentifierDto()
            {
                Id = this.Id,
                PatientId = this.PatientId,
                Identifier = this.Identifier,
                Source = this.Source,
                ExtIdentifierSource = this.ExtIdentifierSource,
                ExtIdentifierSourceId = this.ExtIdentifierSourceId,
                Sequence = this.Sequence,
                IsDeleted = this.IsDeleted,
                IsActive = this.IsActive,
                CreateDate = this.CreateDate,
                CreateUser = this.CreateUser
            };
        }
    }

    public class PatientIdentifiersDto : DtoBase
    {
        public List<PatientIdentifierDto> Items { get; set; }
    }
}
