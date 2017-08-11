USE InsuranceVerification

IF NOT EXISTS (SELECT * FROM sys.columns c WHERE c.name = 'IsActive' AND c.object_id = OBJECT_ID('Payers'))
	ALTER TABLE Payers ADD IsActive bit NOT NULL CONSTRAINT DF_Payers_IsActive DEFAULT (1);