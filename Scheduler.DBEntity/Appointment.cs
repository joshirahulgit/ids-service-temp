using Scheduler.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.DBEntity
{
    public class Appointment : EntityBase
    {
        public String Number { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public List<AppointmentResource> Sources { get; set; }
        public AppointmentStatus Status { get; set; }
        public Account Account { get; set; }
        public Visit Visit { get; set; }
        public bool UseAttachedParamsForOrderCreation { get; set; }
        public string DefaultOrderPriority { get; set; }
        public long? RescheduledApptId { get; set; }
        public string PendingReasonCode { get; set; }
        public string PendingReasonText { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public String Comment { get; set; }
        public String TechName { get; set; }
        public String TechUserId { get; set; }
        public bool IsAuthorizationAlert { get; set; }
        public int? PatientVisitId { get; set; }
        public bool IsLocked { get; set; }
        public string IsLockedBy { get; set; }
        public List<PaymentFee> PaymentFees { get; set; }
        public long BaseAppointmentId { get; set; }
        public long GroupedAppointmentId { get; set; }
        public bool IsAssociationToParentRequired { get; set; }
        public int? GroupId { get; set; }
        public string RequestedTimeRange { get; set; }

        public override string ToString()
        {
            string ret = string.Format("({0}) ", Status.StatusName);
            ret += string.Join("/", Sources.Select(a => a.DisplayText).ToArray());
            return ret;
        }

        public List<OrderCreationValues> OrderValues { get; private set; }


        private long? _lattestAppStatus;
        private long? _locationId;
        public string _worktype;
        private bool _isAuthorizationAlert;

        public Appointment()
        {
            _lattestAppStatus = null;
            this.Sources = new List<AppointmentResource>();
            this.OrderValues = new List<OrderCreationValues>();
            this.AppointmentProcedureWithLocationAlerts = new List<string>();
            DefaultOrderPriority = string.Empty;
        }

        public Appointment(long appointmentId)
        {
            Id = appointmentId;
            this.Sources = new List<AppointmentResource>();
            this.AppointmentProcedureWithLocationAlerts = new List<string>();
            this.Visit = new Visit();
            this.Account = new Account();
            this.PaymentFees = new List<PaymentFee>();
            this.Status = new AppointmentStatus();
        }

        //public void Delete(RepositoryLocator locator)
        //{
        //    //Here we can handle time resources deletion if required
        //    locator.ResourceRepository.RemovePatientCommentsByAppointmentId(this.Account.Id, this.Id);
        //    locator.AppointmentRepository.Remove(this);

        //}

        //public void DeletePending(RepositoryLocator locator)
        //{
        //    locator.AppointmentRepository.RemovePending(this);
        //}

        public void SetComment(string comment)
        {
            Comment = comment;
        }


        public void AttachResource(AppointmentResource resource)
        {
            //Here we can do attached resource validation
            if (resource.Id <= 0)
                throw new SchedulerException(SchedulerExceptionType.Operational, "Attached resource doesn't exists");

            this.Sources.Add(resource);
        }

        //public Appointment UpdateStatusAndSetArrivedForNew(RepositoryLocator locator, AppointmentStatus newStatus)
        //{
        //    _lattestAppStatus = Status.Id;
        //    Status = newStatus;
        //    return Update(locator, this, false);
        //}

        //public Appointment UpdateStatus(RepositoryLocator locator, AppointmentStatus newStatus)
        //{
        //    this._lattestAppStatus = this.Status.Id;
        //    this.Status = newStatus;
        //    return Update(locator, this, true);
        //}

        //public Appointment UpdateStatus(RepositoryLocator locator, AppointmentStatus newStatus, long rescheduledApptId)
        //{
        //    this.RescheduledApptId = rescheduledApptId;
        //    this._lattestAppStatus = this.Status.Id;
        //    this.Status = newStatus;
        //    return Update(locator, this, true);
        //}


        public void SetRescheduledApptId(long rescheduledApptId)
        {
            this.RescheduledApptId = rescheduledApptId;
        }

        //public String UpdateProcedureGlobalId(RepositoryLocator locator, String oldGlobalId, String newGlobalId)
        //{
        //    if (this.Visit == null)
        //        throw new ArgumentNullException("Visit is not initialized");

        //    foreach (Procedure p in this.Visit.Procedures)
        //    {
        //        if (p.GlobalId == oldGlobalId)
        //        {
        //            p.UpdateGlobalId(locator, newGlobalId);
        //        }
        //    }

        //    foreach (AppointmentOrder order in this.Visit.LinkedOrders)
        //    {
        //        if (
        //            order.AppointmentItemId == oldGlobalId &&
        //            order.AppointmentItemType.HasValue &&
        //            order.AppointmentItemType.Value == (int)AppointmentOrderItemType.Procedure)
        //        {
        //            order.UpdateAppointmentItemLinking(locator, newGlobalId, (int)AppointmentOrderItemType.Procedure);
        //        }
        //    }

        //    return newGlobalId;
        //}

        //public void ResetStatus(bool useParams, OrderCreateParametersSetDto dto)
        //{
        //    _lattestAppStatus = null;
        //    this.Status = null;
        //    this.UseAttachedParamsForOrderCreation = useParams;

        //    this.OrderValues.Clear();
        //    foreach (OrderCreateParametersDto parm in dto.Set)
        //        this.OrderValues.Add(OrderCreationValues.ExtractFromDto(parm));
        //}

        //public void RemoveOrder(RepositoryLocator locator, long orderId)
        //{
        //    if (this.Visit == null)
        //        throw new ArgumentNullException("Visit is not initialized");

        //    foreach (AppointmentOrder order in this.Visit.LinkedOrders)
        //    {
        //        if (order.Id == orderId)
        //        {
        //            order.Remove(locator);
        //        }
        //    }
        //    this.Visit.LinkedOrders.RemoveAll(o => o.Id == orderId);
        //}

        //public String UpdateDiagnosisGlobalId(RepositoryLocator locator, String oldGlobalId, String newGlobalId)
        //{
        //    if (this.Visit == null)
        //        throw new ArgumentNullException("Visit is not initialized");


        //    foreach (Diagnosis d in this.Visit.Diagnosises)
        //    {
        //        if (d.GlobalId == oldGlobalId)
        //        {
        //            d.UpdateGlobalId(locator, newGlobalId);
        //        }
        //    }

        //    foreach (AppointmentOrder order in this.Visit.LinkedOrders)
        //    {
        //        if (
        //            order.AppointmentItemId == oldGlobalId &&
        //            order.AppointmentItemType.HasValue &&
        //            order.AppointmentItemType.Value == (int)AppointmentOrderItemType.Diagnosis)
        //        {
        //            order.UpdateAppointmentItemLinking(locator, newGlobalId, (int)AppointmentOrderItemType.Diagnosis);
        //        }
        //    }

        //    return newGlobalId;
        //}


        //private void ClearUnassignedProcedures(RepositoryLocator locator)
        //{
        //    if (this.Visit == null || this.Visit.Procedures == null)
        //        return;

        //    List<AppointmentResourceModality> currentRooms = this.Rooms;
        //    List<Procedure> procForRemoving = new List<Procedure>();

        //    foreach (Procedure p in this.Visit.Procedures)
        //    {                                   //request apps
        //        if (p.LinkedApptId.HasValue && p.LinkedApptId.Value >= 0 && (p.LinkedApptId.Value != this.Id && this.Id > 0))
        //        {
        //            procForRemoving.Add(p);
        //            continue;
        //        }

        //        if (!p.LinkedRoomId.HasValue)
        //            continue;

        //        if (currentRooms.Where(r => r.Id == p.LinkedRoomId.Value).FirstOrDefault() == null)
        //        {
        //            p.BindToRoom(currentRooms.FirstOrDefault());
        //            //procForRemoving.Add(p);
        //        }
        //    }

        //    foreach (Procedure p in procForRemoving)
        //    {
        //        this.Visit.Procedures.Remove(p);

        //        //                IEnumerable<AppointmentOrder> orders = 
        //        //                            this.Visit.LinkedOrders.Where(o => o.AppointmentItemId == p.GlobalId && o.AppointmentItemType == (long)(long)AppointmentOrderItemType.Procedure);
        //        //
        //        //                foreach (AppointmentOrder ao in orders)
        //        //                    locator.AccountRepository.RemoveOrder(ao.Id);
        //        //
        //        //                this.Visit.LinkedOrders.RemoveAll(o => o.AppointmentItemId == p.GlobalId && o.AppointmentItemType == (long)(long)AppointmentOrderItemType.Procedure);
        //    }
        //}

        //public Appointment Update(RepositoryLocator locator, Appointment updatedAppointment, bool setArrivedForNew = false)
        //{
        //    if (this.Id != updatedAppointment.Id)
        //        throw new SchedulerException(SchedulerExceptionType.Operational, "Appointment update failed");

        //    if (this.BusyTime.AppointmentRangeSeconds < 1 && this.Status?.Id != (long)AppointmentStatuses.Pending)
        //        throw new SchedulerException(SchedulerExceptionType.Operational,
        //            "Specified reservation span is less than 1 second");

        //    //Here we can do update validation
        //    this.Account = new Account(updatedAppointment.Account.Id);
        //    this.Description = updatedAppointment.Description;
        //    this.Name = updatedAppointment.Name;
        //    this.DefaultOrderPriority = updatedAppointment.DefaultOrderPriority;
        //    this.Number = updatedAppointment.Number;
        //    this.CreateBy = updatedAppointment.CreateBy;
        //    this.CreatedOn = updatedAppointment.CreatedOn;
        //    this.LastModifiedBy = updatedAppointment.LastModifiedBy;
        //    this.LastModifiedOn = updatedAppointment.LastModifiedOn;
        //    this.PendingReasonCode = updatedAppointment.PendingReasonCode;
        //    this.PendingReasonText = updatedAppointment.PendingReasonText;
        //    this.UseAttachedParamsForOrderCreation = updatedAppointment.UseAttachedParamsForOrderCreation;
        //    List<OrderCreationValues> tmp = new List<OrderCreationValues>(updatedAppointment.OrderValues);
        //    this.OrderValues.Clear();
        //    this.OrderValues.AddRange(tmp);

        //    List<AppointmentResource> newResources = new List<AppointmentResource>();
        //    foreach (AppointmentResource r in updatedAppointment.Sources)
        //        newResources.Add(r);


        //    //Here we need to cache all removed rooms and in order 
        //    //to remove procedures associated to these rooms
        //    //List<long> removedRooms = new List<long>();
        //    //foreach (AppointmentResourceModality modality in this.Rooms)
        //    //{
        //    //    if (newResources.Where(r => r.ResourceType.Id == (long)ResourceTypes.Room && r.Id == modality.Id).FirstOrDefault() == null)
        //    //        removedRooms.Add(modality.Id);
        //    //}

        //    this.Sources.Clear();

        //    foreach (AppointmentResource r in newResources)
        //    {
        //        if (r.Id <= 0)
        //        {
        //            this.Sources.Add(locator.ResourceRepository.Create(r).Clear());
        //        }
        //        else
        //            this.Sources.Add(r);
        //        //.Clear() Grety mod. 2013-01-24. this prevented phys.TypeId identification at order creation
        //    }

        //    if (!this._lattestAppStatus.HasValue && this.Status != null)
        //    {
        //        this._lattestAppStatus = this.Status.Id;
        //    }

        //    this.Status = new AppointmentStatus(updatedAppointment.Status.Id);

        //    if (Visit != null && updatedAppointment.Visit != null)
        //    {
        //        if (updatedAppointment.Visit.Mammography != null && !updatedAppointment.Visit.Mammography.IsEmpty)
        //        {
        //            if (Visit.Mammography == null || Visit.Mammography.Id == 0)
        //            {
        //                //                        if ()
        //                {
        //                    updatedAppointment.Visit.Mammography.BindToAppointment(this.Id);
        //                    updatedAppointment.Visit.Mammography.Create(locator);
        //                }
        //            }
        //            else
        //            {
        //                if (Visit.Mammography.DiffersFrom(updatedAppointment.Visit.Mammography))
        //                {
        //                    updatedAppointment.Visit.Mammography.BindToAppointment(this.Id);
        //                    Visit.Mammography.Update(locator, updatedAppointment.Visit.Mammography);
        //                }
        //            }
        //        }
        //    }
        //    this.Visit = updatedAppointment.Visit;

        //    UpdateUsedAuthorizations(locator);

        //    ClearUnassignedProcedures(locator);
        //    //if (this.Visit.Procedures != null && this.Visit.Procedures.Count > 0)
        //    //{
        //    //    //Here we need to remove procedures assigned to removed rooms if such exist.
        //    //    foreach (long removedRoom in removedRooms)
        //    //    {
        //    //        this.Visit.Procedures.RemoveAll(p => p.LinkedRoomId.HasValue && p.LinkedRoomId.Value == removedRoom);
        //    //    }
        //    //}

        //    CheckResources();
        //    GenerateProceduredId();

        //    CheckForCheckListItem(locator);

        //    locator.AppointmentRepository.Update(this);

        //    if (Visit != null)
        //        UpdateAttachedGuarantors(locator, Visit.AttachedGuarantors, this.Id);

        //    if (Visit != null && Visit.Procedures != null && Visit.Procedures.Count > 0)
        //    {
        //        foreach (Procedure procedure in Visit.Procedures)
        //        {
        //            //                    locator.AuditRepository.Create(new AuditEntry(procedure.Id.ToString()
        //            //                    string.Format("Procedure updated. OrderId = {0}, Priority changed to {1}.", order.OrderId, order.Priority)
        //            //                                                , order.OrderId,
        //            //                                                AuditEntityNameEnum.Order.ToString(),
        //            //                                                AuditActionTypeEnum.Update.ToString()));
        //        }
        //    }

        //    Account account = ReadAccountSettings(locator);
        //    OnAppointmentStatusChanged(locator, account, this._lattestAppStatus, this.Status.Id);

        //    locator.AppointmentRepository.DoOrderUpdates(this);

        //    //            setArrivedForNew &= account.AccountSettings.UpdateAllNewToArrive;

        //    if (LocationId.HasValue && PatientID.HasValue && StartTime.HasValue)
        //    {
        //        int? statusAutoChange = locator.AppointmentRepository.GetSpeficifactionForAutoStatus(LocationId.Value);
        //        if (setArrivedForNew && statusAutoChange.HasValue && Status.Id == /*(long)AppointmentStatuses.Arrived*/statusAutoChange)
        //        {
        //            List<Appointment> appointmentsToUpdate = locator.AppointmentRepository.GetAllNewForArrive(Id, LocationId.Value, PatientID.Value, StartTime.Value);
        //            foreach (Appointment app in appointmentsToUpdate)
        //            {
        //                app.LoadModalityResources(locator);
        //                app.UpdateStatusAndSetArrivedForNew(locator, Status);
        //            }
        //        }
        //    }
        //    return this;
        //}

        //private void CheckForCheckListItem(RepositoryLocator locator)
        //{
        //    if (this.Visit?.AppointmentCheckListItems?.Count > 0)
        //    {
        //        if (this.Visit.AppointmentCheckListItems.Any(a => a.Id < 0) && this.Id > 0)
        //        {
        //            var prevCheckList = locator.ResourceRepository.GetAppoinmentCheckListValues(this.Id);
        //            if (prevCheckList?.Count > 0 && prevCheckList.All(a => a.Id != 0))
        //            {
        //                this.Visit.SetAppointmentCheckListItems(prevCheckList);
        //            }
        //        }
        //    }
        //}

        //private void LoadModalityResources(RepositoryLocator locator)
        //{
        //    Dictionary<int, AppointmentResource> resourcesForUpdate = new Dictionary<int, AppointmentResource>();
        //    foreach (AppointmentResource ar in Sources.Where(r => r.ResourceType != null && r.ResourceType.Id == (long)ResourceTypes.Room))
        //    {
        //        ResourceTypes rType = (ResourceTypes)ar.ResourceType.Id;
        //        AppointmentResource r = locator.ResourceRepository.GetById(new ResourceID(ar.Id, rType));
        //        if (r != null)
        //        {
        //            int idx = Sources.IndexOf(ar);
        //            resourcesForUpdate.Add(idx, r);
        //        }
        //    }
        //    foreach (var item in resourcesForUpdate)
        //    {
        //        Sources[item.Key] = item.Value;
        //    }
        //}

        //public void SendEmailInviteOnlineAppointment(RepositoryLocator locator)
        //{
        //    //todo:Sunil - add email notification of appointment
        //    if (Visit == null || !Visit.HasOnlineAppointment) return;
        //    if (Visit.OnlineAppointmentId == null || StartTime == null || PatientID == null) return;
        //    var patient =
        //        locator.ResourceRepository.GetById(new ResourceID((long)PatientID, ResourceTypes.Patient)) as
        //            AppointmentResourcePatient;
        //    (new External.PatientVisitEhr(locator)).SendEmailInviteOnlineAppointment(Visit.OnlineAppointmentId.Value,
        //        StartTime.Value, patient,
        //        Visit.VisitReason);
        //}

        //private void UpdateUsedAuthorizations(RepositoryLocator locator)
        //{
        //    if (this.Visit != null)
        //    {
        //        List<UsedAuthorization> copy = new List<UsedAuthorization>(Visit.UsedAuthorizations);
        //        foreach (UsedAuthorization usedAuthorization in copy)
        //        {
        //            AppointmentResourcePatient patient = null;
        //            switch (usedAuthorization.EntityStatus)
        //            {
        //                case EntityStatus.Added:
        //                    PatientAuthorization business =
        //                        locator.ResourceRepository.GetPatientAuthById(usedAuthorization.PatientAuthorization.Id);
        //                    if (business == null) throw new SchedulerException(SchedulerExceptionType.AuthorizationDoesntExist);
        //                    usedAuthorization.Id = business.Use(locator, Id, usedAuthorization.Used, usedAuthorization.Comment);

        //                    patient = locator.ResourceRepository.GetById(new ResourceID(usedAuthorization.PatientAuthorization.PatientId,
        //                                                                      ResourceTypes.Patient)) as AppointmentResourcePatient;
        //                    locator.AuditRepository.Create(new AuditEntry(Id,
        //                                                                  string.Format(
        //                                                                      "Use authorization code {0} cpt = \"{1}\" for patient ({2}, MRN={3})",
        //                                                                      usedAuthorization.PatientAuthorization
        //                                                                                       .AuthorizationNumber,
        //                                                                      usedAuthorization.PatientAuthorization
        //                                                                                       .ProcedureId,
        //                                                                      patient.FirstName + " " + patient.LastName,
        //                                                                      patient.RecordNumber
        //                                                                      ), usedAuthorization.Id.ToString(), AuditEntityNameEnum.Authorization.ToString(), AuditActionTypeEnum.Attach.ToString()));
        //                    break;
        //                case EntityStatus.Deleted:
        //                    usedAuthorization.Unuse(locator);
        //                    //usedAuthorization.PatientAuthorization.PatientId
        //                    patient = locator.ResourceRepository.GetById(new ResourceID(usedAuthorization.PatientAuthorization.PatientId,
        //                                                                      ResourceTypes.Patient)) as AppointmentResourcePatient;
        //                    locator.AuditRepository.Create(new AuditEntry(Id, string.Format("Unuse authorization code {0} cpt = \"{1}\" for patient ({2}, MRN={3})",
        //                                                                usedAuthorization.PatientAuthorization.AuthorizationNumber,
        //                                                                usedAuthorization.PatientAuthorization.ProcedureId,
        //                                                                patient.FirstName + " " + patient.LastName,
        //                                                                patient.RecordNumber
        //                                                            ), usedAuthorization.Id.ToString(), AuditEntityNameEnum.Authorization.ToString(), AuditActionTypeEnum.Detach.ToString()));

        //                    this.Visit.UsedAuthorizations.Remove(usedAuthorization);
        //                    break;
        //                case EntityStatus.Modified:
        //                    throw new SchedulerException(SchedulerExceptionType.AuthorizationActivation,
        //                                                 "Modification of used authorization is not supported.");

        //            }
        //        }
        //    }
        //}

        //public static List<SchedulerWarning> ShiftOrders(RepositoryLocator locator, Appointment sourceAppointment, Appointment newAppointment)
        //{
        //    List<SchedulerWarning> resultWarnings = new List<SchedulerWarning>();
        //    if (sourceAppointment == null || sourceAppointment.Visit == null)
        //        throw new ArgumentNullException("sourceAppointment");

        //    if (newAppointment == null || newAppointment.Visit == null)
        //        throw new ArgumentNullException("newAppointment");

        //    //Here we need to cancel or re-assign already created orders
        //    //Grety mod 2012-10-04. Check orders. If any order is unbound (AppointmentItemId refers to non-existing already item, then it will be attached to appt directly
        //    foreach (AppointmentOrder order in sourceAppointment.Visit.LinkedOrders)
        //    {
        //        if (order.AppointmentItemType.HasValue)
        //        {
        //            bool orderRemapped = false;
        //            switch (order.AppointmentItemType.Value)
        //            {
        //                case (long)AppointmentOrderItemType.Diagnosis:
        //                    {
        //                        foreach (Diagnosis d in newAppointment.Visit.Diagnosises)
        //                        {
        //                            if (d.GlobalId == order.AppointmentItemId)
        //                            {
        //                                locator.AccountRepository.ChangeOrderMapping(order.Id, newAppointment.Id, (long)AppointmentOrderItemType.Diagnosis, d.GlobalId);

        //                                orderRemapped = true;
        //                                //Here we need to assign order to 
        //                                break;
        //                            }
        //                        }
        //                    }
        //                    break;
        //                case (long)AppointmentOrderItemType.Procedure:
        //                    {
        //                        var procedure = newAppointment.Visit.Procedures.FirstOrDefault(p => p.GlobalId == order.AppointmentItemId);
        //                        if (procedure != null)//if procedure item remained (GlobalId may be same, but Cpt may have changed) we update order mapping
        //                        {
        //                            locator.AccountRepository.ChangeOrderMapping(order.Id, newAppointment.Id, (long)AppointmentOrderItemType.Procedure, procedure.GlobalId);
        //                            locator.AccountRepository.UpdateOrderForProcedure(order.Id, newAppointment.Id, procedure);
        //                            orderRemapped = true;
        //                        }
        //                        else//if procedure was deleted
        //                        {
        //                            procedure = sourceAppointment.Visit.Procedures.FirstOrDefault(p => p.GlobalId == order.AppointmentItemId);
        //                            AppointmentResourceModality room;
        //                            if (procedure != null)// try to find room through the procedure in the old appointment
        //                                room = newAppointment.Rooms.FirstOrDefault(r => r.Id == procedure.LinkedRoomId);
        //                            else//if failed through procedure, attempt first available room in appointment
        //                                room = newAppointment.Rooms.FirstOrDefault();
        //                            if (room != null)
        //                            {
        //                                locator.AccountRepository.ChangeOrderMapping(order.Id, newAppointment.Id, (long)AppointmentOrderItemType.Room, room.Id.ToString());
        //                                order.MapToRoom(newAppointment.Id, room);
        //                                //locator.AccountRepository.UpdateOrderForRoom(order.Id, newAppointment.Id, modality);
        //                                orderRemapped = true;
        //                            }
        //                            //                                    else // if procedure wasn't bound to a room map to appointment
        //                            //                                        order.MapToAppointment(newAppointment);
        //                        }
        //                    }
        //                    break;
        //                case (long)AppointmentOrderItemType.Room:
        //                    {
        //                        var room = newAppointment.Rooms.FirstOrDefault(r => r.Id.ToString() == order.AppointmentItemId);
        //                        if (room != null)
        //                        {//check if room (to which order is bound) exists
        //                            var procedure = newAppointment.Visit.Procedures.FirstOrDefault(p => p.LinkedRoomId == room.Id);
        //                            if (procedure != null)//if there appeared a procedure bound to the room, we swithc order mapping to this procedure
        //                            {
        //                                locator.AccountRepository.ChangeOrderMapping(order.Id, newAppointment.Id, (long)AppointmentOrderItemType.Procedure, procedure.GlobalId);
        //                                order.MapToProcedure(newAppointment.Id, procedure);
        //                                locator.AccountRepository.UpdateOrderForProcedure(order.Id, newAppointment.Id, procedure);
        //                                orderRemapped = true;
        //                            }
        //                            else
        //                            {//if procedure didn't exist, we just update order mapping to room
        //                                locator.AccountRepository.ChangeOrderMapping(order.Id, newAppointment.Id, (long)AppointmentOrderItemType.Room, room.Id.ToString());
        //                                //locator.AccountRepository.UpdateOrderForRoom(order.Id, newAppointment.Id, modality);
        //                                orderRemapped = true;
        //                            }
        //                        }
        //                        else//room was removed from appt
        //                        {
        //                            if (newAppointment.Rooms.Count == 1)
        //                            {//attempt to bind to the only available room
        //                                room = newAppointment.Rooms[0];
        //                                locator.AccountRepository.ChangeOrderMapping(order.Id, newAppointment.Id, (long)AppointmentOrderItemType.Room, room.Id.ToString());
        //                                order.MapToRoom(newAppointment.Id, room);
        //                                locator.AccountRepository.UpdateOrderForRoom(order.Id, newAppointment.Id, room);
        //                                orderRemapped = true;
        //                            }
        //                            //                                    else //otherwise cannot remap automatically
        //                            //                                        resultWarnings.Add(new SchedulerWarning(SchedulerWarningType.ManualOrderManagementIsRequired, "Manual order management is required"));
        //                        }
        //                    }
        //                    break;
        //            }
        //            if (!orderRemapped)
        //            {
        //                order.MapToAppointment(newAppointment);
        //                locator.AccountRepository.ChangeOrderMapping(order.Id, newAppointment.Id, null, "");
        //                resultWarnings.Add(new SchedulerWarning(SchedulerWarningType.ManualOrderManagementIsRequired, "Manual order management is required"));
        //                //locator.AccountRepository.RemoveOrder(order.Id);
        //            }
        //        }
        //        else
        //        {
        //            //Order is assigned to appointment itself
        //            locator.AccountRepository.ChangeOrderMapping(order.Id, newAppointment.Id, null, null);
        //        }
        //    }
        //    return resultWarnings;
        //}


        //public static List<Appointment> FindAppointments(RepositoryLocator locator, List<AppointmentResource> requestResources)
        //{
        //    throw new NotImplementedException();
        //}

        private void CheckResources()
        {
            if (this.Sources.Count(s => s.ResourceType.Id == (long)ResourceTypes.Patient) > 1)
                throw new SchedulerException(SchedulerExceptionType.AppointmentConfigurationException, "More then one patient is associated with appointment");

            List<KeyValuePair<long, long>> existingResources = new List<KeyValuePair<long, long>>();

            foreach (AppointmentResource r in this.Sources)
            {
                if (existingResources.Contains(new KeyValuePair<long, long>(r.Id, r.ResourceType.Id)))
                    throw new SchedulerException(SchedulerExceptionType.ResourceDublicatedException, "The same resource is attached to the appointment more than one time");

                existingResources.Add(new KeyValuePair<long, long>(r.Id, r.ResourceType.Id));
            }

            existingResources.Clear();
        }

        //public Appointment Create(RepositoryLocator locator)
        //{
        //    if (this.BusyTime == null)
        //        throw new SchedulerException(SchedulerExceptionType.Operational, "Appointment can not be created because appointment time is not specified");

        //    if (this.BusyTime.IsSpecified && this.BusyTime.AppointmentRangeSeconds < 1)
        //        throw new SchedulerException(SchedulerExceptionType.Operational, "Specified reservation span is less then 1 second");

        //    //Generate Resource Ids and save new resources where it is not defined (especially for time)
        //    List<long> resourceIds = new List<long>();
        //    foreach (AppointmentResource source in this.Sources)
        //    {
        //        if (source.Id <= 0)
        //            resourceIds.Add(source.Create(locator).Id);
        //        else
        //            resourceIds.Add(source.Id);
        //    }

        //    ClearUnassignedProcedures(locator);
        //    CheckResources();
        //    GenerateProceduredId();
        //    IncludeProceduresOverhead();

        //    Appointment result = locator.AppointmentRepository.Create(this);

        //    List<AppointmentAutoCreateParams> autoCreateParameters = new List<AppointmentAutoCreateParams>(); //locator.AppointmentRepository.FindAutoCreateParametersForAppointment(result);
        //    List<Appointment> appToCreate = new List<Appointment>();

        //    foreach (AppointmentAutoCreateParams parameter in autoCreateParameters)

        //    {
        //        AppointmentDto appdto = Appointment.Convert2Dto(result);
        //        Appointment app = Appointment.ExtractFromDto(appdto);

        //        app.Visit.Procedures.Clear();

        //        app.SetLocation(parameter.LocationId);

        //        AppointmentResourceModality m = new AppointmentResourceModality();
        //        m.SetResourceTypeAndId(new AppointmentResourceType((long)ResourceTypes.Room), parameter.ModalityId);
        //        m.SetModalityType(parameter.ModalityTypeId);
        //        Procedure p = new Procedure(0, parameter.ProcedureDescription, parameter.ProcedureCode);
        //        p.SetModalityType((int)parameter.ModalityTypeId);
        //        p.SetLinkedRoomId(parameter.ModalityId);
        //        app.Visit.Procedures.Add(p);

        //        List<AppointmentResourceModality> modalityTypes = app.Sources.OfType<AppointmentResourceModality>().ToList();
        //        foreach (AppointmentResourceModality modality in modalityTypes)
        //            app.Sources.Remove(modality);
        //        app.Sources.Add(m);

        //        int appDuration = parameter.TimeAdjust + parameter.ModalityDefaultDuration + parameter.ProcedureDefaultOverhead;

        //        app.SetDateInfo(app.EndTime.Value, app.EndTime.Value.AddMinutes(appDuration));
        //        appToCreate.Add(app);
        //    }
        //    foreach (Appointment appointment in appToCreate)
        //        appointment.Create(locator);

        //    if (Visit != null && Visit.Mammography != null && !Visit.Mammography.IsEmpty)
        //    {
        //        Visit.Mammography.BindToAppointment(this.Id);
        //        Visit.Mammography.Create(locator);
        //    }

        //    if (this.Status != null &&
        //            (this.Status.Id == (long)AppointmentStatuses.Rescheduled ||
        //             (this.Status.Id == (long)AppointmentStatuses.New && this.Visit.Id >= 0)))
        //    {
        //        Account account = ReadAccountSettings(locator);
        //        OnAppointmentStatusChanged(locator, account, null, this.Status.Id);
        //    }
        //    UpdateProcedureLinkage();
        //    foreach (UsedAuthorization usedAuth in this.Visit.UsedAuthorizations)
        //    {
        //        usedAuth.AppointmentId = result.Id;
        //    }
        //    if (this.Visit != null)
        //        UpdateAttachedGuarantors(locator, this.Visit.AttachedGuarantors, result.Id);
        //    UpdateUsedAuthorizations(locator);

        //    locator.AppointmentRepository.DoOrderUpdates(this);

        //    if (BaseAppointmentId > 0 && IsAssociationToParentRequired)
        //        locator.AppointmentRepository.CreateLinkedAppointmentEntry(this);
        //    if (GroupedAppointmentId > 0)
        //        locator.AppointmentRepository.CreateGroupedExams(new List<Appointment>() { this, new Appointment(GroupedAppointmentId) });

        //    return result;
        //}

        //public static void UpdateAttachedGuarantors(RepositoryLocator locator, List<long> guarantorIds, long apptId)
        //{
        //    var existingGuarantors = locator.ResourceRepository.GetAttachedGuarantors(apptId);
        //    foreach (long guarantorId in existingGuarantors.Where(g => !guarantorIds.Contains(g)))
        //    {
        //        locator.ResourceRepository.DetachGuarantorFromAppointment(guarantorId, apptId);
        //    }
        //    foreach (long guarantorId in guarantorIds.Where(g => !existingGuarantors.Contains(g)))
        //    {
        //        locator.ResourceRepository.AttachGuarantorToAppointment(guarantorId, apptId);
        //    }
        //}

        private void UpdateProcedureLinkage()
        {
            if (this.Visit == null)
                return;

            if (this.Visit.Procedures != null)
            {
                foreach (Procedure p in this.Visit.Procedures)
                {
                    p.BindToAppointment(this.Id);
                }
            }
        }

        //private Account ReadAccountSettings(RepositoryLocator locator)
        //{
        //    Account account = Services.AccountService.CheckForAccountInCache(locator, this.Account.Id);
        //    if (account.AccountSettings == null || !account.AccountSettings.Items.Any())
        //    {
        //        account.LoadAccountSettings(new AccountSettingCollection() { Items = locator.AccountSettingRepository.GetAll() });
        //    }
        //    return account;
        //}

        //private void OnAppointmentStatusChanged(RepositoryLocator locator, Account account, long? lastStatusId, long newStatusId)
        //{
        //    //            Account account = locator.AccountRepository.GetById(this.Account.Id);

        //    //            if (lastStatusId.HasValue && lastStatusId != newStatusId && (lastStatusId == (long)AppointmentStatuses.Blocked) && this.PatientID == null)
        //    //                throw new SchedulerException(SchedulerExceptionType.PatientDoesnotExists, "Cannot unblock an appointment without a patient");

        //    //Check if we need to update the paitent visit.
        //    bool createOrder, createEncounter;

        //    createEncounter = this.Sources.OfType<AppointmentResourceModality>().Any(p => p.CreateEncounter);
        //    createOrder = this.Sources.OfType<AppointmentResourceModality>().Any(p => p.CreateOrder);

        //    if (CheckIfPatientVisitKeepSyncIsRequired(account, lastStatusId, newStatusId) && createEncounter)
        //        ManagePatientVisits(locator, false);

        //    if (CheckIfOrdersAutoCreationIsRequired(account, lastStatusId, newStatusId) && createOrder)
        //    {
        //        if (account.OrderCreationParameters.Count == 0)
        //            throw new SchedulerException(SchedulerExceptionType.NotSupportedConfiguration, "Order creation is not configured for this account");

        //        SuccessfullyCreatedOrders = CreateOrders(locator, account, account.OrderCreationParameters);
        //    }

        //    List<Procedure> proctoCreateTheOrder = new List<Procedure>();
        //    foreach (Procedure procedure in Visit.Procedures)
        //    {
        //        if (procedure.IsOrderRequired)
        //        {
        //            if (Visit.LinkedOrders.FirstOrDefault(p => p.CPTCode == procedure.Code) == null)
        //            {
        //                proctoCreateTheOrder.Add(procedure);
        //            }
        //        }
        //    }

        //    if (proctoCreateTheOrder.Count > 0)
        //    {
        //        List<OrderCreationValues> result = new List<OrderCreationValues>();
        //        CreateOrderForProcedures(locator, account.Name, account.OrderCreationParameters, result, proctoCreateTheOrder, this.Worktype2);
        //    }

        //    if (lastStatusId.HasValue && lastStatusId != newStatusId)
        //    {
        //        if ((newStatusId == (long)AppointmentStatuses.Cancel || newStatusId == (long)AppointmentStatuses.NoShow) && this.Visit != null && account.AccountSettings.RemoveOrdersOnCancelStatus)
        //        {
        //            List<AppointmentOrder> allApptOrders = this.Visit.LinkedOrders.ToList();
        //            foreach (AppointmentOrder order in allApptOrders)
        //            {
        //                this.RemoveOrder(locator, order.Id);
        //            }
        //        }
        //        if (account.AccountSettings.ConsentDateUpdateTriggeredOnStatus == newStatusId)
        //        {
        //            var patient = this.Sources.OfType<AppointmentResourcePatient>().FirstOrDefault();
        //            locator.ResourceRepository.LoadPatientResources(patient);
        //            if (patient.ConsentForm == null || patient.ConsentForm < this.EndTime)
        //            {
        //                locator.AuditRepository.Create(AuditEntry.CreateAuditEntryItem(Appointment.Convert2Dto(this), patient.Id.ToString(),
        //                                                          AuditActionTypeEnum.Update.ToString(),
        //                                                          AuditEntityNameEnum.Patient.ToString(),
        //                                                          string.Format("Consent date changed from {0} to {1}", patient.ConsentForm == null ? "null" : patient.ConsentForm.ToString(), this.EndTime),
        //                                                          ""));
        //                patient.SetConsentForm(this.EndTime);
        //                locator.ResourceRepository.Update(patient);
        //            }
        //        }
        //    }
        //    locator.AppointmentRepository.UpdateRppStatus(Id, newStatusId);
        //}

        //public void ManagePatientVisits(RepositoryLocator locator, bool checkRequired)
        //{
        //    if (checkRequired)
        //    {
        //        bool visitExists = locator.AccountRepository.CheckIfPaitentVivistExists(this.Id);

        //        if (visitExists)
        //            ProceedPaitentVisitCreation(locator);

        //    }
        //    else
        //    {
        //        ProceedPaitentVisitCreation(locator);
        //    }
        //}

        //private void ProceedPaitentVisitCreation(RepositoryLocator locator)
        //{
        //    int patientVisitId;
        //    if (CheckForExistingAppointmentVisit(locator, Id, out patientVisitId)) // update existing visit
        //    {
        //        UpdateEhrMaritalStatus(locator, patientVisitId);
        //        UpdateEhrAdditionalProperties(locator, patientVisitId);
        //        SaveSmokingToEhrTables(locator, this);
        //        UpdatePatientVisit(locator, patientVisitId);
        //        //patient visit exists, let's update procedures and diagnoses

        //        //quick update - delete previous, add new
        //        RemoveExistingCptsInPatientVisit(locator, patientVisitId);
        //        /*
        //                        List<Procedure> patientVisitProcedures =
        //                            locator.AccountRepository.GetPatientVisitProceduresBytVisitId(patientVisitId);

        //                        List<Procedure> procsToAdd =( from i in Visit.Procedures
        //                            where !(patientVisitProcedures.Any(p => p.Code == i.Code))
        //                            select i).ToList();

        //                        List<Diagnosis> patientVisitDiagnoses =
        //                            locator.AccountRepository.GetPatientVisitDiagnosesBytVisitId(patientVisitId);

        //                        List<Diagnosis> diagsToAdd = (from i in Visit.Diagnosises
        //                                                      where !(patientVisitDiagnoses.Any(p => p.Code == i.Code))
        //                                                      select i).ToList();*/

        //        foreach (Procedure procedure in Visit.Procedures)
        //        {
        //            AttachProcedureToPatientVisit(locator, patientVisitId, procedure);

        //            //create audit
        //            locator.AuditRepository.Create(new AuditEntry((int)Id, "Update procedure in the patient visit", patientVisitId.ToString(),
        //                AuditEntityNameEnum.Procedure.ToString(), AuditActionTypeEnum.Update.ToString()));
        //        }

        //        foreach (Diagnosis diagnosis in Visit.Diagnosises)
        //        {
        //            AttachDiagnosisToPatientVisit(locator, patientVisitId, diagnosis);

        //            // create audit
        //            locator.AuditRepository.Create(new AuditEntry((int)Id, "Update diagnosis in the patient visit", patientVisitId.ToString(),
        //                AuditEntityNameEnum.Diagnosis.ToString(), AuditActionTypeEnum.Update.ToString()));
        //        }

        //        UpdateVitalsToPatientVisit(locator, patientVisitId, Visit);
        //    }
        //    else // create new visit
        //    {
        //        patientVisitId = CreatePatientVisit(locator);
        //        if (patientVisitId != 0) // here we need to mark visit as scheduler created
        //        {
        //            foreach (Diagnosis diagnosis in this.Visit.Diagnosises)
        //            {
        //                int id = AttachDiagnosisToPatientVisit(locator, patientVisitId, diagnosis);
        //                //create audit
        //                locator.AuditRepository.Create(new AuditEntry((int)Id, "Attach diagnosis to the patient visit", patientVisitId.ToString(),
        //                    AuditEntityNameEnum.Procedure.ToString(), AuditActionTypeEnum.Create.ToString()));
        //            }

        //            foreach (Procedure procedure in this.Visit.Procedures)
        //            {
        //                int id = AttachProcedureToPatientVisit(locator, patientVisitId, procedure);

        //                //create audit
        //                locator.AuditRepository.Create(new AuditEntry((int)Id, "Attach procedure to the patient visit", id.ToString(),
        //                    AuditEntityNameEnum.Procedure.ToString(), AuditActionTypeEnum.Create.ToString()));
        //            }
        //            AttachVitalsToPatientVisit(locator, patientVisitId, Visit);
        //            locator.AccountRepository.UpdateEhrChiefComplaint(this.Visit.VisitReason, patientVisitId);
        //            UpdateEhrAdditionalProperties(locator, patientVisitId);
        //            UpdateEhrMaritalStatus(locator, patientVisitId);
        //            MarkVisitAsSchedulerCreated(locator, patientVisitId);
        //            SaveSmokingToEhrTables(locator, this);
        //        }
        //    }
        //}

        //private void InsertEhrBloodPressure(RepositoryLocator locator, int patientVisitId)
        //{
        //    locator.AppointmentRepository.SyncEhrBloodPressureValues(this, patientVisitId);
        //}

        //private void UpdateEhrAdditionalProperties(RepositoryLocator locator, int patientVisitId)
        //{
        //    AppointmentResourcePhysician phys = this.Sources.OfType<AppointmentResourcePhysician>().FirstOrDefault();
        //    locator.AccountRepository.UpdateEhrProvider(phys, patientVisitId);

        //    locator.AccountRepository.UpdateEhrLocation(LocationId, patientVisitId);
        //    locator.AccountRepository.UpdateEhrIsPregnant(this.Visit.IsPregnant, patientVisitId);
        //}

        //private void UpdateEhrMaritalStatus(RepositoryLocator locator, int patientVisitId)
        //{
        //    AppointmentResourcePatient patient = this.Sources.OfType<AppointmentResourcePatient>().FirstOrDefault();
        //    if (patient != null)
        //        if (!string.IsNullOrEmpty(patient.MaritalStatus))
        //            locator.AccountRepository.UpdateEhrMaritalStatus(patient.MaritalStatus == "3", patientVisitId);
        //        else
        //            locator.AccountRepository.UpdateEhrMaritalStatus(false, patientVisitId);
        //}

        //private void SaveSmokingToEhrTables(RepositoryLocator locator, Appointment appointment)
        //{
        //    locator.AppointmentRepository.SaveSmokingToEhrTables(appointment);
        //}

        //private void UpdateVitalsToPatientVisit(RepositoryLocator locator, int patientVisitId, Visit visit)
        //{
        //    locator.AccountRepository.UpdateVitalsToPatientVisit(patientVisitId, visit);
        //}

        //private void AttachVitalsToPatientVisit(RepositoryLocator locator, int patientVisitId, Visit visit)
        //{
        //    locator.AccountRepository.AttachVitalsToPatientVisit(patientVisitId, visit);
        //}

        //private void UpdatePatientVisit(RepositoryLocator locator, int visitId)
        //{
        //    locator.AppointmentRepository.UpdatePatientVisit(this, visitId);
        //}

        //private void RemoveExistingCptsInPatientVisit(RepositoryLocator locator, int patientVisitId)
        //{
        //    locator.AccountRepository.RemoveExistingCptsInPatientVisit(patientVisitId);
        //}

        //private int AttachDiagnosisToPatientVisit(RepositoryLocator locator, int patientVisitId, Diagnosis diagnosis)
        //{
        //    return locator.AccountRepository.AttachDiagnosisToPatientVisit(patientVisitId, diagnosis);
        //}

        //private bool CheckForExistingAppointmentVisit(RepositoryLocator locator, long appId, out int patientVisitId)
        //{
        //    int visitId = locator.AccountRepository.GetPatientVisitIdByAppointmentId(appId);
        //    patientVisitId = visitId;
        //    return patientVisitId > 0;
        //}

        public List<OrderCreationValues> SuccessfullyCreatedOrders { get; private set; }

        /// <summary>
        /// Method returns true in case when account is configured to create orders automatically 
        /// and appointment contains either 1 or more assigned procedure or room.
        /// </summary>
        /// <param name="locator"></param>
        /// <param name="accountId"></param>
        /// <param name="lastStatusId"></param>
        /// <param name="newStatusId"></param>
        /// <returns></returns>
        private bool CheckIfOrdersAutoCreationIsRequired(Account account, long? lastStatusId, long newStatusId)
        {


            //Automatical orders creation must be done only is corresponding mode is selected in account configuration
            if (account.OrderCreationMode == OrderCreationMode.AutoOneOne || account.OrderCreationMode == OrderCreationMode.AutoOneMany)
            {
                //String statusName = Enum.GetName(typeof(AppointmentStatuses), newStatusId);

                if (account.OrderCreationTrigger.Contains((int)newStatusId) || newStatusId == (long)Core.AppointmentStatuses.TechComplete)//2014-04-18. Grety mod. Hotfix to update orders on TechComplete
                //it should be removed when normal worflow will be implemented (keep track of AutocOrder creation event and update orders on order's slots changes after it has happened)
                {
                    //Here we make sure that status was really changed
                    if (lastStatusId.HasValue && lastStatusId.Value == newStatusId)
                        return false;

                    String wtSource = account.WorkTypeSourceTable;

                    if (wtSource.Contains("$G.[CPTCodesFull]") || wtSource.Contains("$A.[CodeReferences]"))
                    {
                        if (this.Visit == null)
                            throw new ArgumentNullException("Appointment configuration issue. Reference to visit is broken");

                        return true;// this.Visit.Procedures.Count > 0 || this.Rooms.Count > 0;
                    }
                    else
                    {
                        throw new SchedulerException(SchedulerExceptionType.NotSupportedConfiguration,
                            String.Format("Specified configuration '{0}' in Global.WorkTypeSourceTable is not supported",
                            wtSource));
                    }
                }
            }
            return false;
        }

        private bool CheckIfPatientVisitKeepSyncIsRequired(Account account, long? lastStatusId,
            long newStatusId)
        {
            if (account.VisitCreationTrigger.Contains((int)newStatusId))
            {
                if (lastStatusId.HasValue && lastStatusId.Value == newStatusId)
                    return false;

                return true;
            }

            return false;
        }

        //public List<OrderCreationValues> CreateOrders(RepositoryLocator locator, Account account, List<OrderCreationParameter> orderParameters)
        //{
        //    bool atLeastOneFailed = false;
        //    List<OrderCreationValues> result = new List<OrderCreationValues>();
        //    OrderCreateParametersSetDto paramsDto = new OrderCreateParametersSetDto();



        //    if (this.UseAttachedParamsForOrderCreation)
        //    {
        //        //In this case extra parameters were passed into service.
        //        //No need to make validation one more time bacause all parameters were already validated in UI 
        //        //(Anycase I'd refactor this functionallity in the future because we can't rely on client validation 
        //        //in server side code)
        //        foreach (OrderCreationValues val in this.OrderValues)
        //        {
        //            try
        //            {
        //                paramsDto.Add(val.Convert2Dto());
        //                string orderId = CreateOrder(locator, val, val.AppointmentItemId, val.AppointmentItemIdentifier, false);
        //                if (val.IsUpdateToMammoRequired)
        //                    locator.MammographyRepository.UpdateMammographyWithOrderId(this.Id, orderId);
        //            }
        //            catch
        //            {
        //                atLeastOneFailed = true;
        //            }
        //        }
        //        if (atLeastOneFailed)
        //        {
        //            //Here we have to reverse whole ATOMic operation
        //            throw new SchedulerException(SchedulerExceptionType.OrderCreationParameterIsNotSupplied, paramsDto);
        //        }
        //        if (OrderValues.Count == 0)
        //        {
        //            //create appointment with canceling automaticaly order creation
        //            locator.AuditRepository.Create(new AuditEntry(Id, string.Format("Automatic order creation was cancelled by user"), String.Empty, AuditEntityNameEnum.Order.ToString(),
        //                                                                            AuditActionTypeEnum.Create.ToString()));
        //        }
        //        return this.OrderValues;
        //    }
        //    // sunil: this is used to determine the ordercreationmode per procedure, to prevent additional cpt's from creating orders
        //    ResolveProcedureOverrideCreationMode(locator);
        //    List<AppointmentResourceModality> rooms = this.Rooms;
        //    //if there are rooms we proc all procedures connected to these rooms
        //    //Per each procedure we create an order
        //    //if there is no proc in room then there is an order to be created per room
        //    List<Procedure> processedProcedures = new List<Procedure>();

        //    foreach (AppointmentResourceModality room in rooms)
        //    {
        //        List<Procedure> procPerRooms = this.Visit.Procedures.Where(a => a.LinkedRoomId == room.Id &&
        //            !a.OverrideCreationMode.HasValue).ToList();
        //        //                    a.IsOrderRequired).ToList();
        //        if (procPerRooms.Count > 0)
        //        {
        //            ModalityType modalityTypeRef = account.ModalityTypes.FirstOrDefault(a => a.Id == room.ModalityType.Id);
        //            string worktype = null;
        //            if (modalityTypeRef != null) worktype = modalityTypeRef.Name;

        //            CreateOrderForProcedures(locator, account.Name, orderParameters, result, procPerRooms, worktype);
        //            foreach (Procedure procPerRoom in procPerRooms)
        //            {
        //                if (!processedProcedures.Contains(procPerRoom))
        //                    processedProcedures.Add(procPerRoom);
        //            }
        //        }
        //        else
        //        {
        //            CreateOrderForRoom(locator, account.Name, orderParameters, result, room);
        //        }
        //    }
        //    List<Procedure> unprocessedProcedures =
        //        this.Visit.Procedures.Where(a => !processedProcedures.Contains(a) && !a.OverrideCreationMode.HasValue).ToList();
        //    //                this.Visit.Procedures.Where(a => !processedProcedures.Contains(a) && a.IsOrderRequired).ToList();
        //    CreateOrderForProcedures(locator, account.Name, orderParameters, result, unprocessedProcedures, this.Worktype);

        //    if (unprocessedProcedures.Count == 0 && processedProcedures.Count == 0 && this.Rooms.Count == 0)
        //    {
        //        OrderCreationValues orderValues = new OrderCreationValues(this.Id, null, null);
        //        //Create appointment level order
        //        foreach (OrderCreationParameter param in orderParameters)
        //        {
        //            try
        //            {
        //                String wtDesc = string.Empty;
        //                if (this.LocationId.HasValue)
        //                    locator.AccountRepository.FindWorkTypeDescription(account.Name,
        //                                    this.Worktype, this.LocationId.Value);
        //                orderValues.Add(param.ResolveValue(this,
        //                                            wtDesc,
        //                                            null,
        //                                            this.Worktype,
        //                                            this.Worktype,
        //                                            account.Name,
        //                                            locator));
        //            }
        //            catch (SchedulerException)
        //            {
        //                orderValues.AddUnresolvedParam(param);
        //            }
        //        }
        //        if (orderValues.ResolvedSuccessfully)
        //        {
        //            CreateOrder(locator, orderValues, null, null, false);
        //        }
        //        result.Add(orderValues);
        //    }

        //    atLeastOneFailed = result.Any(p => !p.ResolvedSuccessfully);
        //    if (atLeastOneFailed)
        //    {
        //        foreach (OrderCreationValues val in result)
        //        {
        //            paramsDto.Add(val.Convert2Dto());
        //        }
        //        //Here we have to reverse whole ATOMic operation
        //        throw new SchedulerException(SchedulerExceptionType.OrderCreationParameterIsNotSupplied, paramsDto);
        //    }


        //    return result;
        //    //}
        //    //catch (Exception e)
        //    //{
        //    //    throw new SchedulerException(
        //    //        SchedulerExceptionType.OrderCreation, 
        //    //        String.Format("Unexpected server exception happened during order creation. {0}",
        //    //        e.Message));
        //    //}
        //}

        //private int AttachProcedureToPatientVisit(RepositoryLocator locator, int patientVisitId, Procedure procedure)
        //{
        //    return locator.AccountRepository.AttachProcedureToPatientVisit(patientVisitId, procedure);
        //}

        //private void MarkVisitAsSchedulerCreated(RepositoryLocator locator, int paitentVisitid)
        //{
        //    locator.AccountRepository.MarkVisitAsSchedulerCreated(Id, paitentVisitid);
        //}

        //private int CreatePatientVisit(RepositoryLocator locator)
        //{
        //    return locator.AccountRepository.CreatePatientVisit(this);
        //}

        //private void ResolveProcedureOverrideCreationMode(RepositoryLocator locator)
        //{
        //    // sunil: original code, seems like it was never used and also has logic error
        //    //if (Visit == null) return;
        //    //foreach (Procedure procedure in Visit.Procedures)
        //    //{
        //    //    List<OrderTransformParameter> transforms = locator.AccountRepository.FindOrderTransformParams(procedure.Code, this.Account.Id);
        //    //    if(transforms != null)
        //    //    {
        //    //        OrderTransformParameter ptrans = transforms.FirstOrDefault(a => a.MapFieldValue == procedure.Code);
        //    //        if (ptrans != null)
        //    //            procedure.OverrideCreationMode = ptrans.OverrideCreationMode;
        //    //    }
        //    //}

        //    // sunil: modified code 11/12/2012 for getting the proper creation mode per proc
        //    if (Visit == null) return;
        //    foreach (Procedure procedure in Visit.Procedures)
        //    {
        //        OrderTransformParameter transforms = locator.AccountRepository.GetOrderTransformParamForProcCode(procedure.Code, this.Account.Id);
        //        if (transforms != null)
        //        {
        //            procedure.OverrideCreationMode = transforms.OverrideCreationMode;
        //        }
        //    }
        //}

        //private void CreateOrderForRoom(RepositoryLocator locator, string accountName, List<OrderCreationParameter> orderParameters, List<OrderCreationValues> result,
        //                                AppointmentResourceModality room)
        //{
        //    if (this.Visit.ContainsOrder(this.Id, (long)AppointmentOrderItemType.Room, room.Id.ToString()))
        //    {
        //        return;
        //    }

        //    AppointmentResourceModality freshRoomRef =
        //        locator.ResourceRepository.GetById(new ResourceID(room.Id, ResourceTypes.Room)) as
        //        AppointmentResourceModality;
        //    if (freshRoomRef == null)
        //    {
        //        return;
        //    }

        //    //                    if (workTypes.Contains(freshRoomRef.RoomType.Name))
        //    //                        //Only one order per work type must be created
        //    //                        continue;

        //    OrderCreationValues orderValues = new OrderCreationValues(this.Id,
        //                                                              (long)AppointmentOrderItemType.Room,
        //                                                              freshRoomRef.Id.ToString());
        //    foreach (OrderCreationParameter param in orderParameters)
        //    {
        //        try
        //        {
        //            String wtDesc = locator.AccountRepository.FindWorkTypeDescription(accountName,
        //                                                                              freshRoomRef.RoomType.Name,
        //                                                                              room.Location.Id);
        //            orderValues.Add(param.ResolveValue(this, wtDesc, null, freshRoomRef.RoomType.Name,
        //                                               freshRoomRef.RoomName, accountName, locator));
        //        }
        //        catch (SchedulerException)
        //        {
        //            orderValues.AddUnresolvedParam(param);
        //            if (freshRoomRef.IsMammographyResource)
        //                orderValues.IsUpdateToMammoRequired = true;
        //        }
        //    }
        //    if (orderValues.ResolvedSuccessfully)
        //    {
        //        var orderId = CreateOrder(locator, orderValues, (long)AppointmentOrderItemType.Room,
        //                    freshRoomRef.Id.ToString(), false);

        //        if (freshRoomRef.IsMammographyResource)
        //            locator.MammographyRepository.UpdateMammographyWithOrderId(this.Id, orderId);
        //    }
        //    //                        workTypes.Add((freshRoomRef.RoomType.Name));
        //    result.Add(orderValues);
        //}

        //private void CreateOrderForProcedures(RepositoryLocator locator, string accountName, List<OrderCreationParameter> orderParameters, List<OrderCreationValues> result,
        //                                      List<Procedure> procPerRooms, string worktype)
        //{
        //    foreach (Procedure procedure in procPerRooms)
        //    {
        //        if (this.Visit.ContainsOrder(this.Id, (long)AppointmentOrderItemType.Procedure,
        //                                     procedure.GlobalId))
        //        {
        //            continue;
        //        }

        //        if (String.IsNullOrEmpty(procedure.Code) &&
        //            String.IsNullOrEmpty(procedure.CommonDescription))
        //        {
        //            continue;
        //        }

        //        String resolvedWT = worktype;

        //        OrderCreationValues orderValues = new OrderCreationValues(this.Id,
        //                        (long)AppointmentOrderItemType.Procedure, procedure.GlobalId);
        //        foreach (OrderCreationParameter param in orderParameters)
        //        {
        //            try
        //            {
        //                orderValues.Add(param.ResolveValue(this,
        //                                                   procedure.CommonDescription,
        //                                                   procedure.Code,
        //                                                   resolvedWT,
        //                                                   procedure.CommonDescription,
        //                                                   accountName,
        //                                                   locator));
        //            }
        //            catch (SchedulerException)
        //            {
        //                orderValues.AddUnresolvedParam(param);
        //                if (!string.IsNullOrEmpty(procedure.MammogramType))
        //                {
        //                    orderValues.IsUpdateToMammoRequired = true;
        //                }
        //            }
        //        }
        //        if (orderValues.ResolvedSuccessfully)
        //        {
        //            var orderId = CreateOrder(locator, orderValues, (long)AppointmentOrderItemType.Procedure,
        //                        procedure.GlobalId, false);
        //            if (!string.IsNullOrEmpty(procedure.MammogramType))
        //                locator.MammographyRepository.UpdateMammographyWithOrderId(this.Id, orderId);
        //        }
        //        //workTypes.Add(resolvedWT);
        //        result.Add(orderValues);
        //    }
        //}

        //public string CreateOrder(RepositoryLocator locator, OrderCreationValues orderValues, long? itemType, String itemIdentifier, bool isManual)
        //{
        //    var referring = orderValues.GetParam("PhysicianId");
        //    if (referring != null)
        //        locator.AppointmentRepository.AddReferringIfAbsent(this, referring.Value.ToString());

        //    string newOrderID = locator.AccountRepository.CreateOrderEx(this.Id,
        //                 (int?)itemType,
        //                 itemIdentifier,
        //                 orderValues);

        //    locator.AuditRepository.Create(new AuditEntry(Convert.ToInt32(Id),
        //        GetAuditTextInformation(newOrderID, isManual, itemType, itemIdentifier, orderValues.Convert2Dto()),
        //        newOrderID,
        //        AuditEntityNameEnum.Order.ToString(),
        //        AuditActionTypeEnum.Create.ToString()));
        //    //Here we need to attach just created order to parent item (depends on the item type and id)
        //    if (itemType.HasValue)
        //    {
        //        switch (itemType)
        //        {
        //            case (long)AppointmentOrderItemType.Procedure:

        //                break;
        //            case (long)AppointmentOrderItemType.Diagnosis:
        //                break;
        //            case (long)AppointmentOrderItemType.Room:
        //                break;
        //            default:
        //                throw new ArgumentOutOfRangeException("itemType");
        //        }
        //    }
        //    return newOrderID;
        //}

        //private string GetAuditTextInformation(string orderID, bool isManual, long? itemType, string itemIdentifier, OrderCreateParametersDto param)
        //{
        //    StringBuilder forEntity = new StringBuilder(50);
        //    if (itemType.HasValue)
        //    {
        //        switch (itemType)
        //        {
        //            case (long)AppointmentOrderItemType.Procedure:
        //                forEntity.AppendFormat("for procedure (id = {0})", itemIdentifier);
        //                break;
        //            case (long)AppointmentOrderItemType.Diagnosis:
        //                forEntity.AppendFormat("for diagnosis (id = {0})", itemIdentifier);
        //                break;
        //            case (long)AppointmentOrderItemType.Room:
        //                forEntity.AppendFormat("for room (id = {0})", itemIdentifier);
        //                break;
        //            default:
        //                forEntity.Append("for unknown type");
        //                break;
        //        }
        //    }

        //    return string.Format("Order {0} created {1}. (OrderId = {2} ExamCode = {3}, Priority= {4}).",
        //        isManual ? "manually" : "automatically", (UseAttachedParamsForOrderCreation ? "with user assistance " : "") + forEntity, orderID, param.ExamCode, param.Priority);
        //}

        //public AppointmentOrder UpdateOrder(RepositoryLocator locator, AppointmentOrder order)
        //{
        //    AppointmentOrder result = locator.AccountRepository.UpdateOrder(order);
        //    locator.AuditRepository.Create(new AuditEntry(Id, string.Format("Order updated. OrderId = {0}, AppId = {1}.", result.OrderId, result.AppointmentId)
        //        , result.OrderId,
        //        AuditEntityNameEnum.Order.ToString(),
        //        AuditActionTypeEnum.Update.ToString()));
        //    return result;
        //}

        private void GenerateProceduredId()
        {
            if (this.Visit == null)
                return;

            if (this.Visit.Procedures != null)
            {
                foreach (Procedure p in this.Visit.Procedures)
                {
                    if (String.IsNullOrEmpty(p.GlobalId))
                        //p.InitId("Create new order");
                        p.InitId(Guid.NewGuid().ToString());
                }
            }

            if (this.Visit.Diagnosises != null)
            {
                foreach (Diagnosis d in this.Visit.Diagnosises)
                {
                    if (String.IsNullOrEmpty(d.GlobalId))
                        //d.InitId("Create new order");
                        d.InitId(Guid.NewGuid().ToString());
                }
            }
        }

        public DateTime? StartTime
        {
            get
            {
                return this.BusyTime == null ? (DateTime?)null : this.BusyTime.StartTime;
            }
        }

        public DateTime? EndTime
        {
            get
            {
                return this.BusyTime == null ? (DateTime?)null : this.BusyTime.EndTime;
            }
        }

        internal AppointmentResourceTime BusyTime
        {
            get
            {
                if (this.Sources != null)
                {
                    foreach (AppointmentResource r in this.Sources)
                    {
                        if (r.ResourceType == null)
                            continue;

                        if (r.ResourceType.Id == (long)ResourceTypes.Time)
                        {
                            AppointmentResourceTime t = r as AppointmentResourceTime;
                            if (t != null)
                                return t;
                        }
                    }
                }
                return null;
            }
        }

        public long? PatientID
        {
            get
            {
                foreach (AppointmentResource r in this.Sources)
                    if (r.ResourceType.Id == (long)ResourceTypes.Patient)
                        return r.Id;

                return null;
            }
        }

        public long? ModalityId
        {
            get
            {
                foreach (AppointmentResource r in this.Sources)
                {
                    if (r.ResourceType.Id == (long)ResourceTypes.Room)
                    {
                        AppointmentResourceModality m = r as AppointmentResourceModality;
                        if (m == null)
                            continue;

                        return m.Id;
                    }
                }

                return null;
            }
        }

        private List<AppointmentResourceModality> Rooms
        {
            get
            {
                List<AppointmentResourceModality> result = new List<AppointmentResourceModality>();
                foreach (AppointmentResource r in this.Sources)
                {
                    if (r.ResourceType == null)
                        continue;
                    if (r.ResourceType.Id == (int)ResourceTypes.Room)
                    {
                        result.Add(r as AppointmentResourceModality);
                    }
                }
                return result;
            }
        }

        public long? PhysicianId
        {
            get
            {
                //Grety. 2013-01-24. Fix: prevent technicians added as physicians at order creation
                foreach (AppointmentResource r in this.Sources)
                    if (r.ResourceType.Id == (long)ResourceTypes.Physician && (r as AppointmentResourcePhysician).TypeId != 4 /*technician*/)
                        return r.Id;

                return null;
            }
        }

        public long? LocationId
        {
            get
            {
                foreach (AppointmentResource r in this.Sources)
                {
                    if (r.ResourceType.Id == (long)ResourceTypes.Room)
                    {
                        AppointmentResourceModality modality = r as AppointmentResourceModality;
                        if (modality == null || modality.Location == null)
                            continue;
                        return modality.Location.Id;
                    }
                }
                return _locationId;
            }
            set { _locationId = value; }
        }

        public string Worktype
        {
            get
            {
                foreach (AppointmentResource r in this.Sources)
                {
                    if (r.ResourceType.Id == (long)ResourceTypes.Room)
                    {
                        AppointmentResourceModality modality = r as AppointmentResourceModality;
                        if (modality == null)
                            continue;
                        return modality.ModalityType.Name;
                    }
                }
                return _worktype;
            }
            set { _worktype = value; }
        }

        public string Worktype2
        {
            get
            {
                throw new NotImplementedException("As a part of refactoring, need to fix.");
                //CP: Fix
                //foreach (AppointmentResource r in this.Sources)
                //{
                //    if (r.ResourceType.Id == (long)ResourceTypes.Room)
                //    {
                //        AppointmentResourceModality modality = r as AppointmentResourceModality;
                //        if (modality == null)
                //            continue;

                //        if (Services.AccountService.AccountCache.ContainsKey(GlobalContext.RequestContext.AccountId))
                //        {
                //            ModalityType firstOrDefault =
                //                Services.AccountService.AccountCache[Container.RequestContext.AccountId].Account
                //                    .ModalityTypes.FirstOrDefault(p => p.Id == modality.ModalityType.Id);
                //            if (firstOrDefault != null)
                //                return firstOrDefault.Name;
                //        }
                //    }
                //}
                return _worktype;
            }
            private set { _worktype = value; }
        }

        //public static Appointment ExtractFromDto(AppointmentDto dto)
        //{
        //    Appointment result = new Appointment();
        //    result.Account = new Account(dto.AccountID);
        //    result.Id = dto.AppointmentID;
        //    result.Description = dto.Description;
        //    result.Name = dto.Name;
        //    result.Number = dto.Number;
        //    result.LocationId = dto.LocationId;
        //    result.Worktype = dto.Worktype;
        //    result.UseAttachedParamsForOrderCreation = dto.UseAttachedParamsForOrderCreation;
        //    result.DefaultOrderPriority = dto.DefaultOrderPriority;
        //    result.PendingReasonCode = dto.PendingReasonCode;
        //    result.PendingReasonText = dto.PendingReasonText;
        //    result.CreateBy = dto.CreateBy;
        //    result.CreatedOn = dto.CreatedOn;
        //    result.LastModifiedBy = dto.LastModifiedBy;
        //    result.LastModifiedOn = dto.LastModifiedOn;
        //    result.Comment = dto.Comment;
        //    result.IsAuthorizationAlert = dto.IsAuthorizationAlert;
        //    result.TechUserId = dto.TechUserId;
        //    result.TechName = dto.TechName;
        //    result.BaseAppointmentId = dto.BaseAppointmentId;
        //    result.GroupedAppointmentId = dto.GroupedAppointmentId;
        //    result.IsAssociationToParentRequired = dto.IsAssociationToParentRequired;
        //    result.GroupId = dto.GroupId;
        //    result.RequestedTimeRange = dto.RequestedTimeRange;
        //    foreach (AppointmentResourceDto r in dto.Resources)
        //        result.Sources.Add(AppointmentResource.ExtractFromDto(r));

        //    foreach (OrderCreateParametersDto parm in dto.OrderCreationParams.Set)
        //        result.OrderValues.Add(OrderCreationValues.ExtractFromDto(parm));

        //    result.Status = new AppointmentStatus(dto.StatusID);
        //    result.Visit = Visit.ExtractFromDto(dto.Visit);

        //    result.AppointmentProcedureWithLocationAlerts.AddRange(dto.AppointmentProcedureWithLocationAlerts);

        //    return result;
        //}

        public List<string> AppointmentProcedureWithLocationAlerts { get; set; }

        public void LoadVisit(Visit visit)
        {
            this.Visit = visit;
        }
        public void LoadAttachedGuarantors(List<long> attachedguarantors)
        {
            this.Visit.AttachedGuarantors.AddRange(attachedguarantors);
        }

        public void AttachVisit(Visit visit)
        {
            this.Visit = visit;
            IncludeProceduresOverhead();
        }

        private void IncludeProceduresOverhead()
        {
            return;
            if (this.Visit == null)
                return;
            //In cases when extra procedures are added to the appointments 
            //we need to check if additional time for some appointment is required and 
            //if it is required then we need to exted appointment time range with required time 
            //(perform reschedule operation without status changing)
            int totalOverHeadMinuts = 0;
            foreach (Procedure procedure in this.Visit.Procedures)
            {
                if (procedure.LinkedApptId.HasValue && procedure.LinkedApptId.Value == this.Id)
                    if (procedure.TimeOverheadMinutes != null)
                        totalOverHeadMinuts += procedure.TimeOverheadMinutes.Value;
            }

            if (totalOverHeadMinuts > 0L && this.BusyTime != null)
            {
                this.BusyTime.ExtendRange(totalOverHeadMinuts);
            }
        }

        //internal void SetOrderCreationParams(bool p, OrderCreateParametersSetDto orderCreateParametersSetDto)
        //{
        //    this.UseAttachedParamsForOrderCreation = p;
        //    this.OrderValues.Clear();

        //    foreach (OrderCreateParametersDto parm in orderCreateParametersSetDto.Set)
        //        this.OrderValues.Add(OrderCreationValues.ExtractFromDto(parm));
        //}


        //public static AppointmentDto Convert2Dto(Appointment app)
        //{
        //    AppointmentDto result = new AppointmentDto();
        //    result.AccountID = app.Account.Id;
        //    result.GroupedAppointmentId = app.GroupedAppointmentId;
        //    result.AppointmentID = app.Id;
        //    result.PatientVisitId = app.PatientVisitId;
        //    result.Description = app.Description;
        //    result.Name = app.Name;
        //    result.Number = app.Number;
        //    result.LocationId = app.LocationId;
        //    result.DefaultOrderPriority = app.DefaultOrderPriority;
        //    result.PendingReasonCode = app.PendingReasonCode;
        //    result.PendingReasonText = app.PendingReasonText;
        //    result.Comment = app.Comment;

        //    foreach (AppointmentResource r in app.Sources)
        //        result.Resources.Add(AppointmentResource.Convert2Dto(r));

        //    result.StatusID = app.Status.Id;
        //    result.Visit = Visit.Convert2Dto(app.Visit);
        //    result.CreateBy = app.CreateBy;
        //    result.CreatedOn = app.CreatedOn;
        //    result.LastModifiedBy = app.LastModifiedBy;
        //    result.LastModifiedOn = app.LastModifiedOn;
        //    result.IsAuthorizationAlert = app.IsAuthorizationAlert;
        //    result.TechName = app.TechName;
        //    result.TechUserId = app.TechUserId;
        //    result.IsLocked = app.IsLocked;
        //    result.IsLockedBy = app.IsLockedBy;
        //    result.GroupId = app.GroupId;
        //    if (app.PaymentFees != null)
        //        foreach (PaymentFee paymentFee in app.PaymentFees)
        //            result.PaymentFees.Add(PaymentFee.Convert2Dto(paymentFee));

        //    result.AppointmentProcedureWithLocationAlerts.AddRange(app.AppointmentProcedureWithLocationAlerts);
        //    result.RequestedTimeRange = app.RequestedTimeRange;
        //    return result;
        //}


        public void ChangeStatus(AppointmentStatus status)
        {
            this.Status = status;
        }

        //public void UpdateOrdersWhenRequired(RepositoryLocator locator)
        //{
        //    foreach (AppointmentOrder order in Visit.LinkedOrders)
        //    {
        //        locator.AccountRepository.UpdateOrderScheduleWhenNecessary(order.Id);
        //    }
        //}

        //CP: Fix
        //public void SetAuthAlert(SafeDataReader sr)
        //{
        //    long? typeID = sr.GetNullableInt64("ResourceType");
        //    if (typeID.HasValue && typeID == 2)
        //    {
        //        if (sr.ContainsColumn("alert"))
        //        {
        //            if (!IsAuthorizationAlert)
        //            {
        //                int alert = sr.GetInt32("alert");
        //                bool isTrue = alert > 0;
        //                if (isTrue)
        //                    IsAuthorizationAlert = true;
        //            }
        //        }
        //    }
        //}

        //public void UpdatePendingFields(AppointmentDto incomeApp)
        //{
        //    this.PendingReasonCode = incomeApp.PendingReasonCode;
        //    this.PendingReasonText = incomeApp.PendingReasonText;
        //}

        //public void UpdateTechInfo(RepositoryLocator locator, Appointment napp)
        //{
        //    locator.AppointmentRepository.UpdateTechInfo(Id, napp.TechName, napp.TechUserId);
        //}

        public void SetTechInfo(string techName, string techUserId)
        {
            this.TechName = techName;
            this.TechUserId = techUserId;
        }

        public static List<AppointmentResourceModality> GetAffectedVirtualRooms(List<AppointmentResourceModality> rooms, List<AppointmentResourceModality> allRooms)
        {
            var vgroups = rooms.Where(a => a.VirtualRoomId.HasValue).Select(a => a.VirtualRoomId).Distinct().ToList();
            var result = allRooms.Where(a => vgroups.Contains(a.VirtualRoomId) && !(rooms.Any(r => r.Id == a.Id))).ToList();
            return result;
        }

        public void SetId(int newId)
        {
            Id = newId;
        }

        //public void UpdatePatientVisitWhenRequired(RepositoryLocator locator, bool checkRequired)
        //{
        //    ManagePatientVisits(locator, checkRequired);
        //}

        public void SetPatientVisitId(int? visitId)
        {
            PatientVisitId = visitId;
        }

        public void SetDateInfo(DateTime? startDate, DateTime? endDate)
        {
            this.BusyTime.SetDateTime(startDate ?? DateTime.MinValue, endDate ?? DateTime.MinValue);
        }

        public void SetAccount(Account account)
        {
            Account = account;
        }

        public void SetStatus(AppointmentStatus appointmentStatus)
        {
            Status = appointmentStatus;
        }

        public void SetLocation(long locationId)
        {
            LocationId = locationId;
        }

        public void SetVisit(Visit visit)
        {
            this.Visit = visit;
        }

        public void LoadAffectedFees(List<PaymentFee> getPaymentFees)
        {
            PaymentFees = getPaymentFees;
        }

        public void SetLocationId(int? locationId)
        {
            this.LocationId = locationId;
        }
    }

    public class PatientVisitProceduresComparer : IEqualityComparer<Procedure>
    {
        public bool Equals(Procedure x, Procedure y)
        {
            return x.Code == y.Code;
        }

        public int GetHashCode(Procedure obj)
        {
            return obj.GetHashCode();
        }
    }
}
