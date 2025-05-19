using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Web.Services;
using AdvanceBll;
using AdvanceEntity;

public partial class Home_Notification : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ReadAllNotification();

    }

    [WebMethod]
    public static string GetUserInfo()
    {
        string userinfo = Common.Administrative + "|" + Common.IsApprover + "|" + Common.IsCertifier + "|" + Common.IsInspector + "|" + Common.IsCustodian;
        return userinfo;
    }
    [WebMethod]
    public static string GetNotification()
    {
        var res = "";

        NotificationBll x = new NotificationBll();
        DataTable dt = x.GetNotification(Common.ConnString, Common.UserID, "");

        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                res += dt.Rows[i]["NotificationID"].ToString() + "|";
                res += dt.Rows[i]["ToUser"].ToString() + "|";
                res += dt.Rows[i]["Name"].ToString() + "|";
                res += dt.Rows[i]["Notif"].ToString() + "|";
                res += dt.Rows[i]["Date"].ToString() + "|";
                res += dt.Rows[i]["IsDone"].ToString() + "|";
                res += dt.Rows[i]["Image"].ToString() + "|";
                res += dt.Rows[i]["Trx"].ToString() + "|";
                res += dt.Rows[i]["Link"].ToString() + "|";
                res += dt.Rows[i]["IsRead"].ToString();
                res += "\n";
            }

            return res;
        }
        else { return ""; }
    }




    [WebMethod]
    public static string ReadNotification(int NotifID)
    {

        NotificationBll x = new NotificationBll();
        var res = "";

        try
        {
            res = x.ReadNotification(Common.ConnString, NotifID, Common.UserID);

            return res;
        }
        catch (Exception ex)
        {
            return ex.Message;
            throw;
        }
    }

    [WebMethod]
    public static string ReadAllNotification()
    {
        NotificationBll x = new NotificationBll();
        var res = "";
        try
        {
            res = x.ReadALLNotification(Common.ConnString,  Common.UserID);

            return res;
        }
        catch (Exception ex)
        {
            return ex.Message;
            throw;
        }

       
    }

   

}
