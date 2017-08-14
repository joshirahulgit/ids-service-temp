using Scheduler.Business.Entity;
using Scheduler.DBEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Business.Implementation
{
    internal static class DBEntityExt
    {
        internal static AccountEnumDto ToDto(this AccountEnum accountEnum)
        {
            AccountEnumDto dto = new AccountEnumDto();
            dto.Id = accountEnum.Id;
            dto.Name = accountEnum.Name;
            dto.Value = accountEnum.Value;
            dto.IsVisible = accountEnum.IsVisible;
            dto.EnumType = accountEnum.EnumType;
            dto.IsDefault = accountEnum.IsDefault;
            dto.UserCanEdit = accountEnum.UserCanEdit;
            dto.UserCanDelete = accountEnum.UserCanDelete;
            return dto;
        }

        internal static AccountEnum ToDbEntity(this AccountEnumDto acEnumDto)
        {
            AccountEnum res = new AccountEnum(acEnumDto.Id);
            res.Name = acEnumDto.Name;
            res.Value = acEnumDto.Value;
            res.IsVisible = acEnumDto.IsVisible;
            res.EnumType = acEnumDto.EnumType;
            res.IsDefault = acEnumDto.IsDefault;
            res.UserCanEdit = acEnumDto.UserCanEdit;
            res.UserCanDelete = acEnumDto.UserCanDelete;
            return res;
        }

        internal static AccountSettingCollectionDto ToDto(this AccountSettingCollection Items)
        {
            AccountSettingCollectionDto result = new AccountSettingCollectionDto();
            result.Items = new List<AccountSettingDto>();
            foreach (AccountSetting accountSetting in Items.Items)
            {
                result.Items.Add(accountSetting.ToDto());
            }
            return result;
        }

        public static AccountSettingDto ToDto(this AccountSetting u)
        {
            if (u == null)
                return null;

            AccountSettingDto result = new AccountSettingDto();
            result.Id = u.Id;
            result.Name = u.Name;
            result.Value = u.Value;
            result.Application = u.Application;
            result.CreateDate = u.CreateDate;
            result.CreateUser = u.CreateUser;
            result.UpdateDate = u.UpdateDate;
            result.UpdateUser = u.UpdateUser;
            result.IsActive = u.IsActive;
            return result;
        }

        public static AccountSettingCollection ToDbEntity(this AccountSettingCollectionDto dto)
        {
            var result = new AccountSettingCollection();
            foreach (AccountSettingDto settingDto in dto.Items)
            {
                result.Items.Add(settingDto.ToDbEntity());
            }
            return result;
        }


        public static AccessControlEntryDto ToDto(this AccessControlEntry entry)
        {
            AccessControlEntryDto result = new AccessControlEntryDto();
            result.Id = (SchedulerSecuredEntities)entry.Id;
            result.Name = entry.Name;
            result.Create = entry.Create;
            result.Update = entry.Update;
            result.Read = entry.Read;
            result.Delete = entry.Delete;
            result.IsInherit = entry.IsInherit;
            return result;
        }

        public static AccessControlEntry ToDbEntity(this AccessControlEntryDto entry)
        {
            AccessControlEntry result = new AccessControlEntry();
            result.Id = (long)entry.Id;
            result.Name = entry.Name;
            result.Create = entry.Create;
            result.Update = entry.Update;
            result.Read = entry.Read;
            result.Delete = entry.Delete;
            result.IsInherit = entry.IsInherit;
            return result;
        }

        public static AccessControlListDto ToDto(this AccessControlList acl)
        {
            AccessControlListDto result = new AccessControlListDto();
            foreach (AccessControlEntry entry in acl.Entries)
            {
                result.Entries.Add(entry.ToDto());
            }
            return result;
        }

        public static AccessControlList ToDbEntity(this AccessControlListDto acl)
        {
            AccessControlList result = new AccessControlList();
            foreach (AccessControlEntryDto entry in acl.Entries)
            {
                result.Entries.Add(entry.ToDbEntity());
            }
            return result;
        }

        public static Account ToDbEntity(this AccountDto dto)
        {
            throw new NotImplementedException();
            //     if (dto == null)
            //         return null;

            //     Account res = new Account();

            //     foreach (OrderCreationParameterDto op in dto.OrderCreationParameters)
            //         res.OrderCreationParameters.Add(OrderCreationParameter.ExtractFromDto(op));

            //     foreach (AppointmentResourceTypeDto st in dto.ResourceTypes)
            //         res.ResourceTypes.Add(AppointmentResourceType.ExtractFromDto(st));

            //     foreach (AppointmentStatusDto s in dto.AppointmentStatuses)
            //         res.AppointmentStatuses.Add(AppointmentStatus.ExtractFromDto(s));

            //     /*       foreach (DiagnosisDto d in dto.AvailableDiangnosises)
            //                res.AvailableDiangnosises.Add(Diagnosis.ExtractFromDto(d));

            //            foreach (ProcedureTypeDto p in dto.AvailableProcedures)
            //                res.AvailableProcedures.Add(ProcedureType.ExtractFromDto(p));
            //*/
            //     foreach (ModalityTypeDto m in dto.ModalityTypes)
            //         res.ModalityTypes.Add(ModalityType.ExtractFromDto(m));

            //     foreach (ResourceAreaDto a in dto.ResouceAreas)
            //         res.ResouceAreas.Add(Area.ExtractFromDto(a));

            //     foreach (ResourceLocationDto l in dto.ResourceLocations)
            //         res.ResourceLocations.Add(ResourceLocation.ExtractFromDto(l));

            //     foreach (CommentTypeDto ct in dto.CommentTypes)
            //         res.CommentTypes.Add(CommentType.ExtractFromDto(ct));

            //     foreach (long key in dto.ColorsConfiguration.Keys)
            //         res.ColorsConfiguration.Add(key, dto.ColorsConfiguration[key]);

            //     foreach (string key in dto.AvailablePayers.Keys)
            //         res.AvailablePayers.Add(key, dto.AvailablePayers[key]);

            //     foreach (string key in dto.PayerStates.Keys)
            //         res.PayerStates.Add(key, dto.PayerStates[key]);

            //     foreach (string key in dto.AvailableProviders.Keys)
            //         res.AvailableProviders.Add(key, dto.AvailableProviders[key]);

            //     foreach (string key in dto.AvailableInsRelationships.Keys)
            //         res.AvailableInsRelationships.Add(key, dto.AvailableInsRelationships[key]);

            //     foreach (string key in dto.AvailableLanguages.Keys)
            //         res.AvailableLanguages.Add(key, dto.AvailableLanguages[key]);

            //     foreach (string key in dto.WorkTypes.Keys)
            //         res.WorkTypes.Add(key, dto.WorkTypes[key]);

            //     foreach (AccountEnumDto patientCategory in dto.VisitCategories.AccountEnums)
            //         res.VisitCategories.Add(AccountEnum.ExtractFromDto(patientCategory));

            //     foreach (AccountEnumDto hcpcScode in dto.HCPCSCodes.AccountEnums)
            //         res.HCPCScodes.Add(AccountEnum.ExtractFromDto(hcpcScode));

            //     foreach (AccountEnumDto enumScheduledBy in dto.EnumsScheduledBy.AccountEnums)
            //         res.EnumsScheduledBy.Add(AccountEnum.ExtractFromDto(enumScheduledBy));

            //     foreach (AccountEnumDto enumAilment in dto.EnumsPatientAilment.AccountEnums)
            //         res.EnumPatientAilment.Add(AccountEnum.ExtractFromDto(enumAilment));

            //     foreach (AccountEnumDto enumHeardOfUs in dto.EnumsHeardOfUs.AccountEnums)
            //         res.EnumsHeardOfUs.Add(AccountEnum.ExtractFromDto(enumHeardOfUs));

            //     foreach (AccountEnumDto maritalStatus in dto.EnumsMaritalStatus.AccountEnums)
            //         res.EnumMaritalStatus.Add(AccountEnum.ExtractFromDto(maritalStatus));

            //     foreach (AccountEnumDto contactRelation in dto.EnumContactRelation.AccountEnums)
            //         res.EnumContactRelation.Add(AccountEnum.ExtractFromDto(contactRelation));

            //     foreach (AccountEnumDto filterC in dto.EnumFiltersConfiguration.AccountEnums)
            //         res.EnumFiltersConfiguration.Add(AccountEnum.ExtractFromDto(filterC));

            //     foreach (AccountEnumDto contactType in dto.EnumContactType.AccountEnums)
            //         res.EnumContactType.Add(AccountEnum.ExtractFromDto(contactType));

            //     foreach (AccountEnumDto relationship in dto.GuarantorRelationShip.AccountEnums)
            //         res.GuarantorRelationShip.Add(AccountEnum.ExtractFromDto(relationship));

            //     foreach (AccountEnumDto enumPriority in dto.EnumPriority.AccountEnums)
            //         res.EnumPriority.Add(AccountEnum.ExtractFromDto(enumPriority));

            //     foreach (AccountEnumDto enumPendingReason in dto.EnumPendingReason.AccountEnums)
            //         res.EnumPendingReason.Add(AccountEnum.ExtractFromDto(enumPendingReason));

            //     foreach (AccountEnumDto enumEmpStatus in dto.EnumEmploymentStatus.AccountEnums)
            //         res.EnumEmploymentStatus.Add(AccountEnum.ExtractFromDto(enumEmpStatus));

            //     foreach (AccountEnumDto enumDiagFlag in dto.EnumDiagnosisFlags.AccountEnums)
            //         res.EnumDiagnosisFlags.Add(AccountEnum.ExtractFromDto(enumDiagFlag));

            //     foreach (AccountEnumDto referralGroup in dto.EnumReferralGroups.AccountEnums)
            //         res.EnumReferralGroups.Add(AccountEnum.ExtractFromDto(referralGroup));

            //     foreach (AccountEnumDto referralGroup in dto.EnumPaymentStatuses.AccountEnums)
            //         res.EnumPaymentStatuses.Add(AccountEnum.ExtractFromDto(referralGroup));

            //     foreach (AccountEnumDto referralGroup in dto.EnumCreditCardTypes.AccountEnums)
            //         res.EnumCreditCardTypes.Add(AccountEnum.ExtractFromDto(referralGroup));

            //     foreach (AccountEnumDto refSpeciality in dto.EnumReferralSpecialities.AccountEnums)
            //         res.EnumReferralSpecialities.Add(AccountEnum.ExtractFromDto(refSpeciality));

            //     res.WorkingSchedule = WorkingSchedule.ExtractFromDto(dto.WorkingSchedule);

            //     foreach (AccountEnumDto enumAuthorizationUserStatus in dto.EnumAuthorizationUserStatuses.AccountEnums)
            //         res.EnumPendingReason.Add(AccountEnum.ExtractFromDto(enumAuthorizationUserStatus));

            //     res.AccountSettings = AccountSettingCollection.ExtractFromDto(dto.AccountSettings);

            //     foreach (AccountEnumDto ae in dto.EnumMammoLaterality.AccountEnums)
            //         res.EnumMammoLaterality.Add(AccountEnum.ExtractFromDto(ae));

            //     foreach (AccountEnumDto ae in dto.EnumMammoMammogramType.AccountEnums)
            //         res.EnumMammoMammogramType.Add(AccountEnum.ExtractFromDto(ae));

            //     foreach (AccountEnumDto ae in dto.EnumMammoMammogramSubType.AccountEnums)
            //         res.EnumMammoMammogramSubType.Add(AccountEnum.ExtractFromDto(ae));

            //     foreach (AccountEnumDto ae in dto.EnumMammoNodalStatus.AccountEnums)
            //         res.EnumMammoNodalStatus.Add(AccountEnum.ExtractFromDto(ae));

            //     foreach (AccountEnumDto ae in dto.EnumMammoTumorSize.AccountEnums)
            //         res.EnumMammoTumorSize.Add(AccountEnum.ExtractFromDto(ae));

            //     foreach (AccountEnumDto ae in dto.EnumMammoBiopsyType.AccountEnums)
            //         res.EnumMammoBiopsyType.Add(AccountEnum.ExtractFromDto(ae));

            //     foreach (AccountEnumDto ae in dto.EnumMammoBirads.AccountEnums)
            //         res.EnumMammoBirads.Add(AccountEnum.ExtractFromDto(ae));

            //     foreach (AccountEnumDto ae in dto.EnumMammoBreastDensity.AccountEnums)
            //         res.EnumMammoBreastDensity.Add(AccountEnum.ExtractFromDto(ae));

            //     foreach (AccountEnumDto ae in dto.EnumTestResultStatus.AccountEnums)
            //         res.EnumTestResultStatus.Add(AccountEnum.ExtractFromDto(ae));


            //     foreach (AccountEnumDto gender in dto.AllGenders.AccountEnums)
            //         res.AllGenders.Add(AccountEnum.ExtractFromDto(gender));

            //     foreach (RaceDto race in dto.AllRaces.Races)
            //         res.AllRaces.Add(Race.ExtractFromDto(race));

            //     foreach (EthnicityDto gender in dto.AllEthnicity.Entities)
            //         res.AllEthnicity.Add(Ethnicity.ExtractFromDto(gender));

            //     foreach (AccountEnumDto gender in dto.AllSmoking.AccountEnums)
            //         res.AllSmoking.Add(AccountEnum.ExtractFromDto(gender));

            //     foreach (AccountEnumDto gender in dto.AllPatientStatuses.AccountEnums)
            //         res.AllPatientStatuses.Add(AccountEnum.ExtractFromDto(gender));

            //     foreach (AccountEnumDto gender in dto.AllSpecialNeeds.AccountEnums)
            //         res.AllSpecialNeeds.Add(AccountEnum.ExtractFromDto(gender));

            //     foreach (AccountEnumDto gender in dto.AllRelationships.AccountEnums)
            //         res.AllRelationships.Add(AccountEnum.ExtractFromDto(gender));

            //     foreach (AccountEnumDto gender in dto.AllEhrSystems.AccountEnums)
            //         res.AllEhrSystems.Add(AccountEnum.ExtractFromDto(gender));

            //     //            foreach (MarketingRepDto gender in dto.AllMarketingReps)
            //     //                res.AllMarketingReps.Add(MarketingRep.ExtractFromDto(gender));


            //     res.ColorsConfiguration = dto.ColorsConfiguration;
            //     res.ViewPatientsInValidLocationsOnly = dto.ViewPatientsInValidLocationsOnly;
            //     res.DefaultViewMode = (int)dto.DefaultViewMode;
            //     res.ScheduleMode = (int)dto.ScheduleMode;
            //     res.ProcedureExpansionMode = dto.ProcedureExpansionMode;
            //     res.PayersSearchMode = dto.PayersSearchMode;
            //     res.IsScheduleAppointmentByEstimationSlots = dto.IsScheduleAppointmentByEstimationSlots;
            //     res.IsStateOfServiceEnabled = dto.IsStateOfServiceEnabled;
            //     res.IsProcedureRequired = dto.IsProcedureRequired;
            //     //            res.StartWorkingHour     = dto.StartWorkingTime.Hours;
            //     //            res.StartWorkingMinute   = dto.StartWorkingTime.Minutes;
            //     //            res.EndWorkingHour       = dto.FinishWorkingTime.Hours;
            //     //            res.EndWorkingMinute     = dto.FinishWorkingTime.Minutes;
            //     res.HourDivisionSegment = dto.HourDivisionSegment;
            //     //            res.WorkingDays          = dto.WorkingDays;
            //     foreach (UserRoleDto role in dto.AvailableRoles)
            //     {
            //         res.AvailableRoles.Add(UserRole.ExtractFromDto(role));
            //     }
            //     res.AvailableAccessControlEntities = AccessControlList.ExtractFromDto(dto.AvailableAccessControlEntities);

            //     res.Id = dto.Id;
            //     res.MRNReadOnly = dto.MRNReadOnly;
            //     res.IsMammographyActive = dto.IsMammographyActive;
            //     res.Name = dto.AccountName;
            //     res.NumberOfVisibleHours = dto.NumberOfVisibleHours;
            //     res.StartWeekOn = dto.StartWeekOn;
            //     res.State = dto.State;
            //     res.LogoUrl = dto.LogoUrl;
            //     res.Address = dto.Address;
            //     res.Address2 = dto.Address2;
            //     res.City = dto.City;
            //     res.ZipCode = dto.ZipCode;
            //     res.Phone = dto.Phone;


            //     res.OrderCreationMode = dto.OrderCreationMode;
            //     //            res.OrderCreationTrigger = dto.OrderCreationTrigger;

            //     foreach (int i in dto.OrderCreationTrigger)
            //         res.OrderCreationTrigger.Add(i);

            //     foreach (int i in dto.VisitCreationTrigger)
            //         res.VisitCreationTrigger.Add(i);

            //     res.VisitCreationTrigger = dto.VisitCreationTrigger;
            //     res.WorkTypeSourceTable = dto.WorkTypeSourceTable;
            //     res.WorkTypeSourceColumn = dto.WorkTypeSourceColumn;
            //     res.HasAccessToScheduler = dto.HasAccessToScheduler;
            //     res.IsSchedulerAdmin = dto.IsSchedulerAdmin;
            //     res.IsWorkWithPatientVisitAllowed = dto.IsWorkWithPatientVisitAllowed;
            //     res.IsDictator = dto.IsDictator;
            //     res.IsAdmin = dto.IsAdmin;

            //     res.IsLocationFilterVis = dto.IsLocationFilterVis;
            //     res.IsModalityFilterVis = dto.IsModalityFilterVis;
            //     res.IsRoomFilterVis = dto.IsRoomFilterVis;
            //     res.IsRoleFilterVis = dto.IsRoleFilterVis;
            //     res.IsProviderFilterVis = dto.IsProviderFilterVis;
            //     res.IsApptStatusFilterVis = dto.IsApptStatusFilterVis;
            //     res.IsPendingEnabled = dto.IsPendingEnabled;
            //     res.IsDaysFilterVis = dto.IsDaysFilterVis;
            //     res.IsPhyGroupVis = dto.IsPhyGroupVis;
            //     res.IsWtGroupVis = dto.IsWtGroupVis;
            //     res.IsReferralRequired = dto.IsReferralRequired ?? false;
            //     res.IsPaymentsEnabled = dto.IsPaymentsEnabled;
            //     res.IsProcessPaymentsEnabled = dto.IsProcessPaymentsEnabled;
            //     res.IsBillingNoteRequired = dto.IsBillingNoteRequired;
            //     res.IsCreateOrderRequired = dto.IsCreateOrderRequired;
            //     res.IsVisitReasonRequired = dto.IsVisitReasonRequired;
            //     res.PreselectProcedureTypes = dto.PreselectProcedureTypes;
            //     res.PatientCategoryRequired = dto.PatientCategoryRequired;
            //     res.IsProcedureGlobalSearchEnabled = dto.IsProcedureGlobalSearchEnabled;
            //     res.IsCommentForBlockingRequired = dto.IsCommentForBlockingRequired;
            //     res.IsPatientDOBMandatory = dto.IsPatientDOBMandatory;
            //     res.IsWarningMessagesEnabled = dto.IsWarningMessagesEnabled;
            //     res.SendEmailFromAddress = dto.SendEmailFromAddress;
            //     return res;
        }

        public static AccountDto ToDto(this Account account)
        {
            throw new NotImplementedException();

            //if (account == null)
            //    return null;

            //AccountDto result = new AccountDto();
            //result.ViewPatientsInValidLocationsOnly = account.ViewPatientsInValidLocationsOnly;
            //result.Id = account.Id;
            //result.AccountName = account.Name;
            ////            result.WorkingDays = account.WorkingDays;
            //foreach (UserRole role in account.AvailableRoles)
            //{
            //    result.AvailableRoles.Add(UserRole.Convert2Dto(role));
            //}
            //result.AvailableAccessControlEntities = AccessControlList.Convert2Dto(account.AvailableAccessControlEntities);
            ////            result.StartWorkingTime = new AccountDto.DayTime(account.StartWorkingHour, account.StartWorkingMinute);
            ////            result.FinishWorkingTime = new AccountDto.DayTime(account.EndWorkingHour, account.EndWorkingMinute);
            //result.IsScheduleAppointmentByEstimationSlots = account.IsScheduleAppointmentByEstimationSlots;
            //result.IsStateOfServiceEnabled = account.IsStateOfServiceEnabled;
            //result.IsProcedureRequired = account.IsProcedureRequired;
            //result.HourDivisionSegment = account.HourDivisionSegment;
            //result.ScheduleMode = (ScheduleMode)account.ScheduleMode;
            //result.ProcedureExpansionMode = account.ProcedureExpansionMode;
            //result.PayersSearchMode = account.PayersSearchMode;
            //result.DefaultViewMode = (ReservationViewMode)account.DefaultViewMode;
            //result.StartWeekOn = account.StartWeekOn;
            //result.NumberOfVisibleHours = account.NumberOfVisibleHours;
            //result.AccountDiagnosesAreAvailable = account.AccountDiagnosesAreAvailable;
            //result.AccountProceduresAreAvailable = account.AccountProceduresAreAvailable;
            //result.State = account.State;
            //result.LogoUrl = account.LogoUrl;
            //result.Address = account.Address;
            //result.Address2 = account.Address2;
            //result.City = account.City;
            //result.ZipCode = account.ZipCode;
            //result.Phone = account.Phone;
            //result.IsCrmEnabled = account.IsCrmEnabled;

            //result.SendEmailFromAddress = account.SendEmailFromAddress;

            //result.OrderCreationMode = account.OrderCreationMode;
            //result.OrderCreationTrigger = account.OrderCreationTrigger;
            //result.VisitCreationTrigger = account.VisitCreationTrigger;
            //result.WorkTypeSourceTable = account.WorkTypeSourceTable;
            //result.WorkTypeSourceColumn = account.WorkTypeSourceColumn;
            //result.HasAccessToScheduler = account.HasAccessToScheduler;
            //result.IsSchedulerAdmin = account.IsSchedulerAdmin;
            //result.IsWorkWithPatientVisitAllowed = account.IsWorkWithPatientVisitAllowed;
            //result.IsDictator = account.IsDictator;
            //result.IsAdmin = account.IsAdmin;

            //result.IsLocationFilterVis = account.IsLocationFilterVis;
            //result.IsModalityFilterVis = account.IsModalityFilterVis;
            //result.IsRoomFilterVis = account.IsRoomFilterVis;
            //result.IsRoleFilterVis = account.IsRoleFilterVis;
            //result.IsProviderFilterVis = account.IsProviderFilterVis;
            //result.IsApptStatusFilterVis = account.IsApptStatusFilterVis;
            //result.IsDaysFilterVis = account.IsDaysFilterVis;
            //result.IsPhyGroupVis = account.IsPhyGroupVis;
            //result.IsWtGroupVis = account.IsWtGroupVis;
            //result.IsReferralRequired = account.IsReferralRequired;
            //result.IsPaymentsEnabled = account.IsPaymentsEnabled;
            //result.IsProcessPaymentsEnabled = account.IsProcessPaymentsEnabled;
            //result.IsBillingNoteRequired = account.IsBillingNoteRequired;
            //result.IsCreateOrderRequired = account.IsCreateOrderRequired;
            //result.IsPendingEnabled = account.IsPendingEnabled;
            //result.IsVisitReasonRequired = account.IsVisitReasonRequired;
            //result.PreselectProcedureTypes = account.PreselectProcedureTypes;
            //result.IsLocationFilterVis = account.IsLocationFilterVis;
            //result.IsModalityFilterVis = account.IsModalityFilterVis;
            //result.IsRoomFilterVis = account.IsRoomFilterVis;
            //result.IsRoleFilterVis = account.IsRoleFilterVis;
            //result.IsProviderFilterVis = account.IsProviderFilterVis;
            //result.IsApptStatusFilterVis = account.IsApptStatusFilterVis;
            //result.IsDaysFilterVis = account.IsDaysFilterVis;
            //result.IsPhyGroupVis = account.IsPhyGroupVis;
            //result.IsWtGroupVis = account.IsWtGroupVis;
            //result.PatientCategoryRequired = account.PatientCategoryRequired;
            //result.IsProcedureGlobalSearchEnabled = account.IsProcedureGlobalSearchEnabled;
            //result.IsWarningMessagesEnabled = account.IsWarningMessagesEnabled;
            //result.IsCommentForBlockingRequired = account.IsCommentForBlockingRequired;
            //result.IsPatientDOBMandatory = account.IsPatientDOBMandatory;
            //result.MRNReadOnly = account.MRNReadOnly;
            //result.IsMammographyActive = account.IsMammographyActive;

            //foreach (CommentType c in account.CommentTypes)
            //{
            //    //CommentTypeDto dto = new CommentTypeDto();
            //    //dto.TypeId = c.Id;
            //    //dto.TypeName = c.Name;
            //    //dto.IsVisible = c.IsVisible;
            //    //dto.IsSystem = c.IsSystem;
            //    result.CommentTypes.Add(CommentType.Convert2Dto(c));
            //}

            //foreach (TechCompleteSuggestionList list in account.TechCompleteSuggestionList)
            //{
            //    TechCompleteSuggestionListDto dto = new TechCompleteSuggestionListDto();
            //    dto.Id = list.Id;
            //    dto.DisplayName = list.DisplayName;
            //    dto.IsVisible = list.IsVisible;
            //    result.TechCompleteSuggestionList.Add(dto);
            //}

            //foreach (ModalityType mt in account.ModalityTypes)
            //{
            //    ModalityTypeDto dto = new ModalityTypeDto();
            //    dto.TypeId = mt.Id;
            //    dto.TypeName = mt.Name;
            //    dto.ModalityId = mt.ModalityId;
            //    result.ModalityTypes.Add(dto);
            //}
            //foreach (PhysicianType pt in account.PhysicianTypes)
            //{
            //    PhysicianTypeDto dto = new PhysicianTypeDto();
            //    dto.TypeId = pt.Id;
            //    dto.TypeName = pt.Name;
            //    dto.Color = pt.Color;
            //    result.PhysicianTypes.Add(dto);
            //}

            //result.CodeCategories.Add(new CodeCategoryDto() { TypeId = -1, TypeName = "ALL" });

            //foreach (CodeCategory cc in account.CodeCategories)
            //{
            //    CodeCategoryDto ccdto = new CodeCategoryDto();
            //    ccdto.TypeId = cc.Id;
            //    ccdto.TypeName = cc.Name;
            //    ccdto.IsActive = cc.IsActive;
            //    if (cc.Parent != null)
            //        ccdto.Parent = new CodeCategoryDto(cc.Parent.Id);

            //    result.CodeCategories.Add(ccdto);
            //}

            //foreach (Area a in account.ResouceAreas)
            //{
            //    ResourceAreaDto dto = new ResourceAreaDto();
            //    dto.Id = a.Id;
            //    dto.City = a.City;
            //    dto.Country = a.Country;
            //    dto.State = a.State;
            //    result.ResouceAreas.Add(dto);
            //}

            //foreach (ResourceLocation location in account.ResourceLocations)
            //{
            //    ResourceLocationDto dto = ResourceLocation.Convert2Dto(location);
            //    result.ResourceLocations.Add(dto);
            //}

            //foreach (AppointmentResourceType rt in account.ResourceTypes)
            //{
            //    AppointmentResourceTypeDto dto = new AppointmentResourceTypeDto();
            //    dto.Id = rt.Id;
            //    dto.Filterable = rt.Filterable;
            //    dto.AccountId = result.Id;
            //    dto.TypeName = rt.TypeName;
            //    result.ResourceTypes.Add(dto);
            //}

            //foreach (AppointmentStatus s in account.AppointmentStatuses)
            //{
            //    AppointmentStatusDto dto = new AppointmentStatusDto();
            //    dto.StatusID = s.Id;
            //    dto.StatusName = s.StatusName;
            //    dto.AppliedStatusName = s.AppliedStatusName;
            //    dto.Color = s.Color;
            //    dto.IsVisible = s.IsVisible;
            //    dto.SortIndex = s.SortIndex;
            //    dto.IsSystemStatus = s.IsSystemStatus;
            //    foreach (AppointmentStatusTransition transition in s.AllowedTransition)
            //        dto.AllowedTransition.Add(AppointmentStatusTransition.Convert2Dto(transition));
            //    //                dto.AllowedTransition.AddRange(s.AllowedTransition);
            //    result.AppointmentStatuses.Add(dto);
            //}

            //foreach (VolumeUnit unit in account.VolumeUnits)
            //{
            //    result.VolumeUnits.VolumeUnits.Add(VolumeUnit.Convert2Dto(unit));
            //}

            //foreach (long key in account.ColorsConfiguration.Keys)
            //    result.ColorsConfiguration.Add(key, account.ColorsConfiguration[key]);

            //foreach (string key in account.AvailablePayers.Keys)
            //    result.AvailablePayers.Add(key, account.AvailablePayers[key]);

            //foreach (string key in account.PayerStates.Keys)
            //    result.PayerStates.Add(key, account.PayerStates[key]);

            //// TODO: ReferralSpecs
            ////  foreach (int key in account.ReferralSpecialities.Keys)
            ////  result.ReferralSpecialities.Add(key, account.ReferralSpecialities[key]);

            //result.ReferralSpecialities = new PhysicianSpecialitiesDto()
            //{ PhysicianSpecialities = new List<PhysicianSpecialityDto>() };
            ////
            ////            foreach (PhysicianSpeciality referralSpeciality in account.ReferralSpecialities)
            ////                result.ReferralSpecialities.PhysicianSpecialities.Add(PhysicianSpeciality.Convert2Dto(referralSpeciality));

            //// foreach (int key in account.VolumeUnits.Keys)
            ////    result.VolumeUnits.Add(key, account.VolumeUnits[key]);

            //foreach (AccountEnum patientCategorie in account.VisitCategories)
            //    result.VisitCategories.AccountEnums.Add(AccountEnum.Convert2Dto(patientCategorie));

            //foreach (string key in account.AvailableProviders.Keys)
            //    result.AvailableProviders.Add(key, account.AvailableProviders[key]);

            //foreach (string key in account.AvailableInsRelationships.Keys)
            //    result.AvailableInsRelationships.Add(key, account.AvailableInsRelationships[key]);

            //foreach (string key in account.AvailableLanguages.Keys)
            //    result.AvailableLanguages.Add(key, account.AvailableLanguages[key]);

            //foreach (string key in account.UsaStates.Keys)
            //    result.UsaStates.Add(key, account.UsaStates[key]);

            //foreach (string key in account.WorkTypes.Keys)
            //    result.WorkTypes.Add(key, account.WorkTypes[key]);

            //foreach (OrderCreationParameter p in account.OrderCreationParameters)
            //    result.OrderCreationParameters.Add(OrderCreationParameter.Convert2Dto(p));

            //foreach (AccountGenerateIDconfig a in account.GenerateIDconfigs)
            //    result.GenerateIDconfigs.Add(AccountGenerateIDconfig.Convert2Dto(a));

            //foreach (AuthorizationAlert alert in account.AutorizationAlerts)
            //    result.AutorizationAlerts.Add(AuthorizationAlert.Convert2Dto(alert));

            //foreach (AccountEnum hcpcScode in account.HCPCScodes)
            //    result.HCPCSCodes.AccountEnums.Add(AccountEnum.Convert2Dto(hcpcScode));

            //foreach (AccountEnum enumScheduledBy in account.EnumsScheduledBy)
            //    result.EnumsScheduledBy.AccountEnums.Add(AccountEnum.Convert2Dto(enumScheduledBy));

            //foreach (AccountEnum enumScheduledBy in account.EnumPatientAilment)
            //    result.EnumsPatientAilment.AccountEnums.Add(AccountEnum.Convert2Dto(enumScheduledBy));

            //foreach (AccountEnum enumHeardOfUs in account.EnumsHeardOfUs)
            //    result.EnumsHeardOfUs.AccountEnums.Add(AccountEnum.Convert2Dto(enumHeardOfUs));

            //foreach (AccountEnum maritalStatus in account.EnumMaritalStatus)
            //    result.EnumsMaritalStatus.AccountEnums.Add(AccountEnum.Convert2Dto(maritalStatus));

            //foreach (AccountEnum contactType in account.EnumContactType)
            //    result.EnumContactType.AccountEnums.Add(AccountEnum.Convert2Dto(contactType));

            //foreach (AccountEnum contactRelation in account.EnumContactRelation)
            //    result.EnumContactRelation.AccountEnums.Add(AccountEnum.Convert2Dto(contactRelation));

            //foreach (AccountEnum filterC in account.EnumFiltersConfiguration)
            //    result.EnumFiltersConfiguration.AccountEnums.Add(AccountEnum.Convert2Dto(filterC));


            //foreach (AccountEnum maritalStatus in account.GuarantorRelationShip)
            //    result.GuarantorRelationShip.AccountEnums.Add(AccountEnum.Convert2Dto(maritalStatus));

            //foreach (AccountEnum enumPriority in account.EnumPriority)
            //    result.EnumPriority.AccountEnums.Add(AccountEnum.Convert2Dto(enumPriority));

            //foreach (AccountEnum enumPendingReason in account.EnumPendingReason)
            //    result.EnumPendingReason.AccountEnums.Add(AccountEnum.Convert2Dto(enumPendingReason));

            //foreach (AccountEnum enumEmpStatus in account.EnumEmploymentStatus)
            //    result.EnumEmploymentStatus.AccountEnums.Add(AccountEnum.Convert2Dto(enumEmpStatus));

            //foreach (AccountEnum enumDiagFlag in account.EnumDiagnosisFlags)
            //    result.EnumDiagnosisFlags.AccountEnums.Add(AccountEnum.Convert2Dto(enumDiagFlag));

            //foreach (AccountEnum refSpeciality in account.EnumReferralSpecialities)
            //    result.EnumReferralSpecialities.AccountEnums.Add(AccountEnum.Convert2Dto(refSpeciality));

            //foreach (AccountEnum refGroup in account.EnumReferralGroups)
            //    result.EnumReferralGroups.AccountEnums.Add(AccountEnum.Convert2Dto(refGroup));

            //foreach (AccountEnum refGroup in account.EnumPaymentStatuses)
            //    result.EnumPaymentStatuses.AccountEnums.Add(AccountEnum.Convert2Dto(refGroup));

            //foreach (AccountEnum refGroup in account.EnumCreditCardTypes)
            //    result.EnumCreditCardTypes.AccountEnums.Add(AccountEnum.Convert2Dto(refGroup));

            //result.WorkingSchedule = WorkingSchedule.Convert2Dto(account.WorkingSchedule);

            //foreach (AccountEnum enumAuthorizationUserStatus in account.EnumAuthorizationUserStatuses)
            //    result.EnumAuthorizationUserStatuses.AccountEnums.Add(AccountEnum.Convert2Dto(enumAuthorizationUserStatus));

            //foreach (AccountEnum enumMultipleIdentifierSource in account.EnumMultipleIdentifierSource)
            //    result.EnumMultipleIdentifierSource.AccountEnums.Add(AccountEnum.Convert2Dto(enumMultipleIdentifierSource));

            //foreach (AccountEnum enumMultipleIdentifierSource in account.EnumPatientCommentTransferredTo)
            //    result.EnumPatientCommentTransferredTo.AccountEnums.Add(AccountEnum.Convert2Dto(enumMultipleIdentifierSource));

            //result.AccountSettings = account.AccountSettings.Convert2Dto();

            //foreach (AccountEnum ae in account.EnumMammoLaterality)
            //    result.EnumMammoLaterality.AccountEnums.Add(AccountEnum.Convert2Dto(ae));

            //foreach (AccountEnum ae in account.EnumMammoMammogramType)
            //    result.EnumMammoMammogramType.AccountEnums.Add(AccountEnum.Convert2Dto(ae));

            //foreach (AccountEnum ae in account.EnumMammoMammogramSubType)
            //    result.EnumMammoMammogramSubType.AccountEnums.Add(AccountEnum.Convert2Dto(ae));

            //foreach (AccountEnum ae in account.EnumMammoNodalStatus)
            //    result.EnumMammoNodalStatus.AccountEnums.Add(AccountEnum.Convert2Dto(ae));

            //foreach (AccountEnum ae in account.EnumMammoTumorSize)
            //    result.EnumMammoTumorSize.AccountEnums.Add(AccountEnum.Convert2Dto(ae));

            //foreach (AccountEnum ae in account.EnumMammoBiopsyType)
            //    result.EnumMammoBiopsyType.AccountEnums.Add(AccountEnum.Convert2Dto(ae));

            //foreach (AccountEnum ae in account.EnumMammoBirads)
            //    result.EnumMammoBirads.AccountEnums.Add(AccountEnum.Convert2Dto(ae));

            //foreach (AccountEnum ae in account.EnumMammoBreastDensity)
            //    result.EnumMammoBreastDensity.AccountEnums.Add(AccountEnum.Convert2Dto(ae));

            //foreach (AccountEnum ae in account.EnumTestResultStatus)
            //    result.EnumTestResultStatus.AccountEnums.Add(AccountEnum.Convert2Dto(ae));

            //foreach (AccountEnum gender in account.AllGenders)
            //    result.AllGenders.AccountEnums.Add(AccountEnum.Convert2Dto(gender));

            //foreach (Race gender in account.AllRaces)
            //    result.AllRaces.Races.Add(Race.Convert2Dto(gender));

            //foreach (var gender in account.AllEthnicity)
            //    result.AllEthnicity.Entities.Add(Ethnicity.Convert2Dto(gender));

            //foreach (AccountEnum gender in account.AllSmoking)
            //    result.AllSmoking.AccountEnums.Add(AccountEnum.Convert2Dto(gender));

            //foreach (AccountEnum gender in account.AllPatientStatuses)
            //    result.AllPatientStatuses.AccountEnums.Add(AccountEnum.Convert2Dto(gender));

            //foreach (AccountEnum gender in account.AllSpecialNeeds)
            //    result.AllSpecialNeeds.AccountEnums.Add(AccountEnum.Convert2Dto(gender));

            //foreach (AccountEnum gender in account.AllRelationships)
            //    result.AllRelationships.AccountEnums.Add(AccountEnum.Convert2Dto(gender));

            //foreach (NotificationSlot.NotificationSlot slot in account.NotificationSlot)
            //    result.NotificationSlots.Add(slot.ReflectedConvert2Mobile<NotificationSlotDto>());

            //foreach (MarketingRep rep in account.AllMarketingReps)
            //    result.AllMarketingReps.Add(rep.ReflectedConvert2Mobile<MarketingRepDto>());

            //foreach (var rep in account.AllEhrSystems)
            //    result.AllEhrSystems.AccountEnums.Add(AccountEnum.Convert2Dto(rep));

            //foreach (AppointmentCheckListItem item in account.AppointmentCheckListItems)
            //{
            //    result.AppointmentCheckListItems.Add(AppointmentCheckListItem.Convert2Dto(item));
            //}

            //return result;
        }

        public static AccountGenerateIDconfigDto ToDto(this AccountGenerateIDconfig i)
        {
            AccountGenerateIDconfigDto r = new AccountGenerateIDconfigDto();
            r.Id = i.Id;
            r.AccountId = i.AccountId;
            r.CustomLocationCode = i.CustomLocationCode;
            r.GuidLen = i.GuidLen;
            r.IDFormatString = i.IDFormatString;
            r.IdTypeName = i.IdTypeName;
            r.IsSeqPadded = i.IsSeqPadded;
            r.Location = i.Location;
            r.NextAvailableSeq = i.NextAvailableSeq;
            r.PostFix = i.PostFix;
            r.PreFix = i.PreFix;
            r.SeqPaddingChar = i.SeqPaddingChar;
            r.SeqPaddingDir = i.SeqPaddingDir;
            r.SeqPaddingLen = i.SeqPaddingLen;
            r.StartingSeq = i.StartingSeq;
            r.UseGuid = i.UseGuid;
            return r;

        }

        public static AccountGenerateIDconfig ToDbEntity(this AccountGenerateIDconfigDto configuration)
        {
            return new AccountGenerateIDconfig()
            {
                Id = configuration.Id,
                AccountId = configuration.AccountId,
                IdTypeName = configuration.IdTypeName,
                IDFormatString = configuration.IDFormatString,

                Location = configuration.Location,
                CustomLocationCode = configuration.CustomLocationCode,
                PreFix = configuration.PreFix,
                PostFix = configuration.PostFix,
                SeqPaddingChar = configuration.SeqPaddingChar,
                SeqPaddingDir = configuration.SeqPaddingDir,

                StartingSeq = configuration.StartingSeq,
                NextAvailableSeq = configuration.NextAvailableSeq,
                SeqPaddingLen = configuration.SeqPaddingLen,
                GuidLen = configuration.GuidLen,

                IsSeqPadded = configuration.IsSeqPadded,
                UseGuid = configuration.UseGuid,
            };
        }

        public static AccountSetting ToDbEntity(this AccountSettingDto dto)
        {
            AccountSetting result = new AccountSetting();
            result.Id = dto.Id;
            result.Name = dto.Name;
            result.Value = dto.Value;
            result.Application = dto.Application;
            result.CreateDate = dto.CreateDate;
            result.CreateUser = dto.CreateUser;
            result.UpdateDate = dto.UpdateDate;
            result.UpdateUser = dto.UpdateUser;
            result.IsActive = dto.IsActive;
            return result;
        }

        public static Address ToDbEntity(this AddressDto addressDto)
        {
            Address result = new Address();
            result.Id = addressDto.Id;
            result.Address1 = addressDto.Address1;
            result.Address2 = addressDto.Address2;
            result.City = addressDto.City;
            result.State = addressDto.State;
            result.ZipCode = addressDto.ZipCode;
            result.Country = addressDto.Country;
            result.Phone = addressDto.Phone;
            result.Fax = addressDto.Fax;
            result.Email = addressDto.Email;
            result.Mobile = addressDto.Mobile;
            result.IsInternational = addressDto.IsInternational;
            result.EntityStatus = addressDto.EntityStatus;
            return result;
        }

        public static AddressDto ToDto(this Address address)
        {
            AddressDto result = new AddressDto();
            result.Id = address.Id;
            result.Address1 = address.Address1;
            result.Address2 = address.Address2;
            result.City = address.City;
            result.Country = address.Country;
            result.Fax = address.Fax;
            result.Mobile = address.Mobile;
            result.Phone = address.Phone;
            result.State = address.State;
            result.ZipCode = address.ZipCode;
            result.IsInternational = address.IsInternational;
            result.EntityStatus = address.EntityStatus;
            result.Email = address.Email;
            return result;
        }

        //public static AddressTypeDto ToDto(this AddressType type)
        //{
        //    AddressTypeDto r = new AddressTypeDto();
        //    r.Address1 = type.Address1;
        //    r.Address2 = type.Address2;
        //    r.City = type.City;
        //    r.Country = type.Country;
        //    r.County = type.County;
        //    r.Email = type.Email;
        //    r.Fax = type.Fax;
        //    r.InternationalProvince = type.InternationalProvince;
        //    r.Phone = type.Phone;
        //    r.POBox = type.POBox;
        //    r.State = type.State;
        //    r.ZipCode = type.ZipCode;
        //    return r;
        //}

        //internal static AddressType ExtractFromDto(AddressTypeDto type)
        //{
        //    AddressType r = new AddressType();
        //    r.Address1 = type.Address1;
        //    r.Address2 = type.Address2;
        //    r.City = type.City;
        //    r.Country = type.Country;
        //    r.County = type.County;
        //    r.Email = type.Email;
        //    r.Fax = type.Fax;
        //    r.InternationalProvince = type.InternationalProvince;
        //    r.Phone = type.Phone;
        //    r.POBox = type.POBox;
        //    r.State = type.State;
        //    r.ZipCode = type.ZipCode;
        //    return r;
        //}

        //public static User ExtractFromDto(UserDto dto)
        //{
        //    if (dto == null)
        //        return null;

        //    User u = new User();
        //    u.Id = dto.Id;
        //    u.Login = dto.Login;
        //    u.FirstName = dto.FirstName;
        //    u.LastName = dto.LastName;
        //    u.Password = dto.Password;
        //    u.DefaultAppointmentLength = dto.DefaultAppointmentLength;
        //    u.IsSuperAdmin = dto.IsSuperAdmin;

        //    foreach (AccountDto a in dto.Accounts)
        //        u.Accounts.Add(Account.ExtractFromDto(a));

        //    foreach (UserProfileDto p in dto.Profiles)
        //        u.Profiles.Add(UserProfile.ExtractFromDto(p));

        //    u.CurrentProfile = UserProfile.ExtractFromDto(dto.CurrentProfile);

        //    u.Roles.AddRange(dto.Roles);

        //    return u;
        //}

        //public static UserDto Convert2Dto(User u)
        //{
        //    if (u == null)
        //        return null;

        //    UserDto result = new UserDto();
        //    result.DefaultAppointmentLength = u.DefaultAppointmentLength;
        //    result.Id = u.Id;
        //    result.Login = u.Login;
        //    result.LastName = u.LastName;
        //    result.FirstName = u.FirstName;
        //    result.IsIDSUser = u.IsIDSUser;
        //    result.Password = u.Password;
        //    //            result.IsAdmin                  = u.IsAdmin;
        //    result.IsSuperAdmin = u.IsSuperAdmin;
        //    //            result.HasAccessToScheduler     = u.HasAccessToScheduler;
        //    foreach (Account a in u.Accounts)
        //        result.Accounts.Add(Account.Convert2Dto(a));

        //    foreach (UserProfile p in u.Profiles)
        //        result.Profiles.Add(UserProfile.Convert2Dto(p));

        //    result.CurrentProfile = UserProfile.Convert2Dto(u.CurrentProfile);

        //    result.Roles.AddRange(u.Roles);
        //    return result;
        //}

        //public static UserProfileComment ExtractFromDto(UserProfileCommentDto p)
        //{
        //    if (p == null)
        //        return null;

        //    UserProfileComment res = new UserProfileComment();
        //    res.IsAppointmentOnly = p.IsAppointmentOnly;
        //    foreach (CommentTypeDto dto in p.PredefinedCommentTypes)
        //        res.PredefinedCommentTypes.Add(CommentType.ExtractFromDto(dto));

        //    res.TimeFrom = p.TimeFrom;
        //    res.TimeTo = p.TimeTo;

        //    return res;
        //}

        //public static UserProfileCommentDto Convert2Dto(UserProfileComment profile)
        //{
        //    if (profile == null)
        //        return null;

        //    UserProfileCommentDto result = new UserProfileCommentDto();
        //    result.IsAppointmentOnly = profile.IsAppointmentOnly;

        //    foreach (CommentType type in profile.PredefinedCommentTypes)
        //        result.PredefinedCommentTypes.Add(CommentType.Convert2Dto(type));

        //    result.TimeTo = profile.TimeTo;
        //    result.TimeFrom = profile.TimeFrom;

        //    return result;
        //}

        //internal static ColumnConfig ExtractFromDto(UserProfileDto.ColumnConfigDto c)
        //{
        //    ColumnConfig res = new ColumnConfig();
        //    res.Id = c.Id;
        //    res.Name = c.Name;
        //    res.Order = c.Order;
        //    res.Size = c.Size;
        //    res.Sort = c.Sort;
        //    return res;
        //}

        //public static UserProfile ExtractFromDto(UserProfileDto p)
        //{
        //    if (p == null)
        //        return null;

        //    UserProfile res = new UserProfile();
        //    res.Id = p.Id;
        //    res.AccountID = p.AccountID;
        //    res.UserId = p.UserId;
        //    res.RoleId = p.RoleId;
        //    res.RoleName = p.RoleName;
        //    res.Filters = p.Filters;
        //    res.DefaultViewMode = p.DefaultViewMode;
        //    res.ScheduleMode = p.ScheduleMode;
        //    res.PrintOption = (int)p.PrintOption;
        //    res.CalendarDisplayMode = p.CalendarDisplayMode;
        //    res.FirstVisibleHour = p.FirstVisibleHour;
        //    res.LastVisibleHour = p.LastVisibleHour;
        //    res.PatientInfoVisitIndex = p.PatientInfoVisitIndex;
        //    res.CaptionMode = p.CaptionMode;
        //    res.ProfileName = p.ProfileName;
        //    res.IsSummaryPageEnabled = p.IsSummaryPageEnabled;
        //    res.IsExportedSuccessfully = p.IsExportedSuccessfully;
        //    res.IsDefault = p.IsDefault;

        //    foreach (UserProfileDto.ColumnConfigDto c in p.Columns)
        //        res.Columns.Add(ColumnConfig.ExtractFromDto(c));

        //    if (p.TabsConfiguration != null)
        //        foreach (KeyValuePair<int, bool> b in p.TabsConfiguration)
        //            res.TabsConfiguraton.Add(b.Key, b.Value);

        //    res.CommentDefaults = UserProfileComment.ExtractFromDto(p.CommentDefaults);
        //    return res;
        //}

        //public static UserProfileDto Convert2Dto(UserProfile profile)
        //{
        //    UserProfileDto result = new UserProfileDto();
        //    result.Id = profile.Id;
        //    result.AccountID = profile.AccountID;
        //    result.UserId = profile.UserId;
        //    result.RoleId = profile.RoleId;
        //    result.RoleName = profile.RoleName;
        //    result.DefaultViewMode = profile.DefaultViewMode;
        //    result.Filters = profile.Filters;
        //    result.ScheduleMode = profile.ScheduleMode;
        //    result.PrintOption = (PrintOptions)profile.PrintOption;
        //    result.CalendarDisplayMode = profile.CalendarDisplayMode;
        //    result.FirstVisibleHour = profile.FirstVisibleHour;
        //    result.LastVisibleHour = profile.LastVisibleHour;
        //    result.PatientInfoVisitIndex = profile.PatientInfoVisitIndex;
        //    result.CaptionMode = profile.CaptionMode;
        //    result.ProfileName = profile.ProfileName;
        //    result.IsSummaryPageEnabled = profile.IsSummaryPageEnabled;
        //    result.IsExportedSuccessfully = profile.IsExportedSuccessfully;
        //    result.IsDefault = profile.IsDefault;

        //    foreach (UserProfile.ColumnConfig col in profile.Columns)
        //        result.Columns.Add(new UserProfileDto.ColumnConfigDto() { Name = col.Name, Order = col.Order, Size = col.Size, Sort = col.Sort });

        //    foreach (KeyValuePair<int, bool> b in profile.TabsConfiguraton)
        //        result.TabsConfiguration.Add(b.Key, b.Value);

        //    result.CommentDefaults = UserProfileComment.Convert2Dto(profile.CommentDefaults);

        //    return result;
        //}

        //public static PatientInvoice ExtractFromDto(PatientInvoiceDto payment)
        //{
        //    PatientInvoice result = new PatientInvoice();
        //    result.CreatedDate = payment.CreatedDate;
        //    result.PatientPayments = new List<PatientPayment>();
        //    foreach (PatientPaymentDto patientPayment in payment.PatientPayments)
        //    {
        //        result.PatientPayments.Add(PatientPayment.ExtractFromDto(patientPayment));
        //    }
        //    return result;
        //}

        //public static PatientInvoiceDto ConvertToDto(PatientInvoice invoice)
        //{
        //    PatientInvoiceDto result = new PatientInvoiceDto();
        //    result.CreatedDate = invoice.CreatedDate;
        //    result.PatientPayments = new List<PatientPaymentDto>();
        //    foreach (PatientPayment patientPayment in invoice.PatientPayments)
        //    {
        //        result.PatientPayments.Add(PatientPayment.Convert2Dto(patientPayment));
        //    }
        //    return result;
        //}

        //public AuditEntryDto Convert2Dto()
        //{
        //    AuditEntryDto result = new AuditEntryDto();

        //    result.AppointmentId = this.AppointmentId;
        //    result.Location = this.Location;
        //    result.UserId = this.UserId;
        //    result.AuditMsg = this.AuditMsg;
        //    result.UserName = this.UserName;
        //    result.Printer = this.Printer;
        //    result.Date = this.Date;
        //    result.ComputerName = this.ComputerName;
        //    result.Destination = this.Destination;
        //    result.UserActivityId = this.UserActivityId;
        //    result.ActionType = ActionType;
        //    result.EntityId = EntityId;
        //    result.EntityName = EntityName;
        //    return result;
        //}


        public static UnhandledExceptionEntryDto ToDto(this UnhandledExceptionEntry app)
        {
            UnhandledExceptionEntryDto r = new UnhandledExceptionEntryDto();
            r.ExceptionDate = app.ExceptionDate;
            r.Message = app.Message;
            r.MethodName = app.MethodName;
            r.StackTrace = app.StackTrace;
            r.UserLogin = app.UserLogin;
            return r;
        }

        public static UnhandledExceptionEntry ToDbEntity(this UnhandledExceptionEntryDto dto)
        {
            UnhandledExceptionEntry r = new UnhandledExceptionEntry(dto.UserLogin, dto.Message, dto.StackTrace, dto.ExceptionDate, dto.MethodName);
            return r;
        }

        public static ExceptionRequest ToDbEntity(this ExceptionRequestDto req)
        {
            ExceptionRequest r = new ExceptionRequest();
            r.StartDate = req.StartDate;
            r.EndDate = req.EndDate;
            r.Message = req.Message;
            r.User = req.User;

            return r;
        }

        //public static OrderReport ExtractFromDto(OrderReportDto orderReportDto)
        //{
        //    OrderReport result = new OrderReport();

        //    result.Account = orderReportDto.Account;
        //    result.FilePath = orderReportDto.FilePath;
        //    result.JobId = orderReportDto.JobId;
        //    result.Html = orderReportDto.Html;

        //    return result;
        //}

        //public static OrderReportDto Convert2Dto(OrderReport order)
        //{
        //    OrderReportDto result = new OrderReportDto();

        //    result.Account = order.Account;
        //    result.FilePath = order.FilePath;
        //    result.JobId = order.JobId;
        //    result.Html = order.Html;

        //    return result;
        //}

        //public static PatientVisitHistory ExtractFromDto(PatientVisitHistoryDto patientVisitHistory)
        //{
        //    PatientVisitHistory r = new PatientVisitHistory();

        //    //            foreach (DiagnosisDto d in patientVisitHistory.Diagnoses)
        //    //                r.Diagnosises.Add(Diagnosis.ExtractFromDto(d));

        //    //            foreach (ProcedureDto p in patientVisitHistory.Procedures)
        //    //                r.Procedures.Add(Procedure.ExtractFromDto(p));

        //    //            foreach (UsedAuthorizationDto auth in patientVisitHistory.UsedAuthorizations)
        //    //                r.UsedAuthorizations.Add(UsedAuthorization.ExtractFromDto(auth));

        //    //            foreach (ReferralDto p in patientVisitHistory.Referrals)
        //    //                r.Referrals.Add(Referral.ExtractFromDto(p));

        //    foreach (var orderReportDto in patientVisitHistory.OrderReports)
        //        r.OrderReports.Add(OrderReport.ExtractFromDto(orderReportDto));


        //    r.PatientVisitHistoryID = patientVisitHistory.PatientVisitHistoryID;
        //    //            r.FirstName                 = patientVisitHistory.FirstName;
        //    //            r.LastName                  = patientVisitHistory.LastName;
        //    r.EndDate = patientVisitHistory.EndDate;
        //    r.StartDate = patientVisitHistory.StartDate;
        //    r.VisitReason = patientVisitHistory.VisitReason;

        //    r.ModalityId = patientVisitHistory.ModalityId;
        //    r.ModalityName = patientVisitHistory.ModalityName;

        //    r.LocationName = patientVisitHistory.LocationName;
        //    r.LocationId = patientVisitHistory.LocationId;
        //    r.PendingReason = patientVisitHistory.PendingReason;
        //    r.PendingReasonCode = patientVisitHistory.PendingReasonCode;

        //    r.Patient = AppointmentResourcePatient.ExtractFromDto(patientVisitHistory.Patient);
        //    return r;
        //}

        //public static PatientVisitHistoryDto Convert2Dto(PatientVisitHistory patientVisitHistory)
        //{
        //    PatientVisitHistoryDto r = new PatientVisitHistoryDto();
        //    //
        //    //            foreach (Diagnosis d in patientVisitHistory.Diagnosises)
        //    //            {
        //    //                DiagnosisDto dto = Diagnosis.Convert2Dto(d);
        //    //                dto.Category = d.Category;
        //    //                r.Diagnoses.Add(dto);
        //    //            }

        //    //            foreach (Procedure p in patientVisitHistory.Procedures)
        //    //                r.Procedures.Add(Procedure.Convert2Dto(p));

        //    /*
        //                foreach (UsedAuthorization auth in patientVisitHistory.UsedAuthorizations)
        //                    r.UsedAuthorizations.Add(UsedAuthorization.Convert2Dto(auth));
        //    */

        //    foreach (AppointmentResourcePhysician provider in patientVisitHistory.Providers)
        //        r.Providers.Add((AppointmentResourcePhysicianDto)AppointmentResourcePhysician.Convert2Dto(provider));

        //    //            if (patientVisitHistory.Referrals != null)
        //    //                foreach (Referral referral in patientVisitHistory.Referrals)
        //    //                    r.Referrals.Add(Referral.Convert2Dto(referral));

        //    foreach (var orderReport in patientVisitHistory.OrderReports)
        //    {
        //        r.OrderReports.Add(OrderReport.Convert2Dto(orderReport));
        //    }

        //    r.PatientVisitHistoryID = patientVisitHistory.PatientVisitHistoryID;
        //    //            r.FirstName                 = patientVisitHistory.FirstName;
        //    //            r.LastName                  = patientVisitHistory.LastName;
        //    r.EndDate = patientVisitHistory.EndDate;
        //    r.StartDate = patientVisitHistory.StartDate;
        //    r.VisitReason = patientVisitHistory.VisitReason;
        //    r.Status = patientVisitHistory.Status;

        //    r.ModalityId = patientVisitHistory.ModalityId;
        //    r.ModalityName = patientVisitHistory.ModalityName;


        //    r.LocationName = patientVisitHistory.LocationName;
        //    r.LocationId = patientVisitHistory.LocationId;
        //    r.PendingReason = patientVisitHistory.PendingReason;
        //    r.PendingReasonCode = patientVisitHistory.PendingReasonCode;
        //    r.PatientId = patientVisitHistory.PatientIntId;
        //    r.Visit = Visit.Convert2Dto(patientVisitHistory.Visit);
        //    r.Patient = (AppointmentResourcePatientDto)AppointmentResource.Convert2Dto(patientVisitHistory.Patient);
        //    return r;
        //}

        //public static PatientContact ExtractFromDto(PatientContactDto dto)
        //{
        //    PatientContact result = new PatientContact();
        //    result.Id = dto.ID;
        //    result.Address = dto.Address;
        //    result.Address2 = dto.Address2;
        //    result.City = dto.City;
        //    result.Comment = dto.Comment;
        //    result.ContactTypeId = dto.ContactTypeId;
        //    result.ContactTypeName = dto.ContactTypeName;
        //    result.Email = dto.Email;
        //    result.EmployerName = dto.EmployerName;
        //    result.EmployerPhone = dto.EmployerPhone;
        //    result.EmploymentAddress = dto.EmploymentAddress;
        //    result.EmploymentAddress2 = dto.EmploymentAddress2;
        //    result.EmploymentCity = dto.EmploymentCity;
        //    result.EmploymentState = dto.EmploymentState;
        //    result.EmploymentZip = dto.EmploymentZip;
        //    result.Fax = dto.Fax;
        //    result.FirstName = dto.FirstName;
        //    result.LastName = dto.LastName;
        //    result.PatientId = dto.PatientId;
        //    result.Phone = dto.Phone;
        //    result.Mobile = dto.Mobile;
        //    result.RelationshipId = dto.RelationshipId;
        //    result.RelationshipName = dto.RelationshipName;
        //    result.State = dto.State;
        //    result.Zip = dto.Zip;
        //    result.IsAuthorizedDelegate = dto.IsAuthorizedDelegate;
        //    result.CreateDate = dto.CreateDate;
        //    result.ModifyDate = dto.ModifyDate;
        //    return result;
        //}

        //public static PatientContactDto Convert2Dto(PatientContact business)
        //{
        //    PatientContactDto dto = new PatientContactDto();
        //    dto.ID = business.Id;
        //    dto.Address = business.Address;
        //    dto.Address2 = business.Address2;
        //    dto.City = business.City;
        //    dto.Comment = business.Comment;
        //    dto.ContactTypeId = business.ContactTypeId;
        //    dto.ContactTypeName = business.ContactTypeName;
        //    dto.Email = business.Email;
        //    dto.EmployerName = business.EmployerName;
        //    dto.EmployerPhone = business.EmployerPhone;
        //    dto.EmploymentAddress = business.EmploymentAddress;
        //    dto.EmploymentAddress2 = business.EmploymentAddress2;
        //    dto.EmploymentCity = business.EmploymentCity;
        //    dto.EmploymentState = business.EmploymentState;
        //    dto.EmploymentZip = business.EmploymentZip;
        //    dto.Fax = business.Fax;
        //    dto.FirstName = business.FirstName;
        //    dto.LastName = business.LastName;
        //    dto.PatientId = business.PatientId;
        //    dto.Phone = business.Phone;
        //    dto.Mobile = business.Mobile;
        //    dto.RelationshipId = business.RelationshipId;
        //    dto.RelationshipName = business.RelationshipName;
        //    dto.State = business.State;
        //    dto.Zip = business.Zip;
        //    dto.IsAuthorizedDelegate = business.IsAuthorizedDelegate;
        //    dto.CreateDate = business.CreateDate;
        //    dto.ModifyDate = business.ModifyDate;
        //    return dto;
        //}

        //public static PatientAdditionalData ExtractFromDto(PatientAdditionalDataDto dto)
        //{
        //    return new PatientAdditionalData
        //    {
        //        Id = dto.Id,
        //        PatientId = dto.PatientId,
        //        HasBeenPregnant = dto.HasBeenPregnant,
        //        AgeFirstMenstruation = dto.AgeFirstMenstruation,
        //        AgeAtMenopause = dto.AgeAtMenopause,
        //        AgeWhenOvariesRemoved = dto.AgeWhenOvariesRemoved,
        //        MenopauseStatus = dto.MenopauseStatus,
        //        DateOfLastMammogram = dto.DateOfLastMammogram,
        //        FirstScreeningMammography = dto.FirstScreeningMammography,
        //        LocationX = dto.LocationX,
        //        NoOfBirths = dto.NoOfBirths,
        //        AgeAtFirstBirth = dto.AgeAtFirstBirth,
        //        GeneticTestResults = dto.GeneticTestResults
        //    };
        //}

        //public static PatientAdditionalDataDto Convert2Dto(PatientAdditionalData pad)
        //{
        //    return new PatientAdditionalDataDto
        //    {
        //        Id = pad.Id,
        //        PatientId = pad.PatientId,
        //        HasBeenPregnant = pad.HasBeenPregnant,
        //        AgeFirstMenstruation = pad.AgeFirstMenstruation,
        //        AgeAtMenopause = pad.AgeAtMenopause,
        //        AgeWhenOvariesRemoved = pad.AgeWhenOvariesRemoved,
        //        MenopauseStatus = pad.MenopauseStatus,
        //        DateOfLastMammogram = pad.DateOfLastMammogram,
        //        FirstScreeningMammography = pad.FirstScreeningMammography,
        //        LocationX = pad.LocationX,
        //        NoOfBirths = pad.NoOfBirths,
        //        AgeAtFirstBirth = pad.AgeAtFirstBirth,
        //        GeneticTestResults = pad.GeneticTestResults
        //    };
        //}

        //public static PatientFamilyHistoryProblem ExtractFromDto(PatientFamilyHistoryProblemDto dto)
        //{
        //    return new PatientFamilyHistoryProblem
        //    {
        //        PfhpId = dto.PfhpId,
        //        PfmId = dto.PfmId,
        //        AgeDeceased = dto.AgeDeceased,
        //        AgeDiagnosed = dto.AgeDiagnosed,
        //        MedicalProblem = dto.MedicalProblem,
        //        PatientIntId = dto.PatientIntId,
        //        PmplId = dto.PmplId,
        //        Relationship = dto.Relationship,
        //        RelnStatus = dto.RelnStatus,
        //        RelnStatusCode = dto.RelnStatusCode,
        //        RelnTypCode = dto.RelnTypCode,
        //    };
        //}

        //public static PatientFamilyHistoryProblemDto Convert2Dto(PatientFamilyHistoryProblem o)
        //{
        //    return new PatientFamilyHistoryProblemDto
        //    {
        //        PfhpId = o.PfhpId,
        //        PfmId = o.PfmId,
        //        AgeDeceased = o.AgeDeceased,
        //        AgeDiagnosed = o.AgeDiagnosed,
        //        MedicalProblem = o.MedicalProblem,
        //        PatientIntId = o.PatientIntId,
        //        PmplId = o.PmplId,
        //        Relationship = o.Relationship,
        //        RelnStatus = o.RelnStatus,
        //        RelnStatusCode = o.RelnStatusCode,
        //        RelnTypCode = o.RelnTypCode,
        //    };
        //}


        //public static Surgeon ExtractFromDto(SurgeonDto dto)
        //{
        //    Surgeon r = new Surgeon();
        //    r.Id = dto.Id;
        //    r.Name = dto.Name;
        //    return r;
        //}

        //public static SurgeonDto Convert2Dto(Surgeon dto)
        //{
        //    SurgeonDto r = new SurgeonDto();
        //    r.Id = dto.Id;
        //    r.Name = dto.Name;
        //    return r;
        //}


        //public static PathologyPathResultsDto Convert2Dto(PathologyPathResults m)
        //{
        //    PathologyPathResultsDto r = new PathologyPathResultsDto();

        //    r.Id = m.Id;
        //    r.Code = m.Code;
        //    r.Description = m.Description;

        //    return r;
        //}


        //public static PathologyPathResults ExtractFromDto(PathologyPathResultsDto m)
        //{
        //    PathologyPathResults r = new PathologyPathResults();

        //    r.Id = m.Id;
        //    r.Code = m.Code;
        //    r.Description = m.Description;
        //    return r;
        //}



    }
}
