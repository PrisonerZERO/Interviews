DECLARE @CheckingAccountId INT = 1;
DECLARE @SavingsAccountId INT = 2;

INSERT INTO [dbo].[BankAccount](BankAccountTypeId, OwnerFullName, Balance, AnnualPercentageRate) 
VALUES (@CheckingAccountId, 'Jamie Sharon', 100.00, 1.6)

INSERT INTO [dbo].[BankAccount](BankAccountTypeId, OwnerFullName, Balance, AnnualPercentageRate) 
VALUES (@CheckingAccountId, 'Rhoda Trigg', 2500.00, 6.6)

INSERT INTO [dbo].[BankAccount](BankAccountTypeId, OwnerFullName, Balance, AnnualPercentageRate) 
VALUES (@CheckingAccountId, 'Denny Sapp', 50.00, 1.6)

INSERT INTO [dbo].[BankAccount](BankAccountTypeId, OwnerFullName, Balance, AnnualPercentageRate) 
VALUES (@SavingsAccountId, 'Grisel Piraino', 30000.00, 1.6)

INSERT INTO [dbo].[BankAccount](BankAccountTypeId, OwnerFullName, Balance, AnnualPercentageRate) 
VALUES (@SavingsAccountId, 'Verdie Turnbough', 1780.00, 1.6)
GO