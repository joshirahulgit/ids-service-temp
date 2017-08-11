using Scheduler.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.DBEntity
{
    public class OrderCreationParameter : EntityBase
    {
        public OrderCreationParameter()
        {
        }

        public OrderCreationParameter(string paramName)
        {
            this.ParamName = paramName;
            this.ParamType = "object";
            this.IsSystemRequired = false;
            this.DefaultValue = null;
        }

        public String ParamName { get; set; }
        public String ParamType { get; set; }
        public bool IsSystemRequired { get; set; }
        public bool IsRequired { get; set; }
        public String DefaultValue { get; set; }
        public bool PromptUserForDefault { get; set; }

        public const string PATIENT_ID_PARAM = "PatientId";
        public const string LOCATION_ID_PARAM = "LocationId";
        public const string LOCATION_NAME_PARAM = "LocationName";
        public const string EXAMCODE_PARAM = "ExamCode";
        public const string WORKTYPE_PARAM = "WorkType";
        public const string ROOM_NAME = "RoomName";
        public const string EXAMDESC_PARAM = "ExamDescription";
        public const string PHYS_ID_PARAM = "PhysicianId";
        public const string PHYS_NAME_PARAM = "PhysicianName";
        public const string REASON_PARAM = "Reason";
        public const string VISIT_TYPE_PARAM = "VisitType";
        public const string DICTATOR_ID_PARAM = "DictatorId";
        public const string DICTATOR_NAME_PARAM = "DictatorName";
        public const string PRIORITY_PARAM = "Priority";
        public const string MULTY_ORDER_ID_PARAM = "MultipleOrderId";
        public const string ORDER_ID = "OrderId";
        public const string DOS_PARAM = "DoS";
        public const string ACCOUNT_NAME_PARAM = "AccountName";
        public const string CC_PARAM = "CC";
        public const string MODALITY_PARAM = "Modality";
        public const string RECCURING_SERIES_PARAM = "SeriesID";

        private OrderCreationValue ResolveNullableItem<T>(String paramName, Nullable<T> value) where T : struct
        {
            if (value.HasValue)
                return new OrderCreationValue(this, value.Value);
            else if (!String.IsNullOrEmpty(this.DefaultValue))
                return new OrderCreationValue(this, this.DefaultValue, true);
            else if (this.IsSystemRequired || this.IsRequired)
                throw new SchedulerException(SchedulerExceptionType.OrderCreationParameterIsNotSupplied,
                    String.Format("Automatic order creation failed. Required parameter {0} is missing", paramName));
            else
                return new OrderCreationValue(this);
        }

        private OrderCreationValue ResolveStringItem(String paramName, String value)
        {
            if (!String.IsNullOrEmpty(value))
                return new OrderCreationValue(this, value);
            else if (!String.IsNullOrEmpty(this.DefaultValue))
                return new OrderCreationValue(this, this.DefaultValue, true);
            else if (this.IsSystemRequired || this.IsRequired)
                throw new SchedulerException(SchedulerExceptionType.OrderCreationParameterIsNotSupplied,
                    String.Format("Automatic order creation failed. Required parameter {0} is missing", paramName));
            else
                return new OrderCreationValue(this);
        }

        //public OrderCreationValue ResolveValue(Appointment appointment, String visitTypeStr, String examCodeStr, String workTypeStr, String examDescription, String accountName, RepositoryLocator locator)
        //{
        //    switch (this.ParamName)
        //    {
        //        case PATIENT_ID_PARAM:
        //            {
        //                if (appointment.PatientID.HasValue)
        //                {
        //                    AppointmentResource resource = locator.ResourceRepository.GetById(new ResourceID(appointment.PatientID.Value, Common.Enums.ResourceTypes.Patient));
        //                    AppointmentResourcePatient patient = resource as AppointmentResourcePatient;
        //                    if (patient != null)
        //                        return ResolveStringItem(this.ParamName, patient.RecordNumber);
        //                }
        //                return ResolveStringItem(this.ParamName, null);
        //            }
        //        case LOCATION_ID_PARAM:
        //            {
        //                return ResolveNullableItem(this.ParamName, appointment.LocationId);
        //            }
        //        case EXAMCODE_PARAM:
        //            {
        //                return ResolveStringItem(this.ParamName, examCodeStr);
        //            }
        //        case WORKTYPE_PARAM:
        //            {
        //                return ResolveStringItem(this.ParamName, workTypeStr);
        //            }
        //        case EXAMDESC_PARAM:
        //            {
        //                return ResolveStringItem(this.ParamName, examDescription);
        //            }
        //        case PHYS_ID_PARAM:
        //            {
        //                //Grety mod 2013-02-28. Currently there are no types of refPhysicians so all refs go into orders
        //                //List<string> mappedTypes = locator.AccountRepository.FindRefTypesForOrderPhysicianID(accountName);

        //                //when there are many phisicians we select primary through mapped types, usually primary is among mapped
        //                foreach (Referral referral in appointment.Visit.Referrals)
        //                {
        //                    //if (mappedTypes.Contains(referral.Type))
        //                    return ResolveStringItem(this.ParamName, referral.ReferralId);
        //                }
        //                if (appointment.Visit.Referrals.Count > 0) // if failed to find among mapped types (try to stick to old behaviour)
        //                    return ResolveStringItem(this.ParamName, appointment.Visit.Referrals[0].ReferralId);
        //                return ResolveStringItem(this.ParamName, null);
        //            }
        //        case REASON_PARAM:
        //            {
        //                return ResolveStringItem(this.ParamName, appointment.Visit != null ? appointment.Visit.VisitReason : null);
        //            }
        //        case DICTATOR_ID_PARAM:
        //            {
        //                return ResolveNullableItem(this.ParamName, appointment.PhysicianId);
        //            }
        //        case PRIORITY_PARAM:
        //            {
        //                return ResolveStringItem(this.ParamName, appointment.DefaultOrderPriority);
        //            }
        //        case MULTY_ORDER_ID_PARAM:
        //            {
        //                return ResolveStringItem(this.ParamName, null);
        //            }
        //        case ORDER_ID:
        //            {
        //                String result = String.Empty;
        //                var gen = locator.AccountRepository.GetIdGenerator(appointment.LocationId.HasValue ? appointment.LocationId.ToString() : string.Empty,
        //                                                                       IdGenerationTypeName.ORDER);
        //                result = gen.GetNewId(locator);
        //                return ResolveStringItem(this.ParamName, result);
        //            }
        //        case DOS_PARAM:
        //            {
        //                return ResolveNullableItem(this.ParamName, appointment.StartTime);
        //            }
        //        case ACCOUNT_NAME_PARAM:
        //            {
        //                return ResolveStringItem(this.ParamName, accountName);
        //            }
        //        case CC_PARAM:
        //            {
        //                List<string> mappedTypes = locator.AccountRepository.FindRefTypesForOrderСС(accountName);
        //                foreach (Referral referral in appointment.Visit.Referrals)
        //                {
        //                    if (mappedTypes.Contains(referral.Type))
        //                        return ResolveStringItem(this.ParamName, referral.ReferralId);
        //                }
        //                return ResolveStringItem(this.ParamName, null);
        //            }
        //        case RECCURING_SERIES_PARAM:
        //            {
        //                return ResolveStringItem(this.ParamName, null);
        //            }
        //        case VISIT_TYPE_PARAM:
        //            {
        //                return ResolveStringItem(this.ParamName, visitTypeStr);
        //            }
        //        case MODALITY_PARAM:
        //            {
        //                long? modalityId = appointment.ModalityId;
        //                if (modalityId.HasValue)
        //                {
        //                    AppointmentResourceModality modality1 =
        //                        locator.ResourceRepository.GetById(new ResourceID(modalityId.Value, Common.Enums.ResourceTypes.Room)) as AppointmentResourceModality;
        //                    if (modality1 != null)
        //                        return ResolveStringItem(this.ParamName, modality1.RoomName);
        //                }
        //                return ResolveStringItem(this.ParamName, null);
        //            }
        //        default:
        //            throw new SchedulerException(SchedulerExceptionType.OrderCreationParameterIsNotSupplied,
        //                String.Format("Unexpected order creation parameter {0}", this.ParamName));
        //    }
        //}

        public void SetDefaultValue(string value)
        {
            DefaultValue = value;
        }

        //public static OrderCreationParameter ExtractFromDto(OrderCreationParameterDto op)
        //{
        //    OrderCreationParameter result = new OrderCreationParameter();
        //    result.Id = op.Id;
        //    result.IsRequired = op.IsRequired;
        //    result.IsSystemRequired = op.IsSystemRequired;
        //    result.ParamName = op.ParamName;
        //    result.ParamType = op.ParamType;
        //    result.PromptUserForDefault = op.PromptUserForDefault;
        //    result.DefaultValue = op.DefaultValue;

        //    return result;
        //}

        //public static OrderCreationParameterDto Convert2Dto(OrderCreationParameter p)
        //{
        //    OrderCreationParameterDto result = new OrderCreationParameterDto();
        //    result.Id = p.Id;
        //    result.DefaultValue = p.DefaultValue;
        //    result.IsSystemRequired = p.IsSystemRequired;
        //    result.ParamName = p.ParamName;
        //    result.ParamType = p.ParamType;
        //    result.IsRequired = p.IsRequired;
        //    result.PromptUserForDefault = p.PromptUserForDefault;
        //    return result;
        //}

        public override string ToString()
        {
            return string.Format("{0} {1}", ParamName, ParamType);
        }
    }

    public class OrderCreationValue
    {
        private OrderCreationParameter _parameter;
        private object _value;
        private bool _isValueSpecified;
        private bool _isDefaultValue;

        #region Construction

        public OrderCreationValue(OrderCreationParameter parameter)
        {
            if (parameter == null)
                throw new ArgumentNullException("parameter");

            _parameter = parameter;
            _isValueSpecified = false;
            _isDefaultValue = false;
        }

        public OrderCreationValue(OrderCreationParameter parameter, object value)
            : this(parameter)
        {
            _value = value;
            _isValueSpecified = true;
        }


        public OrderCreationValue(OrderCreationParameter parameter, object value, bool defaultValue)
            : this(parameter, value)
        {
            _isDefaultValue = defaultValue;
        }

        public OrderCreationValue(string parameterName, object value)
        {
            _parameter = new OrderCreationParameter(parameterName);
            _value = value;
            _isValueSpecified = true;
            _isDefaultValue = false;
        }



        #endregion

        public bool IsDefaultValue
        {
            get { return _isDefaultValue; }
        }

        public bool IsValueSpecified
        {
            get { return _isValueSpecified; }
        }

        public Object Value
        {
            get { return _value; }
        }

        public String ParamName
        {
            get { return _parameter.ParamName; }
        }
    }

    public class OrderCreationValues : List<OrderCreationValue>
    {
        private long _appointmentID;
        private long? _appointmentItemId;
        private String _appintmentItemIdentifier;

        public bool IsUpdateToMammoRequired { get; set; }

        public long AppointmentId
        {
            get { return _appointmentID; }
        }

        public long? AppointmentItemId
        {
            get { return _appointmentItemId; }
        }

        public String AppointmentItemIdentifier
        {
            get { return _appintmentItemIdentifier; }
        }

        public OrderCreationValues(long appId, long? appItemId, string appItemIdStr)
        {
            _appointmentID = appId;
            _appointmentItemId = appItemId;
            _appintmentItemIdentifier = appItemIdStr;
        }

        private List<OrderCreationParameter> _unresolvedParams = new List<OrderCreationParameter>();

        //public static OrderCreationValues ExtractFromDto(OrderCreateParametersDto dto)
        //{
        //    OrderCreationValues result = new OrderCreationValues(dto.AppointmentId, dto.AppointmentItemType, dto.AppointmentItemId);
        //    result.Add(new OrderCreationValue(OrderCreationParameter.ACCOUNT_NAME_PARAM, dto.Account));
        //    if (dto.CC != null && dto.CC.Count > 0)
        //        result.Add(new OrderCreationValue(OrderCreationParameter.CC_PARAM, dto.CC[0].ReferralId));

        //    result.IsUpdateToMammoRequired = dto.IsUpdateToMammoRequired;
        //    result.Add(new OrderCreationValue(OrderCreationParameter.DICTATOR_ID_PARAM, dto.DictatorId));
        //    result.Add(new OrderCreationValue(OrderCreationParameter.DICTATOR_NAME_PARAM, dto.Dictator));
        //    result.Add(new OrderCreationValue(OrderCreationParameter.DOS_PARAM, dto.DateOfService));
        //    result.Add(new OrderCreationValue(OrderCreationParameter.EXAMCODE_PARAM, dto.ExamCode));
        //    result.Add(new OrderCreationValue(OrderCreationParameter.EXAMDESC_PARAM, dto.ExamDescription));
        //    result.Add(new OrderCreationValue(OrderCreationParameter.LOCATION_ID_PARAM, dto.LocationId));
        //    result.Add(new OrderCreationValue(OrderCreationParameter.LOCATION_NAME_PARAM, dto.Location));
        //    result.Add(new OrderCreationValue(OrderCreationParameter.MODALITY_PARAM, dto.Modality));
        //    result.Add(new OrderCreationValue(OrderCreationParameter.MULTY_ORDER_ID_PARAM, String.Empty));
        //    result.Add(new OrderCreationValue(OrderCreationParameter.ORDER_ID, dto.OrderId));
        //    result.Add(new OrderCreationValue(OrderCreationParameter.PATIENT_ID_PARAM, dto.PatientId));
        //    result.Add(new OrderCreationValue(OrderCreationParameter.PHYS_ID_PARAM, dto.PhysicianId));
        //    result.Add(new OrderCreationValue(OrderCreationParameter.PHYS_NAME_PARAM, dto.Physician));
        //    result.Add(new OrderCreationValue(OrderCreationParameter.PRIORITY_PARAM, dto.Priority));
        //    result.Add(new OrderCreationValue(OrderCreationParameter.REASON_PARAM, dto.Reason));
        //    result.Add(new OrderCreationValue(OrderCreationParameter.RECCURING_SERIES_PARAM, dto.RecurringSeriesId));
        //    result.Add(new OrderCreationValue(OrderCreationParameter.VISIT_TYPE_PARAM, dto.Reason));
        //    result.Add(new OrderCreationValue(OrderCreationParameter.WORKTYPE_PARAM, dto.WorkTypeId));
        //    result.Add(new OrderCreationValue(OrderCreationParameter.ROOM_NAME, dto.RoomName));
        //    return result;
        //}

        //public OrderCreateParametersDto Convert2Dto()
        //{
        //    OrderCreateParametersDto result = new OrderCreateParametersDto();
        //    result.AppointmentId = _appointmentID;
        //    result.AppointmentItemId = _appintmentItemIdentifier;
        //    result.AppointmentItemType = _appointmentItemId;

        //    result.Account = GetValue(OrderCreationParameter.ACCOUNT_NAME_PARAM) as String;
        //    object cc = GetValue(OrderCreationParameter.CC_PARAM);
        //    if (cc != null)
        //    {
        //        result.CC = new List<ReferralDto>();
        //        result.CC.Add(new ReferralDto(-1, cc as String, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty));
        //    }

        //    result.DictatorId = Convert.ToString(GetValue(OrderCreationParameter.DICTATOR_ID_PARAM));
        //    result.Dictator = GetValue(OrderCreationParameter.DICTATOR_NAME_PARAM) as String;

        //    object dos = GetValue(OrderCreationParameter.DOS_PARAM);
        //    if (dos != null)
        //        result.DateOfService = Convert.ToDateTime(dos);

        //    result.ExamCode = GetValue(OrderCreationParameter.EXAMCODE_PARAM) as String;
        //    result.ExamDescription = GetValue(OrderCreationParameter.EXAMDESC_PARAM) as String;
        //    result.Location = GetValue(OrderCreationParameter.LOCATION_NAME_PARAM) as String;

        //    object locIdValue = GetValue(OrderCreationParameter.LOCATION_ID_PARAM);
        //    if (locIdValue != null)
        //        result.LocationId = Convert.ToInt32(locIdValue);

        //    result.Modality = GetValue(OrderCreationParameter.MODALITY_PARAM) as String;
        //    result.OrderId = GetValue(OrderCreationParameter.ORDER_ID) as String;
        //    result.PatientId = GetValue(OrderCreationParameter.PATIENT_ID_PARAM) as String;
        //    result.PhysicianId = GetValue(OrderCreationParameter.PHYS_ID_PARAM) as String;
        //    result.Physician = GetValue(OrderCreationParameter.PHYS_NAME_PARAM) as String;
        //    result.Priority = GetValue(OrderCreationParameter.PRIORITY_PARAM) as String;
        //    result.Reason = GetValue(OrderCreationParameter.REASON_PARAM) as String;
        //    result.RecurringSeriesId = GetValue(OrderCreationParameter.RECCURING_SERIES_PARAM) as String;
        //    result.VisitType = GetValue(OrderCreationParameter.VISIT_TYPE_PARAM) as String;
        //    result.WorkTypeId = GetValue(OrderCreationParameter.WORKTYPE_PARAM) as String;
        //    result.IsUpdateToMammoRequired = this.IsUpdateToMammoRequired;

        //    return result;
        //}

        public OrderCreationValue GetParam(string paramName)
        {
            foreach (OrderCreationValue val in this)
            {
                if (val.ParamName.Equals(paramName, StringComparison.CurrentCultureIgnoreCase))
                    return val;
            }
            return null;
        }

        public object GetValue(string paramName)
        {
            OrderCreationValue val = GetParam(paramName);
            return val == null ? null : val.Value;
        }

        public object GetSQLParam(string paramName)
        {
            OrderCreationValue val = GetParam(paramName);
            return val == null ? DBNull.Value : val.Value;
        }

        public void AddUnresolvedParam(OrderCreationParameter param)
        {
            _unresolvedParams.Add(param);
        }

        public bool ResolvedSuccessfully
        {
            get { return _unresolvedParams.Count == 0; }
        }
    }
}
