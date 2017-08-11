using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.DBEntity
{
    public class PaymentFee : EntityBase
    {
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

        //config part
        public List<int> SchedulerLocationIds { get; set; }
        public List<int> CodeReferenceIds { get; set; }
        public List<int> ProcedureCodes { get; set; }
        public List<int> StateCodes { get; set; }
        public List<int> ZipCodes { get; set; }
        public List<int> LocalPayerIds { get; set; }

        public string SchedulerLocationIdsString
        {
            set { SchedulerLocationIds = ParseToList(value); }
        }

        public string CodeReferenceIdsString
        {
            set { CodeReferenceIds = ParseToList(value); }
        }

        public string ProcedureCodesString
        {
            set { ProcedureCodes = ParseToList(value); }
        }

        public string StateCodesString
        {
            set { StateCodes = ParseToList(value); }
        }

        public string ZipCodesString
        {
            set { ZipCodes = ParseToList(value); }
        }

        public string LocalPayerIdsString
        {
            set { LocalPayerIds = ParseToList(value); }
        }

        public PaymentFee()
        {
            SchedulerLocationIds = new List<int>();
            CodeReferenceIds = new List<int>();
            ProcedureCodes = new List<int>();
            StateCodes = new List<int>();
            ZipCodes = new List<int>();
            LocalPayerIds = new List<int>();
        }

        //public static PaymentFee ExtractFromDto(PaymentFeeDto dto)
        //{
        //    PaymentFee result = new PaymentFee()
        //    {
        //        FeeScheduleName = dto.FeeScheduleName,
        //        Id = dto.FeeScheduleId,
        //        FeeScheduleType = dto.FeeScheduleType,
        //        EffectiveDate = dto.EffectiveDate,
        //        ExpirationDate = dto.ExpirationDate,
        //        Amount = dto.Amount,
        //        CreatedBy = dto.CreatedBy,
        //        CreatedOn = dto.CreatedOn,
        //        ModifiedBy = dto.ModifiedBy,
        //        ModifiedOn = dto.ModifiedOn,
        //        IsActive = dto.IsActive,
        //        IsDeleted = dto.IsDeleted,

        //        SchedulerLocationIds = dto.SchedulerLocationIds,
        //        CodeReferenceIds = dto.CodeReferenceIds,
        //        ProcedureCodes = dto.ProcedureCodes,
        //        StateCodes = dto.StateCodes,
        //        ZipCodes = dto.ZipCodes,
        //        LocalPayerIds = dto.LocalPayerIds
        //    };
        //    return result;
        //}

        //public static PaymentFeeDto Convert2Dto(PaymentFee p)
        //{
        //    PaymentFeeDto result = new PaymentFeeDto()
        //    {
        //        FeeScheduleName = p.FeeScheduleName,
        //        FeeScheduleType = p.FeeScheduleType,
        //        EffectiveDate = p.EffectiveDate,
        //        ExpirationDate = p.ExpirationDate,
        //        Amount = p.Amount,
        //        CreatedBy = p.CreatedBy,
        //        CreatedOn = p.CreatedOn,
        //        ModifiedBy = p.ModifiedBy,
        //        ModifiedOn = p.ModifiedOn,
        //        IsActive = p.IsActive,
        //        IsDeleted = p.IsDeleted,

        //        SchedulerLocationIds = p.SchedulerLocationIds,
        //        CodeReferenceIds = p.CodeReferenceIds,
        //        ProcedureCodes = p.ProcedureCodes,
        //        StateCodes = p.StateCodes,
        //        ZipCodes = p.ZipCodes,
        //        FeeScheduleId = p.Id,
        //        LocalPayerIds = p.LocalPayerIds
        //    };
        //    return result;
        //}

        //helper
        private List<int> ParseToList(string value)
        {
            List<int> result = new List<int>();
            if (!string.IsNullOrEmpty(value))
            {
                string[] arr = value.Split(',');
                foreach (string a in arr)
                {
                    int id = 0;
                    if (Int32.TryParse(a, out id))
                    {
                        result.Add(id);
                    }
                }
            }
            return result;
        }

    }
}
