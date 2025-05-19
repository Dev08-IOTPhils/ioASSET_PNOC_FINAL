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

public partial class Files_Data_Custodian : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        CheckAccess();
    }
    public static string ViewAccess = "";
    public static string AddAccess = "";
    public static string EditAccess = "";
    public static string DeleteAccess = "";
    public void CheckAccess()
    {
        RoleBll rb = new RoleBll();

        string AccessButon = rb.CheckR("Files_Data/Custodian.aspx", Convert.ToInt32(Common.Role), Common.ConnString);
        string[] ButtonAccess = AccessButon.Split('|');
        ViewAccess = ButtonAccess[0];
        AddAccess = ButtonAccess[1];
        EditAccess = ButtonAccess[2];
        DeleteAccess = ButtonAccess[3];
    }
    [WebMethod]
    public static string SaveCustodian(int id, string custodian, string description, bool active)
    {
        CustodianEntity custodianEntity = new CustodianEntity();
        CustodianBll custodianBll = new CustodianBll();

        custodianEntity.CustodianID = id;
        custodianEntity.Custodian = custodian;
        custodianEntity.Description = description;
        custodianEntity.Active = active;
        custodianEntity.CreatedBy = Common.UserName;
        custodianEntity.ConnectionString = Common.ConnString;

        return custodianBll.SaveCustodian(custodianEntity).ToString();

    }

    [WebMethod]
    public static string CustodianByID(int id)
    {
        CustodianEntity custodianEntity = new CustodianEntity();
        CustodianBll custodianBll = new CustodianBll();

        custodianEntity.CustodianID = id;
        custodianEntity.ConnectionString = Common.ConnString;

        return custodianBll.CustodianbyID(custodianEntity);
    }


    [WebMethod]
    public static string GetCustodian()
    {
        CustodianBll custodianBll = new CustodianBll();
        string html = string.Empty;
        List<CustodianEntity> JsonItemList = new List<CustodianEntity>();
        try
        {
            DataTable dt = custodianBll.GetCustodian(Common.ConnString);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    CustodianEntity x = new CustodianEntity();
                    x.Custodian = dt.Rows[i]["Custodian"].ToString();
                    x.Description = dt.Rows[i]["Description"].ToString();

                    x.IsActive = "<img " + ((bool.Parse(dt.Rows[i]["Active"].ToString()) == true ? "src = '../Image/Icon/yes.png'" : "src = '../Image/Icon/no.png'")) + "/>";
                    if (DeleteAccess == "0")
                    {
                        x.Action = "";
                    }
                    else
                    {

                        x.Action = " <center><button class='btn btn-xs btn-warning btn_Edit' type='button' data-toggle='tooltip'  title='Edit Custodian Information' onclick=\"GetCustodianID('" + dt.Rows[i]["CustodianID"] + "')\"><span class='fa fa-edit'></span></button> "
                            + " <button type='button' class='btn btn-xs btn-danger btn_Delete btn_DeleteCustodian' data-toggle='tooltip' title='Delete Custodian' onclick=\"deleteCustodian('" + dt.Rows[i]["CustodianID"] + "')\"><span class='fa fa-trash'></span></button> </center>";
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
    public static string DeleteCustodian(int id)
    {
        CustodianEntity custodianEntity = new CustodianEntity();
        CustodianBll custodianBll = new CustodianBll();

        custodianEntity.CustodianID = id;
        custodianEntity.CreatedBy = Common.UserName;
        custodianEntity.ConnectionString = Common.ConnString;

        return custodianBll.DeleteCustodian(custodianEntity).ToString();
        
    }
}