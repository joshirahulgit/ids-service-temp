USE [Global]
GO

/****** Object:  Table [dbo].[CodeTypes]    Script Date: 09/01/2011 11:13:22 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CodeTypes]') AND type in (N'U'))
DROP TABLE [dbo].[CodeTypes]
GO

CREATE TABLE [dbo].[CodeTypes](
	[CodeTypeID] [int] IDENTITY(1,1) NOT NULL,
	[CodeTypeName] [varchar](10) NOT NULL,
	[CodeTypeDescription] [varchar](250) NULL,
 CONSTRAINT [PK_CodeTypes] PRIMARY KEY CLUSTERED 
(
	[CodeTypeID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

INSERT INTO CodeTypes (CodeTypeName)VALUES('CPT')
INSERT INTO CodeTypes (CodeTypeName)VALUES('ICD9Diag')
INSERT INTO CodeTypes (CodeTypeName)VALUES('ICD9Proc')

/****** Object:  Table [dbo].[ICD9DiagCodes]    Script Date: 09/01/2011 11:14:34 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ICD9DiagCodes]') AND type in (N'U'))
DROP TABLE [dbo].[ICD9DiagCodes]
GO

CREATE TABLE [dbo].[ICD9DiagCodes](
	[ICD9Code] [varchar](10) NOT NULL,
	[ChangeIndicator] [char](1) NULL,
	[CodeStatus] [char](1) NULL,
	[ShortDesc] [varchar](30) NULL,
	[Mediumdesc] [varchar](50) NULL,
	[LongDesc] [varchar](500) NULL
) ON [PRIMARY]

GO

/****** Object:  Table [dbo].[ICD9ProcCodes]    Script Date: 09/01/2011 11:14:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ICD9ProcCodes]') AND type in (N'U'))
DROP TABLE [dbo].[ICD9ProcCodes]
GO

CREATE TABLE [dbo].[ICD9ProcCodes](
	[ICD9Code] [varchar](10) NULL,
	[ChangeIndicator] [char](1) NULL,
	[CodeStatus] [char](1) NULL,
	[ShortDesc] [varchar](30) NULL,
	[Mediumdesc] [varchar](50) NULL,
	[LongDesc] [varchar](500) NULL
) ON [PRIMARY]

GO