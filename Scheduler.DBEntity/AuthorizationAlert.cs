using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.DBEntity
{
    public class AuthorizationAlert : EntityBase
    {
        public AuthorizationAlert()
        {
            AuthorizationProcedures = new List<AuthorizationProcedure>();
        }

        public int PayerId { get; set; }
        public string PayerName { get; set; }
        public bool IsDeleted { get; set; }
        public List<AuthorizationProcedure> AuthorizationProcedures { get; set; }

        public void LoadProcedures(List<AuthorizationProcedure> procs)
        {
            AuthorizationProcedures = procs;
        }

        //public static AuthorizationAlertDto Convert2Dto(AuthorizationAlert authorizationAlert)
        //{
        //    AuthorizationAlertDto r = new AuthorizationAlertDto();
        //    r.Id = authorizationAlert.Id;
        //    r.PayerId = authorizationAlert.PayerId;
        //    r.PayerName = authorizationAlert.PayerName;
        //    foreach (AuthorizationProcedure procedure in authorizationAlert.AuthorizationProcedures)
        //    {
        //        r.AuthorizationProcedures.Add(AuthorizationProcedure.Convert2Dto(procedure));
        //    }

        //    return r;
        //}

        //public static AuthorizationAlert ExtractfromDto(AuthorizationAlertDto authorizationAlertDto)
        //{
        //    AuthorizationAlert r = new AuthorizationAlert();
        //    r.Id = authorizationAlertDto.Id;
        //    r.PayerId = authorizationAlertDto.PayerId;
        //    r.PayerName = authorizationAlertDto.PayerName;
        //    r.IsDeleted = authorizationAlertDto.IsDeleted;
        //    foreach (AuthorizationProcedureDto dto in authorizationAlertDto.AuthorizationProcedures)
        //        r.AuthorizationProcedures.Add(AuthorizationProcedure.ExtractFromDto(dto));

        //    return r;
        //}


    }
}
