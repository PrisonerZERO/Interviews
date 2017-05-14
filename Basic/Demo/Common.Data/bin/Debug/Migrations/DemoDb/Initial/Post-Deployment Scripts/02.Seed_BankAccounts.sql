DECLARE @ExecutedByName NVARCHAR(400) = 'bushido/systemseed'
DECLARE @ExecutedDatetime DATETIME = (SELECT GETUTCDATE());

DECLARE @CheckingAccountId INT = 1;
DECLARE @SavingsAccountId INT = 2;

INSERT INTO [data].[BankAccount](BankAccountTypeId, OwnerFullName, Balance, AnnualPercentageRate, ExecutedByName, ExecutedDatetime) 
VALUES (@CheckingAccountId, 'Jamie Sharon', 100.00, 1.6, @ExecutedByName, @ExecutedDatetime)

INSERT INTO [data].[BankAccount](BankAccountTypeId, OwnerFullName, Balance, AnnualPercentageRate, ExecutedByName, ExecutedDatetime) 
VALUES (@CheckingAccountId, 'Rhoda Trigg', 2500.00, 6.6, @ExecutedByName, @ExecutedDatetime)

INSERT INTO [data].[BankAccount](BankAccountTypeId, OwnerFullName, Balance, AnnualPercentageRate, ExecutedByName, ExecutedDatetime) 
VALUES (@CheckingAccountId, 'Denny Sapp', 50.00, 1.6, @ExecutedByName, @ExecutedDatetime)

INSERT INTO [data].[BankAccount](BankAccountTypeId, OwnerFullName, Balance, AnnualPercentageRate, ExecutedByName, ExecutedDatetime) 
VALUES (@SavingsAccountId, 'Grisel Piraino', 30000.00, 1.6, @ExecutedByName, @ExecutedDatetime)

INSERT INTO [data].[BankAccount](BankAccountTypeId, OwnerFullName, Balance, AnnualPercentageRate, ExecutedByName, ExecutedDatetime) 
VALUES (@SavingsAccountId, 'Verdie Turnbough', 1780.00, 1.6, @ExecutedByName, @ExecutedDatetime)
GO