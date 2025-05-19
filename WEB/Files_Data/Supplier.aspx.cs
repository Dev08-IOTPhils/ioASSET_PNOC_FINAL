using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Data;
using AdvanceEntity;
using AdvanceBll;
using System.Web.Script.Serialization;

public partial class Files_Data_Supplier : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        CheckAccess();
        //SupplierList.InnerHtml = GetSupplier(1);
    }

    public static string ViewAccess = "";
    public static string AddAccess = "";
    public static string EditAccess = "";
    public static string DeleteAccess = "";
    public void CheckAccess()
    {
        RoleBll rb = new RoleBll();

        string AccessButon = rb.CheckR("Files_Data/Supplier.aspx", Convert.ToInt32(Common.Role), Common.ConnString);
        string[] ButtonAccess = AccessButon.Split('|');
        ViewAccess = ButtonAccess[0];
        AddAccess = ButtonAccess[1];
        EditAccess = ButtonAccess[2];
        DeleteAccess = ButtonAccess[3];
    }

    [WebMethod]
    public static string SupplierByID(int id)
    {
        SupplierEntity supplierEntity = new SupplierEntity();
        SupplierBll supplierBll = new SupplierBll();

        supplierEntity.SupplierID = id;
        supplierEntity.ConnectionString = Common.ConnString;

        return supplierBll.GetSupplierByID(supplierEntity);
    }

    [WebMethod]
    public static string GetSupplier()
    {
        SupplierBll supplierBll = new SupplierBll();
        string html = string.Empty;
        List<SupplierEntity> JsonItemList = new List<SupplierEntity>();
        try
        {
            DataTable dt = supplierBll.GetSupplier(Common.ConnString);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    SupplierEntity x = new SupplierEntity();
                    x.Supplier = dt.Rows[i]["SupplierName"].ToString();
                    x.ContactNumber = dt.Rows[i]["ContactNumber"].ToString();
                    x.Email = dt.Rows[i]["Email"].ToString();

                    x.IsActive = "<img " + ((bool.Parse(dt.Rows[i]["Active"].ToString()) == true ? "src = '../Image/Icon/yes.png'" : "src = '../Image/Icon/no.png'")) + "/>";
                    if (DeleteAccess == "0")
                    {
                        x.Action = " <center><button class='btn btn-xs btn-warning btn_Edit' type='button' data-toggle='tooltip'  title='Edit Supplier Information' onclick=\"GetSupplierID('" + dt.Rows[i]["SupplierID"] + "')\"><span class='fa fa-edit'></span></button>  </center>";

                    }
                    else
                    {
                        x.Action = " <center><button class='btn btn-xs btn-warning btn_Edit' type='button' data-toggle='tooltip'  title='Edit Supplier Information' onclick=\"GetSupplierID('" + dt.Rows[i]["SupplierID"] + "')\"><span class='fa fa-edit'></span></button> "
                          + " <button type='button' class='btn btn-xs btn-danger btn_Delete btn_DeleteSupplier' data-toggle='tooltip' title='Delete Supplier' onclick=\"deleteSupplier('" + dt.Rows[i]["SupplierID"] + "')\"><span class='fa fa-trash'></span></button> </center>";

                    }
                    //    " <a href='' class='btn btn-xs btn-danger btn_del'><span class='fa fa-times'></span></a> " +
                    //    " <a href='' class='btn btn-xs btn-warning btn_deliverAll'><span class='fa fa-truck'></span></a>";
                    //x.Email = dt.Rows[i]["Email"].ToString();
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
    public static string DeleteSupplier(int id)
    {

        SupplierEntity supplierEntity = new SupplierEntity();
        SupplierBll supplierBll = new SupplierBll();

        supplierEntity.SupplierID = id;
        supplierEntity.CreatedBy = Common.UserName;
        supplierEntity.ConnectionString = Common.ConnString;

        return supplierBll.DeleteSupplier(supplierEntity).ToString();

    }
}