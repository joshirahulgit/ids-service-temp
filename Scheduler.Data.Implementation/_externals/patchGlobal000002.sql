USE [Global]
GO
SET IDENTITY_INSERT SchedulerOrderCreationModes ON 
GO
INSERT INTO [Global].[dbo].[SchedulerOrderCreationModes]
           ([SchedulerOrderCreationModeId],[Mode]
           ,[Description])
     VALUES
           (0, 'NotSpecified','Not Specified')
           
SET IDENTITY_INSERT SchedulerOrderCreationModes OFF 
GO


GO