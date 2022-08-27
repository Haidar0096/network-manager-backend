-- Misc Scripts

-- Create the devices table if it does not exist
exec spCreateDevicesTableIfNotExists
go

-- Create the ports table if it does not exist
exec spCreatePortsTableIfNotExists
go

-- Populate the devices db with some data
DECLARE @i int = 0
DECLARE @DeviceNumber VARCHAR(5)
DECLARE @query VARCHAR(1000)
WHILE @i < 500
	BEGIN
		SET @i = @i + 1
		SET @DeviceNumber = @i
		SET @query = 'insert into Devices(Name) Values (''Device '+ @DeviceNumber + ''')'
		print(@query)
		exec(@query)
	END
go

-- Populate the ports db with some data
DECLARE @i int = 0
DECLARE @DeviceId INT
DECLARE @MinDeviceId INT = 1
DECLARE @MaxDeviceId INT = 500
DECLARE @query VARCHAR(1000)
WHILE @i < 100
	BEGIN
		SET @i = @i + 1
		SET @DeviceId = FLOOR ( RAND() * ( @MaxDeviceId - @MinDeviceId + 1 ) + @MinDeviceId )
		SET @query = 'insert into Ports(Number,DeviceId) values ( ' + convert(varchar(5),@i) + ', ' + convert(varchar(5),@DeviceId) + ')'
		print(@query)
		exec(@query)
	END
go

-- Add a device
insert into Devices(Name) values ('qqq' + CONVERT(varchar(255), NEWID()))
go

DECLARE @MinDeviceId INT = 1
DECLARE @MaxDeviceId INT = 10000
SELECT FLOOR(RAND()*(@MaxDeviceId-@MinDeviceId+1)+@MinDeviceId);
go

-- Add a device
insert into Devices(Name) values ('Device 1')
go

-- Add a port
insert into Ports(Number,DeviceId) values (1234,2)
go

-- Select all rows from devices
select * from Devices;
go

-- Select all rows from ports
select * from Ports;
go

-- Delete the ports table
drop table ports
go


-- Delete all rows
delete from Devices
go

-- Delete the devices table
drop table Devices
go

-- Delete Device 1
delete from devices where id=7174
go

-- Get a Device by id
exec spGetDeviceById @Id=4
go

-- Get devices with specific name
exec spGetDevicesByName @Name='Device 1' , @ExactMatch= 1
go

-- Get devices with specific name
exec spGetDevicesByNamePaginated @Name='dev' , @ExactMatch= 0, @Offset='2' ,@RowCount='4', @OrderByColumn='Id'
go

-- Get count of devices with specific name
exec spGetDevicesCountByName @Name='ssss' , @ExactMatch= 0
go

-- Get ports with specific number
exec spGetPortsByPortNumberPaginated @Number=2 , @ExactMatch= 0, @Offset='0' ,@RowCount='100', @OrderByColumn='Id'
go


-- Get count of ports with specific number
exec spGetPortsCountForPortNumber  @Number=2 , @ExactMatch= 0
go


-- Insert a Port
exec spInsertPort @Number = 123456 , @DeviceId = 10, @Id=0  
go

-- Get ports paginated
exec spGetRowsPaginated  @TableName='Ports' , @Offset='0' ,@RowCount='5', @OrderByColumn='Id'
go


-- Get all rows of a specific column
exec spGetRowsByColumnName @TableName='Devices', @ColumnName='Id'
go

-- Update device
exec spUpdateDevice @Id=2, @Name='Device bla bla'
go