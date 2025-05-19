<%@ WebHandler Language="C#" Class="FormLogoHandler" %>

using System;
using System.Web;
using System.IO;
using System.Web.SessionState;

public class FormLogoHandler : IHttpHandler {
    
    public void ProcessRequest(HttpContext context) {
        string filedata = string.Empty;
        string stringParamFileName = (string)context.Request["CompanyName"];
        context.Response.ContentType = "text/plain";
        string FileNameDirectory = stringParamFileName.Split('|')[0];
        string FileNameImage = stringParamFileName.Split('|')[1];

        string dirFullPath = HttpContext.Current.Server.MapPath("~/Image/" + FileNameDirectory);
        if (!Directory.Exists(dirFullPath))
        {
            Directory.CreateDirectory(dirFullPath);
        }
        if (context.Request.Files.Count > 0) {
            HttpFileCollection files = context.Request.Files;
            for (int i = 0; i < files.Count; i++) {
                HttpPostedFile file = files[i];
                if (Path.GetExtension(file.FileName).ToLower() != ".png") {
                    context.Response.ContentType = "text/plain";
                    context.Response.Write("Only PNG are allowed!");
                    return;
                }
                decimal size = Math.Round(((decimal) file.ContentLength / (decimal) 1024), 2);
                if (size > 2048) {
                    context.Response.ContentType = "text/plain";
                    context.Response.Write("File size should not exceed 2 MB!");
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
                filedata = FileNameImage;
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