<%@ WebHandler Language="C#" Class="Propertyupload" %>

using System;
using System.Web;
using System.IO;
using System.Web.SessionState;

public class Propertyupload : IHttpHandler {
    
    public void ProcessRequest(HttpContext context) {
        string filedata = string.Empty;
        context.Response.ContentType = "text/plain";

        string dirFullPath = HttpContext.Current.Server.MapPath("~/FilesUploaded/");
        if (!Directory.Exists(dirFullPath))
        {
            Directory.CreateDirectory(dirFullPath);
        }
        if (context.Request.Files.Count > 0) {
            HttpFileCollection files = context.Request.Files;
            for (int i = 0; i < files.Count; i++) {
                HttpPostedFile file = files[i];
                if (Path.GetExtension(file.FileName).ToLower() != ".csv" && Path.GetExtension(file.FileName).ToLower() != ".xls"
                                                                         //Path.GetExtension(file.FileName).ToLower() != ".png" &&
                                                                         //Path.GetExtension(file.FileName).ToLower() != ".gif" &&
                                                                         //Path.GetExtension(file.FileName).ToLower() != ".jpeg" //&&
                                                                         //Path.GetExtension(file.FileName).ToLower() != ".pdf"
                ) {
                    context.Response.ContentType = "text/plain";
                    context.Response.Write("Only .csv file is allowed.!");
                    return;
                }
                decimal size = Math.Round(((decimal) file.ContentLength / (decimal) 1024), 2);
                if (size > 2048) {
                    context.Response.ContentType = "text/plain";
                    context.Response.Write("File too large!");
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
        context.Response.Write("File Uploaded Successfully:" + filedata);
        ////if you want to use file path in aspx.cs page , then assign it in to session  
        //context.Session["PathImage"] = filedata;
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}