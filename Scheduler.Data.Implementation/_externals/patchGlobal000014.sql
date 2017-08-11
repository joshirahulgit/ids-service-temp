
USE [InsuranceVerification]
GO
IF NOT EXISTS (SELECT
		*
	FROM sys.indexes
	WHERE name = 'payeraddresses_payerId'
	AND object_id = OBJECT_ID('PayerAddresses'))
BEGIN
	CREATE NONCLUSTERED INDEX [payeraddresses_payerId]
	ON [dbo].[PayerAddresses] ([PayerID])
	INCLUDE ([PayerAddressID])
END
GO