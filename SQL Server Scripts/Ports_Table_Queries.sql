-- Procedure to create the Devices DB if it does not exist
EXEC spDropProcedureIfExists @ProcedureName='spCreatePortsTableIfNotExists'
GO
CREATE PROCEDURE spCreatePortsTableIfNotExists
AS
BEGIN
	-- Only create the table if it does not exist
	IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  TABLE_NAME = 'Ports')
	BEGIN
		CREATE TABLE Ports(
		Id INT PRIMARY KEY IDENTITY(1,1),
		Number INT NOT NULL,
		DeviceId INT NOT NULL,
		CONSTRAINT AK_Number UNIQUE(Number),
		CONSTRAINT FK_Ports_Devices FOREIGN KEY (DeviceId)
		REFERENCES Devices (Id)
		ON DELETE CASCADE
		ON UPDATE CASCADE
		)
	END
END
GO

-- Procedure to get all Ports by port number with pagination
EXEC spDropProcedureIfExists @ProcedureName='spGetPortsByPortNumberPaginated'
GO
CREATE PROCEDURE spGetPortsByPortNumberPaginated @Number VARCHAR(500) , @ExactMatch BIT, @Offset VARCHAR(500) ,@RowCount VARCHAR(50), @OrderByColumn VARCHAR(500)
AS
	BEGIN
		DECLARE @command VARCHAR(1000)
		IF (@ExactMatch=1) 
			BEGIN
				SET @command = 'SELECT * FROM Ports p WHERE p.Number= ''' + @Number + ''' ORDER BY '+ @OrderByColumn + ' OFFSET '+ @Offset + ' ROWS FETCH NEXT '+ @RowCount +' ROWS ONLY'
				EXEC (@command)
			END
		ELSE
			BEGIN
				SET @command = 'SELECT * FROM Ports p WHERE p.Number LIKE ''' + @Number + '%'' ORDER BY '+ @OrderByColumn + ' OFFSET '+ @Offset + ' ROWS FETCH NEXT '+ @RowCount +' ROWS ONLY'
				EXEC (@command)
			END
	END
GO

-- Procedure to get count of all Ports with certain number
EXEC spDropProcedureIfExists @ProcedureName='spGetPortsCountForPortNumber'
GO
CREATE PROCEDURE spGetPortsCountForPortNumber @Number VARCHAR(500) , @ExactMatch BIT
AS
	BEGIN
		DECLARE @command VARCHAR(1000)
		IF (@ExactMatch=1) 
			BEGIN
				SELECT COUNT(*) FROM Ports p WHERE p.Number=@Number
				
			END
		ELSE
			BEGIN
				SET @command = 'SELECT COUNT(*) FROM Ports p WHERE p.Number LIKE ''' + @Number+'%''' 
				EXEC (@command)
			END
	END
GO

--Procedure to insert a row in ports tables
EXEC spDropProcedureIfExists @ProcedureName='spInsertPort'
GO
CREATE PROCEDURE spInsertPort @Number INT , @DeviceId INT, @Id INT OUTPUT
AS
	BEGIN
		INSERT INTO Ports(Number,DeviceId) Values (@Number, @DeviceId)
		SELECT @Id = SCOPE_IDENTITY()
	END
GO