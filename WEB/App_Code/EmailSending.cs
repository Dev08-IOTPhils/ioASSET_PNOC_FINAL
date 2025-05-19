using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Web;

/// <summary>
/// Summary description for EmailSending
/// </summary>
public class EmailSending
{
    public static void SendEmail(string smtp_, string email, string subject, string body)
    {
        try {
            string[] smtp = smtp_.Split('|');
            string smtphost = smtp[0];
            string smtpport = smtp[1];
            string smtpmail = smtp[2];
            string smtppass = smtp[3];
            MailMessage mail = new MailMessage();
            mail.To.Add(email);

            mail.From = new MailAddress(smtpmail);
            mail.Subject = subject;
            string Body = "<font face='Consolas'><br><br><i>***This is a system generated mail. Please do not reply.***</i><br><br><br>" + body;
            Body += "<br><br><br><i>***This is a system generated mail. Please do not reply.***</i></font>";

            mail.Body = Body;
            SmtpClient smtpMail = new SmtpClient();
            smtpMail.Host = smtphost; //Or Your SMTP Server Address
            smtpMail.Port = Convert.ToInt32(smtpport);
            smtpMail.UseDefaultCredentials = false;
            smtpMail.Credentials = new System.Net.NetworkCredential
            (smtpmail, smtppass);

            //Or your Smtp Email ID and Password
            mail.IsBodyHtml = true;
            smtpMail.EnableSsl = true;

            ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
            smtpMail.Send(mail); }
        catch(Exception ex)
        {
            throw ex;
        }
    }
}