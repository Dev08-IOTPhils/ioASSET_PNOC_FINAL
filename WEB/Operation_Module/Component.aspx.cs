using AdvanceBll;
using AdvanceEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Files_Data_Component : BasePage
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

        string AccessButon = rb.CheckR("Operation_Module/Component.aspx", Convert.ToInt32(Common.Role), Common.ConnString);
        string[] ButtonAccess = AccessButon.Split('|');
        ViewAccess = ButtonAccess[0];
        AddAccess = ButtonAccess[1];
        EditAccess = ButtonAccess[2];
        DeleteAccess = ButtonAccess[3];
    }
    [WebMethod]
    public static string SelectProperty()
    {
        ComponentBll componentBll = new ComponentBll();

        string html = string.Empty;
        List<PropertyEntity> JsonItemList = new List<PropertyEntity>();
        try
        {
            DataTable dt = componentBll.SelectProperty(Common.ConnString);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    PropertyEntity x = new PropertyEntity();
                    x.PropertyNumber = "<p precid='" + dt.Rows[i]["RecID"].ToString() + "' pclassi='" + dt.Rows[i]["ClassificationName"].ToString() + "' ppnum='" + dt.Rows[i]["PropertyNumber"].ToString() + "' pdesc='" + dt.Rows[i]["Description"].ToString() + "' pserial='" + dt.Rows[i]["SerialNumber"].ToString() +
                      "' pcat='" + dt.Rows[i]["CategoryName"].ToString() + "'  >" + dt.Rows[i]["PropertyNumber"].ToString() + "</p>";
                    x.Description = dt.Rows[i]["Description"].ToString();
                    x.ClassificationName = dt.Rows[i]["ClassificationName"].ToString();
                    x.SerialNo = dt.Rows[i]["SerialNumber"].ToString();
                    x.Category = dt.Rows[i]["CategoryName"].ToString();

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
    public static string GetCompEntry()
    {
        ComponentBll componentBll = new ComponentBll();

        string html = string.Empty;
        List<PropertyEntity> JsonItemList = new List<PropertyEntity>();
        try
        {
            DataTable dt = componentBll.GetCompEntry(Common.ConnString);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    PropertyEntity x = new PropertyEntity();
                    x.PropertyNumber = dt.Rows[i]["PropertyNumber"].ToString();
                    if (dt.Rows[i]["TransactionType"].ToString() != "")
                    {
                        x.MaintenanceRemarks = dt.Rows[i]["PropertyStatus"].ToString() + " (" + dt.Rows[i]["TransactionType"].ToString() + ")";
                    }
                    else
                    {
                        x.MaintenanceRemarks = dt.Rows[i]["PropertyStatus"].ToString();
                    }
                    
                    x.TransactionNumber = "<p grid='" + dt.Rows[i]["ComponentEntryHeaderID"].ToString() + "' categ='" + dt.Rows[i]["CategoryName"].ToString() + "' descr='" + dt.Rows[i]["Description"].ToString() + "' serno='" + dt.Rows[i]["SerialNumber"].ToString() + "'  classi='" + dt.Rows[i]["ClassificationName"].ToString() + "' grno='" + dt.Rows[i]["ComponentEntryNo"].ToString() + "' pmlid='" + dt.Rows[i]["PMLID"].ToString() + "' pnum='" + dt.Rows[i]["PropertyNumber"].ToString() +
                      "' status='" + dt.Rows[i]["Status"].ToString() + "' >" + dt.Rows[i]["ComponentEntryNo"].ToString() + "</p>";
                    x.CreatedBy = dt.Rows[i]["CreatedBy"].ToString(); x.CreatedBy = dt.Rows[i]["CreatedBy"].ToString();
                    x.DateOfTransaction = dt.Rows[i]["CreatedDate"].ToString();
                    x.Status = dt.Rows[i]["Status"].ToString();
                    string action = "<center>";
                    //action += "<center><button type='button' class='btn btn-xs btn-primary fa fa-paperclip btn_Attachment' data-toggle='tooltip' title='Add Attachment' onclick=\"addAttachment('" + dt.Rows[i]["ComponentEntryNo"] + "','Component')\"></button>";

                    
                    if (Common.IsCustodian == "True" || Common.Administrative == "True")
                    {
                        if (dt.Rows[i]["Status"].ToString() == "Locked")
                        {
                            if (dt.Rows[i]["PropertyStatus"].ToString() == "On Stock")
                            {
                                action += " <button type='button' class='btn btn-xs btn-primary fa fa-unlock ' data-toggle='tooltip' title='Unlock Component Entry' onclick=\"UnlockTransaction('" + dt.Rows[i]["ComponentEntryHeaderID"] + "')\"></button> ";
                            }
                        }
                        else
                        {
                            action += " <button type='button' class='btn btn-xs btn-primary fa fa-lock ' data-toggle='tooltip' title='Lock Component Entry' onclick=\"LockTransaction('" + dt.Rows[i]["ComponentEntryHeaderID"] + "')\"></button> ";
                        }
                    }
                    //if (dt.Rows[i]["Status"].ToString() == "Approved" || dt.Rows[i]["Status"].ToString() == "Cancelled")
                    //{
                    //    action += "";
                    //}
                    //else
                    //{
                    //    action += "  <button type='button' class='btn btn-xs btn-danger fa fa-times ' data-toggle='tooltip' title='Cancel Component' onclick=\"cancelComponent()\"></button>";
                    //}
                    x.Action = action + "</center>";
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
    public static string LockTransaction(int TransId)
    {
        ComponentBll componentBll = new ComponentBll();       

        return componentBll.LockTransaction(TransId, Common.ConnString, Common.UserName).ToString();
    }

    [WebMethod]
    public static string UnlockTransaction(int TransId)
    {
        ComponentBll componentBll = new ComponentBll();

        return componentBll.UnlockTransaction(TransId, Common.ConnString, Common.UserName).ToString();
    }
    [WebMethod]
    public static string CreateComponents(int TransId, int pmlid)
    {
        ComponentBll componentBll = new ComponentBll();

        return componentBll.CreateComponent(TransId, pmlid, Common.ConnString, Common.UserName).ToString();
    }
    [WebMethod]
    public static string GetTagList()
    {
        TagTypeBll tagTypeBll = new TagTypeBll();

        DataTable dt = tagTypeBll.GetTag(Common.ConnString);
        return Control.MyConverter.ConvertDataTabletoJSON(dt);
    }

    [WebMethod]
    public static string AddComponents(string idForUpdate, string propertyno, string acquireddate, string longdesc, string serial, string uom, decimal acquiredcost)
    {
        PropertyEntity x = new PropertyEntity();
        ComponentBll componentBll = new ComponentBll();

        x.TransactionNumber = idForUpdate;
        //x.Description = desc;
        x.Description = longdesc;
        x.SerialNo = serial;
        //x.Classification = classification;
        //x.Brand = brand;
        //x.Model = model;
        //x.TagID = tag;
        x.PropertyNumber = propertyno;
        x.DateOfAcquisition = acquireddate;
        x.UnitOfMeasurement = uom;
        x.AcquiredCost = acquiredcost;
        x.CreatedBy = Common.UserName;
        x.ConnectionString = Common.ConnString;

        return componentBll.AddComponents(x).ToString();
    }
    [WebMethod]
    public static string GetCompAdded(string idForUpdate)
    {
        ComponentBll componentBll = new ComponentBll();

        string html = string.Empty;
        List<PropertyEntity> JsonItemList = new List<PropertyEntity>();
        try
        {
            DataTable dt = componentBll.GetCompAdded(Common.ConnString, idForUpdate);
            return Control.MyConverter.ConvertDataTabletoJSON(dt);
        }
        catch (Exception ex)
        {
            return "";
        }

    }

    [WebMethod]
    public static string UpdateComponents(int id, string idForUpdate, string propertyno, string longdesc, string serial, string acquireddate, string uom, decimal acquiredcost)
    {
        PropertyEntity x = new PropertyEntity();
        ComponentBll componentBll = new ComponentBll();

        x.PropertyID = id;
        x.TransactionNumber = idForUpdate;
        x.PropertyNumber = propertyno;
        x.LongDescription = longdesc;
        x.SerialNo = serial;
        x.DateOfAcquisition = acquireddate;
        x.UnitOfMeasurement = uom;
        x.AcquiredCost = acquiredcost;
        x.CreatedBy = Common.UserName;
        x.ConnectionString = Common.ConnString;

        return componentBll.UpdateComponents(x).ToString();
    }


    [WebMethod]
    public static string RemoveComponents(int idForUpdate)
    {
        ComponentBll componentBll = new ComponentBll();
        

        return componentBll.DeleteComponent(idForUpdate, Common.ConnString).ToString();
    }

    [WebMethod]
    public static string CancelComponent(string transno, string remarks)
    {
        PropertyEntity x = new PropertyEntity();
        ComponentBll componentBll = new ComponentBll();

        x.CreatedBy = Common.UserName;
        x.PropertyNumber = transno;
        x.Remarks = remarks;
        x.ConnectionString = Common.ConnString;
        
        return componentBll.CancelComponent(x).ToString();
    }
}