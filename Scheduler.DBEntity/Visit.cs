using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.DBEntity
{
    public class Visit : EntityBase
    {
        public bool HasImplants { get; private set; }
        public bool IsPregnant { get; private set; }
        public List<Procedure> Procedures { get; private set; }
        public List<Diagnosis> Diagnosises { get; private set; }
        public List<Referral> Referrals { get; private set; }
        public List<AppointmentOrder> LinkedOrders { get; private set; }
        public float PatientHeight { get; private set; }
        public float PatientWeight { get; private set; }
        public String Smoking { get; private set; }
        public String BPFrom { get; private set; }
        public String BPTo { get; private set; }
        public String VisitReason { get; private set; }
        public String PatientCategory { get; private set; }
        public String PatientAilment { get; private set; }
        public String StateOfAccident { get; private set; }
        public String PlaceOfService { get; private set; }
        public int ImageCount { get; private set; }
        public int SeriesCount { get; private set; }
        public DateTime? DOI { get; private set; }
        public bool NoAuthRequired { get; private set; }
        public String NoAuthRequiredComment { get; private set; }
        public List<UsedAuthorization> UsedAuthorizations { get; private set; }
        public String ReferringNotes { get; private set; }
        public String EnumScheduledBy { get; private set; }
        public List<String> SpecialNeed { get; private set; }
        public List<long> AttachedGuarantors { get; private set; }
        public Mammography Mammography { get; private set; }
        public bool HasOnlineAppointment { get; private set; }
        public int? OnlineAppointmentId { get; set; }
        public string EhrTransitionGuid { get; private set; }
        public List<AppointmentCheckListValue> AppointmentCheckListItems { get; private set; }
        public Dictionary<string, bool> QuestionnaireCollection { get; private set; }

        private List<MammographyHistory> MammographyHistories { get; set; }

        public Visit()
        {
            Mammography = new Mammography();
            this.Procedures = new List<Procedure>();
            this.Diagnosises = new List<Diagnosis>();
            this.Referrals = new List<Referral>();
            this.LinkedOrders = new List<AppointmentOrder>();
            UsedAuthorizations = new List<UsedAuthorization>();
            AttachedGuarantors = new List<long>();
            MammographyHistories = new List<MammographyHistory>();
            AppointmentCheckListItems = new List<AppointmentCheckListValue>();
            SpecialNeed = new List<string>();
            QuestionnaireCollection = new Dictionary<string, bool>();
        }

        public Visit(List<Procedure> procedures, List<Diagnosis> diagnoses, List<Referral> referrals, List<AppointmentOrder> linkedOrders, float height,
            float weight, string smoking, string bpFrom, string bpTo, string visitReason, string patientCategory, string stateOfAccident, DateTime? doi,
            bool noauthrequired, string noauthrequiredcomment, List<UsedAuthorization> usedAuth, string referringNotes, string enumScheduledBy,
            string patientAilment, List<string> specialNeed, bool isPregnant, bool hasImplants, string placeOfService, int imageCount, int seriesCount, bool hasOnlineAppointment, int? onlineAppointmentId) :
            this(procedures, diagnoses, referrals, linkedOrders, height, weight, smoking, bpFrom, bpTo, visitReason, patientCategory,
            stateOfAccident, doi, noauthrequired, noauthrequiredcomment, referringNotes, enumScheduledBy, patientAilment, isPregnant, hasImplants, hasOnlineAppointment, onlineAppointmentId)
        {
            this.UsedAuthorizations = usedAuth;
            this.SpecialNeed = new List<string>(specialNeed);
            this.Id = Environment.TickCount;
            Mammography = new Mammography();
            MammographyHistories = new List<MammographyHistory>();
            PlaceOfService = placeOfService;
            ImageCount = imageCount;
            SeriesCount = seriesCount;
        }

        public Visit(List<Procedure> procedures, List<Diagnosis> diagnoses, List<Referral> referrals, List<AppointmentOrder> linkedOrders,
            float height, float weight, string smoking, string bpFrom, string bpTo, string visitReason, string patientCategory,
            string stateOfAccident, DateTime? doi, bool noauthrequired, string noauthrequiredcomment, string referringNotes,
            string enumScheduledBy, string patientAilment, bool isPregnant, bool hasImplants, bool hasOnlineAppointment, int? onlineAppointmentId)
        {
            Mammography = new Mammography();
            MammographyHistories = new List<MammographyHistory>();
            this.Procedures = procedures;
            this.Diagnosises = diagnoses;
            this.Referrals = referrals;
            this.LinkedOrders = linkedOrders;
            this.PatientHeight = height;
            this.PatientWeight = weight;
            this.Smoking = smoking;
            this.BPFrom = bpFrom;
            this.BPTo = bpTo;
            this.VisitReason = visitReason;
            this.PatientCategory = patientCategory;
            this.PatientAilment = patientAilment;
            this.DOI = doi;
            this.NoAuthRequired = noauthrequired;
            this.NoAuthRequiredComment = noauthrequiredcomment;
            this.StateOfAccident = stateOfAccident;
            this.PlaceOfService = PlaceOfService;
            this.SeriesCount = SeriesCount;
            this.ImageCount = ImageCount;
            this.ReferringNotes = referringNotes;
            this.EnumScheduledBy = enumScheduledBy;
            this.IsPregnant = isPregnant;
            this.HasImplants = hasImplants;
            this.AttachedGuarantors = new List<long>();
            this.HasOnlineAppointment = hasOnlineAppointment;
            this.OnlineAppointmentId = onlineAppointmentId;
            LinkedOrders = linkedOrders;
            UsedAuthorizations = new List<UsedAuthorization>();
            QuestionnaireCollection = new Dictionary<string, bool>();
        }

        public Visit(List<Procedure> procedures)
        {
            Mammography = new Mammography();
            MammographyHistories = new List<MammographyHistory>();
            this.Procedures = procedures;
            this.Diagnosises = new List<Diagnosis>();
            this.Referrals = new List<Referral>();
            this.LinkedOrders = new List<AppointmentOrder>();
            UsedAuthorizations = new List<UsedAuthorization>();
            AttachedGuarantors = new List<long>();
            QuestionnaireCollection = new Dictionary<string, bool>();
            this.Id = -1;
        }

        public static double CalculateBMI(double weightLbs, double heightIn)
        {
            if (heightIn != 0)
                return (703 * (double)weightLbs / (heightIn * heightIn));
            return 0;
        }

        internal bool ContainsOrder(long appointmnetId, long? appointmentItemTypeId, String appointmentItemKey)
        {
            if (this.LinkedOrders == null)
                return false;

            foreach (AppointmentOrder order in this.LinkedOrders)
            {
                if (order.AppointmentId == appointmnetId &&
                    order.AppointmentItemType == appointmentItemTypeId &&
                    order.AppointmentItemId == appointmentItemKey)
                    return true;
            }
            return false;
        }

        //internal static Visit ExtractFromDto(VisitDto visitDto)
        //{
        //    if (visitDto == null) return new Visit();

        //    Visit result = new Visit() { Id = visitDto.VisitID };

        //    foreach (DiagnosisDto d in visitDto.Diagnoses)
        //        result.Diagnosises.Add(Diagnosis.ExtractFromDto(d));

        //    foreach (ProcedureDto p in visitDto.Procedures)
        //        result.Procedures.Add(Procedure.ExtractFromDto(p));

        //    foreach (ReferralDto p in visitDto.Referrals)
        //        result.Referrals.Add(Referral.ExtractFromDto(p));

        //    foreach (AppointmentOrderDto order in visitDto.Orders)
        //        result.LinkedOrders.Add(AppointmentOrder.ExtractFromDto(order));

        //    foreach (UsedAuthorizationDto auth in visitDto.UsedAuthorizations)
        //        result.UsedAuthorizations.Add(UsedAuthorization.ExtractFromDto(auth));

        //    result.PatientHeight = visitDto.PatientHeight;
        //    result.HasImplants = visitDto.HasImplants;
        //    result.IsPregnant = visitDto.IsPregnant;
        //    result.PatientWeight = visitDto.PatientWeight;
        //    result.Smoking = visitDto.Smoking;
        //    result.BPFrom = visitDto.BPFrom;
        //    result.BPTo = visitDto.BPTo;
        //    result.VisitReason = visitDto.VisitReason;
        //    result.PatientCategory = visitDto.PatientCategory;
        //    result.DOI = visitDto.DOI;
        //    result.StateOfAccident = visitDto.StateOfAccident;
        //    result.PlaceOfService = visitDto.PlaceOfService;
        //    result.ImageCount = visitDto.ImageCount;
        //    result.SeriesCount = visitDto.SeriesCount;
        //    result.NoAuthRequired = visitDto.NoAuthRequired;
        //    result.NoAuthRequiredComment = visitDto.NoAuthRequiredComment;
        //    result.ReferringNotes = visitDto.ReferringNotes;
        //    result.EnumScheduledBy = visitDto.EnumScheduledBy;
        //    result.PatientAilment = visitDto.PatientAilment;
        //    result.SpecialNeed = visitDto.SpecialNeed;
        //    result.AttachedGuarantors = new List<long>(visitDto.AttachedGuarantors);
        //    result.Mammography = Entities.Mammography.Mammography.ExtractFromDto(visitDto.Mammography);
        //    result.HasOnlineAppointment = visitDto.HasOnlineAppointment;
        //    result.OnlineAppointmentId = visitDto.OnlineAppointmentId;
        //    result.EhrTransitionGuid = visitDto.EhrTransitionGuid;

        //    foreach (MammographyHistoryDto mammographyHistoryDto in visitDto.MammographyHistory.Entries)
        //        result.MammographyHistories.Add(MammographyHistory.ExtractFromDto(mammographyHistoryDto));

        //    foreach (AppointmentCheckListValueDto item in visitDto.AppointmentCheckListItems)
        //    {
        //        result.AppointmentCheckListItems.Add(AppointmentCheckListValue.ExtractFromDto(item));
        //    }
        //    foreach (KeyValuePair<string, bool> item in visitDto.QuestionnaireCollection)
        //        result.QuestionnaireCollection.Add(item.Key, item.Value);
        //    return result;
        //}

        //public static VisitDto Convert2Dto(Visit visit)
        //{
        //    if (visit == null)
        //        return null;

        //    VisitDto result = new VisitDto();

        //    foreach (Diagnosis d in visit.Diagnosises)
        //    {
        //        DiagnosisDto dto = Diagnosis.Convert2Dto(d);
        //        dto.Category = d.Category;
        //        result.Diagnoses.Add(dto);
        //    }

        //    foreach (Procedure p in visit.Procedures)
        //        result.Procedures.Add(Procedure.Convert2Dto(p));

        //    foreach (AppointmentOrder o in visit.LinkedOrders)
        //        result.Orders.Add(AppointmentOrder.Convert2Dto(o));

        //    if (visit.Referrals != null)
        //        foreach (Referral referral in visit.Referrals)
        //            result.Referrals.Add(Referral.Convert2Dto(referral));

        //    foreach (UsedAuthorization auth in visit.UsedAuthorizations)
        //        result.UsedAuthorizations.Add(UsedAuthorization.Convert2Dto(auth));

        //    foreach (MammographyHistory history in visit.MammographyHistories)
        //        result.MammographyHistory.Entries.Add(MammographyHistory.Convert2Dto(history));

        //    if (visit.AppointmentCheckListItems == null)
        //    {
        //        visit.AppointmentCheckListItems = new List<AppointmentCheckListValue>();
        //    }

        //    foreach (AppointmentCheckListValue item in visit.AppointmentCheckListItems)
        //    {
        //        result.AppointmentCheckListItems.Add(AppointmentCheckListValue.Convert2Dto(item));
        //    }
        //    foreach (var item in visit.QuestionnaireCollection)
        //        result.QuestionnaireCollection.Add(item.Key, item.Value);

        //    result.HasImplants = visit.HasImplants;
        //    result.IsPregnant = visit.IsPregnant;
        //    result.VisitID = visit.Id;
        //    result.PatientHeight = visit.PatientHeight;
        //    result.PatientWeight = visit.PatientWeight;
        //    result.BPFrom = visit.BPFrom;
        //    result.BPTo = visit.BPTo;
        //    result.Smoking = visit.Smoking;
        //    result.VisitReason = visit.VisitReason;
        //    result.PatientCategory = visit.PatientCategory;
        //    result.DOI = visit.DOI;
        //    result.StateOfAccident = visit.StateOfAccident;
        //    result.PlaceOfService = visit.PlaceOfService;
        //    result.ImageCount = visit.ImageCount;
        //    result.SeriesCount = visit.SeriesCount;
        //    result.NoAuthRequired = visit.NoAuthRequired;
        //    result.NoAuthRequiredComment = visit.NoAuthRequiredComment;
        //    result.ReferringNotes = visit.ReferringNotes;
        //    result.EnumScheduledBy = visit.EnumScheduledBy;
        //    result.PatientAilment = visit.PatientAilment;
        //    result.SpecialNeed = visit.SpecialNeed;
        //    result.Mammography = Entities.Mammography.Mammography.Convert2Dto(visit.Mammography);
        //    result.HasOnlineAppointment = visit.HasOnlineAppointment;
        //    result.OnlineAppointmentId = visit.OnlineAppointmentId;
        //    result.EhrTransitionGuid = visit.EhrTransitionGuid;
        //    //result.AttachedGuarantors = new List<long>(visit.AttachedGuarantors);//attached guarantors are never read at serverside with appt
        //    return result;
        //}

        public void SetMammography(Mammography mammography)
        {
            Mammography = mammography;
        }

        public void SetMammographyHistory(List<MammographyHistory> history)
        {
            MammographyHistories = history;
        }

        public void SetVisitSpecialNeeds(List<string> visitSpecialNeeds)
        {
            SpecialNeed = visitSpecialNeeds;
        }

        public void SetRefferals(List<Referral> referrals)
        {
            this.Referrals = referrals;
        }

        public void SetAppointmentCheckListItems(List<AppointmentCheckListValue> appoinmentCheckListItems)
        {
            AppointmentCheckListItems = appoinmentCheckListItems;
        }

        public void SetQuestionnaireResults(Dictionary<string, bool> questionnaireResults)
        {
            QuestionnaireCollection.Clear();
            foreach (KeyValuePair<string, bool> pair in questionnaireResults)
                QuestionnaireCollection.Add(pair.Key, pair.Value);
        }
    }
}
