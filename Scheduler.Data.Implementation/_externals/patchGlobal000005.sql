USE [Global]
GO
/****** Object:  Table [dbo].[OrderParameters]    Script Date: 04/18/2012 17:08:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OrderParameters]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[OrderParameters](
	[OrderParameterId] [int] IDENTITY(1,1) NOT NULL,
	[ParameterName] [varchar](150) NOT NULL,
	[ParameterType] [varchar](50) NOT NULL,
	[IsSystemRequired] [bit] NOT NULL,
 CONSTRAINT [PK_OrderParameters] PRIMARY KEY CLUSTERED 
(
	[OrderParameterId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SchedulerOrderRefTypeMapping]    Script Date: 04/18/2012 17:08:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SchedulerOrderRefTypeMapping]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SchedulerOrderRefTypeMapping](
	[MappingID] [int] IDENTITY(1,1) NOT NULL,
	[AccountName] [varchar](128) NOT NULL,
	[RefferalType] [varchar](128) NOT NULL,
	[IsDictator] [bit] NOT NULL,
	[IsCC] [bit] NOT NULL,
 CONSTRAINT [PK_SchedulerOrderRefTypeMapping] PRIMARY KEY CLUSTERED 
(
	[MappingID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SchedulerOrderCreationModes]    Script Date: 04/18/2012 17:08:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SchedulerOrderCreationModes]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SchedulerOrderCreationModes](
	[SchedulerOrderCreationModeId] [int] IDENTITY(1,1) NOT NULL,
	[Mode] [varchar](50) NOT NULL,
	[Description] [varchar](max) NULL,
 CONSTRAINT [PK_SchedulerOrderCreationModes] PRIMARY KEY CLUSTERED 
(
	[SchedulerOrderCreationModeId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SchedulerConfigurations]    Script Date: 04/18/2012 17:08:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SchedulerConfigurations]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SchedulerConfigurations](
	[SchedulerConfigurationId] [int] IDENTITY(1,1) NOT NULL,
	[AccountId] [varchar](50) NOT NULL,
	[SchedulerOrderCreationModeId] [int] NOT NULL,
	[OrderCreationStatusTrigger] [varchar](50) NULL,
	[MapToWorkTypeSourceTable] [varchar](150) NULL,
	[MapToWorkTypeField] [varchar](150) NULL,
	[MapVisitTypeFrom] [varchar](200) NULL,
	[IsLocationFilterVis] [bit] NOT NULL,
	[IsModalityFilterVis] [bit] NOT NULL,
	[IsRoomFilterVis] [bit] NOT NULL,
	[IsRoleFilterVis] [bit] NOT NULL,
	[IsProviderFilterVis] [bit] NOT NULL,
	[IsApptStatusFilterVis] [bit] NOT NULL,
	[IsDaysFilterVis] [bit] NOT NULL,
	[IsPhyGroupVis] [bit] NOT NULL,
	[IsWtGroupVis] [bit] NOT NULL,
	[EnforceWorkDays] [bit] NOT NULL,
	[EnforceWorkTime] [bit] NOT NULL,
 CONSTRAINT [PK_SchedulerConfiguration] PRIMARY KEY CLUSTERED 
(
	[SchedulerConfigurationId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SchedulerOrderTransforms]    Script Date: 04/18/2012 17:08:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SchedulerOrderTransforms]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SchedulerOrderTransforms](
	[TransformId] [int] IDENTITY(1,1) NOT NULL,
	[SchedulerConfigurationId] [int] NOT NULL,
	[MapFieldValue] [varchar](500) NULL,
	[AccountWtValue] [varchar](50) NOT NULL,
	[MapFieldGroup] [varchar](50) NULL,
	[IsGroupPrompt] [bit] NOT NULL,
	[OverrideCreationMode] [int] NULL,
 CONSTRAINT [PK_SchedulerOrderTransforms] PRIMARY KEY CLUSTERED 
(
	[TransformId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SchedulerOrderParameters]    Script Date: 04/18/2012 17:08:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SchedulerOrderParameters]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SchedulerOrderParameters](
	[SchedulerOrderParameterId] [int] IDENTITY(1,1) NOT NULL,
	[SchedulerConfigurationId] [int] NOT NULL,
	[OrderParameterId] [int] NOT NULL,
	[ParameterDefaultValue] [varchar](500) NULL,
	[isRequired] [bit] NULL,
 CONSTRAINT [PK_SchedulerOrderParameters] PRIMARY KEY CLUSTERED 
(
	[SchedulerOrderParameterId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Default [DF_OrderParameters_IsSystemRequired]    Script Date: 04/18/2012 17:08:01 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_OrderParameters_IsSystemRequired]') AND parent_object_id = OBJECT_ID(N'[dbo].[OrderParameters]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_OrderParameters_IsSystemRequired]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[OrderParameters] ADD  CONSTRAINT [DF_OrderParameters_IsSystemRequired]  DEFAULT ((0)) FOR [IsSystemRequired]
END


End
GO
/****** Object:  Default [DF__Scheduler__IsLoc__795F91EE]    Script Date: 04/18/2012 17:08:01 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__Scheduler__IsLoc__795F91EE]') AND parent_object_id = OBJECT_ID(N'[dbo].[SchedulerConfigurations]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__Scheduler__IsLoc__795F91EE]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[SchedulerConfigurations] ADD  DEFAULT ((1)) FOR [IsLocationFilterVis]
END


End
GO
/****** Object:  Default [DF__Scheduler__IsMod__7A53B627]    Script Date: 04/18/2012 17:08:01 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__Scheduler__IsMod__7A53B627]') AND parent_object_id = OBJECT_ID(N'[dbo].[SchedulerConfigurations]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__Scheduler__IsMod__7A53B627]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[SchedulerConfigurations] ADD  DEFAULT ((1)) FOR [IsModalityFilterVis]
END


End
GO
/****** Object:  Default [DF__Scheduler__IsRoo__7B47DA60]    Script Date: 04/18/2012 17:08:01 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__Scheduler__IsRoo__7B47DA60]') AND parent_object_id = OBJECT_ID(N'[dbo].[SchedulerConfigurations]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__Scheduler__IsRoo__7B47DA60]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[SchedulerConfigurations] ADD  DEFAULT ((1)) FOR [IsRoomFilterVis]
END


End
GO
/****** Object:  Default [DF__Scheduler__IsRol__7C3BFE99]    Script Date: 04/18/2012 17:08:01 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__Scheduler__IsRol__7C3BFE99]') AND parent_object_id = OBJECT_ID(N'[dbo].[SchedulerConfigurations]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__Scheduler__IsRol__7C3BFE99]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[SchedulerConfigurations] ADD  DEFAULT ((1)) FOR [IsRoleFilterVis]
END


End
GO
/****** Object:  Default [DF__Scheduler__IsPro__7D3022D2]    Script Date: 04/18/2012 17:08:01 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__Scheduler__IsPro__7D3022D2]') AND parent_object_id = OBJECT_ID(N'[dbo].[SchedulerConfigurations]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__Scheduler__IsPro__7D3022D2]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[SchedulerConfigurations] ADD  DEFAULT ((1)) FOR [IsProviderFilterVis]
END


End
GO
/****** Object:  Default [DF__Scheduler__IsApp__7E24470B]    Script Date: 04/18/2012 17:08:01 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__Scheduler__IsApp__7E24470B]') AND parent_object_id = OBJECT_ID(N'[dbo].[SchedulerConfigurations]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__Scheduler__IsApp__7E24470B]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[SchedulerConfigurations] ADD  DEFAULT ((1)) FOR [IsApptStatusFilterVis]
END


End
GO
/****** Object:  Default [DF__Scheduler__IsDay__7F186B44]    Script Date: 04/18/2012 17:08:01 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__Scheduler__IsDay__7F186B44]') AND parent_object_id = OBJECT_ID(N'[dbo].[SchedulerConfigurations]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__Scheduler__IsDay__7F186B44]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[SchedulerConfigurations] ADD  DEFAULT ((1)) FOR [IsDaysFilterVis]
END


End
GO
/****** Object:  Default [DF__Scheduler__IsPhy__000C8F7D]    Script Date: 04/18/2012 17:08:01 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__Scheduler__IsPhy__000C8F7D]') AND parent_object_id = OBJECT_ID(N'[dbo].[SchedulerConfigurations]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__Scheduler__IsPhy__000C8F7D]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[SchedulerConfigurations] ADD  DEFAULT ((1)) FOR [IsPhyGroupVis]
END


End
GO
/****** Object:  Default [DF__Scheduler__IsWtG__0100B3B6]    Script Date: 04/18/2012 17:08:01 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__Scheduler__IsWtG__0100B3B6]') AND parent_object_id = OBJECT_ID(N'[dbo].[SchedulerConfigurations]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__Scheduler__IsWtG__0100B3B6]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[SchedulerConfigurations] ADD  DEFAULT ((1)) FOR [IsWtGroupVis]
END


End
GO
/****** Object:  Default [DF__Scheduler__Enfor__01F4D7EF]    Script Date: 04/18/2012 17:08:01 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__Scheduler__Enfor__01F4D7EF]') AND parent_object_id = OBJECT_ID(N'[dbo].[SchedulerConfigurations]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__Scheduler__Enfor__01F4D7EF]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[SchedulerConfigurations] ADD  DEFAULT ((0)) FOR [EnforceWorkDays]
END


End
GO
/****** Object:  Default [DF__Scheduler__Enfor__02E8FC28]    Script Date: 04/18/2012 17:08:01 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__Scheduler__Enfor__02E8FC28]') AND parent_object_id = OBJECT_ID(N'[dbo].[SchedulerConfigurations]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__Scheduler__Enfor__02E8FC28]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[SchedulerConfigurations] ADD  DEFAULT ((0)) FOR [EnforceWorkTime]
END


End
GO
/****** Object:  Default [DF__Scheduler__IsGro__5F9FBFEB]    Script Date: 04/18/2012 17:08:01 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__Scheduler__IsGro__5F9FBFEB]') AND parent_object_id = OBJECT_ID(N'[dbo].[SchedulerOrderTransforms]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__Scheduler__IsGro__5F9FBFEB]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[SchedulerOrderTransforms] ADD  DEFAULT ((0)) FOR [IsGroupPrompt]
END


End
GO
/****** Object:  ForeignKey [FK_SchedulerConfigurations_SchedulerOrderCreationModes]    Script Date: 04/18/2012 17:08:01 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SchedulerConfigurations_SchedulerOrderCreationModes]') AND parent_object_id = OBJECT_ID(N'[dbo].[SchedulerConfigurations]'))
ALTER TABLE [dbo].[SchedulerConfigurations]  WITH CHECK ADD  CONSTRAINT [FK_SchedulerConfigurations_SchedulerOrderCreationModes] FOREIGN KEY([SchedulerOrderCreationModeId])
REFERENCES [dbo].[SchedulerOrderCreationModes] ([SchedulerOrderCreationModeId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SchedulerConfigurations_SchedulerOrderCreationModes]') AND parent_object_id = OBJECT_ID(N'[dbo].[SchedulerConfigurations]'))
ALTER TABLE [dbo].[SchedulerConfigurations] CHECK CONSTRAINT [FK_SchedulerConfigurations_SchedulerOrderCreationModes]
GO
/****** Object:  ForeignKey [FK_SchedulerOrderParameters_OrderParameters]    Script Date: 04/18/2012 17:08:01 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SchedulerOrderParameters_OrderParameters]') AND parent_object_id = OBJECT_ID(N'[dbo].[SchedulerOrderParameters]'))
ALTER TABLE [dbo].[SchedulerOrderParameters]  WITH CHECK ADD  CONSTRAINT [FK_SchedulerOrderParameters_OrderParameters] FOREIGN KEY([OrderParameterId])
REFERENCES [dbo].[OrderParameters] ([OrderParameterId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SchedulerOrderParameters_OrderParameters]') AND parent_object_id = OBJECT_ID(N'[dbo].[SchedulerOrderParameters]'))
ALTER TABLE [dbo].[SchedulerOrderParameters] CHECK CONSTRAINT [FK_SchedulerOrderParameters_OrderParameters]
GO
/****** Object:  ForeignKey [FK_SchedulerOrderParameters_SchedulerConfigurations]    Script Date: 04/18/2012 17:08:01 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SchedulerOrderParameters_SchedulerConfigurations]') AND parent_object_id = OBJECT_ID(N'[dbo].[SchedulerOrderParameters]'))
ALTER TABLE [dbo].[SchedulerOrderParameters]  WITH CHECK ADD  CONSTRAINT [FK_SchedulerOrderParameters_SchedulerConfigurations] FOREIGN KEY([SchedulerConfigurationId])
REFERENCES [dbo].[SchedulerConfigurations] ([SchedulerConfigurationId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SchedulerOrderParameters_SchedulerConfigurations]') AND parent_object_id = OBJECT_ID(N'[dbo].[SchedulerOrderParameters]'))
ALTER TABLE [dbo].[SchedulerOrderParameters] CHECK CONSTRAINT [FK_SchedulerOrderParameters_SchedulerConfigurations]
GO
/****** Object:  ForeignKey [FK_SchedulerOrderTransforms_SchedulerConfigurations]    Script Date: 04/18/2012 17:08:01 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SchedulerOrderTransforms_SchedulerConfigurations]') AND parent_object_id = OBJECT_ID(N'[dbo].[SchedulerOrderTransforms]'))
ALTER TABLE [dbo].[SchedulerOrderTransforms]  WITH CHECK ADD  CONSTRAINT [FK_SchedulerOrderTransforms_SchedulerConfigurations] FOREIGN KEY([SchedulerConfigurationId])
REFERENCES [dbo].[SchedulerConfigurations] ([SchedulerConfigurationId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SchedulerOrderTransforms_SchedulerConfigurations]') AND parent_object_id = OBJECT_ID(N'[dbo].[SchedulerOrderTransforms]'))
ALTER TABLE [dbo].[SchedulerOrderTransforms] CHECK CONSTRAINT [FK_SchedulerOrderTransforms_SchedulerConfigurations]
GO
