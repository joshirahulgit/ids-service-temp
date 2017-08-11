using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Business.Entity
{
    public class ProcedureDto : CptBaseDto
    {
        private List<CPTModifierDto> _modifiers;

        public string StringCategory { get; set; }

        public ProcedureDto(long id, string code, string shortDescription, string mediumDescription, string longDescription, string globalId, bool isGlobal)
            : base(id, code, shortDescription, mediumDescription, longDescription, globalId, isGlobal)
        {
            this.Modifiers = new List<CPTModifierDto>();
            this.Insurances = new List<PayerDto>();
        }

        public ProcedureDto(long id, string code, string shortDescription, string mediumDescription, string longDescription, string globalId, bool isGlobal, long? appId, long? roomID)
            : this(id, code, shortDescription, mediumDescription, longDescription, globalId, isGlobal)
        {
            this.LinkedApptId = appId;
            this.LinkedRoomId = roomID;
            this.Modifiers = new List<CPTModifierDto>();
            this.Insurances = new List<PayerDto>();
        }


        public ProcedureDto(String category)
            : base()
        {
            this.Id = -1;
            this.Category = category;
            this.Modifiers = new List<CPTModifierDto>();
            this.Insurances = new List<PayerDto>();
        }

        public ProcedureDto(ProcedureDto dto) : base(dto)
        {
            this.Comment = dto.Comment;
            this.LinkedApptId = dto.LinkedApptId;
            this.LinkedRoomId = dto.LinkedRoomId;
            this.TimeOverheadMinutes = dto.TimeOverheadMinutes;
            this.Amount = dto.Amount;
            this.ProcNote = dto.ProcNote;
            this.Volume = dto.Volume;
            this.AlertText = dto.AlertText;
            this.MammogramType = dto.MammogramType;
            this.IsOrderRequired = dto.IsOrderRequired;
            this.DisplayOrder = dto.DisplayOrder;
            this.Modifiers = new List<CPTModifierDto>(); this.Insurances = new List<PayerDto>();
            foreach (CPTModifierDto modifier in dto.Modifiers)
            {
                this.Modifiers.Add(new CPTModifierDto(modifier));
            }

            foreach (PayerDto payerDto in dto.Insurances)
            {
                this.Insurances.Add(new PayerDto(payerDto));
            }
            this.HCPCScodeName = dto.HCPCScodeName;
            this.OverrideCreationMode = dto.OverrideCreationMode;
            this.LinkedRoomTypeId = dto.LinkedRoomTypeId;
            this.IsSelfPay = dto.IsSelfPay;
        }

        //        [DataMember(Name="A")]
        //        public ProcedureTypeDto Type {get;set;}
        //        
        public String Comment { get; set; }

        public int? TimeOverheadMinutes { get; set; }

        public int LinkedRoomTypeId { get; set; }

        public int? OverrideCreationMode { get; set; }

        public List<CPTModifierDto> Modifiers
        {
            get
            {
                if (_modifiers == null)
                    _modifiers = new List<CPTModifierDto>();

                return _modifiers;
            }
            set { _modifiers = value; }
        }
        public string HCPCScodeName { get; set; }

        public string MammogramType { get; set; }

        public bool IsOrderRequired { get; set; }

        public int DisplayOrder { get; set; }
        public bool CreateOrder { get; set; }

        public int? PatientInsuranceId { get; set; }

        public int? PatientGuarantorId { get; set; }

        public List<PayerDto> Insurances { get; set; }

        public ResourceDurationDto ResourceDurationOverride { get; set; }

        public bool IsSelfPay { get; set; }

        public string ProcNote { get; set; }
    }

    public class ProceduresDto : DtoBase
    {
        public IList<ProcedureDto> Procedures { get; set; }

        public ProceduresDto()
        {
            Procedures = new List<ProcedureDto>();
        }

        public ProceduresDto(IList<ProcedureDto> procedures)
        {
            Procedures = procedures;
        }

        public static bool Differ(List<ProcedureDto> da, List<ProcedureDto> db)
        {
            bool differ = da.Count != db.Count;
            if (!differ)
                foreach (ProcedureDto a in da)
                {
                    differ = db.Any(b => b.Code == a.Code && b.ShortDescription == a.ShortDescription && a.IsGlobal == b.IsGlobal &&
                        a.Comment == b.Comment && a.HCPCScodeName == b.HCPCScodeName && a.TimeOverheadMinutes == b.TimeOverheadMinutes &&
                        !ModifiersDto.Differ(a.Modifiers, b.Modifiers));
                    if (differ)
                        break;
                }
            return differ;
        }
    }
}
