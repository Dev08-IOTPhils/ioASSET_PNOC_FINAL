<%@ WebHandler Language="C#" Class="FilesData" %>

using System;
using System.Web;
using System.IO;
using System.Web.SessionState;

public class FilesData : IHttpHandler {
    
     public void ProcessRequest (HttpContext context) {
        string filedata = string.Empty;
        string stringParamFileName = (string)context.Request["transactionnumber"];
        context.Response.ContentType = "text/plain";

        string FileNameDirectory = stringParamFileName.Split('|')[0];
        string OperationModule = stringParamFileName.Split('|')[1];
        string FileName = stringParamFileName.Split('|')[2];
        string dirFullPath = "";
        if (OperationModule == "Stock")
        {
            dirFullPath = HttpContext.Current.Server.MapPath("~/Image/StockImage/" + FileNameDirectory + "/");
        }
        if (OperationModule == "Property")
        {
            dirFullPath = HttpContext.Current.Server.MapPath("~/Image/PropertyImage/" + FileNameDirectory + "/");
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
                    Path.GetExtension(file.FileName).ToLower() != ".jpeg" //&&
                    //Path.GetExtension(file.FileName).ToLower() != ".pdf"
                ){
                    context.Response.ContentType = "text/plain";
                    context.Response.Write("Only jpg, png , gif, .jpeg are allowed.!");
                    return;
                }
                decimal size = Math.Round(((decimal) file.ContentLength / (decimal) 1024), 2);
                if (size > 2048) {
                    context.Response.ContentType = "text/plain";
                    context.Response.Write("File size should not exceed 2 MB.!");
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
                filedata = FileName;
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