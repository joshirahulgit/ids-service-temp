using Scheduler.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Business.Entity
{
    public class AccountGenerateIDconfigDto : DtoBase
    {
        public string AccountId { get; set; }
        public string Location { get; set; }
        public string CustomLocationCode { get; set; }
        public string IdTypeName { get; set; }
        public string IDFormatString { get; set; }
        public string PreFix { get; set; }
        public string PostFix { get; set; }
        public int? StartingSeq { get; set; }
        public int? NextAvailableSeq { get; set; }
        public bool? IsSeqPadded { get; set; }
        public int? SeqPaddingLen { get; set; }
        public string SeqPaddingChar { get; set; }
        public string SeqPaddingDir { get; set; }
        public bool UseGuid { get; set; }
        public int? GuidLen { get; set; }
        public long Id { get; set; }
        public EntityStatus EntityStatus { get; set; }
    }


    public class AccountGenerateIDconfigsDto : DtoBase
    {
        public AccountGenerateIDconfigsDto()
        {
            this.Configurations = new List<AccountGenerateIDconfigDto>();
        }

        public IList<AccountGenerateIDconfigDto> Configurations { get; set; }
    }
}
