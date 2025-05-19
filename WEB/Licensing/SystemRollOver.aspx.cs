using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Xml;
using System.Data.SqlClient;
using AdvanceBll;
using AdvanceEntity;

public partial class Licensing_SystemRollOver : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void AddUpdateConnectionString(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:startloading();", true);
        string name = DateTime.Now.Year.ToString();
        bool isNew = false;
        string path = Server.MapPath("~/Web.Config");
        XmlDocument doc = new XmlDocument();
        doc.Load(path);
        XmlNodeList list = doc.DocumentElement.SelectNodes(string.Format("connectionStrings/add[@name='{0}']", name));
        XmlNode node;
        isNew = list.Count == 0;
        if (isNew)
        {
            node = doc.CreateNode(XmlNodeType.Element, "add", null);
            XmlAttribute attribute = doc.CreateAttribute("name");
            attribute.Value = name;
            node.Attributes.Append(attribute);

            attribute = doc.CreateAttribute("connectionString");
            attribute.Value = "";
            node.Attributes.Append(attribute);

            //attribute = doc.CreateAttribute("providerName");
            //attribute.Value = "System.Data.SqlClient";
            //node.Attributes.Append(attribute);
        }
        else
        {
            node = list[0];
        }
        string conString = node.Attributes["connectionString"].Value;
        SqlConnectionStringBuilder conStringBuilder = new SqlConnectionStringBuilder(conString);
        conStringBuilder.InitialCatalog = "IOT_AIMMIS_ADVANCE_" + name;
        conStringBuilder.DataSource = "LAPTOP-UOHK8644\\MSSQLSERVER2012";
        conStringBuilder.IntegratedSecurity = false;
        conStringBuilder.UserID = "sa";
        conStringBuilder.Password = "P@ssw0rd";
        node.Attributes["connectionString"].Value = conStringBuilder.ConnectionString;
        if (isNew)
        {
            doc.DocumentElement.SelectNodes("connectionStrings")[0].AppendChild(node);
        }
        doc.Save(path);

        //Set Your SQL Server Name Here
        DBHelper.SourceSQLServer = @"LAPTOP-UOHK8644\MSSQLSERVER2012";

        //Set Your Database Name Here (To Be Copied or Scripted)
        DBHelper.SourceDatabase = "IOT_AIMMIS_ADVANCE";

        ////Set Destination SQL Server Name Here
        DBHelper.DestinationSQLServer = @"LAPTOP-UOHK8644\MSSQLSERVER2012";

        //Set New Database Name Here 
        DBHelper.DestinationDatabase = "IOT_AIMMIS_ADVANCE_" + name;

        //Set True if you want to copy Data
        //Set False if you want to copy Only Schema
        DBHelper.CopyDatabase(true);

        MiscBll mb = new MiscBll();

        mb.TruncateTables(Common.ConnString, "IOT_AIMMIS_ADVANCE_" + name);

        ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:endloading();", true);

        Response.Write("<script>alert('System Roll Over Successfully done.');</script>");
        
        Common.LogOut();
        Response.Redirect("../Login.aspx");
    }
}