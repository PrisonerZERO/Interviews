IF EXISTS(SELECT 1 FROM sys.triggers sys_triggers INNER JOIN sys.tables sys_tables ON sys_tables.OBJECT_ID = sys_triggers.parent_id
		  WHERE sys_tables.SCHEMA_ID = SCHEMA_ID('data') AND sys_triggers.name = 'trg_diu_BankAccount') 
BEGIN
	DROP TRIGGER [data].[trg_diu_BankAccount]
END
GO

CREATE TRIGGER [data].[trg_diu_BankAccount]
   ON  [data].[BankAccount]
   AFTER INSERT, DELETE, UPDATE NOT FOR REPLICATION
AS
  BEGIN
	/*
		Inserts transactional audit records into a history table
	*/

	SET NOCOUNT ON;

	DECLARE @Type VARCHAR(20);

	IF EXISTS(SELECT * FROM INSERTED)
	BEGIN
		IF EXISTS(SELECT * FROM DELETED)
		BEGIN
			SET @Type ='UPDATED';
		END
		ELSE
		BEGIN
			SET @Type ='INSERTED';
		END

		INSERT INTO [data].[BankAccountHistory]
		(
			BankAccountId
			,BankAccountTypeId
			,OwnerFullName
			,Balance
			,AnnualPercentageRate
			,ExecutedByName
			,ExecutedDatetime
			,TransactionTypeName
		)
		SELECT 
			BankAccountId
			,BankAccountTypeId
			,OwnerFullName
			,Balance
			,AnnualPercentageRate
			,ExecutedByName
			,ExecutedDatetime
			,@Type
		FROM INSERTED
                    
	END
	ELSE
	BEGIN
		SET @type = 'DELETED';
                    
		INSERT INTO [data].[BankAccountHistory]
		(
			BankAccountId
			,BankAccountTypeId
			,OwnerFullName
			,Balance
			,AnnualPercentageRate
			,ExecutedByName
			,ExecutedDatetime
			,TransactionTypeName
		)
		SELECT 
			BankAccountId
			,BankAccountTypeId
			,OwnerFullName
			,Balance
			,AnnualPercentageRate
			,ExecutedByName
			,ExecutedDatetime
			,@Type                 
		FROM DELETED
	END;
END
GO