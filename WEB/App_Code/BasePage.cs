using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using AdvanceEntity;
using AdvanceBll;
using System.Data;
using System.Management;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Configuration;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;

/// <summary>
/// Summary description for BasePage
/// </summary>
public class BasePage : System.Web.UI.Page
{
    public static string retEmail = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        SystemConfigurations shSystemConfigurationsEmail = (SystemConfigurations)Session["shSystemConfigurationsEmail"];
        retEmail = shSystemConfigurationsEmail.Value;
    }
    //ConnectionStringBll csbll = new ConnectionStringBll();
    protected override void OnInit(System.EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        if (Common.IsUserAuthenticated == false)
            Response.Redirect("~/Login.aspx");
        else
        {
            System.IO.FileInfo oFileInfo = new System.IO.FileInfo(System.Web.HttpContext.Current.Request.Url.AbsolutePath);

            if (!Common.ModuleData.Contains(oFileInfo.Name))
                System.Web.HttpContext.Current.Response.Redirect(Page.ResolveClientUrl("~/PageNotFound.aspx"));
            //Response.Redirect("~/404.aspx");

        }

        base.OnInit(e);
    }

   

    [WebMethod]
    public static string GetUserInfo()
    {
        string userinfo = Common.Administrative + "|" + Common.IsApprover + "|" + Common.IsCertifier + "|" + Common.IsInspector + "|" + Common.IsCustodian;
        return userinfo;
    }

    [WebMethod]
    public static string GetAttachments(string transactionnumber, string transactiontype)
    {
        string[] filesGR = new string[0];
        try
        {
            filesGR = Directory.GetFiles(System.Web.HttpContext.Current.Server.MapPath("../SupportingDocuments/" + transactiontype + "/" + transactionnumber));
        }
        catch
        {

        }
        string Attachments = "";
        foreach (string name in filesGR)
        {
            int j = name.Split('\\').Length;
            string[] file = name.Split('\\');
            Attachments += "<a target='_blank' href='../SupportingDocuments/" + transactiontype + "/" + transactionnumber + "/" + file[j - 1] + "'><label>" + file[j - 1] + "</label></a><button type='button' id='" + file[j - 1] + "' onclick=\"DeleteAttachment('" + file[j - 1] + "')\" class='btn btn-danger btn-xs' Text='Remove'>Remove</button><br>";
        }
        return Attachments;
    }

    [WebMethod]
    public static string DeleteAttachment(string transactiontype, string transactionnumber, string filename)
    {
        try
        {

            string strPhysicalFolder = System.Web.HttpContext.Current.Server.MapPath("../SupportingDocuments/" + transactiontype + "/" + transactionnumber + "/");

            string strFileFullPath = strPhysicalFolder + filename;

            if (System.IO.File.Exists(strFileFullPath))
            {
                System.IO.File.Delete(strFileFullPath);
                return "file deleted.";
            }
            else
            {
                return "file not found.";
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    [WebMethod]
    public static string GetUserCustodian()
    {
        CustodianBll cb = new CustodianBll();
        DataTable dt = cb.GetUserCustodian(Common.CustodianID, Common.Administrative, Common.ConnString);
        return Control.MyConverter.ConvertDataTabletoJSON(dt);
    }




    [WebMethod]
    public static string ChangePassword(string password, string newpassword, string confirmpassword)
    {
        string response = "";
        UserEntity userEntity = new UserEntity();
        UserBll userBLL = new UserBll();
        string pass = AES.Encrypt(newpassword, newpassword);

        if (PasswordPolicy.IsValid(newpassword) == true)
        {

            userEntity.Password = password;
            userEntity.NewPassword = pass;
            userEntity.ConfirmPassword = confirmpassword;
            userEntity.UserName = Common.UserName;

            try
            {
                string en = userBLL.GetPassword(Common.ConnString, userEntity);

                string dec = AES.Decrypt(en, password);
                if (dec == password)
                {
                    userBLL.ChangePassword(Common.ConnString, userEntity);

                    response = "success";
                }
                else
                {
                    response = "Incorrect Password";
                }

            }
            catch (Exception ex)
            {
                //lblError.Text = "<h5 style='color:red'><b>User Does not Exist!</b><h5>";
            }
        }
        else
        {
            response = "Password must contain:\n\n*Upper Case\n*Lower Case\n*Numeric characters\n*Non-alphanumeric character";
        }

        return response;
    }

    private static decimal DecimalParse = 0;
    private static object ImageFormat;

    public static string ConvertToMoneyFormat(string data)
    {
        try
        {
            string resultvalue = "";
            if (decimal.TryParse(data, out DecimalParse))
            {
                resultvalue = String.Format("{0:n}", (decimal.Parse(data)));
            }

            return resultvalue;
        }
        catch (Exception)
        { return ""; }
    }

    public static string ConvertImageToBase64()
    {
        string fileName = "Image\\" + Common.CompanyInfo + "\\FormLogo.png";
        string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);

        int newWidth = 90;
        int newHeight = 130;

        // Resize the image and convert it to base64
        string base64Image = ResizeAndConvertToBase64(filePath, newWidth, newHeight);



        //byte[] fileBytes = File.ReadAllBytes(filePath);
        //string base64ImageRepresentation = Convert.ToBase64String(fileBytes);

        return base64Image;
        //string base64ImageRepresentation = Convert.ToBase64String(fileBytes);
        //string bs64= ResizeBase64Image(base64ImageRepresentation,100,100);
        //return bs64;
    }

    public static string ResizeAndConvertToBase64(string imagePath, int newWidth, int newHeight)
    {
        // Load the original image
        using (Bitmap originalImage = new Bitmap(imagePath))
        {
            // Create a new bitmap with the specified width and height
            using (Bitmap resizedImage = new Bitmap(originalImage, newWidth, newHeight))
            {
                // Convert the resized image to base64
                string base64String = ConvertToBase64(resizedImage);

                return base64String;
            }
        }
    }

    private static string ConvertToBase64(Bitmap image)
    {
        using (MemoryStream memoryStream = new MemoryStream())
        {
            // Save the image to the memory stream in PNG format (you can change the format if needed)
            image.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);

            // Convert the image bytes to base64 string
            byte[] imageBytes = memoryStream.ToArray();
            string base64String = Convert.ToBase64String(imageBytes);

            return base64String;
        }
    }



    static string ResizeBase64Image(string base64String, int newWidth, int newHeight)
    {
        // Convert base64 string to byte array
        byte[] imageBytes = Convert.FromBase64String(base64String);

        using (MemoryStream ms = new MemoryStream(imageBytes))
        {
            // Create Image from byte array
            using (System.Drawing.Image originalImage = System.Drawing.Image.FromStream(ms))
            {
                // Create a new bitmap with the desired dimensions
                using (Bitmap resizedImage = new Bitmap(newWidth, newHeight))
                {
                    // Create a Graphics object to draw on the bitmap
                    using (Graphics g = Graphics.FromImage(resizedImage))
                    {
                        // Set the interpolation mode for better quality
                        g.InterpolationMode = InterpolationMode.HighQualityBicubic;

                        // Draw the original image onto the new bitmap with the new dimensions
                        g.DrawImage(originalImage, 0, 0, newWidth, newHeight);
                    }

                    // Save the resized image to a new MemoryStream
                    using (MemoryStream resizedMs = new MemoryStream())
                    {
                        resizedImage.Save(resizedMs, System.Drawing.Imaging.ImageFormat.Jpeg);

                        // Convert the resized image to base64 string
                        return Convert.ToBase64String(resizedMs.ToArray());
                    }
                }
            }
        }
    }



    public static string ConvertDataTabletoJSON(DataTable dt)
    {

        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        serializer.MaxJsonLength = Int32.MaxValue;
        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        Dictionary<string, object> row;
        foreach (DataRow dr in dt.Rows)
        {
            row = new Dictionary<string, object>();
            foreach (DataColumn col in dt.Columns)
            {
                row.Add(col.ColumnName, dr[col]);
            }
            rows.Add(row);
        }
        return serializer.Serialize(rows);
    }

    //added by nero for filter
    [WebMethod]
    public static string getfilesDataForFilter()
    {
        //AdvanceFilterBll x = new AdvanceFilterBll();

        //return x.GetFilesDataForFilter(Common.ConnString);
        return "";
    }

    [WebMethod]
    public static int AddAdvanceFilter(string ReportID, string Logic, string FilterBY, string Operator, string val, string trx)
    {
        AdvanceFilterBll x = new AdvanceFilterBll();

        return x.AddAdvanceFilter(ReportID, Logic, FilterBY, Operator, val, trx, Common.ConnString, Common.UserID);
    }

    //filter
    [WebMethod]
    public static string GetFilter(string ReportName)
    {
        PropertyMasterlistBll y = new PropertyMasterlistBll();
        MiscBll miscBll = new MiscBll();

        //string depreciationSetting = miscBll.GetDepreciation(Common.ConnString).Split('|')[0];

        string html = string.Empty;
        List<PropertyEntity> JsonItemList = new List<PropertyEntity>();
        try
        {
            DataTable dt = y.GetFilter(ReportName, Common.ConnString, Common.UserName);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    PropertyEntity x = new PropertyEntity();
                    x.Logic = dt.Rows[i]["Logic"].ToString();
                    x.FilterBy = dt.Rows[i]["FilterBy"].ToString();
                    x.Operator = dt.Rows[i]["Operator"].ToString();
                    x.Values = dt.Rows[i]["Values"].ToString();
                    x.IsActive = "<label hidden>" + dt.Rows[i]["isActive"].ToString() + "</label><img onclick=\"GetActive(" + dt.Rows[i]["RecID"] + ")\"" + ((bool.Parse(dt.Rows[i]["isActive"].ToString()) == true ? "src = '../Image/Icon/yes.png'" : "src = '../Image/Icon/no.png'")) + "/>";
                    x.Action = " <center><button class='btn btn-xs btn-warning btn_Edit' type='button' data-toggle='tooltip'  title='Edit Filter Information' onclick=\"GetFilterID('" + dt.Rows[i]["RecID"] + "','" + dt.Rows[i]["Logic"] + "','" + dt.Rows[i]["FilterBy"] + "','" + dt.Rows[i]["Operator"] + "','" + dt.Rows[i]["Values"] + "')\"><span class='fa fa-edit'></span></button> "
                        + " <button type='button' class='btn btn-xs btn-danger btn_Delete' data-toggle='tooltip' title='Delete Filter' onclick=\"DeleteFilter('" + dt.Rows[i]["RecID"] + "')\"><span class='fa fa-trash'></span></button> </center>";
                    //    " <a href='' class='btn btn-xs btn-danger btn_del'><span class='fa fa-times'></span></a> " +
                    //    " <a href='' class='btn btn-xs btn-warning btn_deliverAll'><span class='fa fa-truck'></span></a>";

                    JsonItemList.Add(x);

                }

                JavaScriptSerializer json = new JavaScriptSerializer();
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
    public static int GetActive(string id)
    {
        PropertyMasterlistBll y = new PropertyMasterlistBll();
        MiscBll miscBll = new MiscBll();

        //string depreciationSetting = miscBll.GetDepreciation(Common.ConnString).Split('|')[0];

        string html = string.Empty;
        List<PropertyEntity> JsonItemList = new List<PropertyEntity>();
        try
        {
            return y.GetActive(id, Common.ConnString);
        }
        catch (Exception ex)
        {
            return 0;
        }
    }

    [WebMethod]
    public static int DeleteFilter(string id)
    {
        PropertyMasterlistBll y = new PropertyMasterlistBll();
        MiscBll miscBll = new MiscBll();

        //string depreciationSetting = miscBll.GetDepreciation(Common.ConnString).Split('|')[0];

        string html = string.Empty;
        List<PropertyEntity> JsonItemList = new List<PropertyEntity>();
        try
        {
            return y.DeleteFilter(id, Common.ConnString);
        }
        catch (Exception ex)
        {
            return 0;
        }
    }

    [WebMethod]
    public static int SaveFilter(string ReportName, string Logic, string Filter, string Operator, string Value)
    {
        PropertyMasterlistBll y = new PropertyMasterlistBll();
        MiscBll miscBll = new MiscBll();

        //string depreciationSetting = miscBll.GetDepreciation(Common.ConnString).Split('|')[0];

        string html = string.Empty;
        List<PropertyEntity> JsonItemList = new List<PropertyEntity>();
        try
        {
            return y.SaveFilter(ReportName, Common.UserName, Logic, Filter, Operator, Value, Common.ConnString);
        }
        catch (Exception ex)
        {
            return 0;
        }
    }


    [WebMethod]
    public static int UpdateFilter(string RecId, string ReportName, string Logic, string Filter, string Operator, string Value)
    {
        PropertyMasterlistBll y = new PropertyMasterlistBll();
        MiscBll miscBll = new MiscBll();

        //string depreciationSetting = miscBll.GetDepreciation(Common.ConnString).Split('|')[0];

        string html = string.Empty;
        List<PropertyEntity> JsonItemList = new List<PropertyEntity>();
        try
        {
            return y.UpdateFilter(RecId, ReportName, Common.UserName, Logic, Filter, Operator, Value, Common.ConnString);
        }
        catch (Exception ex)
        {
            return 0;
        }
    }



    [WebMethod]
    public static string GetAdvancefilterperUser(string ReportID, string trx)
    {
        //Console.WriteLine("getADVFilter");

        AdvanceFilterBll x = new AdvanceFilterBll();
        List<AdvanceFilter> AF = new List<AdvanceFilter>();
        var RET = string.Empty;
        try
        {
            DataTable dt = x.GetAdvancefilterperUser(ReportID, trx, Common.ConnString, Common.UserID);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    AdvanceFilter y = new AdvanceFilter();
                    var btnclass = string.Empty;
                    y.Logic = dt.Rows[i]["Logic"].ToString();
                    y.FilterBy = dt.Rows[i]["FilterBy"].ToString();
                    y.Operator = dt.Rows[i]["operator"].ToString();
                    y.Values = dt.Rows[i]["Values"].ToString();
                    if (dt.Rows[i]["isUse"].ToString() == "True")
                    {
                        btnclass = "btn-success";
                    }
                    else
                    {
                        btnclass = "btn-secondary";
                    }
                    y.ReportID = "<center><button class='btn btn-xs " + btnclass + "' id='UsedFilter" + dt.Rows[i]["RecID"] + "' type='button' data-toggle='tooltip'  title='Use Filter' onclick=\"ModADVfilter('" + dt.Rows[i]["RecID"] + "','USE')\"><span class=\"	fa fa-check-circle\" color: White;\"></span></button> "
                        + "<button class='btn btn-xs btn-danger' type='button' data-toggle='tooltip'  title='Delete Filter' onclick=\"ModifyAdvFilter('" + dt.Rows[i]["RecID"] + "','DELETE')\"><span class=\"fa fa-trash\" color: White;\"></span></button> ";
                    AF.Add(y);
                }

                JavaScriptSerializer jss = new JavaScriptSerializer();
                jss.MaxJsonLength = Int32.MaxValue;
                RET = jss.Serialize(AF);

                return RET;
            }
            else
            {
                return "";
            }
        }
        catch (Exception)
        {
            throw;
        }
    }



    [WebMethod]
    public static int ADVfilterModification(string RecID, string trx)
    {
        AdvanceFilterBll x = new AdvanceFilterBll();

        return x.ADVfilterModification(RecID, trx, Common.ConnString);
    }


    [WebMethod]
    public static string GetSMTP()
    {
        MiscBll mb = new MiscBll();
        string smtp = mb.GetSMTP(Common.ConnString);
        return smtp;
    }

    [WebMethod]
    public static string GetUserAcccountSettings()
    {
        MiscBll mb = new MiscBll();
        string upasspolicy = mb.GetUserAcccountSettings(Common.ConnString);

        return upasspolicy;
    }

    [WebMethod]
    public static string CheckR(string context)
    {
        RoleBll rb = new RoleBll();
        return rb.CheckR(context, Convert.ToInt32(Common.Role), Common.ConnString);

    }


    [WebMethod]
    public static string CheckA(string ModuleAccess)
    {
        RoleBll rb = new RoleBll();
        string isAccess = "";
        if (ModuleAccess != "")
        {
            List<string> Access = rb.CheckA(Convert.ToInt32(Common.Role), Common.ConnString);
            string[] mAccess = ModuleAccess.Split('|');
            foreach (string MAccess in mAccess)
            {
                if (Access.Contains(MAccess))
                {
                    isAccess += "1|";
                }
                else
                {
                    isAccess += "0|";
                }
            }
        }

        return isAccess.Remove(isAccess.Length - 1);

    }


    [WebMethod]
    public static string GetAssetInventoryAccount()
    {
        ChartOfAccountsBll coaBll = new ChartOfAccountsBll();
        DataTable dt = coaBll.GetAssetInventoryAccount(Common.ConnString);
        return Control.MyConverter.ConvertDataTabletoJSON(dt);
    }

    [WebMethod]
    public static string GetCetagoryList()
    {
        CategoryBll categoryBll = new CategoryBll();
        DataTable dt = categoryBll.GetCategoryList(Common.ConnString);
        return Control.MyConverter.ConvertDataTabletoJSON(dt);
    }

    [WebMethod]
    public static string SaveCategory(int id, string category, string description, int inventoryaccount, bool active)
    {
        CategoryEntity categoryEntity = new CategoryEntity();
        CategoryBll categoryBll = new CategoryBll();

        categoryEntity.CategoryID = id;
        categoryEntity.CategoryName = category;
        categoryEntity.Description = description;
        categoryEntity.AssetInventoryAccount = inventoryaccount;
        categoryEntity.Active = active;
        categoryEntity.CreatedBy = Common.UserName;
        categoryEntity.ConnectionString = Common.ConnString;

        return categoryBll.SaveCategory(categoryEntity).ToString();
    }

    [WebMethod]
    public static string GetModelList()
    {
        ModelBll modelBll = new ModelBll();

        DataTable dt = modelBll.GetModelList(Common.ConnString);
        return Control.MyConverter.ConvertDataTabletoJSON(dt);
    }

    [WebMethod]
    public static string SaveModel(int modelid, string modelname, string description, bool active)
    {
        ModelEntity modelEntity = new ModelEntity();
        ModelBll modelBll = new ModelBll();

        modelEntity.ModelID = modelid;
        modelEntity.ModelName = modelname;
        modelEntity.Description = description;
        modelEntity.Active = active;
        modelEntity.Image = ".";

        modelEntity.CreatedBy = Common.UserName;
        modelEntity.ConnectionString = Common.ConnString;

        return modelBll.SaveModel(modelEntity).ToString();

    }

    [WebMethod]
    public static string SaveClassification(int id, string classification, string description, bool active, string classificationcode, string floater)
    //public static string SaveClassification(int id, string classification, string cc, int startingcount, string description, bool active)
    {
        ClassificationEntity classificationEntity = new ClassificationEntity();
        ClassificationBll classificationBll = new ClassificationBll();

        classificationEntity.ClassificationID = id;
        classificationEntity.ClassificationName = classification;
        classificationEntity.ClassificationCode = classificationcode;
        classificationEntity.Description = description;
        classificationEntity.IsFloater = floater;
        classificationEntity.Active = active;
        classificationEntity.CreatedBy = Common.UserName;
        classificationEntity.ConnectionString = Common.ConnString;

        return classificationBll.SaveClassification(classificationEntity).ToString();
    }
    [WebMethod]
    public static string GetClassificationList()
    {
        ClassificationBll classificationBll = new ClassificationBll();

        DataTable dt = classificationBll.GetClassificationList(Common.ConnString);
        return Control.MyConverter.ConvertDataTabletoJSON(dt);
    }

    [WebMethod]
    public static string GetBrandList()
    {
        BrandBll brandBll = new BrandBll();

        DataTable dt = brandBll.GetBrandList(Common.ConnString);
        return Control.MyConverter.ConvertDataTabletoJSON(dt);
    }

    [WebMethod]
    public static string SaveBrand(int id, string brand, string description, bool active)
    {
        BrandEntity brandEntity = new BrandEntity();
        BrandBll brandBll = new BrandBll();

        brandEntity.BrandID = id;
        brandEntity.BrandName = brand;
        brandEntity.Description = description;
        brandEntity.Active = active;
        brandEntity.CreatedBy = Common.UserName;
        brandEntity.ConnectionString = Common.ConnString;

        return brandBll.SaveBrand(brandEntity).ToString();

    }

    [WebMethod]
    public static int CheckUOMCode(int id, string cc)
    {
        UOMEntity uomEntity = new UOMEntity();
        UOMBll uomBll = new UOMBll();

        uomEntity.UnitOfMeasurementID = id;
        uomEntity.UOMCode = cc;
        uomEntity.ConnectionString = Common.ConnString;

        return uomBll.checkUOMCode(uomEntity);
    }

    [WebMethod]
    public static string GetUnitOfMeasurementList()
    {
        UOMBll uomBll = new UOMBll();

        DataTable dt = uomBll.GetUOMlist(Common.ConnString);
        return Control.MyConverter.ConvertDataTabletoJSON(dt);
    }

    [WebMethod]
    public static string SaveTag(int tagid, string tag, int tagtypeid, string description, bool active)
    {
        TagTypeEntity x = new TagTypeEntity();
        TagTypeBll y = new TagTypeBll();

        x.TagID = tagid;
        x.Tag = tag;
        x.TagTypeID = tagtypeid;
        x.Description = description;
        x.Active = active;
        x.CreatedBy = Common.UserName;
        x.ConnectionString = Common.ConnString;

        return y.SaveTag(x).ToString();

        //return GetLocation(pageNo);
    }

    [WebMethod]
    public static string GetTag()
    {
        TagTypeBll ttbll = new TagTypeBll();

        DataTable dt = ttbll.GetTag(Common.ConnString);
        return Control.MyConverter.ConvertDataTabletoJSON(dt);
    }

    [WebMethod]
    public static string GetTagType()
    {
        TagTypeBll tagTypeBll = new TagTypeBll();

        DataTable dt = tagTypeBll.GetTagType(Common.ConnString);

        return Control.MyConverter.ConvertDataTabletoJSON(dt);
    }


    [WebMethod]
    public static string GetSite()
    {
        WarehouseBll warehouseBll = new WarehouseBll();
        DataTable dt = warehouseBll.GetWarehouseList(Common.ConnString);

        return Control.MyConverter.ConvertDataTabletoJSON(dt);
    }

    [WebMethod]
    public static string GetLocation()
    {
        LocationEntity locationEntity = new LocationEntity();
        LocationBll locationBll = new LocationBll();

        locationEntity.ConnectionString = Common.ConnString;

        DataTable dt = locationBll.GetLocationList(locationEntity);

        return Control.MyConverter.ConvertDataTabletoJSON(dt);
    }

    [WebMethod]
    public static string SaveTagType(int id, string TagType, string description, bool active)
    {
        TagTypeEntity x = new TagTypeEntity();
        TagTypeBll y = new TagTypeBll();

        x.TagTypeID = id;
        x.TagType = TagType;
        x.Description = description;
        x.Active = active;
        x.CreatedBy = Common.UserName;
        x.ConnectionString = Common.ConnString;

        return y.SaveTagType(x).ToString();
    }
    [WebMethod]
    public static string SaveDepartment(int id, string department, string description, bool active)
    {
        DepartmentEntity departmentEntity = new DepartmentEntity();
        DepartmentBll departmentBll = new DepartmentBll();

        departmentEntity.DepartmentID = id;
        departmentEntity.Department = department;
        departmentEntity.Description = description;
        departmentEntity.Active = active;
        departmentEntity.User = Common.UserName;
        departmentEntity.ConnectionString = Common.ConnString;

        return departmentBll.SaveDepartment(departmentEntity).ToString();

    }
    [WebMethod]
    public static string GetDepartmentList()
    {
        DepartmentEntity departmentEntity = new DepartmentEntity();
        DepartmentBll departmentBll = new DepartmentBll();

        departmentEntity.ConnectionString = Common.ConnString;

        DataTable dt = departmentBll.GetDepartmentList(departmentEntity);

        return Control.MyConverter.ConvertDataTabletoJSON(dt);
    }

    [WebMethod]
    public static string GetDepartmentList1()
    {
        DepartmentEntity departmentEntity = new DepartmentEntity();
        DepartmentBll departmentBll = new DepartmentBll();

        departmentEntity.ConnectionString = Common.ConnString;

        DataTable dt = departmentBll.GetDepartmentList1(departmentEntity);

        return Control.MyConverter.ConvertDataTabletoJSON(dt);
    }
    [WebMethod]
    public static string SavePosition(int positionid, string position, int departmentid, string description, bool active)
    {
        PositionEntity positionEntity = new PositionEntity();
        PositionBll positionBll = new PositionBll();

        positionEntity.PositionID = positionid;
        positionEntity.Position = position;
        positionEntity.DepartmentID = departmentid;
        positionEntity.Description = description;
        positionEntity.Active = active;
        positionEntity.CreatedBy = Common.UserName;
        positionEntity.ConnectionString = Common.ConnString;

        return positionBll.SavePosition(positionEntity).ToString();

    }
    [WebMethod]
    public static string GetJobTitle()
    {
        DepartmentEntity departmentEntity = new DepartmentEntity();
        DepartmentBll departmentBll = new DepartmentBll();

        departmentEntity.ConnectionString = Common.ConnString;

        DataTable dt = departmentBll.GetDepartmentList(departmentEntity);

        return Control.MyConverter.ConvertDataTabletoJSON(dt);
    }
    [WebMethod]
    public static string GetSupplier()
    {
        SupplierBll supplierBll = new SupplierBll();

        DataTable dt = supplierBll.GetSupplierList(Common.ConnString);

        return Control.MyConverter.ConvertDataTabletoJSON(dt);
    }

    [WebMethod]
    public static string SaveSupplier(int id, string supplier, string contactnumber, string email, bool active)
    {
        SupplierEntity supplierEntity = new SupplierEntity();
        SupplierBll supplierBll = new SupplierBll();

        supplierEntity.SupplierID = id;
        supplierEntity.Supplier = supplier;
        supplierEntity.ContactNumber = contactnumber;
        supplierEntity.Active = active;
        supplierEntity.CreatedBy = Common.UserName;
        supplierEntity.ConnectionString = Common.ConnString;
        supplierEntity.Email = email;

        return supplierBll.SaveSupplier(supplierEntity).ToString();
    }

    [WebMethod]
    public static string GetEmployee()
    {
        UserEntity userEntity = new UserEntity();
        UserBll userBll = new UserBll();

        DataTable dt = userBll.GetUserListBySite(Common.ConnString, Convert.ToInt32(Common.SiteID));

        return Control.MyConverter.ConvertDataTabletoJSON(dt);
    }
    [WebMethod]
    public static string GetPosition(int department)
    {
        PositionEntity positionEntity = new PositionEntity();
        PositionBll positionBll = new PositionBll();

        positionEntity.ConnectionString = Common.ConnString;
        positionEntity.DepartmentID = department;

        DataTable dt = positionBll.GetPosition(positionEntity);
        return Control.MyConverter.ConvertDataTabletoJSON(dt);
    }

    static int ConvertStringToInt(string sval)
    {
        int myout = 0;
        int.TryParse(sval, out myout);
        return myout;
    }

    [WebMethod]
    //public static string SaveUser(string firstname, string lastname, string cardno, int departmentid, string position, string emailaddress, string username, string password, bool administrative, bool active, string extension,int lobid, int pageNo)
    public static string SaveUser(int userid, string firstname, string lastname, string employeeno, string emailaddress, string username, bool active, bool custodian, bool administrative, bool approver, bool inspector, bool certifier, string extension, int department, int position, string integrator, int siteid)
    //public static string SaveUser(int userid, string firstname, string lastname, string employeeno, string emailaddress, string username, bool active, bool administrative, bool approver, bool inspector, bool certifier, string extension, int department, int position, string integrator, int siteid, int pageNo)

    {
        int Pass_Minimum_Length = 0;
        int Upper_Case_length = 0;
        int Lower_Case_length = 0;
        int NonAlpha_length = 0;
        int Numeric_length = 0;
        string[] upasspolicy = GetUserAcccountSettings().Split('|');

        if (upasspolicy[4] == "True")
        {
            Upper_Case_length = ConvertStringToInt(upasspolicy[5]);
        }
        if (upasspolicy[6] == "True")
        {
            Lower_Case_length = Convert.ToInt32(upasspolicy[7]);
        }
        if (upasspolicy[8] == "True")
        {
            Numeric_length = Convert.ToInt32(upasspolicy[9]);
        }
        if (upasspolicy[10] == "True")
        {
            NonAlpha_length = Convert.ToInt32(upasspolicy[11]);
        }

        Pass_Minimum_Length = Convert.ToInt32(upasspolicy[3]);

        string generatedpass = "";
        var generator = new PasswordGenerator(Pass_Minimum_Length,
                                      Pass_Minimum_Length,
                                      Lower_Case_length,
                                      Upper_Case_length,
                                      Numeric_length,
                                      NonAlpha_length);
        generatedpass = generator.Generate();


        UserEntity userEntity = new UserEntity();
        UserBll userBLL = new UserBll();

        userEntity.UserID = userid;
        userEntity.FirstName = firstname;
        userEntity.LastName = lastname;
        userEntity.EmployeeNo = employeeno;
        userEntity.EmailAddress = emailaddress;
        userEntity.UserName = username;
        userEntity.ImageExtension = '.' + extension;
        userEntity.DepartmentID = department;
        userEntity.PositionID = position;
        //userEntity.IntegratorID = integrator;
        userEntity.IsCustodian = custodian;
        userEntity.Custodians = integrator;
        userEntity.IsAdministrative = administrative;
        userEntity.IsApprover = approver;
        userEntity.IsInspector = inspector;
        userEntity.IsCertifier = certifier;
        userEntity.Active = active;
        userEntity.SiteID = siteid;
        userEntity.CreatedBy = Common.UserName;
        userEntity.ConnectionString = Common.ConnString;
        userEntity.Password = AES.Encrypt(generatedpass, generatedpass);


        //string SaveUser = userBLL.SaveUser(userEntity).ToString();

        string SaveUser = userBLL.SaveUser(userEntity, Convert.ToInt32(Common.noofminda), Convert.ToInt32(Common.nooftsuc)).ToString();
        try
        {
            if (SaveUser == "0")
            {
                string smtp = GetSMTP();
                string Body = "";
                Body += "Hello Sir/Maam " + lastname + ", " + firstname + ",<br><br>"
                   + "You may now log-in to " + retEmail + " using these credentials : <br/><br/> Username: <b>" + username + "</b><br>Password: <b>" + generatedpass + "</b><br><br>"
                   + "<b>PLEASE PROTECT YOUR ACCOUNT INFORMATION</b>. You assume full responsibility for maintaining the confidentiality of your Username and Password and for all transactions made using your account. <br><br>"
                   + "The Management/Administrator shall not be held liable for any activities arising from access or use of your account, whether such access or use was with or without authority granted by you as account holder.<br><br> "
                   + "Please be reminded also to <b>log out</b> from your account at the end of each session.";

                EmailSending.SendEmail(smtp, emailaddress, Common.EmailSubject + " (Password)", Body);
            }
        }
        catch (Exception)
        {
        }


        return SaveUser;

    }

    [WebMethod]
    public static string GetDeviceUsageType()
    {
        DeviceBll deviceBll = new DeviceBll();

        DataTable dt = deviceBll.GetDeviceTypeList(Common.ConnString);

        return Control.MyConverter.ConvertDataTabletoJSON(dt);
        //ddlDeviceUsageType.DataSource = dt;
        //ddlDeviceUsageType.DataValueField = "DevicetypeID";
        //ddlDeviceUsageType.DataTextField = "DeviceType";
        //ddlDeviceUsageType.DataBind();
        ////ddlDeviceUsageType.Items.Insert(0, "--- Select Device Type ---");
    }

    [WebMethod]
    public static string SaveDevice(int id, string device, string ipaddress, int port, string description, int devicetypeid, bool active)
    {
        DeviceEntity deviceEntity = new DeviceEntity();
        DeviceBll deviceBll = new DeviceBll();

        deviceEntity.DeviceID = id;
        deviceEntity.DeviceName = device;
        deviceEntity.IPAddress = ipaddress;
        deviceEntity.Port = port;
        deviceEntity.Description = description;
        deviceEntity.DeviceTypeID = devicetypeid;
        deviceEntity.Active = active;
        deviceEntity.CreatedBy = Common.UserID;
        deviceEntity.ConnectionString = Common.ConnString;

        return deviceBll.SaveDevice(deviceEntity).ToString();

    }

    [WebMethod]
    public static string GetDeviceList()
    {
        TagTypeBll tagTypeBll = new TagTypeBll();

        DataTable dt = tagTypeBll.GetDeviceList(Common.ConnString);
        return Control.MyConverter.ConvertDataTabletoJSON(dt);

        //ddlPrinter.DataSource = dt;
        //ddlPrinter.DataValueField = "DeviceID";
        //ddlPrinter.DataTextField = "DeviceName";
        //ddlPrinter.DataBind();
        //ddlPrinter.Items.Insert(0, "--- Select Device ---");
        //ddlPrinter.Items.Insert(1, "Paper Print");
    }
    [WebMethod]
    public static string ReportPage(string key1, string key2, string key3, string key4, string key5)
    {
        try
        {

            if (key1 == "IAR")
            {

                //get datable of IAR
                PropertyBll propertyBll = new PropertyBll();

                //DataTable dt3 = propertyBll.GetGoodsReceiveDetails(Common.ConnString, Convert.ToInt32(key2));
                GoodsReceiveHistoryBll GoodsReceiveHistoryBll = new GoodsReceiveHistoryBll();

                DataTable dt = propertyBll.GetGoodsReceiveDetails(Common.ConnString, Convert.ToInt32(key2));
                DataTable dt1 = GoodsReceiveHistoryBll.GetGRHeader(Common.ConnString, Convert.ToInt32(key2));
                String ReportTitle = "IAR";

                //DataTable dt = new DataTable();
                DataTable dtFiltered = new DataTable();
                dtFiltered.Columns.Add("PRNumber", typeof(string));
                dtFiltered.Columns.Add("Description", typeof(string));
                dtFiltered.Columns.Add("Sample1", typeof(string));
                dtFiltered.Columns.Add("UnitOfMeasurement", typeof(string));
                dtFiltered.Columns.Add("Quantity", typeof(string));
                dtFiltered.Columns.Add("Sample12", typeof(string));
                dtFiltered.Columns.Add("ItemCode", typeof(string));
                DataRow dtFilteredRow = dtFiltered.NewRow();

                DataTable dtGRHeader = new DataTable();
                dtGRHeader.Columns.Add("SupplierName", typeof(string));
                dtGRHeader.Columns.Add("PONumber", typeof(string));
                dtGRHeader.Columns.Add("GoodsReceiveNumber", typeof(string));
                dtGRHeader.Columns.Add("CreatedDate", typeof(string));
                DataRow dtGRHeaderRow = dtGRHeader.NewRow();


                foreach (DataRow suRow in dt.Rows)
                {

                    dtFilteredRow = dtFiltered.NewRow();
                    dtFilteredRow["ItemCode"] = suRow[4];
                    dtFilteredRow["Description"] = suRow[16];
                    dtFilteredRow["UnitOfMeasurement"] = suRow[17];
                    dtFilteredRow["Quantity"] = suRow[5];

                    dtFiltered.Rows.Add(dtFilteredRow);

                    //add to FIlter
                }

                foreach (DataRow suRow1 in dt1.Rows)
                {

                    dtGRHeaderRow = dtGRHeader.NewRow();
                    dtGRHeaderRow["SupplierName"] = suRow1[20];
                    dtGRHeaderRow["PONumber"] = suRow1[19];
                    dtGRHeaderRow["GoodsReceiveNumber"] = suRow1[1];
                    dtGRHeaderRow["CreatedDate"] = suRow1[8];
                    //dtGRHeaderRow["Approver"] = suRow1[10];

                    dtGRHeader.Rows.Add(dtGRHeaderRow);

                    //add to FIlter
                }

                //call ExportToExcelIAR
                return ExcelReports.ExportToExcelIAR(dtFiltered, dtGRHeader, ReportTitle, dt, dt1);

            }
            else if (key1 == "RIS" && key3 == "PI")
            {
                PropertyEntity x = new PropertyEntity();
                PropertyIssuanceBll y = new PropertyIssuanceBll();

                x.IssuanceNumber = key2;
                x.ConnectionString = Common.ConnString;

                string issuancedetails = y.PropertyIssuanceDetails(x);
                string employeename = issuancedetails.Split('|')[1];
                string employeedep = issuancedetails.Split('|')[2];
                string employeepos = issuancedetails.Split('|')[3];
                string approvername = issuancedetails.Split('|')[4];
                string transactorname = issuancedetails.Split('|')[5];
                string transactordep = issuancedetails.Split('|')[6];
                string transactorpos = issuancedetails.Split('|')[7];
                string custodian = issuancedetails.Split('|')[8];
                string remarks = issuancedetails.Split('|')[9];
                string issueddate = issuancedetails.Split('|')[10];
                string site = issuancedetails.Split('|')[13];

                string approverpos = issuancedetails.Split('|')[14];
                string issuedby = issuancedetails.Split('|')[15];
                string issuedpos = issuancedetails.Split('|')[16];


                DataTable dt = y.PLPendingUpload(x);

                return ExcelReports.ExportToExcelRISPRequest(dt, key2, employeename, employeedep, employeepos, transactorname, transactordep, transactorpos, issueddate, remarks, custodian, approvername, site, approverpos, issuedby, issuedpos);

            }
            else if (key1 == "RIS" && key3 == "PR")
            {
                PropertyEntity x = new PropertyEntity();
                PropertyIssuanceBll y = new PropertyIssuanceBll();
                PropertyRequestBll z = new PropertyRequestBll();

                x.RequestNumber = key2;
                x.ConnectionString = Common.ConnString;

                string requestdetails = y.PropertyRequestDetails(x);
                string employeename = requestdetails.Split('|')[1];
                string employeedep = requestdetails.Split('|')[2];
                string employeepos = requestdetails.Split('|')[3];
                string approvername = requestdetails.Split('|')[4];
                string transactorname = requestdetails.Split('|')[5];
                string transactordep = requestdetails.Split('|')[6];
                string transactorpos = requestdetails.Split('|')[7];
                string custodian = requestdetails.Split('|')[8];
                string remarks = requestdetails.Split('|')[9];
                string issueddate = requestdetails.Split('|')[10];
                string returndate = requestdetails.Split('|')[12];
                string site = requestdetails.Split('|')[13];
                string approverpos = requestdetails.Split('|')[14];
                string IssuedBy = requestdetails.Split('|')[15];
                string IssuedByPos = requestdetails.Split('|')[16];

                DataTable dt = z.PRLPendingUpload(x);

                return ExcelReports.ExportToExcelRISPRequest(dt, key2, employeename, employeedep, employeepos, "", "", "", issueddate, remarks, custodian, approvername, site, approverpos, IssuedBy, IssuedByPos);

            }
            else if (key1 == "ICS" && key3 == "PIssuance")
            {
                PropertyEntity x = new PropertyEntity();
                PropertyIssuanceBll y = new PropertyIssuanceBll();

                x.IssuanceNumber = key2;
                x.ConnectionString = Common.ConnString;

                string issuancedetails = y.PropertyIssuanceDetails(x);
                string employeename = issuancedetails.Split('|')[1];
                string employeedep = issuancedetails.Split('|')[2];
                string employeepos = issuancedetails.Split('|')[3];
                string approvername = issuancedetails.Split('|')[4];
                string transactorname = issuancedetails.Split('|')[5];
                string transactordep = issuancedetails.Split('|')[6];
                string transactorpos = issuancedetails.Split('|')[7];
                string custodian = issuancedetails.Split('|')[8];
                string remarks = issuancedetails.Split('|')[9];
                string issueddate = issuancedetails.Split('|')[10];

                DataTable dt = y.PLPendingUploadWithComponent(x);

                return ExcelReports.ExportToExcelICS(dt, key2, employeename, employeedep, employeepos, transactorname, transactordep, transactorpos, issueddate, remarks, custodian, approvername);

            }
            else if (key1 == "ICS" && key3 == "PRequest")
            {
                PropertyEntity x = new PropertyEntity();
                PropertyIssuanceBll y = new PropertyIssuanceBll();
                PropertyRequestBll z = new PropertyRequestBll();

                x.RequestNumber = key2;
                x.ConnectionString = Common.ConnString;

                string requestdetails = y.PropertyRequestDetails(x);
                string employeename = requestdetails.Split('|')[1];
                string employeedep = requestdetails.Split('|')[2];
                string employeepos = requestdetails.Split('|')[3];
                string approvername = requestdetails.Split('|')[4];
                string transactorname = "";
                string transactordep = "";
                string transactorpos = "";
                string custodian = requestdetails.Split('|')[5];
                string remarks = requestdetails.Split('|')[6];
                string issueddate = requestdetails.Split('|')[7];
                string returndate = requestdetails.Split('|')[9];

                DataTable dt = z.PRLPendingUpload(x);


                return ExcelReports.ExportToExcelICSRequest(dt, key2, employeename, employeedep, employeepos, transactorname, transactordep, transactorpos, issueddate, remarks, custodian, approvername);

            }
            else if (key1 == "ICS" && key3 == "PTransfer")
            {
                PropertyEntity x = new PropertyEntity();
                PropertyTransferBll y = new PropertyTransferBll();

                string transferdetails = y.PropertyTransferDetailsView(Common.ConnString, key2);

                string approvername = transferdetails.Split('|')[1];
                string approveremail = transferdetails.Split('|')[2];
                string transactorname = transferdetails.Split('|')[3];
                string transactoremail = transferdetails.Split('|')[4];
                string custodian = transferdetails.Split('|')[5];
                string remarks = transferdetails.Split('|')[6];
                string createddate = transferdetails.Split('|')[7];
                string fromsitename = transferdetails.Split('|')[8];
                string fromlocationname = transferdetails.Split('|')[9];
                string fromareaname = transferdetails.Split('|')[10];
                string tositename = transferdetails.Split('|')[11];
                string tolocationname = transferdetails.Split('|')[12];
                string toareaname = transferdetails.Split('|')[13];
                string transactordep = transferdetails.Split('|')[14];
                string transactorpos = transferdetails.Split('|')[15];
                string approverdep = transferdetails.Split('|')[16];
                string approverpos = transferdetails.Split('|')[17];
                string issueddate = transferdetails.Split('|')[18];
                string employeename = transferdetails.Split('|')[19];
                string employeepos = transferdetails.Split('|')[20];
                string fromemployee = transferdetails.Split('|')[21]; //fromemployee
                string employeedep = transferdetails.Split('|')[23];
                string fromemployeepos = transferdetails.Split('|')[24];

                string fromloc = fromsitename + " - " + fromlocationname + " - " + fromareaname;
                string toloc = tositename + " - " + tolocationname + " - " + toareaname;
                DataTable dt = y.PLPendingApprovalWithComponent(Common.ConnString, key2);


                return ExcelReports.ExportToExcelICSTransfer(dt, key2, employeename, employeedep, employeepos, transactorname, transactordep, transactorpos, issueddate, remarks, custodian, approvername, fromemployee, fromemployeepos);

            }
            else if (key1 == "WMR" && key3 == "PDisposal")
            {
                PropertyEntity x = new PropertyEntity();
                PropertyDisposalBll y = new PropertyDisposalBll();

                x.DisposalNumber = key2;
                x.ConnectionString = Common.ConnString;

                string disposaldetails = y.PropertyDisposalDetails(x);
                string transactorname = disposaldetails.Split('|')[1];
                string approvername = disposaldetails.Split('|')[2];
                string inspectorname = disposaldetails.Split('|')[3];
                string certifiername = disposaldetails.Split('|')[4];
                string transactordep = disposaldetails.Split('|')[5];
                string transactorpos = disposaldetails.Split('|')[6];
                string custodian = disposaldetails.Split('|')[7];
                string remarks = disposaldetails.Split('|')[8];
                string disposaldate = disposaldetails.Split('|')[9];

                DataTable dt = y.PDLPendingUpload(x);

                return ExcelReports.ExportToExcelWMR(dt, key2, transactorname, approvername, inspectorname, certifiername, transactordep, transactorpos, disposaldate, remarks, custodian);

            }

            else if (key1 == "CWMR" && key3 == "PDisposal")
            {
                PropertyEntity x = new PropertyEntity();
                PropertyDisposalBll y = new PropertyDisposalBll();

                x.PropertyDisposalID = key2;
                x.ConnectionString = Common.ConnString;

                //string disposaldetails = y.PropertyDisposalDetails(x);
                //string transactorname = disposaldetails.Split('|')[1];
                //string approvername = disposaldetails.Split('|')[2];
                //string inspectorname = disposaldetails.Split('|')[3];
                //string certifiername = disposaldetails.Split('|')[4];
                //string transactordep = disposaldetails.Split('|')[5];
                //string transactorpos = disposaldetails.Split('|')[6];
                //string custodian = disposaldetails.Split('|')[7];
                //string remarks = disposaldetails.Split('|')[8];
                //string disposaldate = disposaldetails.Split('|')[9];

                DataTable dt = y.PropertyDisposalExcels(x);

                return ExcelReports.ExportToExcelCWMR(dt, key4, key5);

            }
            else if (key1 == "WMR" && key3 == "PDisposalR")
            {
                PropertyEntity x = new PropertyEntity();
                PropertyDisposalBll y = new PropertyDisposalBll();

                x.DisposalNumber = key2;
                x.ConnectionString = Common.ConnString;

                string disposaldetails = y.PropertyDisposalDetailsRequest(x);
                string transactorname = disposaldetails.Split('|')[1];
                string approvername = disposaldetails.Split('|')[2];
                string inspectorname = disposaldetails.Split('|')[3];
                string certifiername = disposaldetails.Split('|')[4];
                string transactordep = disposaldetails.Split('|')[5];
                string transactorpos = disposaldetails.Split('|')[6];
                string custodian = disposaldetails.Split('|')[7];
                string remarks = disposaldetails.Split('|')[8];
                string disposaldate = disposaldetails.Split('|')[9];

                DataTable dt = y.PDLPendingUploadRequest(x);

                return ExcelReports.ExportToExcelWMRR(dt, key2, transactorname, approvername, inspectorname, certifiername, transactordep, transactorpos, disposaldate, remarks, custodian);

            }
            else if (key1 == "ITR")
            {
                PropertyEntity x = new PropertyEntity();
                PropertyTransferBll y = new PropertyTransferBll();

                x.TransferNumber = key2;
                x.ConnectionString = Common.ConnString;

                string transferdetails = y.PropertyTransferDetailsView(Common.ConnString, key2);

                string approvername = transferdetails.Split('|')[1];
                string approveremail = transferdetails.Split('|')[2];
                string transactorname = transferdetails.Split('|')[3];
                string transactoremail = transferdetails.Split('|')[4];
                string custodian = transferdetails.Split('|')[5];
                string remarks = transferdetails.Split('|')[6];
                string createddate = transferdetails.Split('|')[7];
                string fromsitename = transferdetails.Split('|')[8];
                string fromlocationname = transferdetails.Split('|')[9];
                string fromareaname = transferdetails.Split('|')[10];
                string tositename = transferdetails.Split('|')[11];
                string tolocationname = transferdetails.Split('|')[12];
                string toareaname = transferdetails.Split('|')[13];
                string transactordep = transferdetails.Split('|')[14];
                string transactorpos = transferdetails.Split('|')[15];
                string approverdep = transferdetails.Split('|')[16];
                string approverpos = transferdetails.Split('|')[17];
                string issueddate = transferdetails.Split('|')[18];
                string employeename = transferdetails.Split('|')[19];
                string employeepos = transferdetails.Split('|')[20];

                string fromEmployeeName = transferdetails.Split('|')[21]; //added by nero 022124
                string trxType = transferdetails.Split('|')[22]; //added by nero 022124

                ////Commented by nero 022124
                //string fromloc = fromsitename + " - " + fromlocationname + " - " + fromareaname;
                //string toloc = tositename + " - " + tolocationname + " - " + toareaname;

                string fromloc = fromsitename + " - " + fromlocationname + " - " + fromareaname;
                string toloc = tositename + " - " + tolocationname + " - " + toareaname;

                if (trxType == "1")
                {
                    fromloc = fromEmployeeName;
                    toloc = employeename;
                }
                else
                {
                    fromloc = fromsitename + " - " + fromlocationname + " - " + fromareaname;
                    toloc = tositename + " - " + tolocationname + " - " + toareaname;
                }

                DataTable dt = y.PLPendingApproval(Common.ConnString, key2);


                return ExcelReports.ExportToExcelITR(dt, key2, employeename, approverdep, employeepos, transactorname, transactordep, transactorpos, issueddate, remarks, custodian, approvername, fromloc, toloc, approverpos);

            }
            else if (key1 == "PTR")
            {
                PropertyEntity x = new PropertyEntity();
                PropertyTransferBll y = new PropertyTransferBll();

                string transferdetails = y.PropertyTransferDetailsView(Common.ConnString, key2);

                string approvername = transferdetails.Split('|')[1];
                string approveremail = transferdetails.Split('|')[2];
                string transactorname = transferdetails.Split('|')[3];
                string transactoremail = transferdetails.Split('|')[4];
                string custodian = transferdetails.Split('|')[5];
                string remarks = transferdetails.Split('|')[6];
                string createddate = transferdetails.Split('|')[7];
                string fromsitename = transferdetails.Split('|')[8];
                string fromlocationname = transferdetails.Split('|')[9];
                string fromareaname = transferdetails.Split('|')[10];
                string tositename = transferdetails.Split('|')[11];
                string tolocationname = transferdetails.Split('|')[12];
                string toareaname = transferdetails.Split('|')[13];
                string transactordep = transferdetails.Split('|')[14];
                string transactorpos = transferdetails.Split('|')[15];
                string approverdep = transferdetails.Split('|')[16];
                string approverpos = transferdetails.Split('|')[17];
                string issueddate = transferdetails.Split('|')[18];
                string employeename = transferdetails.Split('|')[19];
                string employeepos = transferdetails.Split('|')[20];
                string employeefromname = transferdetails.Split('|')[21];
                string employeedep = transferdetails.Split('|')[23];

                string fromloc = fromsitename + " - " + fromlocationname + " - " + fromareaname;
                string toloc = tositename + " - " + tolocationname + " - " + toareaname;
                DataTable dt = y.PLPendingApproval(Common.ConnString, key2);

                return ExcelReports.ExportToExcelPTR(dt, key2, employeename, approverdep, employeepos, transactorname, transactordep, transactorpos, issueddate, remarks, custodian, approvername, fromloc, toloc, employeefromname, employeedep);
            }
            else if (key1 == "RLSDDP" && key3 == "PDisposal")
            {
                PropertyEntity x = new PropertyEntity();
                PropertyDisposalBll y = new PropertyDisposalBll();

                x.DisposalNumber = key2;
                x.ConnectionString = Common.ConnString;

                string disposaldetails = y.PropertyDisposalDetails(x);
                string transactorname = disposaldetails.Split('|')[1];
                string approvername = disposaldetails.Split('|')[2];
                string inspectorname = disposaldetails.Split('|')[3];
                string certifiername = disposaldetails.Split('|')[4];
                string transactordep = disposaldetails.Split('|')[5];
                string transactorpos = disposaldetails.Split('|')[6];
                string custodian = disposaldetails.Split('|')[7];
                string remarks = disposaldetails.Split('|')[8];
                string disposaldate = disposaldetails.Split('|')[9];

                DataTable dt = y.PDLPendingUpload(x);

                return ExcelReports.ExportToExcelRLSDDP(dt, key2, transactorname, approvername, inspectorname, certifiername, transactordep, transactorpos, disposaldate, remarks, custodian);

            }
            else if (key1 == "RLSDDP" && key3 == "PDisposalR")
            {
                PropertyEntity x = new PropertyEntity();
                PropertyDisposalBll y = new PropertyDisposalBll();

                x.DisposalNumber = key2;
                x.ConnectionString = Common.ConnString;

                string disposaldetails = y.PropertyDisposalDetailsRequest(x);
                string transactorname = disposaldetails.Split('|')[1];
                string approvername = disposaldetails.Split('|')[2];
                string inspectorname = disposaldetails.Split('|')[3];
                string certifiername = disposaldetails.Split('|')[4];
                string transactordep = disposaldetails.Split('|')[5];
                string transactorpos = disposaldetails.Split('|')[6];
                string custodian = disposaldetails.Split('|')[7];
                string remarks = disposaldetails.Split('|')[8];
                string disposaldate = disposaldetails.Split('|')[9];

                DataTable dt = y.PDLPendingUploadRequest(x);

                return ExcelReports.ExportToExcelRLSDDPR(dt, key2, transactorname, approvername, inspectorname, certifiername, transactordep, transactorpos, disposaldate, remarks, custodian);

            }
            else if (key1 == "IIRUP")
            {
                PropertyEntity x = new PropertyEntity();
                PropertyDisposalBll y = new PropertyDisposalBll();

                x.DisposalNumber = key2;
                x.ConnectionString = Common.ConnString;

                string disposaldetails = y.PropertyDisposalDetails(x);
                string transactorname = disposaldetails.Split('|')[1];
                string approvername = disposaldetails.Split('|')[2];
                string inspectorname = disposaldetails.Split('|')[3];
                string certifiername = disposaldetails.Split('|')[4];
                string transactordep = disposaldetails.Split('|')[5];
                string transactorpos = disposaldetails.Split('|')[6];
                string custodian = disposaldetails.Split('|')[7];
                string remarks = disposaldetails.Split('|')[8];
                string disposaldate = disposaldetails.Split('|')[9];
                string approverpos = disposaldetails.Split('|')[14];

                DataTable dt = y.PDLPendingUpload(x);

                return ExcelReports.ExportToExcelIIRUP(dt, key2, transactorname, approvername, inspectorname, certifiername, transactordep, transactorpos, disposaldate, remarks, custodian, approverpos);
            }

            else if (key1 == "CIIRUP")
            {
                PropertyEntity x = new PropertyEntity();
                PropertyDisposalBll y = new PropertyDisposalBll();

                x.PropertyDisposalID = key2;
                x.ConnectionString = Common.ConnString;

                //string disposaldetails = y.PropertyDisposalDetails(x);
                //string transactorname = disposaldetails.Split('|')[1];
                //string approvername = disposaldetails.Split('|')[2];
                //string inspectorname = disposaldetails.Split('|')[3];
                //string certifiername = disposaldetails.Split('|')[4];
                //string transactordep = disposaldetails.Split('|')[5];
                //string transactorpos = disposaldetails.Split('|')[6];
                //string custodian = disposaldetails.Split('|')[7];
                //string remarks = disposaldetails.Split('|')[8];
                //string disposaldate = disposaldetails.Split('|')[9];
                //string approverpos = disposaldetails.Split('|')[14];

                DataTable dt = y.PropertyDisposalExcels(x);

                return ExcelReports.ExportToExcelIIRUP(dt);
            }

            else if (key1 == "RPCI")
            {
                PropertyInventoryProcessBll y = new PropertyInventoryProcessBll();
                DataTable dt = y.GetStockInventoryListPrint(Common.ConnString, key2);

                return ExcelReports.ExportToExcelRPCI(dt, key4);

            }
            else if (key1 == "IIRUSP")
            {
                PropertyEntity x = new PropertyEntity();
                PropertyDisposalBll y = new PropertyDisposalBll();

                x.DisposalNumber = key2;
                x.ConnectionString = Common.ConnString;

                string disposaldetails = y.PropertyDisposalDetails(x);
                string transactorname = disposaldetails.Split('|')[1];
                string approvername = disposaldetails.Split('|')[2];
                string inspectorname = disposaldetails.Split('|')[3];
                string certifiername = disposaldetails.Split('|')[4];
                string transactordep = disposaldetails.Split('|')[5];
                string transactorpos = disposaldetails.Split('|')[6];
                string custodian = disposaldetails.Split('|')[7];
                string remarks = disposaldetails.Split('|')[8];
                string disposaldate = disposaldetails.Split('|')[9];
                string approverpos = disposaldetails.Split('|')[14];

                DataTable dt = y.PDLPendingUpload(x);


                return ExcelReports.ExportToExcelIIRUSP(dt, key2, transactorname, approvername, inspectorname, certifiername, transactordep, transactorpos, disposaldate, remarks, custodian, approverpos);


            }
            else if (key1 == "CIIRUSP")
            {
                PropertyEntity x = new PropertyEntity();
                PropertyDisposalBll y = new PropertyDisposalBll();

                x.PropertyDisposalID = key2;
                x.ConnectionString = Common.ConnString;

                //string disposaldetails = y.PropertyDisposalDetails(x);
                //string transactorname = disposaldetails.Split('|')[1];
                //string approvername = disposaldetails.Split('|')[2];
                //string inspectorname = disposaldetails.Split('|')[3];
                //string certifiername = disposaldetails.Split('|')[4];
                //string transactordep = disposaldetails.Split('|')[5];
                //string transactorpos = disposaldetails.Split('|')[6];
                //string custodian = disposaldetails.Split('|')[7];
                //string remarks = disposaldetails.Split('|')[8];
                //string disposaldate = disposaldetails.Split('|')[9];
                //string approverpos = disposaldetails.Split('|')[14];

                DataTable dt = y.PropertyDisposalExcels(x);


                return ExcelReports.ExportToExcelCIIRUSP(dt);


            }
            else if (key1 == "RLSDDSP" && key3 == "PDisposal")
            {
                PropertyEntity x = new PropertyEntity();
                PropertyDisposalBll y = new PropertyDisposalBll();

                x.DisposalNumber = key2;
                x.ConnectionString = Common.ConnString;

                string disposaldetails = y.PropertyDisposalDetails(x);
                string transactorname = disposaldetails.Split('|')[1];
                string approvername = disposaldetails.Split('|')[2];
                string inspectorname = disposaldetails.Split('|')[3];
                string certifiername = disposaldetails.Split('|')[4];
                string transactordep = disposaldetails.Split('|')[5];
                string transactorpos = disposaldetails.Split('|')[6];
                string custodian = disposaldetails.Split('|')[7];
                string remarks = disposaldetails.Split('|')[8];
                string disposaldate = disposaldetails.Split('|')[9];

                DataTable dt = y.PDLPendingUpload(x);

                return ExcelReports.ExportToExcelRLSDDSP(dt, key2, transactorname, approvername, inspectorname, certifiername, transactordep, transactorpos, disposaldate, remarks, custodian);
            }
            else if (key1 == "RLSDDSP" && key3 == "PDisposalR")
            {
                PropertyEntity x = new PropertyEntity();
                PropertyDisposalBll y = new PropertyDisposalBll();

                x.DisposalNumber = key2;
                x.ConnectionString = Common.ConnString;

                string disposaldetails = y.PropertyDisposalDetailsRequest(x);
                string transactorname = disposaldetails.Split('|')[1];
                string approvername = disposaldetails.Split('|')[2];
                string inspectorname = disposaldetails.Split('|')[3];
                string certifiername = disposaldetails.Split('|')[4];
                string transactordep = disposaldetails.Split('|')[5];
                string transactorpos = disposaldetails.Split('|')[6];
                string custodian = disposaldetails.Split('|')[7];
                string remarks = disposaldetails.Split('|')[8];
                string disposaldate = disposaldetails.Split('|')[9];

                DataTable dt = y.PDLPendingUploadRequest(x);

                return ExcelReports.ExportToExcelRLSDDSPR(dt, key2, transactorname, approvername, inspectorname, certifiername, transactordep, transactorpos, disposaldate, remarks, custodian);
            }
            else if (key1 == "RSMI")
            {
                RSPIEntity x = new RSPIEntity();
                RSMIBll y = new RSMIBll();

                DataTable dt = y.getlists(Common.ConnString, Convert.ToInt32(key3), key4, key5);

                DataTable dt1 = y.getlists1(Common.ConnString, Convert.ToInt32(key3), key4, key5);


                return ExcelReports.ExportToExcelRSMI(dt, dt1);
            }
            else if (key1 == "RCPPE")//done
            {
                InventoryEntity x = new InventoryEntity();
                PropertyInventoryProcessBll y = new PropertyInventoryProcessBll();
                x.InventoryCode = key2;
                x.ConnectionString = Common.ConnString;


                DataTable dt = y.GetPropertyInventoryListPrintPAR(Common.ConnString, key2);


                return ExcelReports.ExportToExcelRCPPE(dt, key2);
            }
            else if (key1 == "RSEPI")//tapos na
            {

                LedgerCardEntity x = new LedgerCardEntity();
                regSEPIBll y = new regSEPIBll();

                x.PropertyNo = key2;
                x.ConnectionString = Common.ConnString;

                DataTable dt = y.getlists(x, key4, key5);


                return ExcelReports.ExportToExcelRSEPI(dt, key2);
            }
            //PRS
            else if (key1 == "RRSP")
            {
                PropertyReturnRequestBll y = new PropertyReturnRequestBll();

                DataTable dt = y.PropertyReturnList(Common.ConnString, key2);

                return ExcelReports.ExportToExcelRRSP(dt, key4, key2, key5.Split('|')[0], key5.Split('|')[1], key5.Split('|')[2], key5.Split('|')[3], key5.Split('|')[4]);

            }
            else if (key1 == "RPCSPH")
            {
                InventoryEntity x = new InventoryEntity();
                PropertyInventoryProcessBll y = new PropertyInventoryProcessBll();
                x.InventoryCode = key2;
                x.ConnectionString = Common.ConnString;

                DataTable dt = y.GetPropertyInventoryListPrintICSH(Common.ConnString, key2);
                return ExcelReports.ExportToExcelRPCSP(dt, key2, key4);
            }
            else if (key1 == "RPCSP")
            {
                InventoryEntity x = new InventoryEntity();
                PropertyInventoryProcessBll y = new PropertyInventoryProcessBll();
                x.InventoryCode = key2;
                x.ConnectionString = Common.ConnString;

                DataTable dt = y.GetPropertyInventoryListPrintICS(Common.ConnString, key2);
                return ExcelReports.ExportToExcelRPCSP(dt, key2, key4);
            }
            //else if (key1 == "SPC")
            //{
            //    PropertyEntity x = new PropertyEntity();
            //    PropertyCardBll z = new PropertyCardBll();

            //    x.PropertyNumber = key2;
            //    x.ConnectionString = Common.ConnString;
            //    //string propertycarddetails = z.GetPropertyCardByItemCode(Common.ConnString, ExpenseType, PropertyID);


            //    DataTable dt = new DataTable();

            //    return ExcelReports.ExportToExcelSPC(dt);

            //}
            else if (key1 == "SPC" && key3 == "ICS")
            {
                PropertyCardBll z = new PropertyCardBll();
                int ExpenseType = 0;
                if (key5 == "ICS")
                {
                    ExpenseType = 2;
                }
                else
                {
                    ExpenseType = 1;
                }
                string PPE = key4.Split('|')[0];
                string Pnum = key4.Split('|')[1];
                string Description = key4.Split('|')[2];

                DataTable dt = z.GetPropertyCardByItemCode(Common.ConnString, ExpenseType, Convert.ToInt32(key2));

                return ExcelReports.ExportToExcelSPCICS(dt, PPE, Pnum, Description);
            }
            else if (key1 == "SPC" && key3 == "SPC")
            {
                string PPE = key4.Split('|')[0];
                string Pnum = key4.Split('|')[1];
                string Description = key4.Split('|')[2];
                PropertyCardBll z = new PropertyCardBll();

                DataTable dt = z.GetPropertyCard(Common.ConnString, key2);


                return ExcelReports.ExportToExcelSPC(dt, PPE, Pnum, Description, key2);
            }
            else if (key1 == "SPLC")
            {
                LedgerCardEntity x = new LedgerCardEntity();
                SEPBll y = new SEPBll();

                x.PropertyNo = key2;
                x.ConnectionString = Common.ConnString;

                DataTable dt = y.getlists(x);
                //string itemcode = dt.Rows[0]["ItemCode"].ToString();
                //string description = dt.Rows[0]["Description"].ToString();


                //return GenerateSEPLedgerCard(dt, propertyno, itemcode, description);

                return ExcelReports.ExportToExcelSPLC(dt, key2, key4, key5);
            }
            else if (key1 == "RIS" && key3 == "PI")
            {
                PropertyEntity x = new PropertyEntity();
                PropertyIssuanceBll y = new PropertyIssuanceBll();

                x.IssuanceNumber = key2;
                x.ConnectionString = Common.ConnString;

                string issuancedetails = y.PropertyIssuanceDetails(x);
                string employeename = issuancedetails.Split('|')[1];
                string employeedep = issuancedetails.Split('|')[2];
                string employeepos = issuancedetails.Split('|')[3];
                string approvername = issuancedetails.Split('|')[4];
                string transactorname = issuancedetails.Split('|')[5];
                string transactordep = issuancedetails.Split('|')[6];
                string transactorpos = issuancedetails.Split('|')[7];
                string custodian = issuancedetails.Split('|')[8];
                string remarks = issuancedetails.Split('|')[9];
                string issueddate = issuancedetails.Split('|')[10];
                string site = issuancedetails.Split('|')[13];

                string approverpos = issuancedetails.Split('|')[14];
                string issuedby = issuancedetails.Split('|')[15];
                string issuedpos = issuancedetails.Split('|')[16];


                DataTable dt = y.PLPendingUpload(x);

                return ExcelReports.ExportToExcelRISPRequest(dt, key2, employeename, employeedep, employeepos, transactorname, transactordep, transactorpos, issueddate, remarks, custodian, approvername, site, approverpos, issuedby, issuedpos);

            }
            else if (key1 == "RIS" && key3 == "PR")
            {
                PropertyEntity x = new PropertyEntity();
                PropertyIssuanceBll y = new PropertyIssuanceBll();
                PropertyRequestBll z = new PropertyRequestBll();

                x.RequestNumber = key2;
                x.ConnectionString = Common.ConnString;

                string requestdetails = y.PropertyRequestDetails(x);
                string employeename = requestdetails.Split('|')[1];
                string employeedep = requestdetails.Split('|')[2];
                string employeepos = requestdetails.Split('|')[3];
                string approvername = requestdetails.Split('|')[4];
                string transactorname = requestdetails.Split('|')[5];
                string transactordep = requestdetails.Split('|')[6];
                string transactorpos = requestdetails.Split('|')[7];
                string custodian = requestdetails.Split('|')[8];
                string remarks = requestdetails.Split('|')[9];
                string issueddate = requestdetails.Split('|')[10];
                string returndate = requestdetails.Split('|')[12];
                string site = requestdetails.Split('|')[13];
                string approverpos = requestdetails.Split('|')[14];
                string IssuedBy = requestdetails.Split('|')[15];
                string IssuedByPos = requestdetails.Split('|')[16];

                DataTable dt = z.PRLPendingUpload(x);

                return ExcelReports.ExportToExcelRISPRequest(dt, key2, employeename, employeedep, employeepos, "", "", "", issueddate, remarks, custodian, approvername, site, approverpos, IssuedBy, IssuedByPos);

            }
            else if (key1 == "RIS" && key3 == "SI")
            {
                StockEntity x = new StockEntity();
                StockIssuanceBll y = new StockIssuanceBll();

                x.IssuanceNo = key2;
                x.ConnectionString = Common.ConnString;

                string issuancedetails = y.StockIssuanceDetails(x);
                string employeename = issuancedetails.Split('|')[1];
                string employeedep = issuancedetails.Split('|')[2];
                string employeepos = issuancedetails.Split('|')[3];
                string approvername = issuancedetails.Split('|')[4];
                string transactorname = issuancedetails.Split('|')[5];
                string transactordep = issuancedetails.Split('|')[6];
                string transactorpos = issuancedetails.Split('|')[7];
                string custodian = issuancedetails.Split('|')[8];
                string remarks = issuancedetails.Split('|')[9];
                string issueddate = issuancedetails.Split('|')[10];
                string site = issuancedetails.Split('|')[13];
                string approverpos = issuancedetails.Split('|')[14];

                DataTable dt = y.StockDetailsPendingUpload(Common.ConnString, key2);

                return ExcelReports.ExportToExcelRISSIssuance(dt, key2, employeename, employeedep, employeepos, transactorname, transactordep, transactorpos, issueddate, remarks, custodian, approvername, site, approverpos);
            }
            else if (key1 == "RIS" && key3 == "SR")
            {
                StockEntity x = new StockEntity();
                StockRequestBll y = new StockRequestBll();

                x.RequestNo = key2;
                x.ConnectionString = Common.ConnString;

                string requestdetails = y.StockRequestDetails(x);
                string employeename = requestdetails.Split('|')[1];
                string employeedep = requestdetails.Split('|')[2];
                string employeepos = requestdetails.Split('|')[3];
                string approvername = requestdetails.Split('|')[4];
                string transactorname = requestdetails.Split('|')[5];
                string transactordep = requestdetails.Split('|')[6];
                string transactorpos = requestdetails.Split('|')[7];
                string custodian = requestdetails.Split('|')[8];
                string remarks = requestdetails.Split('|')[9];
                string issueddate = requestdetails.Split('|')[10];
                string approverpos = requestdetails.Split('|')[12];
                string site = requestdetails.Split('|')[13];

                DataTable dt = y.StockDetailsPendingUpload(Common.ConnString, key2);

                return ExcelReports.ExportToExcelRISSRequest(dt, key2, employeename, employeedep, employeepos, transactorname, transactordep, transactorpos, issueddate, remarks, custodian, approvername, site, approverpos);
            }
            else if (key1 == "SC")
            {
                string Description = key4.Split('|')[0];
                string ItemCode = key4.Split('|')[1];
                string CriticalLevel = key4.Split('|')[2];
                string UnitOfMeasurement = key4.Split('|')[3];

                StockCardBll z = new StockCardBll();


                DataTable dt = z.GetStockCardV2(Common.ConnString, Convert.ToInt32(key2));

                return ExcelReports.ExportToExcelSC(dt, Description, ItemCode, CriticalLevel, UnitOfMeasurement);
            }
            else if (key1 == "PC" && key3 == "PAR")
            {
                PropertyCardBll z = new PropertyCardBll();
                int ExpenseType = 0;
                if (key5 == "PAR")
                {
                    ExpenseType = 1;
                }
                else
                {
                    ExpenseType = 2;
                }
                string PPE = key4.Split('|')[0];
                string Pnum = key4.Split('|')[1];
                string Description = key4.Split('|')[2];

                DataTable dt = z.GetPropertyCardByItemCode(Common.ConnString, ExpenseType, Convert.ToInt32(key2));

                return ExcelReports.ExportToExcelPCPAR(dt, PPE, Pnum, Description);
            }

            //PROPERTY CARD
            else if (key1 == "PC" && key3 == "PC")
            {
                PropertyCardBll z = new PropertyCardBll();
                string PPE = key4.Split('|')[0];
                string Pnum = key2;
                string Description = key4.Split('|')[2];

                DataTable dt = z.GetPropertyCard(Common.ConnString, key2);

                return ExcelReports.ExportToExcelPC(dt, PPE, Pnum, Description);
            }
            else if (key1 == "PAR" && key3 == "PTransfer")
            {
                PropertyEntity x = new PropertyEntity();
                PropertyTransferBll y = new PropertyTransferBll();

                string transferdetails = y.PropertyTransferDetailsView(Common.ConnString, key2);

                string approvername = transferdetails.Split('|')[1];
                string approveremail = transferdetails.Split('|')[2];
                string transactorname = transferdetails.Split('|')[3];
                string transactoremail = transferdetails.Split('|')[4];
                string custodian = transferdetails.Split('|')[5];
                string remarks = transferdetails.Split('|')[6];
                string createddate = transferdetails.Split('|')[7];
                string fromsitename = transferdetails.Split('|')[8];
                string fromlocationname = transferdetails.Split('|')[9];
                string fromareaname = transferdetails.Split('|')[10];
                string tositename = transferdetails.Split('|')[11];
                string tolocationname = transferdetails.Split('|')[12];
                string toareaname = transferdetails.Split('|')[13];
                string transactordep = transferdetails.Split('|')[14];
                string transactorpos = transferdetails.Split('|')[15];
                string approverdep = transferdetails.Split('|')[16];
                string approverpos = transferdetails.Split('|')[17];
                string issueddate = transferdetails.Split('|')[18];
                string employeename = transferdetails.Split('|')[19];
                string employeepos = transferdetails.Split('|')[20];
                string fromemp = transferdetails.Split('|')[21];
                string fromemppos = transferdetails.Split('|')[24];
                string fromempdep = transferdetails.Split('|')[25];

                string employeedep = "";

                string fromloc = fromsitename + " - " + fromlocationname + " - " + fromareaname;
                string toloc = tositename + " - " + tolocationname + " - " + toareaname;
                DataTable dt = y.PLPendingApprovalWithComponent(Common.ConnString, key2);

                return ExcelReports.ExportToExcelPARTransfer(dt, key2, employeename, employeedep, employeepos, transactorname, transactordep, transactorpos, issueddate, remarks, custodian, approvername, fromemp, fromemppos, fromempdep);
            }
            else if (key1 == "PAR" && key3 == "PIssuance")
            {
                PropertyEntity x = new PropertyEntity();
                PropertyIssuanceBll y = new PropertyIssuanceBll();

                x.IssuanceNumber = key2;
                x.ConnectionString = Common.ConnString;

                string issuancedetails = y.PropertyIssuanceDetails(x);
                string employeename = issuancedetails.Split('|')[1];
                string employeedep = issuancedetails.Split('|')[2];
                string employeepos = issuancedetails.Split('|')[3];
                string approvername = issuancedetails.Split('|')[4];
                string transactorname = issuancedetails.Split('|')[5];
                string transactordep = issuancedetails.Split('|')[6];
                string transactorpos = issuancedetails.Split('|')[7];
                string custodian = issuancedetails.Split('|')[8];
                string remarks = issuancedetails.Split('|')[9];
                string issueddate = issuancedetails.Split('|')[10];

                DataTable dt = y.PLPendingUploadWithComponent(x);

                return ExcelReports.ExportToExcelPAR(dt, key2, employeename, employeedep, employeepos, transactorname, transactordep, transactorpos, issueddate, remarks, custodian, approvername);
            }
            else if (key1 == "PAR" && key3 == "PRequest")
            {
                PropertyEntity x = new PropertyEntity();
                PropertyIssuanceBll y = new PropertyIssuanceBll();
                PropertyRequestBll z = new PropertyRequestBll();

                x.RequestNumber = key2;
                x.ConnectionString = Common.ConnString;

                string requestdetails = y.PropertyRequestDetails(x);
                string employeename = requestdetails.Split('|')[1];
                string employeedep = requestdetails.Split('|')[2];
                string employeepos = requestdetails.Split('|')[3];
                string approvername = requestdetails.Split('|')[4];
                string transactorname = "";
                string transactordep = "";
                string transactorpos = "";
                string custodian = requestdetails.Split('|')[5];
                string remarks = requestdetails.Split('|')[6];
                string issueddate = requestdetails.Split('|')[7];
                string returndate = requestdetails.Split('|')[9];

                DataTable dt = z.PRLPendingUpload(x);


                return ExcelReports.ExportToExcelPAR(dt, key2, employeename, employeedep, employeepos, transactorname, transactordep, transactorpos, issueddate, remarks, custodian, approvername);
            }
            else if (key1 == "PPELC")
            {
                LedgerCardEntity x = new LedgerCardEntity();
                PPEBll y = new PPEBll();

                x.PropertyNo = key2;
                x.ConnectionString = Common.ConnString;

                DataTable dt = y.getlists(x);

                return ExcelReports.ExportToExcelPPELC(dt, key2);
            }
            else if (key1 == "RSPI")
            {
                RSPIEntity x = new RSPIEntity();
                RSPIBll y = new RSPIBll();

                x.PropertyNo = key2;
                x.ConnectionString = Common.ConnString;

                DataTable dt = y.getlists(x, key4, key5);


                return ExcelReports.ExportToExcelRSPI(dt, key2);
            }
            else if (key1 == "PPELC")
            {
                string propertyno = "";
                string itemcode = "";
                string description = "";

                DataTable dt = new DataTable();

                return ExcelReports.ExportToExcelSPLC(dt, propertyno, itemcode, description);
            }
            else if (key1 == "GP")
            {
                PropertyEntity x = new PropertyEntity();
                PropertyGatePassBll y = new PropertyGatePassBll();

                x.GatePassNumber = key2;
                x.ConnectionString = Common.ConnString;

                string gatepassdetails = y.PropertyGatePassDetails(x);
                string accountablename = gatepassdetails.Split('|')[1];
                string approvername = gatepassdetails.Split('|')[2];
                string remarks = gatepassdetails.Split('|')[3];
                string createddate = gatepassdetails.Split('|')[4];

                DataTable dt = y.PropertyGatePassRequestList(Common.ConnString, key2);

                return ExcelReports.ExportToExcelGP(dt, key2, accountablename, approvername);
            }
            else if (key1 == "IPL")
            {
                InventoryEntity x = new InventoryEntity();
                PropertyInventoryProcessBll y = new PropertyInventoryProcessBll();
                PropertyBll Z = new PropertyBll();
                x.InventoryCode = key2;
                x.ConnectionString = Common.ConnString;


                DataTable dt = y.GetPropertyInventoryListPrint(Common.ConnString, key2);
                if (dt.Rows.Count > 0)
                {
                    int countF = Z.getCountFound(key2, Common.ConnString);
                    int countNF = Z.getCountMissing(key2, Common.ConnString);
                    int countM = Z.getCountMisplaced(key2, Common.ConnString);
                    string amount = Z.getCountAcquiredCost(key2, Common.ConnString);
                    return ExcelReports.ExportToExcelIPR(dt, key2, countF, countNF, countM, BasePage.ConvertToMoneyFormat(amount));
                }
                else
                {
                    return ExcelReports.ExportToExcelNoRecordsFound("Inventory Property List");
                }
            }
            else if (key1 == "IPLF")
            {
                InventoryEntity x = new InventoryEntity();
                PropertyInventoryProcessBll y = new PropertyInventoryProcessBll();
                PropertyBll Z = new PropertyBll();
                x.InventoryCode = key2;
                x.ConnectionString = Common.ConnString;


                DataTable dt = y.GetPropertyInventoryListPrintFOUND(Common.ConnString, key2);
                if (dt.Rows.Count > 0)
                {
                    int count = Z.getCountFound(key2, Common.ConnString);
                    string amount = Z.getCountAcquiredCostFound(key2, Common.ConnString);
                    return ExcelReports.ExportToExcelIPRFound(dt, key2, count, BasePage.ConvertToMoneyFormat(amount));
                }
                else
                {
                    return ExcelReports.ExportToExcelNoRecordsFound("Inventory Property List");
                }
            }
            else if (key1 == "IPLNF")
            {
                InventoryEntity x = new InventoryEntity();
                PropertyInventoryProcessBll y = new PropertyInventoryProcessBll();
                PropertyBll Z = new PropertyBll();
                x.InventoryCode = key2;
                x.ConnectionString = Common.ConnString;


                DataTable dt = y.GetPropertyInventoryListPrintNOTFOUND(Common.ConnString, key2);
                if (dt.Rows.Count > 0)
                {
                    int count = Z.getCountMissing(key2, Common.ConnString);
                    string amount = Z.getCountAcquiredCostNotFound(key2, Common.ConnString);
                    return ExcelReports.ExportToExcelIPRNotFound(dt, key2, count, BasePage.ConvertToMoneyFormat(amount));
                }
                else
                {
                    return ExcelReports.ExportToExcelNoRecordsFound("Inventory Property List");
                }
            }
            else if (key1 == "IPLM")
            {
                InventoryEntity x = new InventoryEntity();
                PropertyInventoryProcessBll y = new PropertyInventoryProcessBll();
                PropertyBll Z = new PropertyBll();
                x.InventoryCode = key2;
                x.ConnectionString = Common.ConnString;


                DataTable dt = y.GetPropertyInventoryListPrintMISPLACED(Common.ConnString, key2);
                if (dt.Rows.Count > 0)
                {
                    int count = Z.getCountMisplaced(key2, Common.ConnString);
                    string amount = Z.getCountAcquiredCostMisplaced(key2, Common.ConnString);
                    return ExcelReports.ExportToExcelIPRMisplaced(dt, key2, count, BasePage.ConvertToMoneyFormat(amount));
                }
                else
                {
                    return ExcelReports.ExportToExcelNoRecordsFound("Inventory Property List");
                }
            }
            else if (key1 == "ICF")
            {
                InventoryEntity x = new InventoryEntity();
                PropertyInventoryProcessBll y = new PropertyInventoryProcessBll();
                PropertyBll Z = new PropertyBll();
                x.InventoryCode = key2;
                x.ConnectionString = Common.ConnString;


                DataTable dt = y.GetPropertyInventoryListPrint(Common.ConnString, key2);
                //DataTable dt1 = x.GetInventoryList(Common.ConnString, key2);

                if (dt.Rows.Count > 0)
                {
                    return ExcelReports.ExportToExcelICF(dt, key2, key3);
                }
                else
                {
                    return ExcelReports.ExportToExcelNoRecordsFound("Inventory Count");
                }
            }
            else if (key1 == "ELCard")
            {
                RSPIEntity x = new RSPIEntity();

                // DataTable dt = new DataTable(); /*y.getlists(Common.ConnString, Convert.ToInt32(key3), key4, key5);*/
                PropertyBll y = new PropertyBll();
                DataTable dt = y.GetITemList(Common.ConnString, key2);
                DataTable dt1 = new DataTable(); /*y.getlists1(Common.ConnString, Convert.ToInt32(key3), key4, key5);*/
                return ExcelReports.ExportToExcelELC(dt, dt1);
            }

            else if (key1 == "GSISForm")
            {
                PropertyEntity x = new PropertyEntity();

                // DataTable dt = new DataTable(); /*y.getlists(Common.ConnString, Convert.ToInt32(key3), key4, key5);*/
                PropertyBll y = new PropertyBll();
                DataTable dt = y.GetITemListGSISForm(Common.ConnString, key2, key3, Convert.ToBoolean(key4));
                DataTable dt1 = new DataTable(); /*y.getlists1(Common.ConnString, Convert.ToInt32(key3), key4, key5);*/
                return ExcelReports.ExportToExcelGSISForm(dt);
            }

            return "";
        }
        catch (Exception ex)
        {
            string msg = ex.Message.ToString();
            return "error";
        }
    }

}