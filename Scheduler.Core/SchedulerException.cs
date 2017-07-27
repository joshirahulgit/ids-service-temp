using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scheduler.Core
{
    public class SchedulerException : Exception //TODO Need to think, how to handle it -By RJ//: FaultException//Exception//ApplicationException 
    {
        //[DataMember(Name = "T")]
        public SchedulerExceptionType ExceptionType { get; set; }

        //TODO: No data member in core project, -By RJ
        //[DataMember(Name = "V")]
        // TODO: No DTO Base, should be coming through some interface
        //public DtoBase AdditionalInfo { get; set; }

        #region Construction

#if SILVERLIGHT

            public SchedulerException(SchedulerExceptionType exceptionType)
            {
                this.ExceptionType = exceptionType;
            }

            public SchedulerException(SchedulerExceptionType exceptionType, String message)
            {
                this.ExceptionType = exceptionType;
            }

            public SchedulerException(SchedulerExceptionType type, DtoBase additionalInfo)
                :this(type,String.Empty)
            {
                this.AdditionalInfo = additionalInfo;
            }

#else

        public SchedulerException(SchedulerExceptionType exceptionType)
                : base(String.Empty) 
        {
            this.ExceptionType = exceptionType;
        }

    public SchedulerException(SchedulerExceptionType exceptionType, String message)
            : base(message)
        {
        this.ExceptionType = exceptionType;
    }

        //No DTO Base is allowed here. -BY RJ
    //public SchedulerException(SchedulerExceptionType type, DtoBase additionalInfo)
    //        : this(type, String.Empty)
    //    {
    //    this.AdditionalInfo = additionalInfo;
    //}

#endif

    #endregion
}

public enum SchedulerExceptionType
{
    //[EnumMember] No enum member allowed, also not required here. -By RJ
    Authentication,
    //[EnumMember] No enum member allowed, also not required here. -By RJ
    DifferentAccounts,
    //[EnumMember] No enum member allowed, also not required here. -By RJ
    Default,
    //[EnumMember] No enum member allowed, also not required here. -By RJ
    Validation,
    //[EnumMember] No enum member allowed, also not required here. -By RJ
    Operational,
    //[EnumMember] No enum member allowed, also not required here. -By RJ
    AppointmentDoesnotExists,
    //[EnumMember] No enum member allowed, also not required here. -By RJ
    PatientDoesnotExists,
    //[EnumMember] No enum member allowed, also not required here. -By RJ
    DatabaseConnectionFailed,
    //[EnumMember] No enum member allowed, also not required here. -By RJ
    ResourceDoesntExists,
    //[EnumMember] No enum member allowed, also not required here. -By RJ
    DbPatchesAreNotAvailable,
    //[EnumMember] No enum member allowed, also not required here. -By RJ
    StorageSyncronizationFailed,
    //[EnumMember] No enum member allowed, also not required here. -By RJ
    ResourceDublicatedException,
    //[EnumMember] No enum member allowed, also not required here. -By RJ
    EmailIsNotSent,
    //[EnumMember] No enum member allowed, also not required here. -By RJ
    PayerIsNotUpdate,
    //[EnumMember] No enum member allowed, also not required here. -By RJ
    UserIsNotCreated,
    //[EnumMember] No enum member allowed, also not required here. -By RJ
    UserIsNotUpdated,
    //[EnumMember] No enum member allowed, also not required here. -By RJ
    UserNameAlreadyExists,
    //[EnumMember] No enum member allowed, also not required here. -By RJ
    OrderCreation,
    //[EnumMember] No enum member allowed, also not required here. -By RJ
    TimeResourceIsWrong,
    //[EnumMember] No enum member allowed, also not required here. -By RJ
    ImageUpdloadError,
    //[EnumMember] No enum member allowed, also not required here. -By RJ
    ImageDownloadError,
    //[EnumMember] No enum member allowed, also not required here. -By RJ
    AppointmentConfigurationException,
    //[EnumMember] No enum member allowed, also not required here. -By RJ
    AppointmentRequestProcessError,
    //[EnumMember] No enum member allowed, also not required here. -By RJ
    InsuranceCompanyNameIsMissed,
    //[EnumMember] No enum member allowed, also not required here. -By RJ
    ExternalAuthentificationServiceError,
    //[EnumMember] No enum member allowed, also not required here. -By RJ
    CannotCreateEmptyComment,
    //[EnumMember] No enum member allowed, also not required here. -By RJ
    LocalCPTStorageError,
    //[EnumMember] No enum member allowed, also not required here. -By RJ
    NotSupportedConfiguration,
    //[EnumMember] No enum member allowed, also not required here. -By RJ
    OrderCreationParameterIsNotSupplied,
    //[EnumMember] No enum member allowed, also not required here. -By RJ
    UniqueUIDViolation,
    //[EnumMember] No enum member allowed, also not required here. -By RJ
    UnknownDALError,
    //[EnumMember] No enum member allowed, also not required here. -By RJ
    GuidGenerationSequenceFailed,
    //[EnumMember] No enum member allowed, also not required here. -By RJ
    UndefinedIdGenerationType,
    //[EnumMember] No enum member allowed, also not required here. -By RJ
    AuthorizationActivation,
    //[EnumMember] No enum member allowed, also not required here. -By RJ
    UnkonwnOrderKey,
    //[EnumMember] No enum member allowed, also not required here. -By RJ
    PatientContactMethodNotSupported,
    //[EnumMember] No enum member allowed, also not required here. -By RJ
    AccountIdByAccountNameAndUserNameNotFound,
    //[EnumMember] No enum member allowed, also not required here. -By RJ
    AuthorizationDoesntExist,
    //[EnumMember] No enum member allowed, also not required here. -By RJ
    RoleDoesntExist,
    //[EnumMember] No enum member allowed, also not required here. -By RJ
    ContactNotFound,
    //[EnumMember] No enum member allowed, also not required here. -By RJ
    ResourceIsInUse,
}
}
