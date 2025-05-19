<%@ WebHandler Language="C#" Class="OperationUploadHandler" %>

using System;
using System.Web;
using System.IO;
using System.Web.SessionState;

public class OperationUploadHandler : IHttpHandler {

    public void ProcessRequest (HttpContext context) {
        string filedata = string.Empty;
        string stringParamFileName = (string)context.Request["transactionnumber"];
        context.Response.ContentType = "text/plain";

        string FileNameDirectory = stringParamFileName.Split('|')[0];
        string OperationModule = stringParamFileName.Split('|')[1];
        string dirFullPath = "";
        if (OperationModule == "PropertyDisposalRequest")
        {
            dirFullPath = HttpContext.Current.Server.MapPath("~/SupportingDocuments/PropertyDisposalRequest/" + FileNameDirectory + "/");
        }
        if (OperationModule == "GoodsReceive")
        {
            dirFullPath = HttpContext.Current.Server.MapPath("~/SupportingDocuments/GoodsReceive/" + FileNameDirectory + "/");
        }
        if (OperationModule == "PropertyIssuance")
        {
            dirFullPath = HttpContext.Current.Server.MapPath("~/SupportingDocuments/PropertyIssuance/" + FileNameDirectory + "/");
        }
        if (OperationModule == "PropertyRequest")
        {
            dirFullPath = HttpContext.Current.Server.MapPath("~/SupportingDocuments/PropertyRequest/" + FileNameDirectory + "/");
        }
        if (OperationModule == "PropertyReturn")
        {
            dirFullPath = HttpContext.Current.Server.MapPath("~/SupportingDocuments/PropertyReturn/" + FileNameDirectory + "/");
        }
        if (OperationModule == "PropertyReturnRequest")
        {
            dirFullPath = HttpContext.Current.Server.MapPath("~/SupportingDocuments/PropertyReturnRequest/" + FileNameDirectory + "/");
        }
         if (OperationModule == "PropertyTransfer")
        {
            dirFullPath = HttpContext.Current.Server.MapPath("~/SupportingDocuments/PropertyTransfer/" + FileNameDirectory + "/");
        }
        if (OperationModule == "StockIssuance")
        {
            dirFullPath = HttpContext.Current.Server.MapPath("~/SupportingDocuments/StockIssuance/" + FileNameDirectory + "/");
        }
        if (OperationModule == "StockRequest")
        {
            dirFullPath = HttpContext.Current.Server.MapPath("~/SupportingDocuments/StockRequest/" + FileNameDirectory + "/");
        }
        if (OperationModule == "StockReturn")
        {
            dirFullPath = HttpContext.Current.Server.MapPath("~/SupportingDocuments/StockReturn/" + FileNameDirectory + "/");
        }
         if (OperationModule == "InventoryCount")
        {
            dirFullPath = HttpContext.Current.Server.MapPath("~/SupportingDocuments/InventoryCount/" + FileNameDirectory + "/");
        }
        if (OperationModule == "PropertyDisposal")
        {
            dirFullPath = HttpContext.Current.Server.MapPath("~/SupportingDocuments/PropertyDisposal/" + FileNameDirectory + "/");
        }
        if (OperationModule == "InventoryCount")
        {
            dirFullPath = HttpContext.Current.Server.MapPath("~/SupportingDocuments/InventoryCount/" + FileNameDirectory + "/");
        }
        if (OperationModule == "StockDisposal")
        {
            dirFullPath = HttpContext.Current.Server.MapPath("~/SupportingDocuments/StockDisposal/" + FileNameDirectory + "/");
        }
        if (OperationModule == "PropertyMaintenance")
        {
                string MaintenanceId = stringParamFileName.Split('|')[2];
                string NextStep = stringParamFileName.Split('|')[3];


              //  string rootPath = HttpContext.Current.Server.MapPath("~/SupportingDocuments/PropertyMaintenance/" + MaintenanceId + "-" + FileNameDirectory + "/");
                dirFullPath = HttpContext.Current.Server.MapPath("~/SupportingDocuments/PropertyMaintenance/" +MaintenanceId + "-" + FileNameDirectory + "/"+ NextStep + "/");


           // dirFullPath = HttpContext.Current.Server.MapPath("~/SupportingDocuments/PropertyDisposal/" + FileNameDirectory + "/");
        }
        if (!Directory.Exists(dirFullPath))
        {
            Directory.CreateDirectory(dirFullPath);
        }
        if (context.Request.Files.Count > 0) {
            HttpFileCollection files = context.Request.Files;
            for (int i = 0; i < files.Count; i++) {
                HttpPostedFile file = files[i];
                if (Path.GetExtension(file.FileName).ToLower() != ".jpg" &&
                    Path.GetExtension(file.FileName).ToLower() != ".png" &&
                    Path.GetExtension(file.FileName).ToLower() != ".gif" &&
                    Path.GetExtension(file.FileName).ToLower() != ".jpeg" &&
                    Path.GetExtension(file.FileName).ToLower() != ".pdf" &&
                    Path.GetExtension(file.FileName).ToLower() != ".txt" &&
                    Path.GetExtension(file.FileName).ToLower() != ".doc" &&
                    Path.GetExtension(file.FileName).ToLower() != ".docx" &&
                    Path.GetExtension(file.FileName).ToLower() != ".xls" &&
                    Path.GetExtension(file.FileName).ToLower() != ".csv" &&
                    Path.GetExtension(file.FileName).ToLower() != ".xlsx"
                ) {
                    context.Response.ContentType = "text/plain";
                    context.Response.Write("Only jpg, png , gif, jpeg, pdf, txt, docx, xlsx, csv are allowed.!");
                    return;
                }
                decimal size = Math.Round(((decimal) file.ContentLength / (decimal) 1024), 2);
                if (size > 10240) {
                    context.Response.ContentType = "text/plain";
                    context.Response.Write("File size should not exceed 10 MB.!");
                    return;
                }
                string fname;
                if (HttpContext.Current.Request.Browser.Browser.ToUpper() == "IE" || HttpContext.Current.Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER") {
                    string[] testfiles = file.FileName.Split(new char[] {
                        '\\'
                    });
                    fname = testfiles[testfiles.Length - 1];
                } else {
                    fname = file.FileName;
                }
                //here UploadFile is define my folder name , where files will be store.  
                //string uploaddir = System.Configuration.ConfigurationManager.AppSettings["UploadFile"];  
                //filedata = Guid.NewGuid() + fname;
                filedata = fname;
                fname = Path.Combine(dirFullPath, filedata);
                file.SaveAs(fname);
            }
        }
        context.Response.ContentType = "text/plain";
        context.Response.Write("File Uploaded Successfully:" + filedata + "!");
        ////if you want to use file path in aspx.cs page , then assign it in to session  
        //context.Session["PathImage"] = filedata;
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}