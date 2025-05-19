using System;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for SystemConfigurations
/// </summary>
public class SystemConfigurationsManager : IDisposable
{
    public Int32? FilterBySystemConfigurationID { get; set; }
    public String FilterByCode { get; set; }
    public String FilterByValue { get; set; }
    public Boolean? FilterByIsRecordActive { get; set; }
    public Int32? FilterByCreatedByID { get; set; }
    public Int32? FilterByLastModifiedByID { get; set; }

    public string ExecutionMessages { get; set; }
    private string ConnectionString;
    private SqlConnection con = new SqlConnection();
    private SqlDataAdapter adapter = new SqlDataAdapter();
    private SqlCommand command = new SqlCommand();
    private SqlDataReader DataReader;
    private SqlTransaction sqltransaction;
    private bool _disposed;

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public SystemConfigurationsManager()
    {
    }

    ~SystemConfigurationsManager()
    {
    }

    protected virtual void Dispose(bool disposing)
    {
        // If you need thread safety, use a lock around these 
        // operations, as well as in your methods that use the resource.
        if (!_disposed)
        {
            if (disposing)
            {
                if (con != null)
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                        con.Dispose();

                    }

                if (adapter != null)
                    adapter.Dispose();

                if (command != null)
                    command.Dispose();

                if (DataReader != null)
                {
                    DataReader.Close();
                    DataReader.Dispose();
                }

                if (sqltransaction != null)
                {
                    sqltransaction.Dispose();
                }

                con = null;
                adapter = null;
                command = null;
                DataReader = null;
                sqltransaction = null;
            }

            // Indicate that the instance has been disposed.

            _disposed = true;
        }
    }

    public string GetTheConnectionString
    {
        get
        {
            return ConnectionString;
        }
        set
        {
            ConnectionString = value;
            con.ConnectionString = ConnectionString;
            con.Open();
        }


    }

    private SystemConfigurationsCollection<SystemConfigurations> MapColumnToProperties(SqlDataReader dr)
    {

        SystemConfigurationsCollection<SystemConfigurations> shSystemConfigurationsCollection = new SystemConfigurationsCollection<SystemConfigurations>();
        SystemConfigurations sel = new SystemConfigurations();

        sel.ConnectionString(ConnectionString);
        shSystemConfigurationsCollection = sel.Mapping(dr);

        return shSystemConfigurationsCollection;

    }

    public SystemConfigurationsCollection<SystemConfigurations> GetData()
    {

        SqlParameter[] Parameter = new SqlParameter[6];

        if (con.State == ConnectionState.Closed)
            con.Open();


        Parameter[0] = new SqlParameter("@SystemConfigurationID", FilterBySystemConfigurationID);
        Parameter[1] = new SqlParameter("@Code", FilterByCode);
        Parameter[2] = new SqlParameter("@Value", FilterByValue);
        Parameter[3] = new SqlParameter("@IsRecordActive", FilterByIsRecordActive);
        Parameter[4] = new SqlParameter("@CreatedByID", FilterByCreatedByID);
        Parameter[5] = new SqlParameter("@LastModifiedByID", FilterByLastModifiedByID);

        adapter = new SqlDataAdapter("sp_GetSystemConfigurations", con);

        adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
        adapter.SelectCommand.Parameters.AddRange(Parameter);
        DataReader = adapter.SelectCommand.ExecuteReader();

        SystemConfigurationsCollection<SystemConfigurations> shSystemConfigurationsCollection =
            new SystemConfigurationsCollection<SystemConfigurations>();

        //call the method that will map column to properties

        shSystemConfigurationsCollection = MapColumnToProperties(DataReader);

        return shSystemConfigurationsCollection;

    }


    private int ExecuteTheSqlCommand(string sp_name, SqlParameter[] Parameters)
    {
        int ret;

        try
        {
            // assign the connection srting
            if (con.State == ConnectionState.Closed)
            {
                con.ConnectionString = ConnectionString;
                con.Open();
            }


            //set the properties for the sql command rollback and commit transaction is include
            //     sqltransaction = con.BeginTransaction();

            command = con.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;

            if (Parameters != null)
            {
                command.Parameters.AddRange(Parameters);
            }

            command.Transaction = sqltransaction;
            command.CommandText = sp_name;
            command.Parameters.Add("RETURN_VALUE", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;
            command.Parameters.Add("@Messsages", SqlDbType.NVarChar, 500).Direction = ParameterDirection.Output;
            command.Parameters["@Messsages"].Value = ExecutionMessages;
            command.ExecuteNonQuery();
            ExecutionMessages = Convert.ToString(command.Parameters["@Messsages"].Value);
            ret = Convert.ToInt16(command.Parameters["RETURN_VALUE"].Value);
            //    sqltransaction.Commit();
            //command.Connection = con;
            //command.Connection.Open();

        }
        catch (System.Data.SqlClient.SqlException ex)
        {
            ret = -1;
            //   sqltransaction.Rollback();
            throw ex;
        }
        catch (Exception ex)
        {
            ret = -1;
            throw ex;
        }

        return ret;

    }

    public int SetData(SystemConfigurations SystemConfigurationsUnit)
    {
        int result = 0;
        SqlParameter[] Parameter = new SqlParameter[7];

        try
        {
            //check the business rules of the fields
            //result = CheckBusinessRules.SystemConfigurationsFields(SystemConfigurationsUnit);



            // assign first the value of parameters
            if (result >= 0)
            {

                Parameter[0] = new SqlParameter("@Code", SystemConfigurationsUnit.Code);
                Parameter[1] = new SqlParameter("@Value", SystemConfigurationsUnit.Value);
                Parameter[2] = new SqlParameter("@IsRecordActive", SystemConfigurationsUnit.IsRecordActive);
                Parameter[3] = new SqlParameter("@CreatedByID", SystemConfigurationsUnit.CreatedByID);
                Parameter[4] = new SqlParameter("@LastModifiedByID", SystemConfigurationsUnit.LastModifiedByID);


                if (SystemConfigurationsUnit.DBExecution == GenericUnit.DBExecutionType.Insert)
                    Parameter[5] = new SqlParameter("@SystemConfigurationID", null);
                else
                    Parameter[5] = new SqlParameter("@SystemConfigurationID", SystemConfigurationsUnit.SystemConfigurationID);

                Parameter[6] = new SqlParameter("@InsertFlag", (int)SystemConfigurationsUnit.DBExecution);

                result = ExecuteTheSqlCommand("sp_SetSystemConfigurations", Parameter);
            }

            return result;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

}

