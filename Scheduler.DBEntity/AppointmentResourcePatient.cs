using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.DBEntity
{
    public class AppointmentResourcePatient : AppointmentResource
    {
        public String RecordNumber { get; set; }
        public String ExternalID { get; set; }

        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String MiddleName { get; set; }
        public String Address1 { get; set; }
        public String Address2 { get; set; }
        public String ZipCode { get; set; }
        public String Phone { get; set; }

        public String MaidenName { get; set; }
        public String SSN { get; set; }
        public DateTime Dob { get; set; }
        public String Gender { get; set; }
        public int? LocationId { get; set; }
        public string AbbadoxLocation { get; set; }
        public string LocationName { get; set; }

        public String City { get; set; }
        public String State { get; set; }
        public String Mobile { get; set; }
        public String Emergency { get; set; }
        public String WorkPhone { get; set; }
        public String Fax { get; set; }
        public String Email { get; set; }
        //        public String Race { get; set; }

        public List<Race> Races { get; set; }
        public String Ethnicicty { get; set; }
        //public float Height { get; private set; }
        //public float Weight { get; private set; }
        //public float MBI { get; private set; }
        //public String Smoking { get; private set; }
        //public String BPFrom { get; private set; }
        //public String BPTo { get; private set; }

        //Need to comment to see all the places where to update and read from the database
        //public int Confirmation
        //{
        //    get
        //    {
        //        int result = 0;
        //        result |= ContactByCall ? 1 : 0;
        //        result |= ContactByEmail ? 2 : 0;
        //        result |= ConfirmByMail ? 4 : 0;
        //        result |= ConfirmBySms ? 8 : 0;
        //        result |= NotifyByCall ? 16 : 0;
        //        result |= NotifyByEmail ? 32 : 0;
        //        result |= NotifyByMail ? 64 : 0;
        //        result |= NotifyBySms ? 128 : 0;
        //        return result;
        //    }

        //    private set
        //    {
        //        ContactByCall = (value & 1) == 1;
        //        ContactByEmail = (value & 2) == 2;
        //        ConfirmByMail = (value & 4) == 4;
        //        ConfirmBySms = (value & 8) == 8;
        //        NotifyByCall = (value & 16) == 16;
        //        NotifyByEmail = (value & 32) == 32;
        //        NotifyByMail = (value & 64) == 64;
        //        NotifyBySms = (value & 128) == 128;
        //    }
        //}
        public int InsuranceType { get; set; }
        public String Status { get; set; }
        public DateTime DateOfDeseace { get; set; }
        public String Cause { get; set; }
        public bool IsSelfPay { get; set; }
        public bool RequiresTranslation { get; set; }
        public String TranslationLanguage { get; set; }
        public String PatientStatus { get; set; }
        public bool IsDeceased { get; set; }
        public DateTime? DeceaseDate { get; set; }
        public String CauseOfDeath { get; set; }
        public bool IsActive { get; set; }
        public bool IsVIP { get; set; }
        public List<PatientGuarantor> PatientGuarantors { get; set; }


        public List<PatientComment> Comments { get; set; }
        public List<string> SpecialNeeds { get; set; }
        public List<Address> AdditionalAddresses { get; set; }
        public List<PatientEmployment> PatientEmployments { get; set; }
        public List<PatientAuthorization> PatientAuthorizations { get; set; }
        public List<PatientIdentifier> MultipleIdentifiers { get; set; }
        public List<UsedAuthorization> UsedAuthorizations { get; set; }
        public List<Payer> Payers { get; set; }
        public List<SchedulerImage> IdCards { get; set; }
        public Dictionary<int, string> PreviousTransactions { get; set; }

        public List<PatientPayment> PatientPayments { get; set; }

        public bool ContactByCall { get; set; }
        public bool ContactByEmail { get; set; }
        public bool ConfirmByMail { get; set; }
        public bool ConfirmBySms { get; set; }
        public bool ConfirmByMobile { get; set; }

        public bool NotifyByCall { get; set; }
        public bool NotifyByEmail { get; set; }
        public bool NotifyByMail { get; set; }
        public bool NotifyBySms { get; set; }
        public bool NotifyByMobile { get; set; }

        public bool IsOptOutManualCalls { get; set; }
        public bool IsOptOutRoboCalls { get; set; }
        public bool IsOptOutLetters { get; set; }

        public string EnumHeardOfUsName { get; set; }
        public string MaritalStatus { get; set; }
        public DateTime? AdvanceDirectives { get; set; }
        public DateTime? ConsentForm { get; set; }
        public DateTime? LastAppointmentDate { get; set; }
        public DateTime LastModificationDateTime { get; set; }
        public string VerificationStatus { get; set; }

        public Dictionary<long, Appointment> LastApps { get; set; }

        public bool IsFullyCached { get; set; }

        public AppointmentResourcePatient()
        {
            this.ResourceType = new AppointmentResourceType((long)Core.ResourceTypes.Patient);
            this.Comments = new List<PatientComment>();
            this.Payers = new List<Payer>();
            this.IdCards = new List<SchedulerImage>();
            SpecialNeeds = new List<string>();
            AdditionalAddresses = new List<Address>();
            PatientEmployments = new List<PatientEmployment>();
            PatientAuthorizations = new List<PatientAuthorization>();
            MultipleIdentifiers = new List<PatientIdentifier>();
            UsedAuthorizations = new List<UsedAuthorization>();
            PreviousTransactions = new Dictionary<int, string>();
            Races = new List<Race>();
            this.PatientPayments = new List<PatientPayment>();
        }


        //public override void Delete(RepositoryLocator locator)
        //{
        //    locator.ResourceRepository.Remove(this);
        //}

        public override string DisplayText { get { return String.Concat(this.FirstName, ' ', this.LastName).Trim(); } }

        public override AppointmentResource Clear()
        {
            return new AppointmentResourcePatient() { Id = this.Id, ResourceType = this.ResourceType, Account = new Account(this.Account.Id), RecordNumber = this.RecordNumber };
        }

        public override bool IsValid(AppointmentResource resource)
        {
            if (!(resource is AppointmentResourcePatient)) return false;

            return this.Id == resource.Id;

            /*string pattern = (resource as AppointmentResourcePatient).FirstName.ToLower();

            return LastName.ToLower().Contains(pattern) || FirstName.ToLower().Contains(pattern);*/
        }

        //public override AppointmentResource Create(RepositoryLocator locator)
        //{
        //    //Data verification might be implemented here
        //    return Update(locator, this);
        //}

//        public override AppointmentResource Update(RepositoryLocator locator, AppointmentResource updatedResource)
//        {
//            AppointmentResourcePatient updatedPatient = updatedResource as AppointmentResourcePatient;
//            if (!object.ReferenceEquals(this, updatedPatient))
//            {
//                this.Account = updatedPatient.Account;
//                this.Address1 = updatedPatient.Address1;
//                this.Address2 = updatedPatient.Address2;
//                this.IsFullyCached = updatedPatient.IsFullyCached;
//                //this.BPFrom   = updatedPatient.BPFrom;
//                //this.BPTo     = updatedPatient.BPTo;
//                this.Cause = updatedPatient.Cause;
//                this.City = updatedPatient.City;
//                this.Comments = updatedPatient.Comments;
//                //this.Confirmation   = updatedPatient.Confirmation;

//                this.ContactByCall = updatedPatient.ContactByCall;
//                this.ContactByEmail = updatedPatient.ContactByEmail;
//                this.ConfirmByMail = updatedPatient.ConfirmByMail;
//                this.ConfirmBySms = updatedPatient.ConfirmBySms;
//                this.ConfirmByMobile = updatedPatient.ConfirmByMobile;

//                this.NotifyByCall = updatedPatient.NotifyByCall;
//                this.NotifyByEmail = updatedPatient.NotifyByEmail;
//                this.NotifyByMail = updatedPatient.NotifyByMail;
//                this.NotifyBySms = updatedPatient.NotifyBySms;
//                this.NotifyByMobile = updatedPatient.NotifyByMobile;

//                this.IsOptOutManualCalls = updatedPatient.IsOptOutManualCalls;
//                this.IsOptOutRoboCalls = updatedPatient.IsOptOutRoboCalls;
//                this.IsOptOutLetters = updatedPatient.IsOptOutLetters;

//                this.DateOfDeseace = updatedPatient.DateOfDeseace;
//                this.Dob = updatedPatient.Dob;
//                this.Email = updatedPatient.Email;
//                this.Ethnicicty = updatedPatient.Ethnicicty;
//                this.ExternalID = updatedPatient.ExternalID;
//                this.Fax = updatedPatient.Fax;
//                this.FirstName = updatedPatient.FirstName;
//                this.Gender = updatedPatient.Gender;
//                //this.Height = updatedPatient.Height;
//                this.Id = updatedPatient.Id;
//                this.InsuranceType = updatedPatient.InsuranceType;
//                this.LastName = updatedPatient.LastName;
//                this.MaidenName = updatedPatient.MaidenName;
//                //this.MBI = updatedPatient.MBI;
//                this.MiddleName = updatedPatient.MiddleName;
//                this.Mobile = updatedPatient.Mobile;
//                this.Emergency = updatedPatient.Emergency;
//                this.WorkPhone = updatedPatient.WorkPhone;
//                this.Payers = new List<Payer>(updatedPatient.Payers);
//                this.Phone = updatedPatient.Phone;
//                this.IsSelfPay = updatedPatient.IsSelfPay;
//                //            this.Race = updatedPatient.Race;
//                this.Races.Clear();
//                foreach (Race race in updatedPatient.Races)
//                    this.Races.Add(race);
//                this.RecordNumber = updatedPatient.RecordNumber;
//                this.RequiresTranslation = updatedPatient.RequiresTranslation;
//                this.ResourceType = updatedPatient.ResourceType;
//                //this.Smoking = updatedPatient.Smoking;
//                this.SSN = updatedPatient.SSN;
//                this.State = updatedPatient.State;
//                this.Status = updatedPatient.Status;
//                this.CauseOfDeath = updatedPatient.CauseOfDeath;
//                this.DeceaseDate = updatedPatient.DeceaseDate;
//                this.TranslationLanguage = updatedPatient.TranslationLanguage;
//                this.PatientStatus = updatedPatient.PatientStatus;
//                //this.Weight = updatedPatient.Weight;
//                this.ZipCode = updatedPatient.ZipCode;
//                this.IsActive = updatedPatient.IsActive; //updatedPatient.IsTemporary;
//                this.IsVIP = updatedPatient.IsVIP;
//                this.IsDeceased = updatedPatient.IsDeceased; //updatedPatient.IsTemporary;
//                this.LocationId = updatedPatient.LocationId;
//                this.PatientPayments = updatedPatient.PatientPayments;

//                this.SpecialNeeds = updatedPatient.SpecialNeeds;
//                this.AdditionalAddresses = updatedPatient.AdditionalAddresses;
//                this.PatientEmployments = updatedPatient.PatientEmployments;

//                //Grety mod, 2011-08-26, IdCards must be managed from special business methods
//                this.IdCards.Clear();
//                this.IdCards.AddRange(updatedPatient.IdCards);
//                List<PatientEmployment> employmentCopy = new List<PatientEmployment>(updatedPatient.PatientEmployments);

//                List<Address> addressesCopy = new List<Address>(updatedPatient.AdditionalAddresses);
//                this.AdditionalAddresses.Clear();
//                this.PatientEmployments.Clear();
//                foreach (Address addr in addressesCopy)
//                {
//                    switch (addr.EntityStatus)
//                    {
//                        case (int)EntityStatus.Added:
//                            {
//                                CreateAdditionalAddress(locator, addr);
//                                break;
//                            }
//                        case (int)EntityStatus.Deleted:
//                            {
//                                RemoveAdditionalAddress(locator, addr);
//                                break;
//                            }
//                        case (int)EntityStatus.Modified:
//                            {
//                                UpdateAdditionalAddress(locator, addr);
//                                break;
//                            }
//                        case (int)EntityStatus.NotModified:
//                            {
//                                this.AdditionalAddresses.Add(addr);
//                                break;
//                            }
//                    }
//                }

//                SaveEmployment(locator, employmentCopy);

//                this.EnumHeardOfUsName = updatedPatient.EnumHeardOfUsName;
//                this.MaritalStatus = updatedPatient.MaritalStatus;
//                this.ConsentForm = updatedPatient.ConsentForm;
//                this.LastAppointmentDate = updatedPatient.LastAppointmentDate;
//                this.LastModificationDateTime = updatedPatient.LastModificationDateTime;
//                this.AdvanceDirectives = updatedPatient.AdvanceDirectives;
//                UpdatePayers(locator, updatedPatient.Payers);
//                locator.ResourceRepository.Update(this);
//                return this;
//            }
//            else
//            {
//                locator.ResourceRepository.Update(updatedPatient);
//                SaveEmployment(locator, new List<PatientEmployment>(updatedPatient.PatientEmployments));
//                UpdatePayers(locator, updatedPatient.Payers);
//                return updatedPatient;
//            }
//        }

//        private void SaveEmployment(RepositoryLocator locator, List<PatientEmployment> employments)
//        {
//            if (employments != null)
//            {
//                foreach (PatientEmployment employment in employments)
//                {
//                    if (employment.Id == 0 && this.Id > 0)
//                        CreatePatientEmployment(locator, employment);
//                    else if (employment.Id > 0 && this.Id > 0)
//                        UpdatePatientEmployment(locator, employment);
//                }
//            }
//        }

//        private void UpdatePatientEmployment(RepositoryLocator locator, PatientEmployment employment)
//        {
//            PatientEmployment updatePE = employment.Update(locator, employment, this.Id);
//            this.PatientEmployments.Add(updatePE);
//        }


//        public void UpdatePayers(RepositoryLocator locator, List<Payer> payers)
//        {
//            List<Payer> payersCopy = new List<Payer>(payers.Count);
//            foreach (Payer p in payers)
//                payersCopy.Add(p);
//            this.Payers.Clear();
//            foreach (Payer p in payersCopy)
//            {
//                switch (p.EntityStatus)
//                {
//                    case (int)EntityStatus.Added:
//                        {
//                            Payer payer = CreatePayer(locator, p);
//                            //                            Payers.Add(payer);
//                            break;
//                        }
//                    case (int)EntityStatus.Deleted:
//                        {
//                            RemovePayer(locator, p);
//                            break;
//                        }
//                    case (int)EntityStatus.Modified:
//                        {
//                            Payer payer = UpdatePayer(locator, p);
//                            //                            Payers.Add(payer);
//                            break;
//                        }
//                    case (int)EntityStatus.NotModified:
//                        {
//                            this.Payers.Add(p);
//                            break;
//                        }
//                }
//            }
//        }

//        private Address UpdateAdditionalAddress(RepositoryLocator locator, Address addr)
//        {
//            Address updatedAddr = addr.Update(locator, addr, this.Id);
//            this.AdditionalAddresses.Add(updatedAddr);
//            return updatedAddr;
//        }

//        private void RemoveAdditionalAddress(RepositoryLocator locator, Address addr)
//        {
//            addr.Delete(locator, this.Id);
//        }

//        private void CreatePatientEmployment(RepositoryLocator locator, PatientEmployment employment)
//        {
//            PatientEmployment newEmp = employment.Create(locator, this.Id);
//            this.PatientEmployments.Add(newEmp);
//        }

//        private Address CreateAdditionalAddress(RepositoryLocator locator, Address addr)
//        {
//            Address newAddr = addr.Create(locator, this.Id);

//            this.AdditionalAddresses.Add(newAddr);

//            return newAddr;
//        }

//        public Payer UpdatePayer(RepositoryLocator locator, Payer newPayer)
//        {
//            Payer updatedPayer = newPayer.Update(locator, newPayer, this.Id, this.RecordNumber);
//            this.Payers.Add(updatedPayer);

//            //            locator.ResourceRepository.Update(this);

//            return updatedPayer;
//        }

//        public void RemovePayer(RepositoryLocator locator, Payer payer)
//        {
//            payer.Delete(locator, this.Id, this.RecordNumber);
//            //            locator.ResourceRepository.Update(this);
//        }

//        public SchedulerImage AddImage(RepositoryLocator locator, SchedulerImage image)
//        {
//            image.AttachPatient(this);
//            SchedulerImage result = image.Save(locator);
//            this.IdCards.Add(result);
//            //locator.ResourceRepository.Update(this);
//            return result;
//        }

//        public void DeleteImage(RepositoryLocator locator, SchedulerImage image)
//        {
//            image.AttachPatient(this);
//            image.Delete(locator);
//            SchedulerImage existing = this.IdCards.Where(i => i.Id == image.Id).FirstOrDefault();
//            if (existing != null)
//                this.IdCards.Remove(existing);
//            this.Update(locator, this);
//        }

//        public PatientComment CreateComment(RepositoryLocator locator, PatientComment comment)
//        {
//            PatientComment newComment = locator.ResourceRepository.CreateComment(this.Id, comment, false);

//            this.Comments.Add(newComment);

//            //locator.ResourceRepository.Update(this);

//            return newComment;
//        }

//        public Payer CreatePayer(RepositoryLocator locator, Payer payer)
//        {
//            Payer newPayer = payer.Create(locator, this.Id, this.RecordNumber);

//            this.Payers.Add(newPayer);

//            //locator.ResourceRepository.Update(this);

//            return newPayer;
//        }


//        public PatientPayment CreatePayment(RepositoryLocator locator, PatientPayment payment)
//        {
//            Account currentAccount = Services.AccountService.CheckForAccountInCache(locator, Container.RequestContext.AccountId);
//            PatientPayment newPayment = locator.ResourceRepository.CreatePayment(payment, currentAccount.IsProcessPaymentsEnabled);

//            this.PatientPayments.Add(newPayment);

//            //locator.ResourceRepository.Update(this);

//            return newPayment;
//        }

//        public string ResetPatientInsuranceFutureAppointments(RepositoryLocator locator, long patIntId)
//        {
//            return locator.ResourceRepository.ResetPatientInsuranceFutureAppointments(patIntId);
//        }

//        public static AppointmentResourcePatient RequestNewPatient(RepositoryLocator locator, long accountId, int? location)
//        {
//            AppointmentResourcePatient newPatient = new AppointmentResourcePatient();

//            string locStr = location.HasValue ? location.ToString() : null;
//            AccountGenerateIDconfig gen = locator.AccountRepository.GetIdGenerator(locStr, IdGenerationTypeName.MRN);
//            newPatient.RecordNumber = gen.GetNewId(locator);

//            newPatient.Account = new Account(accountId);
//#warning change logic in setting active
//            newPatient.IsActive = false;
//            newPatient.IsVIP = false;
//            newPatient.LocationId = location;

//            newPatient = locator.ResourceRepository.Create(newPatient) as AppointmentResourcePatient;

//            if (newPatient != null)
//                locator.AuditRepository.Create(new AuditEntry(null, string.Format("Patient was created. Mrn = {0}", newPatient.RecordNumber), newPatient.Id.ToString(), AuditEntityNameEnum.Patient.ToString(), AuditActionTypeEnum.Create.ToString()));


//            return newPatient;
//        }

//        public static AppointmentResourcePatient ExtractFromDto(AppointmentResourcePatientDto dto)
//        {
//            if (dto == null) return null;
//            AppointmentResourcePatient patient = new AppointmentResourcePatient();
//            patient.IsFullyCached = dto.IsFullyCached;
//            patient.Account = new Account(dto.AccountId);
//            patient.Address1 = dto.Address1;
//            patient.Address2 = dto.Address2;
//            //patient.BPFrom = dto.BPFrom;
//            //patient.BPTo = dto.BPTo;
//            patient.Cause = dto.Cause;
//            patient.City = dto.City;

//            foreach (PatientCommentDto c in dto.Comments)
//                patient.Comments.Add(PatientComment.ExtractFromDto(c));

//            //patient.Confirmation = (int)dto.Confirmation;
//            patient.ContactByCall = dto.ConfirmByCall;
//            patient.ContactByEmail = dto.ConfirmByEmail;
//            patient.ConfirmByMail = dto.ConfirmByMail;
//            patient.ConfirmBySms = dto.ConfirmBySms;
//            patient.ConfirmByMobile = dto.ConfirmByMobile;

//            patient.NotifyByCall = dto.NotifyByCall;
//            patient.NotifyByEmail = dto.NotifyByEmail;
//            patient.NotifyByMail = dto.NotifyByMail;
//            patient.NotifyBySms = dto.NotifyBySms;
//            patient.NotifyByMobile = dto.NotifyByMobile;

//            patient.IsOptOutManualCalls = dto.IsOptOutManualCalls;
//            patient.IsOptOutRoboCalls = dto.IsOptOutRoboCalls;
//            patient.IsOptOutLetters = dto.IsOptOutLetters;

//            patient.LocationId = dto.LocationID;
//            patient.DateOfDeseace = dto.DateOfDeseace;
//            patient.Dob = dto.Dob;
//            patient.Email = dto.Email;
//            patient.Ethnicicty = dto.Ethnicicty;
//            patient.ExternalID = dto.ExternalID;
//            patient.Fax = dto.Fax;
//            patient.FirstName = dto.FirstName;
//            patient.Gender = dto.Gender;
//            //patient.Height = dto.Height;
//            patient.Id = dto.Id;
//            patient.InsuranceType = (int)dto.InsuranceType;
//            patient.LastName = dto.LastName;
//            patient.MaidenName = dto.MaidenName;
//            //patient.MBI = dto.MBI;
//            patient.MiddleName = dto.MiddleName;
//            patient.Mobile = dto.Mobile;
//            patient.Emergency = dto.Emergency;
//            patient.WorkPhone = dto.WorkPhone;

//            foreach (PayerDto p in dto.Payers)
//                patient.Payers.Add(Payer.ExtractFromDto(p));

//            patient.Phone = dto.Phone;
//            patient.IsSelfPay = dto.IsSelfPay;
//            //            patient.Race = dto.Race;
//            foreach (RaceDto race in dto.Races)
//                patient.Races.Add(Race.ExtractFromDto(race));

//            patient.RecordNumber = dto.RecordNumber;
//            patient.RequiresTranslation = dto.RequiresTranslation;
//            patient.ResourceType = new AppointmentResourceType(dto.TypeId);
//            //patient.Smoking = dto.Smoking;
//            patient.SSN = dto.SSN;
//            patient.State = dto.State;
//            patient.Status = dto.Status;

//            patient.TranslationLanguage = dto.TranslationLanguage;
//            patient.PatientStatus = dto.PatientStatus;
//            //patient.Weight = dto.Weight;
//            patient.ZipCode = dto.ZipCode;
//            patient.IsActive = dto.IsActive;
//            patient.IsVIP = dto.IsVIP;
//            patient.IsDeceased = dto.IsDeceased;
//            patient.CauseOfDeath = dto.CauseOfDeath;
//            patient.DeceaseDate = dto.DeceaseDate;

//            patient.EnumHeardOfUsName = dto.EnumHeardOfUsName;
//            patient.MaritalStatus = dto.MaritalStatus;
//            patient.AdvanceDirectives = dto.AdvanceDirectives;
//            patient.ConsentForm = dto.ConsentForm;
//            patient.LastAppointmentDate = dto.LastAppointmentDate;
//            patient.LastModificationDateTime = dto.LastModificationDateTime;

//            patient.AbbadoxLocation = dto.AbbadoxLocation;
//            patient.LocationId = dto.LocationID;
//            patient.LocationName = dto.LocationName;

//            foreach (PatientEmploymentDto item in dto.PatientEmployment)
//            {
//                if (item != null)
//                    patient.PatientEmployments.Add(PatientEmployment.ExtractFromDto(item));
//            }

//            foreach (ImageDto imageDto in dto.IdCards)
//                patient.IdCards.Add(SchedulerImage.ExtractFromDto(imageDto));
//            foreach (string specialNeed in dto.SpecialNeeds)
//                patient.SpecialNeeds.Add(specialNeed);
//            foreach (AddressDto addressDto in dto.AdditionalAddresses)
//                //Sunil: was throwing an error since was addressDto null
//                if (addressDto != null)
//                    patient.AdditionalAddresses.Add(Address.ExtractFromDto(addressDto));

//            foreach (PatientPaymentDto pp in dto.PatientPayments)
//                patient.PatientPayments.Add(PatientPayment.ExtractFromDto(pp));

//            if (patient.LastApps != null)
//            {
//                dto.LastApps = patient.LastApps.ToDictionary(s => s.Key, s => Appointment.Convert2Dto(s.Value));
//            }

//            return patient;
//        }

        public void LoadImages(List<SchedulerImage> images)
        {
            this.IdCards = images;
        }

        public void LoadPayers(List<Payer> payers)
        {
            this.Payers = payers;
        }

        public void LoadComments(List<PatientComment> list)
        {
            this.Comments = list;
        }

        public void LoadGuarantors(List<PatientGuarantor> guarantors)
        {
            this.PatientGuarantors = guarantors;
        }

        public void LoadAdditionalAddresses(List<Address> addresses)
        {
            AdditionalAddresses = addresses;
        }



        public void LoadPayments(List<PatientPayment> list)
        {
            this.PatientPayments = list;
        }


        //public void DoNotify(RepositoryLocator locator, ContactPatientMethods method, string message)
        //{
        //    switch (method)
        //    {
        //        case ContactPatientMethods.Call:
        //            locator.ResourceRepository.NotifyPatientByCall(this.Id, message);
        //            break;
        //        case ContactPatientMethods.Email:
        //            locator.ResourceRepository.NotifyPatientByEmail(this.Id, message);
        //            break;
        //        case ContactPatientMethods.Mail:
        //            locator.ResourceRepository.NotifyPatientByMail(this.Id, message);
        //            break;
        //        case ContactPatientMethods.SMS:
        //            locator.ResourceRepository.NotifyPatientBySMS(this.Id, message);
        //            break;
        //        default:
        //            throw new SchedulerException(SchedulerExceptionType.PatientContactMethodNotSupported, method.ToString());
        //    }
        //}

        //public void DoContact(RepositoryLocator locator, ContactPatientMethods method, string message)
        //{
        //    switch (method)
        //    {
        //        case ContactPatientMethods.Call:
        //            locator.ResourceRepository.ContactPatientByCall(this.Id, message);
        //            break;
        //        case ContactPatientMethods.Email:
        //            locator.ResourceRepository.ContactPatientByEmail(this.Id, message);
        //            break;
        //        case ContactPatientMethods.Mail:
        //            locator.ResourceRepository.ContactPatientByMail(this.Id, message);
        //            break;
        //        case ContactPatientMethods.SMS:
        //            locator.ResourceRepository.ContactPatientBySMS(this.Id, message);
        //            break;
        //        default:
        //            throw new SchedulerException(SchedulerExceptionType.PatientContactMethodNotSupported, method.ToString());
        //    }
        //}

        public void LoadPatientAuthorization(List<PatientAuthorization> auths, List<UsedAuthorization> usedAuths)
        {
            PatientAuthorizations = auths;
            UsedAuthorizations = usedAuths;
        }

        public void LoadMultipleIdentifier(List<PatientIdentifier> patientIdentifiers)
        {
            MultipleIdentifiers = patientIdentifiers;
        }

        public void LoadPreviousTransactions(Dictionary<int, string> transactions)
        {
            PreviousTransactions = transactions;
        }

        public void LoadEmployment(List<PatientEmployment> getPatientEmployment)
        {
            PatientEmployments = getPatientEmployment;
        }

        public void SetConsentForm(DateTime? date)
        {
            ConsentForm = date;
        }

        public void LoadRaces(List<Race> races)
        {
            //            Races = new List<Race>(races);
            Races = races;
        }

        public void SetSpecialNeeds(List<string> loadSpecialNeeds)
        {
            this.SpecialNeeds = loadSpecialNeeds;
        }

        public void SetRecordNumber(string rn)
        {
            this.RecordNumber = rn;
        }

        public void SetLastApps(Dictionary<long, Appointment> lastApps)
        {
            LastApps = lastApps;
        }

        public void SetDetails(int id, string last, string first)
        {
            this.Id = id;
            this.LastName = last;
            this.FirstName = first;
        }

        public void SetAccount(Account account)
        {
            Account = account;
        }

        //public void UpdateSelfPay(RepositoryLocator locator)
        //{
        //    locator.ResourceRepository.UpdatePatientSelfPay(this);
        //}

        public void SetIsSelftPay(bool patientIdIsSelfPay)
        {
            this.IsSelfPay = patientIdIsSelfPay;
        }

        public void SetLastModificationDateTime(DateTime lastModificationDateTime)
        {
            LastModificationDateTime = lastModificationDateTime;
        }
    }
}
