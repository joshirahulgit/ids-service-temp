using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Business.Entity
{
    public class CPTModifierDto : DtoBase
    {
        public int ID { get; set; }
        public String ExternalCode { get; set; }
        public String Code { get; set; }
        public String Description { get; set; }
        public bool IsGlobal { get; set; }

        public CPTModifierDto() { }

        public CPTModifierDto(CPTModifierDto modifier)
        {
            this.ID = modifier.ID;
            this.ExternalCode = modifier.ExternalCode;
            this.Code = modifier.Code;
            this.Description = modifier.Description;
            this.IsGlobal = modifier.IsGlobal;
        }

        public string DisplayText
        {
            get { return this.ToString(); }
        }

        public override string ToString()
        {
            return String.Concat("(", Code, ") ", Description);
        }

    }

    public class ModifiersDto : DtoBase
    {
        public IList<CPTModifierDto> Modifiers { get; set; }

        public ModifiersDto()
        {
            Modifiers = new List<CPTModifierDto>();
        }

        public ModifiersDto(IList<CPTModifierDto> modifiers)
        {
            Modifiers = modifiers;
        }

        public static bool Differ(List<CPTModifierDto> ma, List<CPTModifierDto> mb)
        {
            bool differ = ma.Count != mb.Count;
            if (!differ)
            {
                foreach (CPTModifierDto a in ma)
                {
                    differ = mb.Any(b => a.ID == b.ID);
                    if (differ)
                        break;
                }
            }
            return differ;
        }
    }
}
