using AdvanceBll;
using AdvanceEntity;
using OpenHtmlToPdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Custodian_Operation_Module_SupplyAllocation : BasePage
{
    string retSite = "";
    string retLoc = "";
    string retArea = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        SystemConfigurations shSystemConfigurationsSite = (SystemConfigurations)Session["shSystemConfigurationsSite"];
        retSite = shSystemConfigurationsSite.Value;
        lblDisplayFromSite.Text = "From " + retSite + "/Department:";
        lblDisplayToSite.Text = "To " + retSite + "/Department:";


        CheckAccess();
        GetApprover();
    }

    #region metadata

    public static string ViewAccess = "";
    public static string AddAccess = "";
    public static string EditAccess = "";
    public static string DeleteAccess = "";

    public void CheckAccess()
    {
        RoleBll rb = new RoleBll();

        string AccessButon = rb.CheckR("Custodian_Operation_Module/SupplyAllocation.aspx", Convert.ToInt32(Common.Role), Common.ConnString);
        string[] ButtonAccess = AccessButon.Split('|');
        ViewAccess = ButtonAccess[0];
        AddAccess = ButtonAccess[1];
        EditAccess = ButtonAccess[2];
        DeleteAccess = ButtonAccess[3];
    }

    [WebMethod]
    public static List<ListItem> GetWarehouseByID()
    {
        WarehouseBll warehouseBll = new WarehouseBll();
        WarehouseEntity warehouseEntity = new WarehouseEntity();

        warehouseEntity.WarehouseID = Convert.ToInt32(Common.SiteID);
        warehouseEntity.ConnectionString = Common.ConnString;

        return warehouseBll.GetWarehouseByID(warehouseEntity);
    }

    [WebMethod]
    public static string GetWarehousePT()
    {
        WarehouseBll warehouseBll = new WarehouseBll();

        DataTable dt = warehouseBll.GetWarehouseList(Common.ConnString);

        return Control.MyConverter.ConvertDataTabletoJSON(dt);
    }

    [WebMethod]
    public static string GetLocationListPT(int warehouse)
    {
        LocationEntity locationEntity = new LocationEntity();
        LocationBll locationBll = new LocationBll();

        locationEntity.WarehouseID = warehouse;
        locationEntity.ConnectionString = Common.ConnString;

        DataTable dt = locationBll.GetLocationListBySitePT(locationEntity);
        return Control.MyConverter.ConvertDataTabletoJSON(dt);
    }

    [WebMethod]
    public static string GetOfficeListsPT(int location)
    {
        AreaEntity areaEntity = new AreaEntity();
        AreaBll areaBll = new AreaBll();

        areaEntity.LocationID = location;
        areaEntity.ConnectionString = Common.ConnString;

        DataTable dt = areaBll.GetAreaListByLocationPT(areaEntity);
        return Control.MyConverter.ConvertDataTabletoJSON(dt);
    }
    //To

    [WebMethod]
    public static string GetWarehouseToPT()
    {
        WarehouseBll warehouseBll = new WarehouseBll();

        DataTable dt = warehouseBll.GetWarehouseList(Common.ConnString);

        return Control.MyConverter.ConvertDataTabletoJSON(dt);
    }

    //end
    [WebMethod]
    public static string GetCustodianID()
    {
        UserEntity userEntity = new UserEntity();
        UserBll userBll = new UserBll();

        userEntity.UserName = Common.UserName;
        userEntity.ConnectionString = Common.ConnString;

        return userBll.GetCustodianID(userEntity);
    }



    [WebMethod]
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

    //added by: domdom

    [WebMethod]
    public static string StockTransferToAllocation(int smlid, int fromsite, int tosite, int quantity, int custodian)
    {
        SupplyAllocationBll y = new SupplyAllocationBll();
        StockEntity x = new StockEntity();

        x.RecID = smlid;
        //x.EmployeeID = employeeid;
        x.FromSite = fromsite;
        x.ToSite = tosite;
        x.CustodianID = custodian;
        x.Quantity = quantity;
        x.CreatedBy = Common.UserName;
        x.ConnectionString = Common.ConnString;

        return y.StockTransferToAllocation(x).ToString();
    }

    #endregion metadata

    #region Datatables

    [WebMethod]
    public static string GetList(int site, int tosite, string search, int custodianid)
    {
        StockEntity stocktyEntity = new StockEntity();
        SupplyAllocationBll supplyallocationBll = new SupplyAllocationBll();

        stocktyEntity.ConnectionString = Common.ConnString;

        string html = string.Empty;
        List<StockEntity> JsonItemList = new List<StockEntity>();

        try
        {
            DataTable dt = supplyallocationBll.GetAvailableSupplyForTransfer(stocktyEntity, site, search, custodianid);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    StockEntity x = new StockEntity();
                    x.RecID = Convert.ToInt32(dt.Rows[i]["RecID"]);
                    x.ItemCode = dt.Rows[i]["ItemCode"].ToString();
                    x.Description = dt.Rows[i]["Description"].ToString();
                    x.QuantityString = dt.Rows[i]["Quantity"].ToString();
                    x.UnitOfMeasurement = dt.Rows[i]["UOMcode"].ToString();
                    x.Location = dt.Rows[i]["WarehouseName"].ToString();
                    x.Classification = dt.Rows[i]["ClassificationName"].ToString();
                    x.Custodian = dt.Rows[i]["Custodian"].ToString();
                    if (AddAccess == "0")
                    {
                        x.Action = "";
                    }
                    else
                    {
                        x.Action = " <center><a href='' class='btn btn-xs btn-success fa fa-plus btn_Add' smlid='" + dt.Rows[i]["RecID"].ToString() + "' data-toggle='tooltip'  title='Add To Allocation List' type='button'></a> </center>";
                    }

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

    [WebMethod]
    public static string GetToTransferAssetList(int fromsite, int tosite, int custodian)
    {
        StockEntity stockEntity = new StockEntity();
        SupplyAllocationBll supplyallocationBll = new SupplyAllocationBll();

        stockEntity.SiteID = fromsite;
        stockEntity.ToSite = tosite;
        stockEntity.CustodianID = custodian;
        stockEntity.CreatedBy = Common.UserName;
        stockEntity.ConnectionString = Common.ConnString;

        string html = string.Empty;
        List<StockEntity> JsonItemList = new List<StockEntity>();
        try
        {
            DataTable dt = supplyallocationBll.GetToTransferAssetList(stockEntity);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    StockEntity x = new StockEntity();
                    x.RecID = Convert.ToInt32(dt.Rows[i]["RecordID"]);
                    x.ItemCode = dt.Rows[i]["ItemCode"].ToString();
                    x.Description = dt.Rows[i]["Description"].ToString();
                    x.QuantityString = dt.Rows[i]["Quantity"].ToString();
                    x.UnitOfMeasurement = dt.Rows[i]["UOMcode"].ToString();
                    //x.Location = dt.Rows[i]["WarehouseName"].ToString();
                    x.Classification = dt.Rows[i]["ClassificationName"].ToString();
                    x.Custodian = dt.Rows[i]["Custodian"].ToString();

                    x.Action = " <center><button class='btn btn-xs btn-danger btn_Remove fa fa-close' smlid='" + dt.Rows[i]["RecordID"].ToString() + "' data-toggle='tooltip'  title='Remove from List' type='button' ></button></center>";

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

    [WebMethod]
    public static string RemoveToTransfer(int smlid)
    {
        StockEntity stockEntity = new StockEntity();
        SupplyAllocationBll supplyallocationBll = new SupplyAllocationBll();

        stockEntity.RecID = smlid;
        stockEntity.CreatedBy = Common.UserName;
        stockEntity.ConnectionString = Common.ConnString;
        //propertyEntity.TransferType = trasferType;

        supplyallocationBll.RemoveToTransfer(stockEntity);

        return "";
    }
    //[WebMethod]
    //public static void UploadSupplyAllocation(string transactionno)
    //{
    //    StockEntity stockEntity = new StockEntity();
    //    SupplyAllocationBll supplyallocationBll = new SupplyAllocationBll();

    //    stockEntity.StockAllocationNo = transactionno;
    //    stockEntity.CreatedBy = Common.UserName;
    //    stockEntity.ConnectionString = Common.ConnString;
    //    supplyallocationBll.UploadSupplyAllocation(stockEntity);
    //}

    [WebMethod]
    public static string UploadSupplyAllocation(string transactionno)
    {
        SupplyAllocationBll y = new SupplyAllocationBll();

        DataTable dt = y.UploadSupplyAllocation(Common.ConnString, transactionno, Common.UserName);

        string valuestring = "";
        List<string> returnValue = new List<string>();
        if (dt.Rows.Count >= 1)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                returnValue.Add(dt.Rows[i]["Returnvalue"].ToString());
            }
            string[] list = returnValue.ToArray();
            valuestring = string.Join(", ", list);
            if (list.Length > 1)
            {
                return valuestring;
            }
            else
            {
                if (valuestring == "Success")
                {
                    return valuestring;
                }
                else
                {
                    return valuestring;
                }
            }
        }
        return valuestring;
    }

    [WebMethod]
    public static string PropertyTransferPendingApproval(string search, int custodian)
    {
        SupplyAllocationBll supplyallocationBll = new SupplyAllocationBll();

        string html = string.Empty;
        List<StockEntity> JsonItemList = new List<StockEntity>();
        try
        {
            DataTable dt = supplyallocationBll.PropertyTransferPendingApproval(search, Common.ConnString, Common.CustodianID, Convert.ToInt32(Common.SiteID), Common.Administrative);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    StockEntity x = new StockEntity();
                    x.StockAllocationNo = dt.Rows[i]["StockAllocationNo"].ToString();
                    x.Description = dt.Rows[i]["Remarks"].ToString();
                    x.FromSiteStr = dt.Rows[i]["FromSiteName"].ToString();
                    x.ToSiteStr = dt.Rows[i]["ToSiteName"].ToString();
                    //x.StringNoOfItem = dt.Rows[i]["Quantity"].ToString();
                    x.CreatedBy = dt.Rows[i]["TransactorName"].ToString();
                    x.TransactionDate = dt.Rows[i]["CreatedDate"].ToString();
                    x.Status = dt.Rows[i]["Status"].ToString();


                    string action = "<center><button type = 'button' class='btn btn-xs btn-primary  btn_Attachment' data-toggle='tooltip' title='Add Attachment' onclick=\"addAttachment('" + dt.Rows[i]["StockAllocationNo"] + "','PropertyTransfer')\"><span class='fa fa-paperclip'></span></button>";
                    if (ViewAccess == "0")
                    {
                        action += "";
                    }
                    else
                    {
                        action += " <button class='btn btn-xs btn-primary btn_View' data-toggle='tooltip'  title='View List of Properties' onclick=\"ViewStockAllocationDetails('" + dt.Rows[i]["StockAllocationNo"] + "','" + dt.Rows[i]["TransactorName"] + "','" + dt.Rows[i]["CreatedDate"] + "','" + dt.Rows[i]["TransactorEmail"] + "','" + dt.Rows[i]["Status"] + "')\"><span class='fa fa-eye'></span></button>";
                    }
                    if (DeleteAccess == "0")
                    {
                        action += "";
                    }
                    else
                    {
                        action += " <button class='btn btn-xs btn-danger btn_Cancel' data-toggle='tooltip'  title='Cancel Transaction' onclick=\"CancelTransaction('" + dt.Rows[i]["StockAllocationNo"] + "')\" ><span class='fa fa-times-circle'></span></button></center>";
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

    [WebMethod]
    public static string CancelPropertyTransfer(string transactionno, string remarks)
    {
        StockEntity stockEntity = new StockEntity();
        SupplyAllocationBll supplyallocationBll = new SupplyAllocationBll();

        stockEntity.StockAllocationNo = transactionno;
        stockEntity.CreatedBy = Common.UserName;
        stockEntity.ConnectionString = Common.ConnString;
        stockEntity.Description = remarks;

        supplyallocationBll.CancelPropertyTransfer(stockEntity);

        return "";
    }

    [WebMethod]
    public static string PLPendingApproval(string transactionno)
    {
        SupplyAllocationBll supplyallocationBll = new SupplyAllocationBll();

        string html = string.Empty;
        List<StockEntity> JsonItemList = new List<StockEntity>();
        try
        {
            DataTable dt = supplyallocationBll.PLPendingApproval(Common.ConnString, transactionno);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    StockEntity x = new StockEntity();
                    x.StockAllocationNo = dt.Rows[i]["StockAllocationNo"].ToString();
                    x.Description = dt.Rows[i]["Description"].ToString();
                    x.ItemCode = dt.Rows[i]["ItemCode"].ToString();
                    x.FromSiteStr = dt.Rows[i]["FromSiteID"].ToString();
                    x.ToSiteStr = dt.Rows[i]["ToSiteID"].ToString();
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

    [WebMethod]
    public static string ConfirmPropertyTransfer(int approverid, string purpose, int fromsite, int tosite, int custodianid)
    {
        try
        {
            StockEntity stockEntity = new StockEntity();
            SupplyAllocationBll supplyallocationBll = new SupplyAllocationBll();

            stockEntity.SiteID = fromsite;
            stockEntity.ToSite = tosite;
            stockEntity.CreatedBy = Common.UserName;
            stockEntity.Remarks = purpose;
            stockEntity.ApproverID = approverid;
            stockEntity.CustodianID = custodianid;
            stockEntity.ConnectionString = Common.ConnString;

            //string smtphost = Common.SMTPhost;
            //string smtpport = Common.SMTPport;
            //string smtpmail = Common.SMTPmail;
            //string smtppass = Common.SMTPpass;

            string transferno = supplyallocationBll.ConfirmPropertyTransfer(stockEntity);

            //DataTable dt = supplyallocationBll.PLPendingApproval(Common.ConnString, transferno);

            //string transferdetails = propertytransferBll.PropertyTransferDetails(Common.ConnString, transferno);
            //string approvername = transferdetails.Split('|')[1];
            //string approveremail = transferdetails.Split('|')[2];
            //string transactorname = transferdetails.Split('|')[3];
            //string transactoremail = transferdetails.Split('|')[4];
            //string custodian = transferdetails.Split('|')[5];
            //string remarks = transferdetails.Split('|')[6];
            //string createddate = transferdetails.Split('|')[7];

            return "sorry";

        }
        //if (approveremail != "")
        //{
        //    string hostaddress = (HttpContext.Current.Request.Url.AbsoluteUri).Replace(HttpContext.Current.Request.Url.AbsolutePath, "");

        //    MailMessage mail = new MailMessage();
        //    mail.To.Add(approveremail);

        //    mail.From = new MailAddress(smtpmail);
        //    mail.Subject = Common.EmailSubject + " (Property Transfer)";
        //    string Body = "<font face='Consolas'><br><br><i>***This is a system generated mail. Please do not reply.***</i><br><br><br> Hi " + approvername + ",<br><br><br>";
        //    if (TransferType == 1) //if person transfer type
        //    {
        //        Body += "A PROPERTY TRANSFER transaction is AWAITING FOR YOUR APPROVAL: <br><br><a href='" + hostaddress + "/Home/RequestManagement.aspx'>Please click here to redirect to management approval page.</a><br><br>"
        //       + "Transfer No : " + transferno + "<br>"
        //       + "Remarks : " + remarks + " <br>"
        //       + "Processed By : " + transactorname + " <br>"
        //       + "Processed Date : " + createddate + "<br>"
        //       + "Source : " + dt.Rows[0]["FromEmployeeName"].ToString() + "<br>"
        //       + "Destination : " + dt.Rows[0]["ToEmployeeName"].ToString() + "<br><br>";
        //    }
        //    else
        //    {
        //        Body += "A PROPERTY TRANSFER transaction is AWAITING FOR YOUR APPROVAL: <br><br><a href='" + hostaddress + "/Home/RequestManagement.aspx'>Please click here to redirect to management approval page.</a><br><br>"
        //            + "Transfer No : " + transferno + "<br>"
        //            + "Remarks : " + remarks + " <br>"
        //            + "Processed By : " + transactorname + " <br>"
        //            + "Processed Date : " + createddate + "<br>"
        //            + "Source : " + dt.Rows[0]["FromSiteName"].ToString() + " - " + dt.Rows[0]["FromLocationName"].ToString() + " - " + dt.Rows[0]["FromAreaName"].ToString() + "<br>"
        //            + "Destination : " + dt.Rows[0]["ToSiteName"].ToString() + " - " + dt.Rows[0]["ToLocationName"].ToString() + " - " + dt.Rows[0]["ToAreaName"].ToString() + "<br><br>";
        //    }
        //    Body += "<table border='1'><tr>"
        //        + "<th>Property No</th>"
        //        + "<th>Description</th>"
        //        + "<th>Serial No</th></tr>";
        //    if (dt.Rows.Count > 0)
        //    {
        //        for (int i = 0; i < dt.Rows.Count; i++)
        //        {
        //            Body += "<tr>"
        //                + "<td>" + dt.Rows[i]["PropertyNumber"].ToString() + "</td>"
        //                + "<td>" + dt.Rows[i]["Description"].ToString() + "</td>"
        //                + "<td>" + dt.Rows[i]["SerialNumber"].ToString() + "</td>"
        //                + "</tr>";
        //        }
        //        Body += "</table>";
        //    }
        //    Body += "<br><br><br><i>***This is a system generated mail. Please do not reply.***</i></font>";

        //    mail.Body = Body;
        //    SmtpClient smtp = new SmtpClient();
        //    smtp.Host = smtphost; //Or Your SMTP Server Address
        //    smtp.Port = Convert.ToInt32(smtpport);
        //    smtp.UseDefaultCredentials = false;
        //    smtp.Credentials = new System.Net.NetworkCredential
        //    (smtpmail, smtppass);

        //    //Or your Smtp Email ID and Password
        //    mail.IsBodyHtml = true;
        //    smtp.EnableSsl = true;

        //    ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
        //    smtp.Send(mail);

        //    return "okay";


        //else
        //{
        //    return "sorry";
        //}
        catch (Exception ex)
        {
            ex.ToString();

            return "sorry";
        }
    }


    [WebMethod]
    public static string SupplyAllocationApproval()
    {
        return Common.SupplyAllocationApproval;
    }


    [WebMethod]
    public static string RemoveAllToTransfer(int fromsite, int tosite, int custodian)
    {
        StockEntity supplyEntity = new StockEntity();
        SupplyAllocationBll supplyallocationBll = new SupplyAllocationBll();

        supplyEntity.SiteID = fromsite;
        supplyEntity.ToSite = tosite;
        supplyEntity.CreatedBy = Common.UserName;
        supplyEntity.ConnectionString = Common.ConnString;
        supplyEntity.CustodianID = custodian;


        supplyallocationBll.RemoveAllToTransfer(supplyEntity);

        return "";
    }

    [WebMethod]
    public static string SupplyTransferGetCount(int fromsite, int tosite, int custodian)
    {
        StockEntity stockEntity = new StockEntity();
        SupplyAllocationBll supplyallocationBll = new SupplyAllocationBll();

        stockEntity.SiteID = fromsite;
        stockEntity.ToSite = tosite;
        stockEntity.CreatedBy = Common.UserName;
        stockEntity.ConnectionString = Common.ConnString;
        stockEntity.CustodianID = custodian;

        return supplyallocationBll.SupplyTransferGetCount(stockEntity);
    }
    #endregion Datatables
}