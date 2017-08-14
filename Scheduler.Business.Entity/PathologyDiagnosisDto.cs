using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Business.Entity
{
    public class PathologyDiagnosisDto
    {
        public PathologyDiagnosisDto()
        {
        }

        public PathologyDiagnosisDto(PathologyDiagnosisDto d)
        {
            Id = d.Id;
            Code = d.Code;
            ShortDesc = d.ShortDesc;
            CodeType = d.CodeType;
            IsDeleted = d.IsDeleted;
        }

        public override string ToString()
        {
            return DisplayText;
        }

        public string DisplayText
        {
            get
            {
                if (!string.IsNullOrEmpty(Code) && !string.IsNullOrEmpty(ShortDesc))
                    return string.Format("{0} ({1})", ShortDesc, Code);

                if (string.IsNullOrEmpty(ShortDesc) && !string.IsNullOrEmpty(Code))
                    return Code;

                return string.Empty;
            }
        }

        public long Id { get; set; }

        public string Code { get; set; }

        public string ShortDesc { get; set; }

        public string CodeType { get; set; }

        public bool IsDeleted { get; set; }
    }
}
