using LanguageExt;
using NetworkManagerApi.Common.Logger;
using NetworkManagerApi.Models;
using NetworkManagerApi.Persistence;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Http;

namespace NetworkManagerApi.Repositories
{
    public class MSSQLDeviceRepository : IDeviceRepository
    {
        private static readonly string _connectionString = ConfigurationManager.ConnectionStrings["Demo_DB_ConnectionString"].ConnectionString;

        private static readonly ILogger _logger = (ILogger)GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(ILogger));

        private static Device DeviceMapper(SqlDataReader reader) => new((int)reader["Id"], (string)reader["Name"]);

        public Either<string, IEnumerable<Device>> GetAllDevices()
        {
            try
            {
                var parameters = new List<SqlParameter>()
                {
                    new()
                    {
                        ParameterName = "@TableName",
                        Value = "Devices",
                        Direction = ParameterDirection.Input,
                        DbType = DbType.String,
                    }
                };
                var devices = MSSQLUtils.SpExecuteReader("spGetAllRows", _connectionString, parameters, DeviceMapper);

                return devices;
            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.error, e.Message);
                return "An error has occurred while fetching the devices.";
            }
        }

        public Either<string, IEnumerable<Device>> GetDevicesByName(string name, bool exactMatch)
        {
            try
            {
                var parameters = new List<SqlParameter>()
                {
                     new()
                    {
                        ParameterName = "@Name",
                        Value = name,
                        Direction = ParameterDirection.Input,
                        DbType = DbType.String,
                    },
                     new()
                    {
                        ParameterName = "@ExactMatch",
                        Value = exactMatch,
                        Direction = ParameterDirection.Input,
                        DbType = DbType.Boolean,
                    }
                };
                var devices = MSSQLUtils.SpExecuteReader("spGetDevicesByName", _connectionString, parameters, DeviceMapper);

                return devices;
            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.error, e.Message);
                return "An error has occurred while fetching the devices.";
            }
        }

        public Either<string, IEnumerable<int>> GetDeviceIds()
        {
            try
            {
                var parameters = new List<SqlParameter>()
                {
                    new()
                    {
                        ParameterName = "@TableName",
                        Value = "Devices",
                        Direction = ParameterDirection.Input,
                        DbType = DbType.String,
                    },
                     new()
                    {
                        ParameterName = "@ColumnName",
                        Value = "Id",
                        Direction = ParameterDirection.Input,
                        DbType = DbType.String,
                    },
                };
                var ids = MSSQLUtils.SpExecuteReader("spGetRowsByColumnName", _connectionString, parameters, reader => (int)reader["Id"]);

                return ids;
            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.error, e.Message);
                return "An error has occurred.";
            }
        }

        public Either<string, IEnumerable<Device>> GetDevicesByNamePaginated(string name, int offset, int count, bool exactMatch)
        {
            try
            {
                var parameters = new List<SqlParameter>()
                {
                     new()
                    {
                        ParameterName = "@Name",
                        Value = name,
                        Direction = ParameterDirection.Input,
                        DbType = DbType.String,
                    },
                       new()
                    {
                        ParameterName = "@Offset",
                        Value = offset,
                        Direction = ParameterDirection.Input,
                        DbType = DbType.String,
                    },
                     new()
                    {
                        ParameterName = "@RowCount",
                        Value = count,
                        Direction = ParameterDirection.Input,
                        DbType = DbType.String,
                    },
                     new()
                    {
                        ParameterName = "@OrderByColumn",
                        Value = "Id",
                        Direction = ParameterDirection.Input,
                        DbType = DbType.String,
                    },
                      new()
                    {
                        ParameterName = "@ExactMatch",
                        Value = exactMatch,
                        Direction = ParameterDirection.Input,
                        DbType = DbType.Boolean,
                    }
                };
                var devices = MSSQLUtils.SpExecuteReader("spGetDevicesByNamePaginated", _connectionString, parameters, DeviceMapper);

                return devices;
            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.error, e.Message);
                return "An error has occurred while fetching the devices.";
            }
        }

        public Either<string, IEnumerable<Device>> GetDevicesPaginated(int offset, int count)
        {
            try
            {
                var parameters = new List<SqlParameter>()
                {
                     new()
                    {
                        ParameterName = "@TableName",
                        Value = "Devices",
                        Direction = ParameterDirection.Input,
                        DbType = DbType.String,
                    },
                     new()
                    {
                        ParameterName = "@Offset",
                        Value = offset,
                        Direction = ParameterDirection.Input,
                        DbType = DbType.String,
                    },
                     new()
                    {
                        ParameterName = "@RowCount",
                        Value = count,
                        Direction = ParameterDirection.Input,
                        DbType = DbType.String,
                    },
                     new()
                    {
                        ParameterName = "@OrderByColumn",
                        Value = "Id",
                        Direction = ParameterDirection.Input,
                        DbType = DbType.String,
                    },
                };
                var devices = MSSQLUtils.SpExecuteReader("spGetRowsPaginated", _connectionString, parameters, DeviceMapper);

                return devices;
            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.error, e.Message);
                return "An error has occurred while fetching the devices.";
            }
        }

        public Either<string, int> AddDevice(string name)
        {
            try
            {
                SqlParameter idParam = new()
                {
                    ParameterName = "@Id",
                    Direction = ParameterDirection.Output,
                    DbType = DbType.Int32,
                };

                var parameters = new List<SqlParameter>()
                {
                    idParam,
                    new()
                    {
                        ParameterName = "@Name",
                        Value = name,
                        Direction = ParameterDirection.Input,
                        DbType = DbType.String,
                    },
                };
                MSSQLUtils.SpExecuteNonQuery("spInsertDevice", _connectionString, parameters);

                return (int)idParam.Value;
            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.error, e.Message);
                return "An error has occurred while adding the device.";
            }
        }

        public Option<string> UpdateDevice(int id, string name)
        {
            try
            {
                var parameters = new List<SqlParameter>()
                {
                    new()
                    {
                        ParameterName = "@Name",
                        Value = name,
                        Direction = ParameterDirection.Input,
                        DbType = DbType.String,
                    },
                    new()
                    {
                        ParameterName = "@Id",
                        Value = id,
                        Direction = ParameterDirection.Input,
                        DbType = DbType.Int32,
                    }
                };
                MSSQLUtils.SpExecuteNonQuery("spUpdateDevice", _connectionString, parameters);
                return Option<string>.None;
            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.error, e.Message);
                return "An error has occurred while updating the device.";
            }
        }
        public Option<string> DeleteDevice(int id)
        {
            try
            {
                var parameters = new List<SqlParameter>()
                {
                     new()
                        {
                            ParameterName = "@Id",
                            Value = id,
                            Direction = ParameterDirection.Input,
                            DbType = DbType.Int32,
                        }
                };
                MSSQLUtils.SpExecuteNonQuery("spDeleteDevice", _connectionString, parameters);
                return Option<string>.None;
            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.error, e.Message);
                return "An error has occurred while deleting the device.";
            }
        }

        public Either<string, Option<Device>> GetDeviceById(int id)
        {
            try
            {
                var parameters = new List<SqlParameter>()
                {
                    new()
                    {
                        ParameterName = "@Id",
                        Value = id,
                        Direction = ParameterDirection.Input,
                        DbType = DbType.Int32,
                    }
                };

                var results = MSSQLUtils.SpExecuteReader("spGetDeviceById", _connectionString, parameters, DeviceMapper);
                if (results.Count == 0)
                {
                    return Option<Device>.None;
                }
                else
                {
                    return Option<Device>.Some(results[0]);
                }
            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.error, e.Message);
                return "An error has occurred while fetching the device.";
            }
        }

        public Either<string, int> GetDevicesCount()
        {
            try
            {
                var parameters = new List<SqlParameter>()
                {
                    new()
                    {
                        ParameterName = "@TableName",
                        Value = "Devices",
                        Direction = ParameterDirection.Input,
                        DbType = DbType.String,
                    }
                };


                return MSSQLUtils.SpExecuteScalar<int>("spGetRowsCount", _connectionString, parameters);
            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.error, e.Message);
                return "An error has occurred.";
            }
        }

        public Either<string, int> GetDevicesCountForName(string deviceName, bool exactMatch)
        {
            try
            {
                var parameters = new List<SqlParameter>()
                {
                    new()
                    {
                        ParameterName = "@Name",
                        Value = deviceName,
                        Direction = ParameterDirection.Input,
                        DbType = DbType.String,
                    },
                    new()
                    {
                        ParameterName = "@ExactMatch",
                        Value = exactMatch,
                        Direction = ParameterDirection.Input,
                        DbType = DbType.Boolean,
                    },
                };


                return MSSQLUtils.SpExecuteScalar<int>("spGetDevicesCountForName", _connectionString, parameters);
            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.error, e.Message);
                return "An error has occurred.";
            }
        }
    }
}