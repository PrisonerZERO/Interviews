SET IDENTITY_INSERT [data].[BankAccountType] ON;
GO

DECLARE @ExecutedByName NVARCHAR(400) = 'bushido/systemseed'
DECLARE @ExecutedDatetime DATETIME = (SELECT GETUTCDATE());

INSERT INTO [data].[BankAccountType](BankAccountTypeId, BankAccountTypeName, ExecutedByName, ExecutedDatetime) VALUES (1, 'Checking Account', @ExecutedByName, @ExecutedDatetime)

INSERT INTO [data].[BankAccountType](BankAccountTypeId, BankAccountTypeName, ExecutedByName, ExecutedDatetime) VALUES (2, 'Savings Account', @ExecutedByName, @ExecutedDatetime)

SET IDENTITY_INSERT [data].[BankAccountType] OFF;
GO