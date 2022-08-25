-- Procedure to drop a procedure by name if it exists
-- Drop this procedure first if it exists
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'spDropProcedureIfExists')
			BEGIN
				DECLARE @command VARCHAR(1000)
				SET @command = 'DROP PROCEDURE spDropProcedureIfExists' 
				EXEC (@command)
			END
GO
CREATE PROCEDURE spDropProcedureIfExists @ProcedureName VARCHAR(500)
AS
	BEGIN
		IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = @ProcedureName)
			BEGIN
				DECLARE @command VARCHAR(1000)
				SET @command = 'DROP PROCEDURE ' + @ProcedureName
				EXEC (@command)
			END
	END
GO

-- Procedure to drop a table by name if it exists
EXEC spDropProcedureIfExists @ProcedureName='spDropTableIfExists'
GO
CREATE PROCEDURE spDropTableIfExists @TableName VARCHAR(500) , @SchemaName VARCHAR(500)
AS
	BEGIN
		IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = @SchemaName AND  TABLE_NAME = @TableName)
			BEGIN
				DECLARE @command VARCHAR(1000)
				SET @command = 'DROP Table ' + @TableName
				EXEC (@command)
			END
	END
GO

-- Procedure to get a table's info by name if it exists
EXEC spDropProcedureIfExists @ProcedureName='spGetTableInfo'
GO
CREATE PROCEDURE spGetTableInfo @TableName VARCHAR(500) , @SchemaName VARCHAR(500)
AS
	BEGIN
	SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = @SchemaName AND  TABLE_NAME = @TableName
	END
GO

-- Procedure to drop a type by name if it exists
EXEC spDropProcedureIfExists @ProcedureName='spDropTypeIfExists'
GO
CREATE PROCEDURE spDropTypeIfExists @TypeName VARCHAR(500)
AS
	BEGIN
		IF EXISTS (SELECT * FROM sys.types WHERE name = @TypeName)
			BEGIN
				DECLARE @command VARCHAR(1000)
				SET @command = 'DROP TYPE ' + @TypeName
				EXEC (@command)
			END
	END
GO

-- Procedure to get all rows in a table
EXEC spDropProcedureIfExists @ProcedureName='spGetAllRows'
GO
CREATE PROCEDURE spGetAllRows @TableName VARCHAR(500)
AS
	BEGIN
		DECLARE @command VARCHAR(1000)
		SET @command = 'SELECT * FROM ' + @TableName 
		EXEC (@command)
	END
GO

-- Procedure to get all rows in a table in a paginated manner
EXEC spDropProcedureIfExists @ProcedureName='spGetRowsPaginated'
GO
CREATE PROCEDURE spGetRowsPaginated @TableName VARCHAR(500) , @Offset VARCHAR(500) ,@RowCount VARCHAR(50), @OrderByColumn VARCHAR(500)
AS
	BEGIN
		DECLARE @command VARCHAR(1000)
		SET @command = 'SELECT * FROM ' + @TableName + ' ORDER BY '+ @OrderByColumn + ' OFFSET '+ @Offset + ' ROWS FETCH NEXT '+ @RowCount +' ROWS ONLY'
		EXEC (@command)
	END
GO

-- Procedure to get number of rows in a table
EXEC spDropProcedureIfExists @ProcedureName='spGetRowsCount'
GO
CREATE PROCEDURE spGetRowsCount @TableName VARCHAR(500)
AS
	BEGIN
		DECLARE @command VARCHAR(1000)
		SET @command = 'SELECT COUNT(*) FROM ' + @TableName
		EXEC (@command)
	END
GO

-- Procedure to get all rows in a table with a column selected
EXEC spDropProcedureIfExists @ProcedureName='spGetRowsByColumnName'
GO
CREATE PROCEDURE spGetRowsByColumnName @TableName VARCHAR(500), @ColumnName VARCHAR(500)
AS
	BEGIN
		DECLARE @command VARCHAR(1000)
		SET @command = 'SELECT ' + @ColumnName + ' FROM ' + @TableName 
		EXEC (@command)
	END
GO