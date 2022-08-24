-- Misc Scripts

-- Create the devices db if it does not exist
exec spCreateDevicesDBIfNotExists
go

-- Populate the db with some data
DECLARE @i int = 0
DECLARE @DeviceNumber VARCHAR(5)
DECLARE @query VARCHAR(1000)
WHILE @i < 50000
	BEGIN
		SET @i = @i + 1
		SET @DeviceNumber = @i
		SET @query = 'insert into Devices(Name) Values (''Device '+ @DeviceNumber + ''')'
		print(@query)
		exec(@query)
	END
go

-- Add a device
insert into Devices(Name) values ('qqq' + CONVERT(varchar(255), NEWID()))
go

-- Select all rows
select * from Devices;
go

-- Delete all rows
delete from Devices
go

-- Delete the devices database
drop table Devices
go

-- Delete Device 1
delete from devices where id=37
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