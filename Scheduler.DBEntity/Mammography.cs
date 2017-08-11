using Scheduler.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.DBEntity
{
    public class Mammography : EntityBase
    {
        public Mammography()
        {
            LeftBreast = new Pathology();
            RightBreast = new Pathology();
            Tumors = new List<Tumor>();
        }

        public long Id { get; set; }
        public long AppointmentID { get; set; }
        public string NotifiedBy { get; set; }
        public string Laterality { get; set; }
        public string BIRADcode { get; set; }
        public string SurgeonName { get; set; }
        public string BreastDensity { get; set; }
        public DateTime? NotifiedDate { get; set; }
        public DateTime? LayletterSentDate { get; set; }
        public DateTime? FollowUpDate { get; set; }
        public List<Tumor> Tumors { get; set; }
        public bool? IsDiscordant { get; set; }
        public bool? IsPregnant { get; set; }
        public bool IsDeleted { get; set; }
        public bool? HasImplants { get; set; }
        public Pathology LeftBreast { get; set; }
        public Pathology RightBreast { get; set; }
        public string Procedures { get; set; }

        public bool IsEmpty
        {
            get
            {
                bool isEmpty = string.IsNullOrEmpty(this.BIRADcode) &&
                    string.IsNullOrEmpty(this.BreastDensity) &&
                    !this.FollowUpDate.HasValue &&
                    !this.HasImplants.HasValue &&
                    !this.IsDiscordant.HasValue &&
                    !this.IsPregnant.HasValue &&
                    string.IsNullOrEmpty(this.Laterality) &&
                    !this.LayletterSentDate.HasValue &&
                    string.IsNullOrEmpty(this.NotifiedBy) &&
                    !this.NotifiedDate.HasValue &&
                    string.IsNullOrEmpty(this.SurgeonName);
                isEmpty &= Tumors.Count == 0;
                isEmpty &= LeftBreast.Diagnoses.Count == 0;
                isEmpty &= LeftBreast.Pathologies.Count == 0;
                isEmpty &= RightBreast.Diagnoses.Count == 0;
                isEmpty &= RightBreast.Pathologies.Count == 0;
                return isEmpty;
            }
        }

        //public static Mammography ExtractFromDto(MammographyDto m)
        //{
        //    Mammography r = new Mammography();
        //    r.Id = m.Id;
        //    r.AppointmentID = m.AppointmentID;
        //    r.Laterality = m.Laterality;
        //    r.BreastDensity = m.BreastDensity;
        //    r.SurgeonName = m.SurgeonName;
        //    r.BIRADcode = m.BIRADcode;
        //    r.HasImplants = m.HasImplants;
        //    r.IsDiscordant = m.IsDiscordant;
        //    r.IsPregnant = m.IsPregnant;
        //    r.IsDeleted = m.IsDeleted;
        //    r.LeftBreast = Pathology.ExtractFromDto(m.LeftBreast);
        //    r.RightBreast = Pathology.ExtractFromDto(m.RightBreast);
        //    r.NotifiedBy = m.NotifiedBy;
        //    r.NotifiedDate = m.NotifiedDate;
        //    r.LayletterSentDate = m.LayletterSentDate;
        //    r.Procedures = m.Procedures;
        //    r.FollowUpDate = m.FollowUpDate;

        //    foreach (TumorDto dto in m.Tumors)
        //        r.Tumors.Add(Tumor.ExtractFromDto(dto));
        //    return r;
        //}

        //public static MammographyDto Convert2Dto(Mammography m)
        //{
        //    MammographyDto r = new MammographyDto();
        //    if (m == null) return r;
        //    r.Id = m.Id;
        //    r.AppointmentID = m.AppointmentID;
        //    r.SurgeonName = m.SurgeonName;
        //    r.HasImplants = m.HasImplants;
        //    r.BreastDensity = m.BreastDensity;
        //    r.Laterality = m.Laterality;
        //    r.BIRADcode = m.BIRADcode;
        //    r.IsDiscordant = m.IsDiscordant;
        //    r.IsPregnant = m.IsPregnant;
        //    r.IsDeleted = m.IsDeleted;
        //    r.LeftBreast = Pathology.Convert2Dto(m.LeftBreast);
        //    r.RightBreast = Pathology.Convert2Dto(m.RightBreast);
        //    r.NotifiedBy = m.NotifiedBy;
        //    r.NotifiedDate = m.NotifiedDate;
        //    r.LayletterSentDate = m.LayletterSentDate;
        //    r.Procedures = m.Procedures;
        //    r.FollowUpDate = m.FollowUpDate;

        //    foreach (Tumor tumor in m.Tumors)
        //        r.Tumors.Add(Tumor.Convert2Dto(tumor));
        //    return r;
        //}

        public void BindToAppointment(long id)
        {
            AppointmentID = id;
        }

        public bool DiffersFrom(Mammography m)
        {
            return this.BIRADcode != m.BIRADcode ||
                   this.BreastDensity != m.BreastDensity ||
                   this.FollowUpDate != m.FollowUpDate ||
                   this.HasImplants != m.HasImplants ||
                   this.IsDiscordant != m.IsDiscordant ||
                   this.IsPregnant != m.IsPregnant ||
                   this.Laterality != m.Laterality ||
                   this.LayletterSentDate != m.LayletterSentDate ||
                   !this.LeftBreast.Equals(m.LeftBreast) ||
                   this.NotifiedBy != m.NotifiedBy ||
                   this.NotifiedDate != m.NotifiedDate ||
                   !this.RightBreast.Equals(m.RightBreast) ||
                   this.SurgeonName != m.SurgeonName ||
                   Helpers.ListsDiffer(this.Tumors, m.Tumors)
                ;
        }

        //public void Create(RepositoryLocator locator)
        //{
        //    var t = locator.MammographyRepository.Create(this);
        //    if (t != null)
        //        Id = t.Id;
        //}

        //public void Update(RepositoryLocator locator, Mammography newMammography)
        //{
        //    locator.MammographyRepository.BackUp(newMammography.Id);
        //    locator.MammographyRepository.Update(newMammography);
        //    UpdateTumors(locator, Tumors, newMammography.Tumors);
        //    //sunil: 06/22/2017 -- need to check if there was an entry in the MammographyPathologyData table for this MammographyData ID
        //    if (newMammography.LeftBreast.Id < 1)
        //        newMammography.LeftBreast.SetId(
        //            locator.MammographyRepository.CreateNewMammographyPathDataEntry((int)newMammography.Id, "L"));
        //    UpdatePathology(locator, "L", LeftBreast, newMammography.LeftBreast);
        //    if (newMammography.RightBreast.Id < 1)
        //        newMammography.RightBreast.SetId(
        //            locator.MammographyRepository.CreateNewMammographyPathDataEntry((int)newMammography.Id, "R"));
        //    UpdatePathology(locator, "R", RightBreast, newMammography.RightBreast);
        //}

        //private void UpdatePathology(RepositoryLocator locator, string side, Pathology oldPathology, Pathology newPathology)
        //{
        //    foreach (PathologyDiagnosis oldDiag in oldPathology.Diagnoses)
        //    {
        //        PathologyDiagnosis newDiag = newPathology.Diagnoses.FirstOrDefault(d => d.Id == oldDiag.Id);
        //        if (newDiag == null)
        //            locator.MammographyRepository.RemovePathologyDiagnosis(oldDiag.Id);
        //        else
        //            if (!oldDiag.Equals(newDiag))
        //            locator.MammographyRepository.UpdatePathologyDiagnosis(newDiag);
        //    }
        //    foreach (PathologyDiagnosis newDiag in newPathology.Diagnoses)
        //    {
        //        PathologyDiagnosis oldDiag = oldPathology.Diagnoses.FirstOrDefault(d => d.Id == newDiag.Id);
        //        if (oldDiag == null)
        //            newDiag.Create(locator, newPathology.Id);
        //    }
        //    foreach (PathologyDetail oldResult in oldPathology.Pathologies)
        //    {
        //        PathologyDetail newResult = newPathology.Pathologies.FirstOrDefault(d => d.Id == oldResult.Id);
        //        if (newResult == null)
        //            locator.MammographyRepository.RemovePathologyResult(oldResult.Id);
        //        else
        //            if (!oldResult.Equals(newResult))
        //            locator.MammographyRepository.UpdatePathologyResult(newResult);
        //    }
        //    foreach (PathologyDetail newResult in newPathology.Pathologies)
        //    {
        //        PathologyDetail oldResult = oldPathology.Pathologies.FirstOrDefault(d => d.Id == newResult.Id);
        //        if (oldResult == null)
        //            newResult.Create(locator, newPathology.Id);
        //    }
        //}

        //private void UpdateTumors(RepositoryLocator locator, List<Tumor> oldTumors, List<Tumor> newTumors)
        //{
        //    foreach (Tumor oldTumor in oldTumors)
        //    {
        //        Tumor newTumor = newTumors.FirstOrDefault(t => t.Id == oldTumor.Id);
        //        if (newTumor == null)
        //            locator.MammographyRepository.RemoveTumor(oldTumor.Id);
        //        else
        //        {
        //            if (!oldTumor.Equals(newTumor))
        //                locator.MammographyRepository.UpdateTumor(newTumor);
        //        }
        //    }
        //    foreach (Tumor newTumor in newTumors)
        //    {
        //        Tumor oldTumor = oldTumors.FirstOrDefault(t => t.Id == newTumor.Id);
        //        if (oldTumor == null)
        //            locator.MammographyRepository.CreateTumor(this.Id, newTumor);
        //    }
        //}

        //public void Delete(RepositoryLocator locator)
        //{
        //    locator.MammographyRepository.Remove(this);
        //}
    }
}
