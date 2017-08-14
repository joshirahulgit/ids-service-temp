using Scheduler.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Scheduler.Business.Entity
{
    public class AppointmentResourcePatientsDto : DtoBase
    {
        public IList<AppointmentResourcePatientDto> Patients { get; set; }
    }

    public class AppointmentResourcePatientDto : AppointmentResourceDto
    {
        private List<PayerDto> _payers;
        private List<PatientCommentDto> _comments;
        private List<ImageDto> _idCards;
        private List<AddressDto> _additionalAddresses;
        private List<PatientEmploymentDto> _patientEmployment;
        private List<PatientPaymentDto> _payments;
        private List<string> _specialNeeds;

        private List<PatientAuthorizationDto> _patientAutorizations;
        private List<UsedAuthorizationDto> _usedAuthorizationDto;
        private List<PatientIdentifierDto> _multipleIdentifiers;

        public List<PatientIdentifierDto> MultipleIdentifiers
        {
            get
            {
                if (_multipleIdentifiers == null)
                    _multipleIdentifiers = new List<PatientIdentifierDto>();
                return _multipleIdentifiers;
            }
            set { _multipleIdentifiers = value; }
        }


        public List<PatientAuthorizationDto> PatientAuthorizations
        {
            get
            {
                if (_patientAutorizations == null)
                    _patientAutorizations = new List<PatientAuthorizationDto>();
                return _patientAutorizations;
            }
            set { _patientAutorizations = value; }
        }


        public Dictionary<int, string> PreviousTransactions
        {
            get
            {
                if (_previousTransactions == null)
                    _previousTransactions = new Dictionary<int, string>();

                return _previousTransactions;
            }
            set { _previousTransactions = value; }
        }

        public List<UsedAuthorizationDto> UsedAuthorizations
        {
            get
            {
                if (_usedAuthorizationDto == null)
                    _usedAuthorizationDto = new List<UsedAuthorizationDto>();
                return _usedAuthorizationDto;
            }
            set { _usedAuthorizationDto = value; }
        }

        public PatientAdditionalDataDto PatientAdditionalData { get; set; }

        public override string DisplayText
        {
            get { return FullName; }
        }
        /*

                [DataContract(Name = "CM", Namespace = "Scheduler.BO.XML")]
                public enum ConfiramtionMode
                {
                    [EnumMember]
                    Call  = 0,
                    [EnumMember]
                    SMS   = 1,
                    [EnumMember]
                    Email = 2,
                    [EnumMember]
                    Mail
                }
        */

        public enum InsurenceType
        {
            Insured = 0,
            Dependant = 1,
            SelfPay = 2
        }

        public AppointmentResourcePatientDto()
            : base((long)ResourceTypes.Patient)
        {
            this.TranslationLanguage = String.Empty;

            _payers = new List<PayerDto>();
            _comments = new List<PatientCommentDto>();
            _idCards = new List<ImageDto>();
            _specialNeeds = new List<string>();
            _additionalAddresses = new List<AddressDto>();
            _patientEmployment = new List<PatientEmploymentDto>();
            _patientAutorizations = new List<PatientAuthorizationDto>();
            _payments = new List<PatientPaymentDto>();
            Races = new List<RaceDto>();
        }

        public AppointmentResourcePatientDto(long id, long accountId)
            : this()
        {
            this.Id = id;
            this.AccountId = accountId;
        }


        protected override AppointmentResourceDto DoClone()
        {
            AppointmentResourcePatientDto res = new AppointmentResourcePatientDto();
            this.DoCopy(res);
            return res;
        }

        protected override void DoCopy(AppointmentResourceDto dest)
        {
            AppointmentResourcePatientDto p = dest as AppointmentResourcePatientDto;
            if (p == null)
                return;

            p.Id = this.Id;
            p.IdCards.Clear();
            p.IdCards.AddRange(this.IdCards);
            p.RecordNumber = this.RecordNumber;
            p.ExternalID = this.ExternalID;
            p.FirstName = this.FirstName;
            p.MiddleName = this.MiddleName;
            p.LastName = this.LastName;
            p.MaidenName = this.MaidenName;
            p.SSN = this.SSN;
            p.Dob = this.Dob;
            p.Gender = this.Gender;
            p.Address1 = this.Address1;
            p.Address2 = this.Address2;
            p.City = this.City;
            p.State = this.State;
            p.ZipCode = this.ZipCode;
            p.Phone = this.Phone;
            p.Mobile = this.Mobile;
            p.Emergency = this.Emergency;
            p.WorkPhone = this.WorkPhone;
            p.Fax = this.Fax;
            p.Email = this.Email;
            //            p.Race = this.Race;

            p.Races.Clear();
            foreach (RaceDto dto in this.Races)
                p.Races.Add(dto);
            p.Ethnicicty = this.Ethnicicty;
            //p.Height = this.Height;
            //p.Weight = this.Weight;
            //p.MBI = this.MBI;
            //p.Smoking = this.Smoking;
            //p.BPFrom = this.BPFrom;
            //p.BPTo = this.BPTo;
            //p.Confirmation = this.Confirmation;

            p.NotifyByCall = this.NotifyByCall;
            p.NotifyByEmail = this.NotifyByEmail;
            p.NotifyByMail = this.NotifyByMail;
            p.NotifyBySms = this.NotifyBySms;
            p.NotifyByMobile = this.NotifyByMobile;

            p.IsOptOutManualCalls = this.IsOptOutManualCalls;
            p.IsOptOutRoboCalls = this.IsOptOutRoboCalls;
            p.IsOptOutLetters = this.IsOptOutLetters;

            p.ConfirmByCall = this.ConfirmByCall;
            p.ConfirmByEmail = this.ConfirmByEmail;
            p.ConfirmByMail = this.ConfirmByMail;
            p.ConfirmBySms = this.ConfirmBySms;
            p.ConfirmByMobile = this.ConfirmByMobile;

            p.RequiresTranslation = this.RequiresTranslation;
            p.Status = this.Status;
            p.DateOfDeseace = this.DateOfDeseace;
            p.Cause = this.Cause;
            p.InsuranceType = this.InsuranceType;
            p.Comments = new List<PatientCommentDto>();
            p.IsSelfPay = this.IsSelfPay;
            p.IsActive = this.IsActive;
            p.IsDeceased = this.IsDeceased;
            p.CauseOfDeath = this.CauseOfDeath;
            p.DeceaseDate = this.DeceaseDate;
            p.LocationID = this.LocationID;
            p.LocationName = this.LocationName;
            p.AbbadoxLocation = this.AbbadoxLocation;
            //            p.PatientEmployment = this.PatientEmployment;
            p.IsVIP = this.IsVIP;
            p.WorkPhone = this.WorkPhone;
            p.Comments.Clear();
            foreach (PatientCommentDto comment in this.Comments)
                p.Comments.Add(new PatientCommentDto(comment));

            p.Payers.Clear();
            foreach (PayerDto pay in this.Payers)
                p.Payers.Add(new PayerDto(pay));

            p.AdditionalAddresses.Clear();
            foreach (AddressDto address in this.AdditionalAddresses)
                p.AdditionalAddresses.Add(new AddressDto(address));

            p.PatientEmployment.Clear();
            foreach (PatientEmploymentDto pe in this.PatientEmployment)
                p.PatientEmployment.Add(pe);

            p.SpecialNeeds.Clear();
            foreach (string specialNeed in this.SpecialNeeds)
                p.SpecialNeeds.Add(specialNeed);

            p.PatientGuarantors.Clear();
            foreach (var item in PatientGuarantors)
                p.PatientGuarantors.Add(item);

            p.PatientAuthorizations.Clear();
            foreach (var item in PatientAuthorizations)
                p.PatientAuthorizations.Add(item);

            p.UsedAuthorizations.Clear();
            foreach (var item in UsedAuthorizations)
                p.UsedAuthorizations.Add(item);

            p.PreviousTransactions.Clear();
            foreach (var item in PreviousTransactions)
                p.PreviousTransactions.Add(item.Key, item.Value);

            p.MultipleIdentifiers.Clear();
            foreach (var item in MultipleIdentifiers)
                p.MultipleIdentifiers.Add(item);

            p.PatientPayments = new List<PatientPaymentDto>();
            p.PatientPayments.Clear();
            foreach (var item in PatientPayments)
                p.PatientPayments.Add(new PatientPaymentDto(item));

            p.RequiresTranslation = this.RequiresTranslation;
            p.TranslationLanguage = this.TranslationLanguage;
            p.PatientStatus = this.PatientStatus;
            p.DeceaseDate = this.DeceaseDate;
            p.CauseOfDeath = this.CauseOfDeath;
            p.EnumHeardOfUsName = this.EnumHeardOfUsName;
            p.MaritalStatus = this.MaritalStatus;
            p.AdvanceDirectives = this.AdvanceDirectives;
            p.ConsentForm = this.ConsentForm;
            p.VerificationStatus = this.VerificationStatus;
            p.LastApps = this.LastApps;
            p.LastAppointmentDate = this.LastAppointmentDate;

            // p.IsFullyCached = this.IsFullyCached;
            p.LastModificationDateTime = this.LastModificationDateTime;

            //just for comparing
            p.EthnicityDescription = this.EthnicityDescription;
            p.MaritalStatusDesc = this.MaritalStatusDesc;
            p.LanguageDescription = this.LanguageDescription;
            p.HeardOfUsDescription = this.HeardOfUsDescription;
        }

        public AppointmentResourcePatientDto(AppointmentResourcePatientDto patient)
            : this()
        {
            this.Id = patient.Id;
            patient.DoCopy(this);
        }

        public List<ImageDto> IdCards
        {
            get { return _idCards; }
            set { _idCards = value; }
        }

        public List<PatientCommentDto> Comments
        {
            get { return _comments; }
            set { _comments = value; }
        }

        public bool IsFullyCached { get; set; }

        public List<PatientPaymentDto> PatientPayments
        {
            get { return _payments; }
            set { _payments = value; }
        }

        private List<PatientGuarantorDto> _patientGuarantors;
        private Dictionary<int, string> _previousTransactions;

        public List<PatientGuarantorDto> PatientGuarantors
        {
            get
            {
                if (_patientGuarantors == null)
                    _patientGuarantors = new List<PatientGuarantorDto>();

                return _patientGuarantors;
            }
            set { _patientGuarantors = value; }
        }

        public String RecordNumber { get; set; }

        public String ExternalID { get; set; }

        public String FirstName { get; set; }

        public String MiddleName { get; set; }

        public String LastName { get; set; }

        public String ZipCode { get; set; }

        public String Phone { get; set; }

        public String MaidenName { get; set; }

        public String SSN { get; set; }

        public DateTime Dob { get; set; }

        public String Gender { get; set; }

        public String Address1 { get; set; }

        public String Address2 { get; set; }

        public String City { get; set; }

        public String State { get; set; }

        public String Mobile { get; set; }

        public String Emergency { get; set; }

        public String Fax { get; set; }

        public String Email { get; set; }

        //        [DataMember(Name = "W")]
        //        public String Race { get; set; }

        public List<RaceDto> Races { get; set; }

        public String Ethnicicty { get; set; }

        //[DataMember(Name = "Y")]
        //public float Height { get; set; }

        //[DataMember(Name = "Z")]
        //public float Weight { get; set; }

        //[DataMember(Name = "A1")]
        //public float MBI { get; set; }

        //[DataMember(Name = "B1")]
        //public String Smoking { get; set; }

        //[DataMember(Name = "C1")]
        //public String BPFrom { get; set; }

        //[DataMember(Name = "D1")]
        //public String BPTo { get; set; }

        //        [DataMember(Name = "E1")]
        //        public ConfiramtionMode Confirmation { get; set; }

        public bool ConfirmByCall { get; set; }

        public bool ConfirmBySms { get; set; }

        public bool ConfirmByEmail { get; set; }

        public bool ConfirmByMail { get; set; }

        public bool ConfirmByMobile { get; set; }

        public bool NotifyByCall { get; set; }

        public bool NotifyBySms { get; set; }

        public bool NotifyByEmail { get; set; }

        public bool NotifyByMail { get; set; }

        public bool NotifyByMobile { get; set; }

        public bool IsOptOutManualCalls { get; set; }

        public bool IsOptOutRoboCalls { get; set; }

        public bool IsOptOutLetters { get; set; }

        public InsurenceType InsuranceType { get; set; }

        public String Status { get; set; }

        public DateTime DateOfDeseace { get; set; }

        public String Cause { get; set; }

        public bool IsSelfPay { get; set; }

        public string LocationName { get; set; }

        public bool RequiresTranslation { get; set; }

        public String TranslationLanguage { get; set; }

        public String PatientStatus { get; set; }

        public DateTime? DeceaseDate { get; set; }

        public String CauseOfDeath { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeceased { get; set; }

        public int? LocationID { get; set; }

        public List<PayerDto> Payers
        {
            get { return _payers; }
            set { _payers = value; }
        }

        public List<AddressDto> AdditionalAddresses
        {
            get { return _additionalAddresses; }
            set { _additionalAddresses = value; }
        }

        public List<PatientEmploymentDto> PatientEmployment
        {
            get { return _patientEmployment; }
            set { _patientEmployment = value; }
        }

        public List<string> SpecialNeeds
        {
            get { return _specialNeeds; }
            set { _specialNeeds = value; }
        }

        public string EnumHeardOfUsName { get; set; }

        public string MaritalStatus { get; set; }

        public DateTime? AdvanceDirectives { get; set; }

        public DateTime? ConsentForm { get; set; }

        public DateTime? LastAppointmentDate { get; set; }

        public DateTime LastModificationDateTime { get; set; }

        public bool IsNew { get; set; }

        public string VerificationStatus { get; set; }

        public string AbbadoxLocation { get; set; }

        public bool IsVIP { get; set; }

        public string WorkPhone { get; set; }

        public Dictionary<long, AppointmentDto> LastApps { get; set; }

        public string SpecialNeed
        {
            get
            {
                if (SpecialNeeds != null && SpecialNeeds.Count > 0)
                    return SpecialNeeds[0];
                return string.Empty;
            }
            set
            {
                if (SpecialNeeds != null && SpecialNeeds.Count > 0)
                    SpecialNeeds[0] = value;
                else
                {
                    if (SpecialNeeds == null) SpecialNeeds = new List<string>();
                    SpecialNeeds.Add(value);
                }

            }
        }

        public String DeceaseDateShortStr
        {
            get { return DeceaseDate.HasValue ? DeceaseDate.Value.ToShortDateString() : string.Empty; }
        }

        public String DobStr
        {
            get
            {
                if (this.Dob == DateTime.MinValue)
                    return string.Empty;
                return this.Dob.ToShortDateString();
            }
        }

        public override bool IsValid(AppointmentResourceDto resource)
        {
            if (!(resource is AppointmentResourcePatientDto)) return false;

            string pattern = (resource as AppointmentResourcePatientDto).FirstName.ToLower();

            return LastName.ToLower().Contains(pattern) || FirstName.ToLower().Contains(pattern);
        }

        public override string ToString()
        {
            return this.FullName;
        }

        public string FullName
        {
            get
            {
                return String.Concat(this.FirstName, ' ', this.LastName).Trim();
            }
        }

        public string AgeString
        {
            get
            {
                //if (this.Dob != DateTime.MinValue)
                //{
                //    return GetAge(this.Dob).ToString();
                //}
                //return string.Empty;
                return CalculateAge(this.Dob, DateTime.Now);
            }
        }

        //private int GetAge(DateTime dob)
        //{
        //    int age = DateTime.Today.Year - dob.Year;
        //    if (dob > DateTime.Today.AddYears(-age))
        //    {
        //        age--;
        //    }
        //    return age;
        //}

        public static string CalculateAge(DateTime dob, DateTime now)
        {
            if (dob > DateTime.Now) return "N/A";
            var days = now.Day - dob.Day;
            if (days < 0)
            {
                var nNow = now.AddMonths(-1);
                days += (int)(now - nNow).TotalDays;
                now = nNow;
            }
            var months = now.Month - dob.Month;
            if (months < 0)
            {
                months += 12;
                now = now.AddYears(-1);
            }
            var years = now.Year - dob.Year;
            return
                (years > 0 ? years + "y " : "") +
                ((months > 0 || years == 0 && months == 0) ? months + "m " : "");
            // +(days > 0 ? days + "d" : "");
        }

        public string ConfirmationStr
        {
            get
            {
                string result = (ConfirmByCall ? "Phone," : "") +
                    (ConfirmByEmail ? "Email," : "") +
                    (ConfirmByMail ? "Mail," : "") +
                    (ConfirmBySms ? "Text," : "") +
                    (ConfirmByMobile ? "Mobile," : "");
                if (result.Length > 0) result = result.Substring(0, result.Length - 1);
                return result;
            }
        }


        public bool IsSearchParamsEmpty()
        {
            return FirstName == null || LastName == null || Phone == null || Dob == DateTime.MinValue || RecordNumber == null;
        }

        public void SetIsNew(int? maxDay)
        {
            if (maxDay.HasValue && maxDay.Value != 0)
            {
                if (LastAppointmentDate.HasValue)
                {
                    if ((DateTime.Now - LastAppointmentDate.Value).Days > maxDay.Value)
                    {
                        IsNew = true;
                    }
                }
                else
                {
                    IsNew = true;
                }
            }
        }

        public string FormattedPhone
        {
            get { return FormatNumber(Phone); }
            set { Phone = value; }
        }

        public string FormattedMobile
        {
            get { return FormatNumber(Mobile); }
            set { Mobile = value; }
        }

        public string FormattedFax
        {
            get { return FormatNumber(Fax); }
            set { Fax = value; }
        }

        public string FormattedEmergency
        {
            get { return FormatNumber(Emergency); }
            set { Emergency = value; }
        }

        public string FormattedWorkPhone
        {
            get { return FormatNumber(WorkPhone); }
            set { WorkPhone = value; }
        }

        private string FormatNumber(string phone)
        {
            long n;
            if (!long.TryParse(phone, out n)) return phone;
            switch (phone.Length)
            {
                case 7:
                    return Regex.Replace(phone, @"(\d{3})(\d{4})", "$1-$2");
                case 10:
                    return Regex.Replace(phone, @"(\d{3})(\d{3})(\d{4})", "($1) $2-$3");
                case 11:
                    return Regex.Replace(phone, @"(\d{1})(\d{3})(\d{3})(\d{4})", "$1-$2-$3-$4");
                default:
                    return phone;
            }
        }

        //for comparing
        public string ConsentFormString
        {
            get { return ConsentForm.HasValue ? ConsentForm.ToString() : string.Empty; }
        }

        public string RaceDescriptions
        {
            get
            {
                if (Races != null)
                {
                    return string.Join(", ", Races.Select(s => s.Description).ToArray());
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public string SpecialNeedsDesc
        {
            get
            {
                if (SpecialNeeds != null)
                {
                    return string.Join(", ", SpecialNeeds.OrderBy(o => o).ToArray());
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public string StatusDesc
        {
            get
            {
                return StatusUniversal.HasValue ? (StatusUniversal.Value ? "Active" : "Inactive") : "Deceased";
            }
        }

        public bool? StatusUniversal
        {
            set
            {
                if (value.HasValue)
                {
                    IsActive = value.Value;
                    IsDeceased = false;
                }
                else
                {
                    IsActive = false;
                    IsDeceased = true;
                }
            }
            get
            {
                if (IsDeceased)
                {
                    return null;
                }
                else
                {
                    if (IsActive)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }


        public string CauseOfDeathDesc
        {
            get
            {
                if (IsDeceased)
                {
                    return CauseOfDeath;
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public DateTime? DateOfDeseaceWithCheck
        {
            get
            {
                if (IsDeceased)
                {
                    return DeceaseDate;
                }
                else
                {
                    return null;
                }
            }
            set { DeceaseDate = value; }
        }



        public string EthnicityDescription { get; set; }
        public string MaritalStatusDesc { get; set; }
        public string LanguageDescription { get; set; }
        public string HeardOfUsDescription { get; set; }
    }

    public class PatientComparer : IEqualityComparer<AppointmentResourcePatientDto>
    {
        private static PatientComparer _instance;

        public static PatientComparer Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new PatientComparer();

                return _instance;
            }
        }

        #region IEqualityComparer<HolidayDto> Members

        public bool Equals(AppointmentResourcePatientDto x, AppointmentResourcePatientDto y)
        {
            return x.Id == y.Id && x.AccountId == y.AccountId;
        }

        public int GetHashCode(AppointmentResourcePatientDto obj)
        {
            return (obj.Id.GetHashCode() << 5) ^ (obj.AccountId.GetHashCode() << 5);
        }

        #endregion
    }
}
