using AdvanceBll;
using System;
using System.IO;
using System.Web.UI;

public partial class About : BasePage
{
    string ret = "", ret1 = "", ret2 = "", ret3 = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (LicenseChecker.GetLicenseType() == "Perm. License")
        {
            lblLicenseType.InnerHtml = "Permanent License";
        }
        else
        {
            lblLicenseType.InnerHtml = LicenseChecker.GetLicenseType();
        }
        ShowHideRequest();
        LoadLicenseDetails();

        SystemConfigurations shSystemConfigurationsIOVers = (SystemConfigurations)Session["shSystemConfigurationsIOVers"];
        SystemConfigurations shSystemConfigurationsContactName = (SystemConfigurations)Session["shSystemConfigurationsContactName"];
        SystemConfigurations shSystemConfigurationsContactEmail = (SystemConfigurations)Session["shSystemConfigurationsContactEmail"];
        SystemConfigurations shSystemConfigurationsContactNumber = (SystemConfigurations)Session["shSystemConfigurationsContactNumber"];

        ret = shSystemConfigurationsIOVers.Value;
        ret1 = shSystemConfigurationsContactName.Value;
        ret2 = shSystemConfigurationsContactEmail.Value;
        ret3 = shSystemConfigurationsContactNumber.Value;

        lblListIOVers.Text = ret;
        lblContactName.Text = ret1;
        lblContactEmail.Text = ret2;
        lblContactNumber.Text = ret3;
    }

    void LoadLicenseDetails()
    {
        try
        {
            txtPO.Text = LicenseChecker.sil.PurchaseOrder;
            txtDR.Text = LicenseChecker.sil.DeliveryReceipt;
            txtSI.Text = LicenseChecker.sil.SalesInvoice;
            txtCompanyName.Text = LicenseChecker.sil.CompanyName;
            txtAddress.Text = LicenseChecker.sil.Address;
            txtContactName.Text = LicenseChecker.sil.ContactPerson;
            txtContactNo.Text = LicenseChecker.sil.ContactNumber;
            txtEmail.Text = LicenseChecker.sil.EmailAddress;
        }
        catch { }
    }

    protected void btnSSAUpload_Click(object sender, EventArgs e)
    {
        try
        {
            Boolean fileOK = false;

            String path = Server.MapPath("~/Bin/");
            if (fuSSAUpload.HasFile)
            {
                String fileExtension = System.IO.Path.GetExtension(fuSSAUpload.FileName).ToLower();

                String[] allowedExtensions = { ".dat" };
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
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                        fuSSAUpload.SaveAs(path + fuSSAUpload.FileName);
                        string returnValue = LicenseChecker.uploadactivation(path, fuSSAUpload.FileName);
                        if (returnValue == "activated")
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Activated();", true);
                        }
                        else if (returnValue == "extended")
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Extended();", true);
                        }
                        else if (returnValue == "temp")
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:TempActivated();", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:Failed();", true);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.Write(ex);
                    }
                }
                else ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "javascript:InvalidFile();", true);
            }
        }
        catch { }
    }

    protected void btnDownload_Click(object sender, System.EventArgs e)
    {
        try
        {
            string sino = txtDR.Text.Trim();
            string pono = txtPO.Text.Trim();
            string drno = txtDR.Text.Trim();
            string companyname = txtCompanyName.Text.Trim();
            string address = txtAddress.Text.Trim();
            string contactperson = txtContactName.Text.Trim();
            string contactno = txtContactNo.Text.Trim();
            string emailaddress = txtEmail.Text.Trim();
            string filePath = Server.MapPath("~\\RegistrationKey") + "\\RequestLicense_" + companyname + ".dat";

           string elifeulav = LicenseChecker.LicenseGen(true);
            string[] value = elifeulav.Split('|');

            using (StreamWriter sw = new StreamWriter(filePath))
            {
                string getsysteminf = LicenseChecker.generateregistryString(true) + "|"
                    + sino + "|"
                    + companyname + "|"
                    + address + "|"
                    + contactperson + "|"
                    + contactno + "|"
                    + txtEmail.Text.Trim() + "|"
                    + value[10] + "|"
                    + value[11] + "|"
                    + pono + "|"
                    + drno + "|"
                    + value[14] + "|"
                    + value[15] + "|"
                    + value[16] + "|"
                    + value[17] + "|"
                    + value[18] + "|"
                    + value[19] + "|"
                    + value[20] + "|"
                    + value[21];

                string encryptedValue = LicenseChecker.Encrypt(LicenseChecker.Encrypt(LicenseChecker.Encrypt(LicenseChecker.Encrypt(LicenseChecker.Encrypt(getsysteminf)))));

                sw.WriteLine(encryptedValue);
            }

            string fileName = "RequestLicense_" + companyname + ".dat";
            string fileExtension = ".csv";

            // Set Response.ContentType
            Response.ContentType = GetContentType(fileExtension);

            // Append header
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);

            // Write the file to the Response
            Response.TransmitFile(Server.MapPath("~/RegistrationKey/" + fileName));
            Response.End();
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }

    private string GetContentType(string fileExtension)
    {
        if (string.IsNullOrEmpty(fileExtension))
            return string.Empty;

        string contentType = string.Empty;
        switch (fileExtension)
        {
            case ".htm":
            case ".html":
                contentType = "text/HTML";
                break;

            case ".txt":
            case ".csv":
                contentType = "text/plain";
                break;

            case ".doc":
            case ".rtf":
            case ".docx":
                contentType = "Application/msword";
                break;

            case ".xls":
            case ".xlsx":
                contentType = "Application/x-msexcel";
                break;

            case ".jpg":
            case ".jpeg":
                contentType = "image/jpeg";
                break;

            case ".gif":
                contentType = "image/GIF";
                break;

            case ".pdf":
                contentType = "application/pdf";
                break;
        }

        return contentType;
    }

    private void ShowHideRequest()
    {
        if (lblLicenseType.InnerHtml == "Permanent License")
        {
            btnAdd.Visible = false;
            btnUploadSSA.Visible = true;
        }
        else
        {
            btnAdd.Visible = true;
            btnUploadSSA.Visible = false;
        }
    }
}