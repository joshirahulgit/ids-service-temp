using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Business.Entity
{
    public class AuthorizationProcedureDto
    {
        public long Id { get; set; }

        public int PayerId { get; set; }

        public string Description { get; set; }

        public string Code { get; set; }

        public string ProcedureAmount { get; set; }

        public string ProcedureUnit { get; set; }

        public bool IsDeleted { get; set; }


    }
}
