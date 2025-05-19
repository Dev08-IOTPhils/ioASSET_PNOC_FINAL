using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Data;
using AdvanceBll;
using AdvanceEntity;
using System.Security.Cryptography;
using System.Text;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Runtime.InteropServices;


using System.Data.SqlClient;
using System.Configuration;
using System.Data.OleDb;
using System.Web.Script.Serialization;
using System.Drawing;
using QRCoder;

public partial class RollOver_User : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    #region DropDownList



    [WebMethod]
    public void GetFromWarehouse()
    {
        WarehouseBll warehouseBll = new WarehouseBll();

        DataTable dt = warehouseBll.GetWarehouseList(Common.ConnString);

        ddlSite.DataSource = dt;
        ddlSite.DataValueField = "WarehouseID";
        ddlSite.DataTextField = "WarehouseName";
        ddlSite.DataBind();
        ddlSite.Items.Insert(0, "--- Select Site/Department ---");

    }

    [WebMethod]
    public void GetDepartmentList()
    {
        DepartmentEntity departmentEntity = new DepartmentEntity();
        DepartmentBll departmentBll = new DepartmentBll();

        departmentEntity.ConnectionString = Common.ConnString;

        DataTable dt = departmentBll.GetDepartmentList(departmentEntity);

        ddlDepartment.DataSource = dt;
        ddlDepartment.DataValueField = "DepartmentID";
        ddlDepartment.DataTextField = "DepartmentName";
        ddlDepartment.DataBind();
        ddlDepartment.Items.Insert(0, "--- Select User Department ---");
    }

    [WebMethod]
    public void GetIntegratorList()
    {
        CustodianBll custodianBll = new CustodianBll();

        DataTable dt = custodianBll.GetCustodianList(Common.ConnString);

        ddlIntegrator.DataSource = dt;
        ddlIntegrator.DataValueField = "CustodianID";
        ddlIntegrator.DataTextField = "Custodian";
        ddlIntegrator.DataBind();
        ddlIntegrator.Items.Insert(0, "--- Select Custodian Type ---");
    }

    [WebMethod]
    public static string GetDepartmentID(int id)
    {
        DepartmentBll departmentBll = new DepartmentBll();
        DepartmentEntity departmentEntity = new DepartmentEntity();

        departmentEntity.PositionID = id;
        departmentEntity.ConnectionString = Common.ConnString;

        return departmentBll.GetDepartmentID(departmentEntity);
    }

    [WebMethod]
    public static List<ListItem> GetPosition(int department)
    {
        PositionEntity positionEntity = new PositionEntity();
        PositionBll positionBll = new PositionBll();

        positionEntity.ConnectionString = Common.ConnString;
        positionEntity.DepartmentID = department;

        return positionBll.GetPosition(positionEntity);
    }


    #endregion

    #region Methods

    [WebMethod]
    public static string GetUser()
    {
        UserBll userBLL = new UserBll();
        string html = string.Empty;
        List<UserEntity> JsonItemList = new List<UserEntity>();
        try
        {
            DataTable dt = userBLL.GetUser(Common.ConnString);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    UserEntity x = new UserEntity();
                    x.Name = dt.Rows[i]["LastName"].ToString() + ", " + dt.Rows[i]["FirstName"].ToString();
                    x.EmployeeNo = dt.Rows[i]["EmployeeNo"].ToString();
                    x.EmailAddress = dt.Rows[i]["EmailAddress"].ToString();
                    x.Approver = "<img " + ((bool.Parse(dt.Rows[i]["IsApprover"].ToString()) == true ? "src='../Image/Icon/yes.png'" : "src='../Image/Icon/no.png'")) + "/>";
                    x.Inspector = "<img " + ((bool.Parse(dt.Rows[i]["IsInspector"].ToString()) == true ? "src='../Image/Icon/yes.png'" : "src='../Image/Icon/no.png'")) + "/>";
                    x.Certifier = "<img " + ((bool.Parse(dt.Rows[i]["IsCertifier"].ToString()) == true ? "src='../Image/Icon/yes.png'" : "src='../Image/Icon/no.png'")) + "/>";

                    x.IsActive = "<img " + ((bool.Parse(dt.Rows[i]["Active"].ToString()) == true ? "src = '../Image/Icon/yes.png'" : "src = '../Image/Icon/no.png'")) + "/>";

                    if (Common.UserName == "superadmin")
                    {
                        x.Action = "<center><button class='btn btn-xs btn-danger' type='button' title='Reset Password' onclick=\"resetPassword('" + dt.Rows[i]["UserID"] + "')\"><span class='fa fa-exchange'></span></button> "
                            + "<button class='btn btn-xs btn-warning'  type='button' title='Edit User Information' onclick=\"GetModelID('" + dt.Rows[i]["UserID"] + "')\"><span class='fa fa-edit'></span></button> "
                            //+ "<button class='btn btn-xs btn-danger'  type='button' title='Delete User' onclick=\"deleteUser('" + dt.Rows[i]["UserID"] + "')\"><span class='fa fa-trash'></span></button></center>";

                            + "<button class='btn btn-xs btn-danger'  type='button' title='Print QR' onclick=\"printqr('" + dt.Rows[i]["EmployeeNo"] + "')\"><span class='fa fa-qrcode'></span></button></center>";
                    }
                    else
                    {
                        if (dt.Rows[i]["UserID"].ToString() == "1")
                        {
                            x.Action = "<center><button class='btn btn-xs btn-danger' disabled type='button' title='Reset Password' onclick=\"resetPassword('" + dt.Rows[i]["UserID"] + "')\"><span class='fa fa-exchange'></span></button> "
                                + "<button class='btn btn-xs btn-warning' disabled type='button' title='Edit User Information' onclick=\"GetModelID('" + dt.Rows[i]["UserID"] + "')\"><span class='fa fa-edit'></span></button> "
                                + "<button class='btn btn-xs btn-danger' disabled type='button' title='Delete User' onclick=\"deleteUser('" + dt.Rows[i]["UserID"] + "')\"><span class='fa fa-trash'></span></button></center>";
                        }
                        else
                        {
                            x.Action = "<center><button class='btn btn-xs btn-danger' type='button' title='Reset Password' onclick=\"resetPassword('" + dt.Rows[i]["UserID"] + "')\"><span class='fa fa-exchange'></span></button> "
                                + "<button class='btn btn-xs btn-warning' type='button' title='Edit User Information' onclick=\"GetModelID('" + dt.Rows[i]["UserID"] + "')\"><span class='fa fa-edit'></span></button> "
                                + "<button class='btn btn-xs btn-danger' type='button' title='Delete User' onclick=\"deleteUser('" + dt.Rows[i]["UserID"] + "')\"><span class='fa fa-trash'></span></button></center>";
                        }
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

    //[WebMethod]
    //public static string GetUser(string search, int pageNo)
    //{
    //    UserBll userBLL = new UserBll();
    //    DataTable dtUser = userBLL.GetUser(search, pageNo);

    //    string html = "";
    //    html += "<div class='table-responsive'><table class='table table-bordered table-hover dataTable' role='grid'>"
    //            + "<thead>"
    //            + "<tr class='info'>"
    //            + "<th><center><b>Name</b><center></th>"
    //            + "<th><center><b>Employee No</b></center></th>"
    //            + "<th><center><b>Email Address</b></center></th>"
    //            + "<th><center><b>Approver</b></center></th>"
    //            + "<th><center><b>Inspector</b></center></th>"
    //            + "<th><center><b>Certifier</b></center></th>"
    //            + "<th><center><b>Active</b></center></th>"
    //            + "<th><center><b>Action</b></center></th>"
    //            + "</tr>"
    //            + "</thead><tbody>";

    //    if (dtUser.Rows.Count > 0)
    //    {
    //        for (int i = 0; i < dtUser.Rows.Count; i++)
    //        {
    //            html += "<tr>"
    //                  + "<td class='col-md-2 text-plain'>" + dtUser.Rows[i]["LastName"].ToString() + ", " + dtUser.Rows[i]["FirstName"].ToString() + "</td>"
    //                  + "<td text-plain'>" + dtUser.Rows[i]["EmployeeNo"].ToString() + "</td>"
    //                  + "<td text-plain'>" + dtUser.Rows[i]["EmailAddress"].ToString() + "</td>"
    //                  + "<td align='center' class='col-md-1'><img " + ((bool.Parse(dtUser.Rows[i]["IsApprover"].ToString()) == true ? "src='../Image/Icon/yes.png'" : "src='../Image/Icon/no.png'")) + "/></td>"
    //                  + "<td align='center' class='col-md-1'><img " + ((bool.Parse(dtUser.Rows[i]["IsInspector"].ToString()) == true ? "src='../Image/Icon/yes.png'" : "src='../Image/Icon/no.png'")) + "/></td>"
    //                  + "<td align='center' class='col-md-1'><img " + ((bool.Parse(dtUser.Rows[i]["IsCertifier"].ToString()) == true ? "src='../Image/Icon/yes.png'" : "src='../Image/Icon/no.png'")) + "/></td>"
    //                  + "<td align='center'><img " + ((bool.Parse(dtUser.Rows[i]["Active"].ToString()) == true ? "src='../Image/Icon/yes.png'" : "src='../Image/Icon/no.png'")) + "/></td>";

    //            if (Common.UserName == "superadmin")
    //            {
    //                html += "<td align='center' class='col-md-1'><button class='btn btn-xs btn-danger' type='button' title='Reset Password' onclick=\"resetPassword('" + dtUser.Rows[i]["UserID"] + "')\"><span class='fa fa-exchange'></span></button> <button class='btn btn-xs btn-warning'  type='button' title='Edit User Information' onclick=\"GetModelID('" + dtUser.Rows[i]["UserID"] + "')\"><span class='fa fa-edit'></span></button> <button class='btn btn-xs btn-danger'  type='button' title='Delete User' onclick=\"deleteUser('" + dtUser.Rows[i]["UserID"] + "')\"><span class='fa fa-trash'></span></button></td>";
    //            }
    //            else
    //            {
    //                if (dtUser.Rows[i]["UserID"].ToString() == "1")
    //                {
    //                    html += "<td align='center' class='col-md-1'><button class='btn btn-xs btn-danger' disabled type='button' title='Reset Password' onclick=\"resetPassword('" + dtUser.Rows[i]["UserID"] + "')\"><span class='fa fa-exchange'></span></button> <button class='btn btn-xs btn-warning' disabled type='button' title='Edit User Information' onclick=\"GetModelID('" + dtUser.Rows[i]["UserID"] + "')\"><span class='fa fa-edit'></span></button> <button class='btn btn-xs btn-danger' disabled type='button' title='Delete User' onclick=\"deleteUser('" + dtUser.Rows[i]["UserID"] + "')\"><span class='fa fa-trash'></span></button></td>";
    //                }
    //                else
    //                {
    //                    html += "<td align='center' class='col-md-1'><button class='btn btn-xs btn-danger' type='button' title='Reset Password' onclick=\"resetPassword('" + dtUser.Rows[i]["UserID"] + "')\"><span class='fa fa-exchange'></span></button> <button class='btn btn-xs btn-warning' type='button' title='Edit User Information' onclick=\"GetModelID('" + dtUser.Rows[i]["UserID"] + "')\"><span class='fa fa-edit'></span></button> <button class='btn btn-xs btn-danger' type='button' title='Delete User' onclick=\"deleteUser('" + dtUser.Rows[i]["UserID"] + "')\"><span class='fa fa-trash'></span></button></td>";
    //                }
    //            }
    //            html += "</tr>";
    //        }
    //        html += "</tbody></table></div>";
    //        int totalPage = Int32.Parse(dtUser.Rows[0]["totalPage"].ToString());
    //        html += "<div style=\"text-align:center;padding:3px;\">" + Control.Paging.Style0(pageNo, totalPage) + "</div>";
    //    }
    //    else
    //        html = " <center><h5 style='color: red;'>No Records Found </h4> </center>";

    //    return html;
    //}

    [WebMethod]
    //public static string SaveUser(string firstname, string lastname, string cardno, int departmentid, string position, string emailaddress, string username, string password, bool administrative, bool active, string extension,int lobid, int pageNo)
    public static string SaveUser(int userid, string firstname, string lastname, string employeeno, string emailaddress, string username, bool active, bool approver, bool inspector, bool certifier, string extension, int department, int position, int integrator, int siteid, int pageNo)
    {
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
        userEntity.IntegratorID = integrator;
        userEntity.IsApprover = approver;
        userEntity.IsInspector = inspector;
        userEntity.IsCertifier = certifier;
        userEntity.Active = active;
        userEntity.SiteID = siteid;
        userEntity.CreatedBy = Common.UserName;
        userEntity.ConnectionString = Common.ConnString;

        return userBLL.SaveUser(userEntity).ToString();

    }

    protected void Upload(object sender, EventArgs e)
    {
        //Response.Write("<script>alert('"+ txtUsername.Text + "');</script>");
        Boolean fileOK = false;
        String path = Server.MapPath("~/Pictures/EmployeeImages/");
        if (FileUpload1.HasFile)
        {
            String fileExtension =
                System.IO.Path.GetExtension(FileUpload1.FileName).ToLower();
            //extension = fileExtension;
            String[] allowedExtensions = { ".gif", ".png", ".jpeg", ".jpg" };
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
                    FileUpload1.SaveAs(path + txtUsername.Text + System.IO.Path.GetExtension(FileUpload1.FileName).ToLower());
                }
                catch (Exception ex)
                {
                    //Label1.Text = "File could not be uploaded.";
                }
            }
            else
            {
                Response.Write("<script>alert('Please Select .PNG, .JPEG or .JPG to upload');</script>");
            }
        }
        else
        {

        }
    }

    [WebMethod]
    public static string GetUserByID(int id)
    {
        UserBll userBll = new UserBll();
        UserEntity userEntity = new UserEntity();

        userEntity.UserID = id;
        userEntity.ConnectionString = Common.ConnString;
        return userBll.GetUserByID(userEntity);
    }

    [WebMethod]
    public static string DeleteUser(int id)
    {
        UserBll userBll = new UserBll();
        UserEntity userEntity = new UserEntity();

        userEntity.UserID = id;
        userEntity.CreatedBy = Common.UserID;
        userEntity.ConnectionString = Common.ConnString;

        return userBll.DeleteUser(userEntity).ToString();

    }

    [WebMethod]
    public static string ResetPassword(int id)
    {
        UserBll userBll = new UserBll();
        UserEntity userEntity = new UserEntity();

        userEntity.UserID = id;
        userEntity.CreatedBy = Common.UserID;
        userEntity.ConnectionString = Common.ConnString;

        return userBll.ResetPassword(userEntity).ToString();

        //return GetUser(pageNo);
    }
    #endregion

    [WebMethod]
    public static string PrintQR(string employeeno)
    {
        string code = employeeno;
        QRCodeGenerator qrGenerator = new QRCodeGenerator();
        QRCodeData qrCodeData = qrGenerator.CreateQrCode(code, QRCodeGenerator.ECCLevel.Q);
        QRCode qrCode = new QRCode(qrCodeData);
        System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();
        imgBarCode.Height = 150;
        imgBarCode.Width = 150;
        using (Bitmap bitMap = qrCode.GetGraphic(20))
        {
            using (MemoryStream ms = new MemoryStream())
            {
                bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                byte[] byteImage = ms.ToArray();
                //imgBarCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
                //Page page = (Page)HttpContext.Current.Handler;
                //PlaceHolder plBarCode = (PlaceHolder)page.FindControl("plBarCode");
                //plBarCode.Controls.Add(imgBarCode);
                return imgBarCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
            }
        }

        //QRCodeGenerator qrGenerator = new QRCodeGenerator();
        //QRCodeData qrCodeData = qrGenerator.CreateQrCode(code, QRCodeGenerator.ECCLevel.Q);
        //QRCode qrCode = new QRCode(qrCodeData);
        //System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();
        //imgBarCode.Height = 150;
        //imgBarCode.Width = 150;
        //using (Bitmap bitMap = qrCode.GetGraphic(20))
        //{
        //    using (MemoryStream ms = new MemoryStream())
        //    {
        //        bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
        //        byte[] byteImage = ms.ToArray();
        //        return byteImage;
        //    }
        //}
    }
}