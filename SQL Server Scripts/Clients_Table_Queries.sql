-- Procedure to create the Clients table if it does not exist
EXEC spDropProcedureIfExists @ProcedureName='spCreateClientsTableIfNotExists'
GO
CREATE PROCEDURE spCreateClientsTableIfNotExists
AS
BEGIN
	-- Only create the table if it does not exist
	IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  TABLE_NAME = 'Clients')
	BEGIN
		CREATE TABLE Clients(
		Id INT PRIMARY KEY IDENTITY(1,1),
		Name VARCHAR(100) NOT NULL,
		)
	END
END
GO