-- Procedure to create the Devices table if it does not exist
EXEC spDropProcedureIfExists @ProcedureName='spCreateDevicesTableIfNotExists'
GO
CREATE PROCEDURE spCreateDevicesTableIfNotExists
AS
BEGIN
	-- Only create the table if it does not exist
	IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  TABLE_NAME = 'Devices')
	BEGIN
		CREATE TABLE Devices(
		Id INT PRIMARY KEY IDENTITY(1,1),
		Name VARCHAR(500) NOT NULL,
		--CONSTRAINT AK_Name UNIQUE(Name)
		)
	END
END
GO

-- Procedure to insert all rows from a table into Devices table
EXEC spDropProcedureIfExists @ProcedureName='spInsertTableIntoDevicesTable'
GO
EXEC spDropTypeIfExists @TypeName ='DevicesTableType' 
CREATE TYPE DevicesTableType -- Define a new TABLE type as a model for insert operation on our Devices table 
AS
	TABLE(
		Id INT PRIMARY KEY IDENTITY(1,1),
		Name VARCHAR(500) NOT NULL
	) 
GO
CREATE PROCEDURE spInsertTableIntoDevicesTable @TableToInsert DevicesTableType readonly
AS
	BEGIN
		DECLARE @NewlyAddedPrimaryKeys Table (Id int) -- tmp table to store the newly created Ids
		INSERT INTO Devices -- insert the values from the input parameter to the Devices table
			OUTPUT INSERTED.Id INTO @NewlyAddedPrimaryKeys -- output the newly created Ids into the tmp table
			SELECT t.Name FROM @TableToInsert t -- insert the values from the input parameter to the Devices table
		SELECT * FROM @NewlyAddedPrimaryKeys -- return the tmp table with the newly created Ids
	END
GO

--Procedure to insert a row in devices table
EXEC spDropProcedureIfExists @ProcedureName='spInsertDevice'
GO
CREATE PROCEDURE spInsertDevice @Name VARCHAR(500), @Id INT OUTPUT
AS
	BEGIN
		INSERT INTO Devices(Name) Values (@Name)
		SELECT @Id = SCOPE_IDENTITY()
	END
GO

-- Procedure to update a row in Devices table
EXEC spDropProcedureIfExists @ProcedureName='spUpdateDevice'
GO
CREATE PROCEDURE spUpdateDevice @Id int, @Name VARCHAR(500)
AS
	BEGIN
		UPDATE Devices SET Name=@Name WHERE Id=@Id
	END
GO

-- Procedure to delete a row in Devices table
EXEC spDropProcedureIfExists @ProcedureName='spDeleteDevice'
GO
CREATE PROCEDURE spDeleteDevice @Id int
AS
	BEGIN
		DELETE FROM Devices WHERE Id=@Id
	END
GO

-- Procedure to get a Device by Id
EXEC spDropProcedureIfExists @ProcedureName='spGetDeviceById'
GO
CREATE PROCEDURE spGetDeviceById @Id INT
AS
	BEGIN
		SELECT * FROM Devices d WHERE d.Id= @Id
	END
GO

-- Procedure to get all Devices by Name
EXEC spDropProcedureIfExists @ProcedureName='spGetDevicesByName'
GO
CREATE PROCEDURE spGetDevicesByName @Name VARCHAR(500) , @ExactMatch BIT
AS
	BEGIN
		IF (@ExactMatch=1)
			SELECT * FROM Devices d WHERE d.Name= @Name
		ELSE
			BEGIN
				DECLARE @command VARCHAR(1000)
				SET @command = 'SELECT * FROM Devices d WHERE d.Name LIKE ''' + @Name +'%'''
				EXEC (@command)
			END
	END
GO

-- Procedure to get all Devices by Name with pagination
EXEC spDropProcedureIfExists @ProcedureName='spGetDevicesByNamePaginated'
GO
CREATE PROCEDURE spGetDevicesByNamePaginated @Name VARCHAR(500) , @ExactMatch BIT, @Offset VARCHAR(500) ,@RowCount VARCHAR(50), @OrderByColumn VARCHAR(500)
AS
	BEGIN
		DECLARE @command VARCHAR(1000)
		IF (@ExactMatch=1) 
			BEGIN
				SET @command = 'SELECT * FROM Devices d WHERE d.Name= ''' + @Name + ''' ORDER BY '+ @OrderByColumn + ' OFFSET '+ @Offset + ' ROWS FETCH NEXT '+ @RowCount +' ROWS ONLY'
				EXEC (@command)
			END
		ELSE
			BEGIN
				SET @command = 'SELECT * FROM Devices d WHERE d.Name LIKE ''' + @Name + '%'' ORDER BY '+ @OrderByColumn + ' OFFSET '+ @Offset + ' ROWS FETCH NEXT '+ @RowCount +' ROWS ONLY'
				EXEC (@command)
			END
	END
GO

-- Procedure to get count of all Devices with certain name
EXEC spDropProcedureIfExists @ProcedureName='spGetDevicesCountForName'
GO
CREATE PROCEDURE spGetDevicesCountForName @Name VARCHAR(500) , @ExactMatch BIT
AS
	BEGIN
		DECLARE @command VARCHAR(1000)
		IF (@ExactMatch=1) 
			BEGIN
				SELECT COUNT(*) FROM Devices d WHERE d.Name=@Name
				
			END
		ELSE
			BEGIN
				SET @command = 'SELECT COUNT(*) FROM Devices d WHERE d.Name LIKE ''' + @Name +'%''' 
				EXEC (@command)
			END
	END
GO
