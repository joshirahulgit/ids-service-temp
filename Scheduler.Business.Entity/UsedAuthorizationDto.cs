using Scheduler.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Business.Entity
{
    public class UsedAuthorizationDto
    {
        public UsedAuthorizationDto(UsedAuthorizationDto auth)
        {
            Id = auth.Id;
            ProcedureId = auth.ProcedureId;
            PatientWalletId = auth.PatientWalletId;
            AppointmentId = auth.AppointmentId;
            Remaining = auth.Remaining;
            TotalUsed = auth.TotalUsed;
            Comment = auth.Comment;
            UsedDate = auth.UsedDate;
            Count = auth.Count;
            Used = auth.Used;
            PatientAuthorization = new PatientAuthorizationDto(auth.PatientAuthorization);
            EntityStatus = auth.EntityStatus;
        }

        public UsedAuthorizationDto()
        {

        }

        public long Id { get; set; }

        public string ProcedureId { get; set; }

        public long PatientWalletId { get; set; }

        public long AppointmentId { get; set; }

        public int Remaining { get; set; }

        public int TotalUsed { get; set; }

        public string Comment { get; set; }

        public DateTime UsedDate { get; set; }

        public int Count { get; set; }

        public int Used { get; set; }

        public PatientAuthorizationDto PatientAuthorization { get; set; }

        public EntityStatus EntityStatus { get; set; }

        public override string ToString()
        {
            if (PatientAuthorization != null)
                return string.Format("{0} ({1})", PatientAuthorization.Description, PatientAuthorization.ProcedureId);

            return string.Empty;
        }
    }

    public class UsedAuthorizationsDto : DtoBase
    {
        public IList<UsedAuthorizationDto> UsedAuthorizations { get; set; }
    }
}
