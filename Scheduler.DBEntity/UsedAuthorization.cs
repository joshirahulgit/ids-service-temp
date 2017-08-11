using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.DBEntity
{
    public class UsedAuthorization
    {
        public long Id { get; set; }
        public string ProcedureId { get; set; }
        public string Comment { get; set; }
        public long PatientWalletId { get; set; }
        public long AppointmentId { get; set; }
        public int Count { get; set; }
        public DateTime UsedDate { get; set; }
        public int Used { get; set; }
        public int TotalUsed { get; set; }
        public int Remaining { get; set; }
        public PatientAuthorization PatientAuthorization { get; set; }
        public Core.EntityStatus EntityStatus { get; private set; }

        //public static UsedAuthorization ExtractFromDto(UsedAuthorizationDto authObj)
        //{
        //    UsedAuthorization r = new UsedAuthorization();
        //    r.AppointmentId = authObj.AppointmentId;
        //    r.Count = authObj.Count;
        //    r.Comment = authObj.Comment;
        //    r.Id = authObj.Id;
        //    r.PatientWalletId = authObj.PatientWalletId;
        //    r.ProcedureId = authObj.ProcedureId;
        //    r.UsedDate = authObj.UsedDate;
        //    r.Used = authObj.Used;
        //    r.Remaining = authObj.Remaining;
        //    r.TotalUsed = authObj.TotalUsed;
        //    r.EntityStatus = authObj.EntityStatus;
        //    if (authObj.PatientAuthorization != null)
        //        r.PatientAuthorization = PatientAuthorization.ExtractFromDto(authObj.PatientAuthorization);
        //    return r;
        //}

        //public static UsedAuthorization ExtractFromReader(SafeDataReader sr)
        //{
        //    UsedAuthorization r = new UsedAuthorization();
        //    r.Id = sr.GetInt32("Id");
        //    r.PatientWalletId = sr.GetInt32("PatientWalletId");
        //    r.ProcedureId = sr.GetNullableString("ProcedureId");
        //    r.Used = sr.GetInt32("Used");
        //    r.UsedDate = sr.GetDateTime("UsedDate");
        //    r.AppointmentId = sr.GetInt32("AppId");
        //    r.Comment = sr.GetNullableString("Comment");
        //    r.TotalUsed = sr.GetInt32("TotalUsed");
        //    r.Remaining = sr.GetInt32("Remaining");
        //    r.EntityStatus = EntityStatus.NotModified;
        //    return r;
        //}

        public void LoadPatientWallet(PatientAuthorization auth)
        {
            PatientAuthorization = auth;
        }

        //public static UsedAuthorizationDto Convert2Dto(UsedAuthorization auth)
        //{
        //    UsedAuthorizationDto r = new UsedAuthorizationDto();
        //    r.Comment = auth.Comment;
        //    r.Count = auth.Count;
        //    r.Id = auth.Id;
        //    r.PatientWalletId = auth.PatientWalletId;
        //    r.ProcedureId = auth.ProcedureId;
        //    r.UsedDate = auth.UsedDate;
        //    r.Used = auth.Used;
        //    r.PatientAuthorization = PatientAuthorization.Convert2Dto(auth.PatientAuthorization);
        //    r.AppointmentId = auth.AppointmentId;
        //    r.TotalUsed = auth.TotalUsed;
        //    r.Remaining = auth.Remaining;
        //    r.EntityStatus = auth.EntityStatus;
        //    return r;
        //}

        //public void Unuse(RepositoryLocator locator)
        //{
        //    locator.ResourceRepository.DeactivateAuthorizationUnit(this.Id);
        //}
    }
}
