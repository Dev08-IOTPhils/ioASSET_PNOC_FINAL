using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AdvanceBll;
using AdvanceEntity;
using System.Web.Services;
using System.Web.Security;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Configuration;

public partial class SignIn : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public string GetImages()
    {
        string[] files = Directory.GetFiles(System.Web.HttpContext.Current.Server.MapPath("~/Image/IOT"));
        string html = "";
        html += "";
        int i = 1;
        foreach (string name in files)
        {
            int j = name.Split('\\').Length;
            string[] file = name.Split('\\');
            html += "<div data-b='" + i + "'><img class='mySlides' src='../Image/IOT/" + file[j - 1] + "' style='height:500px;width:700px;'></div>";
            i++;
        }
        html += "";

        return html;
    }
}