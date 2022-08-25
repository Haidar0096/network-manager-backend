using NetworkManagerApi.Common.Logger;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Http;

namespace NetworkManagerApi.Persistence
{
    /// <summary>
    /// This class contains methods that prepare the persistence layer.
    /// </summary>
    public static class PersistenceBootstrapper
    {

        static readonly string _connectionString = ConfigurationManager.ConnectionStrings["Demo_DB_ConnectionString"].ConnectionString;

        static readonly ILogger _logger = (ILogger)GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(ILogger));

        /// <summary>
        /// Prepares the persistence layer by creating the Databases if needed.
        /// </summary>
        public static void Run()
        {
            using SqlConnection connection = new(_connectionString);
            try
            {
                // Create the Devices table if it does not exist
                MSSQLUtils.SpExecuteNonQuery("spCreateDevicesTableIfNotExists", _connectionString, new());

                // Create the Ports table if it does not exist
                MSSQLUtils.SpExecuteNonQuery("spCreatePortsTableIfNotExists", _connectionString, new());
            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.error, e.Message);
                Environment.Exit(1);
            }
        }
    }
}