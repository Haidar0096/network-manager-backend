using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace NetworkManagerApi.Persistence
{
    public static class MSSQLUtils
    {
        public static List<T> SpExecuteReader<T>(string spName, string connectionString, List<SqlParameter> parameters, Func<SqlDataReader, T> mapper)
        {
            using SqlConnection connection = new(connectionString);
            SqlCommand command = new(spName, connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            foreach (SqlParameter parameter in parameters)
            {
                command.Parameters.Add(parameter);

            }
            List<T> results = new();
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                results.Add(mapper(reader));
            }
            return results;
        }

        public static int SpExecuteNonQuery(string spName, string connectionString, List<SqlParameter> parameters)
        {
            using SqlConnection connection = new(connectionString);
            SqlCommand command = new(spName, connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            foreach (SqlParameter parameter in parameters)
            {
                command.Parameters.Add(parameter);

            }
            connection.Open();
            return command.ExecuteNonQuery();
        }

        public static T SpExecuteScalar<T>(string spName, string connectionString, List<SqlParameter> parameters)
        {
            using SqlConnection connection = new(connectionString);
            SqlCommand command = new(spName, connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            foreach (SqlParameter parameter in parameters)
            {
                command.Parameters.Add(parameter);

            }
            connection.Open();
            return (T)command.ExecuteScalar();
        }
    }
}