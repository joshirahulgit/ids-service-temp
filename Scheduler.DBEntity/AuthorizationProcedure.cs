using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.DBEntity
{
    public class AuthorizationProcedure : EntityBase
    {
        public int PayerId { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public string ProcedureAmount { get; set; }
        public string ProcedureUnit { get; set; }
        public bool IsDeleted { get; set; }

        //public static AuthorizationProcedureDto Convert2Dto(AuthorizationProcedure procedure)
        //{
        //    AuthorizationProcedureDto r = new AuthorizationProcedureDto();
        //    r.Id = procedure.Id;
        //    r.PayerId = procedure.PayerId;
        //    r.Description = procedure.Description;
        //    r.Code = procedure.Code;
        //    r.ProcedureAmount = procedure.ProcedureAmount;
        //    r.ProcedureUnit = procedure.ProcedureUnit;
        //    return r;
        //}

        //public static AuthorizationProcedure ExtractFromDto(AuthorizationProcedureDto procedure)
        //{
        //    AuthorizationProcedure r = new AuthorizationProcedure();
        //    r.Id = procedure.Id;
        //    r.PayerId = procedure.PayerId;
        //    r.Description = procedure.Description;
        //    r.Code = procedure.Code;
        //    r.ProcedureAmount = procedure.ProcedureAmount;
        //    r.ProcedureUnit = procedure.ProcedureUnit;
        //    r.IsDeleted = procedure.IsDeleted;
        //    return r;
        //}
    }
}
