using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Business.Entity
{
    public class PaymentFeesDto : DtoBase
    {
        public PaymentFeesDto()
        {
            PaymentFees = new List<PaymentFeeDto>();
        }

        public List<PaymentFeeDto> PaymentFees { get; set; }
    }

    public class PaymentFeeDto : DtoBase
    {
        public long FeeScheduleId { get; set; }
        public string FeeScheduleName { get; set; }

        public string FeeScheduleType { get; set; }

        public DateTime EffectiveDate { get; set; }

        public DateTime? ExpirationDate { get; set; }

        public Decimal Amount { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public string ModifiedBy { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

        //config

        public List<int> SchedulerLocationIds { get; set; }

        public List<int> CodeReferenceIds { get; set; }

        public List<int> ProcedureCodes { get; set; }

        public List<int> StateCodes { get; set; }

        public List<int> ZipCodes { get; set; }

        public List<int> LocalPayerIds { get; set; }

        public int PaymentCollectedLocation { get; set; }

        public PaymentFeeDto()
        {
            SchedulerLocationIds = new List<int>();
            CodeReferenceIds = new List<int>();
            ProcedureCodes = new List<int>();
            StateCodes = new List<int>();
            ZipCodes = new List<int>();
            LocalPayerIds = new List<int>();
        }
    }
}
