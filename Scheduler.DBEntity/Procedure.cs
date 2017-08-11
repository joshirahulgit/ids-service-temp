using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.DBEntity
{
    public class Procedure : CptBase
    {
        public override bool Equals(object obj)
        {
            Procedure c = obj as Procedure;
            if (c != null)
                if (c.Id == Id && c.Code == Code && c.GlobalId == GlobalId) return true;
            return false;
        }

        public Procedure(int id, string desc, string code)
        {
            this.Id = id;
            this.ShortDescription = desc;
            this.Code = code;

            Modifiers = new List<CPTModifier>();
            Insurances = new List<Payer>();
        }

        public override string ToString()
        {
            return ShortDescription;
        }

        public String GlobalId { get; set; }
        public String Amount { get; set; }
        public string ProcNote { get; set; }
        public String Volume { get; set; }
        public String MammogramType { get; set; }
        public String HCPCScodeName { get; set; }
        public bool IsOrderRequired { get; set; }
        public int DisplayOrder { get; set; }
        public bool CreateOrder { get; set; }
        public int? PatientInsuranceId { get; set; }
        public int? PatientGuarantorId { get; set; }
        public bool IsSelfPay { get; set; }

        public List<Payer> Insurances { get; set; }

        public List<CPTModifier> Modifiers { get; set; }

        public ResourceDuration ResourceDurationOverride { get; set; }

        public void InitId(string id)
        {
            this.GlobalId = id;
        }

        public int? TimeOverheadMinutes { get; set; }

        public int? OverrideCreationMode { get; set; }

        public Procedure()
        {
            Modifiers = new List<CPTModifier>();
            Insurances = new List<Payer>();
        }

        //public bool Save2LocalStorage(RepositoryLocator locator)
        //{
        //    return locator.AccountRepository.AddProcedure2LocalStorage(this) != null;
        //}

        //public void UpdateGlobalId(RepositoryLocator locator, String newId)
        //{
        //    locator.AccountRepository.UpdateProcedureGuid(this.GlobalId, newId);
        //    this.GlobalId = newId;
        //}

        //public static Procedure ExtractFromDto(ProcedureDto dto)
        //{
        //    Procedure p = new Procedure()
        //    {
        //        Code = dto.Code,
        //        Id = dto.Id,
        //        ShortDescription = dto.ShortDescription,
        //        LongDescription = dto.LongDescription,
        //        GlobalId = dto.GlobalId,
        //        LinkedRoomId = dto.LinkedRoomId,
        //        LinkedApptId = dto.LinkedApptId,
        //        TimeOverheadMinutes = dto.TimeOverheadMinutes,
        //        OverrideCreationMode = dto.OverrideCreationMode,
        //        Category = dto.Category,
        //        Amount = dto.Amount,
        //        ProcNote = dto.ProcNote,
        //        Volume = dto.Volume,
        //        HCPCScodeName = dto.HCPCScodeName,
        //        AlertText = dto.AlertText,
        //        IsOrderRequired = dto.IsOrderRequired,
        //        DisplayOrder = dto.DisplayOrder,
        //        CreateOrder = dto.CreateOrder,
        //        PatientGuarantorId = dto.PatientGuarantorId,
        //        PatientInsuranceId = dto.PatientInsuranceId,
        //        IsSelfPay = dto.IsSelfPay,
        //        MammogramType = dto.MammogramType
        //    };

        //    foreach (PayerDto payerDto in dto.Insurances)
        //    {
        //        p.Insurances.Add(Payer.ExtractFromDto(payerDto));
        //    }
        //    foreach (CPTModifierDto m in dto.Modifiers)
        //    {
        //        p.Modifiers.Add(CPTModifier.ExtractFromDto(m));
        //    }
        //    return p;
        //}

        public void BindToAppointment(long appointmentId)
        {
            LinkedApptId = appointmentId;
        }

        //public static ProcedureDto Convert2Dto(Procedure p)
        //{
        //    ProcedureDto dto = new ProcedureDto(p.Id, p.Code, p.ShortDescription, p.MediumDescription, p.LongDescription, p.GlobalId, p.IsGlobal, p.LinkedApptId, p.LinkedRoomId);

        //    dto.TimeOverheadMinutes = p.TimeOverheadMinutes; //-1;//stupid workaround to hide overhead for editing of created appointments
        //    dto.Category = p.Category;
        //    dto.AlertText = p.AlertText;
        //    dto.Amount = p.Amount;
        //    dto.ProcNote = p.ProcNote;
        //    dto.Volume = p.Volume;
        //    dto.PatientGuarantorId = p.PatientGuarantorId;
        //    dto.PatientInsuranceId = p.PatientInsuranceId;
        //    dto.IsSelfPay = p.IsSelfPay;
        //    dto.HCPCScodeName = p.HCPCScodeName;
        //    dto.OverrideCreationMode = p.OverrideCreationMode;
        //    dto.MammogramType = p.MammogramType;
        //    dto.IsOrderRequired = p.IsOrderRequired;
        //    dto.DisplayOrder = p.DisplayOrder;
        //    dto.LinkedRoomTypeId = p.LinkedRoomTypeId;
        //    dto.LinkedRoomId = p.LinkedRoomId;
        //    dto.CreateOrder = p.CreateOrder;
        //    if (dto.Modifiers == null) dto.Modifiers = new List<CPTModifierDto>();
        //    if (dto.Insurances == null) dto.Insurances = new List<PayerDto>();

        //    foreach (Payer payer in p.Insurances)
        //        dto.Insurances.Add(Payer.Convert2Dto(payer));

        //    foreach (CPTModifier m in p.Modifiers)
        //    {
        //        dto.Modifiers.Add(CPTModifier.ConvertToDto(m));
        //    }
        //    if (p.ResourceDurationOverride != null)
        //    {
        //        dto.ResourceDurationOverride = new ResourceDurationDto
        //        {
        //            Id = p.ResourceDurationOverride.Id,
        //            ActualDuration = p.ResourceDurationOverride.ActualDuration,
        //            AdditionalLeadTime = p.ResourceDurationOverride.AdditionalLeadTime,
        //            DecrementTime = p.ResourceDurationOverride.DecrementTime,
        //            IncrementTime = p.ResourceDurationOverride.IncrementTime,
        //            SedationTime = p.ResourceDurationOverride.SedationTime
        //        };
        //    }
        //    return dto;
        //}

        public void BindToRoom(AppointmentResourceModality room)
        {
            if (room != null)
                LinkedRoomId = room.Id;
            else LinkedRoomId = null;
        }

        //public bool Update(RepositoryLocator locator, Procedure newProcedure)
        //{
        //    this.ShortDescription = newProcedure.ShortDescription;
        //    this.AlertText = newProcedure.AlertText;
        //    this.Code = newProcedure.Code;
        //    this.TimeOverheadMinutes = newProcedure.TimeOverheadMinutes;
        //    this.Category = newProcedure.Category;
        //    this.Id = newProcedure.Id;
        //    this.Amount = newProcedure.Amount;
        //    this.ProcNote = newProcedure.ProcNote;
        //    this.HCPCScodeName = newProcedure.HCPCScodeName;
        //    this.Volume = newProcedure.Volume;
        //    this.IsOrderRequired = newProcedure.IsOrderRequired;
        //    this.DisplayOrder = newProcedure.DisplayOrder;
        //    return locator.AccountRepository.AddProcedure2LocalStorage(this) != null;
        //}

        public bool CompareTo(Procedure p)
        {
            if (p == null)
                return false;
            return p.Code == Code && p.GlobalId == GlobalId && p.ProcNote == ProcNote;
        }

        public void SetCode(string code)
        {
            Code = code;
        }

        public void SetModalityType(int modalityTypeId)
        {
            this.LinkedRoomTypeId = modalityTypeId;
            //            this.ModalityTypeId = modalityTypeId;
        }

        public void SetLinkedRoomId(long modalityId)
        {
            LinkedRoomId = modalityId;
        }
    }
}
