using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AdvanceEntity;
using AdvanceBll;
using System.Data;
using System.Web.Services;
using System.Web.Script.Serialization;
using System.IO;

public partial class Files_Data_Stock : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        CheckAccess();
        //GetAssetInventoryAccount();
        GetIncomeAccount();
        GetExpenseAccount();
        //GetClassificationList();
        //GetCategoryList();
        //GetBrandList();
        //GetModelList();
        //GetUnitOfMeasurementList();
        //GetTagType();
    }

    #region METADATA
    public static string ViewAccess = "";
    public static string AddAccess = "";
    public static string EditAccess = "";
    public static string DeleteAccess = "";
    public void CheckAccess()
    {
        RoleBll rb = new RoleBll();

        string AccessButon = rb.CheckR("Files_Data/Area.aspx", Convert.ToInt32(Common.Role), Common.ConnString);
        string[] ButtonAccess = AccessButon.Split('|');
        ViewAccess = ButtonAccess[0];
        AddAccess = ButtonAccess[1];
        EditAccess = ButtonAccess[2];
        DeleteAccess = ButtonAccess[3];
    }
    //private void GetTagType()
    //{
    //    TagTypeBll tagTypeBll = new TagTypeBll();

    //    DataTable dt = tagTypeBll.GetTag(Common.ConnString);

    //    ddlTagType.DataSource = dt;
    //    ddlTagType.DataValueField = "TagID";
    //    ddlTagType.DataTextField = "TagName";
    //    ddlTagType.DataBind();
    //    ddlTagType.Items.Insert(0, "--- Select Tag ---");
    //}
    //private void GetClassificationList()
    //{
    //    ClassificationBll classificationBll = new ClassificationBll();

    //    DataTable dt = classificationBll.GetClassificationList(Common.ConnString);

    //    ddlClassification.DataSource = dt;
    //    ddlClassification.DataValueField = "ClassificationID";
    //    ddlClassification.DataTextField = "ClassificationName";
    //    ddlClassification.DataBind();
    //    ddlClassification.Items.Insert(0, "--- Select Classification ---");
    //}

    //private void GetCategoryList()
    //{
    //    CategoryBll categoryBll = new CategoryBll();

    //    DataTable dt = categoryBll.GetCategoryList(Common.ConnString);

    //    ddlCategory.DataSource = dt;
    //    ddlCategory.DataValueField = "CategoryID";
    //    ddlCategory.DataTextField = "CategoryName";
    //    ddlCategory.DataBind();
    //    ddlCategory.Items.Insert(0, "--- Select Category ---");
    //}

    //private void GetBrandList()
    //{
    //    BrandBll brandBll = new BrandBll();

    //    DataTable dt = brandBll.GetBrandList(Common.ConnString);

    //    ddlBrand.DataSource = dt;
    //    ddlBrand.DataValueField = "BrandID";
    //    ddlBrand.DataTextField = "BrandName";
    //    ddlBrand.DataBind();
    //    ddlBrand.Items.Insert(0, "--- Select Brand ---");
    //}

    //private void GetModelList()
    //{
    //    ModelBll modelBll = new ModelBll();

    //    DataTable dt = modelBll.GetModelList(Common.ConnString);

    //    ddlModel.DataSource = dt;
    //    ddlModel.DataValueField = "ModelID";
    //    ddlModel.DataTextField = "ModelName";
    //    ddlModel.DataBind();
    //    ddlModel.Items.Insert(0, "--- Select Model ---");
    //}

    //private void GetUnitOfMeasurementList()
    //{
    //    UOMBll uomBll = new UOMBll();

    //    DataTable dt = uomBll.GetUOMlist(Common.ConnString);

    //    ddlUnitOfMeasurement.DataSource = dt;
    //    ddlUnitOfMeasurement.DataValueField = "UnitOfMeasurementID";
    //    ddlUnitOfMeasurement.DataTextField = "UnitOfMeasurement";
    //    ddlUnitOfMeasurement.DataBind();
    //    ddlUnitOfMeasurement.Items.Insert(0, "--- Select Unit Of Measurement ---");
    //}

    //private void GetAssetInventoryAccount()
    //{
    //    ChartOfAccountsBll coaBll = new ChartOfAccountsBll();

    //    DataTable dt = coaBll.GetAssetInventoryAccount(Common.ConnString);

    //    ddlAssetInventoryAccount.DataSource = dt;
    //    ddlAssetInventoryAccount.DataValueField = "ChartOfAccountsID";
    //    ddlAssetInventoryAccount.DataTextField = "AccountNumber";
    //    ddlAssetInventoryAccount.DataBind();
    //    ddlAssetInventoryAccount.Items.Insert(0,"--- Select Inventory Account ---");
    //}

    private void GetIncomeAccount()
    {
        ChartOfAccountsBll coaBll = new ChartOfAccountsBll();

        DataTable dt = coaBll.GetIncomeAccount(Common.ConnString);

        ddlIncomeAccount.DataSource = dt;
        ddlIncomeAccount.DataValueField = "ChartOfAccountsID";
        ddlIncomeAccount.DataTextField = "AccountNumber";
        ddlIncomeAccount.DataBind();
        ddlIncomeAccount.Items.Insert(0, "--- Select Income Account ---");
    }

    private void GetExpenseAccount()
    {
        ChartOfAccountsBll coaBll = new ChartOfAccountsBll();

        DataTable dt = coaBll.GetExpenseAccount(Common.ConnString);

        ddlExpenseAccount.DataSource = dt;
        ddlExpenseAccount.DataValueField = "ChartOfAccountsID";
        ddlExpenseAccount.DataTextField = "AccountNumber";
        ddlExpenseAccount.DataBind();
        ddlExpenseAccount.Items.Insert(0, "--- Select Expense Account ---");
    }

    [WebMethod]
    public static string GetRFIDandBarcodeGeneration()
    {
        MiscBll miscBll = new MiscBll();

        return miscBll.GetRFIDandBarcodeGeneration(Common.ConnString);
    }
    #endregion

    #region METHODS
    [WebMethod]
    //public static int SaveStock(int id, string itemcode, int classificationid, int categoryid, int brandid, int modelid, int uomid, string description, bool active, bool isreturnable, int assetinventoryaccount, int incomeaccount, int expenseaccount, string stockimage, int criticallevel, int tagtype, string rfidnumber, string barcodenumber)
    public static int SaveStock(int id, string itemcode, int classificationid, int categoryid, int brandid, int modelid, int uomid, string description, bool active, bool isreturnable, int incomeaccount, int expenseaccount, string stockimage, int criticallevel, int tagtype, string rfidnumber, string barcodenumber)
    {
        StockEntity x = new StockEntity();
        StockBll y = new StockBll();

        x.StockID = id;
        x.ItemCode = itemcode;
        x.ClassificationID = classificationid;
        x.CategoryID = categoryid;
        x.BrandID = brandid;
        x.ModelID = modelid;
        x.UnitOfMeasurementID = uomid;
        x.Description = description;
        x.Active = active;
        x.Returnable = isreturnable;
        x.CreatedBy = Common.UserName;
        //x.AssetInventoryAccount = assetinventoryaccount;
        x.IncomeAccount = incomeaccount;
        x.ExpenseAccount = expenseaccount;
        x.StockImage = stockimage;
        x.ConnectionString = Common.ConnString;
        x.CriticalLevel = criticallevel;
        x.TagTypeID = tagtype;
        x.RFIDNumber = rfidnumber;
        x.BarcodeNumber = barcodenumber;

        return y.SaveStock(x);
    }

    protected void StockImageUpload(object sender, EventArgs e)
    {
        try
        {
            string itemcode = txtItemCode.Text;
            Boolean fileOK = false;
            String path = Server.MapPath("~/Image/StockImage/" + itemcode + "/");
            if (FileUploadStockImage.HasFile)
            {
                String fileExtension = System.IO.Path.GetExtension(FileUploadStockImage.FileName).ToLower();

                String[] allowedExtensions = { ".png", ".jpeg", ".jpg" };
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
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                        FileUploadStockImage.SaveAs(path + itemcode + System.IO.Path.GetExtension(FileUploadStockImage.FileName).ToLower());
                    }
                    catch (Exception ex)
                    {
                        //Label1.Text = "File could not be uploaded.";
                    }
                }
                else
                {
                    Response.Write("<script>swal('Warning','Please Select .PNG, .JPEG or .JPG to upload','warning');</script>");
                }
            }
            else
            {

            }
        }
        catch
        {

        }
    }

    [WebMethod]
    public static string GetStock()
    {
        StockBll stockBll = new StockBll();
        string html = string.Empty;
        List<StockEntity> JsonItemList = new List<StockEntity>();
        try
        {
            DataTable dt = stockBll.GetStock(Common.ConnString);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    StockEntity x = new StockEntity();
                    x.ItemCode = dt.Rows[i]["ItemCode"].ToString();
                    x.Description = dt.Rows[i]["Description"].ToString();
                    x.Classification = dt.Rows[i]["ClassificationName"].ToString();
                    x.Category = dt.Rows[i]["CategoryName"].ToString();
                    x.Brand = dt.Rows[i]["BrandName"].ToString();
                    x.Model = dt.Rows[i]["ModelName"].ToString();
                    x.TagType = dt.Rows[i]["TagName"].ToString();
                    x.IsReturnable = "<img " + ((bool.Parse(dt.Rows[i]["IsReturnable"].ToString()) == true ? "src = '../Image/Icon/yes.png'" : "src = '../Image/Icon/no.png'")) + "/>";
                    x.IsActive = "<img " + ((bool.Parse(dt.Rows[i]["Active"].ToString()) == true ? "src = '../Image/Icon/yes.png'" : "src = '../Image/Icon/no.png'")) + "/>";
                    if (DeleteAccess == "0")
                    {
                        x.Action = " <center><button class='btn btn-xs btn-warning btn_Edit' type='button' data-toggle='tooltip'  title='Edit Stock Information' onclick=\"GetStockID('" + dt.Rows[i]["StockID"] + "')\"><span class='fa fa-edit'></span></button> </center>";
                    }
                    else
                    {
                        x.Action = " <center><button class='btn btn-xs btn-warning btn_Edit' type='button' data-toggle='tooltip'  title='Edit Stock Information' onclick=\"GetStockID('" + dt.Rows[i]["StockID"] + "')\"><span class='fa fa-edit'></span></button> "
                            + " <button type='button' class='btn btn-xs btn-danger btn_Delete btn_DeleteStock' data-toggle='tooltip' title='Delete Stock' onclick=\"deleteStock('" + dt.Rows[i]["StockID"] + "')\"><span class='fa fa-trash'></span></button> </center>";
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
    public static string StockByID(int id)
    {
        StockEntity x = new StockEntity();
        StockBll stockBll = new StockBll();

        x.StockID = id;
        x.ConnectionString = Common.ConnString;

        return stockBll.StockByID(x);
    }

    [WebMethod]
    public static string DeleteStock(int id)
    {
        StockEntity x = new StockEntity();
        StockBll stockBll = new StockBll();

        x.StockID = id;
        x.CreatedBy = Common.UserName;
        x.ConnectionString = Common.ConnString;

        return stockBll.DeleteStock(x).ToString();

    }

    [WebMethod]
    public static string GetTagByID(int tagid)
    {
        TagTypeBll y = new TagTypeBll();
        TagTypeEntity x = new TagTypeEntity();

        x.TagID = tagid;
        x.ConnectionString = Common.ConnString;

        return y.GetTagByID(x);
    }
    #endregion
}