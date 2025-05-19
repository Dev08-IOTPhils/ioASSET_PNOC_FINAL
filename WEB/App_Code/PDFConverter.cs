using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using OpenHtmlToPdf;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for PDFConverter
/// </summary>
public class PDFConverter
{
    public enum PageOrientation
    {
        Portrait = 1,
        Landscape = 2
    }

    public static byte[] ConvertHTMLtoPDF(string html, PaperSize size, PaperMargins pm, PageOrientation po)
    {
        byte[] pdf = null;
        if (po == PageOrientation.Portrait)
        {
            pdf = Pdf.From(html).OfSize(size).WithoutOutline().WithMargins(pm).Portrait().Content();
        }
        else
        {
            pdf = Pdf.From(html).OfSize(size).WithoutOutline().WithMargins(pm).Landscape().Content();
        }
        return pdf;
    }

    public static byte[] AddSignature(byte[] pdf,
        string certbase64,
        string passphrase)
    {
        certbase64 = certbase64.Split(',')[1];
        iTextSharp.text.pdf.PdfReader reader = new iTextSharp.text.pdf.PdfReader(pdf);
        iTextSharp.text.pdf.PdfStamper fromstamper;

        Org.BouncyCastle.Pkcs.Pkcs12Store store;
        using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(certbase64)))
        {
            store = new Org.BouncyCastle.Pkcs.Pkcs12Store(ms, passphrase.ToCharArray());
            ms.Close();
        }
        var alias = "";
        foreach (string al in store.Aliases)
        {
            if (store.IsKeyEntry(al) && store.GetKey(al).Key.IsPrivate)
            {
                alias = al;
                break;
            }
        }

        MemoryStream ms1 = new MemoryStream();
        fromstamper = iTextSharp.text.pdf.PdfStamper.CreateSignature(reader, ms1, '\0', null, true);

        var pk = store.GetKey(alias);
        List<Org.BouncyCastle.X509.X509Certificate> chain = store.GetCertificateChain(alias).Select(c => c.Certificate).ToList();

        var parameters = pk.Key as Org.BouncyCastle.Crypto.Parameters.RsaPrivateCrtKeyParameters;

        var appearance = fromstamper.SignatureAppearance;
        iTextSharp.text.pdf.security.IExternalSignature pks = new iTextSharp.text.pdf.security.PrivateKeySignature(parameters, iTextSharp.text.pdf.security.DigestAlgorithms.SHA256);
        iTextSharp.text.pdf.security.MakeSignature.SignDetached(appearance, pks, chain, null, null, null, 0, iTextSharp.text.pdf.security.CryptoStandard.CMS);
        ms1.Close();
        return ms1.ToArray();
    }

    private static int FindImageInPDFDictionary(PdfDictionary pg)
    {
        int image = 0;
        PdfDictionary res =
            (PdfDictionary)PdfReader.GetPdfObject(pg.Get(PdfName.RESOURCES));

        PdfDictionary xobj =
          (PdfDictionary)PdfReader.GetPdfObject(res.Get(PdfName.XOBJECT));
        if (xobj != null)
        {
            foreach (PdfName name in xobj.Keys)
            {
                PdfObject obj = xobj.Get(name);
                if (obj.IsIndirect())
                {
                    PdfDictionary tg = (PdfDictionary)PdfReader.GetPdfObject(obj);

                    PdfName type =
                      (PdfName)PdfReader.GetPdfObject(tg.Get(PdfName.SUBTYPE));

                    //image at the root of the pdf
                    if (PdfName.IMAGE.Equals(type))
                    {
                        image++;
                    }// image inside a form
                    else if (PdfName.FORM.Equals(type))
                    {
                        return FindImageInPDFDictionary(tg);
                    } //image inside a group
                    else if (PdfName.GROUP.Equals(type))
                    {
                        return FindImageInPDFDictionary(tg);
                    }
                }
            }
        }
        return image;
    }

    public static int countImage(byte[] pdf)
    {
        try
        {
            iTextSharp.text.pdf.PdfReader reader = new iTextSharp.text.pdf.PdfReader(pdf);
            //MemoryStream ms1 = new MemoryStream();

            //var stamper = new PdfStamper(reader, ms1);

            PdfDictionary pg = reader.GetPageN(reader.NumberOfPages);
            int count = FindImageInPDFDictionary(pg);

            return count;
        }
        catch (Exception) { return 0; }
    }

    public static bool savePDF(byte[] pdf, string transnum, string transtype, string operationtype)
    {
        try
        {
            string defaultfileName = "result.pdf";
            string filepath = HttpContext.Current.Server.MapPath("~/SupportingDocuments/" + operationtype + "/" + transnum + "/" + transtype + "/");
            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }
            using (var fs = new FileStream(filepath + defaultfileName, FileMode.Create, FileAccess.Write))
            {
                fs.Write(pdf, 0, pdf.Length);
                return true;
            }
        }
        catch (Exception)
        {
            return false;
        }
    }

    public static byte[] getPDF(string transnum, string transtype, string operationtype)
    {
        try
        {
            string defaultfileName = "result.pdf";
            string filepath = HttpContext.Current.Server.MapPath("~/SupportingDocuments/" + operationtype + "/" + transnum + "/" + transtype + "/");
            byte[] bytes = System.IO.File.ReadAllBytes(filepath + defaultfileName);
            return bytes;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public static bool IsBlankImage(System.Drawing.Image image)
    {
        bool isBlank = true;
        //Image img = Image.FromFile(path);
        Bitmap bmp = new Bitmap(image);
        for (int x = 0; x < bmp.Width && isBlank; x++)
            for (int y = 0; y < bmp.Height && isBlank; y++)
                if (bmp.GetPixel(x, y).Name != "ffffffff") isBlank = false;
        bmp.Dispose();
        image.Dispose();
        return isBlank;
    }

    public static int CountJpeg(byte[] pdf)
    {
        int count = 0;

        PdfReader reader = new PdfReader(pdf);
        PRStream pst;
        PdfObject po;
        PdfImageObject pio;
        // number of objects in pdf document
        int n = reader.XrefSize;
        //FileStream fs = null;
        // set image file location
        //String path = "E:/";
        for (int i = 0; i < n; i++)
        {
            // get the object at the index i in the objects collection
            po = reader.GetPdfObject(i);
            // object not found so continue
            if (po == null || !po.IsStream())
                continue;
            //cast object to stream
            pst = (PRStream)po;
            //get the object type
            PdfObject type = pst.Get(PdfName.SUBTYPE);
            //check if the object is the image type object
            if (type != null && type.ToString().Equals(PdfName.IMAGE.ToString()))
            {
                pio = new PdfImageObject(pst);
                // fs = new FileStream(path + "image" + i + ".jpg", FileMode.Create);
                //read bytes of image in to an array
                byte[] imgdata = pio.GetImageAsBytes();
                try
                {
                    using (Stream stream = new MemoryStream(imgdata))
                    {
                        if (!IsBlankImage(System.Drawing.Image.FromStream(stream)))
                        {
                            count++;
                        }
                    }
                }
                catch { }
            }
        }
        return count;
    }

    public static void SaveStreamToFile(Stream stream, int count)
    {
        if (stream.Length == 0) return;

        string filepath = HttpContext.Current.Server.MapPath("~/SupportingDocuments/GoodsReceive/2023-06-0008/IAR/result" + count + ".jpg");

        // Create a FileStream object to write a stream to a file
        using (FileStream fileStream = System.IO.File.Create(filepath, (int)stream.Length))
        {
            // Fill the bytes[] array with the stream data
            byte[] bytesInStream = new byte[stream.Length];
            stream.Read(bytesInStream, 0, (int)bytesInStream.Length);

            // Use FileStream object to write to the specified file
            fileStream.Write(bytesInStream, 0, bytesInStream.Length);
        }
    }

    public static bool checkPDF(string transnum, string transtype, string operationtype)
    {
        try
        {
            string defaultfileName = "result.pdf";
            string filepath = HttpContext.Current.Server.MapPath("~/SupportingDocuments/" + operationtype + "/" + transnum + "/" + transtype + "/");
            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }
            if (File.Exists(filepath + defaultfileName)) return true;
            else return false;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public static System.Drawing.Image ConvertImage(string base64)
    {
        base64 = base64.Split(',')[1];
        byte[] bytes = Convert.FromBase64String(base64);

        System.Drawing.Image image;
        using (MemoryStream ms = new MemoryStream(bytes))
        {
            image = System.Drawing.Image.FromStream(ms);
        }

        return image;
    }

    public static byte[] AddImage(byte[] pdf,
            string imagebase64,
            float positionX,
            float positionY,
            bool datestamp,
            float imagewidth,
            float imageheight,
            float fontsize,
            float textspace)
    {
        try
        {
            iTextSharp.text.pdf.PdfReader reader = new iTextSharp.text.pdf.PdfReader(pdf);
            MemoryStream ms1 = new MemoryStream();

            var stamper = new PdfStamper(reader, ms1);
            var pdfContentByte = stamper.GetOverContent(reader.NumberOfPages);
            iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(ConvertImage(imagebase64), System.Drawing.Imaging.ImageFormat.Png);
            //image.ScaleAbsolute(170f, 80f);
            image.ScaleAbsolute(imagewidth, imageheight);
            image.SetAbsolutePosition(positionX, positionY);

            pdfContentByte.AddImage(image);

            if (datestamp)
            {
                iTextSharp.text.Font font = new iTextSharp.text.Font();
                font.Size = fontsize;
                ColumnText.ShowTextAligned(pdfContentByte, Element.ALIGN_CENTER, new Phrase(DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss K"), font), (positionX + textspace), positionY, 0);
            }

            stamper.Close();
            ms1.Close();
            return ms1.ToArray();
        }
        catch (Exception ex)
        {
            string xx = ex.Message;
            return null;
        }
    }

    public static byte[] AddImageOnGRAIR(byte[] pdf, string imagebase64, bool datestamp)
    {
        try
        {
            int imageCount = CountJpeg(pdf);
            if (imageCount == 1) pdf = AddImage(pdf, imagebase64, 85, 100, datestamp, 170f, 80f, 12f, 85);
            else pdf = AddImage(pdf, imagebase64, 350, 100, datestamp, 170f, 80f, 12f, 85);
        }
        catch (Exception)
        {
        }
        return pdf;
    }

    public static byte[] AddImageOnPRICS(byte[] pdf, string imagebase64, bool datestamp)
    {
        try
        {
            int imageCount = CountJpeg(pdf);
            if (imageCount == 1) pdf = AddImage(pdf, imagebase64, 85, 130, datestamp, 170f, 80f, 12f, 85);
            else pdf = AddImage(pdf, imagebase64, 350, 130, datestamp, 170f, 80f, 12f, 85);
        }
        catch (Exception)
        {
        }
        return pdf;
    }

    public static byte[] AddImageOnPRITR(byte[] pdf, string imagebase64, bool datestamp)
    {
        try
        {
            int imageCount = CountJpeg(pdf);
            if (imageCount == 2)
            {
                pdf = AddImage(pdf, imagebase64, 130, 90, datestamp, 85f, 40f, 7f, 42);
            }
            else if (imageCount == 3)
            {
                pdf = AddImage(pdf, imagebase64, 285, 90, datestamp, 85f, 40f, 7f, 42);
            }
            else pdf = AddImage(pdf, imagebase64, 415, 90, datestamp, 85f, 40f, 7f, 42);
        }
        catch (Exception)
        {
        }
        return pdf;
    }

    public static byte[] AddImageOnPRRIS(byte[] pdf, string imagebase64, bool datestamp)
    {
        try
        {
            int imageCount = CountJpeg(pdf);
            if (imageCount == 2)
            {
                pdf = AddImage(pdf, imagebase64, 123, 80, datestamp, 85f, 40f, 7f, 42);
            }
            else if (imageCount == 3)
            {
                pdf = AddImage(pdf, imagebase64, 238, 80, datestamp, 85f, 40f, 7f, 42);
            }
            else if (imageCount == 4)
            {
                pdf = AddImage(pdf, imagebase64, 353, 80, datestamp, 85f, 40f, 7f, 42);
            }
            else pdf = AddImage(pdf, imagebase64, 458, 80, datestamp, 85f, 40f, 7f, 42);

            

        }
        catch (Exception)
        {
        }
        return pdf;
    }

    public static byte[] AddImageOnSIRIS(byte[] pdf, string imagebase64, bool datestamp)
    {
        try
        {
            int imageCount = CountJpeg(pdf);
            if (imageCount == 2)
            {
                pdf = AddImage(pdf, imagebase64, 123, 80, datestamp, 85f, 40f, 7f, 42);
            }
            else if (imageCount == 3)
            {
                pdf = AddImage(pdf, imagebase64, 238, 80, datestamp, 85f, 40f, 7f, 42);
            }
            else if (imageCount == 4)
            {
                pdf = AddImage(pdf, imagebase64, 353, 80, datestamp, 85f, 40f, 7f, 42);
            }
            else pdf = AddImage(pdf, imagebase64, 458, 80, datestamp, 85f, 40f, 7f, 42);
        }
        catch (Exception)
        {
        }
        return pdf;
    }

    public static byte[] AddImageOnSIITR(byte[] pdf, string imagebase64, bool datestamp)
    {
        try
        {
            int imageCount = CountJpeg(pdf);
            if (imageCount == 2)
            {
                pdf = AddImage(pdf, imagebase64, 130, 90, datestamp, 85f, 40f, 7f, 42);
            }
            else if (imageCount == 3)
            {
                pdf = AddImage(pdf, imagebase64, 285, 90, datestamp, 85f, 40f, 7f, 42);
            }
            else pdf = AddImage(pdf, imagebase64, 415, 90, datestamp, 85f, 40f, 7f, 42);
            
        }
        catch (Exception)
        {
        }
        return pdf;
    }

    public static byte[] AddImageOnSIRRIS(byte[] pdf, string imagebase64, bool datestamp)
    {
        try
        {
            int imageCount = CountJpeg(pdf);
            if (imageCount == 2)
            {
                pdf = AddImage(pdf, imagebase64, 123, 80, datestamp, 85f, 40f, 7f, 42);
            }
            else if (imageCount == 3)
            {
                pdf = AddImage(pdf, imagebase64, 238, 80, datestamp, 85f, 40f, 7f, 42);
            }
            else if (imageCount == 4)
            {
                pdf = AddImage(pdf, imagebase64, 353, 80, datestamp, 85f, 40f, 7f, 42);
            }
            else pdf = AddImage(pdf, imagebase64, 458, 80, datestamp, 85f, 40f, 7f, 42);
        }
        catch (Exception)
        {
        }
        return pdf;
    }

    //public static byte[] AddImageOnPRRIS(byte[] pdf, string imagebase64, bool datestamp)
    //{
    //    try
    //    {
    //        int imageCount = CountJpeg(pdf);
    //        if (imageCount == 1)
    //        {
    //            pdf = AddImage(pdf, imagebase64, 110, 110, datestamp, 85f, 40f, 7f, 42);
    //        }
    //        else if (imageCount == 2)
    //        {
    //            pdf = AddImage(pdf, imagebase64, 210, 110, datestamp, 85f, 40f, 7f, 42);
    //        }
    //        else if (imageCount == 3)
    //        {
    //            pdf = AddImage(pdf, imagebase64, 335, 110, datestamp, 85f, 40f, 7f, 42);
    //        }
    //        else pdf = AddImage(pdf, imagebase64, 465, 110, datestamp, 85f, 40f, 7f, 42);
    //    }
    //    catch (Exception)
    //    {
    //    }
    //    return pdf;
    //}

    public static byte[] AddImageOnPIICS(byte[] pdf, string imagebase64, bool datestamp)
    {
        try
        {
            int imageCount = CountJpeg(pdf);
            if (imageCount == 2) pdf = AddImage(pdf, imagebase64, 85, 140, datestamp, 170f, 80f, 12f, 85);
            else pdf = AddImage(pdf, imagebase64, 350, 140, datestamp, 170f, 80f, 12f, 85);
        }
        catch (Exception)
        {
        }
        return pdf;
    }

    public static byte[] AddImageOnPIRIS(byte[] pdf, string imagebase64, bool datestamp)
    {
        try
        {
            int imageCount = CountJpeg(pdf);
            if (imageCount == 2)
            {
                pdf = AddImage(pdf, imagebase64, 123, 80, datestamp, 85f, 40f, 7f, 42);
            }
            else if (imageCount == 3)
            {
                pdf = AddImage(pdf, imagebase64, 238, 80, datestamp, 85f, 40f, 7f, 42);
            }
            else if (imageCount == 4)
            {
                pdf = AddImage(pdf, imagebase64, 353, 80, datestamp, 85f, 40f, 7f, 42);
            }
            else pdf = AddImage(pdf, imagebase64, 458, 80, datestamp, 85f, 40f, 7f, 42);
            
        }
        catch (Exception)
        {
        }
        return pdf;
    }

    public static byte[] AddImageOnPRTIS(byte[] pdf, string imagebase64, bool datestamp)
    {
        try
        {
            int imageCount = CountJpeg(pdf);
            if (imageCount == 1) pdf = AddImage(pdf, imagebase64, 105, 135, datestamp, 114f, 54f, 7f, 50);
            else pdf = AddImage(pdf, imagebase64, 360, 135, datestamp, 114f, 54f, 7f, 50);
        }
        catch (Exception)
        {
        }
        return pdf;
    }

    public static byte[] AddImageOnPRRSEP(byte[] pdf, string imagebase64, bool datestamp)
    {
        try
        {
            int imageCount = CountJpeg(pdf);
            if (imageCount == 1) pdf = AddImage(pdf, imagebase64, 105, 125, datestamp, 114f, 54f, 7f, 50);
            else pdf = AddImage(pdf, imagebase64, 365, 125, datestamp, 114f, 54f, 7f, 50);
        }
        catch (Exception)
        {
        }
        return pdf;
    }

    public static byte[] AddImageOnPRTurnIn(byte[] pdf, string imagebase64, bool datestamp)
    {
        try
        {
            int imageCount = CountJpeg(pdf);
            if (imageCount == 1) pdf = AddImage(pdf, imagebase64, 105, 135, datestamp, 114f, 54f, 7f, 50);
            else pdf = AddImage(pdf, imagebase64, 365, 135, datestamp, 114f, 54f, 7f, 50);
        }
        catch (Exception)
        {
        }
        return pdf;
    }

    public static byte[] AddImageOnPRReturned(byte[] pdf, string imagebase64, bool datestamp)
    {
        try
        {
            int imageCount = CountJpeg(pdf);
            if (imageCount == 1) pdf = AddImage(pdf, imagebase64, 105, 135, datestamp, 114f, 54f, 7f, 50);
            else pdf = AddImage(pdf, imagebase64, 365, 135, datestamp, 114f, 54f, 7f, 50);
        }
        catch (Exception)
        {
        }
        return pdf;
    }

    public static byte[] AddImageOnGatePass(byte[] pdf, string imagebase64, bool datestamp)
    {
        try
        {
            int imageCount = CountJpeg(pdf);
            if (imageCount == 1)
            {
                pdf = AddImage(pdf, imagebase64, 115, 190, datestamp, 85f, 40f, 7f, 42);
            }
            else if (imageCount == 2)
            {
                pdf = AddImage(pdf, imagebase64, 385, 190, datestamp, 85f, 40f, 7f, 42);
            }
            else pdf = AddImage(pdf, imagebase64, 385, 80, datestamp, 85f, 40f, 7f, 42);
        }
        catch (Exception)
        {
        }
        return pdf;
    }

    public static byte[] AddImageOnPDWasteMaterial(byte[] pdf, string imagebase64, bool datestamp)
    {
        try
        {
            int imageCount = CountJpeg(pdf);
            if (imageCount == 1)
            {
                pdf = AddImage(pdf, imagebase64, 130, 270, datestamp, 85f, 40f, 7f, 42);
            }
            else if (imageCount == 2)
            {
                pdf = AddImage(pdf, imagebase64, 380, 270, datestamp, 85f, 40f, 7f, 42);
            }
            else if (imageCount == 3)
            {
                pdf = AddImage(pdf, imagebase64, 130, 90, datestamp, 85f, 40f, 7f, 42);
            }
            else pdf = AddImage(pdf, imagebase64, 380, 90, datestamp, 85f, 40f, 7f, 42);
        }
        catch (Exception)
        {
        }
        return pdf;
    }

    public static byte[] AddImageOnPDDisposalForm(byte[] pdf, string imagebase64, bool datestamp)
    {
        try
        {
            int imageCount = CountJpeg(pdf);
            if (imageCount == 2) pdf = AddImage(pdf, imagebase64, 105, 90, datestamp, 114f, 54f, 7f, 50);
            else pdf = AddImage(pdf, imagebase64, 365, 90, datestamp, 114f, 54f, 7f, 50);
        }
        catch (Exception)
        {
        }
        return pdf;
    }

    public static byte[] AddImageOnPDROSEP(byte[] pdf, string imagebase64, bool datestamp)
    {
        try
        {
            int imageCount = CountJpeg(pdf);
            if (imageCount == 1) pdf = AddImage(pdf, imagebase64, 145, 200, datestamp, 114f, 54f, 7f, 50);
            else pdf = AddImage(pdf, imagebase64, 410, 195, datestamp, 114f, 54f, 7f, 50);
        }
        catch (Exception)
        {
        }
        return pdf;
    }

    public static byte[] AddImageOnPDRSEP(byte[] pdf, string imagebase64, bool datestamp)
    {
        try
        {
            int imageCount = CountJpeg(pdf);
            if (imageCount == 1)
            {
                pdf = AddImage(pdf, imagebase64, 60, 90, datestamp, 85f, 40f, 7f, 42);
            }
            else if (imageCount == 2)
            {
                pdf = AddImage(pdf, imagebase64, 210, 90, datestamp, 85f, 40f, 7f, 42);
            }
            else if (imageCount == 3)
            {
                pdf = AddImage(pdf, imagebase64, 340, 110, datestamp, 85f, 40f, 7f, 42);
            }
            else pdf = AddImage(pdf, imagebase64, 460, 110, datestamp, 85f, 40f, 7f, 42);
        }
        catch (Exception)
        {
        }
        return pdf;
    }

    public static byte[] AddImageOnSDDisposalForm(byte[] pdf, string imagebase64, bool datestamp)
    {
        try
        {
            int imageCount = CountJpeg(pdf);
            if (imageCount == 2) pdf = AddImage(pdf, imagebase64, 110, 70, datestamp, 114f, 54f, 7f, 50);
            else pdf = AddImage(pdf, imagebase64, 370, 70, datestamp, 114f, 54f, 7f, 50);
        }
        catch (Exception)
        {
        }
        return pdf;
    }

    public static byte[] AddImageOnSDWasteMaterial(byte[] pdf, string imagebase64, bool datestamp)
    {
        try
        {
            int imageCount = CountJpeg(pdf);
            if (imageCount == 1) pdf = AddImage(pdf, imagebase64, 110, 245, datestamp, 114f, 54f, 7f, 50);
            else if (imageCount == 2) pdf = AddImage(pdf, imagebase64, 370, 245, datestamp, 114f, 54f, 7f, 50);
            else if (imageCount == 3) pdf = AddImage(pdf, imagebase64, 110, 65, datestamp, 114f, 54f, 7f, 50);
            else pdf = AddImage(pdf, imagebase64, 370, 65, datestamp, 114f, 54f, 7f, 50);
        }
        catch (Exception)
        {
        }
        return pdf;
    }

    public static byte[] AddImageOnRSMIRSMI(byte[] pdf, string imagebase64, bool datestamp)
    {
        try
        {
            int imageCount = CountJpeg(pdf);
            if (imageCount == 1) pdf = AddImage(pdf, imagebase64, 110, 65, datestamp, 114f, 54f, 7f, 50);
            else pdf = AddImage(pdf, imagebase64, 370, 65, datestamp, 114f, 54f, 7f, 50);
        }
        catch (Exception)
        {
        }
        return pdf;
    }

    public static byte[] AddImageOnPTITR(byte[] pdf, string imagebase64, bool datestamp)
    {
        try
        {
            int imageCount = CountJpeg(pdf);
            if (imageCount == 2)
            {
                pdf = AddImage(pdf, imagebase64, 130, 90, datestamp, 85f, 40f, 7f, 42);
            }
            else if (imageCount == 3)
            {
                pdf = AddImage(pdf, imagebase64, 285, 90, datestamp, 85f, 40f, 7f, 42);
            }
            else pdf = AddImage(pdf, imagebase64, 415, 90, datestamp, 85f, 40f, 7f, 42);
            
        }
        catch (Exception)
        {
        }
        return pdf;
    }

    public static string CreatePDF(string html,
        string imagebase64,
        string certbase64,
        string passphrase,
        bool datestamp,
        string transnum,
        string transtype,
        string operationtype
        )
    {
        try
        {
            byte[] pdf;
            if (checkPDF(transnum, transtype, operationtype))
            {//meron na
                pdf = getPDF(transnum, transtype, operationtype);
                pdf = PDFConverter.AddSignature(pdf, certbase64, passphrase);
                if (transtype == "IAR" && operationtype == "GoodsReceive")
                {
                    pdf = AddImageOnGRAIR(pdf, imagebase64, datestamp);
                }
                else if (transtype == "ICS" && operationtype == "PropertyIssuance")
                {
                    pdf = AddImageOnPIICS(pdf, imagebase64, datestamp);
                }
                else if (transtype == "RIS" && operationtype == "PropertyIssuance")
                {
                    pdf = AddImageOnPIRIS(pdf, imagebase64, datestamp);
                }
                else if (transtype == "ICS" && operationtype == "PropertyRequest")
                {
                    pdf = AddImageOnPRICS(pdf, imagebase64, datestamp);
                }
                else if (transtype == "ITR" && operationtype == "PropertyRequest")
                {
                    pdf = AddImageOnPRITR(pdf, imagebase64, datestamp);
                }
                else if (transtype == "RIS" && operationtype == "PropertyRequest")
                {
                    pdf = AddImageOnPRRIS(pdf, imagebase64, datestamp);
                }
                else if (transtype == "RIS" && operationtype == "StockIssuance")
                {
                    pdf = AddImageOnSIRIS(pdf, imagebase64, datestamp);
                }
                else if (transtype == "ITR" && operationtype == "StockIssuance")
                {
                    pdf = AddImageOnSIITR(pdf, imagebase64, datestamp);
                }
                else if (transtype == "RIS" && operationtype == "StockRequest")
                {
                    pdf = AddImageOnSIRRIS(pdf, imagebase64, datestamp);
                }
                else if (transtype == "TIS" && operationtype == "PropertyReturn")
                {
                    pdf = AddImageOnPRTIS(pdf, imagebase64, datestamp);
                }
                else if (transtype == "RSEP" && operationtype == "PropertyReturn")
                {
                    pdf = AddImageOnPRRSEP(pdf, imagebase64, datestamp);
                }
                else if (transtype == "Turn In" && operationtype == "PropertyReturnRequest")
                {
                    pdf = AddImageOnPRTurnIn(pdf, imagebase64, datestamp);
                }
                else if (transtype == "Returned" && operationtype == "PropertyReturnRequest")
                {
                    pdf = AddImageOnPRReturned(pdf, imagebase64, datestamp);
                }
                else if (transtype == "GatePass" && operationtype == "GatePassRequest")
                {
                    pdf = AddImageOnGatePass(pdf, imagebase64, datestamp);
                }
                else if (transtype == "WasteMaterial" && operationtype == "PropertyDisposal")
                {
                    pdf = AddImageOnPDWasteMaterial(pdf, imagebase64, datestamp);
                }
                else if (transtype == "DisposalForm" && operationtype == "PropertyDisposal")
                {
                    pdf = AddImageOnPDDisposalForm(pdf, imagebase64, datestamp);
                }
                else if (transtype == "ROSEP" && operationtype == "PropertyDisposal")
                {
                    pdf = AddImageOnPDROSEP(pdf, imagebase64, datestamp);
                }
                else if (transtype == "RSEP" && operationtype == "PropertyDisposal")
                {
                    pdf = AddImageOnPDRSEP(pdf, imagebase64, datestamp);
                }
                else if (transtype == "Disposal Form" && operationtype == "StockDisposal")
                {
                    pdf = AddImageOnSDDisposalForm(pdf, imagebase64, datestamp);
                }
                else if (transtype == "WasteMaterial" && operationtype == "StockDisposal")
                {
                    pdf = AddImageOnSDWasteMaterial(pdf, imagebase64, datestamp);
                }
                else if (transtype == "RSMI" && operationtype == "RSMI")
                {
                    pdf = AddImageOnRSMIRSMI(pdf, imagebase64, datestamp);
                }
                else if (transtype == "ITR" && operationtype == "PropertyTransfer")
                {
                    pdf = AddImageOnPTITR(pdf, imagebase64, datestamp);
                }
            }
            else
            {//wala pa
                pdf = PDFConverter.ConvertHTMLtoPDF(html, PaperSize.A4, 1.25.Centimeters(), PDFConverter.PageOrientation.Portrait);
                pdf = PDFConverter.AddSignature(pdf, certbase64, passphrase);
                if (transtype == "IAR" && operationtype == "GoodsReceive")
                {
                    pdf = AddImageOnGRAIR(pdf, imagebase64, datestamp);
                }
                else if (transtype == "ICS" && operationtype == "PropertyIssuance")
                {
                    pdf = AddImageOnPIICS(pdf, imagebase64, datestamp);
                }
                else if (transtype == "RIS" && operationtype == "PropertyIssuance")
                {
                    pdf = AddImageOnPIRIS(pdf, imagebase64, datestamp);
                }
                else if (transtype == "ICS" && operationtype == "PropertyRequest")
                {
                    pdf = AddImageOnPRICS(pdf, imagebase64, datestamp);
                }
                else if (transtype == "ITR" && operationtype == "PropertyRequest")
                {
                    pdf = AddImageOnPRITR(pdf, imagebase64, datestamp);
                }
                else if (transtype == "RIS" && operationtype == "PropertyRequest")
                {
                    pdf = AddImageOnPRRIS(pdf, imagebase64, datestamp);
                }
                else if (transtype == "RIS" && operationtype == "StockIssuance")
                {
                    pdf = AddImageOnSIRIS(pdf, imagebase64, datestamp);
                }
                else if (transtype == "ITR" && operationtype == "StockIssuance")
                {
                    pdf = AddImageOnSIITR(pdf, imagebase64, datestamp);
                }
                else if (transtype == "RIS" && operationtype == "StockRequest")
                {
                    pdf = AddImageOnSIRRIS(pdf, imagebase64, datestamp);
                }
                else if (transtype == "TIS" && operationtype == "PropertyReturn")
                {
                    pdf = AddImageOnPRTIS(pdf, imagebase64, datestamp);
                }
                else if (transtype == "RSEP" && operationtype == "PropertyReturn")
                {
                    pdf = AddImageOnPRRSEP(pdf, imagebase64, datestamp);
                }
                else if (transtype == "Turn In" && operationtype == "PropertyReturnRequest")
                {
                    pdf = AddImageOnPRTurnIn(pdf, imagebase64, datestamp);
                }
                else if (transtype == "Returned" && operationtype == "PropertyReturnRequest")
                {
                    pdf = AddImageOnPRReturned(pdf, imagebase64, datestamp);
                }
                else if (transtype == "GatePass" && operationtype == "GatePassRequest")
                {
                    pdf = AddImageOnGatePass(pdf, imagebase64, datestamp);
                }
                else if (transtype == "WasteMaterial" && operationtype == "PropertyDisposal")
                {
                    pdf = AddImageOnPDWasteMaterial(pdf, imagebase64, datestamp);
                }
                else if (transtype == "DisposalForm" && operationtype == "PropertyDisposal")
                {
                    pdf = AddImageOnPDDisposalForm(pdf, imagebase64, datestamp);
                }
                else if (transtype == "ROSEP" && operationtype == "PropertyDisposal")
                {
                    pdf = AddImageOnPDROSEP(pdf, imagebase64, datestamp);
                }
                else if (transtype == "RSEP" && operationtype == "PropertyDisposal")
                {
                    pdf = AddImageOnPDRSEP(pdf, imagebase64, datestamp);
                }
                else if (transtype == "Disposal Form" && operationtype == "StockDisposal")
                {
                    pdf = AddImageOnSDDisposalForm(pdf, imagebase64, datestamp);
                }
                else if (transtype == "WasteMaterial" && operationtype == "StockDisposal")
                {
                    pdf = AddImageOnSDWasteMaterial(pdf, imagebase64, datestamp);
                }
                else if (transtype == "RSMI" && operationtype == "RSMI")
                {
                    pdf = AddImageOnRSMIRSMI(pdf, imagebase64, datestamp);
                }
                else if (transtype == "ITR" && operationtype == "PropertyTransfer")
                {
                    pdf = AddImageOnPTITR(pdf, imagebase64, datestamp);
                }
            }
            savePDF(pdf, transnum, transtype, operationtype);
            return Convert.ToBase64String(pdf, 0, pdf.Length);
        }
        catch (Exception ex)
        {
            if (ex.Message == "PKCS12 key store MAC invalid - wrong password or corrupted file.")
            {
                return "Invalid Pass Phrase";
            }
            else
            {
                return "Error";
            }
        }
    }
}