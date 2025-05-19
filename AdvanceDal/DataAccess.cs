using System.Data;
using System.Data.SqlClient;

namespace AdvanceDal
{
    public class DataAccess
    {
        public static class MicrosoftDB
        {
            public static int ExecuteCommand(
                string commandText,
                CommandType commandType,
                string connectionString,
                params SqlParameter[] parameters)
            {
                SqlConnection Connection = new SqlConnection(connectionString);
                SqlCommand Command = new SqlCommand(commandText);
                int RecordsAffected = 0;

                Connection.Open();
                Command.Connection = Connection;
                Command.CommandType = commandType;
                Command.Parameters.AddRange(parameters);
                RecordsAffected = Command.ExecuteNonQuery();

                Connection.Close();

                if (Connection != null) Connection.Dispose();
                if (Command != null) Command.Dispose();
                Connection = null;
                Command = null;

                return RecordsAffected;
            }

            public static T ExecuteScalar<T>(
                string commandText,
                CommandType commandType,
                string connectionString,
                params SqlParameter[] parameters)
            {
                SqlConnection Connection = new SqlConnection(connectionString);
                SqlCommand Command = new SqlCommand(commandText);
                T returnObject;

                Connection.Open();
                Command.Connection = Connection;
                Command.CommandType = commandType;
                Command.Parameters.AddRange(parameters);
                returnObject = (T)Command.ExecuteScalar();

                Connection.Close();

                if (Connection != null) Connection.Dispose();
                if (Command != null) Command.Dispose();
                Connection = null;
                Command = null;

                return returnObject;
            }

            public static DataSet GetDataSet(
                                        string commandText,
                CommandType commandType,
                string connectionString,
                params SqlParameter[] parameters)
            {
                return GetDataSet(commandText, commandType, connectionString, "dataSet1", 30, parameters);
            }

            public static DataSet GetDataSet(
                string commandText,
                CommandType commandType,
                string connectionString,
                int commandTimeout,
                params SqlParameter[] parameters)
            {
                return GetDataSet(commandText, commandType, connectionString, "dataSet1", commandTimeout, parameters);
            }

            public static DataSet GetDataSet(
                string commandText,
                CommandType commandType,
                string connectionString,
                string dataSetName,
                int CommandTimeout,
                params SqlParameter[] parameters)
            {
                SqlConnection Con = new SqlConnection(connectionString);
                SqlDataAdapter DataAdapter = new SqlDataAdapter();
                SqlCommand Command = new SqlCommand(commandText);
                DataSet ReturnDataSet = new DataSet(dataSetName);

                Con.Open();
                Command.Connection = Con;
                Command.CommandType = commandType;
                Command.CommandTimeout = CommandTimeout;

                Command.Parameters.AddRange(parameters);
                DataAdapter.SelectCommand = Command;
                DataAdapter.Fill(ReturnDataSet);
                Con.Close();

                if (DataAdapter != null) DataAdapter.Dispose();
                if (Command != null) Command.Dispose();
                if (Con != null) Con.Dispose();

                DataAdapter = null;
                Command = null;
                Con = null;

                return ReturnDataSet;
            }

            public static DataTable GetDataTable(
                string commandText,
                CommandType commandType,
                string connectionString,
                params SqlParameter[] parameters)
            {
                return GetDataTable(commandText, commandType, connectionString, "table1", parameters);
            }

            public static DataTable GetDataTable(
                string commandText,
                CommandType commandType,
                string connectionString,
                string tableName,
                params SqlParameter[] parameters)
            {
                SqlConnection Con = new SqlConnection(connectionString);
                SqlDataAdapter DataAdapter = new SqlDataAdapter();
                SqlCommand Command = new SqlCommand(commandText);
                DataTable ReturnDataTable = new DataTable(tableName);

                Con.Open();
                Command.Connection = Con;
                Command.CommandType = commandType;

                Command.Parameters.AddRange(parameters);
                DataAdapter.SelectCommand = Command;
                DataAdapter.Fill(ReturnDataTable);
                Con.Close();

                if (DataAdapter != null) DataAdapter.Dispose();
                if (Command != null) Command.Dispose();
                if (Con != null) Con.Dispose();

                DataAdapter = null;
                Command = null;
                Con = null;

                return ReturnDataTable;
            }
        }
    }
}