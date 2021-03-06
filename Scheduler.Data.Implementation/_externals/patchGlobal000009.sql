USE [Global]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SnomedCodes]') AND type in (N'U'))
DROP TABLE [dbo].[SnomedCodes]
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SnomedCodes]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SnomedCodes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SnomedCode] [varchar](50) NOT NULL,
	[ShortDescription] [varchar](max) NOT NULL,
	[MediumDescription] [varchar](max) NULL,
	[LongDescription] [varchar](max) NULL,
	[IsEncounterCode] [bit] NOT NULL,
 CONSTRAINT [PK_SnomedCodes] PRIMARY KEY CLUSTERED 
(
	[SnomedCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET IDENTITY_INSERT [dbo].[SnomedCodes] ON
INSERT [dbo].[SnomedCodes] ([Id], [SnomedCode], [ShortDescription], [MediumDescription], [LongDescription], [IsEncounterCode]) VALUES (4, N'102449007', N'Tardive dyskinesia', N'Tardive dyskinesia (dysoster)', N'Tardive dyskinesia', 0)
INSERT [dbo].[SnomedCodes] ([Id], [SnomedCode], [ShortDescription], [MediumDescription], [LongDescription], [IsEncounterCode]) VALUES (1, N'163020007', N'blood pressure reading', N'blood pressure reading', N'blood pressure reading', 0)
INSERT [dbo].[SnomedCodes] ([Id], [SnomedCode], [ShortDescription], [MediumDescription], [LongDescription], [IsEncounterCode]) VALUES (2, N'371911009', N'Measurement of blood pressure using cuff method', N'Measurement of blood pressure using cuff method', N'Measurement of blood pressure using cuff method', 0)
SET IDENTITY_INSERT [dbo].[SnomedCodes] OFF
GO
