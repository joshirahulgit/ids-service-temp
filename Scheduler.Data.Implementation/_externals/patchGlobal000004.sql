USE [Global]
GO

IF  NOT EXISTS (SELECT * FROM sys.columns c WHERE c.object_id = OBJECT_ID(N'[dbo].[SchedulerConfigurations]') AND name = 'IsLocationFilterVis')
ALTER TABLE [dbo].[SchedulerConfigurations] ADD [IsLocationFilterVis] [bit] NOT NULL DEFAULT (1);

IF  NOT EXISTS (SELECT * FROM sys.columns c WHERE c.object_id = OBJECT_ID(N'[dbo].[SchedulerConfigurations]') AND name = 'IsModalityFilterVis')
ALTER TABLE [dbo].[SchedulerConfigurations] ADD [IsModalityFilterVis] [bit] NOT NULL DEFAULT (1);

IF  NOT EXISTS (SELECT * FROM sys.columns c WHERE c.object_id = OBJECT_ID(N'[dbo].[SchedulerConfigurations]') AND name = 'IsRoomFilterVis')
ALTER TABLE [dbo].[SchedulerConfigurations] ADD [IsRoomFilterVis] [bit] NOT NULL DEFAULT (1);

IF  NOT EXISTS (SELECT * FROM sys.columns c WHERE c.object_id = OBJECT_ID(N'[dbo].[SchedulerConfigurations]') AND name = 'IsRoleFilterVis')
ALTER TABLE [dbo].[SchedulerConfigurations] ADD [IsRoleFilterVis] [bit] NOT NULL DEFAULT (1);

IF  NOT EXISTS (SELECT * FROM sys.columns c WHERE c.object_id = OBJECT_ID(N'[dbo].[SchedulerConfigurations]') AND name = 'IsProviderFilterVis')
ALTER TABLE [dbo].[SchedulerConfigurations] ADD [IsProviderFilterVis] [bit] NOT NULL DEFAULT (1);

IF  NOT EXISTS (SELECT * FROM sys.columns c WHERE c.object_id = OBJECT_ID(N'[dbo].[SchedulerConfigurations]') AND name = 'IsApptStatusFilterVis')
ALTER TABLE [dbo].[SchedulerConfigurations] ADD [IsApptStatusFilterVis] [bit] NOT NULL DEFAULT (1);

IF  NOT EXISTS (SELECT * FROM sys.columns c WHERE c.object_id = OBJECT_ID(N'[dbo].[SchedulerConfigurations]') AND name = 'IsDaysFilterVis')
ALTER TABLE [dbo].[SchedulerConfigurations] ADD [IsDaysFilterVis] [bit] NOT NULL DEFAULT (1);

IF  NOT EXISTS (SELECT * FROM sys.columns c WHERE c.object_id = OBJECT_ID(N'[dbo].[SchedulerConfigurations]') AND name = 'IsPhyGroupVis')
ALTER TABLE [dbo].[SchedulerConfigurations] ADD [IsPhyGroupVis] [bit] NOT NULL DEFAULT (1);

IF  NOT EXISTS (SELECT * FROM sys.columns c WHERE c.object_id = OBJECT_ID(N'[dbo].[SchedulerConfigurations]') AND name = 'IsWtGroupVis')
ALTER TABLE [dbo].[SchedulerConfigurations] ADD [IsWtGroupVis] [bit] NOT NULL DEFAULT (1);

IF  NOT EXISTS (SELECT * FROM sys.columns c WHERE c.object_id = OBJECT_ID(N'[dbo].[SchedulerConfigurations]') AND name = 'EnforceWorkDays')
ALTER TABLE [dbo].[SchedulerConfigurations] ADD [EnforceWorkDays] [bit] NOT NULL DEFAULT (0);

IF  NOT EXISTS (SELECT * FROM sys.columns c WHERE c.object_id = OBJECT_ID(N'[dbo].[SchedulerConfigurations]') AND name = 'EnforceWorkTime')
ALTER TABLE [dbo].[SchedulerConfigurations] ADD [EnforceWorkTime] [bit] NOT NULL DEFAULT (0);
	
GO

