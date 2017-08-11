using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.DBEntity
{
    public class PhysicianType : EntityType
    {
        private String _color;

        public String Color
        {
            get { return _color; }
            set { _color = value; }
        }

        //public static PhysicianType ExtractFromDto(PhysicianTypeDto dto)
        //{
        //    PhysicianType result = new PhysicianType();
        //    result.Id = dto.TypeId;
        //    result.Name = dto.TypeName;
        //    result._color = dto.Color;
        //    return result;
        //}
    }
}
