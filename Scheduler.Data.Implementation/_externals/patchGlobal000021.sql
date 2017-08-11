USE [Global]
GO

IF NOT exists(SELECT * FROM SecuredEntities where SecuredEntityID BETWEEN 48 AND 48)
begin
	SET IDENTITY_INSERT SecuredEntities ON;
	INSERT INTO SecuredEntities (SecuredEntityID, SecuredEntityName, CreateUser) VALUES(48, 'SchedulerDemographicsTab', 'sunil.thomas');
	SET IDENTITY_INSERT SecuredEntities OFF;											
end																						
																						

IF NOT exists(SELECT * FROM SecuredEntities where SecuredEntityID BETWEEN 49 AND 49)
begin
	SET IDENTITY_INSERT SecuredEntities ON;
	INSERT INTO SecuredEntities (SecuredEntityID, SecuredEntityName, CreateUser) VALUES(49, 'SchedulerMammographyTab', 'sunil.thomas');
	SET IDENTITY_INSERT SecuredEntities OFF;											
end		

IF NOT exists(SELECT * FROM SecuredEntities where SecuredEntityID BETWEEN 50 AND 50)
begin
	SET IDENTITY_INSERT SecuredEntities ON;
	INSERT INTO SecuredEntities (SecuredEntityID, SecuredEntityName, CreateUser) VALUES(50, 'SchedulerPayerInformationTab', 'sunil.thomas');
	SET IDENTITY_INSERT SecuredEntities OFF;											
end		

IF NOT exists(SELECT * FROM SecuredEntities where SecuredEntityID BETWEEN 51 AND 51)
begin
	SET IDENTITY_INSERT SecuredEntities ON;
	INSERT INTO SecuredEntities (SecuredEntityID, SecuredEntityName, CreateUser) VALUES(51, 'SchedulerAuthorizationTab', 'sunil.thomas');
	SET IDENTITY_INSERT SecuredEntities OFF;											
end		

IF NOT exists(SELECT * FROM SecuredEntities where SecuredEntityID BETWEEN 52 AND 52)
begin
	SET IDENTITY_INSERT SecuredEntities ON;
	INSERT INTO SecuredEntities (SecuredEntityID, SecuredEntityName, CreateUser) VALUES(52, 'SchedulerVisitHistoryTab', 'sunil.thomas');
	SET IDENTITY_INSERT SecuredEntities OFF;											
end		

IF NOT exists(SELECT * FROM SecuredEntities where SecuredEntityID BETWEEN 53 AND 53)
begin
	SET IDENTITY_INSERT SecuredEntities ON;
	INSERT INTO SecuredEntities (SecuredEntityID, SecuredEntityName, CreateUser) VALUES(53, 'SchedulerGuarantorTab', 'sunil.thomas');
	SET IDENTITY_INSERT SecuredEntities OFF;											
end		

IF NOT exists(SELECT * FROM SecuredEntities where SecuredEntityID BETWEEN 54 AND 54)
begin
	SET IDENTITY_INSERT SecuredEntities ON;
	INSERT INTO SecuredEntities (SecuredEntityID, SecuredEntityName, CreateUser) VALUES(54, 'SchedulerPaymentsTab', 'sunil.thomas');
	SET IDENTITY_INSERT SecuredEntities OFF;											
end		

IF NOT exists(SELECT * FROM SecuredEntities where SecuredEntityID BETWEEN 55 AND 55)
begin
	SET IDENTITY_INSERT SecuredEntities ON;
	INSERT INTO SecuredEntities (SecuredEntityID, SecuredEntityName, CreateUser) VALUES(55, 'SchedulerContactsTab', 'sunil.thomas');
	SET IDENTITY_INSERT SecuredEntities OFF;											
end		
IF NOT exists(SELECT * FROM SecuredEntities where SecuredEntityID BETWEEN 56 AND 56)
begin
	SET IDENTITY_INSERT SecuredEntities ON;
	INSERT INTO SecuredEntities (SecuredEntityID, SecuredEntityName, CreateUser) VALUES(56, 'SchedulerAppointmentChecklistTab', 'sunil.thomas');
	SET IDENTITY_INSERT SecuredEntities OFF;											
end		
IF NOT exists(SELECT * FROM SecuredEntities where SecuredEntityID BETWEEN 57 AND 57)
begin
	SET IDENTITY_INSERT SecuredEntities ON;
	INSERT INTO SecuredEntities (SecuredEntityID, SecuredEntityName, CreateUser) VALUES(57, 'SchedulerReferralTab', 'sunil.thomas');
	SET IDENTITY_INSERT SecuredEntities OFF;											
end		

IF NOT exists(SELECT * FROM SecuredEntities where SecuredEntityID BETWEEN 58 AND 58)
begin
	SET IDENTITY_INSERT SecuredEntities ON;
	INSERT INTO SecuredEntities (SecuredEntityID, SecuredEntityName, CreateUser) VALUES(58, 'SchedulerCptTab', 'sunil.thomas');
	SET IDENTITY_INSERT SecuredEntities OFF;											
end		
IF NOT exists(SELECT * FROM SecuredEntities where SecuredEntityID BETWEEN 59 AND 59)
begin
	SET IDENTITY_INSERT SecuredEntities ON;
	INSERT INTO SecuredEntities (SecuredEntityID, SecuredEntityName, CreateUser) VALUES(59, 'SchedulerVisitInformationTab', 'sunil.thomas');
	SET IDENTITY_INSERT SecuredEntities OFF;											
end		

IF NOT exists(SELECT * FROM SecuredEntities where SecuredEntityID BETWEEN 60 AND 60)
begin
	SET IDENTITY_INSERT SecuredEntities ON;
	INSERT INTO SecuredEntities (SecuredEntityID, SecuredEntityName, CreateUser) VALUES(60, 'SchedulerPatientCommentsTab', 'sunil.thomas');
	SET IDENTITY_INSERT SecuredEntities OFF;											
end		