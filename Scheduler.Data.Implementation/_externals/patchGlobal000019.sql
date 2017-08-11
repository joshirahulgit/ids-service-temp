USE [Global]
GO

IF NOT exists(SELECT * FROM SecuredEntities where SecuredEntityID BETWEEN 46 AND 46)
begin
	SET IDENTITY_INSERT SecuredEntities ON;
	INSERT INTO SecuredEntities (SecuredEntityID, SecuredEntityName, CreateUser) VALUES(46, 'SchedulerDoubleBooking', 'sunil.thomas');
	SET IDENTITY_INSERT SecuredEntities OFF;											
end																						
																						
