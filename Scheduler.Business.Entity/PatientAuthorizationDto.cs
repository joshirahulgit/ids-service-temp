using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Business.Entity
{
    public class PatientAuthorizationDto
    {
        public enum SystemStatusEnum
        {
            InActive,
            Expired,
            Used,
            Active
        }
        private int _left;

        public PatientAuthorizationDto()
        {

        }

        public PatientAuthorizationDto(PatientAuthorizationDto patientAuthorization)
        {
            Id = patientAuthorization.Id;
            PatientId = patientAuthorization.PatientId;
            Date = patientAuthorization.Date;
            ProcedureId = patientAuthorization.ProcedureId;
            ProcedureDescription = patientAuthorization.ProcedureDescription;
            Payer = patientAuthorization.Payer;
            PayerId = patientAuthorization.PayerId;
            PayerIsGlobal = patientAuthorization.PayerIsGlobal;
            AuthorizationNumber = patientAuthorization.AuthorizationNumber;
            Effective = patientAuthorization.Effective;
            Expires = patientAuthorization.Expires;
            UnitGradation = patientAuthorization.UnitGradation;
            UnitVolume = patientAuthorization.UnitVolume;
            Count = patientAuthorization.Count;
            Used = patientAuthorization.Used;
            Left = patientAuthorization.Left;
            Description = patientAuthorization.Description;
            AuthReferringId = patientAuthorization.AuthReferringId;
            AuthReferringFirst = patientAuthorization.AuthReferringFirst;
            AuthReferringLast = patientAuthorization.AuthReferringLast;
            GlobalDescription = patientAuthorization.GlobalDescription;
            LocalDescription = patientAuthorization.LocalDescription;
            UserStatus = patientAuthorization.UserStatus;
            IsDeleted = patientAuthorization.IsDeleted;
            StatusReason = patientAuthorization.StatusReason;
            RemovalReason = patientAuthorization.RemovalReason;
        }

        public long Id { get; set; }

        public long PatientId { get; set; }

        public DateTime? Date { get; set; }

        public string ProcedureId { get; set; }

        public string Payer { get; set; }

        public string AuthorizationNumber { get; set; }

        public DateTime? Effective { get; set; }

        public DateTime? Expires { get; set; }

        public string UnitGradation { get; set; }

        public string UnitVolume { get; set; }

        public string AuthReferringFirst { get; set; }

        public string AuthReferringLast { get; set; }

        public int Count { get; set; }

        public int? Used { get; set; }

        public string GlobalDescription { get; set; }

        public string LocalDescription { get; set; }

        public int Left
        {
            get
            {
                if (Used.HasValue)
                    return Count - Used.Value;

                return _left;
            }
            set { _left = value; }
        }

        public string ProcedureDescription { get; set; }

        public string DisplayName
        {
            get
            {
                bool procDesc = false;
                bool procGlobalDesc = false;
                bool procLocalDesc = false;
                StringBuilder r = new StringBuilder();

                if (!string.IsNullOrEmpty(GlobalDescription))
                {
                    procGlobalDesc = true;
                    r.Append(GlobalDescription);
                }

                if (!string.IsNullOrEmpty(LocalDescription) && !procGlobalDesc)
                {
                    procLocalDesc = true;
                    r.Append(LocalDescription);
                }

                if (!string.IsNullOrEmpty(ProcedureDescription) && !procGlobalDesc && !procLocalDesc)
                    r.Append(ProcedureDescription);

                if (!string.IsNullOrEmpty(ProcedureId) && ProcedureId != "-1")
                    r.Append(string.Format(" ({0}) ", ProcedureId));

                return r.ToString();
            }
        }

        public string Status
        {
            get
            {
                //if (DateTime.Now > Expires) return "Expired";
                //else if (DateTime.Now < Effective) return "Inactive";
                //else if (Left >= 1) return UserStatus;// "Active"
                //else return "Used";
                switch (SystemStatus)
                {
                    case SystemStatusEnum.Active:
                        if (string.IsNullOrEmpty(UserStatus))
                            return "Active";

                        return UserStatus;
                    //                        return UserStatus ?? "Active";
                    default:
                        return Enum.GetName(typeof(SystemStatusEnum), SystemStatus);
                }
            }
        }

        public string Tooltip
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                if (!string.IsNullOrEmpty(Payer))
                    sb.AppendLine(string.Format("Payer: {0}", Payer));
                sb.AppendLine(string.Format("Auth #: {0}", AuthorizationNumber));
                if (!string.IsNullOrEmpty(Description))
                    sb.AppendLine(string.Format("Desc: {0}", Description));
                if (!string.IsNullOrEmpty(ProcedureId) && !string.IsNullOrEmpty(ProcedureDescription))
                    sb.AppendLine(string.Format("Proc: {0}{1}{2}", ProcedureId, Environment.NewLine, ProcedureDescription));
                sb.AppendLine(string.Format("Units: {0} {1}{2}", this.Count, this.UnitVolume,
                    string.IsNullOrEmpty(UnitGradation) ? "" : string.Format(" ({0})", UnitGradation)));
                sb.AppendLine(string.Format("Used: {0}, Left: {1}", this.Used, this.Left));
                if (Expires.HasValue || Effective.HasValue)
                    sb.AppendLine(string.Format("Valid{0}{1}",
                        Effective.HasValue ? " from " + Effective.Value.ToShortDateString() : "",
                        Expires.HasValue ? " till " + Expires.Value.ToShortDateString() : ""
                        ));
                if (!string.IsNullOrEmpty(this.AuthReferringId))
                    sb.AppendLine(string.Format("Referring: {0}", AuthReferringId));

                sb.Append(string.Format("Status: {0}",
                    this.IsDeleted ? "Removed " + RemovalReason : Status + (Status == "Denied " ? StatusReason : "")));

                return sb.ToString();
            }
        }

        public SystemStatusEnum SystemStatus
        {
            get
            {
                if (DateTime.Now > Expires) return SystemStatusEnum.Expired;
                else if (DateTime.Now < Effective) return SystemStatusEnum.InActive;
                else if (Left >= 1) return SystemStatusEnum.Active;// "Active"
                else return SystemStatusEnum.Used;
            }
        }

        public string Description { get; set; }

        public string AuthReferringId { get; set; }

        public long PayerId { get; set; }

        public bool PayerIsGlobal { get; set; }

        public string UserStatus { get; set; }

        public string RemovalReason { get; set; }

        public string StatusReason { get; set; }

        public bool IsDeleted { get; set; }
    }

    public class PatientAuthorizationsDto : DtoBase
    {
        public PatientAuthorizationsDto()
        {
            PatientAuthorizations = new List<PatientAuthorizationDto>();
        }

        public IList<PatientAuthorizationDto> PatientAuthorizations { get; set; }
    }
}
