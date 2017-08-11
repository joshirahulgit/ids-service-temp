using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.DBEntity
{
    public class Diagnosis : CptBase
    {
        //public string DiagnosisCode { get; protected set; } Use base field Code
        public String GlobalId { get; set; }
        public String Flag { get; set; }
        public string Chronic { get; set; }
        public bool IsChronic { get; set; }
        public Diagnosis()
        {

        }

        public Diagnosis(long id)
            : this()
        {
            this.Id = id;
        }

        public override bool Equals(object obj)
        {
            Diagnosis c = obj as Diagnosis;
            if (c != null)
                if (c.Id == Id && c.Code == Code && c.GlobalId == GlobalId) return true;
            return false;
        }

        //internal static Diagnosis ExtractFromDto(DiagnosisDto d)
        //{
        //    Diagnosis result = new Diagnosis();
        //    result.AlertText = d.AlertText;
        //    result.Id = d.Id;
        //    result.Code = d.Code;
        //    result.OnsetDate = d.OnsetDate;
        //    result.Flag = d.Flag;
        //    result.ShortDescription = d.ShortDescription;
        //    result.LongDescription = d.LongDescription;
        //    result.GlobalId = d.GlobalId;
        //    result.Category = d.Category;
        //    result.Chronic = d.Chronic;
        //    result.IsChronic = d.IsChronic;
        //    return result;
        //}
        //public static DiagnosisDto Convert2Dto(Diagnosis d)
        //{
        //    DiagnosisDto dto = new DiagnosisDto(d.Id, d.Code, d.ShortDescription, d.MediumDescription, d.LongDescription,
        //                                        d.GlobalId, d.IsGlobal, d.AlertText, d.OnsetDate, d.Flag);
        //    dto.Id = d.Id;
        //    dto.Category = d.Category;
        //    dto.AlertText = d.AlertText;
        //    dto.Chronic = d.Chronic;
        //    dto.IsChronic = d.IsChronic;
        //    //            dto.OnsetDate = d.OnsetDate;
        //    return dto;
        //}

        //public bool Save2LocalStorage(RepositoryLocator locator)
        //{
        //    return locator.AccountRepository.AddDiagnosis2LocalStorage(this) != null;
        //}

        internal void InitId(string p)
        {
            this.GlobalId = p;
        }

        //internal void UpdateGlobalId(RepositoryLocator locator, string newGlobalId)
        //{
        //    locator.AccountRepository.UpdateDiagnosisGuid(this.GlobalId, newGlobalId);
        //    this.GlobalId = newGlobalId;
        //}

        //public bool Update(RepositoryLocator locator, Diagnosis newDiagnosis)
        //{
        //    this.Id = newDiagnosis.Id;
        //    this.ShortDescription = newDiagnosis.ShortDescription;
        //    this.AlertText = newDiagnosis.AlertText;
        //    this.Code = newDiagnosis.Code;
        //    this.Category = newDiagnosis.Category;
        //    this.OnsetDate = newDiagnosis.OnsetDate;
        //    this.Flag = newDiagnosis.Flag;

        //    return locator.AccountRepository.AddDiagnosis2LocalStorage(this) != null;
        //}
        public bool CompareTo(Diagnosis p)
        {
            if (p == null)
                return false;
            return p.Code == Code && p.GlobalId == GlobalId;
        }

        public void SetCode(string code)
        {
            this.Code = code;
        }
    }
}
