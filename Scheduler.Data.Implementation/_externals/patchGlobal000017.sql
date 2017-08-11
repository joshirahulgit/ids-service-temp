USE [Global]
GO

IF NOT exists(SELECT * FROM SecuredEntities where SecuredEntityID BETWEEN 44 AND 44)
begin
	SET IDENTITY_INSERT SecuredEntities ON;
	INSERT INTO SecuredEntities (SecuredEntityID, SecuredEntityName, CreateUser) VALUES(44, 'SchedulerExamDuration', 'sunil.thomas');
	SET IDENTITY_INSERT SecuredEntities OFF;											
end																						
																						
