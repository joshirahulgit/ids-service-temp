using Scheduler.Core;
using Scheduler.DBEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Data.Implementation
{
    //To Desc: This class is created to support existing code and also maintain to SOC. -By RJ

    internal static class SafeDataReaderExt
    {
        public static AccountEnum ToAccountEnum(this SafeDataReader sr)
        {
            AccountEnum r = null;
            //r.Id = sr.GetInt64("ID");

            //By RJ: Next 4 lines added to support exiting encapsulation of ID field in Account Enum outside the class.
            if (sr.ContainsColumn("id"))
                r = new AccountEnum(sr.GetInt32("id"));
            else
                r = new AccountEnum();


            if (sr.ContainsColumn("Name"))
                r.Name = sr.GetNullableString("Name");
            if (sr.ContainsColumn("Value"))
                r.Value = sr.GetNullableString("Value");
            if (sr.ContainsColumn("IsVisible"))
                r.IsVisible = sr.GetBoolean("IsVisible");
            if (sr.ContainsColumn("EnumType"))
                r.EnumType = sr.GetNullableString("EnumType");
            if (sr.ContainsColumn("IsDefault"))
                r.IsDefault = sr.GetBoolean("IsDefault");
            if (sr.ContainsColumn("UserCanEdit"))
                r.UserCanEdit = sr.GetBoolean("UserCanEdit");
            if (sr.ContainsColumn("UserCanDelete"))
                r.UserCanDelete = sr.GetBoolean("UserCanDelete");

            return r;
        }

        public static AccountSetting ToAccountSetting(this SafeDataReader sr)
        {
            AccountSetting res = new AccountSetting();
            res.Id = sr.GetInt32("Id");
            res.Name = sr.GetString("Name");
            res.Value = sr.GetString("Value");
            res.Application = sr.GetNullableString("Application") ?? string.Empty;
            res.CreateDate = sr.GetDateTime("CreateDate");
            res.UpdateDate = sr.GetNullableDateTime("UpdateDate");
            res.CreateUser = sr.GetNullableString("CreateUserLogin");
            res.UpdateUser = sr.GetNullableString("UpdateUserLogin");
            res.IsActive = sr.GetBoolean("IsActive");
            return res;
        }

        public static CommentType ToCommentType(this SafeDataReader sr)
        {
            CommentType r = new CommentType(sr.GetInt32("Id"), sr.GetString("DisplayName"), sr.GetBoolean("IsVisible"),
                sr.GetBoolean("IsSystem"),
                sr.ContainsColumn("CannedCommentEnumType") ? sr.GetNullableString("CannedCommentEnumType") : null);
            return r;
        }

        public static CommentItem ToCommentItem(this SafeDataReader sr)
        {
            CommentItem res = new CommentItem();
            res.Id = sr.GetInt32("ID");
            res.Creator = sr.GetString("UserID");
            res.Text = sr.GetString("CommentText");
            res.Time = sr.GetDateTime("LastDate");
            if (sr.ContainsColumn("CommentTypeID"))
                res.Type = new CommentType(sr.GetInt32("CommentTypeID"), String.Empty, false, false);
            return res;
        }

        public static ReferringNote ToReferringNote(this SafeDataReader sr)
        {
            CommentItem ci = sr.ToCommentItem();// CommentItem.ExtractFromReader(sr);
            if (ci != null)
            {
                ReferringNote res = new ReferringNote(ci);
                res.isAlert = sr.GetBoolean("isAlert");
                res.OrderId = sr.GetNullableString("OrderId");
                res.AppointmentId = sr.GetNullableInt32("AppointmentId") ?? -1;
                return res;
            }
            else return null;
        }

        public static Referral ToReferral(this SafeDataReader sr)
        {
            Referral r = new Referral();
            r.Id = sr.GetInt64("ID");
            if (sr.ContainsColumn("Name"))
            {
                r.FirstName = /*r.LastName = */sr.GetNullableString("Name");
            }
            else
            {
                if (sr.ContainsColumn("LastName"))
                    r.LastName = sr.GetNullableString("LastName");

                if (sr.ContainsColumn("FirstName"))
                    r.FirstName = /*r.LastName = */ sr.GetNullableString("FirstName");
            }

            if (sr.ContainsColumn("Address"))
                r.Address = sr.GetNullableString("Address");

            if (sr.ContainsColumn("Address2"))
                r.Address2 = sr.GetNullableString("Address2");

            if (sr.ContainsColumn("City"))
                r.City = sr.GetNullableString("City");

            if (sr.ContainsColumn("State"))
                r.State = sr.GetNullableString("State");

            if (sr.ContainsColumn("Zipcode"))
                r.ZipCode = sr.GetNullableString("Zipcode");

            r.Type = sr.GetNullableString("Type");
            r.Phone = sr.GetNullableString("phone");
            r.Email = sr.GetNullableString("email");
            r.Fax = sr.GetNullableString("fax");
            r.ReferralId = sr.GetNullableString("ReferringId");
            r.Speciality = sr.GetNullableString("Speciality");

            if (sr.ContainsColumn("FaxEnabled"))
                r.IsFaxingEnabled = sr.GetNullableBoolean("FaxEnabled");

            if (sr.ContainsColumn("IsAutoPrintEnabled"))
                r.IsAutoPrintEnabled = sr.GetBoolean("IsAutoPrintEnabled");

            if (sr.ContainsColumn("IsEmailEnabled"))
                r.IsEmailEnabled = sr.GetBoolean("IsEmailEnabled");

            if (sr.ContainsColumn("NPI"))
                r.NPI = sr.GetNullableString("NPI");

            if (sr.ContainsColumn("TaxId"))
                r.TaxId = sr.GetNullableString("TaxId");

            // Start Reading added columns

            if (sr.ContainsColumn("Country"))
                r.Country = sr.GetNullableString("Country");

            if (sr.ContainsColumn("RefGroup"))
                r.Group = sr.GetNullableString("RefGroup");

            if (sr.ContainsColumn("OfficePhone"))
                r.OfficePhone = sr.GetNullableString("OfficePhone");

            if (sr.ContainsColumn("OfficeFax"))
                r.OfficeFax = sr.GetNullableString("OfficeFax");

            if (sr.ContainsColumn("MobilePhone"))
                r.MobilePhone = sr.GetNullableString("MobilePhone");

            if (sr.ContainsColumn("IsActive"))
                r.IsActive = sr.GetBoolean("IsActive");

            if (sr.ContainsColumn("SSN"))
                r.SSN = sr.GetNullableString("SSN");

            if (sr.ContainsColumn("FirstLanguage"))
                r.FirstLanguage = sr.GetNullableString("FirstLanguage");

            if (sr.ContainsColumn("SecondLanguage"))
                r.SecondLanguage = sr.GetNullableString("SecondLanguage");

            if (sr.ContainsColumn("RefINOutStatus"))
                r.RefINOutStatus = sr.GetNullableString("RefINOutStatus");

            if (sr.ContainsColumn("MiddleName"))
                r.MiddleName = sr.GetNullableString("MiddleName");

            if (sr.ContainsColumn("Credentials"))
                r.Credentials = sr.GetNullableString("Credentials");

            if (sr.ContainsColumn("ExternalID"))
                r.ExternalID = sr.GetNullableString("ExternalID");

            if (sr.ContainsColumn("Signature"))
                r.Signature = sr.GetNullableString("Signature");


            // End Reading added columns

            return r;
        }

        public static AppointmentOrder ToAppointmentOrder(this SafeDataReader reader)
        {
            AppointmentOrder result = new AppointmentOrder();
            result.Id = reader.GetInt64("SchedulerOrderId");
            result.AppointmentId = reader.GetInt64("AppointmentId");
            result.AppointmentItemType = reader.GetNullableInt64("AppointmentItemType");
            result.AppointmentItemId = reader.GetNullableString("AppointmentItemId");
            result.PatientId = reader.GetNullableString("PatientId");
            result.Location = reader.GetNullableString("LocationName");
            result.CPTCode = reader.GetNullableString("CPTCode");
            result.WorktypeDescription = reader.GetNullableString("WorktypeDescription");
            result.ExamDescription = reader.GetNullableString("ExamDescription");
            result.PhysicianId = reader.GetNullableString("PhysicianId");
            result.Reason = reader.GetNullableString("Reason");
            result.Dictator = reader.GetNullableString("Dictator");
            result.Priority = reader.GetNullableString("Priority");
            result.MultipleOrderId = reader.GetNullableString("MultipleOrderId");
            result.OrderId = reader.GetNullableString("OrderId");
            result.DOS = reader.GetNullableDateTime("DOS");
            result.AccountName = reader.GetNullableString("AccountName");
            result.RecurringSeriesID = reader.GetNullableString("RecurringSeriesID");
            result.CC = reader.GetNullableString("CC");
            result.Modality = reader.GetNullableString("Modality");
            if (reader.ContainsColumn("JobID"))
                result.JobID = reader.GetNullableString("JobID");
            return result;
        }
        public static AppointmentResource ToAppointmentResource(this SafeDataReader safeReader, long accountID, bool loadDetails)
        {
            long typeID = safeReader.GetInt64("ResourceType");
            AppointmentResource result = null;
            switch (typeID)
            {
                case (long)ResourceTypes.Patient:
                    {
                        if (loadDetails)
                            result = safeReader.ToAppointmentResourcePatient(accountID);
                        else
                            result = new AppointmentResourcePatient();
                        result.ResourceType = new AppointmentResourceType((long)ResourceTypes.Patient);
                        result.Id = safeReader.GetInt64("ResourceID");
                        break;
                    }
                case (long)ResourceTypes.Physician:
                    {
                        if (loadDetails)
                            result = safeReader.ToAppointmentResourcePhysician(accountID);
                        else
                            result = new AppointmentResourcePhysician();
                        result.ResourceType = new AppointmentResourceType((long)ResourceTypes.Physician);
                        result.Id = safeReader.GetInt64("ResourceID");
                        break;
                    }
                case (long)ResourceTypes.Room:
                    {
                        if (loadDetails)
                            result = safeReader.ToAppointmentResourceModality(accountID);
                        else
                            result = new AppointmentResourceModality();
                        result.ResourceType = new AppointmentResourceType((long)ResourceTypes.Room);
                        result.Id = safeReader.GetInt64("ResourceID");
                        break;
                    }
                case (long)ResourceTypes.Time:
                    {
                        if (loadDetails)
                            result = safeReader.ToAppointmentResourceTime(accountID);
                        else
                            result = new AppointmentResourceTime();
                        result.ResourceType = new AppointmentResourceType((long)ResourceTypes.Time);
                        result.Id = safeReader.GetInt64("ResourceID");
                        break;
                    }
                default:
                    throw new Exception("Appointment resource can not be converted. Unknown type passed");
            }
            return result;
        }

        public static AppointmentResourceTime ToAppointmentResourceTime(this IDataReader reader, long accountID)
        {
            using (SafeDataReader sr = new SafeDataReader(reader))
            {
                AppointmentResourceTime res = new AppointmentResourceTime();

                res.ResourceType = new AppointmentResourceType((long)ResourceTypes.Time);
                res.Account = new Account(accountID);
                res.Id = sr.GetInt64("TimeResourceID");
                res.StartTime = sr.GetDateTime("StartTime");
                if (res.StartTime == SqlDateTime.MinValue) res.StartTime = DateTime.MinValue;
                res.EndTime = sr.GetDateTime("EndTime");
                if (res.EndTime == SqlDateTime.MinValue) res.EndTime = DateTime.MinValue;

                return res;
            }
        }

        public static AppointmentStatus ToAppointmentStatus(this SafeDataReader sr)
        {
            AppointmentStatus s = new AppointmentStatus();
            s.Id = sr.GetInt32("Id");
            s.StatusName = sr.GetString("DisplayName");
            s.AppliedStatusName = sr.GetString("AppliedDisplayName");
            s.SortIndex = sr.GetInt32("SortIndex");
            s.IsVisible = sr.GetBoolean("IsVisible");
            s.IsSystemStatus = sr.GetBoolean("IsSystemStatus");
            s.Color = sr.GetString("Color");

            return s;
        }

        public static CPTModifier ToCPTModifier(this SafeDataReader sr)
        {
            CPTModifier result = new CPTModifier();
            result.ID = sr.GetInt32("ID");
            result.ExternalCode = sr.GetNullableString("ExternalCode");
            result.Code = sr.GetNullableString("Code");
            result.Description = sr.GetNullableString("Description");
            result.IsGlobal = sr.GetBoolean("IsGlobal");
            return result;
        }

        //CP: Fix
        //public void ExtractFromReader(SafeDataReader sr)
        //{
        //    Id = sr.GetInt32("RaceID");
        //    Description = sr.GetNullableString("Description");
        //    HL7Code = sr.GetNullableString("HL7Code");
        //}

        public static PatientGuarantor ToPatientGuarantor(this IDataReader reader)
        {
            PatientGuarantor r = new PatientGuarantor();

            using (SafeDataReader sr = new SafeDataReader(reader))
            {
                r.Id = sr.GetInt32("ID");
                r.PatientId = sr.GetNullableString("PatientID");
                r.LastName = sr.GetNullableString("LastName");
                r.FirstName = sr.GetNullableString("FirstName");
                r.MiddleName = sr.GetNullableString("MiddleName");
                r.Suffix = sr.GetNullableString("Suffix");
                // Sunil: Crashing on null date --> r.DOB = DateTime.Parse(sr.GetNullableString("DateOfBirth"));
                /*  string dob          = sr.GetNullableString("DateOfBirth");
                  DateTime dtdob;
                  if (!string.IsNullOrEmpty(dob) && DateTime.TryParse(dob, out dtdob))
                      r.DOB = dtdob;*/
                /*Sunil: please keep this check in place -- this cost me a lot of time because the update patch did not work properly*/
                if (!sr.IsDBNull("DateOfBirth"))
                    r.DOB = sr.GetDateTime("DateOfBirth");
                r.Sex = sr.GetNullableString("Sex");
                r.SSN = sr.GetNullableString("SSN");
                r.RelationshipToPatient = sr.GetNullableString("RelationshipToPatient");
                r.RelationshipToPatientDescription = sr.GetNullableString("RelationshipToPatientDescription");
                r.Address = sr.GetNullableString("Address");
                r.Address2 = sr.GetNullableString("Address2");
                r.Country = sr.GetNullableString("Country");
                r.City = sr.GetNullableString("City");
                r.State = sr.GetNullableString("State");
                r.Zip = sr.GetNullableString("Zip");
                r.Email = sr.GetNullableString("Email");
                r.HomePhone = sr.GetNullableString("HomePhone");
                r.EmploymentName = sr.GetNullableString("EmployerName");
                r.EmploymentAddress = sr.GetNullableString("EmployerAddress");
                r.EmploymentAddress2 = sr.GetNullableString("EmployerAddress2");
                r.EmploymentState = sr.GetNullableString("EmployerState");
                r.EmploymentCity = sr.GetNullableString("EmployerCity");
                r.EmploymentZip = sr.GetNullableString("EmployerZip");
                r.EmploymentPhone = sr.GetNullableString("EmployerPhone");
                r.ContactReason = sr.GetNullableString("ContactReason");
                r.AppointmentId = sr.GetNullableInt32("AppointmentId") ?? 0;
            }

            return r;
        }

        public static PatientComment ToPatientComment(this SafeDataReader sr)
        {
            PatientComment res = new PatientComment();
            res.Id = sr.GetInt32("PatientCommentID");
            res.Creator = sr.GetString("UserID");
            res.Text = sr.GetString("CommentText");
            res.LocationTransferredTo = sr.GetNullableString("LocationTransferredTo");
            res.Time = sr.GetDateTime("LastDate");
            res.Type = new CommentType(sr.GetInt32("CommentTypeID"), sr.GetString("TypeName"),/*sr.GetBoolean("IsVisible")*/false, false);
            res.CommentedEntityId = sr.GetNullableInt32("CommentedEntityId");
            int? t = sr.GetNullableInt32("CommentedEntityType");
            if (t == null) t = (int)CommentedEntityTypes.Patient;
            res.CommentedEntityType = (CommentedEntityTypes)(t);
            res.IsAlert = sr.GetBoolean("IsAlert");
            res.AccountEnumId = sr.GetNullableInt32("AccountEnumId");
            return res;
        }

        public static Address ToAddress(this SafeDataReader sr)
        {
            Address res = new Address();
            res.Id = sr.GetInt32("Id");
            res.Address1 = sr.GetNullableString("Address1") ?? string.Empty;
            res.Address2 = sr.GetNullableString("Address2") ?? string.Empty;
            res.City = sr.GetNullableString("City") ?? string.Empty;
            res.Country = sr.GetNullableString("Country") ?? string.Empty;
            res.ZipCode = sr.GetNullableString("Zip") ?? string.Empty;
            res.Fax = sr.GetNullableString("Fax") ?? string.Empty;
            res.Email = sr.GetNullableString("Email") ?? string.Empty;
            res.IsInternational = sr.GetBoolean("IsInternational");
            res.Mobile = sr.GetNullableString("Mobile") ?? string.Empty;
            res.Phone = sr.GetNullableString("Phone") ?? string.Empty;
            res.State = sr.GetNullableString("State") ?? string.Empty;
            return res;
        }

        public static PatientEmployment ToPatientEmployment(this SafeDataReader sr)
        {
            PatientEmployment r = new PatientEmployment();
            r.Id = sr.GetInt64("ID");
            r.Address = sr.ToAddress();
            r.EmployerName = sr.GetNullableString("EmployerName");
            r.EmploymentStatus = sr.GetNullableString("EmploymentStatusName");

            return r;
        }

        public static Diagnosis ToDiagnosis(this SafeDataReader sr)
        {
            Diagnosis result = new Diagnosis();
            if (sr.ContainsColumn("ID"))
                result.Id = sr.GetInt32("ID");
            //            result.Id =              -1;
            result.Code = sr.GetNullableString("CPTCode");

            if (sr.ContainsColumn("Flag"))
                result.Flag = sr.GetNullableString("Flag");

            result.ShortDescription = sr.GetNullableString("ShortDesc");
            result.MediumDescription = sr.GetNullableString("Mediumdesc");
            result.LongDescription = sr.GetNullableString("LongDesc");
            result.GlobalId = sr.GetNullableString("DiagnosGlobalID");
            result.IsGlobal = sr.GetInt32("IsGlobal") > 0;
            if (sr.ContainsColumn("OnsetDate"))
                result.OnsetDate = sr.GetNullableDateTime("OnsetDate");

            if (sr.ContainsColumn("AlertText"))
                result.AlertText = sr.GetNullableString("AlertText");

            if (sr.ContainsColumn("CodeCategoryId"))
            {
                var id = sr.GetNullableInt32("CodeCategoryId");
                if (id != null)
                    result.Category = id.ToString();
                else result.Category = null;
            }
            if (String.IsNullOrEmpty(result.GlobalId))
                result.GlobalId = Guid.NewGuid().ToString();
            if (sr.ContainsColumn("IsChronic"))
                result.IsChronic = sr.GetBoolean("IsChronic");
            return result;
        }

        //CP: Fix
        //public void MapInsuranceName(AppServices.SafeDataReader sr)
        //{
        //    if (sr.Read())
        //    {
        //        String name = sr.GetNullableString("name");

        //        if (!String.IsNullOrEmpty(name))
        //            this.PayerName = sr.GetNullableString("PayerDetails");
        //        else
        //            this.PayerName = sr.GetInt32("ID").ToString();

        //        return;
        //    }
        //    else
        //    {

        //    }
        //}

        //public static Payer ToPayer(this SafeDataReader sr)
        //{
        //    Payer p = new Payer();

        //    p.Id = sr.GetInt32("PayerID");
        //    p.PayerName = sr.GetString("Name");
        //    p.WebSite = sr.GetNullableString("WebSite");
        //    p.Address = sr.GetNullableString("Address1");
        //    p.Address2 = sr.GetNullableString("Address2");
        //    p.City = sr.GetNullableString("City");
        //    p.State = sr.GetNullableString("State");
        //    p.ZipCode = sr.GetNullableString("ZipCode");
        //    p.Phone = sr.GetNullableString("Phone");
        //    p.Fax = sr.GetNullableString("Fax");
        //    return p;
        //}


        public static Payer ToPayer(this SafeDataReader sr)
        {
            Payer res = new Payer();
            //            if(sr.ContainsColumn("State"))
            res.State = sr.GetNullableString("State");
            res.PayerName = sr.GetNullableString("PlanName");
            res.Id = sr.GetInt32("InsuranceID");
            res.Phone = sr.GetNullableString("Phone");
            res.Fax = sr.GetNullableString("Fax");
            res.PolicyNumber = sr.GetNullableString("PolicyNumber");
            res.ProductName = sr.GetNullableString("ProductName");
            res.GroupNumber = sr.GetNullableString("GroupNumber");
            res.InsuredFirstName = sr.GetNullableString("InsuredFirstName");
            res.LastName = sr.GetNullableString("InsuredLastName");
            DateTime? dob = sr.GetNullableDateTime("InsuredDateOfBirth");
            if (dob.HasValue)
                res.InsuredDOB = dob.Value;

            res.Gender = sr.GetNullableString("InsuredSex");

            if (!string.IsNullOrEmpty(res.Gender))
                res.Gender = res.Gender.ToLower().Substring(0, 1);

            if (res.Gender == "M" || res.Gender == "m")
                res.Gender = "Male";

            if (res.Gender == "F" || res.Gender == "f")
                res.Gender = "Female";

            if (res.Gender != "Male" && res.Gender != "Female")
                res.Gender = "Unknown";

            res.RelationShip = sr.GetNullableString("InsuredRelationshipToPatient");
            int? level = sr.GetNullableInt32("InsuranceLevel");
            if (level.HasValue)
                res.Level = (uint)level.Value;

            //if (sr.ContainsColumn("Address"))
            res.ZipCode = sr.GetNullableString("ZipCode");
            res.Address = sr.GetNullableString("Address");
            res.Address2 = sr.GetNullableString("Address2");
            res.City = sr.GetNullableString("City");

            //            if (sr.ContainsColumn("WebSite"))
            res.WebSite = sr.GetNullableString("WebSite");
            //quicklook
            res.IsEligible = sr.GetNullableBoolean("IsEligible");
            res.VendorPayerId = sr.GetNullableString("VendorPayerID");
            res.PiId = sr.GetInt32("ID");

            int? localPayerId = sr.GetNullableInt32("LocalPayerId");
            int? payerId = sr.GetNullableInt32("PayerId");
            res.IsGlobal = payerId.HasValue;
            if (localPayerId.HasValue || payerId.HasValue)
                res.PayerId = payerId.HasValue ? payerId.Value : localPayerId.Value;

            res.InsuredCity = sr.GetNullableString("InsuredEmploymentAddressCity");
            res.InsuredState = sr.GetNullableString("InsuredEmploymentAddressState");
            res.InsuredZip = sr.GetNullableString("InsuredEmploymentAddressZip");
            res.InsuredAddress = sr.GetNullableString("InsuredAddress");
            res.InsuredPhone = sr.GetNullableString("InsuredPhone");
            res.InsuredEmploymentName = sr.GetNullableString("InsuredEmploymentName");
            res.InsuredEmploymentAddress = sr.GetNullableString("InsuredEmploymentAddress");
            res.PayerAddressId = sr.GetNullableInt32("PayerAddressId") ?? -1;
            res.IsDeleted = sr.GetBoolean("Deleted");
            return res;
        }

        //public static PayerDto Convert2Dto(Payer payer)
        //{
        //    PayerDto dto = new PayerDto();

        //    dto.Address = payer.Address;
        //    dto.ExpirationDate = payer.ExpirationDate;
        //    dto.Comment = payer.Comment;
        //    dto.Fax = payer.Fax;
        //    dto.Gender = payer.Gender;
        //    dto.GroupNumber = payer.GroupNumber;
        //    dto.InsuredDOB = payer.InsuredDOB;
        //    dto.InsuredFirstName = payer.InsuredFirstName;
        //    dto.LastName = payer.LastName;
        //    dto.NPINumber = payer.NPINumber;
        //    if (payer.Patient == null)
        //        dto.PatientId = -1;
        //    else
        //        dto.PatientId = payer.Patient.Id;
        //    dto.InsuranceId = payer.Id;
        //    dto.Phone = payer.Phone;
        //    dto.PolicyNumber = payer.PolicyNumber;
        //    dto.ProductName = payer.ProductName;
        //    dto.Provider = payer.Provider;
        //    dto.RelationShip = payer.RelationShip;
        //    dto.PayerName = payer.PayerName;
        //    dto.LevelIndex = payer.Level;
        //    dto.PayerAddressId = payer.PayerAddressId;
        //    dto.WebSite = payer.WebSite;
        //    dto.PayerId = payer.PayerId;
        //    dto.Address2 = payer.Address2;
        //    dto.ZipCode = payer.ZipCode;
        //    dto.City = payer.City;
        //    dto.State = payer.State;
        //    dto.IsGlobal = payer.IsGlobal;

        //    if (payer.VerificationStatus != null)
        //    {
        //        dto.VerificationStatus.AdditionalInfo = payer.VerificationStatus.AdditionalInfo;
        //        dto.VerificationStatus.IsComplete = payer.VerificationStatus.IsComplete;
        //        dto.VerificationStatus.IsSuccessful = payer.VerificationStatus.IsSuccessful;
        //        dto.VerificationStatus.UserText = payer.VerificationStatus.UserText;
        //        dto.VerificationStatus.VerificationDateTime = payer.VerificationStatus.VerificationDateTime;
        //        dto.VerificationStatus.ValidationRequestID = payer.VerificationStatus.ValidationRequestID;
        //    }
        //    //quicklook
        //    dto.IsEligible = payer.IsEligible;
        //    dto.VendorPayerId = payer.VendorPayerId;
        //    dto.PiId = payer.PiId;

        //    dto.InsuredCity = payer.InsuredCity;
        //    dto.InsuredState = payer.InsuredState;
        //    dto.InsuredZip = payer.InsuredZip;
        //    dto.InsuredAddress = payer.InsuredAddress;
        //    dto.InsuredPhone = payer.InsuredPhone;
        //    dto.InsuredEmploymentName = payer.InsuredEmploymentName;
        //    dto.InsuredEmploymentAddress = payer.InsuredEmploymentAddress;
        //    dto.IsDeleted = payer.IsDeleted;

        //    return dto;
        //}

        public static PatientAuthorization ToPatientAuthorization(this SafeDataReader sr)
        {
            PatientAuthorization r = new PatientAuthorization();
            //            if (sr.ContainsColumn("PatientWalletId"))
            r.Id = sr.GetInt32("PatientWalletId");

            //            if (sr.ContainsColumn("UnitVolume"))
            r.UnitVolume = sr.GetNullableString("UnitVolume");

            //            if (sr.ContainsColumn("AuthReferringId"))
            r.AuthReferringId = sr.GetNullableString("AuthReferringId");

            //            if (sr.ContainsColumn("UnitGradation"))
            r.UnitGradation = sr.GetNullableString("UnitGradation");

            //            if (sr.ContainsColumn("Used"))
            r.Used = sr.GetNullableInt32("Used");

            //            if (sr.ContainsColumn("AuthorizationNumber"))
            r.AuthorizationNumber = sr.GetNullableString("AuthorizationNumber");

            //            if (sr.ContainsColumn("PatientId"))
            r.PatientId = sr.GetInt32("PatientId");

            //            if (sr.ContainsColumn("Date"))
            r.Date = sr.GetNullableDateTime("Date");

            //            if (sr.ContainsColumn("Description"))
            r.Description = sr.GetNullableString("Description");

            //            if (sr.ContainsColumn("ProcedureId"))
            r.ProcedureId = sr.GetNullableString("ProcedureId");

            //            if (sr.ContainsColumn("ProcedureDescription"))
            r.ProcedureDescription = sr.GetNullableString("ProcedureDescription");

            //            if (sr.ContainsColumn("Payer"))
            r.Payer = sr.GetNullableString("Payer");

            //            if (sr.ContainsColumn("Effective"))
            r.Effective = sr.GetNullableDateTime("Effective");

            //            if (sr.ContainsColumn("Expires"))
            r.Expires = sr.GetNullableDateTime("Expires");

            //            if(sr.ContainsColumn("Count"))
            r.Count = sr.GetInt32("Count");

            //            if (sr.ContainsColumn("PayerId"))
            r.PayerId = sr.GetNullableInt32("PayerId") ?? -1;

            //if (sr.ContainsColumn("PayerIsGlobal"))
            r.PayerIsGlobal = sr.GetBoolean("PayerIsGlobal");
            r.IsDeleted = sr.GetBoolean("IsDeleted");

            if (sr.ContainsColumn("EnumAuthorizationUserStatus"))
                r.UserStatus = sr.GetNullableString("EnumAuthorizationUserStatus");

            r.StatusReason = sr.GetNullableString("StatusReason");
            r.RemovalReason = sr.GetNullableString("RemovalReason");

            if (sr.ContainsColumn("RefFirst"))
                r.AuthReferringFirst = sr.GetNullableString("RefFirst");

            if (sr.ContainsColumn("RefLast"))
                r.AuthReferringLast = sr.GetNullableString("RefLast");

            if (sr.ContainsColumn("GlobalDesc"))
                r.GlobalDescription = sr.GetNullableString("GlobalDesc");

            if (sr.ContainsColumn("LocalDesc"))
                r.LocalDescription = sr.GetNullableString("LocalDesc");

            return r;
        }

        public static TaskTemplate ToTaskTemplate(this SafeDataReader sr)
        {
            TaskTemplate result = new TaskTemplate
            {
                Id = sr.GetInt32("TemplateId"),
                Name = sr.GetString("TemplateName")
            };
            return result;
        }

        public static AppointmentCheckListItem ToAppointmentCheckListItem(this SafeDataReader sr)
        {
            AppointmentCheckListItem result = new AppointmentCheckListItem();
            result.Id = sr.GetInt64("ItemId");
            result.Name = sr.GetString("ItemName");
            long? templateId = sr.GetNullableInt32("TemplateId");
            if (templateId.HasValue)
            {
                result.Template = sr.ToTaskTemplate();
            }
            return result;
        }

        public static AppointmentCheckListValue ToAppointmentCheckListValue(this SafeDataReader sr)
        {
            AppointmentCheckListValue result = new AppointmentCheckListValue();
            result.Item = sr.ToAppointmentCheckListItem();

            long? valueId = sr.GetNullableInt64("ValueId");
            if (valueId.HasValue)
            {
                result.Id = sr.GetInt64("ValueId");
                result.Value = sr.GetBoolean("Value");
            }
            return result;
        }

        public static PatientIdentifier ToPatientIdentifier(SafeDataReader sr)
        {
            return new PatientIdentifier
            {
                Id = sr.GetInt32("ID"),
                Identifier = sr.GetString("Identifier"),
                PatientId = (long)sr.GetDecimal("PatientId"),
                Source = sr.GetNullableString("Source"),
                Sequence = sr.GetNullableInt32("Sequence"),
                ExtIdentifierSourceId = sr.GetNullableString("ExtIdentifierSourceId"),
                ExtIdentifierSource = sr.GetNullableString("ExtIdentifierSource"),
                IsActive = sr.GetBoolean("IsActive"),
                CreateDate = sr.GetDateTime("CreateDate"),
                CreateUser = sr.GetNullableString("CreateUser")
            };
        }

        public static PathologyDetail ToPathologyDetail(this SafeDataReader sr)
        {
            PathologyDetail pd = new PathologyDetail();

            pd.Id = sr.GetInt32("Id");
            pd.PathDetails = sr.GetNullableString("PathResultsDetails");
            pd.PathResult = sr.GetString("PathResults");
            pd.ResultStatus = sr.GetNullableString("ResultStatus");
            return pd;
        }

        public static PathologyDiagnosis ToPathologyDiagnosis(this SafeDataReader sr)
        {
            PathologyDiagnosis diagnosis = new PathologyDiagnosis();
            diagnosis.Id = sr.GetInt32("Id");
            diagnosis.Code = sr.GetString("DiagnosisCode");
            diagnosis.CodeType = sr.GetString("DiagnosisCodeType");
            diagnosis.ShortDesc = sr.GetNullableString("ShortDesc");

            return diagnosis;
        }

        public static Tumor ToTumor(this SafeDataReader sr)
        {
            Tumor t = new Tumor();
            t.Id = sr.GetInt32("ID");
            t.Laterality = sr.GetNullableString("Laterality");
            t.NodalStatus = sr.GetNullableString("NodalStatus");
            t.TumorSize = sr.GetNullableString("TumorSize");
            t.BiopsyType = sr.GetNullableString("BiopsyType");
            return t;
        }

        public static Mammography ToMammography(this SafeDataReader sr)
        {
            Mammography m = new Mammography();
            m.Id = sr.GetInt32("ID");
            m.Laterality = sr.GetNullableString("Laterality");
            m.IsPregnant = sr.GetNullableBoolean("IsPregnant");
            m.FollowUpDate = sr.GetNullableDateTime("FollowUpDate");
            m.IsDeleted = sr.GetBoolean("IsDeleted");
            m.HasImplants = sr.GetNullableBoolean("HasImplants");
            m.BIRADcode = sr.GetNullableString("BIRADCode");
            m.BreastDensity = sr.GetNullableString("BreastDensity");
            m.IsDiscordant = sr.GetNullableBoolean("IsDiscordant");
            m.SurgeonName = sr.GetNullableString("SurgeonName");
            m.Laterality = sr.GetNullableString("Laterality");
            m.NotifiedDate = sr.GetNullableDateTime("NotifiedDate");
            m.NotifiedBy = sr.GetNullableString("NotifiedBy");
            m.LayletterSentDate = sr.GetNullableDateTime("LayletterSentDate");
            m.Procedures = sr.GetNullableString("Procedures");
            return m;
        }

        public static SchedulerImage ToSchedulerImage(this SafeDataReader sr)
        {
            return new SchedulerImage(ImageType.PatientId, sr.GetInt64("ID"));
        }

        public static AddressType ToAddressType(this SafeDataReader sr)
        {
            AddressType r = new AddressType();
            if (sr.ContainsColumn("PatientBillingAddressId"))
                r.Id = sr.GetInt32("PatientBillingAddressId");
            r.Address1 = sr.GetNullableString("Address1");
            r.City = sr.GetNullableString("City");
            r.State = sr.GetNullableString("State");
            r.ZipCode = sr.GetNullableString("ZipCode");

            if (sr.ContainsColumn("Address2"))
                r.Address2 = sr.GetNullableString("Address2");
            if (sr.ContainsColumn("Country"))
                r.Country = sr.GetNullableString("Country");
            if (sr.ContainsColumn("County"))
                r.County = sr.GetNullableString("County");
            if (sr.ContainsColumn("Email"))
                r.Email = sr.GetNullableString("Email");
            if (sr.ContainsColumn("Fax"))
                r.Fax = sr.GetNullableString("Fax");
            if (sr.ContainsColumn("InternationalProvince"))
                r.InternationalProvince = sr.GetNullableString("InternationalProvince");
            if (sr.ContainsColumn("Phone"))
                r.Phone = sr.GetNullableString("Phone");
            if (sr.ContainsColumn("POBox"))
                r.POBox = sr.GetNullableString("POBox");
            return r;
        }

        public static CreditCardPayment ToCreditCardPayment(this SafeDataReader sr)
        {
            CreditCardPayment r = new CreditCardPayment();
            r.BillingAddress = sr.ToAddressType();
            if (sr.ContainsColumn("ExpiratioMonth"))
                r.CardExpirationMonth = sr.GetNullableString("ExpiratioMonth");
            if (sr.ContainsColumn("ExpirationYear"))
                r.CardExpirationYear = sr.GetNullableString("ExpirationYear");
            if (sr.ContainsColumn("CardNumber"))
                r.CardNumber = sr.GetNullableString("CardNumber");
            if (sr.ContainsColumn("CardCVV"))
                r.CardCVV = sr.GetNullableString("CardCVV");
            if (sr.ContainsColumn("CardType"))
                r.CardType = sr.GetNullableString("CardType");
            if (sr.ContainsColumn("PayerName"))
                r.PayerName = sr.GetNullableString("PayerName");
            return r;
        }

        public static ChequePayment ToChequePayment(this SafeDataReader sr)
        {
            ChequePayment r = new ChequePayment();
            if (sr.ContainsColumn("PayerName"))
                r.PayerName = sr.GetNullableString("PayerName");
            if (sr.ContainsColumn("BankName"))
                r.ChequeBankName = sr.GetNullableString("BankName");

            if (sr.ContainsColumn("RoutingNumber"))
                r.ChequeRoutingNumber = sr.GetNullableString("RoutingNumber");

            if (sr.ContainsColumn("AccountNumber"))
                r.ChequeAccountNumber = sr.GetNullableString("AccountNumber");

            if (sr.ContainsColumn("ChequeNumber"))
                r.ChequeNumber = sr.GetNullableString("ChequeNumber");

            if (sr.ContainsColumn("ImageCount"))
                r.ImagesCount = sr.GetInt32("ImageCount");

            if (sr.ContainsColumn("ChequeDate"))
            {
                DateTime dt = DateTime.MinValue;
                dt = sr.GetDateTime("ChequeDate");
                r.ChequeDate = dt;
                /*          if (DateTime.TryParse(sr.GetNullableString("ChequeDate"), out dt))
                              r.ChequeDate = dt;*/
            }
            return r;
        }

        public static PatientPayment ToPatientPayment(this SafeDataReader sr)
        {
            PatientPayment r = new PatientPayment();
            r.PatientPaymentOrderID = sr.GetInt32("PatientPaymentOrderID");
            r.PatientIntId = sr.GetInt32("PatientIntId");
            if (sr.GetNullableInt64("AppointmentId") != null)
                r.AppointmentId = Convert.ToInt32(sr.GetInt64("AppointmentId"));
            r.PaymentAmount = sr.GetDecimal("Amount");
            r.PaymentDate = sr.GetDateTime("CreateDate");
            r.IsByCreditCardNew = sr.GetNullableBoolean("IsByCreditCardNew") ?? false;
            r.IsByCreditCardAuth = sr.GetNullableBoolean("IsByCreditCardAuth") ?? false;
            r.IsByCheque = sr.GetNullableBoolean("IsByCheque") ?? false;
            r.IsByCash = sr.GetNullableBoolean("IsByCash") ?? false;
            if (!r.IsByCash)
            {
                if (r.IsByCreditCardAuth)
                    r.CreditCardAuthorization = sr.GetNullableString("CreditCardAuthorization");

                if (r.IsByCreditCardNew || r.IsByCreditCardAuth)
                    r.CreditCardInfo = sr.ToCreditCardPayment();
                else if (r.IsByCheque)
                    r.ChequeInfo = sr.ToChequePayment();
            }
            r.PaymentStatus = sr.GetNullableString("PaymentStatus");
            r.OrderId = sr.GetNullableString("OrderId");
            r.ProcedureCode = sr.GetNullableString("ProcedureCode");

            r.ScheduleFeeName = sr.GetNullableString("FeeScheduleName");
            if (sr.ContainsColumn("ProcedureDescription"))
                r.ProcedureDescription = sr.GetNullableString("ProcedureDescription");
            if (sr.ContainsColumn("CollectedLocationId"))
                r.CollectedLocationId = (int?)sr.GetNullableInt64("CollectedLocationId");
            if (sr.ContainsColumn("CollectedLocationName"))
                r.CollectedLocationName = sr.GetNullableString("CollectedLocationName");
            if (sr.ContainsColumn("Comment"))
                r.Comment = sr.GetNullableString("Comment");
            return r;
        }

        public static AppointmentResourcePatient ToAppointmentResourcePatient(this IDataReader reader, long accountID)
        {
            AppointmentResourcePatient patient = new AppointmentResourcePatient();

            using (SafeDataReader sr = new SafeDataReader(reader))
            {
                patient.Account = new Account(accountID);
                patient.Id = Convert.ToInt64(sr.GetDecimal("PatientId"));
                patient.RecordNumber = sr.GetNullableString("RecordNum");
                patient.ExternalID = sr.GetNullableString("ExternalID");
                patient.FirstName = sr.GetNullableString("FirstName");
                patient.MiddleName = sr.GetNullableString("MiddleName");
                patient.LastName = sr.GetNullableString("LastName");
                patient.MaidenName = sr.GetNullableString("MaidenName");
                patient.MaritalStatus = sr.GetNullableString("MaritalStatus");
                patient.AdvanceDirectives = sr.GetNullableDateTime("AdvanceDirectives");
                patient.ConsentForm = sr.GetNullableDateTime("ConsentForm");
                patient.SSN = sr.GetNullableString("SSN");
                //sunil:03/05/2017 patientinfo table location column = Location.Location which is not an integer
                //if (sr.ContainsColumn("Location"))
                //{
                //    int loc;
                //    if (int.TryParse(sr.GetNullableString("Location"), out loc))
                //        patient.LocationId = loc;
                //}
                if (sr.ContainsColumn("LocationId"))
                    patient.LocationId = sr.GetNullableInt32("LocationId");

                if (sr.ContainsColumn("LocationName"))
                {
                    patient.LocationName = sr.GetNullableString("LocationName");
                    if (string.IsNullOrEmpty(patient.LocationName))
                        patient.LocationName = "Unknown";
                }
                if (sr.ContainsColumn("Location"))
                {
                    //patient.AbbadoxLocation = sr.GetNullableString("AbbadoxLocation");
                    patient.AbbadoxLocation = sr.GetNullableString("Location");
                }

                string dob = sr.GetNullableString("BirthDay");
                DateTime res;

                if (DateTime.TryParse(dob, out res))
                    patient.Dob = res;

                patient.Gender = sr.GetNullableString("Gender");

                if (patient.Gender == "MALE" || patient.Gender == "M" || patient.Gender == "m")
                    patient.Gender = "Male";

                if (patient.Gender == "FEMALE" || patient.Gender == "F" || patient.Gender == "f")
                    patient.Gender = "Female";

                if (patient.Gender != "Male" && patient.Gender != "Female")
                    patient.Gender = "Unknown";

                patient.Address1 = sr.GetNullableString("Address");
                patient.Address2 = sr.GetNullableString("Address2");
                patient.City = sr.GetNullableString("City");
                patient.State = sr.GetNullableString("State");
                patient.ZipCode = sr.GetNullableString("Zipcode");
                patient.Phone = sr.GetNullableString("Phone");
                patient.Mobile = sr.GetNullableString("Mobile");
                patient.Emergency = sr.GetNullableString("Emergency");
                if (sr.ContainsColumn("WorkPhone"))
                    patient.WorkPhone = sr.GetNullableString("WorkPhone");
                patient.Ethnicicty = sr.GetNullableString("Ethnicity");
                //                patient.Race         = sr.GetNullableString("Race");
                patient.Fax = sr.GetNullableString("Fax");
                patient.Email = sr.GetNullableString("Email");
                patient.IsActive = sr.GetBoolean("isActive");
                if (sr.ContainsColumn("IsVIP"))
                    patient.IsVIP = sr.GetBoolean("IsVIP");
                patient.IsDeceased = sr.GetBoolean("isDeceased");
                patient.DeceaseDate = sr.GetNullableDateTime("DateOfDeath");
                patient.CauseOfDeath = sr.GetNullableString("CauseOfDeath");
                patient.IsSelfPay = sr.GetBoolean("IsSelfPay");


                ////TODO: Update Flags here
                //string confirmType = sr.GetNullableString("PatientType");
                //if (!String.IsNullOrEmpty(confirmType))
                //{
                //    Int32 type = 0;
                //    if (Int32.TryParse(confirmType, out type))
                //        patient.Confirmation = type;
                //}

                patient.ConfirmBySms = sr.GetNullableBoolean("ContactBySMS") ?? false;
                patient.ContactByCall = sr.GetNullableBoolean("ContactByCall") ?? false;
                patient.ContactByEmail = sr.GetNullableBoolean("ContactByEmail") ?? false;
                patient.ConfirmByMail = sr.GetNullableBoolean("ContactByMail") ?? false;
                patient.ConfirmByMobile = sr.GetNullableBoolean("ContactByMobile") ?? false;
                patient.NotifyBySms = sr.GetNullableBoolean("NotifytBySMS") ?? false;
                patient.NotifyByCall = sr.GetNullableBoolean("NotifytByCall") ?? false;
                patient.NotifyByEmail = sr.GetNullableBoolean("NotifytByEmail") ?? false;
                patient.NotifyByMail = sr.GetNullableBoolean("NotifytByMail") ?? false;
                patient.NotifyByMobile = sr.GetNullableBoolean("NotifytByMobile") ?? false;
                patient.IsOptOutManualCalls = sr.GetNullableBoolean("IsOptOutManualCalls") ?? false;
                patient.IsOptOutRoboCalls = sr.GetNullableBoolean("IsOptOutRoboCalls") ?? false;
                patient.IsOptOutLetters = sr.GetNullableBoolean("IsOptOutLetters") ?? false;

                int? langId = sr.GetNullableInt32("LanguageID");
                patient.TranslationLanguage = langId.HasValue ? langId.Value.ToString() : String.Empty;

                bool? requresTransaction = sr.GetNullableBoolean("RequiresTranslator");
                if (requresTransaction.HasValue)
                    patient.RequiresTranslation = requresTransaction.Value;

                string comments = sr.GetNullableString("Comments");
                if (!String.IsNullOrEmpty(comments))
                {
                    String[] chunks = comments.Split(new string[] { "```" }, StringSplitOptions.None);
                    if (chunks.Length == 4)
                    {
                        patient.PatientStatus = chunks[0];
                        DateTime parseRes;
                        //if (DateTime.TryParse(chunks[1], out parseRes))
                        //patient.DeceaseDate = parseRes;
                        //patient.CauseOfDeath = chunks[2];
                        //                        patient.SpecialNeeds.Add(chunks[3]);
                    }
                }

                if (sr.ContainsColumn("VerificationStatus"))
                    patient.VerificationStatus = sr.GetNullableString("VerificationStatus");

                patient.EnumHeardOfUsName = sr.GetNullableString("EnumHeardOfUsName");

                if (sr.ContainsColumn("LastAppointmentDate"))
                    patient.LastAppointmentDate = sr.GetNullableDateTime("LastAppointmentDate");

                patient.LastModificationDateTime = sr.GetDateTime("LastModifiedDateTime");
            }

            return patient;
        }

        public static AppointmentResourcePatient ToAppointmentResourcePatient(this SafeDataReader sr)
        {
            AppointmentResourcePatient p = new AppointmentResourcePatient();
            p.Id = sr.GetInt32("AutoCount");
            p.FirstName = sr.GetNullableString("FirstName");
            p.LastName = sr.GetNullableString("LastName");
            p.Dob = sr.GetNullableDateTime("DOB") ?? DateTime.MinValue;

            p.Gender = sr.GetNullableString("Sex");

            if (p.Gender == "MALE" || p.Gender == "M" || p.Gender == "m")
                p.Gender = "Male";

            if (p.Gender == "FEMALE" || p.Gender == "F" || p.Gender == "f")
                p.Gender = "Female";

            if (p.Gender != "Male" && p.Gender != "Female")
                p.Gender = "Unknown";


            p.Address1 = sr.GetNullableString("Address");
            p.Address2 = sr.GetNullableString("Address2");
            p.City = sr.GetNullableString("City");
            p.State = sr.GetNullableString("State");
            p.ZipCode = sr.GetNullableString("ZipCode");
            p.Phone = sr.GetNullableString("Phone");
            p.RecordNumber = sr.GetNullableString("Mrn");
            return p;
        }

        public static ResourceDuration ToResourceDuration(this SafeDataReader sr)
        {
            var r = new ResourceDuration
            {
                Id = sr.GetInt32("ResourceDurationOverrideId"),
                ActualDuration = sr.GetInt32("ActualDuration"),
                AdditionalLeadTime = sr.GetInt32("AddLeadTime"),
                DecrementTime = 0,
                IncrementTime = 0,
                SedationTime = sr.GetInt32("SedationTime")
            };
            return r;
        }

        public static Procedure ToProcedure(this SafeDataReader sr)
        {
            Procedure result = new Procedure();

            if (sr.ContainsColumn("ID"))
                result.Id = sr.GetNullableInt32("ID") ?? 0;
            result.Code = sr.GetNullableString("ICD9Code");
            result.ShortDescription = sr.GetNullableString("ShortDesc");
            result.MediumDescription = sr.GetNullableString("Mediumdesc");
            result.LongDescription = sr.GetNullableString("LongDesc");
            result.GlobalId = sr.GetString("ProcedureGlobalID");
            if (sr.ContainsColumn("MammogramType"))
                result.MammogramType = sr.GetNullableString("MammogramType");

            if (sr.ContainsColumn("AlertText"))
                result.AlertText = sr.GetNullableString("AlertText");

            if (sr.ContainsColumn("HCPCScodeName"))
            {
                result.HCPCScodeName = sr.GetNullableString("HCPCScodeName");
            }
            if (sr.ContainsColumn("CodeCategoryId"))
            {
                var categoryId = sr.GetNullableInt32("CodeCategoryId");
                result.Category = categoryId != null ? categoryId.ToString() : null;
            }

            if (sr.ContainsColumn("Amount"))
            {
                result.Amount = sr.GetNullableString("Amount");
            }

            if (sr.ContainsColumn("ProcNote"))
            {
                result.ProcNote = sr.GetNullableString("ProcNote");
            }

            if (sr.ContainsColumn("Volume"))
            {
                result.Volume = sr.GetNullableString("Volume");
            }

            if (sr.ContainsColumn("DefaultOverhead"))
            {
                result.TimeOverheadMinutes = sr.GetNullableInt32("DefaultOverhead");
            }
            else
            {
                result.TimeOverheadMinutes = sr.GetInt32("TimeOverhead");
            }

            if (sr.ContainsColumn("DefaultAmount"))
            {
                result.Amount = sr.GetNullableString("DefaultAmount");
            }

            if (sr.ContainsColumn("DefaultVolume"))
            {
                result.Volume = sr.GetNullableString("DefaultVolume");
            }
            if (sr.ContainsColumn("DefaultHCPCS"))
            {
                result.HCPCScodeName = sr.GetNullableString("DefaultHCPCS");
            }
            if (sr.ContainsColumn("DisplayOrder"))
            {
                result.DisplayOrder = sr.GetNullableInt32("DisplayOrder") ?? 0;
            }
            result.IsGlobal = sr.GetInt32("IsGlobal") > 0;
            //            result.IsOrderRequired   = sr.GetBoolean("IsOrderRequired");
            result.LinkedRoomId = sr.GetNullableInt64("ModalityId");

            if (sr.ContainsColumn("AppointmentID"))
                result.LinkedApptId = sr.GetNullableInt64("AppointmentID");
            if (sr.ContainsColumn("OverrideCreationMode"))
                result.OverrideCreationMode = sr.GetNullableInt32("OverrideCreationMode");
            if (String.IsNullOrEmpty(result.GlobalId))
                result.GlobalId = Guid.NewGuid().ToString();
            if (sr.ContainsColumn("Type"))
                result.LinkedRoomTypeId = (int)(sr.GetNullableInt64("Type") ?? 0);
            // modifiers
            if (sr.ContainsColumn("Modifier1"))
            {
                if (!string.IsNullOrEmpty(sr.GetNullableString("Modifier1")))
                    result.Modifiers.Add(new CPTModifier(sr.GetString("Modifier1")));
                if (!string.IsNullOrEmpty(sr.GetNullableString("Modifier2")))
                    result.Modifiers.Add(new CPTModifier(sr.GetString("Modifier2")));
                if (!string.IsNullOrEmpty(sr.GetNullableString("Modifier3")))
                    result.Modifiers.Add(new CPTModifier(sr.GetString("Modifier3")));
                if (!string.IsNullOrEmpty(sr.GetNullableString("Modifier4")))
                    result.Modifiers.Add(new CPTModifier(sr.GetString("Modifier4")));
            }
            //result.PatientInsuranceId = sr.GetNullableInt32("PatientInsuranceId");
            // result.PatientGuarantorId = sr.GetNullableInt32("PatientGuarantorId");
            if (sr.ContainsColumn("IsSelfPay"))
                result.IsSelfPay = sr.GetBoolean("IsSelfPay");
            // ResourceDurationOverride
            if (!sr.ContainsColumn("ResourceDurationOverrideId") || sr.IsDBNull("ResourceDurationOverrideId"))
                return result;

            result.ResourceDurationOverride = sr.ToResourceDuration();
            return result;
        }

        public static ModalityType ToModalityType(this IDataReader reader)
        {
            using (SafeDataReader sr = new SafeDataReader(reader))
            {
                ModalityType res = new ModalityType();
                res.Id = sr.GetInt64("TypeId");
                res.Name = sr.GetString("Name");

                if (sr.ContainsColumn("Id")) res.LocationId = sr.GetInt32("Id");
                if (sr.ContainsColumn("Location")) res.LocationCode = sr.GetNullableString("Location");
                /*
                                long? modId = sr.GetNullableInt64("ModalityID");
                                if (modId != null)
                                    res.ModalityId = (int) modId;
                */
                if (sr.ContainsColumn("AllowComparision"))
                {
                    res.AllowComparision = sr.GetBoolean("AllowComparision");
                }
                return res;
            }
        }

        public static ResourceLocation ToResourceLocation(this IDataReader reader)
        {
            using (SafeDataReader sr = new SafeDataReader(reader))
            {
                ResourceLocation res = new ResourceLocation();
                res.Id = sr.GetInt64("LocationId");
                res.LocationName = sr.GetNullableString("LocationName");
                res.AbbadoxLocation = sr.GetNullableString("AbbadoxLocation");
                res.Address = sr.GetNullableString("Address");
                res.Zip = sr.GetNullableString("Zip");
                res.IsForceStateMatch = sr.GetBoolean("IsForceStateMatch");
                res.LocationAlert = sr.GetNullableString("LocationAlert");
                res.PathToImage = sr.ContainsColumn("PathToImage") ? sr.GetNullableString("PathToImage") : null;
                res.Area = new Area(sr.GetNullableString("Country"), sr.GetNullableString("State"), sr.GetNullableString("City"));
                return res;
            }
        }

        //CP: Fix
        //internal static Area ToAreaExtractFromDto(Common.DataTransferObjects.Location.ResourceAreaDto a)
        //{
        //    Area result = new Area();
        //    result.Id = a.Id;
        //    result.City = a.City;
        //    result.Country = a.Country;
        //    result.State = a.State;
        //    return result;
        //}

        public static AppointmentResourceModality ToAppointmentResourceModality(this IDataReader reader, long accountID)
        {
            AppointmentResourceModality res = new AppointmentResourceModality();

            using (SafeDataReader r = new SafeDataReader(reader))
            {
                res.Id = r.GetInt64("ModalityID");
                res.Estimate = new TimeSpan(0, r.GetInt32("Estimate"), 0);
                res.Location = new ResourceLocation(r.GetInt64("Location"));
                res.ModalityType = new ModalityType(r.GetInt64("Type"), r.GetString("TypeName"));
                res.ResourceType = new AppointmentResourceType((long)ResourceTypes.Room);
                res.RoomName = r.GetString("Name");
                res.AccessionNumber = r.GetNullableString("AccessionNumber");
                res.RoomType = new RoomType(r.GetInt64("Type"), r.GetNullableString("TypeName"));
                res.Account = new Account(accountID);
                res.VirtualRoomId = r.GetNullableInt32("VirtualRoomId");
                res.IsMammographyResource = r.GetBoolean("IsMammographyResource");
                res.IsOnlineRoom = r.GetBoolean("IsOnlineRoom");
                res.CreateOrder = r.GetBoolean("CreateOrder");
                res.CreateEncounter = r.GetBoolean("CreateEncounter");
                res.IsActive = r.GetBoolean("IsActive");
            }
            res.WorkingSchedule = new WorkingSchedule();

            return res;
        }

        public static WorkingScheduleItem ToWorkingScheduleItem(this SafeDataReader safeReader)
        {
            WorkingScheduleItem res = new WorkingScheduleItem();
            if (safeReader.ContainsColumn("SmWhId"))
                res.Id = safeReader.GetInt32("SmWhId");
            else if (safeReader.ContainsColumn("SpWhId"))
                res.Id = safeReader.GetInt32("SpWhId");
            else if (safeReader.ContainsColumn("SaWhId"))
                res.Id = safeReader.GetInt32("SaWhId");

            if (safeReader.ContainsColumn("ModalityID"))
                res.ModalityId = safeReader.GetInt64("ModalityID");
            else if (safeReader.ContainsColumn("DictatorID"))
                res.ModalityId = safeReader.GetInt32("DictatorID");

            res.WeekDay = safeReader.GetString("WeekDayName");
            res.StartTime = safeReader.GetDateTime("StartTime");
            res.EndTime = safeReader.GetDateTime("EndTime");
            res.BreakFrom = safeReader.GetNullableDateTime("BreakFrom");
            res.BreakTo = safeReader.GetNullableDateTime("BreakTo");
            res.IsActive = safeReader.GetBoolean("isActive");
            return res;
        }

        public static Holiday ToHoliday(this SafeDataReader sr)
        {
            Holiday result = new Holiday();
            result.Id = sr.GetInt64("Id");
            result.Name = sr.GetString("Name");
            result.StartTime = sr.GetDateTime("StartDate");
            result.EndTime = sr.GetDateTime("EndTime");
            result.Repeat = sr.GetBoolean("AllDay");
            return result;
        }
        public static SchedulerModalityVirtualRoom ToSchedulerModalityVirtualRoom(this IDataReader idr)
        {
            var sr = new SafeDataReader(idr);
            int id = sr.GetInt32("Id");
            if (id == 0)
                return null;

            SchedulerModalityVirtualRoom smvr = new SchedulerModalityVirtualRoom(id);
            smvr.Name = sr.GetString("Name");
            smvr.Description = sr.GetString("Description");
            smvr.IsActive = sr.GetBoolean("IsActive");
            smvr.AllowedExamCount = sr.GetNullableInt32("AllowedExamCount") ?? 0;
            smvr.AllowedExamCountMon = sr.GetNullableInt32("AllowedExamCountMon") ?? -1;
            smvr.AllowedExamCountTue = sr.GetNullableInt32("AllowedExamCountTue") ?? -1;
            smvr.AllowedExamCountWed = sr.GetNullableInt32("AllowedExamCountWed") ?? -1;
            smvr.AllowedExamCountThu = sr.GetNullableInt32("AllowedExamCountThu") ?? -1;
            smvr.AllowedExamCountFri = sr.GetNullableInt32("AllowedExamCountFri") ?? -1;
            smvr.AllowedExamCountSat = sr.GetNullableInt32("AllowedExamCountSat") ?? -1;
            smvr.AllowedExamCountSun = sr.GetNullableInt32("AllowedExamCountSun") ?? -1;
            smvr.IsLinked = sr.GetBoolean("IsLinked");
            return smvr;
        }

        public static MammographyHistory ToMammographyHistory(this SafeDataReader sr)
        {
            MammographyHistory r = new MammographyHistory();
            r.AppointmentId = sr.GetNullableInt64("AppointmentId");
            r.BiradCode = sr.GetNullableString("BIRADCode");
            r.BreastDensity = sr.GetNullableString("BreastDensity");
            r.MammogramType = sr.GetNullableString("MammogramType") ?? string.Empty;
            r.LayLetterSent = sr.GetNullableDateTime("LayletterSentDate");
            r.FollowUpDate = sr.GetNullableDateTime("FollowUpDate");
            r.DOS = sr.GetDateTime("StartTime");
            r.ProcedureCodes = sr.GetNullableString("ProcedureCodes");
            r.Procedures = sr.GetNullableString("Procedures");
            return r;
        }

        public static PaymentFee ToPaymentFee(this SafeDataReader sr)
        {
            PaymentFee result = new PaymentFee();

            result.Id = sr.GetInt32("Id");
            result.FeeScheduleName = sr.GetString("FeeScheduleName");
            result.FeeScheduleType = sr.GetString("FeeScheduleType");
            result.EffectiveDate = sr.GetDateTime("EffectiveDate");
            result.ExpirationDate = sr.GetNullableDateTime("ExpirationDate");
            if (result.ExpirationDate.HasValue && result.ExpirationDate.Value < DateTime.Parse("01/01/1800")) result.ExpirationDate = null;
            result.Amount = sr.GetDecimal("Amount");
            result.CreatedBy = sr.GetNullableString("CreatedBy");
            result.CreatedOn = sr.GetDateTime("CreatedOn");
            result.ModifiedBy = sr.GetNullableString("ModifiedBy");
            result.ModifiedOn = sr.GetNullableDateTime("ModifiedOn");

            result.IsActive = sr.GetBoolean("IsActive");
            result.IsDeleted = sr.GetBoolean("IsDeleted");
            result.SchedulerLocationIdsString = sr.GetNullableString("SchedulerLocationIds");
            result.CodeReferenceIdsString = sr.GetNullableString("CodeReferenceIds");
            result.ProcedureCodesString = sr.GetNullableString("ProcedureCodes");
            result.StateCodesString = sr.GetNullableString("StateCodes");
            result.ZipCodesString = sr.GetNullableString("ZipCodes");
            result.LocalPayerIdsString = sr.GetNullableString("LocalPayerIds");

            return result;
        }

        public static OrderCreationParameter ToOrderCreationParameter(this SafeDataReader safeReader)
        {
            OrderCreationParameter res = new OrderCreationParameter();
            res.Id = safeReader.GetInt32("OrderParameterId");
            res.ParamName = safeReader.GetNullableString("ParameterName");
            res.ParamType = safeReader.GetNullableString("ParameterType");
            res.IsRequired = safeReader.GetNullableBoolean("isRequired") ?? false;
            res.IsSystemRequired = safeReader.GetBoolean("IsSystemRequired");
            res.DefaultValue = safeReader.GetNullableString("ParameterDefaultValue");
            res.PromptUserForDefault = safeReader.GetInt32("PromptUserForDefault") > 0;
            return res;
        }

        public static AccessControlEntry ToAccessControlEntry(this SafeDataReader sr)
        {
            AccessControlEntry res = new AccessControlEntry();
            res.Id = sr.GetInt32("Id");
            res.Create = (Permission)sr.GetInt16("CanCreate");
            res.Update = (Permission)sr.GetInt16("CanUpdate");
            res.Read = (Permission)sr.GetInt16("CanRead");
            res.Delete = (Permission)sr.GetInt16("CanDelete");
            res.Name = sr.GetString("SecuredEntityName");

            return res;
        }

        public static TechCompleteSuggestionList ToTechCompleteSuggestionList(this SafeDataReader sr)
        {
            TechCompleteSuggestionList r = new TechCompleteSuggestionList(sr.GetInt64("Id"), sr.GetString("DisplayName"),
                                                                          sr.GetBoolean("IsVisible"));

            return r;
        }

        public static PhysicianType ToPhysicianType(this SafeDataReader reader)
        {
            PhysicianType type = new PhysicianType();
            type.Id = reader.GetInt32("TypeId");
            type.Name = reader.GetNullableString("TypeName");
            type.Color = reader.GetNullableString("TypeColor");
            return type;
        }

        public static AuthorizationProcedure ToAuthorizationProcedure(this SafeDataReader sr)
        {
            AuthorizationProcedure r = new AuthorizationProcedure();
            r.Id = sr.GetInt32("Id");
            r.PayerId = sr.GetInt32("PayerId");
            r.Description = sr.GetString("Description");
            r.Code = sr.GetString("Code");
            r.ProcedureAmount = sr.GetString("ProcedureAmount");
            r.ProcedureUnit = sr.GetString("ProcedureUnit");
            return r;
        }

        public static AuthorizationAlert ToAuthorizationAlert(this SafeDataReader sr)
        {
            AuthorizationAlert r = new AuthorizationAlert();
            r.Id = sr.GetInt32("Id");
            r.PayerId = sr.GetInt32("PayerId");
            r.PayerName = sr.GetNullableString("PayerName");
            return r;
        }

        public static CodeCategory ToCodeCategory(this IDataReader reader)
        {
            CodeCategory res = new CodeCategory();
            using (SafeDataReader sr = new SafeDataReader(reader))
            {
                res.Id = sr.GetInt32("CodeCategoryId");
                res.Name = sr.GetString("CodeCategory");

                int? parentId = sr.GetNullableInt32("ParentCodeCategoryId");
                if (parentId.HasValue)
                    res.Parent = new CodeCategory(parentId.Value);

                res.IsActive = sr.GetBoolean("isActive");
            }
            return res;
        }

        public static VolumeUnit ToVolumeUnit(this SafeDataReader sr)
        {
            VolumeUnit r = new VolumeUnit(sr.GetInt32("Id"), sr.GetString("DisplayName"), sr.GetBoolean("IsVisible"));

            return r;
        }

        public static AccountGenerateIDconfig ToAccountGenerateIDconfig(this IDataReader reader)
        {
            using (SafeDataReader safeReader = new SafeDataReader(reader))
            {
                AccountGenerateIDconfig res = new AccountGenerateIDconfig();
                res.Id = safeReader.GetInt32("AccountGenerateIDconfigId");
                res.AccountId = safeReader.GetString("AccountId");
                res.IdTypeName = safeReader.GetString("IdTypeName");
                res.IDFormatString = safeReader.GetString("IDFormatString");

                res.Location = safeReader.GetNullableString("Location");
                res.CustomLocationCode = safeReader.GetNullableString("CustomLocationCode");
                res.PreFix = safeReader.GetNullableString("PreFix");
                res.PostFix = safeReader.GetNullableString("PostFix");
                res.SeqPaddingChar = safeReader.GetNullableString("SeqPaddingChar");
                res.SeqPaddingDir = safeReader.GetNullableString("SeqPaddingDir");

                res.StartingSeq = safeReader.GetNullableInt32("StartingSeq");
                res.NextAvailableSeq = safeReader.GetNullableInt32("NextAvailableSeq");
                res.SeqPaddingLen = safeReader.GetNullableInt32("SeqPaddingLen");
                res.GuidLen = safeReader.GetNullableInt32("GuidLen");

                res.IsSeqPadded = safeReader.GetNullableBoolean("IsSeqPadded");
                res.UseGuid = safeReader.GetBoolean("UseGuid");
                return res;
            }
        }

        public static Ethnicity ToEthnicity(this SafeDataReader sr)
        {
            Ethnicity r = new Ethnicity();
            r.Id = sr.GetInt32("EthnicityID");
            r.Description = sr.GetString("Description");

            return r;
        }

        public static NotificationSlot ToNotificationSlot(this SafeDataReader sr)
        {
            NotificationSlot slot = new NotificationSlot();

            slot.Id = sr.GetInt32("Id");
            slot.DayOfWeek = sr.GetNullableString("DayOfWeek");
            slot.StartDate = sr.GetNullableDateTime("StartDate");
            slot.StartTime = sr.GetDateTime("StartTime");
            slot.EndTime = sr.GetDateTime("EndTime");
            slot.EndDate = sr.GetNullableDateTime("EndDate");
            slot.ModalityId = sr.GetInt32("ModalityId");
            slot.Comment = sr.GetString("Comment");
            slot.Color = sr.GetNullableString("Color");
            slot.IsActive = sr.GetBoolean("IsActive");

            return slot;
        }

        //CP: Fix
        //public void ExtractConfigFromReader(IDataReader reader)
        //{
        //    using (SafeDataReader sr = new SafeDataReader(reader))
        //    {
        //        if (sr.Read())
        //        {
        //            this.WorkingDays = new List<DayOfWeek>();

        //            if (sr.GetBoolean("MonIsWorking"))
        //                this.WorkingDays.Add(DayOfWeek.Monday);
        //            if (sr.GetBoolean("TueIsWorking"))
        //                this.WorkingDays.Add(DayOfWeek.Tuesday);
        //            if (sr.GetBoolean("WenIsWorkgin"))
        //                this.WorkingDays.Add(DayOfWeek.Wednesday);
        //            if (sr.GetBoolean("ThrIsWorking"))
        //                this.WorkingDays.Add(DayOfWeek.Thursday);
        //            if (sr.GetBoolean("SatIsWorking"))
        //                this.WorkingDays.Add(DayOfWeek.Saturday);
        //            if (sr.GetBoolean("SunIsWorking"))
        //                this.WorkingDays.Add(DayOfWeek.Sunday);
        //            if (sr.GetBoolean("FriIsWorking"))
        //                this.WorkingDays.Add(DayOfWeek.Friday);

        //            this.StartWeekOn = sr.GetInt32("StartWeekOn");
        //            this.StartWorkingHour = sr.GetInt32("StartWorkingHour");
        //            this.StartWorkingMinute = sr.GetInt32("StartWorkingMinute");
        //            this.EndWorkingHour = sr.GetInt32("EndWorkingHour");
        //            this.EndWorkingMinute = sr.GetInt32("EndWorkingMinute");
        //            this.NumberOfVisibleHours = sr.GetInt32("DefaultViewSize");

        //            this.DefaultViewMode = sr.GetInt32("DefaultViewMode");
        //            this.ScheduleMode = sr.GetInt32("DefaultScheduleMode");
        //            this.IsReferralRequired = (sr.GetNullableBoolean("IsReferralRequired") ?? true);
        //            this.IsBillingNoteRequired = (sr.GetNullableBoolean("IsBillingNoteRequired") ?? true);
        //            this.IsCreateOrderRequired = (sr.GetNullableBoolean("IsCreateOrderRequired") ?? true);
        //            this.IsVisitReasonRequired = (sr.GetNullableBoolean("IsVisitReasonRequired") ?? true);
        //            this.ProcedureExpansionMode = (ProcedureExpansionMode)(sr.GetNullableInt32("ProcedureAutoExpansionMode") ?? 0);
        //            this.PayersSearchMode = (PayersSearchMode)(sr.GetNullableInt32("PayersSearchMode") ?? 0);
        //            this.PreselectProcedureTypes = sr.GetNullableBoolean("PreselectProcedureTypes") ?? false;
        //            this.IsPendingEnabled = sr.GetBoolean("IsPendingEnabled");
        //            this.PatientCategoryRequired = sr.GetBoolean("PatientCategoryRequired");
        //            this.IsProcedureGlobalSearchEnabled = sr.GetBoolean("IsProcedureGlobalSearchEnabled");
        //            this.IsWarningMessagesEnabled = sr.GetBoolean("IsWarningMessagesEnabled");
        //            this.IsCommentForBlockingRequired = sr.GetBoolean("IsCommentForBlockingRequired");
        //            this.IsPatientDOBMandatory = sr.GetBoolean("PatientDOBMandatory");
        //            this.SendEmailFromAddress = sr.GetNullableString("EmailAddress");
        //            this.IsPaymentsEnabled = sr.GetBoolean("IsPaymentsEnabled");
        //            this.IsProcessPaymentsEnabled = sr.GetBoolean("IsProcessPaymentsEnabled");
        //            this.IsScheduleAppointmentByEstimationSlots = sr.GetBoolean("IsScheduleAppointmentByEstimationSlots");
        //            this.IsStateOfServiceEnabled = sr.GetBoolean("IsStateOfServiceEnabled");
        //            this.IsProcedureRequired = sr.GetBoolean("IsProcedureRequired");
        //            this.MRNReadOnly = sr.GetBoolean("MRNReadOnly");
        //            this.IsMammographyActive = sr.GetBoolean("IsMammographyActive");
        //        }
        //    }
        //}

        public static Account ToAccount(this IDataReader reader)
        {
            using (SafeDataReader safeReader = new SafeDataReader(reader))
            {
                Account res = new Account();
                res.Id = Convert.ToInt64(safeReader.GetDecimal("AccountID"));
                res.Name = safeReader.GetString("AccountName");
                res.HourDivisionSegment = 15;
                res.State = safeReader.GetNullableString("AccountState");
                res.LogoUrl = safeReader.GetNullableString("AccountLogo");
                res.Address = safeReader.GetNullableString("AccountAddress");
                res.Address2 = safeReader.GetNullableString("AccountAddress2");
                res.City = safeReader.GetNullableString("AccountCity");
                res.ZipCode = safeReader.GetNullableString("AccountZipcode");
                res.Phone = safeReader.GetNullableString("AccountPhone");

                res.OrderCreationMode = OrderCreationMode.NotSpecified;

                int? modeDbValue = safeReader.GetNullableInt32("SchedulerOrderCreationModeId");
                if (modeDbValue.HasValue)
                {
                    if (Enum.IsDefined(typeof(OrderCreationMode), modeDbValue.Value))
                    {
                        res.OrderCreationMode = (OrderCreationMode)modeDbValue.Value;
                    }
                }

                res.AccessValidLocationsOnly = safeReader.GetBoolean("AccessValidLocationsOnly");
                res.ViewPatientsInValidLocationsOnly = safeReader.GetNullableBoolean("ViewPatientsInValidLocationsOnly");
                //                res.OrderCreationTrigger = safeReader.GetNullableString("OrderCreationStatusTrigger");
                //                res.VisitCreationTrigger = safeReader.GetNullableString("VisitCreationTrigger");
                res.WorkTypeSourceTable = safeReader.GetNullableString("MapToWorkTypeSourceTable");
                res.WorkTypeSourceColumn = safeReader.GetNullableString("MapToWorkTypeField");
                res.MapVisitTypeFrom = safeReader.GetNullableString("MapVisitTypeFrom");

                res.IsLocationFilterVis = (safeReader.GetNullableBoolean("IsLocationFilterVis") ?? true);
                res.IsModalityFilterVis = (safeReader.GetNullableBoolean("IsModalityFilterVis") ?? true);
                res.IsRoomFilterVis = (safeReader.GetNullableBoolean("IsRoomFilterVis") ?? true);
                res.IsRoleFilterVis = (safeReader.GetNullableBoolean("IsRoleFilterVis") ?? true);

                if (safeReader.ContainsColumn("IsPendingEnabled"))
                    res.IsPendingEnabled = safeReader.GetBoolean("IsPendingEnabled");

                res.IsProviderFilterVis = (safeReader.GetNullableBoolean("IsProviderFilterVis") ?? true);
                res.IsApptStatusFilterVis = (safeReader.GetNullableBoolean("IsApptStatusFilterVis") ?? true);
                res.IsDaysFilterVis = (safeReader.GetNullableBoolean("IsDaysFilterVis") ?? true);
                res.IsPhyGroupVis = (safeReader.GetNullableBoolean("IsPhyGroupVis") ?? true);
                res.IsWtGroupVis = (safeReader.GetNullableBoolean("IsWtGroupVis") ?? true);
                if (safeReader.ContainsColumn("IsWarningMessagesEnabled"))
                    res.IsWarningMessagesEnabled = safeReader.GetBoolean("IsWarningMessagesEnabled");
                if (safeReader.ContainsColumn("IsCommentForBlockingRequired"))
                    res.IsCommentForBlockingRequired = safeReader.GetBoolean("IsCommentForBlockingRequired");
                if (safeReader.ContainsColumn("PatientDOBMandatory"))
                    res.IsPatientDOBMandatory = safeReader.GetBoolean("PatientDOBMandatory");







                /*     res.ColorsConfiguration.Add((long)Common.Enums.AppointmentStatuses.New, "#FF417FB3");
                     res.ColorsConfiguration.Add((long)Common.Enums.AppointmentStatuses.Complete, "#FF2DAA2D");//C1C3C5
                     res.ColorsConfiguration.Add((long)Common.Enums.AppointmentStatuses.NoShow, "#FFBFB2DF");
                     res.ColorsConfiguration.Add((long)Common.Enums.AppointmentStatuses.Cancel, "#FFE06969");
                     res.ColorsConfiguration.Add((long)Common.Enums.AppointmentStatuses.Arrived, "#FF71AC71");
                     res.ColorsConfiguration.Add((long)Common.Enums.AppointmentStatuses.Rescheduled, "#FFBDD3E6");//FF84B8E5
                     res.ColorsConfiguration.Add((long)Common.Enums.AppointmentStatuses.TechComplete, "#FF6F7173");
                     res.ColorsConfiguration.Add((long)Common.Enums.AppointmentStatuses.Available, "#FF171717");
                     res.ColorsConfiguration.Add((long)Common.Enums.AppointmentStatuses.Blocked, "#FFFF0000");*/
                if (safeReader.ContainsColumn("SuperUser"))
                {
                    bool? access = safeReader.GetNullableBoolean("SuperUser");
                    if (access.HasValue && access.Value)
                        res.AllowFullAccess();
                }
                if (safeReader.ContainsColumn("SchedulerUserLvl") && !res.HasAccessToScheduler)
                {
                    int? access = safeReader.GetNullableInt16("SchedulerUserLvl");
                    res.HasAccessToScheduler = access.HasValue;
                    res.IsSchedulerAdmin = (access.HasValue && access.Value == 2);
                }
                if (safeReader.ContainsColumn("Dictator"))
                {
                    bool? access = safeReader.GetNullableBoolean("Dictator");
                    if (access.HasValue && access.Value)
                        res.IsDictator = true;
                }
                if (safeReader.ContainsColumn("AccountAdmin"))
                {
                    bool? admin = safeReader.GetNullableBoolean("AccountAdmin");
                    res.IsAdmin = admin.HasValue && admin.Value;
                }

                res.IsWorkWithPatientVisitAllowed = safeReader.GetBoolean("IsWorkWithPatientVisitAllowed");

                return res;
            }
        }

        public static AppointmentResourcePhysician ToAppointmentResourcePhysician(this IDataReader reader, long accountID)
        {
            using (SafeDataReader r = new SafeDataReader(reader))
            {
                AppointmentResourcePhysician res = new AppointmentResourcePhysician();
                res.Id = r.GetInt32("ID");
                res.IsAssigned2Scheduler = r.GetBoolean("IsVisibleInScheduler");
                res.FirstName = r.GetNullableString("FirstName");
                res.MiddleName = r.GetNullableString("Name");
                res.LastName = r.GetNullableString("LastName");
                res.Account = new Account(r.GetString("Account"));
                res.Tag = r.GetNullableString("Tag");
                res.TypeId = r.GetNullableInt32("TypeId");
                res.Email = r.GetNullableString("Email");
                res.SendTo = r.GetNullableString("SendTo");
                res.EmailCopy = r.GetBoolean("EmailCopy");
                res.NPINo = r.GetNullableString("NPI");
                res.ResourceType = new AppointmentResourceType((long)ResourceTypes.Physician);
                res.Account = new Account(accountID);
                res.LocationId = r.GetNullableString("Location");
                res.Color = r.GetNullableString("Color");
                res.UserId = r.GetNullableString("UserId");
                if (r.ContainsColumn("DictatorId"))
                    res.AbbadoxDictatorId = r.GetNullableString("DictatorId");
                res.WorkingSchedule = new WorkingSchedule();
                return res;
            }
        }

        public static Appointment ToAppointment(this SafeDataReader safeReader, long accountID)
        {
            Appointment app = new Appointment();
            app.IsLocked = safeReader.GetBoolean("IsLocked");
            app.IsLockedBy = safeReader.GetNullableString("LockUser");
            app.Account = new Account(accountID);
            app.Id = safeReader.GetInt64("AppointmentID");
            app.GroupId = safeReader.GetNullableInt32("AppGroupId");
            app.Status = new AppointmentStatus(safeReader.GetInt64("Status"));
            app.Name = safeReader.GetNullableString("Name");
            app.Description = safeReader.GetNullableString("Description");
            app.LocationId = safeReader.GetNullableInt64("LocationId");
            app.DefaultOrderPriority = safeReader.GetNullableString("DefaultOrderPriority") ?? string.Empty;
            app.Worktype = safeReader.GetNullableString("Worktype");
            app.Comment = safeReader.GetNullableString("Comment");
            if (safeReader.ContainsColumn("ReasonText"))
                app.PendingReasonText = safeReader.GetNullableString("ReasonText");
            if (safeReader.ContainsColumn("ReasonCode"))
                app.PendingReasonCode = safeReader.GetNullableString("ReasonCode");
            app.Sources.Add(safeReader.ToAppointmentResourceTime(accountID));

            if (safeReader.ContainsColumn("CreateBy"))
                app.CreateBy = safeReader.GetNullableString("CreateBy");
            if (safeReader.ContainsColumn("CreatedOn"))
                app.CreatedOn = safeReader.GetNullableDateTime("CreatedOn");
            if (safeReader.ContainsColumn("LastModifiedBy"))
                app.LastModifiedBy = safeReader.GetNullableString("LastModifiedBy");
            if (safeReader.ContainsColumn("LastModifiedOn"))
                app.LastModifiedOn = safeReader.GetNullableDateTime("LastModifiedOn");

            if (safeReader.ContainsColumn("alert"))
            {
                long? typeID = safeReader.GetNullableInt64("ResourceType");
                if (typeID.HasValue && typeID == 2)
                {
                    int alert = safeReader.GetInt32("alert");
                    app.IsAuthorizationAlert = alert > 0;
                }
            }

            return app;
        }

        public static OrderTransformParameter ToOrderTransformParameter(this SafeDataReader reader)
        {
            OrderTransformParameter p = new OrderTransformParameter();
            p.Id = reader.GetInt32("TransformId");
            p.SchedulerConfigID = reader.GetInt32("SchedulerConfigurationId");
            p.MapFieldValue = reader.GetNullableString("MapFieldValue");
            p.AccountWtValue = reader.GetNullableString("AccountWtValue");
            p.MapFieldGroup = reader.GetNullableString("MapFieldGroup");
            p.IsGroupPrompt = reader.GetBoolean("IsGroupPrompt");
            p.OverrideCreationMode = reader.GetNullableInt32("OverrideCreationMode");
            return p;
        }

        public static SnomedProcedure ToSnomedProcedure(this SafeDataReader sr)
        {
            SnomedProcedure r = new SnomedProcedure();
            r.Id = sr.GetInt32("Id");
            r.SnomedCode = sr.GetString("SnomedCode");
            r.ShortDescription = sr.GetNullableString("ShortDescription");
            r.MediumDescription = sr.GetNullableString("MediumDescription");
            r.LongDescription = sr.GetNullableString("LongDescription");
            r.IsEncounterCode = sr.GetBoolean("IsEncounterCode");
            return r;
        }

        public static PhysicianSpeciality ToPhysicianSpeciality(this SafeDataReader sr)
        {
            PhysicianSpeciality r = new PhysicianSpeciality();
            r.Id = sr.GetInt32("Id");
            r.Name = sr.GetString("SpecialityName");
            r.IsVisible = sr.GetBoolean("IsVisible");
            return r;
        }

        public static CustomPayer ToCustomPayer(this SafeDataReader sr)
        {
            CustomPayer p = new CustomPayer();
            p.Id = sr.GetInt32("PayerID");
            p.Name = sr.GetNullableString("Name");
            p.WebSite = sr.GetNullableString("WebSite");
            p.Address = sr.GetNullableString("Address1");
            p.Address2 = sr.GetNullableString("Address2");
            p.City = sr.GetNullableString("City");
            p.State = sr.GetNullableString("State");
            p.Zip = sr.GetNullableString("ZipCode");
            p.Phone = sr.GetNullableString("Phone");
            p.Fax = sr.GetNullableString("Fax");
            p.IsGlobal = sr.GetBoolean("IsGlobal");
            p.IsEligible = sr.GetNullableBoolean("IsEligible");
            p.VendorPayerId = sr.GetNullableString("VendorPayerID");
            p.PayerAddressId = sr.GetNullableInt32("PayerAddressID") ?? -1;

            if (sr.ContainsColumn("ExternalID"))
                p.ExternalId = sr.GetNullableString("ExternalID");

            if (sr.ContainsColumn("ClientCode"))
                p.ClientCode = sr.GetNullableString("ClientCode");

            return p;
        }
        public static NotificationSlotComment ToNotificationSlotComment(this SafeDataReader sr)
        {
            NotificationSlotComment nsComment = new NotificationSlotComment();
            nsComment.Id = sr.GetInt32("Id");
            nsComment.Comment = sr.GetString("Comment");
            nsComment.Color = sr.GetString("Color");
            nsComment.IsActive = sr.GetBoolean("IsActive");
            return nsComment;
        }

      


    }
}
