USE [Global]
GO

IF NOT EXISTS (SELECT *
			   FROM
				   sys.objects
			   WHERE
				   object_id = object_id(N'[dbo].[SecuredEntities]')
				   AND type IN (N'U'))

BEGIN

	CREATE TABLE dbo.SecuredEntities(
		SecuredEntityID INT IDENTITY (1, 1) NOT NULL,
		SecuredEntityName VARCHAR(250) NOT NULL,
		SecuredEntityDesc VARCHAR(750) NULL,
		isActive BIT NOT NULL CONSTRAINT DF_SecuredEntities_isActive DEFAULT (1),
		CreateDate DATETIME NOT NULL CONSTRAINT DF_SecuredEntities_CreateDate DEFAULT (getdate()),
		CreateUser VARCHAR(50) NOT NULL,
		CONSTRAINT PK_SecuredEntities_SecuredEntityID PRIMARY KEY (SecuredEntityID)
	);


IF NOT exists(SELECT * FROM SecuredEntities where SecuredEntityID BETWEEN 0 AND 199)
begin
	SET IDENTITY_INSERT SecuredEntities ON;
	INSERT INTO SecuredEntities (SecuredEntityID, SecuredEntityName, CreateUser) VALUES(1,  'SchedulerAdmin', 'sunil.thomas');
	INSERT INTO SecuredEntities (SecuredEntityID, SecuredEntityName, CreateUser) VALUES(2,  'SchedulerAdminAccount', 'sunil.thomas');
	INSERT INTO SecuredEntities (SecuredEntityID, SecuredEntityName, CreateUser) VALUES(3,  'SchedulerAdminWorkCalendar', 'sunil.thomas');
	INSERT INTO SecuredEntities (SecuredEntityID, SecuredEntityName, CreateUser) VALUES(4,  'SchedulerAdminUserManagement', 'sunil.thomas');
	INSERT INTO SecuredEntities (SecuredEntityID, SecuredEntityName, CreateUser) VALUES(5,  'SchedulerAdminAccountGeneral', 'sunil.thomas');
	INSERT INTO SecuredEntities (SecuredEntityID, SecuredEntityName, CreateUser) VALUES(6,  'SchedulerAdminAccountProviderRoles', 'sunil.thomas');
	INSERT INTO SecuredEntities (SecuredEntityID, SecuredEntityName, CreateUser) VALUES(7,  'SchedulerAdminAccountProviders', 'sunil.thomas');
	INSERT INTO SecuredEntities (SecuredEntityID, SecuredEntityName, CreateUser) VALUES(8,  'SchedulerAdminAccountModalities', 'sunil.thomas');
	INSERT INTO SecuredEntities (SecuredEntityID, SecuredEntityName, CreateUser) VALUES(9,  'SchedulerAdminAccountAppointmentStatuses', 'sunil.thomas');
	INSERT INTO SecuredEntities (SecuredEntityID, SecuredEntityName, CreateUser) VALUES(10, 'SchedulerAdminAccountCommentTypes', 'sunil.thomas');
	INSERT INTO SecuredEntities (SecuredEntityID, SecuredEntityName, CreateUser) VALUES(11, 'SchedulerAdminAccountVolumeUnits', 'sunil.thomas');
	INSERT INTO SecuredEntities (SecuredEntityID, SecuredEntityName, CreateUser) VALUES(12, 'SchedulerAdminAccountReferralSpecialities', 'sunil.thomas');
	INSERT INTO SecuredEntities (SecuredEntityID, SecuredEntityName, CreateUser) VALUES(13, 'SchedulerAdminAccountIdsConfiguration', 'sunil.thomas');
	INSERT INTO SecuredEntities (SecuredEntityID, SecuredEntityName, CreateUser) VALUES(14, 'SchedulerAdminAccountAudit', 'sunil.thomas');
	INSERT INTO SecuredEntities (SecuredEntityID, SecuredEntityName, CreateUser) VALUES(15, 'SchedulerAdminAccountAuthorization', 'sunil.thomas');
	INSERT INTO SecuredEntities (SecuredEntityID, SecuredEntityName, CreateUser) VALUES(16, 'SchedulerAdminAccountTechCompleteSuggestionList', 'sunil.thomas');
	INSERT INTO SecuredEntities (SecuredEntityID, SecuredEntityName, CreateUser) VALUES(17, 'SchedulerAdminAccountHCPCSList', 'sunil.thomas');
	INSERT INTO SecuredEntities (SecuredEntityID, SecuredEntityName, CreateUser) VALUES(18, 'SchedulerAdminAccountHeardOfUsList', 'sunil.thomas');
	INSERT INTO SecuredEntities (SecuredEntityID, SecuredEntityName, CreateUser) VALUES(19, 'SchedulerAdminAccountPriorityList', 'sunil.thomas');
	INSERT INTO SecuredEntities (SecuredEntityID, SecuredEntityName, CreateUser) VALUES(20, 'SchedulerAdminAccountScheduledByList', 'sunil.thomas');
	INSERT INTO SecuredEntities (SecuredEntityID, SecuredEntityName, CreateUser) VALUES(21, 'SchedulerAdminAccountAuthorizationUserStatus', 'sunil.thomas');
	INSERT INTO SecuredEntities (SecuredEntityID, SecuredEntityName, CreateUser) VALUES(22, 'SchedulerAdminAccountProcedures', 'sunil.thomas');
	INSERT INTO SecuredEntities (SecuredEntityID, SecuredEntityName, CreateUser) VALUES(23, 'SchedulerUserManagementSecureACLs', 'sunil.thomas');
	INSERT INTO SecuredEntities (SecuredEntityID, SecuredEntityName, CreateUser) VALUES(24, 'SchedulerProcedureAddToFavorites', 'sunil.thomas');
	INSERT INTO SecuredEntities (SecuredEntityID, SecuredEntityName, CreateUser) VALUES(25, 'SchedulerAdminAccountDiagnoses', 'sunil.thomas');
	INSERT INTO SecuredEntities (SecuredEntityID, SecuredEntityName, CreateUser) VALUES(26, 'SchedulerAdminAilmentList', 'sunil.thomas');
	INSERT INTO SecuredEntities (SecuredEntityID, SecuredEntityName, CreateUser) VALUES(27, 'SchedulerCompletedAppointments', 'sunil.thomas');
	INSERT INTO SecuredEntities (SecuredEntityID, SecuredEntityName, CreateUser) VALUES(28, 'SchedulerTestData', 'sunil.thomas');
	SET IDENTITY_INSERT SecuredEntities OFF;
end



END
GO