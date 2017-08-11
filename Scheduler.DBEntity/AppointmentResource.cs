using Scheduler.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.DBEntity
{
    public class ResourceID
    {
        private long _resourceID;
        private ResourceTypes _resourceType;
        private long _accountID;

        public ResourceID(long id, ResourceTypes type)
        {
            _resourceID = id;
            _resourceType = type;
            _accountID = GlobalContext.RequestContext.AccountId;
        }

        public bool Equals(ResourceID other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other._resourceID == _resourceID && other._resourceType == _resourceType && other._accountID == _accountID;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(ResourceID)) return false;
            return Equals((ResourceID)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = (int)_resourceID;
                result = (result * 397) ^ (int)_resourceType;
                result = (result * 397) ^ (int)_accountID;
                return result;
            }
        }



        public long AccountId
        {
            get { return _accountID; }
        }

        public long Id
        {
            get { return _resourceID; }
        }

        public ResourceTypes Type
        {
            get { return _resourceType; }
        }

        public ResourceID Set(ResourceTypes type, long id)
        {
            _resourceType = type;
            _resourceID = id;
            return this;
        }
    }

    public class AppointmentResource : EntityBase
    {
        public AppointmentResourceType ResourceType { get; set; }
        public Account Account { get; set; }

        public AppointmentResource()
        {
        }

        public AppointmentResource(long id)
            : this()
        {
            this.Id = id;
        }
        public virtual string DisplayText
        {
            get { return ToString(); }
        }
        public virtual AppointmentResource Clear()
        {
            throw new NotImplementedException();
        }

        //public virtual AppointmentResource Create(RepositoryLocator locator)
        //{
        //    throw new NotImplementedException();
        //}

        //public virtual AppointmentResource Update(RepositoryLocator locator, AppointmentResource updatedResource)
        //{
        //    throw new NotImplementedException();
        //}

        //public virtual void Delete(RepositoryLocator locator)
        //{
        //    throw new NotImplementedException();
        //}

        public virtual bool IsValid(AppointmentResource resource)
        {
            return this.Id == resource.Id;
        }

        //public static AppointmentResource ExtractFromDto(long desiredID, AppointmentResourceDto dto)
        //{
        //    AppointmentResource res = ExtractFromDto(dto);

        //    if (res.Id <= 0)
        //        res.Id = desiredID;

        //    return res;
        //}



        //public static AppointmentResource ExtractFromDto(AppointmentResourceDto dto)
        //{
        //    AppointmentResource result = null;
        //    switch (dto.TypeId)
        //    {
        //        case (long)ResourceTypes.Time:
        //            {
        //                result = AppointmentResourceTime.ExtractFromDto(dto as AppointmentResourceTimeDto);
        //                break;
        //            }
        //        case (long)ResourceTypes.Patient:
        //            {
        //                result = AppointmentResourcePatient.ExtractFromDto(dto as AppointmentResourcePatientDto);
        //                break;
        //            }
        //        case (long)ResourceTypes.Room:
        //            {
        //                result = AppointmentResourceModality.ExtractFromDto(dto as AppointmentResourceModalityDto);
        //                break;
        //            }
        //        case (long)ResourceTypes.Physician:
        //            {
        //                result = AppointmentResourcePhysician.ExtractFromDto(dto as AppointmentResourcePhysicianDto);
        //                break;
        //            }
        //        default:
        //            throw new Exception("Appointment resource can not be converted. Unknown type passed");
        //    }
        //    return result;
        //}

        

        //public static AppointmentResourceDto Convert2Dto(AppointmentResource resource)
        //{
        //    AppointmentResourceDto result = null;

        //    switch (resource.ResourceType.Id)
        //    {
        //        case (long)ResourceTypes.Patient:
        //            {
        //                AppointmentResourcePatient source = resource as AppointmentResourcePatient;
        //                AppointmentResourcePatientDto patient = new AppointmentResourcePatientDto();
        //                patient.Id = source.Id;
        //                patient.AccountId = source.Account == null ? -1 : source.Account.Id;
        //                patient.Address1 = source.Address1;
        //                patient.IsFullyCached = source.IsFullyCached;
        //                patient.Address2 = source.Address2;
        //                //patient.BPFrom    = source.BPFrom;
        //                //patient.BPTo      = source.BPTo;
        //                patient.Cause = source.Cause;
        //                patient.City = source.City;
        //                if (source.LocationId != null) patient.LocationID = source.LocationId.Value;

        //                if (source.Comments != null)
        //                {
        //                    foreach (PatientComment comment in source.Comments)
        //                    {
        //                        patient.Comments.Add(PatientComment.Convert2Dto(comment));
        //                    }
        //                }

        //                if (source.PatientPayments != null)
        //                {
        //                    foreach (PatientPayment pp in source.PatientPayments)
        //                    {
        //                        patient.PatientPayments.Add(PatientPayment.Convert2Dto(pp));
        //                    }
        //                }

        //                //patient.Confirmation = (AppointmentResourcePatientDto.ConfiramtionMode)source.Confirmation;

        //                patient.ConfirmByCall = source.ContactByCall;
        //                patient.ConfirmByEmail = source.ContactByEmail;
        //                patient.ConfirmByMail = source.ConfirmByMail;
        //                patient.ConfirmBySms = source.ConfirmBySms;
        //                patient.ConfirmByMobile = source.ConfirmByMobile;

        //                patient.NotifyByCall = source.NotifyByCall;
        //                patient.NotifyByEmail = source.NotifyByEmail;
        //                patient.NotifyByMail = source.NotifyByMail;
        //                patient.NotifyBySms = source.NotifyBySms;
        //                patient.NotifyByMobile = source.NotifyByMobile;

        //                patient.IsOptOutManualCalls = source.IsOptOutManualCalls;
        //                patient.IsOptOutRoboCalls = source.IsOptOutRoboCalls;
        //                patient.IsOptOutLetters = source.IsOptOutLetters;

        //                patient.DateOfDeseace = source.DateOfDeseace;
        //                patient.Dob = source.Dob;
        //                patient.Email = source.Email;
        //                patient.Ethnicicty = source.Ethnicicty;
        //                patient.ExternalID = source.ExternalID;
        //                patient.Fax = source.Fax;
        //                patient.FirstName = source.FirstName;
        //                patient.Gender = source.Gender;
        //                //patient.Height = source.Height;
        //                patient.Id = source.Id;
        //                patient.InsuranceType = (Scheduler.Common.DataTransferObjects.Appointment.Sources.AppointmentResourcePatientDto.InsurenceType)source.InsuranceType;
        //                patient.LastName = source.LastName;
        //                patient.MaidenName = source.MaidenName;
        //                //patient.MBI = source.MBI;
        //                patient.MiddleName = source.MiddleName;
        //                patient.Mobile = source.Mobile;
        //                patient.Emergency = source.Emergency;
        //                patient.Status = source.Status;
        //                patient.IsActive = source.IsActive;
        //                patient.IsDeceased = source.IsDeceased;
        //                patient.CauseOfDeath = source.CauseOfDeath;
        //                patient.DeceaseDate = source.DeceaseDate;
        //                patient.IsVIP = source.IsVIP;
        //                patient.WorkPhone = source.WorkPhone;
        //                patient.LastModificationDateTime = source.LastModificationDateTime;



        //                if (source.Payers != null)
        //                {
        //                    foreach (Payer p in source.Payers)
        //                        patient.Payers.Add(Payer.Convert2Dto(p));
        //                }

        //                if (source.PatientGuarantors != null)
        //                {
        //                    foreach (PatientGuarantor guarantor in source.PatientGuarantors)
        //                    {
        //                        patient.PatientGuarantors.Add(PatientGuarantor.Convert2Dto(guarantor));
        //                    }
        //                }

        //                patient.Phone = source.Phone;
        //                patient.IsSelfPay = source.IsSelfPay;
        //                patient.LocationName = source.LocationName;
        //                patient.AbbadoxLocation = source.AbbadoxLocation;
        //                //                        patient.Race = source.Race;
        //                foreach (Race race in source.Races)
        //                {
        //                    patient.Races.Add(Race.Convert2Dto(race));
        //                }
        //                patient.RecordNumber = source.RecordNumber;
        //                patient.RequiresTranslation = source.RequiresTranslation;
        //                //patient.Smoking = source.Smoking;
        //                patient.SSN = source.SSN;
        //                patient.State = source.State;

        //                patient.TranslationLanguage = source.TranslationLanguage;
        //                patient.PatientStatus = source.PatientStatus;
        //                patient.TypeId = source.ResourceType.Id;

        //                patient.EnumHeardOfUsName = source.EnumHeardOfUsName;
        //                patient.MaritalStatus = source.MaritalStatus;
        //                patient.AdvanceDirectives = source.AdvanceDirectives;
        //                patient.ConsentForm = source.ConsentForm;
        //                patient.LastAppointmentDate = source.LastAppointmentDate;
        //                patient.VerificationStatus = source.VerificationStatus;


        //                //patient.Weight = source.Weight;
        //                patient.ZipCode = source.ZipCode;

        //                if (source.IdCards != null)
        //                {
        //                    foreach (SchedulerImage image in source.IdCards)
        //                    {
        //                        patient.IdCards.Add(SchedulerImage.Convert2Dto(image));
        //                    }
        //                }
        //                if (source.AdditionalAddresses != null)
        //                {
        //                    foreach (Address address in source.AdditionalAddresses)
        //                    {
        //                        AddressDto addressDto = Address.ConvertToDto(address);
        //                        patient.AdditionalAddresses.Add(addressDto);
        //                    }
        //                }

        //                foreach (KeyValuePair<int, string> item in source.PreviousTransactions)
        //                    patient.PreviousTransactions.Add(item.Key, item.Value);

        //                if (source.PatientEmployments != null)
        //                {
        //                    foreach (PatientEmployment empl in source.PatientEmployments)
        //                    {
        //                        PatientEmploymentDto empDto = PatientEmployment.Convert2Dto(empl);
        //                        patient.PatientEmployment.Add(empDto);
        //                    }
        //                }

        //                if (source.UsedAuthorizations != null)
        //                {
        //                    foreach (var address in source.UsedAuthorizations)
        //                    {
        //                        UsedAuthorizationDto addressDto = UsedAuthorization.Convert2Dto(address);
        //                        patient.UsedAuthorizations.Add(addressDto);
        //                    }
        //                }

        //                if (source.PatientAuthorizations != null)
        //                {
        //                    foreach (PatientAuthorization authorization in source.PatientAuthorizations)
        //                    {
        //                        PatientAuthorizationDto authorizationDto = PatientAuthorization.Convert2Dto(authorization);
        //                        patient.PatientAuthorizations.Add(authorizationDto);
        //                    }
        //                }


        //                if (source.MultipleIdentifiers != null)
        //                {
        //                    foreach (PatientIdentifier patientIdentifier in source.MultipleIdentifiers)
        //                    {
        //                        PatientIdentifierDto patientIdentifierDto = PatientIdentifier.Convert2Dto(patientIdentifier);
        //                        patient.MultipleIdentifiers.Add(patientIdentifierDto);
        //                    }
        //                }

        //                if (source.SpecialNeeds != null)
        //                {
        //                    patient.SpecialNeeds.AddRange(source.SpecialNeeds);
        //                }

        //                if (source.LastApps != null)
        //                {
        //                    patient.LastApps = source.LastApps.Where(s => s.Value != null)
        //                        .ToDictionary(s => s.Key, s => Appointment.Appointment.Convert2Dto(s.Value));
        //                }

        //                result = patient;
        //                break;
        //            }
        //        case (long)ResourceTypes.Physician:
        //            {
        //                AppointmentResourcePhysician source = resource as AppointmentResourcePhysician;
        //                AppointmentResourcePhysicianDto physician = new AppointmentResourcePhysicianDto();
        //                physician.LocationId = string.IsNullOrEmpty(source.LocationId) ? (int?)null : int.Parse(source.LocationId);
        //                physician.AccountId = source.Account == null ? -1 : source.Account.Id;
        //                physician.Fax = source.Fax;
        //                physician.AbbadoxDictatorId = source.AbbadoxDictatorId;
        //                physician.UserId = source.UserId;
        //                physician.FirstName = source.FirstName;
        //                physician.Id = source.Id;
        //                physician.LastName = source.LastName;
        //                physician.MiddleName = source.MiddleName;
        //                physician.Tag = source.Tag;
        //                physician.PhysTypeId = source.TypeId;
        //                physician.NPINo = source.NPINo;
        //                physician.Phone = source.Phone;
        //                physician.SpecializationId = source.Specialization == null ? -1 : source.Specialization.Id;
        //                physician.TypeId = source.ResourceType.Id;
        //                physician.Email = source.Email;
        //                physician.SendTo = source.SendTo;
        //                physician.EmailCopy = source.EmailCopy;
        //                physician.IsAssigned2Scheduler = source.IsAssigned2Scheduler;
        //                physician.Color = source.Color;
        //                physician.WorkingSchedule = WorkingSchedule.Convert2Dto(source.WorkingSchedule);
        //                result = physician;
        //                break;
        //            }
        //        case (long)ResourceTypes.Room:
        //            {
        //                AppointmentResourceModality source = resource as AppointmentResourceModality;
        //                AppointmentResourceModalityDto room = new AppointmentResourceModalityDto();
        //                room.AccessionNumber = source.AccessionNumber;
        //                room.VirtualRoomId = source.VirtualRoomId; ;
        //                //By RJ: Hooking up Virtual room entities with resource DTO.
        //                room.SchedulerModalityVirtualRoom = source.SchedulerModalityVirtualRoom.ToSchedulerModalityVirtualRoomDto();
        //                room.IsMammographyResource = source.IsMammographyResource;
        //                room.IsOnlineRoom = source.IsOnlineRoom;
        //                room.CreateEncounter = source.CreateEncounter;
        //                room.CreateOrder = source.CreateOrder;
        //                room.IsActive = source.IsActive;
        //                room.AccountId = source.Account == null ? -1 : source.Account.Id;
        //                room.Estimate = source.Estimate;
        //                room.Id = source.Id;
        //                //                        room.ModalityTypeId = source.Id;
        //                room.LocationID = source.Location == null ? -1 : source.Location.Id;
        //                room.ModalityTypeId = source.ModalityType == null ? -1 : source.ModalityType.Id;
        //                room.RoomName = source.RoomName;
        //                room.RoomTypeId = source.RoomType == null ? -1 : source.RoomType.Id;
        //                room.TypeId = source.ResourceType.Id;
        //                room.WorkingSchedule = WorkingSchedule.Convert2Dto(source.WorkingSchedule);
        //                result = room;
        //                break;
        //            }
        //        case (long)ResourceTypes.Time:
        //            {
        //                AppointmentResourceTime source = resource as AppointmentResourceTime;
        //                AppointmentResourceTimeDto time = new AppointmentResourceTimeDto();
        //                time.AccountId = source.Account == null ? -1 : source.Account.Id;
        //                time.EndTime = source.EndTime;
        //                time.Id = source.Id;
        //                time.StartTime = source.StartTime;
        //                time.TypeId = source.ResourceType.Id;
        //                result = time;
        //                break;
        //            }
        //    }
        //    return result;
        //}


        public static AppointmentResource GetTimeResouce(DateTime? startDate, DateTime? endDate, long accountId, long id)
        {
            AppointmentResourceTime time = new AppointmentResourceTime();
            time.ResourceType = new AppointmentResourceType((long)ResourceTypes.Time);
            time.Account = new Account(accountId);
            time.SetDateTime(startDate ?? DateTime.MinValue, endDate ?? DateTime.MinValue);
            time.Id = id;
            return time;
        }
    }
}
