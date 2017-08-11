using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.DBEntity
{
    public class CPTModifier : EntityBase
    {
        public int ID { get; set; }
        public String ExternalCode { get; set; }
        public String Code { get; set; }
        public String Description { get; set; }
        public bool IsGlobal { get; set; }

        public CPTModifier() { }
        public CPTModifier(string code)
        {
            Code = code;
        }


        //public static CPTModifier ExtractFromDto(CPTModifierDto dto)
        //{
        //    return new CPTModifier()
        //    {
        //        Id = dto.ID,
        //        ExternalCode = dto.ExternalCode,
        //        Code = dto.Code,
        //        Description = dto.Description,
        //        IsGlobal = dto.IsGlobal
        //    };
        //}

        //public static CPTModifierDto ConvertToDto(CPTModifier m)
        //{
        //    return new CPTModifierDto()
        //    {
        //        ID = m.ID,
        //        ExternalCode = m.ExternalCode,
        //        Code = m.Code,
        //        Description = m.Description,
        //        IsGlobal = m.IsGlobal
        //    };
        //}
    }
}
