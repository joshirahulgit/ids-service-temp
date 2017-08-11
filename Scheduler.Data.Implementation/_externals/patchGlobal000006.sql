USE [InsuranceVerification]
GO
INSERT [dbo].[DependentRelationshipCodes] ([VendorID], [Code], [CodeDesc], [LastDate]) VALUES (1, N'34', N'Legal Guardian', getdate())
INSERT [dbo].[DependentRelationshipCodes] ([VendorID], [Code], [CodeDesc], [LastDate]) VALUES (1, N'34', N'Parent', getdate())
