using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
/// <summary>
/// Summary description for SystemConfigurations
/// </summary>
/// 

[Serializable]
public class SystemConfigurationsCollection<SystemConfigurations> : List<SystemConfigurations>
{
}

[Serializable]
public class SystemConfigurations : GenericUnit
{

    public Int32? SystemConfigurationID { get; set; }
    public String Code { get; set; }
    public String Value { get; set; }
    public Boolean? IsRecordActive { get; set; }
    public Int32? CreatedByID { get; set; }
    public Int32? LastModifiedByID { get; set; }
    private DateTime? datecreated;
    public DateTime? DateCreated { get { return datecreated; } }
    private DateTime? datelastmodified;
    public DateTime? DateLastModified { get { return datelastmodified; } }

    private string constring;
    private SqlConnection con = new SqlConnection();
    private SqlParameter sqlParameter = new SqlParameter();
    private SqlDataAdapter adapter = new SqlDataAdapter();
    private SqlCommand command = new SqlCommand();
    private SqlDataReader DataReader;

    private void GetConString(string _constring)
    { constring = _constring; }

    private string SqlConnectionString
    { get { return constring; } }

    protected internal void ConnectionString(string constring)
    {
        GetConString(constring);
    }

    private SqlDataReader LineitemReader(string sp_name, Int32 ID)
    {

        adapter.Dispose();
        command.Dispose();

        if (DataReader != null)
            DataReader.Close();

        if (con.State == ConnectionState.Closed)
        {
            con.ConnectionString = SqlConnectionString;
            con.Open();
        }


        adapter = new SqlDataAdapter(sp_name, con);
        command = new SqlCommand();
        sqlParameter = new SqlParameter("@SystemConfigurationID", ID);


        adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
        adapter.SelectCommand.Parameters.Add(sqlParameter);
        DataReader = adapter.SelectCommand.ExecuteReader();


        return DataReader;

    }

    private void CallDisposeObjects()
    {

        if (con != null)
        {
            if (con.State == ConnectionState.Open)
            { con.Close(); con.Dispose(); }

        }
        if (adapter != null)
            adapter.Dispose();

        if (command != null)
            command.Dispose();

        if (DataReader != null)
        { DataReader.Close(); DataReader.Dispose(); }

    }
    private void MapColumns(DateTime? _datecreated, DateTime? _datelastmodified)
    {

        datecreated = _datecreated;
        datelastmodified = _datelastmodified;


    }
    protected internal SystemConfigurationsCollection<SystemConfigurations> Mapping(SqlDataReader dr)
    {

        SystemConfigurationsCollection<SystemConfigurations> shSystemConfigurationsCollection = new SystemConfigurationsCollection<SystemConfigurations>();

        SystemConfigurations SystemConfigurationsUnit = new SystemConfigurations();

        try
        {

            while (dr.Read())
            {

                SystemConfigurationsUnit.SystemConfigurationID = dr["SystemConfigurationID"] == DBNull.Value ? (Int32?)null : Convert.ToInt32(dr["SystemConfigurationID"]);
                SystemConfigurationsUnit.Code = Convert.ToString(dr["Code"]);
                SystemConfigurationsUnit.Value = Convert.ToString(dr["Value"]);
                SystemConfigurationsUnit.IsRecordActive = dr["IsRecordActive"] == DBNull.Value ? (Boolean?)null : Convert.ToBoolean(dr["IsRecordActive"]);
                SystemConfigurationsUnit.CreatedByID = dr["CreatedByID"] == DBNull.Value ? (Int32?)null : Convert.ToInt32(dr["CreatedByID"]);
                SystemConfigurationsUnit.LastModifiedByID = dr["LastModifiedByID"] == DBNull.Value ? (Int32?)null : Convert.ToInt32(dr["LastModifiedByID"]);
                SystemConfigurationsUnit.MapColumns(dr["DateCreated"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["DateCreated"]),
                    dr["DateLastModified"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["DateLastModified"])
                );
                shSystemConfigurationsCollection.Add(SystemConfigurationsUnit);
                SystemConfigurationsUnit = new SystemConfigurations();

            }

            return shSystemConfigurationsCollection;
        }

        catch (Exception ex)
        { throw ex; }

        finally
        {
            CallDisposeObjects();
            dr.Close();
        }
    }

}


