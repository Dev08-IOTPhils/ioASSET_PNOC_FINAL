using AdvanceBll;
using AdvanceEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Operation_Module_JunkDisposal : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ModeOfDisposal();
        GetApprover();
    }

    [WebMethod]
    public static string GetJD()
    {
        JunkDisposalBll JunkDisposalBll = new JunkDisposalBll();
        string html = string.Empty;
        List<JunkDisposalEntity> JsonItemList = new List<JunkDisposalEntity>();
        try
        {
            DataTable dt = JunkDisposalBll.getJunkDisposal(Common.ConnString);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string action = " <center>";
                    JunkDisposalEntity x = new JunkDisposalEntity();


                    x.TransactionNo = dt.Rows[i]["DisposalNo"].ToString();
                    x.TransactionBy = dt.Rows[i]["TransactorName"].ToString();
                    x.ApprovedBy = dt.Rows[i]["ApproverName"].ToString();
                    x.Remarks = dt.Rows[i]["Remarks"].ToString();
                    x.Status = dt.Rows[i]["Status"].ToString();
                    if (dt.Rows[i]["Status"].ToString() == "Pending Approval")
                    {
                        action += "<button class='btn btn-xs btn-warning btn_Edit' type='button' title='Edit' onclick=\"editTransaction('" + dt.Rows[i]["JunkDisposalID"] + "','" + dt.Rows[i]["ModeOfDisposal"] + "','" + dt.Rows[i]["Remarks"] + "','" + dt.Rows[i]["ApproverID"] + "')\">Edit</button> <button class='btn btn-xs btn-primary btn_Module'  data-toggle='tooltip'  title='Download' onclick=\"DownloadTemplateJD('')\">Download</button> "
                            + " <button type='button' class='btn btn-xs btn-primary  btn_Attachment' data-toggle='tooltip' title='Add Attachment' onclick=\"addAttachment('" + dt.Rows[i]["DisposalNo"] + "','JunkDisposal')\">Upload</button> </center>";
                    }
                    else if (dt.Rows[i]["Status"].ToString() == "Pending Upload")
                    {
                        action += "<button class='btn btn-xs btn-primary btn_Module'  data-toggle='tooltip'  title='Download' onclick=\"DownloadTemplateJD('')\">Download</button> "
                            + " <button type='button' class='btn btn-xs btn-primary  btn_Attachment' data-toggle='tooltip' title='Add Attachment' onclick=\"addAttachment('" + dt.Rows[i]["DisposalNo"] + "','JunkDisposal')\">Upload</button> "
                             + "<button class='btn btn-xs btn-success btn_Module'  data-toggle='tooltip'  title='Complete' onclick=\"CompleteJD('" + dt.Rows[i]["DisposalNo"] + "')\">Complete</button></center>"; 
                    }
                    x.Action = action;
                    

                    JsonItemList.Add(x);

                }

                JavaScriptSerializer json = new JavaScriptSerializer();
                json.MaxJsonLength = Int32.MaxValue;
                html = json.Serialize(JsonItemList);


                return html;
            }

            else { return ""; }

        }
        catch (Exception ex)
        {
            return "";
        }

    }
    private void ModeOfDisposal()
    {
        ddlModeOfDisposal.Items.Clear();
        ddlModeOfDisposal.Items.Insert(0, "--- Select Mode of Disposal ---");
        ddlModeOfDisposal.Items.Insert(1, "Condemnation/Destruction of Properties");
        ddlModeOfDisposal.Items.Insert(2, "Transfer of Properties to Another Government Agency");
        ddlModeOfDisposal.Items.Insert(3, "Barter");
        ddlModeOfDisposal.Items.Insert(4, "Donation");
        ddlModeOfDisposal.Items.Insert(5, "Return to the Supplier/Vendor");
        ddlModeOfDisposal.Items.Insert(6, "Sale to Government Officials/Employees of the Agency");
        ddlModeOfDisposal.Items.Insert(7, "Public Auction");
        ddlModeOfDisposal.Items.Insert(8, "Negotiated Sale");
        ddlModeOfDisposal.Items.Insert(9, "Sale of Junk or Scrap and/or Recycling");
        ddlModeOfDisposal.Items.Insert(10, "Direct Negotiation");
        ddlModeOfDisposal.Items.Insert(11, "Lost");
    }

    public void GetApprover()
    {
        UserBll userBll = new UserBll();

        DataTable dt = userBll.GetApproverListBySite(Common.ConnString, Convert.ToInt32(Common.SiteID), Convert.ToInt32(Common.UserID));

        ddlApprover.DataSource = dt;
        ddlApprover.DataValueField = "UserID";
        ddlApprover.DataTextField = "FullName";
        ddlApprover.DataBind();
        ddlApprover.Items.Insert(0, "--- Select Approver ---");
    }

    [WebMethod]
    public static string SaveJD(int JDid,string Mod, int approver, string remarks)
    {
        JunkDisposalBll JunkDisposalBll = new JunkDisposalBll();

        return JunkDisposalBll.SaveJD(JDid,Mod, approver, remarks, Common.UserName, Common.ConnString).ToString();

    }

    [WebMethod]
    public static string CompleteJD(string TransactionNo)
    {
        JunkDisposalBll JunkDisposalBll = new JunkDisposalBll();

        return JunkDisposalBll.CompleteJD(TransactionNo, Common.UserName, Common.ConnString).ToString();

    }
    [WebMethod]
    public static string DownloadTemplate(string fileName)
    {

        string path = HttpContext.Current.Server.MapPath("~/FilesDownloadable/");

        //Read the File as Byte Array.
        byte[] bytes = File.ReadAllBytes(path + fileName);

        //Convert File to Base64 string and send to Client.
        return Convert.ToBase64String(bytes, 0, bytes.Length);
    }
}