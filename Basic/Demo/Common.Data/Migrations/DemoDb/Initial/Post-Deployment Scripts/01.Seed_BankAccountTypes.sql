SET IDENTITY_INSERT [dbo].[BankAccountType] ON;
GO

INSERT INTO [dbo].[BankAccountType](BankAccountTypeId, BankAccountTypeName) VALUES (1, 'Checking Account')

INSERT INTO [dbo].[BankAccountType](BankAccountTypeId, BankAccountTypeName) VALUES (2, 'Savings Account')

SET IDENTITY_INSERT [dbo].[BankAccountType] OFF;
GO