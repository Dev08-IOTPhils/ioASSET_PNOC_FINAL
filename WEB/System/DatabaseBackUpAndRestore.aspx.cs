using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.IO;
using System.Data.OleDb;
using AdvanceBll;
using AdvanceEntity;
using System.Web.Services;
using System.Data;
using System.Data.SqlClient;
using System.Text;

public partial class System_DatabaseBackUpAndRestore : BasePage
{
    static SqlConnection con = new SqlConnection();
    static SqlCommand sqlcmd = new SqlCommand();
    static SqlDataAdapter da = new SqlDataAdapter();
    static DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        GetDirectory();
        if (!IsPostBack)
        {
            // Add __doPostback function reference to the page
            ClientScript.GetPostBackEventReference(this, string.Empty);
        }
    }

    [WebMethod]
    public static string GetModule()
    {
        string a = "";
        bool meron = false;
        UserEntity userEntity = new UserEntity();
        UserBll userBll = new UserBll();
        MiscBll miscBll = new MiscBll();

        userEntity.UserName = Common.UserName;
        userEntity.ConnectionString = Common.ConnString;

        DataTable dt = userBll.GetModule(userEntity);

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (dt.Rows[i]["Name"].ToString() == "Restore Database")
            {
                meron = true;
                break;
            }
            else
            {
                meron = false;
                continue;
            }
        }
        if (meron == true)
        {
            return "wag itago";
        }
        else
        {
            return "itago";
        }
    }

    public void GetDirectory()
    {
        string conString = ConfigurationManager.ConnectionStrings[Common.ConnString].ToString();
        SqlConnection connection = new SqlConnection(conString);
        connection.Open();
        string q1 = "Select DBDirectoryPath from tbl_Miscellaneous";
        SqlCommand sqlCmd1 = new SqlCommand(q1, connection);

        SqlDataReader dr1 = sqlCmd1.ExecuteReader();
        if (dr1.Read())
        {
            txtDirectory.Text = dr1[0].ToString().Replace('.', '\\');
        }
        else
        {

        }
        connection.Close();
    }


    protected void BackUp(object sender, EventArgs e)
    {
        con.ConnectionString = ConfigurationManager.ConnectionStrings[Common.ConnString].ConnectionString;

        bool valid = false;
        string backupDIR = txtDirectory.Text;
        try
        {
            if (!System.IO.Directory.Exists(backupDIR))
            {
                System.IO.Directory.CreateDirectory(backupDIR);
            }
            valid = true;
        }
        catch
        {
            //Response.Write("<script>swal('Error','Invalid Path / Directory','error')</script>");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:invalidPath();", true);
            //swal("Successful", "Depreciation Successfully Set!", "success");
            valid = false;
        }

        if (valid == true)
        {
            try
            {

                System.Data.SqlClient.SqlConnectionStringBuilder builder = new System.Data.SqlClient.SqlConnectionStringBuilder(con.ConnectionString);
                string server = builder.DataSource;
                string database = builder.InitialCatalog;
                con.Open();
                sqlcmd = new SqlCommand("backup database "+database+" to disk='" + backupDIR + "IOT_AIMMIS_ADVANCE_" + DateTime.Now.ToString("yyMMdd_HHmmss") + ".bak'", con);
                sqlcmd.ExecuteNonQuery();
                con.Close();
                //Response.Write("<script>swal('Succes','Backup database successful!', 'success');</script>");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:SuccessBackup();", true);
            }
            catch (Exception exee)
            {
                exee.ToString();
                //Response.Write("<script>swal('Error','Error Occured During DB backup process!','error');</script>");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:ErrorBackup();", true);
                con.Close();
            }
        }
    }


    [WebMethod]
    public static string RestoreDatabase(string bakfilename, string restorationname)
    {
        MiscBll miscBll = new MiscBll();
        MiscEntity miscEntity = new MiscEntity();

        miscEntity.RestoreName = restorationname;
        miscEntity.BakFileName = bakfilename;
        miscEntity.CreatedBy = Common.UserName;
        miscEntity.ConnectionString = Common.ConnString;

        miscBll.RestoreDatabase(miscEntity);

        return "";
    }
    


    protected void Upload(object sender, EventArgs e)
    {

        MiscBll miscBll = new MiscBll();
        MiscEntity miscEntity = new MiscEntity();
        Boolean fileOK = false;
        String path = Server.MapPath("~/System/DatabaseRestorePath/");
        String dbserverpath = txtDirectory.Text;
        if (FileUpload1.HasFile)
        {
            String fileExtension = System.IO.Path.GetExtension(FileUpload1.FileName).ToLower();
            String[] allowedExtensions = { ".bak" };
            for (int i = 0; i < allowedExtensions.Length; i++)
            {
                if (fileExtension == allowedExtensions[i])
                {
                    fileOK = true;
                }
            }
            if (fileOK)
            {
                try
                {
                    if (miscBll.GetSystemSetting(Common.ConnString).Split('|')[8].ToString() == "True")
                    {
                        FileUpload1.SaveAs(path + FileUpload1.FileName);
                        RestoreDatabase(dbserverpath + FileUpload1.FileName, FileUpload1.FileName);
                        //Response.Write("<script>swal('Success','Successfully Requested the Database Restoration.','success');</script>");
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Success();", true);
                    }
                    else
                    {
                        try
                        {
                            con.ConnectionString = ConfigurationManager.ConnectionStrings["aimmis1"].ConnectionString;
                            con.Open();
                            string sourceDir = txtDirectory.Text;

                            sqlcmd = new SqlCommand("Alter Database IOT_AIMMIS_ADVANCE SET SINGLE_USER With ROLLBACK IMMEDIATE Restore database IOT_AIMMIS_ADVANCE from disk='" + sourceDir + FileUpload1.FileName + "' Alter Database IOT_AIMMIS_ADVANCE SET MULTI_USER With ROLLBACK IMMEDIATE", con);
                            sqlcmd.ExecuteNonQuery();
                            //Response.Write("<script>alert('Restore database successfully');</script>");
                            //Response.Write("<script>swal('Success','Successful Database Restoration.','success');</script>");
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:successRetore();", true);
                            con.Close();
                            Common.LogOut();
                            Response.Redirect("../Login.aspx");
                        }
                        catch (Exception ex)
                        {
                            //Response.Write("<script>alert('Error during database restoration!');</script>");
                            //Response.Write("<script>swal('Error','Error during database restoration!','error');</script>");
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:errorRetore();", true);
                        }
                    }
                    
                }
                catch (Exception ex)
                {
                    //Label1.Text = "File could not be uploaded.";
                }
            }
            else
            {
                //Response.Write("<script>swal('Warning','Please Select .bak file to restore.','warning');</script>");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:invalidFile();", true);
            }
        }
        else
        {
            //Response.Write("<script>swal('Warning','Please Select a file to restore','warning');</script>");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:invalidFile();", true);
        }
    }

    protected void Restore(object sender, EventArgs e)
    {
        Boolean fileOK = false;
        if (FileUpload1.HasFile)
        {
            String fileExtension = System.IO.Path.GetExtension(FileUpload1.FileName).ToLower();
            String[] allowedExtensions = { ".bak" };
            for (int i = 0; i < allowedExtensions.Length; i++)
            {
                if (fileExtension == allowedExtensions[i])
                {
                    fileOK = true;
                }
            }
            if (fileOK)
            {

                try
                {
                    con.ConnectionString = ConfigurationManager.ConnectionStrings["aimmis1"].ConnectionString;
                    con.Open();
                    string sourceDir = txtDirectory.Text;

                    sqlcmd = new SqlCommand("Alter Database IOT_AIMMIS_ADVANCE SET SINGLE_USER With ROLLBACK IMMEDIATE Restore database IOT_AIMMIS_ADVANCE from disk='" + sourceDir + FileUpload1.FileName + "' Alter Database IOT_AIMMIS_ADVANCE SET MULTI_USER With ROLLBACK IMMEDIATE", con);
                    sqlcmd.ExecuteNonQuery();
                    Response.Write("<script>swal('Success','Restore database successfully','success'); preventDefault();</script>");

                    Common.LogOut();
                    Response.Redirect("../Login.aspx");
                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('Warning','Error during database restoration!','warning'); preventDefault();</script>");
                }
            }
            else
            {
                //Response.Write("<script>alert('Please Select .bak file to restore.');</script>");
                //Response.Write("<script>alert('Warning','Please Select .bak file to restore.','warning'); preventDefault();</script>");

                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:nofile(this);", true);
            }
        }
        else
        {
            //Response.Write("<script>alert('Please Select a file to restore');</script>");
            //Response.Write("<script>swal('Warning','Please Select a file to restore','warning'); preventDefault();</script>");
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:nofile(this);", true);
        }
    }
}