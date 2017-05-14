---------
-- CREATE ROLE: [db_ApplicationUserRole]
---------  
IF DATABASE_PRINCIPAL_ID('db_ApplicationUserRole') IS NULL
BEGIN
	CREATE ROLE [db_ApplicationUserRole]
END
GO

---------
-- CREATE SCHEMA SECURITY: data
---------
IF (SCHEMA_ID('data') IS NULL)
BEGIN
	EXECUTE('CREATE SCHEMA [data] AUTHORIZATION [dbo] ')
END
GO

---------
-- SETUP ROLE: [db_ApplicationUserRole]
-- SECUTITY: data
---------
GRANT DELETE
	ON SCHEMA::[data] TO [db_ApplicationUserRole]
	AS [dbo];

GRANT EXECUTE
	ON SCHEMA::[data] TO [db_ApplicationUserRole]
	AS [dbo];

GRANT INSERT
	ON SCHEMA::[data] TO [db_ApplicationUserRole]
	AS [dbo];

GRANT SELECT
	ON SCHEMA::[data] TO [db_ApplicationUserRole]
	AS [dbo];

GRANT UPDATE
	ON SCHEMA::[data] TO [db_ApplicationUserRole]
	AS [dbo];