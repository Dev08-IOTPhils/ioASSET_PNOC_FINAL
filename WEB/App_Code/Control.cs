using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Control
/// </summary>
public class Control
{
    public static class MyConverter
    {
        public static string ConvertDataTabletoJSON(DataTable dt)
        {

            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
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
    }
    public static class Paging
    {
        public static string Style0(int pageNo, int totalPage)
        {
            string html = "";

            if (pageNo > 1)
            {
                html += "<span class=\"glyphicon glyphicon-step-backward\" title=\"First\" style=\"display:inline-block; vertical-align:middle; cursor:pointer\" onclick=\"Pagination(1);\" ></span>&nbsp;"
                    + "<span class=\"glyphicon glyphicon-triangle-left\" title=\"Previous\" style=\"display:inline-block; vertical-align:middle; cursor:pointer\" onclick=\"Pagination(" + (pageNo - 1).ToString() + ");\" ></span>&nbsp";
            }

            html += "<span>"
                + "<select id=\"ddlPaging\" style=\"font-size:11px;vertical-align:middle;\" onchange=\"Pagination( $('#ddlPaging').val() )\">";

            for (int ctr = 1; ctr <= totalPage; ctr++)
            {
                html += "<option " + (pageNo == ctr ? "selected" : "") + " value=\"" + ctr + "\">" + ctr + "</option>";
            }

            html += "</select><label><h6><strong> of " + totalPage + "</strong></h6></label>"
                + "</span>&nbsp;";


            if (pageNo < totalPage)
            {
                html += "<span class=\"glyphicon glyphicon-triangle-right\" title=\"Next\" style=\"display:inline-block; vertical-align:middle; cursor:pointer\" onclick=\"Pagination(" + (pageNo + 1).ToString() + ");\" ></span>&nbsp;"
                    + "<span class=\"glyphicon glyphicon-step-forward\" title=\"Last\" style=\"display:inline-block; vertical-align:middle; cursor:pointer\" onclick=\"Pagination(" + totalPage.ToString() + ");\" ></span>";
            }

            return html;
        }

        public static string Style4(int pageNo, int totalPage)
        {
            string html = "";

            if (pageNo > 1)
            {
                html += "<span class=\"glyphicon glyphicon-step-backward\" title=\"First\" style=\"display:inline-block; vertical-align:middle; cursor:pointer\" onclick=\"Pagination(1);\" ></span>&nbsp;"
                    + "<span class=\"glyphicon glyphicon-triangle-left\" title=\"Previous\" style=\"display:inline-block; vertical-align:middle; cursor:pointer\" onclick=\"Pagination(" + (pageNo - 1).ToString() + ");\" ></span>&nbsp";
            }

            html += "<span>"
                + "<select id=\"ddlPaging\" style=\"font-size:11px;vertical-align:middle;\" onchange=\"Pagination( $('#ddlPaging').val() )\">";

            for (int ctr = 1; ctr <= totalPage; ctr++)
            {
                html += "<option " + (pageNo == ctr ? "selected" : "") + " value=\"" + ctr + "\">" + ctr + "</option>";
            }

            html += "</select><label><h6><strong> of " + totalPage + "</strong></h6></label>"
                + "</span>&nbsp;";


            if (pageNo < totalPage)
            {
                html += "<span class=\"glyphicon glyphicon-triangle-right\" title=\"Next\" style=\"display:inline-block; vertical-align:middle; cursor:pointer\" onclick=\"Pagination(" + (pageNo + 1).ToString() + ");\" ></span>&nbsp;"
                    + "<span class=\"glyphicon glyphicon-step-forward\" title=\"Last\" style=\"display:inline-block; vertical-align:middle; cursor:pointer\" onclick=\"Pagination(" + totalPage.ToString() + ");\" ></span>";
            }

            return html;
        }

        public static string Style2(int pageNo, int totalPage, string accountCategory)
        {
            string html = "";

            if (pageNo > 1)
            {
                html += "<span class=\"glyphicon glyphicon-step-backward\" title=\"First\" style=\"display:inline-block; vertical-align:middle; cursor:pointer\" onclick=\"Pagination(1,'" + accountCategory + "');\" ></span>&nbsp;"
                    + "<span class=\"glyphicon glyphicon-triangle-left\" title=\"Previous\" style=\"display:inline-block; vertical-align:middle; cursor:pointer\" onclick=\"Pagination(" + (pageNo - 1).ToString() + ",'" + accountCategory + "');\" ></span>&nbsp";
            }

            html += "<span>"
                + "<select id=\"ddlPaging" + accountCategory + "\" style=\"font-size:11px;vertical-align:middle;\" onchange=\"Pagination( $('#ddlPaging" + accountCategory + "').val(),'" + accountCategory + "' )\">";

            for (int ctr = 1; ctr <= totalPage; ctr++)
            {
                html += "<option " + (pageNo == ctr ? "selected" : "") + " value=\"" + ctr + "\">" + ctr + "</option>";
            }

            html += "</select><label> of " + totalPage + "</label>"
                + "</span>&nbsp;";


            if (pageNo < totalPage)
            {
                html += "<span class=\"glyphicon glyphicon-triangle-right\" title=\"Next\" style=\"display:inline-block; vertical-align:middle; cursor:pointer\" onclick=\"Pagination(" + (pageNo + 1).ToString() + ",'" + accountCategory + "');\" ></span>&nbsp;"
                    + "<span class=\"glyphicon glyphicon-step-forward\" title=\"Last\" style=\"display:inline-block; vertical-align:middle; cursor:pointer\" onclick=\"Pagination(" + totalPage.ToString() + ",'" + accountCategory + "');\" ></span>";
            }

            return html;
        }


        public static string Style3(int pageNo, int totalPage, string customer)
        {
            string html = "";

            if (pageNo > 1)
            {
                html += "<span class=\"glyphicon glyphicon-step-backward\" title=\"First\" style=\"display:inline-block; vertical-align:middle; cursor:pointer\" onclick=\"Pagination(1);\" ></span>&nbsp;"
                    + "<span class=\"glyphicon glyphicon-triangle-left\" title=\"Previous\" style=\"display:inline-block; vertical-align:middle; cursor:pointer\" onclick=\"Pagination(" + (pageNo - 1).ToString() + ");\" ></span>&nbsp";
            }

            html += "<span>"
                + "<select id=\"ddlPaging\" style=\"font-size:11px;vertical-align:middle;\" onchange=\"Pagination( $('#ddlPaging').val() )\">";

            for (int ctr = 1; ctr <= totalPage; ctr++)
            {
                html += "<option " + (pageNo == ctr ? "selected" : "") + " value=\"" + ctr + "\">" + ctr + "</option>";
            }

            html += "</select><label><h6><strong> of " + totalPage + "</strong></h6></label>"
                + "</span>&nbsp;";


            if (pageNo < totalPage)
            {
                html += "<span class=\"glyphicon glyphicon-triangle-right\" title=\"Next\" style=\"display:inline-block; vertical-align:middle; cursor:pointer\" onclick=\"Pagination(" + (pageNo + 1).ToString() + ");\" ></span>&nbsp;"
                    + "<span class=\"glyphicon glyphicon-step-forward\" title=\"Last\" style=\"display:inline-block; vertical-align:middle; cursor:pointer\" onclick=\"Pagination(" + totalPage.ToString() + ");\" ></span>";
            }

            return html;
        }


        public static string Style100(int pageNo, int totalPage)
        {
            string html = "";

            if (pageNo > 1)
            {
                html += "<span class=\"glyphicon glyphicon-step-backward\" title=\"First\" style=\"display:inline-block; vertical-align:middle; cursor:pointer\" onclick=\"pahina(1);\" ></span>&nbsp;"
                    + "<span class=\"glyphicon glyphicon-triangle-left\" title=\"Previous\" style=\"display:inline-block; vertical-align:middle; cursor:pointer\" onclick=\"pahina(" + (pageNo - 1).ToString() + ");\" ></span>&nbsp";
            }

            html += "<span>"
                + "<select id=\"ddlpahina\" style=\"font-size:11px;vertical-align:middle;\" onchange=\"pahina( $('#ddlpahina').val() )\">";

            for (int ctr = 1; ctr <= totalPage; ctr++)
            {
                html += "<option " + (pageNo == ctr ? "selected" : "") + " value=\"" + ctr + "\">" + ctr + "</option>";
            }

            html += "</select><label><h6><strong> of " + totalPage + "</strong></h6></label>"
                + "</span>&nbsp;";


            if (pageNo < totalPage)
            {
                html += "<span class=\"glyphicon glyphicon-triangle-right\" title=\"Next\" style=\"display:inline-block; vertical-align:middle; cursor:pointer\" onclick=\"pahina(" + (pageNo + 1).ToString() + ");\" ></span>&nbsp;"
                    + "<span class=\"glyphicon glyphicon-step-forward\" title=\"Last\" style=\"display:inline-block; vertical-align:middle; cursor:pointer\" onclick=\"pahina(" + totalPage.ToString() + ");\" ></span>";
            }

            return html;
        }

        public static string Style101(int pageNo, int totalPage)
        {
            string html = "";

            if (pageNo > 1)
            {
                html += "<span class=\"glyphicon glyphicon-step-backward\" title=\"First\" style=\"display:inline-block; vertical-align:middle; cursor:pointer\" onclick=\"Pagination1(1);\" ></span>&nbsp;"
                    + "<span class=\"glyphicon glyphicon-triangle-left\" title=\"Previous\" style=\"display:inline-block; vertical-align:middle; cursor:pointer\" onclick=\"Pagination1(" + (pageNo - 1).ToString() + ");\" ></span>&nbsp";
            }

            html += "<span>"
                + "<select id=\"ddlPaging1\" style=\"font-size:11px;vertical-align:middle;\" onchange=\"Pagination1( $('#ddlPaging1').val() )\">";

            for (int ctr = 1; ctr <= totalPage; ctr++)
            {
                html += "<option " + (pageNo == ctr ? "selected" : "") + " value=\"" + ctr + "\">" + ctr + "</option>";
            }

            html += "</select><label><h6><strong> of " + totalPage + "</strong></h6></label>"
                + "</span>&nbsp;";


            if (pageNo < totalPage)
            {
                html += "<span class=\"glyphicon glyphicon-triangle-right\" title=\"Next\" style=\"display:inline-block; vertical-align:middle; cursor:pointer\" onclick=\"Pagination1(" + (pageNo + 1).ToString() + ");\" ></span>&nbsp;"
                    + "<span class=\"glyphicon glyphicon-step-forward\" title=\"Last\" style=\"display:inline-block; vertical-align:middle; cursor:pointer\" onclick=\"Pagination1(" + totalPage.ToString() + ");\" ></span>";
            }

            return html;
        }

        public static string Style102(int pageNo, int totalPage)
        {
            string html = "";

            if (pageNo > 1)
            {
                html += "<span class=\"glyphicon glyphicon-step-backward\" title=\"First\" style=\"display:inline-block; vertical-align:middle; cursor:pointer\" onclick=\"Pagination2(1);\" ></span>&nbsp;"
                    + "<span class=\"glyphicon glyphicon-triangle-left\" title=\"Previous\" style=\"display:inline-block; vertical-align:middle; cursor:pointer\" onclick=\"Pagination2(" + (pageNo - 1).ToString() + ");\" ></span>&nbsp";
            }

            html += "<span>"
                + "<select id=\"ddlPaging2\" style=\"font-size:11px;vertical-align:middle;\" onchange=\"Pagination2( $('#ddlPaging2').val() )\">";

            for (int ctr = 1; ctr <= totalPage; ctr++)
            {
                html += "<option " + (pageNo == ctr ? "selected" : "") + " value=\"" + ctr + "\">" + ctr + "</option>";
            }

            html += "</select><label><h6><strong> of " + totalPage + "</strong></h6></label>"
                + "</span>&nbsp;";


            if (pageNo < totalPage)
            {
                html += "<span class=\"glyphicon glyphicon-triangle-right\" title=\"Next\" style=\"display:inline-block; vertical-align:middle; cursor:pointer\" onclick=\"Pagination2(" + (pageNo + 1).ToString() + ");\" ></span>&nbsp;"
                    + "<span class=\"glyphicon glyphicon-step-forward\" title=\"Last\" style=\"display:inline-block; vertical-align:middle; cursor:pointer\" onclick=\"Pagination2(" + totalPage.ToString() + ");\" ></span>";
            }

            return html;
        }

        public static string Style103(int pageNo, int totalPage)
        {
            string html = "";

            if (pageNo > 1)
            {
                html += "<span class=\"glyphicon glyphicon-step-backward\" title=\"First\" style=\"display:inline-block; vertical-align:middle; cursor:pointer\" onclick=\"Pagination3(1);\" ></span>&nbsp;"
                    + "<span class=\"glyphicon glyphicon-triangle-left\" title=\"Previous\" style=\"display:inline-block; vertical-align:middle; cursor:pointer\" onclick=\"Pagination3(" + (pageNo - 1).ToString() + ");\" ></span>&nbsp";
            }

            html += "<span>"
                + "<select id=\"ddlPaging3\" style=\"font-size:11px;vertical-align:middle;\" onchange=\"Pagination3( $('#ddlPaging3').val() )\">";

            for (int ctr = 1; ctr <= totalPage; ctr++)
            {
                html += "<option " + (pageNo == ctr ? "selected" : "") + " value=\"" + ctr + "\">" + ctr + "</option>";
            }

            html += "</select><label><h6><strong> of " + totalPage + "</strong></h6></label>"
                + "</span>&nbsp;";


            if (pageNo < totalPage)
            {
                html += "<span class=\"glyphicon glyphicon-triangle-right\" title=\"Next\" style=\"display:inline-block; vertical-align:middle; cursor:pointer\" onclick=\"Pagination3(" + (pageNo + 1).ToString() + ");\" ></span>&nbsp;"
                    + "<span class=\"glyphicon glyphicon-step-forward\" title=\"Last\" style=\"display:inline-block; vertical-align:middle; cursor:pointer\" onclick=\"Pagination3(" + totalPage.ToString() + ");\" ></span>";
            }

            return html;
        }

        public static string Style104(int pageNo, int totalPage)
        {
            string html = "";

            if (pageNo > 1)
            {
                html += "<span class=\"glyphicon glyphicon-step-backward\" title=\"First\" style=\"display:inline-block; vertical-align:middle; cursor:pointer\" onclick=\"Pagination4(1);\" ></span>&nbsp;"
                    + "<span class=\"glyphicon glyphicon-triangle-left\" title=\"Previous\" style=\"display:inline-block; vertical-align:middle; cursor:pointer\" onclick=\"Pagination4(" + (pageNo - 1).ToString() + ");\" ></span>&nbsp";
            }

            html += "<span>"
                + "<select id=\"ddlPaging4\" style=\"font-size:11px;vertical-align:middle;\" onchange=\"Pagination4( $('#ddlPaging4').val() )\">";

            for (int ctr = 1; ctr <= totalPage; ctr++)
            {
                html += "<option " + (pageNo == ctr ? "selected" : "") + " value=\"" + ctr + "\">" + ctr + "</option>";
            }

            html += "</select><label><h6><strong> of " + totalPage + "</strong></h6></label>"
                + "</span>&nbsp;";


            if (pageNo < totalPage)
            {
                html += "<span class=\"glyphicon glyphicon-triangle-right\" title=\"Next\" style=\"display:inline-block; vertical-align:middle; cursor:pointer\" onclick=\"Pagination4(" + (pageNo + 1).ToString() + ");\" ></span>&nbsp;"
                    + "<span class=\"glyphicon glyphicon-step-forward\" title=\"Last\" style=\"display:inline-block; vertical-align:middle; cursor:pointer\" onclick=\"Pagination4(" + totalPage.ToString() + ");\" ></span>";
            }

            return html;
        }

        public static string PagePrint(int pageNo, int totalPage)
        {
            string html = "";

            if (pageNo > 1)
            {
                html += "<span class=\"glyphicon glyphicon-step-backward\" title=\"First\" style=\"display:inline-block; vertical-align:middle; cursor:pointer\" onclick=\"pagePrint(1);\" ></span>&nbsp;"
                    + "<span class=\"glyphicon glyphicon-triangle-left\" title=\"Previous\" style=\"display:inline-block; vertical-align:middle; cursor:pointer\" onclick=\"pagePrint(" + (pageNo - 1).ToString() + ");\" ></span>&nbsp";
            }

            html += "<span>"
                + "<select id=\"ddlPrintList\" style=\"font-size:11px;vertical-align:middle;\" onchange=\"pagePrint( $('#ddlPrintList').val() )\">";

            for (int ctr = 1; ctr <= totalPage; ctr++)
            {
                html += "<option " + (pageNo == ctr ? "selected" : "") + " value=\"" + ctr + "\">" + ctr + "</option>";
            }

            html += "</select><label><h6><strong> of " + totalPage + "</strong></h6></label>"
                + "</span>&nbsp;";


            if (pageNo < totalPage)
            {
                html += "<span class=\"glyphicon glyphicon-triangle-right\" title=\"Next\" style=\"display:inline-block; vertical-align:middle; cursor:pointer\" onclick=\"pagePrint(" + (pageNo + 1).ToString() + ");\" ></span>&nbsp;"
                    + "<span class=\"glyphicon glyphicon-step-forward\" title=\"Last\" style=\"display:inline-block; vertical-align:middle; cursor:pointer\" onclick=\"pagePrint(" + totalPage.ToString() + ");\" ></span>";
            }

            return html;
        }

        public static string PagePrint1(int pageNo, int totalPage)
        {
            string html = "";

            if (pageNo > 1)
            {
                html += "<span class=\"glyphicon glyphicon-step-backward\" title=\"First\" style=\"display:inline-block; vertical-align:middle; cursor:pointer\" onclick=\"pagePrint1(1);\" ></span>&nbsp;"
                    + "<span class=\"glyphicon glyphicon-triangle-left\" title=\"Previous\" style=\"display:inline-block; vertical-align:middle; cursor:pointer\" onclick=\"pagePrint1(" + (pageNo - 1).ToString() + ");\" ></span>&nbsp";
            }

            html += "<span>"
                + "<select id=\"ddlPrintList1\" style=\"font-size:11px;vertical-align:middle;\" onchange=\"pagePrint1( $('#ddlPrintList1').val() )\">";

            for (int ctr = 1; ctr <= totalPage; ctr++)
            {
                html += "<option " + (pageNo == ctr ? "selected" : "") + " value=\"" + ctr + "\">" + ctr + "</option>";
            }

            html += "</select><label><h6><strong> of " + totalPage + "</strong></h6></label>"
                + "</span>&nbsp;";


            if (pageNo < totalPage)
            {
                html += "<span class=\"glyphicon glyphicon-triangle-right\" title=\"Next\" style=\"display:inline-block; vertical-align:middle; cursor:pointer\" onclick=\"pagePrint1(" + (pageNo + 1).ToString() + ");\" ></span>&nbsp;"
                    + "<span class=\"glyphicon glyphicon-step-forward\" title=\"Last\" style=\"display:inline-block; vertical-align:middle; cursor:pointer\" onclick=\"pagePrint1(" + totalPage.ToString() + ");\" ></span>";
            }

            return html;
        }

        public static string Style105(int pageNo, int totalPage)
        {
            string html = "";

            if (pageNo > 1)
            {
                html += "<span class=\"glyphicon glyphicon-step-backward\" title=\"First\" style=\"display:inline-block; vertical-align:middle; cursor:pointer\" onclick=\"Pagination5(1);\" ></span>&nbsp;"
                    + "<span class=\"glyphicon glyphicon-triangle-left\" title=\"Previous\" style=\"display:inline-block; vertical-align:middle; cursor:pointer\" onclick=\"Pagination5(" + (pageNo - 1).ToString() + ");\" ></span>&nbsp";
            }

            html += "<span>"
                + "<select id=\"ddlPaging5\" style=\"font-size:11px;vertical-align:middle;\" onchange=\"Pagination5( $('#ddlPaging4').val() )\">";

            for (int ctr = 1; ctr <= totalPage; ctr++)
            {
                html += "<option " + (pageNo == ctr ? "selected" : "") + " value=\"" + ctr + "\">" + ctr + "</option>";
            }

            html += "</select><label><h6><strong> of " + totalPage + "</strong></h6></label>"
                + "</span>&nbsp;";


            if (pageNo < totalPage)
            {
                html += "<span class=\"glyphicon glyphicon-triangle-right\" title=\"Next\" style=\"display:inline-block; vertical-align:middle; cursor:pointer\" onclick=\"Pagination5(" + (pageNo + 1).ToString() + ");\" ></span>&nbsp;"
                    + "<span class=\"glyphicon glyphicon-step-forward\" title=\"Last\" style=\"display:inline-block; vertical-align:middle; cursor:pointer\" onclick=\"Pagination5(" + totalPage.ToString() + ");\" ></span>";
            }

            return html;
        }

        public static string Style106(int pageNo, int totalPage)
        {
            string html = "";

            if (pageNo > 1)
            {
                html += "<span class=\"glyphicon glyphicon-step-backward\" title=\"First\" style=\"display:inline-block; vertical-align:middle; cursor:pointer\" onclick=\"Pagination6(1);\" ></span>&nbsp;"
                    + "<span class=\"glyphicon glyphicon-triangle-left\" title=\"Previous\" style=\"display:inline-block; vertical-align:middle; cursor:pointer\" onclick=\"Pagination6(" + (pageNo - 1).ToString() + ");\" ></span>&nbsp";
            }

            html += "<span>"
                + "<select id=\"ddlPaging6\" style=\"font-size:11px;vertical-align:middle;\" onchange=\"Pagination6( $('#ddlPaging6').val() )\">";

            for (int ctr = 1; ctr <= totalPage; ctr++)
            {
                html += "<option " + (pageNo == ctr ? "selected" : "") + " value=\"" + ctr + "\">" + ctr + "</option>";
            }

            html += "</select><label><h6><strong> of " + totalPage + "</strong></h6></label>"
                + "</span>&nbsp;";


            if (pageNo < totalPage)
            {
                html += "<span class=\"glyphicon glyphicon-triangle-right\" title=\"Next\" style=\"display:inline-block; vertical-align:middle; cursor:pointer\" onclick=\"Pagination6(" + (pageNo + 1).ToString() + ");\" ></span>&nbsp;"
                    + "<span class=\"glyphicon glyphicon-step-forward\" title=\"Last\" style=\"display:inline-block; vertical-align:middle; cursor:pointer\" onclick=\"Pagination6(" + totalPage.ToString() + ");\" ></span>";
            }

            return html;
        }

        public static string Style107(int pageNo, int totalPage)
        {
            string html = "";

            if (pageNo > 1)
            {
                html += "<span class=\"glyphicon glyphicon-step-backward\" title=\"First\" style=\"display:inline-block; vertical-align:middle; cursor:pointer\" onclick=\"Pagination7(1);\" ></span>&nbsp;"
                    + "<span class=\"glyphicon glyphicon-triangle-left\" title=\"Previous\" style=\"display:inline-block; vertical-align:middle; cursor:pointer\" onclick=\"Pagination7(" + (pageNo - 1).ToString() + ");\" ></span>&nbsp";
            }

            html += "<span>"
                + "<select id=\"ddlPaging7\" style=\"font-size:11px;vertical-align:middle;\" onchange=\"Pagination7( $('#ddlPaging7').val() )\">";

            for (int ctr = 1; ctr <= totalPage; ctr++)
            {
                html += "<option " + (pageNo == ctr ? "selected" : "") + " value=\"" + ctr + "\">" + ctr + "</option>";
            }

            html += "</select><label><h6><strong> of " + totalPage + "</strong></h6></label>"
                + "</span>&nbsp;";


            if (pageNo < totalPage)
            {
                html += "<span class=\"glyphicon glyphicon-triangle-right\" title=\"Next\" style=\"display:inline-block; vertical-align:middle; cursor:pointer\" onclick=\"Pagination7(" + (pageNo + 1).ToString() + ");\" ></span>&nbsp;"
                    + "<span class=\"glyphicon glyphicon-step-forward\" title=\"Last\" style=\"display:inline-block; vertical-align:middle; cursor:pointer\" onclick=\"Pagination7(" + totalPage.ToString() + ");\" ></span>";
            }

            return html;
        }

        public static string Style108(int pageNo, int totalPage)
        {
            string html = "";

            if (pageNo > 1)
            {
                html += "<span class=\"glyphicon glyphicon-step-backward\" title=\"First\" style=\"display:inline-block; vertical-align:middle; cursor:pointer\" onclick=\"Pagination8(1);\" ></span>&nbsp;"
                    + "<span class=\"glyphicon glyphicon-triangle-left\" title=\"Previous\" style=\"display:inline-block; vertical-align:middle; cursor:pointer\" onclick=\"Pagination8(" + (pageNo - 1).ToString() + ");\" ></span>&nbsp";
            }

            html += "<span>"
                + "<select id=\"ddlPaging8\" style=\"font-size:11px;vertical-align:middle;\" onchange=\"Pagination8( $('#ddlPaging8').val() )\">";

            for (int ctr = 1; ctr <= totalPage; ctr++)
            {
                html += "<option " + (pageNo == ctr ? "selected" : "") + " value=\"" + ctr + "\">" + ctr + "</option>";
            }

            html += "</select><label><h6><strong> of " + totalPage + "</strong></h6></label>"
                + "</span>&nbsp;";


            if (pageNo < totalPage)
            {
                html += "<span class=\"glyphicon glyphicon-triangle-right\" title=\"Next\" style=\"display:inline-block; vertical-align:middle; cursor:pointer\" onclick=\"Pagination8(" + (pageNo + 1).ToString() + ");\" ></span>&nbsp;"
                    + "<span class=\"glyphicon glyphicon-step-forward\" title=\"Last\" style=\"display:inline-block; vertical-align:middle; cursor:pointer\" onclick=\"Pagination8(" + totalPage.ToString() + ");\" ></span>";
            }

            return html;
        }

        public static string Style109(int pageNo, int totalPage)
        {
            string html = "";

            if (pageNo > 1)
            {
                html += "<span class=\"glyphicon glyphicon-step-backward\" title=\"First\" style=\"display:inline-block; vertical-align:middle; cursor:pointer\" onclick=\"Pagination9(1);\" ></span>&nbsp;"
                    + "<span class=\"glyphicon glyphicon-triangle-left\" title=\"Previous\" style=\"display:inline-block; vertical-align:middle; cursor:pointer\" onclick=\"Pagination9(" + (pageNo - 1).ToString() + ");\" ></span>&nbsp";
            }

            html += "<span>"
                + "<select id=\"ddlPaging9\" style=\"font-size:11px;vertical-align:middle;\" onchange=\"Pagination9( $('#ddlPaging9').val() )\">";

            for (int ctr = 1; ctr <= totalPage; ctr++)
            {
                html += "<option " + (pageNo == ctr ? "selected" : "") + " value=\"" + ctr + "\">" + ctr + "</option>";
            }

            html += "</select><label><h6><strong> of " + totalPage + "</strong></h6></label>"
                + "</span>&nbsp;";


            if (pageNo < totalPage)
            {
                html += "<span class=\"glyphicon glyphicon-triangle-right\" title=\"Next\" style=\"display:inline-block; vertical-align:middle; cursor:pointer\" onclick=\"Pagination9(" + (pageNo + 1).ToString() + ");\" ></span>&nbsp;"
                    + "<span class=\"glyphicon glyphicon-step-forward\" title=\"Last\" style=\"display:inline-block; vertical-align:middle; cursor:pointer\" onclick=\"Pagination9(" + totalPage.ToString() + ");\" ></span>";
            }

            return html;
        }

        public static string Style110(int pageNo, int totalPage)
        {
            string html = "";

            if (pageNo > 1)
            {
                html += "<span class=\"glyphicon glyphicon-step-backward\" title=\"First\" style=\"display:inline-block; vertical-align:middle; cursor:pointer\" onclick=\"Pagination10(1);\" ></span>&nbsp;"
                    + "<span class=\"glyphicon glyphicon-triangle-left\" title=\"Previous\" style=\"display:inline-block; vertical-align:middle; cursor:pointer\" onclick=\"Pagination10(" + (pageNo - 1).ToString() + ");\" ></span>&nbsp";
            }

            html += "<span>"
                + "<select id=\"ddlPaging10\" style=\"font-size:11px;vertical-align:middle;\" onchange=\"Pagination10( $('#ddlPaging10').val() )\">";

            for (int ctr = 1; ctr <= totalPage; ctr++)
            {
                html += "<option " + (pageNo == ctr ? "selected" : "") + " value=\"" + ctr + "\">" + ctr + "</option>";
            }

            html += "</select><label><h6><strong> of " + totalPage + "</strong></h6></label>"
                + "</span>&nbsp;";


            if (pageNo < totalPage)
            {
                html += "<span class=\"glyphicon glyphicon-triangle-right\" title=\"Next\" style=\"display:inline-block; vertical-align:middle; cursor:pointer\" onclick=\"Pagination10(" + (pageNo + 1).ToString() + ");\" ></span>&nbsp;"
                    + "<span class=\"glyphicon glyphicon-step-forward\" title=\"Last\" style=\"display:inline-block; vertical-align:middle; cursor:pointer\" onclick=\"Pagination10(" + totalPage.ToString() + ");\" ></span>";
            }

            return html;
        }

        public static string Style111(int pageNo, int totalPage)
        {
            string html = "";

            if (pageNo > 1)
            {
                html += "<span class=\"glyphicon glyphicon-step-backward\" title=\"First\" style=\"display:inline-block; vertical-align:middle; cursor:pointer\" onclick=\"Pagination11(1);\" ></span>&nbsp;"
                    + "<span class=\"glyphicon glyphicon-triangle-left\" title=\"Previous\" style=\"display:inline-block; vertical-align:middle; cursor:pointer\" onclick=\"Pagination11(" + (pageNo - 1).ToString() + ");\" ></span>&nbsp";
            }

            html += "<span>"
                + "<select id=\"ddlPaging11\" style=\"font-size:11px;vertical-align:middle;\" onchange=\"Pagination11( $('#ddlPaging11').val() )\">";

            for (int ctr = 1; ctr <= totalPage; ctr++)
            {
                html += "<option " + (pageNo == ctr ? "selected" : "") + " value=\"" + ctr + "\">" + ctr + "</option>";
            }

            html += "</select><label><h6><strong> of " + totalPage + "</strong></h6></label>"
                + "</span>&nbsp;";


            if (pageNo < totalPage)
            {
                html += "<span class=\"glyphicon glyphicon-triangle-right\" title=\"Next\" style=\"display:inline-block; vertical-align:middle; cursor:pointer\" onclick=\"Pagination11(" + (pageNo + 1).ToString() + ");\" ></span>&nbsp;"
                    + "<span class=\"glyphicon glyphicon-step-forward\" title=\"Last\" style=\"display:inline-block; vertical-align:middle; cursor:pointer\" onclick=\"Pagination11(" + totalPage.ToString() + ");\" ></span>";
            }

            return html;
        }

        public static string Style112(int pageNo, int totalPage)
        {
            string html = "";

            if (pageNo > 1)
            {
                html += "<span class=\"glyphicon glyphicon-step-backward\" title=\"First\" style=\"display:inline-block; vertical-align:middle; cursor:pointer\" onclick=\"Pagination12(1);\" ></span>&nbsp;"
                    + "<span class=\"glyphicon glyphicon-triangle-left\" title=\"Previous\" style=\"display:inline-block; vertical-align:middle; cursor:pointer\" onclick=\"Pagination12(" + (pageNo - 1).ToString() + ");\" ></span>&nbsp";
            }

            html += "<span>"
                + "<select id=\"ddlPaging12\" style=\"font-size:11px;vertical-align:middle;\" onchange=\"Pagination12( $('#ddlPaging12').val() )\">";

            for (int ctr = 1; ctr <= totalPage; ctr++)
            {
                html += "<option " + (pageNo == ctr ? "selected" : "") + " value=\"" + ctr + "\">" + ctr + "</option>";
            }

            html += "</select><label><h6><strong> of " + totalPage + "</strong></h6></label>"
                + "</span>&nbsp;";


            if (pageNo < totalPage)
            {
                html += "<span class=\"glyphicon glyphicon-triangle-right\" title=\"Next\" style=\"display:inline-block; vertical-align:middle; cursor:pointer\" onclick=\"Pagination12(" + (pageNo + 1).ToString() + ");\" ></span>&nbsp;"
                    + "<span class=\"glyphicon glyphicon-step-forward\" title=\"Last\" style=\"display:inline-block; vertical-align:middle; cursor:pointer\" onclick=\"Pagination12(" + totalPage.ToString() + ");\" ></span>";
            }

            return html;
        }


        public static string Style113(int pageNo, int totalPage)
        {
            string html = "";

            if (pageNo > 1)
            {
                html += "<span class=\"glyphicon glyphicon-step-backward\" title=\"First\" style=\"display:inline-block; vertical-align:middle; cursor:pointer\" onclick=\"Pagination13(1);\" ></span>&nbsp;"
                    + "<span class=\"glyphicon glyphicon-triangle-left\" title=\"Previous\" style=\"display:inline-block; vertical-align:middle; cursor:pointer\" onclick=\"Pagination13(" + (pageNo - 1).ToString() + ");\" ></span>&nbsp";
            }

            html += "<span>"
                + "<select id=\"ddlPaging13\" style=\"font-size:11px;vertical-align:middle;\" onchange=\"Pagination13( $('#ddlPaging13').val() )\">";

            for (int ctr = 1; ctr <= totalPage; ctr++)
            {
                html += "<option " + (pageNo == ctr ? "selected" : "") + " value=\"" + ctr + "\">" + ctr + "</option>";
            }

            html += "</select><label><h6><strong> of " + totalPage + "</strong></h6></label>"
                + "</span>&nbsp;";


            if (pageNo < totalPage)
            {
                html += "<span class=\"glyphicon glyphicon-triangle-right\" title=\"Next\" style=\"display:inline-block; vertical-align:middle; cursor:pointer\" onclick=\"Pagination13(" + (pageNo + 1).ToString() + ");\" ></span>&nbsp;"
                    + "<span class=\"glyphicon glyphicon-step-forward\" title=\"Last\" style=\"display:inline-block; vertical-align:middle; cursor:pointer\" onclick=\"Pagination13(" + totalPage.ToString() + ");\" ></span>";
            }

            return html;
        }

        public static string Style114(int pageNo, int totalPage)
        {
            string html = "";

            if (pageNo > 1)
            {
                html += "<span class=\"glyphicon glyphicon-step-backward\" title=\"First\" style=\"display:inline-block; vertical-align:middle; cursor:pointer\" onclick=\"Pagination14(1);\" ></span>&nbsp;"
                    + "<span class=\"glyphicon glyphicon-triangle-left\" title=\"Previous\" style=\"display:inline-block; vertical-align:middle; cursor:pointer\" onclick=\"Pagination14(" + (pageNo - 1).ToString() + ");\" ></span>&nbsp";
            }

            html += "<span>"
                + "<select id=\"ddlPaging14\" style=\"font-size:11px;vertical-align:middle;\" onchange=\"Pagination14( $('#ddlPaging14').val() )\">";

            for (int ctr = 1; ctr <= totalPage; ctr++)
            {
                html += "<option " + (pageNo == ctr ? "selected" : "") + " value=\"" + ctr + "\">" + ctr + "</option>";
            }

            html += "</select><label><h6><strong> of " + totalPage + "</strong></h6></label>"
                + "</span>&nbsp;";


            if (pageNo < totalPage)
            {
                html += "<span class=\"glyphicon glyphicon-triangle-right\" title=\"Next\" style=\"display:inline-block; vertical-align:middle; cursor:pointer\" onclick=\"Pagination14(" + (pageNo + 1).ToString() + ");\" ></span>&nbsp;"
                    + "<span class=\"glyphicon glyphicon-step-forward\" title=\"Last\" style=\"display:inline-block; vertical-align:middle; cursor:pointer\" onclick=\"Pagination14(" + totalPage.ToString() + ");\" ></span>";
            }

            return html;
        }
    }
}