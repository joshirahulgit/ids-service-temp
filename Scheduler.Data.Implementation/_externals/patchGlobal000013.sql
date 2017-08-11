use Global


IF NOT EXISTS (SELECT * FROM sys.columns c WHERE c.name = 'VisitCreationTrigger' AND c.object_id = OBJECT_ID('SchedulerConfigurations'))
	ALTER TABLE SchedulerConfigurations ADD VisitCreationTrigger [varchar](50) NULL
	
GO