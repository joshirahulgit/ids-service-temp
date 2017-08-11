USE [Global]
GO

IF NOT exists(SELECT * FROM SecuredEntities where SecuredEntityID BETWEEN 42 AND 42)
begin
	SET IDENTITY_INSERT SecuredEntities ON;
	INSERT INTO SecuredEntities (SecuredEntityID, SecuredEntityName, CreateUser) VALUES(42, 'SchedulerPatientComments', 'sunil.thomas');
	SET IDENTITY_INSERT SecuredEntities OFF;											
end																						
																						
