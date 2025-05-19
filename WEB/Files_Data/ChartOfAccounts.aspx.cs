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

public partial class Files_Data_ChartOfAccounts :BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        CheckAccess();
           AccountHeader();
    }
    public static string ViewAccess = "";
    public static string AddAccess = "";
    public static string EditAccess = "";
    public static string DeleteAccess = "";
    public void CheckAccess()
    {
        RoleBll rb = new RoleBll();

        string AccessButon = rb.CheckR("Files_Data/ChartOfAccounts.aspx", Convert.ToInt32(Common.Role), Common.ConnString);
        string[] ButtonAccess = AccessButon.Split('|');
        ViewAccess = ButtonAccess[0];
        AddAccess = ButtonAccess[1];
        EditAccess = ButtonAccess[2];
        DeleteAccess = ButtonAccess[3];
    }
    private void AccountHeader()
    {
        ddlAccountHeader.Items.Clear();
        ddlAccountHeader.Items.Insert(0, "--- Select Account Header ---");
        ddlAccountHeader.Items.Insert(1, "Asset");
        ddlAccountHeader.Items.Insert(2, "Liabilities");
        ddlAccountHeader.Items.Insert(3, "Equity");
        ddlAccountHeader.Items.Insert(4, "Expense");
        ddlAccountHeader.Items.Insert(5, "Cost Of Sales");
        ddlAccountHeader.Items.Insert(6, "Income");
        ddlAccountHeader.Items.Insert(7, "Other Expense");
        ddlAccountHeader.Items.Insert(8, "Other Income");
    }

    [WebMethod]
    public static string SaveChartOfAccounts(int id, string accountnumber, string description, string accountheader, decimal balance, bool active)
    {
        ChartOfAccountsEntity coaEntity = new ChartOfAccountsEntity();
        ChartOfAccountsBll ChartOfAccountsBll = new ChartOfAccountsBll();

        coaEntity.ChartOfAccountsID = id;
        coaEntity.AccountNumber = accountnumber;
        coaEntity.Description = description;
        coaEntity.AccountHeader = accountheader;
        coaEntity.Balance = balance;
        coaEntity.Active = active;
        coaEntity.CreatedBy = Common.UserName;
        coaEntity.ConnectionString = Common.ConnString;

        return ChartOfAccountsBll.SaveChartOfAccounts(coaEntity).ToString();

    }

    [WebMethod]
    public static string ChartOfAccountsByID(int id)
    {
        ChartOfAccountsEntity coaEntity = new ChartOfAccountsEntity();
        ChartOfAccountsBll ChartOfAccountsBll = new ChartOfAccountsBll();

        coaEntity.ChartOfAccountsID = id;
        coaEntity.ConnectionString = Common.ConnString;

        return ChartOfAccountsBll.ChartOfAccountsbyID(coaEntity);
    }


    [WebMethod]
    public static string GetChartOfAccounts()
    {
        ChartOfAccountsBll ChartOfAccountsBll = new ChartOfAccountsBll();
        string html = string.Empty;
        List<ChartOfAccountsEntity> JsonItemList = new List<ChartOfAccountsEntity>();
        try
        {
            DataTable dt = ChartOfAccountsBll.GetChartOfAccounts(Common.ConnString);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ChartOfAccountsEntity x = new ChartOfAccountsEntity();
                    x.AccountNumber = dt.Rows[i]["AccountNumber"].ToString();
                    x.Description = dt.Rows[i]["Description"].ToString();
                    x.AccountHeader = dt.Rows[i]["AccountHeader"].ToString();
                    x.StringBalance = BasePage.ConvertToMoneyFormat(dt.Rows[i]["Balance"].ToString());

                    x.IsActive = "<img " + ((bool.Parse(dt.Rows[i]["Active"].ToString()) == true ? "src = '../Image/Icon/yes.png'" : "src = '../Image/Icon/no.png'")) + "/>";
                    if (DeleteAccess == "0")
                    {
                        x.Action = " <center><button class='btn btn-xs btn-warning btn_Edit' type='button' data-toggle='tooltip'  title='Edit ChartOfAccounts Information' onclick=\"GetChartOfAccountsID('" + dt.Rows[i]["ChartOfAccountsID"] + "')\"><span class='fa fa-edit'></span></button></center>";

                    }
                    else
                    {
                        x.Action = " <center><button class='btn btn-xs btn-warning btn_Edit' type='button' data-toggle='tooltip'  title='Edit ChartOfAccounts Information' onclick=\"GetChartOfAccountsID('" + dt.Rows[i]["ChartOfAccountsID"] + "')\"><span class='fa fa-edit'></span></button> "
                          + " <button type='button' class='btn btn-xs btn-danger btn_Delete btn_DeleteChartOfAccounts' data-toggle='tooltip' title='Delete ChartOfAccounts' onclick=\"deleteChartOfAccounts('" + dt.Rows[i]["ChartOfAccountsID"] + "')\"><span class='fa fa-trash'></span></button> </center>";

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
    public static string DeleteChartOfAccounts(int id)
    {
        ChartOfAccountsEntity coaEntity = new ChartOfAccountsEntity();
        ChartOfAccountsBll ChartOfAccountsBll = new ChartOfAccountsBll();

        coaEntity.ChartOfAccountsID = id;
        coaEntity.CreatedBy = Common.UserName;
        coaEntity.ConnectionString = Common.ConnString;

        return ChartOfAccountsBll.DeleteChartOfAccounts(coaEntity).ToString();
        
    }
}