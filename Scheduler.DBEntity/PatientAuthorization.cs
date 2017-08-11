using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.DBEntity
{
    public class PatientAuthorization
    {
        public long Id { get; set; }
        public long PatientId { get; set; }
        public DateTime? Date { get; set; }
        public string Description { get; set; }
        public string ProcedureId { get; set; }
        public string Payer { get; set; }
        public string AuthorizationNumber { get; set; }
        public string GlobalDescription { get; set; }
        public string LocalDescription { get; set; }
        public DateTime? Effective { get; set; }
        public DateTime? Expires { get; set; }
        public int Count { get; set; }
        public int? Used { get; set; }
        public string UnitGradation { get; set; }
        public string UnitVolume { get; set; }
        public string AuthReferringId { get; set; }
        public string AuthReferringFirst { get; set; }
        public string AuthReferringLast { get; set; }
        public string ProcedureDescription { get; set; }
        public long PayerId { get; set; }
        public bool PayerIsGlobal { get; set; }
        public string UserStatus { get; set; }
        public string RemovalReason { get; set; }
        public string StatusReason { get; set; }
        public bool IsDeleted { get; set; }

        public int Left
        {
            get
            {
                int used = Used ?? 0;
                return Count - used;
            }
        }

        //public static PatientAuthorization ExtractFromDto(PatientAuthorizationDto auth)
        //{
        //    PatientAuthorization r = new PatientAuthorization();

        //    r.AuthReferringId = auth.AuthReferringId;
        //    r.UnitGradation = auth.UnitGradation;
        //    r.UnitVolume = auth.UnitVolume;
        //    r.Id = auth.Id;
        //    r.Description = auth.Description;
        //    r.AuthorizationNumber = auth.AuthorizationNumber;
        //    r.Count = auth.Count;
        //    r.Date = auth.Date;
        //    r.Effective = auth.Effective;
        //    r.Expires = auth.Expires;
        //    r.PatientId = auth.PatientId;
        //    r.Payer = auth.Payer;
        //    r.ProcedureId = auth.ProcedureId;
        //    r.ProcedureDescription = auth.ProcedureDescription;
        //    r.Used = auth.Used;
        //    r.PayerId = auth.PayerId;
        //    r.PayerIsGlobal = auth.PayerIsGlobal;
        //    r.UserStatus = auth.UserStatus;
        //    r.StatusReason = auth.StatusReason;
        //    r.RemovalReason = auth.RemovalReason;
        //    r.IsDeleted = auth.IsDeleted;
        //    r.AuthReferringFirst = auth.AuthReferringFirst;
        //    r.AuthReferringLast = auth.AuthReferringLast;
        //    r.GlobalDescription = auth.GlobalDescription;
        //    r.LocalDescription = auth.LocalDescription;
        //    return r;
        //}

        //public static PatientAuthorizationDto Convert2Dto(PatientAuthorization auth)
        //{
        //    PatientAuthorizationDto r = new PatientAuthorizationDto();
        //    r.UnitVolume = auth.UnitVolume;
        //    r.UnitGradation = auth.UnitGradation;
        //    r.Id = auth.Id;
        //    r.AuthReferringId = auth.AuthReferringId;
        //    r.Description = auth.Description;
        //    r.AuthorizationNumber = auth.AuthorizationNumber;
        //    r.Count = auth.Count;
        //    r.Date = auth.Date;
        //    r.Effective = auth.Effective;
        //    r.Expires = auth.Expires;
        //    r.Left = auth.Left;
        //    r.PatientId = auth.PatientId;
        //    r.Payer = auth.Payer;
        //    r.ProcedureId = auth.ProcedureId;
        //    r.ProcedureDescription = auth.ProcedureDescription;
        //    r.Used = auth.Used ?? 0;
        //    r.PayerId = auth.PayerId;
        //    r.PayerIsGlobal = auth.PayerIsGlobal;
        //    r.UserStatus = auth.UserStatus;
        //    r.RemovalReason = auth.RemovalReason;
        //    r.StatusReason = auth.StatusReason;
        //    r.IsDeleted = auth.IsDeleted;
        //    r.AuthReferringFirst = auth.AuthReferringFirst;
        //    r.AuthReferringLast = auth.AuthReferringLast;
        //    r.GlobalDescription = auth.GlobalDescription;
        //    r.LocalDescription = auth.LocalDescription;
        //    return r;
        //}

        //public long Use(RepositoryLocator locator, long appointmentId, int itemCount, string comment)
        //{
        //    if (this.Left - itemCount < 0)
        //        throw new SchedulerException(SchedulerExceptionType.AuthorizationActivation, "You have no more authorizations units.");

        //    if (this.IsDeleted)
        //        throw new SchedulerException(SchedulerExceptionType.AuthorizationActivation,
        //                                     "You can't use deleted authorization");


        //    return locator.ResourceRepository.ActivateAuthorizationUnit(itemCount, appointmentId, comment, this);
        //}

        //public PatientAuthorization Update(RepositoryLocator locator, PatientAuthorizationDto authObj)
        //{
        //    this.AuthorizationNumber = authObj.AuthorizationNumber;
        //    this.AuthReferringId = authObj.AuthReferringId;
        //    this.Count = authObj.Count;
        //    this.Date = authObj.Date;
        //    this.Description = authObj.Description;
        //    this.Effective = authObj.Effective;
        //    this.Expires = authObj.Expires;
        //    this.PatientId = authObj.PatientId;
        //    this.Payer = authObj.Payer;
        //    this.ProcedureId = authObj.ProcedureId;
        //    this.ProcedureDescription = authObj.ProcedureDescription;
        //    this.UnitGradation = authObj.UnitGradation;
        //    this.UnitVolume = authObj.UnitVolume;
        //    this.Used = authObj.Used;
        //    this.PayerId = authObj.PayerId;
        //    this.PayerIsGlobal = authObj.PayerIsGlobal;
        //    this.UserStatus = authObj.UserStatus;
        //    this.RemovalReason = authObj.RemovalReason;
        //    this.StatusReason = authObj.StatusReason;
        //    this.IsDeleted = authObj.IsDeleted;

        //    PatientAuthorization updated = locator.ResourceRepository.UpdatePatientAuthorization(this);

        //    return updated;
        //}

    }
}
