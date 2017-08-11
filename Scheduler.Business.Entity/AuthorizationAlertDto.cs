using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Business.Entity
{
    public class AuthorizationAlertDto : DtoBase
    {
        public AuthorizationAlertDto()
        {
            AuthorizationProcedures = new List<AuthorizationProcedureDto>();
        }

        public long Id { get; set; }

        public int PayerId { get; set; }

        public string PayerName { get; set; }

        public List<AuthorizationProcedureDto> AuthorizationProcedures { get; set; }

        public bool IsDeleted { get; set; }
    }


    public class AuthorizationAlertsDto : DtoBase
    {
        public AuthorizationAlertsDto()
        {
            this.Alerts = new List<AuthorizationAlertDto>();
        }

        public IList<AuthorizationAlertDto> Alerts { get; set; }
    }
}
