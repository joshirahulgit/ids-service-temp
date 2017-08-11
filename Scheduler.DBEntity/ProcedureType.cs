using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.DBEntity
{
    public class ProcedureType : EntityType
    {
        public ProcedureType()
        {
        }

        public ProcedureType(long id)
            : this()
        {
            this.Id = id;
        }

        public string ProcedureCode { get; private set; }

        //internal static ProcedureType ExtractFromDto(ProcedureTypeDto dto)
        //{
        //    ProcedureType result = new ProcedureType();
        //    result.Id = dto.TypeId;
        //    result.Name = dto.TypeName;
        //    result.ProcedureCode = dto.Code;
        //    return result;
        //}
    }
}
