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

public partial class Files_Data_Property : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        CheckAccess();
        //GetClassificationList();
        //GetCategoryList();
        //GetBrandList();
       // GetModelList();
        //GetUnitOfMeasurementList();
        //GetAssetInventoryAccount();
        GetIncomeAccount();
        GetExpenseAccount();
        DepreciationMethod();
        //GetTag();
    }

    #region METADATA
    public static string ViewAccess = "";
    public static string AddAccess = "";
    public static string EditAccess = "";
    public static string DeleteAccess = "";
    public void CheckAccess()
    {
        RoleBll rb = new RoleBll();

        string AccessButon = rb.CheckR("Files_Data/Property.aspx", Convert.ToInt32(Common.Role), Common.ConnString);
        string[] ButtonAccess = AccessButon.Split('|');
        ViewAccess = ButtonAccess[0];
        AddAccess = ButtonAccess[1];
        EditAccess = ButtonAccess[2];
        DeleteAccess = ButtonAccess[3];
    }

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

    private void DepreciationMethod()
    {
        ddlDepreciationMethod.Items.Clear();
        ddlDepreciationMethod.Items.Insert(0, "--- Select Depreciation Method ---");
        ddlDepreciationMethod.Items.Insert(1, "NONE");
        ddlDepreciationMethod.Items.Insert(2, "STRAIGHT LINE DEPRECIATION METHOD");
        ddlDepreciationMethod.Items.Insert(3, "DIMINISHING BALANCE");
        //ddlDepreciationMethod.Items.Insert(4, "UNIT OF PRODUCT METHOD");

    }

    [WebMethod]
    public static string RetrieveThreshold()
    {
        MiscBll miscBll = new MiscBll();

        return miscBll.RetrieveThreshold(Common.ConnString);
    }

    [WebMethod]
    public static string RetrieveUsefulLife()
    {
        MiscBll miscBll = new MiscBll();

        return miscBll.GetDepreciation(Common.ConnString);
    }

    #endregion


    #region METHODS
    [WebMethod]
    public static int SaveProperty(int id, string itemcode, int classificationid, int categoryid, int brandid, int modelid, int uomid, string description, int incomeaccount, int expenseaccount, string propertyimage, int maintenancethreshold, bool active, int tagid, int usefullife, string depreciationmethod)
    {
        PropertyEntity x = new PropertyEntity();
        PropertyBll y = new PropertyBll();

        x.PropertyID = id;
        x.ItemCode = itemcode;
        x.ClassificationID = classificationid;
        x.CategoryID = categoryid;
        x.BrandID = brandid;
        x.ModelID = modelid;
        x.UnitOfMeasurementID = uomid;
        x.Description = description;
        //x.AssetInventoryAccount = assetinventoryaccount;
        x.IncomeAccount = incomeaccount;
        x.ExpenseAccount = expenseaccount;
        x.PropertyImage = propertyimage;
        x.MaintenanceThreshold = maintenancethreshold;
        x.Active = active;
        x.TagID = tagid;
        x.UsefulLife = usefullife;
        x.DepreciationMethod = depreciationmethod;
        x.CreatedBy = Common.UserName;
        x.ConnectionString = Common.ConnString;

        return y.SaveProperty(x);
    }

    protected void PropertyImageUpload(object sender, EventArgs e)
    {
        string itemcode = txtItemCode.Text;
        Boolean fileOK = false;
        String path = Server.MapPath("~/Image/PropertyImage/" + itemcode + "/");
        if (FileUploadPropertyImage.HasFile)
        {
            String fileExtension = System.IO.Path.GetExtension(FileUploadPropertyImage.FileName).ToLower();

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
                    FileUploadPropertyImage.SaveAs(path + itemcode + System.IO.Path.GetExtension(FileUploadPropertyImage.FileName).ToLower());
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:uploadSuccess();", true);
                }
                catch (Exception ex)
                {
                    //Label1.Text = "File could not be uploaded.";
                }
            }
            else
            {
                //Response.Write("<script>swal('Warning','Please Select .PNG, .JPEG or .JPG to upload','warning');</script>");
            }
        }
        else
        {

        }
    }

    [WebMethod]
    public static string GetProperty()
    {
        PropertyBll propertyBll = new PropertyBll();
        string html = string.Empty;
        List<PropertyEntity> JsonItemList = new List<PropertyEntity>();
        try
        {
            DataTable dt = propertyBll.GetProperty(Common.ConnString);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    PropertyEntity x = new PropertyEntity();
                    x.ItemCode = dt.Rows[i]["ItemCode"].ToString();
                    x.Description = dt.Rows[i]["Description"].ToString();
                    x.Classification = dt.Rows[i]["ClassificationName"].ToString();
                    x.Category = dt.Rows[i]["CategoryName"].ToString();
                    x.Brand = dt.Rows[i]["BrandName"].ToString();
                    x.Model = dt.Rows[i]["ModelName"].ToString();
                    x.TagType = dt.Rows[i]["TagName"].ToString();
                    //if (dt.Rows[i]["PropertyImage"].ToString() == "")
                    //{
                    //    x.Picture = "<img src = '../Pictures/noimage.png' class='img-circle'  style='width:95%; margin-left: auto;margin-right: auto;'/>";
                    //}
                    //else
                    //{
                    //    x.Picture = "<img src = '../Image/PropertyImage/" + dt.Rows[i]["ItemCode"].ToString() + "//" + dt.Rows[i]["PropertyImage"].ToString() + "' class='img-circle' style='width:95%;margin-left: auto;margin-right: auto;'/>";
                    //}

                    x.IsActive = "<img " + ((bool.Parse(dt.Rows[i]["Active"].ToString()) == true ? "src = '../Image/Icon/yes.png'" : "src = '../Image/Icon/no.png'")) + "/>";
                    if (DeleteAccess == "0")
                    {
                        x.Action = " <center><button class='btn btn-xs btn-warning btn_Edit' type='button' data-toggle='tooltip'  title='Edit Property Information' onclick=\"GetPropertyID('" + dt.Rows[i]["PropertyID"] + "')\"><span class='fa fa-edit'></span></button>  </center>";

                    }
                    else
                    {
                        x.Action = " <center><button class='btn btn-xs btn-warning btn_Edit' type='button' data-toggle='tooltip'  title='Edit Property Information' onclick=\"GetPropertyID('" + dt.Rows[i]["PropertyID"] + "')\"><span class='fa fa-edit'></span></button> "
                          + " <button type='button' class='btn btn-xs btn-danger btn_Delete btn_DeleteProperty' data-toggle='tooltip' title='Delete Property' onclick=\"deleteProperty('" + dt.Rows[i]["PropertyID"] + "')\"><span class='fa fa-trash'></span></button> </center>";

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
    public static string PropertyByID(int id)
    {
        PropertyEntity x = new PropertyEntity();
        PropertyBll propertyBll = new PropertyBll();

        x.PropertyID = id;
        x.ConnectionString = Common.ConnString;

        string result = propertyBll.PropertyByID(x);

        return result;
    }

    [WebMethod]
    public static string DeleteProperty(int id)
    {
        PropertyEntity x = new PropertyEntity();
        PropertyBll propertyBll = new PropertyBll();

        x.PropertyID = id;
        x.CreatedBy = Common.UserName;
        x.ConnectionString = Common.ConnString;

        return propertyBll.DeleteProperty(x).ToString();

    }
    #endregion
}