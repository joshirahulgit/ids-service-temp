USE [Global]
GO

IF NOT EXISTS (SELECT *
			   FROM
				   sys.objects
			   WHERE
				   object_id = object_id(N'[dbo].[AccountGenerateIDconfigs]')
				   AND type IN (N'U'))
	CREATE TABLE AccountGenerateIDconfigs(
		AccountGenerateIDconfigId INT IDENTITY (1, 1) NOT NULL,
		AccountId VARCHAR(50) NOT NULL,
		Location VARCHAR(50) NULL,
		CustomLocationCode VARCHAR(5) NULL,
		IdTypeName VARCHAR(10) NOT NULL,
		IDFormatString VARCHAR(50) NOT NULL,
		PreFix VARCHAR(10) NULL,
		PostFix VARCHAR(10) NULL,
		StartingSeq INT NULL,
		NextAvailableSeq INT NULL,
		IsSeqPadded BIT NULL,
		SeqPaddingLen INT NULL,
		SeqPaddingChar CHAR(1) NULL,
		SeqPaddingDir CHAR(1) NULL,
		UseGuid BIT NOT NULL,
		GuidLen INT NULL,
		CONSTRAINT PK_AccountGenerateIDconfigs PRIMARY KEY CLUSTERED (AccountGenerateIDconfigId ASC)
	)
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'e.g. of a format string 
	[Pre]?[Loc]?[Seq]?[Post]
	? = seperator
	[Pre] = PreFix
	[Loc] = CustomLocationCode (if not null) else Location
	[Seq] = NextAvailableSeq (with padding if configured)
	[Post] = PostFix
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AccountGenerateIDconfigs', @level2type=N'COLUMN',@level2name=N'IDFormatString'
GO


IF NOT EXISTS (SELECT *
			   FROM
				   dbo.sysobjects
			   WHERE
				   id = object_id(N'[DF_AccountGenerateIDconfigs_IDFormatString_Default]')
				   AND type = 'D')
	ALTER TABLE dbo.AccountGenerateIDconfigs ADD CONSTRAINT DF_AccountGenerateIDconfigs_IDFormatString_Default DEFAULT ('[Seq]') FOR [IDFormatString]
GO

IF NOT EXISTS (SELECT *
			   FROM
				   dbo.sysobjects
			   WHERE
				   id = object_id(N'[DF_AccountGenerateIDconfigs_UseGuid_Default]')
				   AND type = 'D')
	ALTER TABLE dbo.AccountGenerateIDconfigs ADD CONSTRAINT DF_AccountGenerateIDconfigs_UseGuid_Default DEFAULT (0) FOR [UseGuid]
GO

IF NOT EXISTS (SELECT *
			   FROM
				   sys.check_constraints
			   WHERE
				   object_id = object_id(N'[dbo].[CK_AccountGenerateIDconfigs_IdTypeName]')
				   AND parent_object_id = object_id(N'[dbo].[AccountGenerateIDconfigs]'))
BEGIN
	ALTER TABLE dbo.AccountGenerateIDconfigs WITH CHECK ADD CONSTRAINT [CK_AccountGenerateIDconfigs_IdTypeName] CHECK (IdTypeName IN ('MRN', 'ORDER', 'JOB'))
	ALTER TABLE [dbo].[AccountGenerateIDconfigs] CHECK CONSTRAINT [CK_AccountGenerateIDconfigs_IdTypeName]
END
GO

IF NOT EXISTS (SELECT *
			   FROM
				   sys.check_constraints
			   WHERE
				   object_id = object_id(N'[dbo].[CK_AccountGenerateIDconfigs_SeqPaddingDir]')
				   AND parent_object_id = object_id(N'[dbo].[AccountGenerateIDconfigs]'))
BEGIN
	ALTER TABLE dbo.AccountGenerateIDconfigs WITH CHECK ADD CONSTRAINT [CK_AccountGenerateIDconfigs_SeqPaddingDir] CHECK (SeqPaddingDir IN (NULL, 'L', 'R'))
	ALTER TABLE [dbo].[AccountGenerateIDconfigs] CHECK CONSTRAINT [CK_AccountGenerateIDconfigs_SeqPaddingDir]
END
GO

IF NOT EXISTS (SELECT *
			   FROM
				   sys.indexes
			   WHERE
				   object_id = object_id(N'[dbo].[AccountGenerateIDconfigs]')
				   AND name = N'IDX_UniquePerAccountLocationType')
	CREATE UNIQUE NONCLUSTERED INDEX [IDX_UniquePerAccountLocationType] ON [dbo].[AccountGenerateIDconfigs] ([AccountId] ASC, [Location] ASC, [IdTypeName] ASC)
GO