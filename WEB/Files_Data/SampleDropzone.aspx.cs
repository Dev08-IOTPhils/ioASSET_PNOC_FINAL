using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Files_Data_SampleDropzone : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        foreach (string s in Request.Files)
        {
            //String path = Server.MapPath("~/Image/PropertyImage/" + itemcode + "/");
            HttpPostedFile file = Request.Files[s];

            int fileSizeInBytes = file.ContentLength;
            string fileName = Request.Headers["X-File-Name"];
            string fileExtension = "";

            if (!string.IsNullOrEmpty(fileName))
                fileExtension = Path.GetExtension(fileName);

            // IMPORTANT! Make sure to validate uploaded file contents, size, etc. to prevent scripts being uploaded into your web app directory
            string savedFileName = Path.Combine(@"D:\SharedFolder\","sample" + fileExtension);
            file.SaveAs(savedFileName);
        }
    }
}