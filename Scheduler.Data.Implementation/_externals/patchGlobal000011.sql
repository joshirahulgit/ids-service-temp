USE [Global]
GO

IF NOT exists(SELECT * FROM SecuredEntities where SecuredEntityID BETWEEN 29 AND 199)
begin
	SET IDENTITY_INSERT SecuredEntities ON;
	INSERT INTO SecuredEntities (SecuredEntityID, SecuredEntityName, CreateUser) VALUES(29, 'SchedulerAdminAccountSettings', 'sunil.thomas');
	INSERT INTO SecuredEntities (SecuredEntityID, SecuredEntityName, CreateUser) VALUES(30, 'SchedulerAdminAccountReferralGroups', 'sunil.thomas');
	INSERT INTO SecuredEntities (SecuredEntityID, SecuredEntityName, CreateUser) VALUES(31, 'SchedulerAdminAccountDiagnosisFlags', 'sunil.thomas');
	INSERT INTO SecuredEntities (SecuredEntityID, SecuredEntityName, CreateUser) VALUES(32, 'SchedulerAdminAccountCreditCardTypes', 'sunil.thomas');
	INSERT INTO SecuredEntities (SecuredEntityID, SecuredEntityName, CreateUser) VALUES(33, 'SchedulerAdminAccountPaymentStatuses', 'sunil.thomas');
	INSERT INTO SecuredEntities (SecuredEntityID, SecuredEntityName, CreateUser) VALUES(34, 'SchedulerAdminAccountPayerCrosswalk', 'sunil.thomas');
	INSERT INTO SecuredEntities (SecuredEntityID, SecuredEntityName, CreateUser) VALUES(35, 'SchedulerAdminAccountFilters', 'sunil.thomas');
	INSERT INTO SecuredEntities (SecuredEntityID, SecuredEntityName, CreateUser) VALUES(36, 'SchedulerAdminAccountMammoBirads', 'sunil.thomas');
	INSERT INTO SecuredEntities (SecuredEntityID, SecuredEntityName, CreateUser) VALUES(37, 'SchedulerAdminAccountMammoBreastDensity', 'sunil.thomas');
	INSERT INTO SecuredEntities (SecuredEntityID, SecuredEntityName, CreateUser) VALUES(38, 'SchedulerAdminAccountMammoLaterality', 'sunil.thomas');
	INSERT INTO SecuredEntities (SecuredEntityID, SecuredEntityName, CreateUser) VALUES(39, 'SchedulerAdminAccountMammoNodalStatuses', 'sunil.thomas');
	INSERT INTO SecuredEntities (SecuredEntityID, SecuredEntityName, CreateUser) VALUES(40, 'SchedulerAdminAccountMammoTumorSizes', 'sunil.thomas');
	INSERT INTO SecuredEntities (SecuredEntityID, SecuredEntityName, CreateUser) VALUES(41, 'SchedulerAdminAccountMammoBiopsytypes', 'sunil.thomas');
	SET IDENTITY_INSERT SecuredEntities OFF;											
end																						
																						
