using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AdvanceBll;
using AdvanceEntity;
using System.Web.Services;
using System.Data;
using System.Text;
using System.Net;
using System.Web.Script.Serialization;
using QRCoder;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Net.Mail;

public partial class Operation_Module_PropertyTagging : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        CheckAccess();
        GetTagType();
        GetDeviceListUSBWriter();
        TaggingOption();
    }

    //Property

    #region Metadata

    public static string ViewAccess = "";
    public static string AddAccess = "";
    public static string EditAccess = "";
    public static string DeleteAccess = "";
    public void CheckAccess()
    {
        RoleBll rb = new RoleBll();

        string AccessButon = rb.CheckR("Operation_Module/PropertyTagging.aspx", Convert.ToInt32(Common.Role), Common.ConnString);
        string[] ButtonAccess = AccessButon.Split('|');
        ViewAccess = ButtonAccess[0];
        AddAccess = ButtonAccess[1];
        EditAccess = ButtonAccess[2];
        DeleteAccess = ButtonAccess[3];
    }
    private void TaggingOption()
    {
        ddlTaggingOption.Items.Clear();
        ddlTaggingOption.Items.Insert(0, "--- Select Tagging type ---");
        ddlTaggingOption.Items.Insert(1, "Tagging");
        ddlTaggingOption.Items.Insert(2, "Re-tagging");

    }

    private void GetTagType()
    {
        TagTypeBll tagTypeBll = new TagTypeBll();

        DataTable dt = tagTypeBll.GetTag(Common.ConnString);

        ddlTagType.DataSource = dt;
        ddlTagType.DataValueField = "TagID";
        ddlTagType.DataTextField = "TagName";
        ddlTagType.DataBind();
        ddlTagType.Items.Insert(0, "--- Select Tag ---");
    }

    [WebMethod]
    public static string GetTagByID(int tagid)
    {
        TagTypeBll y = new TagTypeBll();
        TagTypeEntity x = new TagTypeEntity();

        x.TagID = tagid;
        x.ConnectionString = Common.ConnString;

        return y.GetTagByID(x).Split('|')[1];
    }

    private void GetDeviceListUSBWriter()
    {
        TagTypeBll tagTypeBll = new TagTypeBll();

        DataTable dt = tagTypeBll.GetDeviceListUSBWriter(Common.ConnString);

        ddlCom.DataSource = dt;
        ddlCom.DataValueField = "DeviceID";
        ddlCom.DataTextField = "DeviceName";
        ddlCom.DataBind();
        ddlCom.Items.Insert(0, "--- Select USB Writer ---");
    }

    [WebMethod]
    public static string GetDeviceStatus(int deviceid)
    {
        DeviceEntity deviceEntity = new DeviceEntity();
        DeviceBll deviceBll = new DeviceBll();

        deviceEntity.DeviceID = deviceid;
        deviceEntity.ConnectionString = Common.ConnString;

        return deviceBll.DevicebyID(deviceEntity).Split('|')[7];
    }

    [WebMethod]
    public static string GetDeviceWriterStatus(int deviceid)
    {
        DeviceEntity deviceEntity = new DeviceEntity();
        DeviceBll deviceBll = new DeviceBll();

        deviceEntity.DeviceID = deviceid;
        deviceEntity.ConnectionString = Common.ConnString;

        return deviceBll.DevicebyID(deviceEntity).Split('|')[7];
    }
    [WebMethod]
    public static List<ListItem> GetApprover()
    {
        UserEntity userEntity = new UserEntity();
        UserBll userBll = new UserBll();
        return userBll.GetApproverForRetagging(Common.ConnString);
    }
    #endregion

    #region datatables
    [WebMethod]
    public static string PropertyAvailableForTagging(int tagtype)
    {
        PropertyTaggingBll assettaggingBll = new PropertyTaggingBll();

        string html = string.Empty;
        List<PropertyEntity> JsonItemList = new List<PropertyEntity>();
        try
        {
            DataTable dt = assettaggingBll.PropertyAvailableForTagging(Common.ConnString, tagtype);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    PropertyEntity x = new PropertyEntity();
                    x.pmlid = Convert.ToInt32(dt.Rows[i]["RecID"]);
                    x.PropertyNumber = dt.Rows[i]["PropertyNumber"].ToString();
                    x.ItemCode = dt.Rows[i]["ItemCode"].ToString();
                    x.Description = dt.Rows[i]["Description"].ToString() + ". " + dt.Rows[i]["LongDescription"].ToString();
                    x.SerialNo = dt.Rows[i]["SerialNumber"].ToString();
                    x.ClassificationName = dt.Rows[i]["ClassificationName"].ToString();
                    x.CategoryName = dt.Rows[i]["CategoryName"].ToString();
                    x.BrandName = dt.Rows[i]["BrandName"].ToString();
                    x.ModelName = dt.Rows[i]["ModelName"].ToString();
                    if (AddAccess == "0")
                    {
                        x.Action = "";
                    }
                    else
                    {
                        if (dt.Rows[i]["TagType"].ToString() == "4")
                        {
                            x.Action = " <center><button class='btn btn-xs btn-success fa fa-tag' data-toggle='tooltip'  title='Tag Property' type='button' onclick=\"TagProperty('" + dt.Rows[i]["RecID"] + "')\"></button></center>";
                        }
                        else
                        {
                            x.Action = " <center><button class='btn btn-xs btn-success fa fa-plus btn_Add' data-toggle='tooltip'  title='Add To Tagging List' type='button' onclick=\"AddToPrint('" + dt.Rows[i]["RecID"] + "')\"></button></center>";
                        }
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
    public static string PropertyAvailableForRetagging(int tagtype)
    {
        PropertyTaggingBll assettaggingBll = new PropertyTaggingBll();

        string html = string.Empty;
        List<PropertyEntity> JsonItemList = new List<PropertyEntity>();
        try
        {
            DataTable dt = assettaggingBll.PropertyAvailableForRetagging(Common.ConnString, tagtype);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    PropertyEntity x = new PropertyEntity();
                    x.pmlid = Convert.ToInt32(dt.Rows[i]["RecID"]);
                    x.PropertyNumber = dt.Rows[i]["PropertyNumber"].ToString();
                    x.ItemCode = dt.Rows[i]["ItemCode"].ToString();
                    x.Description = dt.Rows[i]["Description"].ToString();
                    x.SerialNo = dt.Rows[i]["SerialNumber"].ToString();
                    x.ClassificationName = dt.Rows[i]["ClassificationName"].ToString();
                    x.CategoryName = dt.Rows[i]["CategoryName"].ToString();
                    x.BrandName = dt.Rows[i]["BrandName"].ToString();
                    x.ModelName = dt.Rows[i]["ModelName"].ToString();
                    if (AddAccess == "0")
                    {
                        x.Action = "";
                    }
                    else
                    {
                        if (dt.Rows[i]["TagType"].ToString() == "4")
                        {
                            x.Action = " <center><button class='btn btn-xs btn-success fa fa-tag btn_Tag' data-toggle='tooltip'  title='Tag Property' type='button' onclick=\"TagProperty('" + dt.Rows[i]["RecID"] + "')\"></button></center>";
                        }
                        else
                        {
                            x.Action = " <center><button class='btn btn-xs btn-success fa fa-plus btn_Add' data-toggle='tooltip'  title='Add To Tagging List' type='button' onclick=\"AddToPrint('" + dt.Rows[i]["RecID"] + "')\"></button></center>";
                        }
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
    public static string GetPrintList(int tagtype, int deviceid)
    {
        PropertyTaggingBll assettaggingBll = new PropertyTaggingBll();
        PropertyEntity propertyEntity = new PropertyEntity();

        propertyEntity.TagTypeID = tagtype;
        propertyEntity.DeviceID = deviceid;
        propertyEntity.CreatedBy = Common.UserName;
        propertyEntity.ConnectionString = Common.ConnString;

        string html = string.Empty;
        List<PropertyEntity> JsonItemList = new List<PropertyEntity>();
        try
        {
            DataTable dt = assettaggingBll.GetPrintList(propertyEntity);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    PropertyEntity x = new PropertyEntity();
                    if (dt.Rows[i]["Assettype"].ToString() == "p")
                    {
                        x.AssetType = "Property";
                    }
                    else
                    {
                        x.AssetType = "Stock";
                    }
                    x.ItemCode = dt.Rows[i]["ItemCode"].ToString();
                    x.Description = dt.Rows[i]["Description"].ToString() + ". " + dt.Rows[i]["LongDescription"].ToString();
                    x.ClassificationName = dt.Rows[i]["ClassificationName"].ToString();
                    x.CategoryName = dt.Rows[i]["CategoryName"].ToString();
                    x.BrandName = dt.Rows[i]["BrandName"].ToString();
                    x.ModelName = dt.Rows[i]["ModelName"].ToString();
                    x.PropertyNumber = dt.Rows[i]["PropertyNumber"].ToString();
                    x.SerialNo = dt.Rows[i]["SerialNumber"].ToString();
                    if (DeleteAccess == "0")
                    {
                        x.Action = "";
                    }
                    else
                    {
                        x.Action = " <center><button class='btn btn-xs btn-danger fa fa-close btn_Remove' type='button' onclick=\"RemoveToPrint('" + dt.Rows[i]["RecID"] + "','" + dt.Rows[i]["Assettype"] + "')\"></button></center>";
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
    public static string GetPrintJobs()
    {
        PropertyTaggingBll assettaggingBll = new PropertyTaggingBll();
        PropertyEntity propertyEntity = new PropertyEntity();

        propertyEntity.ConnectionString = Common.ConnString;

        string html = string.Empty;
        List<PropertyEntity> JsonItemList = new List<PropertyEntity>();
        try
        {
            DataTable dt = assettaggingBll.GetPrintJobs(propertyEntity);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    PropertyEntity x = new PropertyEntity();
                    x.ItemCode = dt.Rows[i]["ItemCode"].ToString();
                    x.Description = dt.Rows[i]["Description"].ToString() + ". " + dt.Rows[i]["LongDescription"].ToString();
                    x.LabelToPrint = dt.Rows[i]["PrinterName"].ToString();
                    x.Status = dt.Rows[i]["PrintStatus"].ToString();

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

    //per GR
    [WebMethod]
    public static string AvailableForTaggingPerGR(int tagtype)
    {
        PropertyTaggingBll assettaggingBll = new PropertyTaggingBll();

        string html = string.Empty;
        List<PropertyEntity> JsonItemList = new List<PropertyEntity>();
        try
        {
            DataTable dt = assettaggingBll.AvailableForTaggingPerGR(Common.ConnString, tagtype);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    PropertyEntity x = new PropertyEntity();
                    x.GoodsReceiveNumber = dt.Rows[i]["GRNumber"].ToString();
                    x.PONumber = dt.Rows[i]["PONumber"].ToString();
                    x.PRNumber = dt.Rows[i]["PRNumber"].ToString();
                    x.InvoiceNumber = dt.Rows[i]["InvoiceNumber"].ToString();
                    x.DRNumber = dt.Rows[i]["DRNumber"].ToString();
                    x.Status = dt.Rows[i]["STATUS"].ToString();

                    if (AddAccess == "0")
                    {
                        x.Action = "";
                    }
                    else
                    {
                        if (dt.Rows[i]["TagType"].ToString() == "4")
                        {
                            x.Action = " <center><button class='btn btn-xs btn-success fa fa-tag' data-toggle='tooltip'  title='Tag Property' type='button' onclick=\"TagProperty('" + dt.Rows[i]["GRNumber"] + "')\"></button></center>";
                        }
                        else
                        {
                            x.Action = " <center> <button class='btn btn-xs btn-primary fa fa-eye' data-toggle='tooltip'  title='Add To Tagging List' type='button' onclick=\"ViewPropertyPerGR('" + dt.Rows[i]["GRNumber"] + "')\"></button> &nbsp;"
                                     + "<button class='btn btn-xs btn-success fa fa-plus btn_Add' data-toggle='tooltip'  title='Add To Tagging List' type='button' onclick=\"addtoPrintPerGR('" + dt.Rows[i]["GRNumber"] + "')\"></button></center>";
                        }
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
    public static string AvailableForRetaggingPerGR(int tagtype)
    {
        PropertyTaggingBll assettaggingBll = new PropertyTaggingBll();

        string html = string.Empty;
        List<PropertyEntity> JsonItemList = new List<PropertyEntity>();
        try
        {
            DataTable dt = assettaggingBll.AvailableForRetaggingPerGR(Common.ConnString, tagtype);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    PropertyEntity x = new PropertyEntity();
                    x.GoodsReceiveNumber = dt.Rows[i]["GRNumber"].ToString();
                    x.PONumber = dt.Rows[i]["PONumber"].ToString();
                    x.PRNumber = dt.Rows[i]["PRNumber"].ToString();
                    x.InvoiceNumber = dt.Rows[i]["InvoiceNumber"].ToString();
                    x.DRNumber = dt.Rows[i]["DRNumber"].ToString();
                    x.Status = dt.Rows[i]["STATUS"].ToString();

                    if (AddAccess == "0")
                    {
                        x.Action = "";
                    }
                    else
                    {
                        if (dt.Rows[i]["TagType"].ToString() == "4")
                        {
                            x.Action = " <center><button class='btn btn-xs btn-success fa fa-tag btn_Tag' data-toggle='tooltip'  title='Tag Property' type='button' onclick=\"TagProperty('" + dt.Rows[i]["GRNumber"] + "')\"></button></center>";
                        }
                        else
                        {
                            x.Action = " <center> <button class='btn btn-xs btn-primary fa fa-eye' data-toggle='tooltip'  title='Add To Tagging List' type='button' onclick=\"ViewPropertyPerGR('" + dt.Rows[i]["GRNumber"] + "')\"></button> &nbsp;"
                                     + "<button class='btn btn-xs btn-success fa fa-plus btn_Add' data-toggle='tooltip'  title='Add To Tagging List' type='button' onclick=\"addtoPrintPerGR('" + dt.Rows[i]["GRNumber"] + "')\"></button></center>";
                        }
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

    //

    [WebMethod]
    public static string ViewPropertyTaggingPerGR(string GRNumber, int tagtype)
    {
        PropertyTaggingBll assettaggingBll = new PropertyTaggingBll();

        string html = string.Empty;
        List<PropertyEntity> JsonItemList = new List<PropertyEntity>();
        try
        {
            DataTable dt = assettaggingBll.ViewPropertyTaggingPerGR(Common.ConnString, tagtype, GRNumber);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    PropertyEntity x = new PropertyEntity();
                    x.PropertyNumber = dt.Rows[i]["PropertyNumber"].ToString();
                    x.ItemCode = dt.Rows[i]["ItemCode"].ToString();
                    x.Description = dt.Rows[i]["Description"].ToString() + ". " + dt.Rows[i]["LongDescription"].ToString();
                    x.SerialNo = dt.Rows[i]["SerialNumber"].ToString();
                    x.ClassificationName = dt.Rows[i]["ClassificationName"].ToString();
                    x.CategoryName = dt.Rows[i]["CategoryName"].ToString();
                    x.BrandName = dt.Rows[i]["BrandName"].ToString();
                    x.ModelName = dt.Rows[i]["ModelName"].ToString();


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
    public static string ViewPropertyForRetaggingPerGR(string GRNumber, int tagtype)
    {
        PropertyTaggingBll assettaggingBll = new PropertyTaggingBll();

        string html = string.Empty;
        List<PropertyEntity> JsonItemList = new List<PropertyEntity>();
        try
        {
            DataTable dt = assettaggingBll.ViewPropertyForRetaggingPerGR(Common.ConnString, tagtype, GRNumber);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    PropertyEntity x = new PropertyEntity();
                    x.PropertyNumber = dt.Rows[i]["PropertyNumber"].ToString();
                    x.ItemCode = dt.Rows[i]["ItemCode"].ToString();
                    x.Description = dt.Rows[i]["Description"].ToString() + ". " + dt.Rows[i]["LongDescription"].ToString();
                    x.SerialNo = dt.Rows[i]["SerialNumber"].ToString();
                    x.ClassificationName = dt.Rows[i]["ClassificationName"].ToString();
                    x.CategoryName = dt.Rows[i]["CategoryName"].ToString();
                    x.BrandName = dt.Rows[i]["BrandName"].ToString();
                    x.ModelName = dt.Rows[i]["ModelName"].ToString();


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


    #endregion

    #region Methods
    [WebMethod]
    public static string AddToPrint(int tagtype, int assetid, int deviceid, string assettype, int quantity, int TaggingType)
    {
        PropertyTaggingBll y = new PropertyTaggingBll();
        PropertyEntity x = new PropertyEntity();

        x.TagTypeID = tagtype;
        x.RecID = assetid;
        x.CreatedBy = Common.UserName;
        x.DeviceID = deviceid;
        x.AssetType = assettype;
        x.Quantity = quantity;
        x.ConnectionString = Common.ConnString;
        x.TaggingType = TaggingType;

        return y.AddToPrint(x).ToString();
    }

    [WebMethod]
    public static string RemoveToPrint(int assetid, string assettype, int TaggingType)
    {
        PropertyTaggingBll y = new PropertyTaggingBll();
        PropertyEntity x = new PropertyEntity();

        x.RecID = assetid;
        x.CreatedBy = Common.UserName;
        x.ConnectionString = Common.ConnString;
        x.AssetType = assettype;
        x.TaggingType = TaggingType;

        return y.RemoveToPrint(x).ToString();
    }

    [WebMethod]
    public static string RemoveAllToPrint(int tagtype, int deviceid, int TaggingType)
    {
        PropertyTaggingBll y = new PropertyTaggingBll();
        PropertyEntity x = new PropertyEntity();

        x.TagTypeID = tagtype;
        x.CreatedBy = Common.UserName;
        x.DeviceID = deviceid;
        x.ConnectionString = Common.ConnString;
        x.TaggingType = TaggingType;

        return y.RemoveAllToPrint(x).ToString();
    }

    [WebMethod]
    public static string ConfirmPrint(int tagtype, int deviceid, int approverID, string Remarks, int isRetag, int TaggingType)
    {
        PropertyTaggingBll y = new PropertyTaggingBll();
        PropertyEntity x = new PropertyEntity();

        x.TagTypeID = tagtype;
        x.DeviceID = deviceid;
        x.Remarks = Remarks;
        x.ApproverID = approverID;
        x.TaskID = isRetag;
        x.CreatedBy = Common.UserName;
        x.ConnectionString = Common.ConnString;
        x.TaggingType = TaggingType;

        y.ConfirmPrint(x);
        return "";
    }

    [WebMethod]
    public static string TagProperty(int recid, int deviceid)
    {
        PropertyTaggingBll y = new PropertyTaggingBll();
        PropertyEntity x = new PropertyEntity();

        x.RecID = recid;
        x.DeviceID = deviceid;
        x.CreatedBy = Common.UserName;
        x.ConnectionString = Common.ConnString;

        y.TagProperty(x);

        return "";
    }

    [WebMethod]
    public static string WriteTags(int tagtype)
    {
        PropertyTaggingBll y = new PropertyTaggingBll();
        PropertyEntity x = new PropertyEntity();

        x.TagTypeID = tagtype;
        x.CreatedBy = Common.UserName;
        x.ConnectionString = Common.ConnString;

        y.WriteTags(x);

        return "";
    }

    [WebMethod]
    public static string GetCount(int tagtype, int deviceid, int TaggingType)
    {
        PropertyTaggingBll y = new PropertyTaggingBll();
        PropertyEntity x = new PropertyEntity();

        x.TagTypeID = tagtype;
        x.CreatedBy = Common.UserName;
        x.DeviceID = deviceid;
        x.ConnectionString = Common.ConnString;
        x.TaggingType = TaggingType;

        return y.GetCount(x);
    }

    [WebMethod]
    public static string checkRetaggingApproval()
    {

        return Common.RetaggingApproval;
    }

    [WebMethod]
    public static string getRecIDtoAddPropertyToPrint(string GRNumber, int tagtype)
    {
        PropertyTaggingBll y = new PropertyTaggingBll();
        PropertyEntity x = new PropertyEntity();
        PropertyTaggingBll assettaggingBll = new PropertyTaggingBll();

        string ret = "";
        try
        {
            DataTable dt = assettaggingBll.ViewPropertyTaggingPerGR(Common.ConnString, tagtype, GRNumber);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    ret += dt.Rows[i]["RecID"].ToString() + "|";

                }

                return ret;
            }
            else { return ""; }
        }
        catch (Exception ex)
        {
            return "";
        }


    }

    #endregion

    //Stock
    [WebMethod]
    public static string StockAvailableForTagging(int tagtype)
    {
        PropertyTaggingBll assettaggingBll = new PropertyTaggingBll();

        string html = string.Empty;
        List<StockEntity> JsonItemList = new List<StockEntity>();
        try
        {
            DataTable dt = assettaggingBll.StockAvailableForTagging(tagtype, Common.ConnString);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    StockEntity x = new StockEntity();
                    x.ItemCode = dt.Rows[i]["ItemCode"].ToString();
                    x.Description = dt.Rows[i]["Description"].ToString();
                    x.ClassificationName = dt.Rows[i]["ClassificationName"].ToString();
                    x.CategoryName = dt.Rows[i]["CategoryName"].ToString();
                    x.BrandName = dt.Rows[i]["BrandName"].ToString();
                    x.ModelName = dt.Rows[i]["ModelName"].ToString();
                    if (AddAccess == "0")
                    {
                        x.Action = "";
                    }
                    else
                    {
                        x.Action = " <center><button class='btn btn-xs btn-success fa fa-plus btn_AddStock' data-toggle='tooltip'  title='Add To Tagging List' type='button' onclick=\"AddToPrintStock('" + dt.Rows[i]["StockID"] + "','" + dt.Rows[i]["ItemCode"] + "')\"></button></center>";
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
    public static string StockAvailableForRetagging(int tagtype)
    {
        PropertyTaggingBll assettaggingBll = new PropertyTaggingBll();

        string html = string.Empty;
        List<StockEntity> JsonItemList = new List<StockEntity>();
        try
        {
            DataTable dt = assettaggingBll.StockAvailableForRetagging(tagtype, Common.ConnString);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    StockEntity x = new StockEntity();
                    x.ItemCode = dt.Rows[i]["ItemCode"].ToString();
                    x.Description = dt.Rows[i]["Description"].ToString();
                    x.ClassificationName = dt.Rows[i]["ClassificationName"].ToString();
                    x.CategoryName = dt.Rows[i]["CategoryName"].ToString();
                    x.BrandName = dt.Rows[i]["BrandName"].ToString();
                    x.ModelName = dt.Rows[i]["ModelName"].ToString();

                    x.Action = " <center><button class='btn btn-xs btn-success fa fa-plus btn_AddStock' data-toggle='tooltip'  title='Add To Tagging List' type='button' onclick=\"AddToPrintStock('" + dt.Rows[i]["StockID"] + "','" + dt.Rows[i]["ItemCode"] + "')\"></button></center>";
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

    #region Methods
    [WebMethod]
    public static string CheckRetaggingApproval()
    {
        return Common.RetaggingApproval;
    }
    [WebMethod]

    public static string AddToPrintStock(int recid)
    {
        PropertyTaggingBll y = new PropertyTaggingBll();
        PropertyEntity x = new PropertyEntity();

        x.RecID = recid;
        x.CreatedBy = Common.UserName;
        x.ConnectionString = Common.ConnString;

        return y.AddToPrintStock(x).ToString();
    }

    [WebMethod]
    public static string RemoveToPrintStock(int recid)
    {
        PropertyTaggingBll y = new PropertyTaggingBll();
        PropertyEntity x = new PropertyEntity();

        x.RecID = recid;
        x.CreatedBy = Common.UserName;
        x.ConnectionString = Common.ConnString;

        return y.RemoveToPrintStock(x).ToString();
    }

    [WebMethod]
    public static string RemoveAllToPrintStock(int tagtype)
    {
        PropertyTaggingBll y = new PropertyTaggingBll();
        PropertyEntity x = new PropertyEntity();

        x.CreatedBy = Common.UserName;
        x.ConnectionString = Common.ConnString;

        return y.RemoveAllToPrintStock(x).ToString();
    }

    [WebMethod]
    public static string ConfirmPrintStock(int tagtype)
    {
        PropertyTaggingBll y = new PropertyTaggingBll();
        PropertyEntity x = new PropertyEntity();

        x.CreatedBy = Common.UserName;
        x.ConnectionString = Common.ConnString;

        y.ConfirmPrintStock(x);

        return "";
    }

    [WebMethod]
    public static string WriteTags()
    {
        PropertyTaggingBll y = new PropertyTaggingBll();
        PropertyEntity x = new PropertyEntity();

        x.CreatedBy = Common.UserName;
        x.ConnectionString = Common.ConnString;

        y.WriteTagsStock(x);

        return "";
    }

    [WebMethod]
    public static string GetCountStock(int TaggingType)
    {
        PropertyTaggingBll y = new PropertyTaggingBll();
        PropertyEntity x = new PropertyEntity();

        x.CreatedBy = Common.UserName;
        x.ConnectionString = Common.ConnString;
        x.TaggingType = TaggingType;

        return y.GetCountStock(x);
    }

    #endregion


    //forEmail
    private void EMailThis()
    {
        DataTable dt = new DataTable();

        string smtphost = Common.SMTPhost;
        string smtpport = Common.SMTPport;
        string smtpmail = Common.SMTPmail;
        string smtppass = Common.SMTPpass;
        MailMessage mail = new MailMessage();
        mail.To.Add("/dddapproveremail");

        mail.From = new MailAddress(smtpmail);
        mail.Subject = Common.EmailSubject + " (Property Disposal - Approval)";
        string Body = "<font face='Consolas'><br><br><i>***This is a system generated mail. Please do not reply.***</i><br><br><br> Hi " + "/dddapprovername" + ",<br><br><br>";
        Body += "A PROPERTY DISPOSAL transaction is AWAITING FOR YOUR APPROVAL: <br><br><a href='" + "/dddhostaddress" + " / Home/RequestManagement.aspx'>Please click here to redirect to management approval page.</a><br><br>"
            + "Disposal No : " + "/ddddisposalno" + " < br>"
            + "Purpose/Remarks : " + "/dddremarks" + " < br>"
            + "Processed By : " + "/dddtransactorname" + " < br>"
            + "Processed Date : " + "/ddddisposaldate" + " < br><br>";
        Body += "<table border='1'><tr>"
            + "<th>Property No</th>"
            + "<th>Description</th>"
            + "<th>Serial No</th></tr>";
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Body += "<tr>"
                    + "<td>" + dt.Rows[i]["PropertyNumber"].ToString() + "</td>"
                    + "<td>" + dt.Rows[i]["Description"].ToString() + "</td>"
                    + "<td>" + dt.Rows[i]["SerialNumber"].ToString() + "</td>"
                    + "</tr>";
            }
            Body += "</table>";
        }
        Body += "<br><br><br><i>***This is a system generated mail. Please do not reply.***</i></font>";

        mail.Body = Body;
        SmtpClient smtp = new SmtpClient();
        smtp.Host = smtphost; //Or Your SMTP Server Address
        smtp.Port = Convert.ToInt32(smtpport);
        smtp.UseDefaultCredentials = false;
        smtp.Credentials = new System.Net.NetworkCredential
        (smtpmail, smtppass);

        //Or your Smtp Email ID and Password
        mail.IsBodyHtml = true;
        smtp.EnableSsl = true;

        ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
        smtp.Send(mail);

        string dddreturn = "okay";
    }

    public static string GenerateBase64QRcode(string encodedstring)
    {
        QRCodeGenerator qrGenerator = new QRCodeGenerator();
        QRCodeData qrCodeData = qrGenerator.CreateQrCode(encodedstring, QRCodeGenerator.ECCLevel.H);
        Base64QRCode qrCode = new Base64QRCode(qrCodeData);

        return qrCode.GetGraphic(10);
    }
    // FontEncoder Encode 
    public static string GenerateBarCode(string encode)
    {
        return Converter.Base64FromByteArray(Converter.ExtractResource(BarcodeLib.Barcode.DoEncode(BarcodeLib.TYPE.CODE128, encode)));
    }


    [WebMethod]
    public static string PrintPaper(int tagtype, int deviceid)
    {
        PropertyTaggingBll assettaggingBll = new PropertyTaggingBll();
        PropertyEntity propertyEntity = new PropertyEntity();

        propertyEntity.TagTypeID = tagtype;
        propertyEntity.DeviceID = deviceid;
        propertyEntity.CreatedBy = Common.UserName;
        propertyEntity.ConnectionString = Common.ConnString;

        string html = string.Empty;
        try
        {
            DataTable dt = assettaggingBll.GetPrintList(propertyEntity);
            #region styles
            //style
            html += "<link href='https://fonts.googleapis.com/css?family=Libre+Barcode+128+Text' rel='stylesheet'>"
                        + "<style>"
            + ".page-body {page-break-after: auto;}"
            + "@page {size: A4; margin: 10mm;}"
            + ".bcode { font-family: 'Libre Barcode 128 Text', cursive;font-size: 80px;}"
            + "@media print {"
                + "table {padding: 0px 10px 0px 0px;}"
                + "thead {display: table-header-group; }"
                + "tfoot {display: table-footer-group;}"
                + "body { margin: 0;}"
            + "}"
            + "</style >";
            #endregion

            if (dt.Rows.Count > 0)
            {   //header div
                html += "<div class='page-header'>";
                html += "<table style='width:100%;font-family: Arial;'>"
                    + "<tr><td align='center' style='font-size:15;'><img src='../Image/" + Common.CompanyInfo + "/FormLogo.png';  width='50%'; style='width: 150px; height: 150px' /></td</tr>"
                + "<tr><td align='center' style='font-size:15;'><h3>PROPERTY Tagging</h3></br></br></td</tr></table>";
                html += "</div>";

                html += "<div class='table-responsive'><table id='tbl_PropertyInventoryList22' class='table table-bordered' width='100%'>"
                        + "<tr>"
                        + "<th><center><b>TYPE</b><center></th>"
                        + "<th><center><b>ITEM CODE</b><center></th>"
                        + "<th><center><b>ITEM NUMBER</b><center></th>"
                        + "<th><center><b>DESCRIPTION<b></center></th>"
                        + "<th><center><b>BARCODE</b></center></th>"
                        + "<th><center><b>QR CODE</b></center></th>"
                        + "</tr>";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    html += "<tr><td style='width:10%;'><center>" + dt.Rows[i]["TagType"].ToString() + "</center></td>" +
                        "<td style='width:5%;'><center>" + dt.Rows[i]["ItemCode"].ToString() + "</center></td>" +
                        "<td style='width:15%;'><center>" + dt.Rows[i]["PropertyNumber"].ToString() + "</center></td>" +
                        "<td style='width:15%;'><center>" + dt.Rows[i]["Description"].ToString() + "</center></td>" +
                        "<td style='width:35%;'><center><img width='160' height='100' type='image/x-icon' src='data:image/x-png;base64," + GenerateBarCode(dt.Rows[i]["Barcode"].ToString()) + "'/> </td>" +
                        "<td style='width:20%;'><center><img width='120' height='140' type='image/x-icon' src='data:image/x-png;base64," + GenerateBase64QRcode(dt.Rows[i]["Barcode"].ToString()) + "'/> </td>" +
                        "</tr>";
                }
                html += "</table></div>";
            }
            else
            {
                html += "<center><h5 style='color: red;'>No Records Found </h4> </center>";
            }
            return html;
        }
        catch
        {
            return "";
        }
    }
}