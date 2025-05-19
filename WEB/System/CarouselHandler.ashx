<%@ WebHandler Language="C#" Class="CarouselHandler" %>

using System;
using System.Web;
using System.IO;

public class CarouselHandler : IHttpHandler {
    
    public void ProcessRequest(HttpContext context)
    {
        string stringParamFileName = (string)context.Request["name"];
        context.Response.ContentType = "text/plain";

        string dirFullPath = HttpContext.Current.Server.MapPath("~/Image/ioAsset/Carousel/");
        if (!Directory.Exists(dirFullPath))
        {
            Directory.CreateDirectory(dirFullPath);
        }
        //string[] files;
        //int numFiles;
        //files = System.IO.Directory.GetFiles(dirFullPath);
        //numFiles = files.Length;
        //if (numFiles > 0)
        //{
        //    foreach (string file in files)
        //    {
        //        File.Delete(file);
        //    }
        //    numFiles = 0;
        //}

        //numFiles = numFiles + 1;
        string str_image = "";

        foreach (string s in context.Request.Files)
        {
            HttpPostedFile file = context.Request.Files[s];
            string fileName = file.FileName;
            string fileExtension = file.ContentType;
             if (Path.GetExtension(file.FileName).ToLower() != ".jpg" &&
                    Path.GetExtension(file.FileName).ToLower() != ".png" &&
                    Path.GetExtension(file.FileName).ToLower() != ".gif" &&
                    Path.GetExtension(file.FileName).ToLower() != ".jpeg" //&&
                    //Path.GetExtension(file.FileName).ToLower() != ".pdf"
                )
            {
                context.Response.ContentType = "text/plain";
                context.Response.Write("Only jpg, png , gif, .jpeg are allowed.!");
                return;
            }
            decimal size = Math.Round(((decimal)file.ContentLength / (decimal)1024), 2);
            if (size > 10197)
            {
                context.Response.ContentType = "text/plain";
                context.Response.Write("File size should not exceed 10 MB.!");
                return;
            }
            if (!string.IsNullOrEmpty(fileName))
            {
                fileExtension = Path.GetExtension(fileName);
                //str_image = stringParamFileName + numFiles.ToString() + fileExtension;
                str_image = fileName + fileExtension;
                string pathToSave = HttpContext.Current.Server.MapPath("~/Image/ioAsset/Carousel/") + str_image;
                file.SaveAs(pathToSave);
            }
        }
        context.Response.Write("~/Image/ioAsset/Carousel/" + str_image);
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}