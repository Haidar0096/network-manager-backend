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
    public class MSSQLPortRepository : IPortRepository
    {

        private static readonly string _connectionString = ConfigurationManager.ConnectionStrings["Demo_DB_ConnectionString"].ConnectionString;

        private static readonly ILogger _logger = (ILogger)GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(ILogger));

        private static Port PortMapper(SqlDataReader reader) => new((int)reader["Id"], (int)reader["Number"], (int)reader["DeviceId"]);

        public Either<string, IEnumerable<Port>> GetPortsPaginated(int offset, int count)
        {
            try
            {
                var parameters = new List<SqlParameter>()
                {
                     new()
                    {
                        ParameterName = "@TableName",
                        Value = "Ports",
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
                var ports = MSSQLUtils.SpExecuteReader("spGetRowsPaginated", _connectionString, parameters, PortMapper);

                return ports;
            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.error, e.Message);
                return "An error has occurred while fetching the ports.";
            }
        }

        public Either<string, IEnumerable<Port>> GetPortsByPortNumberPaginated(int portNumber, int offset, int count, bool exactMatch)
        {
            try
            {
                var parameters = new List<SqlParameter>()
                {
                     new()
                    {
                        ParameterName = "@Number",
                        Value = portNumber,
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
                var ports = MSSQLUtils.SpExecuteReader("spGetPortsByPortNumberPaginated", _connectionString, parameters, PortMapper);

                return ports;
            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.error, e.Message);
                return "An error has occurred while fetching the ports.";
            }
        }

        public Either<string, int> GetPortsCount()
        {
            try
            {
                var parameters = new List<SqlParameter>()
                {
                    new()
                    {
                        ParameterName = "@TableName",
                        Value = "Ports",
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

        public Either<string, int> GetPortsCountForPortNumber(int portNumber, bool exactMatch)
        {
            try
            {
                var parameters = new List<SqlParameter>()
                {
                    new()
                    {
                        ParameterName = "@Number",
                        Value = portNumber,
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


                return MSSQLUtils.SpExecuteScalar<int>("spGetPortsCountForPortNumber", _connectionString, parameters);
            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.error, e.Message);
                return "An error has occurred.";
            }
        }

        public Either<string, int> AddPort(int portNumber, int deviceId)
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
                        ParameterName = "@Number",
                        Value = portNumber,
                        Direction = ParameterDirection.Input,
                        DbType = DbType.String,
                    },
                     new()
                    {
                        ParameterName = "@DeviceId",
                        Value = deviceId,
                        Direction = ParameterDirection.Input,
                        DbType = DbType.String,
                    },
                };
                MSSQLUtils.SpExecuteNonQuery("spInsertPort", _connectionString, parameters);

                return (int)idParam.Value;
            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.error, e.Message);
                return "An error has occurred while adding the port.";
            }
        }
    }
}