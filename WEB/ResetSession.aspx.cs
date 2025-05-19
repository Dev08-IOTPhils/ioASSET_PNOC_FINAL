using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ResetSession : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["Reset"] = true;
        Configuration config = WebConfigurationManager.OpenWebConfiguration("~/Web.Config");
        SessionStateSection section = (SessionStateSection)config.GetSection("system.web/sessionState");
        int timeout = (int)section.Timeout.TotalMinutes * 1000 * 60;
        ClientScript.RegisterStartupScript(this.GetType(), "SessionAlert", "SessionExpireAlert(" + timeout + ");", true);
    }

    [WebMethod(EnableSession = true)]
    public static int RefreshSession()
    {
        HttpContext.Current.Session["Reset"] = true;
        Configuration config = WebConfigurationManager.OpenWebConfiguration("~/Web.Config");
        SessionStateSection section = (SessionStateSection)config.GetSection("system.web/sessionState");
        int timeout = (int)section.Timeout.TotalMinutes * 1000 * 60;
        return timeout;
    }

    protected void ResetSession1(object sender, EventArgs e)
    {
        string name = Session["Reset"].ToString();
    }
}