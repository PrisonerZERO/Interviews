SET NOCOUNT ON

------------
-- ROLES & ROLE MEMBERS
------------

-- [db_ApplicationUserRole]
IF DATABASE_PRINCIPAL_ID('db_ApplicationUserRole') IS NOT NULL
BEGIN
	DROP ROLE [db_ApplicationUserRole]
END
GO

------------
-- SCHEMAS
------------

-- DATA
IF (SCHEMA_ID('data') IS NOT NULL)
BEGIN
	DROP SCHEMA [data]
END
GO