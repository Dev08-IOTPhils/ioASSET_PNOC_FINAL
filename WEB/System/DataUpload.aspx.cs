using AdvanceBll;
using AdvanceEntity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;

public partial class System_DataUpload : BasePage
{
    [WebMethod]
    public static string GetRFIDandBarcodeGeneration()
    {
        MiscBll miscBll = new MiscBll();

        return miscBll.GetRFIDandBarcodeGeneration(Common.ConnString);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
    }
    #region Upload without oledb

    [WebMethod]
    public static int CheckIfExists(string filename)
    {
        MiscEntity x = new MiscEntity();
        MiscBll y = new MiscBll();

        x.DataUploadFileNameProperty = filename;
        x.CreatedBy = Common.UserName;
        x.ConnectionString = Common.ConnString;

        return y.CheckIfExists(x);
    }

    [WebMethod]
    public static int CheckIfExistsStock(string filename)
    {
        MiscEntity x = new MiscEntity();
        MiscBll y = new MiscBll();

        x.DataUploadFileNameStock = filename;
        x.CreatedBy = Common.UserName;
        x.ConnectionString = Common.ConnString;

        return y.CheckIfExistsStock(x);
    }

    public static string ColumnExist(string tablename, string columnName)
    {
        string conString = ConfigurationManager.ConnectionStrings[Common.ConnString].ConnectionString;
        using (SqlConnection con = new SqlConnection(conString))
        {
            SqlCommand cmdCheck = con.CreateCommand();
            con.Open();
            cmdCheck.CommandType = System.Data.CommandType.StoredProcedure;
            cmdCheck.Parameters.AddWithValue("tablename", tablename);
            cmdCheck.Parameters.AddWithValue("columnName", columnName.Trim());
            cmdCheck.CommandText = "SP_UploadCheckColumn";
            object temp = cmdCheck.ExecuteScalar();
            con.Close();
            return temp.ToString();
        }
    }

    [WebMethod]
    public static void UploadUserForApproval(string filename)
    {
        MiscEntity x = new MiscEntity();
        MiscBll y = new MiscBll();

        x.DataUploadFileNameUser = filename;
        x.CreatedBy = Common.UserName;
        x.ConnectionString = Common.ConnString;

        y.UploadUserForApproval(x);
    }

    [WebMethod]
    public static int CheckIfExistsUser(string filename)
    {
        MiscEntity x = new MiscEntity();
        MiscBll y = new MiscBll();

        x.DataUploadFileNameUser = filename;
        x.CreatedBy = Common.UserName;
        x.ConnectionString = Common.ConnString;

        return y.CheckIfExistsUser(x);
    }

    public static string LoadUserToDB(string destinationtable, DataTable dtCSV, string filename)
    {
        string returnval = "";
        string consString = ConfigurationManager.ConnectionStrings[Common.ConnString].ConnectionString;
        using (SqlConnection con = new SqlConnection(consString))
        {
            using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
            {
                bool continueUpload = false;
                //Set the database table name
                sqlBulkCopy.DestinationTableName = destinationtable;
                for (int i = 0; i < dtCSV.Columns.Count; i++)
                {
                    //check if header exists in the destination table

                    string ColumnHeader = dtCSV.Columns[i].ColumnName.ToString().Trim();


                    if (ColumnHeader == "JobTitle")
                    {
                        sqlBulkCopy.ColumnMappings.Add(ColumnHeader, "Position");
                        continueUpload = true;
                        continue;

                    }
                    else if (ColumnHeader == "CustodianType" || ColumnHeader == "Custodian")
                    {
                        sqlBulkCopy.ColumnMappings.Add(ColumnHeader, "Custodian");
                        continueUpload = true;
                        continue;
                    }
                    else if (ColumnHeader == "UserDepartment")
                    {
                        sqlBulkCopy.ColumnMappings.Add(ColumnHeader, "Department");
                        continueUpload = true;
                        continue;
                    }

                    else
                    {
                        if (!string.IsNullOrEmpty(ColumnExist(destinationtable, dtCSV.Columns[i].ToString().Trim())))
                        {
                            sqlBulkCopy.ColumnMappings.Add(dtCSV.Columns[i].ColumnName, dtCSV.Columns[i].ColumnName.Trim());
                            continueUpload = true;
                            continue;
                        }
                        else
                        {
                            continueUpload = false;
                            returnval = "Mismatch header";
                            break;
                        }
                    }
                }
                con.Open();
                if (continueUpload == true)
                {
                    sqlBulkCopy.WriteToServer(dtCSV);

                    SqlCommand cmdCheck = con.CreateCommand();
                    cmdCheck.CommandType = System.Data.CommandType.StoredProcedure;
                    cmdCheck.Parameters.AddWithValue("User", Common.UserName);
                    cmdCheck.Parameters.AddWithValue("Filename", filename);
                    cmdCheck.CommandText = "SP_UploadCheckUser";
                    string _ReturnValue = "";
                    object temp = cmdCheck.ExecuteScalar();


                    _ReturnValue = temp.ToString();
                    string returnValue0 = _ReturnValue.Split('|')[0];
                    string returnValue1 = _ReturnValue.Split('|')[1].Replace("\r", "");
                    //try parse first the asset
                    int _TryParse = 0;

                    if (int.TryParse(returnValue0, out _TryParse))
                    {
                        if (Convert.ToInt32(returnValue0) == 1)
                        {
                            if (Common.DataUploadApproval == "True" || Common.DataUploadApproval == "1")
                            {
                                if (CheckIfExistsUser(returnValue1) == 0)
                                {
                                    UploadUserForApproval(returnValue1);
                                    returnval = "Success upload with approval";
                                }
                                else
                                {
                                    returnval = "Upload failed batch name already exists";
                                }
                            }
                            else
                            {
                                SqlCommand cmdUpload = con.CreateCommand();
                                cmdUpload.CommandType = System.Data.CommandType.StoredProcedure;
                                cmdUpload.Parameters.AddWithValue("User", Common.UserName);
                                cmdUpload.Parameters.AddWithValue("CustodianUser", Convert.ToInt32(Common.nooftsuc));
                                cmdUpload.Parameters.AddWithValue("AdminUser", Convert.ToInt32(Common.noofminda));
                                cmdUpload.Parameters.AddWithValue("BatchName", returnValue1);
                                cmdUpload.CommandText = "SP_UploadUser";
                                int count = Convert.ToInt32(cmdUpload.ExecuteScalar());

                                if (count == 0)
                                    returnval = "Approver license is already full";

                                else
                                    returnval = "Success upload without approval:" + count.ToString();

                            }
                        }
                    }
                    else if (returnValue0 == "DUPLICATE USER")
                    {
                        SqlDataAdapter sqlDA = new SqlDataAdapter("SP_UploadCheckUserDuplicate", con);
                        sqlDA.SelectCommand.CommandType = CommandType.StoredProcedure;
                        sqlDA.SelectCommand.Parameters.AddWithValue("BatchName", returnValue1);
                        DataTable dt = new DataTable();
                        sqlDA.Fill(dt);

                        List<string> returnValue = new List<string>();
                        if (dt.Rows.Count >= 1)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                returnValue.Add(dt.Rows[i]["Returnvalue"].ToString());
                            }
                            string[] list = returnValue.ToArray();
                            string valuestring = string.Join(",", list);
                            if (list.Length > 0)
                            {
                                returnval = "Duplicate user:" + valuestring;
                            }
                        }
                    }
                    else if (returnValue0 == "ALREADY EXISTS")
                    {
                        SqlDataAdapter sqlDA = new SqlDataAdapter("SP_UploadCheckUserExists", con);
                        sqlDA.SelectCommand.CommandType = CommandType.StoredProcedure;
                        sqlDA.SelectCommand.Parameters.AddWithValue("BatchName", returnValue1);
                        DataTable dt = new DataTable();
                        sqlDA.Fill(dt);


                        List<UserEntity> JsonItemList = new List<UserEntity>();
                        returnval = "Already exists";
                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                UserEntity x = new UserEntity();
                                x.FirstName = dt.Rows[i]["FirstName"].ToString() + " " + dt.Rows[i]["LastName"].ToString();
                                x.UserName = dt.Rows[i]["UserName"].ToString();
                                x.EmailAddress = dt.Rows[i]["EmailAddress"].ToString();
                                x.Department = dt.Rows[i]["Department"].ToString();
                                x.Position = dt.Rows[i]["Position"].ToString();
                                x.Site = dt.Rows[i]["Region"].ToString();
                                x.Custodians = dt.Rows[i]["Custodian"].ToString();
                                x.Remarks = "AlreadyExists";
                                JsonItemList.Add(x);
                            }
                            JavaScriptSerializer json = new JavaScriptSerializer();
                            json.MaxJsonLength = Int32.MaxValue;
                            returnval = json.Serialize(JsonItemList);
                            return returnval;
                        }
                    }
                    else
                    {
                        SqlDataAdapter sqlDA = new SqlDataAdapter("SP_UploadCheckUserInvalidData", con);
                        sqlDA.SelectCommand.CommandType = CommandType.StoredProcedure;
                        sqlDA.SelectCommand.Parameters.AddWithValue("BatchName", returnValue1);
                        DataTable dt = new DataTable();
                        sqlDA.Fill(dt);

                        List<UserEntity> JsonItemList = new List<UserEntity>();
                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                UserEntity x = new UserEntity();
                                x.FirstName = dt.Rows[i]["FirstName"].ToString() + " " + dt.Rows[i]["LastName"].ToString();
                                x.UserName = dt.Rows[i]["UserName"].ToString();
                                x.EmailAddress = dt.Rows[i]["EmailAddress"].ToString();
                                x.Department = dt.Rows[i]["Department"].ToString();
                                x.Position = dt.Rows[i]["Position"].ToString();
                                x.Custodians = dt.Rows[i]["Custodian"].ToString();
                                x.Site = dt.Rows[i]["Region"].ToString();
                                x.Remarks = "InvalidData";
                                JsonItemList.Add(x);
                            }
                            JavaScriptSerializer json = new JavaScriptSerializer();
                            json.MaxJsonLength = Int32.MaxValue;
                            returnval = json.Serialize(JsonItemList);
                            return returnval;
                        }
                    }
                }


            }
            con.Close();
        }

        return returnval;
    }

    public static DataTable CSVtoDataTable(string strFilePath, char csvDelimiter)
    {
        DataTable dt = new DataTable();
        using (StreamReader sr = new StreamReader(strFilePath))
        {
            string[] headers = sr.ReadLine().Split(csvDelimiter);
            foreach (string header in headers)
            {
                try
                {
                    dt.Columns.Add(header);
                }
                catch { }
            }
            int counter = 0;
            while (!sr.EndOfStream)
            {
                string[] rows = sr.ReadLine().Trim('\n').Split(csvDelimiter);
                if (counter == 0)
                {
                    counter++;
                }
                else
                {
                    DataRow dr = dt.NewRow();
                    for (int i = 0; i < headers.Length; i++)
                    {
                        dr[i] = rows[i];
                    }
                    dt.Rows.Add(dr);
                }
            }
        }
        return dt;
    }

    [WebMethod]
    public static string DownloadTemplate(string fileName)
    {
        //string fileName = "Property_Upload_Template.csv";
        //string fileExtension = ".csv";

        string path = HttpContext.Current.Server.MapPath("~/FilesDownloadable/");

        //Read the File as Byte Array.
        byte[] bytes = File.ReadAllBytes(path + fileName);

        //Convert File to Base64 string and send to Client.
        return Convert.ToBase64String(bytes, 0, bytes.Length);
    }

    public static string LoadPropertyToDB(string destinationtable, DataTable dtCSV, string filename)
    {
        string returnval = "";
        string consString = ConfigurationManager.ConnectionStrings[Common.ConnString].ConnectionString;
        using (SqlConnection con = new SqlConnection(consString))
        {
            using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
            {
                bool continueUpload = false;
                //Set the database table name
                sqlBulkCopy.DestinationTableName = destinationtable;
                for (int i = 0; i < dtCSV.Columns.Count; i++)
                {
                    string ColumnHeader = dtCSV.Columns[i].ColumnName.ToString().Trim();
                    //string RowHeader = dtCSV.Rows[i].ToString().Trim();l


                    if (ColumnHeader == "Site")
                    {
                        sqlBulkCopy.ColumnMappings.Add(ColumnHeader, "Region");
                        continueUpload = true;
                        continue;

                    }
                    else if (ColumnHeader == "Location")
                    {
                        sqlBulkCopy.ColumnMappings.Add(ColumnHeader, "Branch");
                        continueUpload = true;
                        continue;
                    }
                    else if (ColumnHeader == "Area")
                    {
                        sqlBulkCopy.ColumnMappings.Add(ColumnHeader, "Office");
                        continueUpload = true;
                        continue;
                    }
                    else if (ColumnHeader == "CustodianType" || ColumnHeader == "Custodian")
                    {
                        sqlBulkCopy.ColumnMappings.Add(ColumnHeader, "Custodian");
                        continueUpload = true;
                        continue;
                    }
                    else
                    {
                        //check if header exists in the destination table
                        if (!string.IsNullOrEmpty(ColumnExist(destinationtable, dtCSV.Columns[i].ToString().Trim())))
                        {
                            sqlBulkCopy.ColumnMappings.Add(dtCSV.Columns[i].ColumnName, dtCSV.Columns[i].ColumnName.Trim());
                            continueUpload = true;
                            continue;
                        }
                        else
                        {
                            continueUpload = false;
                            returnval = "Mismatch header";
                            break;
                        }
                    }
                }
                con.Open();
                if (continueUpload == true)
                {
                    sqlBulkCopy.WriteToServer(dtCSV);

                    SqlCommand cmdCheck = con.CreateCommand();
                    cmdCheck.CommandType = System.Data.CommandType.StoredProcedure;
                    cmdCheck.Parameters.AddWithValue("User", Common.UserName);
                    cmdCheck.Parameters.AddWithValue("Filename", filename);
                    cmdCheck.CommandText = "SP_UploadCheckProperty";
                    string _ReturnValue = "";
                    object temp = cmdCheck.ExecuteScalar();

                    _ReturnValue = temp.ToString();
                    string returnValue0 = _ReturnValue.Split('|')[0];
                    string returnValue1 = _ReturnValue.Split('|')[1].Replace("\r", "");
                    //try parse first the asset
                    int _TryParse = 0;

                    if (int.TryParse(returnValue0, out _TryParse))
                    {
                        if (Convert.ToInt32(returnValue0) == 1)
                        {
                            if (Common.DataUploadApproval == "True" || Common.DataUploadApproval == "1")
                            {
                                if (CheckIfExists(returnValue1) == 0)
                                {
                                    UploadPropertyForApproval(returnValue1);
                                    returnval = "Success upload with approval";
                                }
                                else
                                {
                                    returnval = "Upload failed batch name already exists";
                                }
                            }
                            else
                            {
                                SqlCommand cmdUpload = con.CreateCommand();
                                cmdUpload.CommandType = System.Data.CommandType.StoredProcedure;
                                cmdUpload.Parameters.AddWithValue("User", Common.UserID);
                                cmdUpload.Parameters.AddWithValue("BatchName", returnValue1);
                                cmdUpload.CommandText = "SP_UploadProperty";
                                int count = Convert.ToInt32(cmdUpload.ExecuteScalar());
                                returnval = "Success upload without approval:" + count.ToString();
                            }
                        }
                    }
                    else if (returnValue0 == "DUPLICATE PROPERTY")
                    {
                        SqlDataAdapter sqlDA = new SqlDataAdapter("SP_UploadCheckPropertyDuplicate", con);
                        sqlDA.SelectCommand.CommandType = CommandType.StoredProcedure;
                        sqlDA.SelectCommand.Parameters.AddWithValue("BatchName", returnValue1);
                        DataTable dt = new DataTable();
                        sqlDA.Fill(dt);

                        List<string> returnValue = new List<string>();
                        if (dt.Rows.Count >= 1)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                returnValue.Add(dt.Rows[i]["Returnvalue"].ToString());
                            }
                            string[] list = returnValue.ToArray();
                            string valuestring = string.Join(",", list);
                            if (list.Length > 0)
                            {
                                returnval = "Duplicate property:" + valuestring;
                            }
                        }
                    }
                    else if (returnValue0 == "DUPLICATE SERIAL")
                    {
                        SqlDataAdapter sqlDA = new SqlDataAdapter("SP_UploadCheckSerialDuplicate", con);
                        sqlDA.SelectCommand.CommandType = CommandType.StoredProcedure;
                        sqlDA.SelectCommand.Parameters.AddWithValue("BatchName", returnValue1);
                        DataTable dt = new DataTable();
                        sqlDA.Fill(dt);

                        List<string> returnValue = new List<string>();
                        if (dt.Rows.Count >= 1)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                returnValue.Add(dt.Rows[i]["Returnvalue"].ToString());
                            }
                            string[] list = returnValue.ToArray();
                            string valuestring = string.Join(",", list);
                            if (list.Length > 0)
                            {
                                returnval = "Duplicate serial:" + valuestring;
                            }
                        }
                    }
                    else if (returnValue0 == "DUPLICATE RFID NUMBER")
                    {
                        SqlDataAdapter sqlDA = new SqlDataAdapter("SP_UploadCheckRFIDDuplicate", con);
                        sqlDA.SelectCommand.CommandType = CommandType.StoredProcedure;
                        sqlDA.SelectCommand.Parameters.AddWithValue("BatchName", returnValue1);
                        DataTable dt = new DataTable();
                        sqlDA.Fill(dt);

                        List<string> returnValue = new List<string>();
                        if (dt.Rows.Count >= 1)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                returnValue.Add(dt.Rows[i]["Returnvalue"].ToString());
                            }
                            string[] list = returnValue.ToArray();
                            string valuestring = string.Join(",", list);
                            if (list.Length > 0)
                            {
                                returnval = "Duplicate rfid:" + valuestring;
                            }
                        }
                    }
                    else if (returnValue0 == "ALREADY EXISTS")
                    {
                        SqlDataAdapter sqlDA = new SqlDataAdapter("SP_UploadCheckPropertyExists", con);
                        sqlDA.SelectCommand.CommandType = CommandType.StoredProcedure;
                        sqlDA.SelectCommand.Parameters.AddWithValue("BatchName", returnValue1);
                        DataTable dt = new DataTable();
                        sqlDA.Fill(dt);

                        List<PropertyEntity> JsonItemList = new List<PropertyEntity>();
                        returnval = "Already exists";
                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                PropertyEntity x = new PropertyEntity();
                                x.PropertyNumber = dt.Rows[i]["PropertyNumber"].ToString();
                                x.ItemCode = dt.Rows[i]["ItemCode"].ToString();
                                x.PONumber = dt.Rows[i]["Custodian"].ToString();
                                x.SupplierName = dt.Rows[i]["Supplier"].ToString();
                                x.Site = dt.Rows[i]["Region"].ToString();
                                x.Location = dt.Rows[i]["Branch"].ToString();
                                x.Area = dt.Rows[i]["Office"].ToString();
                                x.Remarks = "AlreadyExists";
                                JsonItemList.Add(x);
                            }
                            JavaScriptSerializer json = new JavaScriptSerializer();
                            json.MaxJsonLength = Int32.MaxValue;
                            returnval = json.Serialize(JsonItemList);
                            return returnval;
                        }
                    }
                    else if (returnValue0 == "MULTIPLE BATCHNAME")
                    {
                        //SqlDataAdapter sqlDA = new SqlDataAdapter("SP_UploadCheckStockMultipleBatch", con);
                        //sqlDA.SelectCommand.CommandType = CommandType.StoredProcedure;
                        //sqlDA.SelectCommand.Parameters.AddWithValue("BatchName", returnValue1);
                        returnval = "Multiple Batchname detected";
                    }
                    else if (returnValue0 == "ALREADY EXISTS SERIAL")
                    {
                        SqlDataAdapter sqlDA = new SqlDataAdapter("SP_UploadCheckSerialExists", con);
                        sqlDA.SelectCommand.CommandType = CommandType.StoredProcedure;
                        sqlDA.SelectCommand.Parameters.AddWithValue("BatchName", returnValue1);
                        DataTable dt = new DataTable();
                        sqlDA.Fill(dt);
                        List<PropertyEntity> JsonItemList = new List<PropertyEntity>();
                        returnval = "Already exists";
                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                PropertyEntity x = new PropertyEntity();
                                x.PropertyNumber = dt.Rows[i]["PropertyNumber"].ToString();
                                x.SerialNo = dt.Rows[i]["SerialNumber"].ToString();
                                x.ItemCode = dt.Rows[i]["ItemCode"].ToString();
                                x.PONumber = dt.Rows[i]["Custodian"].ToString();
                                x.SupplierName = dt.Rows[i]["Supplier"].ToString();
                                x.Site = dt.Rows[i]["Region"].ToString();
                                x.Location = dt.Rows[i]["Branch"].ToString();
                                x.Area = dt.Rows[i]["Office"].ToString();
                                x.Remarks = "AlreadySerial";
                                JsonItemList.Add(x);
                            }
                            JavaScriptSerializer json = new JavaScriptSerializer();
                            json.MaxJsonLength = Int32.MaxValue;
                            returnval = json.Serialize(JsonItemList);
                            return returnval;
                        }
                    }

                    else
                    {
                        SqlDataAdapter sqlDA = new SqlDataAdapter("SP_UploadCheckPropertyInvalidData", con);
                        sqlDA.SelectCommand.CommandType = CommandType.StoredProcedure;
                        sqlDA.SelectCommand.Parameters.AddWithValue("BatchName", returnValue1);
                        DataTable dt = new DataTable();
                        sqlDA.Fill(dt);

                        List<PropertyEntity> JsonItemList = new List<PropertyEntity>();
                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                PropertyEntity x = new PropertyEntity();
                                x.PropertyNumber = dt.Rows[i]["PropertyNumber"].ToString();
                                x.ItemCode = dt.Rows[i]["ItemCode"].ToString();
                                x.PONumber = dt.Rows[i]["Custodian"].ToString();
                                x.SupplierName = dt.Rows[i]["Supplier"].ToString();
                                x.Site = dt.Rows[i]["Region"].ToString();
                                x.Location = dt.Rows[i]["Branch"].ToString();
                                x.Area = dt.Rows[i]["Office"].ToString();
                                x.DRNumber = dt.Rows[i]["EmployeeNo"].ToString();
                                x.Remarks = "InvalidData";
                                JsonItemList.Add(x);
                            }
                            JavaScriptSerializer json = new JavaScriptSerializer();
                            json.MaxJsonLength = Int32.MaxValue;
                            returnval = json.Serialize(JsonItemList);
                            return returnval;
                        }
                    }
                }
            }
            con.Close();
        }

        return returnval;
    }

    [WebMethod]
    public static string ProcessDataUpload(string directory, string filename, string type)
    {
        directory = directory.Replace(",", "\\");
        try
        {
            string csvFilePath = HttpContext.Current.Server.MapPath("~\\FilesUploaded") + "\\" + filename;

            /////Copy from WebServer to database server
            //System.IO.File.Copy(csvFilePath, directory + "\\" + filename, true);

            //Fetch the location of CSV file
            string filePath = HttpContext.Current.Server.MapPath("~\\FilesUploaded") + "\\";

            char csvDelimeter = '|';
            // load the data from CSV to DataTable
            DataTable dtCSV = CSVtoDataTable(csvFilePath, csvDelimeter);

            if (dtCSV.Rows.Count > 0)
            {
                string fileFullPath = filePath + filename;
                string DBserverPath = directory + "\\" + filename;
                if (type == "Property")
                {
                    for (int i = 0; i < dtCSV.Rows.Count; i++)
                    {
                        //Added by: Domdom
                        string IssuanceNo = "0";
                        string col1 = dtCSV.Rows[i]["ItemCode"].ToString();
                        string col2 = dtCSV.Rows[i]["SerialNumber"].ToString();
                        string col3 = dtCSV.Rows[i]["CustodianType"].ToString();
                        string col4 = dtCSV.Rows[i]["Supplier"].ToString();
                        string col5 = dtCSV.Rows[i]["Site"].ToString();
                        string col6 = dtCSV.Rows[i]["Location"].ToString();
                        string col7 = dtCSV.Rows[i]["Area"].ToString();
                        string col8 = dtCSV.Rows[i]["EmployeeNo"].ToString();
                        string col9 = dtCSV.Rows[i]["BatchName"].ToString();

                        //Added by:Domdom
                        if (dtCSV.Rows[i]["IssuanceNo"].ToString() != "" && dtCSV.Rows[i]["EmployeeNo"].ToString() == "")
                        {
                            IssuanceNo = "1";
                        }
                        else
                        {
                            IssuanceNo = "0";
                        }

                        if (col1 == "" || col2 == "" || col3 == "" || col4 == "" || col5 == "" || col6 == "" || col7 == "" || col9 == "")
                        {
                            return "Empty Field";
                        }
                        if (IssuanceNo == "1")
                        {
                            //Empty Employee Number
                            return "Empty EN";
                        }
                        else if(IssuanceNo == "0")
                        {
                            return "Empty SN";
                        }

                        //dr[i] = rows[i];
                    }
                    return LoadPropertyToDB("t_tbl_PropertyUpload", dtCSV, filename);
                }
                else if (type == "Stock")
                {
                    for (int i = 0; i < dtCSV.Rows.Count; i++)
                    {
                        //Added by: Domdom
                        string col1 = dtCSV.Rows[i]["ItemCode"].ToString();
                        string col2 = dtCSV.Rows[i]["Quantity"].ToString();
                        string col3 = dtCSV.Rows[i]["CustodianType"].ToString();
                        string col4 = dtCSV.Rows[i]["BatchName"].ToString();

                        int col2Int = Convert.ToInt32(col2);

                        if (col1 == "" || col2 == "" || col3 == "" || col4 == "")
                        {
                            return "Empty Field";
                        }
                        if(col2Int < 1)
                        {
                            return "Below zero";
                        }
                    }


                    return LoadStockToDB("t_tbl_StockUpload", dtCSV, filename);
                }
                else
                {
                    for (int i = 0; i < dtCSV.Rows.Count; i++)
                    {
                        //Added by: Domdom
                        string col1 = dtCSV.Rows[i]["FirstName"].ToString();
                        string col2 = dtCSV.Rows[i]["LastName"].ToString();
                        string col3 = dtCSV.Rows[i]["UserName"].ToString();
                        string col4 = dtCSV.Rows[i]["EmployeeNo"].ToString();
                        string col5 = dtCSV.Rows[i]["Site"].ToString();
                        string col6 = dtCSV.Rows[i]["CustodianType"].ToString();
                        string col7 = dtCSV.Rows[i]["UserDepartment"].ToString();
                        string col8 = dtCSV.Rows[i]["JobTitle"].ToString();
                        string col9 = dtCSV.Rows[i]["BatchName"].ToString();

                        string col10 = dtCSV.Rows[i]["IsCustodian"].ToString();

                        if (col1 == "" || col2 == "" || col3 == "" || col4 == "" || col5 == ""  || col7 == "" || col9 == "")
                        {
                            return "Empty Field";
                        }
                        if (col10 == "Yes" || col10 == "YES")
                        {
                            if(col6 == "")
                            {
                                return "Empty Field Custodian";
                            }
                        }
                        else if (col10 == "No" || col10 == "NO")
                        {
                            continue;
                        }
                        else
                        {
                            return "Invalid Data Custodian";
                        }
                    }
                    return LoadUserToDB("t_tbl_UserUpload", dtCSV, filename);
                }
            }
            else
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:FileEmpty();", true);
                return "File Empty";
            }
        }
        catch (Exception ex)
        {
            if (ex.ToString().Contains("Access to the path"))
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:AccessPath();", true);
                return "Access to the path";
            }
            else if (ex.ToString().Contains("The given value of type String from the data source cannot be converted to type decimal of the specified target column."))
            {
                return "Invalid Format for Acquired Cost";
            }
            else if (ex.ToString().Contains("Index was "))
            {
                return "Mismatch header";
            }
            else if (ex.ToString().Contains("Conversion failed when converting date and/ or time from character string."))
            {
                return "Incorrect Date Format.";
            }
            else
            {
                throw;
            }
        }
    }
    [WebMethod]
    public static void UploadPropertyForApproval(string filename)
    {
        MiscEntity x = new MiscEntity();
        MiscBll y = new MiscBll();

        x.DataUploadFileNameProperty = filename;
        x.CreatedBy = Common.UserName;
        x.ConnectionString = Common.ConnString;

        y.UploadPropertyForApproval(x);
    }

    [WebMethod]
    public static void UploadStockForApproval(string filename)
    {
        MiscEntity x = new MiscEntity();
        MiscBll y = new MiscBll();

        x.DataUploadFileNameStock = filename;
        x.CreatedBy = Common.UserName;
        x.ConnectionString = Common.ConnString;

        y.UploadStockForApproval(x);
    }

    private static string LoadStockToDB(string destinationtable, DataTable dtCSV, string filename)
    {
        string returnval = "";
        string consString = ConfigurationManager.ConnectionStrings[Common.ConnString].ConnectionString;
        using (SqlConnection con = new SqlConnection(consString))
        {
            using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
            {
                bool continueUpload = false;
                //Set the database table name
                sqlBulkCopy.DestinationTableName = destinationtable;
                for (int i = 0; i < dtCSV.Columns.Count; i++)
                {

                    //check if header exists in the destination table

                    string ColumnHeader = dtCSV.Columns[i].ColumnName.ToString().Trim();


                    if (ColumnHeader == "Site")
                    {
                        sqlBulkCopy.ColumnMappings.Add(ColumnHeader, "Region");
                        continueUpload = true;
                        continue;

                    }
                    else if (ColumnHeader == "Item Code")
                    {
                        sqlBulkCopy.ColumnMappings.Add(ColumnHeader, "ItemCode");
                        continueUpload = true;
                        continue;

                    }
                    else if (ColumnHeader == "CustodianType" || ColumnHeader == "Custodian")
                    {
                        sqlBulkCopy.ColumnMappings.Add(ColumnHeader, "Custodian");
                        continueUpload = true;
                        continue;
                    }
                    //else if (ColumnHeader == "Department")
                    //{
                    //    sqlBulkCopy.ColumnMappings.Add(ColumnHeader, "Branch");
                    //    continueUpload = true;
                    //    continue;
                    //}

                    else
                    {
                        if (!string.IsNullOrEmpty(ColumnExist(destinationtable, dtCSV.Columns[i].ToString().Trim())))
                        {
                            sqlBulkCopy.ColumnMappings.Add(dtCSV.Columns[i].ColumnName, dtCSV.Columns[i].ColumnName.Trim());
                            continueUpload = true;
                            continue;
                        }
                        else
                        {
                            continueUpload = false;
                            returnval = "Mismatch header";
                            break;
                        }
                    }

                }
                con.Open();
                if (continueUpload == true)
                {
                    sqlBulkCopy.WriteToServer(dtCSV);

                    SqlCommand cmdCheck = con.CreateCommand();
                    cmdCheck.CommandType = System.Data.CommandType.StoredProcedure;
                    cmdCheck.Parameters.AddWithValue("User", Common.UserName);
                    cmdCheck.Parameters.AddWithValue("Filename", filename);
                    cmdCheck.CommandText = "SP_UploadCheckStock";
                    string _ReturnValue = "";
                    object temp = cmdCheck.ExecuteScalar();

                    _ReturnValue = temp.ToString();

                    string returnValue0 = _ReturnValue.Split('|')[0];
                    string returnValue1 = _ReturnValue.Split('|')[1].Replace("\r", "");

                    //try parse first the asset
                    int _TryParse = 0;

                    if (int.TryParse(returnValue0, out _TryParse))
                    {
                        if (Convert.ToInt32(returnValue0) == 1)
                        {
                            if (Common.DataUploadApproval == "True" || Common.DataUploadApproval == "1")
                            {
                                if (CheckIfExistsStock(returnValue1) == 0)
                                {
                                    UploadStockForApproval(returnValue1);
                                    returnval = "Success upload with approval";
                                }
                                else
                                {
                                    returnval = "Upload failed batch name already exists";
                                }
                            }
                            else
                            {
                                SqlCommand cmdUpload = con.CreateCommand();
                                cmdUpload.CommandType = System.Data.CommandType.StoredProcedure;
                                cmdUpload.Parameters.AddWithValue("User", Common.UserID);
                                cmdUpload.CommandText = "SP_UploadStock";
                                int count = Convert.ToInt32(cmdUpload.ExecuteScalar());
                                returnval = "Success upload without approval:" + count.ToString();
                            }
                        }
                    }
                    else if (returnValue0 == "DUPLICATE STOCK")
                    {
                        SqlDataAdapter sqlDA = new SqlDataAdapter("SP_UploadCheckStockDuplicate", con);
                        sqlDA.SelectCommand.CommandType = CommandType.StoredProcedure;
                        sqlDA.SelectCommand.Parameters.AddWithValue("BatchName", returnValue1);
                        DataTable dt = new DataTable();
                        sqlDA.Fill(dt);

                        List<string> returnValue = new List<string>();
                        if (dt.Rows.Count >= 1)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                returnValue.Add(dt.Rows[i]["Returnvalue"].ToString());
                            }
                            string[] list = returnValue.ToArray();
                            string valuestring = string.Join(",", list);
                            if (list.Length > 0)
                            {
                                returnval = "Duplicate stock:" + valuestring;
                            }
                        }
                    }
                    else if (returnValue0 == "ALREADY EXISTS")
                    {
                        SqlDataAdapter sqlDA = new SqlDataAdapter("SP_UploadCheckStockExists", con);
                        sqlDA.SelectCommand.CommandType = CommandType.StoredProcedure;
                        sqlDA.SelectCommand.Parameters.AddWithValue("BatchName", returnValue1);
                        DataTable dt = new DataTable();
                        sqlDA.Fill(dt);

                        List<string> returnValue = new List<string>();
                        if (dt.Rows.Count >= 1)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                returnValue.Add(dt.Rows[i]["Returnvalue"].ToString());
                            }
                            string[] list = returnValue.ToArray();
                            string valuestring = string.Join(",", list);
                            if (list.Length > 0)
                            {
                                returnval = "Already Exists:" + valuestring;
                            }
                        }
                    }

                    else if (returnValue0 == "MULTIPLE BATCHNAME")
                    {
                        //SqlDataAdapter sqlDA = new SqlDataAdapter("SP_UploadCheckStockMultipleBatch", con);
                        //sqlDA.SelectCommand.CommandType = CommandType.StoredProcedure;
                        //sqlDA.SelectCommand.Parameters.AddWithValue("BatchName", returnValue1);
                        returnval = "Multiple Batchname detected";
                    }
                    else
                    {
                        SqlDataAdapter sqlDA = new SqlDataAdapter("SP_UploadCheckStockInvalidData", con);
                        sqlDA.SelectCommand.CommandType = CommandType.StoredProcedure;
                        sqlDA.SelectCommand.Parameters.AddWithValue("BatchName", returnValue1);
                        DataTable dt = new DataTable();
                        sqlDA.Fill(dt);
                        List<StockEntity> JsonItemList = new List<StockEntity>();
                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                StockEntity x = new StockEntity();
                                x.ItemCode = dt.Rows[i]["ItemCode"].ToString();
                                x.Custodian = dt.Rows[i]["Custodian"].ToString();
                                x.Region = dt.Rows[i]["Region"].ToString();
                                //x.Location = dt.Rows[i]["Branch"].ToString();
                                x.QuantityString = dt.Rows[i]["Quantity"].ToString();
                                x.Remarks = "InvalidData";
                                JsonItemList.Add(x);
                            }
                            JavaScriptSerializer json = new JavaScriptSerializer();
                            json.MaxJsonLength = Int32.MaxValue;
                            returnval = json.Serialize(JsonItemList);
                            return returnval;
                        }
                    }
                }
            }
            con.Close();
        }
        return returnval;
    }
    #endregion Upload without oledb

    #region GoodsReceiveUpload

    public static string LoadGRPropertyToDB(string destinationtable, DataTable dtCSV, string filename)
    {
        string returnval = "";
        string consString = ConfigurationManager.ConnectionStrings[Common.ConnString].ConnectionString;
        using (SqlConnection con = new SqlConnection(consString))
        {
            using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
            {
                bool continueUpload = false;
                //Set the database table name
                sqlBulkCopy.DestinationTableName = destinationtable;
                for (int i = 0; i < dtCSV.Columns.Count; i++)
                {
                    string ColumnHeader = dtCSV.Columns[i].ColumnName.ToString().Trim();


                    if (ColumnHeader == "Site")
                    {
                        sqlBulkCopy.ColumnMappings.Add(ColumnHeader, "Region");
                        continueUpload = true;
                        continue;

                    }
                    else if (ColumnHeader == "Location")
                    {
                        sqlBulkCopy.ColumnMappings.Add(ColumnHeader, "Branch");
                        continueUpload = true;
                        continue;
                    }
                    else if (ColumnHeader == "Area")
                    {
                        sqlBulkCopy.ColumnMappings.Add(ColumnHeader, "Office");
                        continueUpload = true;
                        continue;
                    }
                    else if (ColumnHeader == "CustodianType" || ColumnHeader == "Custodian")
                    {
                        sqlBulkCopy.ColumnMappings.Add(ColumnHeader, "Custodian");
                        continueUpload = true;
                        continue;
                    }
                    else
                    {
                        //check if header exists in the destination table
                        if (!string.IsNullOrEmpty(ColumnExist(destinationtable, dtCSV.Columns[i].ToString().Trim())))
                        {
                            sqlBulkCopy.ColumnMappings.Add(dtCSV.Columns[i].ColumnName, dtCSV.Columns[i].ColumnName.Trim());
                            continueUpload = true;
                            continue;
                        }
                        else
                        {
                            continueUpload = false;
                            returnval = "Mismatch header";
                            break;
                        }
                    }
                }
                con.Open();
                if (continueUpload == true)
                {
                    sqlBulkCopy.WriteToServer(dtCSV);

                    SqlCommand cmdCheck = con.CreateCommand();
                    cmdCheck.CommandType = System.Data.CommandType.StoredProcedure;
                    cmdCheck.Parameters.AddWithValue("User", Common.UserName);
                    cmdCheck.Parameters.AddWithValue("Filename", filename);
                    cmdCheck.CommandText = "SP_UploadCheckGRProperty";
                    string _ReturnValue = "";
                    object temp = cmdCheck.ExecuteScalar();

                    _ReturnValue = temp.ToString();
                    string returnValue0 = _ReturnValue.Split('|')[0];
                    string returnValue1 = _ReturnValue.Split('|')[1];
                    //try parse first the asset
                    int _TryParse = 0;

                    if (int.TryParse(returnValue0, out _TryParse))
                    {
                        if (Convert.ToInt32(returnValue0) == 1)
                        {
                            if (Common.CreationApproval == "True" || Common.CreationApproval == "1")
                            {
                                SqlCommand cmdUpload = con.CreateCommand();
                                cmdUpload.CommandType = System.Data.CommandType.StoredProcedure;
                                cmdUpload.Parameters.AddWithValue("User", Common.UserName);
                                cmdUpload.Parameters.AddWithValue("BatchName", returnValue1);
                                cmdUpload.CommandText = "SP_UploadGRProperty";
                                int count = Convert.ToInt32(cmdUpload.ExecuteScalar());
                                returnval = "Success upload with approval";
                            }
                            else
                            {
                                SqlCommand cmdUpload = con.CreateCommand();
                                cmdUpload.CommandType = System.Data.CommandType.StoredProcedure;
                                cmdUpload.Parameters.AddWithValue("User", Common.UserName);
                                cmdUpload.Parameters.AddWithValue("BatchName", returnValue1);
                                cmdUpload.CommandText = "SP_UploadGRProperty";
                                int count = Convert.ToInt32(cmdUpload.ExecuteScalar());
                                returnval = "Success upload without approval:";
                            }
                        }
                    }
                    else if (returnValue0 == "DUPLICATE SERIAL")
                    {
                        SqlDataAdapter sqlDA = new SqlDataAdapter("SP_UploadCheckGRSerialDuplicate", con);
                        sqlDA.SelectCommand.CommandType = CommandType.StoredProcedure;
                        sqlDA.SelectCommand.Parameters.AddWithValue("BatchName", returnValue1);
                        DataTable dt = new DataTable();
                        sqlDA.Fill(dt);

                        List<string> returnValue = new List<string>();
                        if (dt.Rows.Count >= 1)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                returnValue.Add(dt.Rows[i]["Returnvalue"].ToString());
                            }
                            string[] list = returnValue.ToArray();
                            string valuestring = string.Join(",", list);
                            if (list.Length > 0)
                            {
                                returnval = "Duplicate serial:" + valuestring;
                            }
                        }
                    }
                    else if (returnValue0 == "ALREADY EXISTS SERIAL")
                    {
                        SqlDataAdapter sqlDA = new SqlDataAdapter("SP_UploadCheckGRSerialExists", con);
                        sqlDA.SelectCommand.CommandType = CommandType.StoredProcedure;
                        sqlDA.SelectCommand.Parameters.AddWithValue("BatchName", returnValue1);
                        DataTable dt = new DataTable();
                        sqlDA.Fill(dt);
                        List<PropertyEntity> JsonItemList = new List<PropertyEntity>();
                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                PropertyEntity x = new PropertyEntity();
                                x.SerialNo = dt.Rows[i]["SerialNumber"].ToString();
                                x.ItemCode = dt.Rows[i]["PropertyItemCode"].ToString();
                                x.PONumber = dt.Rows[i]["Custodian"].ToString();
                                x.SupplierName = dt.Rows[i]["SupplierName"].ToString();
                                x.Site = dt.Rows[i]["Region"].ToString();
                                x.Location = dt.Rows[i]["Branch"].ToString();
                                x.Remarks = "AlreadySerial";
                                JsonItemList.Add(x);
                            }
                            JavaScriptSerializer json = new JavaScriptSerializer();
                            json.MaxJsonLength = Int32.MaxValue;
                            returnval = json.Serialize(JsonItemList);
                            return returnval;
                        }
                    }
                    else
                    {
                        SqlDataAdapter sqlDA = new SqlDataAdapter("SP_UploadCheckGRPropertyInvalidData", con);
                        sqlDA.SelectCommand.CommandType = CommandType.StoredProcedure;
                        sqlDA.SelectCommand.Parameters.AddWithValue("BatchName", returnValue1);
                        DataTable dt = new DataTable();
                        sqlDA.Fill(dt);

                        List<PropertyEntity> JsonItemList = new List<PropertyEntity>();
                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                PropertyEntity x = new PropertyEntity();
                                x.SerialNo = dt.Rows[i]["SerialNumber"].ToString();
                                x.ItemCode = dt.Rows[i]["ItemCode"].ToString();
                                x.PONumber = dt.Rows[i]["Custodian"].ToString();
                                x.SupplierName = dt.Rows[i]["Supplier"].ToString();
                                x.Site = dt.Rows[i]["Region"].ToString();
                                x.Location = dt.Rows[i]["Branch"].ToString();
                                x.Approver = dt.Rows[i]["Approver"].ToString();
                                x.AssignedPersonName = dt.Rows[i]["AssignedPerson"].ToString();
                                x.Remarks = "InvalidData";
                                JsonItemList.Add(x);
                            }
                            JavaScriptSerializer json = new JavaScriptSerializer();
                            json.MaxJsonLength = Int32.MaxValue;
                            returnval = json.Serialize(JsonItemList);
                            return returnval;
                        }
                    }
                }
            }
            con.Close();
        }

        return returnval;
    }

    [WebMethod]
    public static string ProcessDataUploadGR(string filename, string type)
    {
        try
        {
            string csvFilePath = HttpContext.Current.Server.MapPath("~\\FilesUploaded") + "\\" + filename;

            //Fetch the location of CSV file
            string filePath = HttpContext.Current.Server.MapPath("~\\FilesUploaded") + "\\";

            char csvDelimeter = '|';
            // load the data from CSV to DataTable
            DataTable dtCSV = CSVtoDataTable(csvFilePath, csvDelimeter);

            if (dtCSV.Rows.Count > 0)
            {
                string fileFullPath = filePath + filename;
                if (type == "Property")
                {
                    for (int i = 0; i < dtCSV.Rows.Count; i++)
                    {
                        //Added by: Domdom
                        string col1 = dtCSV.Rows[i]["PONumber"].ToString();
                        string col2 = dtCSV.Rows[i]["PRNumber"].ToString();
                        string col3 = dtCSV.Rows[i]["SupplierName"].ToString();
                        string col4 = dtCSV.Rows[i]["PropertyItemCode"].ToString();
                        string col5 = dtCSV.Rows[i]["SerialNumber"].ToString();
                        string col6 = dtCSV.Rows[i]["AcquiredCost"].ToString();
                        string col7 = dtCSV.Rows[i]["ReceivedBy"].ToString();
                        string col8 = dtCSV.Rows[i]["CustodianType"].ToString();
                        string col9 = dtCSV.Rows[i]["Site"].ToString();
                        string col10 = dtCSV.Rows[i]["Location"].ToString();
                        string col11 = dtCSV.Rows[i]["Approver"].ToString();
                        string col12 = dtCSV.Rows[i]["Remarks"].ToString();


                        if (col1 == "" || col2 == "" || col3 == "" || col4 == "" || col5 == "" || col6 == "" || col7 == "" || col8 == "" || col9 == "" || col10 == "" || col11 == "" || col12 == "")
                        {
                            return "Empty Field";
                        }

                        //dr[i] = rows[i];
                    }

                    return LoadGRPropertyToDB("t_tbl_GoodsReceiveList", dtCSV, filename);
                }
                else
                {
                    for (int i = 0; i < dtCSV.Rows.Count; i++)
                    {
                        //Added by: Domdom
                        string col1 = dtCSV.Rows[i]["PONumber"].ToString();
                        string col2 = dtCSV.Rows[i]["PRNumber"].ToString();
                        string col3 = dtCSV.Rows[i]["SupplierName"].ToString();
                        string col4 = dtCSV.Rows[i]["StockItemCode"].ToString();
                        string col5 = dtCSV.Rows[i]["Quantity"].ToString();
                        string col6 = dtCSV.Rows[i]["AcquiredCost"].ToString();
                        string col7 = dtCSV.Rows[i]["CustodianType"].ToString();
                        string col8 = dtCSV.Rows[i]["Site"].ToString();
                        string col9 = dtCSV.Rows[i]["Approver"].ToString();
                        string col10 = dtCSV.Rows[i]["Remarks"].ToString();



                        if (col1 == "" || col2 == "" || col3 == "" || col4 == "" || col5 == "" || col6 == "" || col7 == "" || col8 == "" || col9 == "" || col10 == "")
                        {
                            return "Empty Field";
                        }  

                        //dr[i] = rows[i];
                    }
                    return LoadGRStockToDB("t_tbl_GoodsReceiveList", dtCSV, filename);
                }
            }
            else
            {
                return "File Empty";
            }
        }
        catch (Exception ex)
        {
            if (ex.ToString().Contains("Access to the path"))
            {
                return "Access to the path";
            }
            else if (ex.ToString().Contains("Object reference"))
            {
                return "Object Reference";
            }
            else if (ex.ToString().Contains("Index was "))
            {
                return "Mismatch header";
            }
            else if (ex.ToString().Contains("decimal "))
            {
                return "Acquired Cost must be greater than or equal zero (0)";
            }
            else if (ex.ToString().Contains("Conversion failed when converting date and/ or time from character string."))
            {
                return "Incorrect Date Format.";
            }

            else
            {
                throw;
            }
        }
    }

    private static string LoadGRStockToDB(string destinationtable, DataTable dtCSV, string filename)
    {
        string returnval = "";
        string consString = ConfigurationManager.ConnectionStrings[Common.ConnString].ConnectionString;
        using (SqlConnection con = new SqlConnection(consString))
        {
            using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
            {
                bool continueUpload = false;
                //Set the database table name
                sqlBulkCopy.DestinationTableName = destinationtable;
                for (int i = 0; i < dtCSV.Columns.Count; i++)
                {
                    string ColumnHeader = dtCSV.Columns[i].ColumnName.ToString().Trim();
                    if (ColumnHeader == "Site")
                    {
                        sqlBulkCopy.ColumnMappings.Add(ColumnHeader, "Region");
                        continueUpload = true;
                        continue;

                    }
                    else if (ColumnHeader == "Item Code")
                    {
                        sqlBulkCopy.ColumnMappings.Add(ColumnHeader, "StockItemCode");
                        continueUpload = true;
                        continue;

                    }
                    else if (ColumnHeader == "CustodianType" || ColumnHeader == "Custodian")
                    {
                        sqlBulkCopy.ColumnMappings.Add(ColumnHeader, "Custodian");
                        continueUpload = true;
                        continue;
                    }

                    else
                    {
                        if (!string.IsNullOrEmpty(ColumnExist(destinationtable, dtCSV.Columns[i].ToString().Trim())))
                        {
                            sqlBulkCopy.ColumnMappings.Add(dtCSV.Columns[i].ColumnName, dtCSV.Columns[i].ColumnName.Trim());
                            continueUpload = true;
                            continue;
                        }
                        else
                        {
                            continueUpload = false;
                            returnval = "Mismatch header";
                            break;
                        }
                    }
                }
                con.Open();
                if (continueUpload == true)
                {
                    sqlBulkCopy.WriteToServer(dtCSV);

                    SqlCommand cmdCheck = con.CreateCommand();
                    cmdCheck.CommandType = System.Data.CommandType.StoredProcedure;
                    cmdCheck.Parameters.AddWithValue("User", Common.UserName);
                    cmdCheck.Parameters.AddWithValue("Filename", filename);
                    cmdCheck.CommandText = "SP_UploadCheckGRStock";
                    string _ReturnValue = "";
                    object temp = cmdCheck.ExecuteScalar();

                    _ReturnValue = temp.ToString();

                    string returnValue0 = _ReturnValue.Split('|')[0];
                    string returnValue1 = _ReturnValue.Split('|')[1].Replace("\r", "");

                    //try parse first the asset
                    int _TryParse = 0;

                    if (int.TryParse(returnValue0, out _TryParse))
                    {
                        if (Convert.ToInt32(returnValue0) == 1)
                        {
                            if (Common.CreationApproval == "True" || Common.CreationApproval == "1")
                            {
                                SqlCommand cmdUpload = con.CreateCommand();
                                cmdUpload.CommandType = System.Data.CommandType.StoredProcedure;
                                cmdUpload.Parameters.AddWithValue("User", Common.UserName);
                                cmdUpload.Parameters.AddWithValue("BatchName", returnValue1);
                                cmdUpload.CommandText = "SP_UploadGRStock";
                                int count = Convert.ToInt32(cmdUpload.ExecuteScalar());
                                returnval = "Success upload with approval";
                            }
                            else
                            {
                                SqlCommand cmdUpload = con.CreateCommand();
                                cmdUpload.CommandType = System.Data.CommandType.StoredProcedure;
                                cmdUpload.Parameters.AddWithValue("User", Common.UserName);
                                cmdUpload.Parameters.AddWithValue("BatchName", returnValue1);
                                cmdUpload.CommandText = "SP_UploadGRStock";
                                int count = Convert.ToInt32(cmdUpload.ExecuteScalar());
                                returnval = "Success upload without approval:";
                            }
                        }
                    }
                    else if (returnValue0 == "DUPLICATE STOCK")
                    {
                        SqlDataAdapter sqlDA = new SqlDataAdapter("SP_UploadCheckGRStockDuplicate", con);
                        sqlDA.SelectCommand.CommandType = CommandType.StoredProcedure;
                        sqlDA.SelectCommand.Parameters.AddWithValue("BatchName", returnValue1);
                        DataTable dt = new DataTable();
                        sqlDA.Fill(dt);

                        List<string> returnValue = new List<string>();
                        if (dt.Rows.Count >= 1)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                returnValue.Add(dt.Rows[i]["Returnvalue"].ToString());
                            }
                            string[] list = returnValue.ToArray();
                            string valuestring = string.Join(",", list);
                            if (list.Length > 0)
                            {
                                returnval = "Duplicate stock:" + valuestring;
                            }
                        }
                    }
                    else if (returnValue0 == "ZERO QUANTITY")
                    {
                        SqlDataAdapter sqlDA = new SqlDataAdapter("SP_UploadCheckGRStockZeroQuantity", con);
                        sqlDA.SelectCommand.CommandType = CommandType.StoredProcedure;
                        sqlDA.SelectCommand.Parameters.AddWithValue("BatchName", returnValue1);
                        DataTable dt = new DataTable();
                        sqlDA.Fill(dt);

                        List<string> returnValue = new List<string>();
                        if (dt.Rows.Count >= 1)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                returnValue.Add(dt.Rows[i]["Returnvalue"].ToString());
                            }
                            string[] list = returnValue.ToArray();
                            string valuestring = string.Join(",", list);
                            if (list.Length > 0)
                            {
                                returnval = "Zero Quantity:" + valuestring;
                            }
                        }
                    }
                    else
                    {
                        SqlDataAdapter sqlDA = new SqlDataAdapter("SP_UploadCheckGRStockInvalidData", con);
                        sqlDA.SelectCommand.CommandType = CommandType.StoredProcedure;
                        sqlDA.SelectCommand.Parameters.AddWithValue("BatchName", returnValue1);
                        DataTable dt = new DataTable();
                        sqlDA.Fill(dt);
                        List<StockEntity> JsonItemList = new List<StockEntity>();
                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                StockEntity x = new StockEntity();
                                x.ItemCode = dt.Rows[i]["ItemCode"].ToString();
                                x.Custodian = dt.Rows[i]["Custodian"].ToString();
                                x.Region = dt.Rows[i]["Region"].ToString();
                                x.SupplierName = dt.Rows[i]["Supplier"].ToString();
                                x.Approver = dt.Rows[i]["Approver"].ToString();
                                x.Remarks = "InvalidData";
                                JsonItemList.Add(x);
                            }
                            JavaScriptSerializer json = new JavaScriptSerializer();
                            json.MaxJsonLength = Int32.MaxValue;
                            returnval = json.Serialize(JsonItemList);
                            return returnval;
                        }
                    }
                }
            }
            con.Close();
        }
        return returnval;
    }

    #endregion GoodsReceiveUpload
}