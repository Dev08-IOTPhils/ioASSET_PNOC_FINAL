using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AdvanceEntity;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using AdvanceBll;
using AdvanceDal;

/// <summary>
/// Summary description for Common
/// </summary>
public static class Common
{
    //public static string UserName { get; set; }
    public static string Category { get; set; }
    public static string TempUserID { get; set; }
    //public static int Role { get; set; }
    public static string Users { get; set; }
    public class LogOnUser
    {
        public string UserId { get; set; }
        public string SessionId { get; set; }
        public DateTime LastActivityDate { get; set; }

        public LogOnUser(string userId, string sessionId)
        {
            UserId = userId;
            SessionId = sessionId;
            LastActivityDate = DateTime.Now;
        }
    }

    #region Session Variables

    public static string FirstLogin
    {
        get
        {
            if (HttpContext.Current.Session["FirstLogin"] == null)
                HttpContext.Current.Session["FirstLogin"] = "";

            return HttpContext.Current.Session["FirstLogin"] as string;
        }
        set
        {
            HttpContext.Current.Session["FirstLogin"] = value;
        }
    }
    public static string ModuleData
    {
        get
        {
            if (HttpContext.Current.Session["ModuleData"] == null)
                HttpContext.Current.Session["ModuleData"] = "";

            return HttpContext.Current.Session["ModuleData"] as string;
        }
        set
        {
            HttpContext.Current.Session["ModuleData"] = value;
        }
    }

    public static string ConnString
    {
        get
        {
            if (HttpContext.Current.Session["ConnString"] == null)
                HttpContext.Current.Session["ConnString"] = "";

            return HttpContext.Current.Session["ConnString"] as string;
        }
        set
        {
            HttpContext.Current.Session["ConnString"] = value;
        }
    }

    public static bool IsUserAuthenticated
    {
        get
        {
            if (HttpContext.Current.Session["IsUserAuthenticated"] == null)
                HttpContext.Current.Session["IsUserAuthenticated"] = false;

            return (bool)HttpContext.Current.Session["IsUserAuthenticated"];
        }
        set
        {
            HttpContext.Current.Session["IsUserAuthenticated"] = value;
        }
    }

    public static string UserID
    {
        get
        {
            if (HttpContext.Current.Session["UserID"] == null)
                HttpContext.Current.Session["UserID"] = "";

            return HttpContext.Current.Session["UserID"] as string;
        }
        set
        {
            HttpContext.Current.Session["UserID"] = value;
        }
    }

    public static string Role
    {
        get
        {
            if (HttpContext.Current.Session["Role"] == null)
                HttpContext.Current.Session["Role"] = "";
            return HttpContext.Current.Session["Role"] as string;
        }
        set
        {
            HttpContext.Current.Session["Role"] = value;
        }
    }

    public static string UserName
    {
        get
        {
            if (HttpContext.Current.Session["UserName"] == null)
                HttpContext.Current.Session["UserName"] = "";

            return HttpContext.Current.Session["UserName"] as string;
        }
        set
        {
            HttpContext.Current.Session["UserName"] = value;
        }
    }

    public static string Password
    {
        get
        {
            if (HttpContext.Current.Session["Password"] == null)
                HttpContext.Current.Session["Password"] = "";

            return HttpContext.Current.Session["Password"] as string;
        }
        set
        {
            HttpContext.Current.Session["Password"] = value;
        }
    }

    public static string LobID
    {
        get
        {
            if (HttpContext.Current.Session["LobID"] == null)
                HttpContext.Current.Session["LobID"] = "";

            return HttpContext.Current.Session["LobID"] as string;
        }
        set
        {
            HttpContext.Current.Session["LobID"] = value;
        }
    }
    public static string Email
    {
        get
        {
            if (HttpContext.Current.Session["Email"] == null)
                HttpContext.Current.Session["Email"] = "";

            return HttpContext.Current.Session["Email"] as string;
        }
        set
        {
            HttpContext.Current.Session["Email"] = value;
        }
    }

    public static string CreatedDate
    {
        get
        {
            if (HttpContext.Current.Session["CreatedDate"] == null)
                HttpContext.Current.Session["CreatedDate"] = "";

            return HttpContext.Current.Session["CreatedDate"] as string;
        }
        set
        {
            HttpContext.Current.Session["CreatedDate"] = value;
        }
    }

    public static string IsCustodian
    {
        get
        {
            if (HttpContext.Current.Session["IsCustodian"] == null)
                HttpContext.Current.Session["IsCustodian"] = "";

            return HttpContext.Current.Session["IsCustodian"] as string;
        }
        set
        {
            HttpContext.Current.Session["IsCustodian"] = value;
        }
    }

    public static string Administrative
    {
        get
        {
            if (HttpContext.Current.Session["Administrative"] == null)
                HttpContext.Current.Session["Administrative"] = "";

            return HttpContext.Current.Session["Administrative"] as string;
        }
        set
        {
            HttpContext.Current.Session["Administrative"] = value;
        }
    }

    public static string IsApprover
    {
        get
        {
            if (HttpContext.Current.Session["IsApprover"] == null)
                HttpContext.Current.Session["IsApprover"] = "";

            return HttpContext.Current.Session["IsApprover"] as string;
        }
        set
        {
            HttpContext.Current.Session["IsApprover"] = value;
        }
    }

    public static string IsInspector
    {
        get
        {
            if (HttpContext.Current.Session["IsInspector"] == null)
                HttpContext.Current.Session["IsInspector"] = "";

            return HttpContext.Current.Session["IsInspector"] as string;
        }
        set
        {
            HttpContext.Current.Session["IsInspector"] = value;
        }
    }

    public static string IsCertifier
    {
        get
        {
            if (HttpContext.Current.Session["IsCertifier"] == null)
                HttpContext.Current.Session["IsCertifier"] = "";

            return HttpContext.Current.Session["IsCertifier"] as string;
        }
        set
        {
            HttpContext.Current.Session["IsCertifier"] = value;
        }
    }


    public static string FullName
    {
        get
        {
            if (HttpContext.Current.Session["FullName"] == null)
                HttpContext.Current.Session["FullName"] = "";

            return HttpContext.Current.Session["FullName"] as string;
        }
        set
        {
            HttpContext.Current.Session["FullName"] = value;
        }
    }

    public static string Image
    {
        get
        {
            if (HttpContext.Current.Session["Image"] == null)
                HttpContext.Current.Session["Image"] = "";

            return HttpContext.Current.Session["Image"] as string;
        }
        set
        {
            HttpContext.Current.Session["Image"] = value;
        }
    }

    //added by nero 20220711
    public static string Signature
    {
        get
        {
            if (HttpContext.Current.Session["Signature"] == null)
                HttpContext.Current.Session["Signature"] = "";

            return HttpContext.Current.Session["Signature"] as string;
        }
        set
        {
            HttpContext.Current.Session["Signature"] = value;
        }
    }


    public static string CustodianID
    {
        get
        {
            if (HttpContext.Current.Session["CustodianID"] == null)
                HttpContext.Current.Session["CustodianID"] = "";

            return HttpContext.Current.Session["CustodianID"] as string;
        }
        set
        {
            HttpContext.Current.Session["CustodianID"] = value;
        }
    }

    public static string SiteID
    {
        get
        {
            if (HttpContext.Current.Session["SiteID"] == null)
                HttpContext.Current.Session["SiteID"] = "";

            return HttpContext.Current.Session["SiteID"] as string;
        }
        set
        {
            HttpContext.Current.Session["SiteID"] = value;
        }
    }

    public static string RoleId
    {
        get
        {
            if (HttpContext.Current.Session["RoleId"] == null)
                HttpContext.Current.Session["RoleId"] = "";

            return HttpContext.Current.Session["RoleId"] as string;
        }
        set
        {
            HttpContext.Current.Session["RoleId"] = value;
        }
    }

    public static List<LogOnUser> ActiveUsers
    {
        get
        {
            if (HttpContext.Current.Application["ActiveUsers"] == null)
                HttpContext.Current.Application["ActiveUsers"] = new List<LogOnUser>();

            return HttpContext.Current.Application["ActiveUsers"] as List<LogOnUser>;
        }
    }

    #endregion

    #region Miscellaneous System Settings
    public static string CreationApproval
    {
        get
        {
            if (HttpContext.Current.Session["CreationApproval"] == null)
                HttpContext.Current.Session["CreationApproval"] = "";

            return HttpContext.Current.Session["CreationApproval"] as string;
        }
        set
        {
            HttpContext.Current.Session["CreationApproval"] = value;
        }
    }

    public static string PropertyNumberGeneration
    {
        get
        {
            if (HttpContext.Current.Session["PropertyNumberGeneration"] == null)
                HttpContext.Current.Session["PropertyNumberGeneration"] = "";

            return HttpContext.Current.Session["PropertyNumberGeneration"] as string;
        }
        set
        {
            HttpContext.Current.Session["PropertyNumberGeneration"] = value;
        }
    }

    public static string MergeStockApproval
    {
        get
        {
            if (HttpContext.Current.Session["MergeStockApproval"] == null)
                HttpContext.Current.Session["MergeStockApproval"] = "";

            return HttpContext.Current.Session["MergeStockApproval"] as string;
        }
        set
        {
            HttpContext.Current.Session["MergeStockApproval"] = value;
        }
    }

    public static string IssuanceApproval
    {
        get
        {
            if (HttpContext.Current.Session["IssuanceApproval"] == null)
                HttpContext.Current.Session["IssuanceApproval"] = "";

            return HttpContext.Current.Session["IssuanceApproval"] as string;
        }
        set
        {
            HttpContext.Current.Session["IssuanceApproval"] = value;
        }
    }

    public static string TransferApproval
    {
        get
        {
            if (HttpContext.Current.Session["TransferApproval"] == null)
                HttpContext.Current.Session["TransferApproval"] = "";

            return HttpContext.Current.Session["TransferApproval"] as string;
        }
        set
        {
            HttpContext.Current.Session["TransferApproval"] = value;
        }
    }

    public static string SupplyAllocationApproval
    {
        get
        {
            if (HttpContext.Current.Session["SupplyAllocation"] == null)
                HttpContext.Current.Session["SupplyAllocation"] = "";

            return HttpContext.Current.Session["SupplyAllocation"] as string;
        }
        set
        {
            HttpContext.Current.Session["SupplyAllocation"] = value;
        }
    }

    public static string TurnOverApproval
    {
        get
        {
            if (HttpContext.Current.Session["TurnOverApproval"] == null)
                HttpContext.Current.Session["TurnOverApproval"] = "";

            return HttpContext.Current.Session["TurnOverApproval"] as string;
        }
        set
        {
            HttpContext.Current.Session["TurnOverApproval"] = value;
        }
    }

    public static string DisposalApproval
    {
        get
        {
            if (HttpContext.Current.Session["DisposalApproval"] == null)
                HttpContext.Current.Session["DisposalApproval"] = "";

            return HttpContext.Current.Session["DisposalApproval"] as string;
        }
        set
        {
            HttpContext.Current.Session["DisposalApproval"] = value;
        }
    }
    public static string StockDisposalApproval
    {
        get
        {
            if (HttpContext.Current.Session["StockDisposalApproval"] == null)
                HttpContext.Current.Session["StockDisposalApproval"] = "";

            return HttpContext.Current.Session["StockDisposalApproval"] as string;
        }
        set
        {
            HttpContext.Current.Session["StockDisposalApproval"] = value;
        }
    }

    public static string StockIssuanceApproval
    {
        get
        {
            if (HttpContext.Current.Session["StockIssuanceApproval"] == null)
                HttpContext.Current.Session["StockIssuanceApproval"] = "";

            return HttpContext.Current.Session["StockIssuanceApproval"] as string;
        }
        set
        {
            HttpContext.Current.Session["StockIssuanceApproval"] = value;
        }
    }

    public static string GatePassApproval
    {
        get
        {
            if (HttpContext.Current.Session["GatePassApproval"] == null)
                HttpContext.Current.Session["GatePassApproval"] = "";

            return HttpContext.Current.Session["GatePassApproval"] as string;
        }
        set
        {
            HttpContext.Current.Session["GatePassApproval"] = value;
        }
    }



    public static string DataUploadApproval
    {
        get
        {
            if (HttpContext.Current.Session["DataUploadApproval"] == null)
                HttpContext.Current.Session["DataUploadApproval"] = "";

            return HttpContext.Current.Session["DataUploadApproval"] as string;
        }
        set
        {
            HttpContext.Current.Session["DataUploadApproval"] = value;
        }
    }

    public static string DatabaseRestoreApproval
    {
        get
        {
            if (HttpContext.Current.Session["DatabaseRestoreApproval"] == null)
                HttpContext.Current.Session["DatabaseRestoreApproval"] = "";

            return HttpContext.Current.Session["DatabaseRestoreApproval"] as string;
        }
        set
        {
            HttpContext.Current.Session["DatabaseRestoreApproval"] = value;
        }
    }

    public static string VerifiyCreation
    {
        get
        {
            if (HttpContext.Current.Session["VerifiyCreation"] == null)
                HttpContext.Current.Session["VerifiyCreation"] = "";

            return HttpContext.Current.Session["VerifiyCreation"] as string;
        }
        set
        {
            HttpContext.Current.Session["VerifiyCreation"] = value;
        }
    }

    public static string VerifyIssuance
    {
        get
        {
            if (HttpContext.Current.Session["VerifyIssuance"] == null)
                HttpContext.Current.Session["VerifyIssuance"] = "";

            return HttpContext.Current.Session["VerifyIssuance"] as string;
        }
        set
        {
            HttpContext.Current.Session["VerifyIssuance"] = value;
        }
    }

    public static string VerifyTransfer
    {
        get
        {
            if (HttpContext.Current.Session["VerifyTransfer"] == null)
                HttpContext.Current.Session["VerifyTransfer"] = "";

            return HttpContext.Current.Session["VerifyTransfer"] as string;
        }
        set
        {
            HttpContext.Current.Session["VerifyTransfer"] = value;
        }
    }

    public static string StockTaking
    {
        get
        {
            if (HttpContext.Current.Session["StockTaking"] == null)
                HttpContext.Current.Session["StockTaking"] = "";

            return HttpContext.Current.Session["StockTaking"] as string;
        }
        set
        {
            HttpContext.Current.Session["StockTaking"] = value;
        }
    }

    public static string RFIDPrintingCreation
    {
        get
        {
            if (HttpContext.Current.Session["RFIDPrintingCreation"] == null)
                HttpContext.Current.Session["RFIDPrintingCreation"] = "";

            return HttpContext.Current.Session["RFIDPrintingCreation"] as string;
        }
        set
        {
            HttpContext.Current.Session["RFIDPrintingCreation"] = value;
        }
    }

    public static string RequestApproval
    {
        get
        {
            if (HttpContext.Current.Session["RequestApproval"] == null)
                HttpContext.Current.Session["RequestApproval"] = "";

            return HttpContext.Current.Session["RequestApproval"] as string;
        }
        set
        {
            HttpContext.Current.Session["RequestApproval"] = value;
        }
    }

    public static string VerifyRequest
    {
        get
        {
            if (HttpContext.Current.Session["VerifyRequest"] == null)
                HttpContext.Current.Session["VerifyRequest"] = "";

            return HttpContext.Current.Session["VerifyRequest"] as string;
        }
        set
        {
            HttpContext.Current.Session["VerifyRequest"] = value;
        }
    }


    public static string VerifyReturnRequest
    {
        get
        {
            if (HttpContext.Current.Session["VerifyReturnRequest"] == null)
                HttpContext.Current.Session["VerifyReturnRequest"] = "";

            return HttpContext.Current.Session["VerifyReturnRequest"] as string;
        }
        set
        {
            HttpContext.Current.Session["VerifyReturnRequest"] = value;
        }
    }

    public static string VerifyDisposal
    {
        get
        {
            if (HttpContext.Current.Session["VerifyDisposal"] == null)
                HttpContext.Current.Session["VerifyDisposal"] = "";

            return HttpContext.Current.Session["VerifyDisposal"] as string;
        }
        set
        {
            HttpContext.Current.Session["VerifyDisposal"] = value;
        }
    }

    //added by nero 20220517
    public static string IssuanceAcknowledgement
    {
        get
        {
            if (HttpContext.Current.Session["IssuanceAcknowledgement"] == null)
                HttpContext.Current.Session["IssuanceAcknowledgement"] = "";

            return HttpContext.Current.Session["IssuanceAcknowledgement"] as string;
        }
        set
        {
            HttpContext.Current.Session["IssuanceAcknowledgement"] = value;
        }
    }

    public static string RetaggingApproval
    {
        get
        {
            if (HttpContext.Current.Session["RetaggingApproval"] == null)
                HttpContext.Current.Session["RetaggingApproval"] = "";

            return HttpContext.Current.Session["RetaggingApproval"] as string;
        }
        set
        {
            HttpContext.Current.Session["RetaggingApproval"] = value;
        }
    }
    
    public static string StockRemainingQuantity
    {
        get
        {
            if (HttpContext.Current.Session["StockRemainingQuantity"] == null)
                HttpContext.Current.Session["StockRemainingQuantity"] = "";

            return HttpContext.Current.Session["StockRemainingQuantity"] as string;
        }
        set
        {
            HttpContext.Current.Session["StockRemainingQuantity"] = value;
        }
    }

    public static string CompanyInfo
    {
        get
        {
            if (HttpContext.Current.Session["CompanyInfo"] == null)
                HttpContext.Current.Session["CompanyInfo"] = "";

            return HttpContext.Current.Session["CompanyInfo"] as string;
        }
        set
        {
            HttpContext.Current.Session["CompanyInfo"] = value;
        }
    }
    public static string VerificationMode
    {
        get
        {
            if (HttpContext.Current.Session["VerificationMode"] == null)
                HttpContext.Current.Session["VerificationMode"] = "";

            return HttpContext.Current.Session["VerificationMode"] as string;
        }
        set
        {
            HttpContext.Current.Session["VerificationMode"] = value;
        }
    }

    public static string PARValue
    {
        get
        {
            if (HttpContext.Current.Session["PAR"] == null)
                HttpContext.Current.Session["PAR"] = "";

            return HttpContext.Current.Session["PAR"] as string;
        }
        set
        {
            HttpContext.Current.Session["PAR"] = value;
        }
    }

    public static string ICSValue
    {
        get
        {
            if (HttpContext.Current.Session["ICS"] == null)
                HttpContext.Current.Session["ICS"] = "";

            return HttpContext.Current.Session["ICS"] as string;
        }
        set
        {
            HttpContext.Current.Session["ICS"] = value;
        }
    }
    #endregion
    #region UAC
    public static string noofresu
    {
        get
        {
            if (HttpContext.Current.Session["noofresu"] == null)
                HttpContext.Current.Session["noofresu"] = "";

            return HttpContext.Current.Session["noofresu"] as string;
        }
        set
        {
            HttpContext.Current.Session["noofresu"] = value;
        }
    }

    public static string noofminda
    {
        get
        {
            if (HttpContext.Current.Session["noofminda"] == null)
                HttpContext.Current.Session["noofminda"] = "";

            return HttpContext.Current.Session["noofminda"] as string;
        }
        set
        {
            HttpContext.Current.Session["noofminda"] = value;
        }
    }

    public static string noofapprover
    {
        get
        {
            if (HttpContext.Current.Session["noofapprover"] == null)
                HttpContext.Current.Session["noofapprover"] = "";

            return HttpContext.Current.Session["noofapprover"] as string;
        }
        set
        {
            HttpContext.Current.Session["noofapprover"] = value;
        }
    }

    public static string nooftsuc
    {
        get
        {
            if (HttpContext.Current.Session["nooftsuc"] == null)
                HttpContext.Current.Session["nooftsuc"] = "";

            return HttpContext.Current.Session["nooftsuc"] as string;
        }
        set
        {
            HttpContext.Current.Session["nooftsuc"] = value;
        }
    }
    #endregion

    #region SMTP MAIL
    public static string SMTPmail
    {
        get
        {
            if (HttpContext.Current.Session["SMTPmail"] == null)
                HttpContext.Current.Session["SMTPmail"] = "";

            return HttpContext.Current.Session["SMTPmail"] as string;
        }
        set
        {
            HttpContext.Current.Session["SMTPmail"] = value;
        }
    }

    public static string SMTPpass
    {
        get
        {
            if (HttpContext.Current.Session["SMTPpass"] == null)
                HttpContext.Current.Session["SMTPpass"] = "";

            return HttpContext.Current.Session["SMTPpass"] as string;
        }
        set
        {
            HttpContext.Current.Session["SMTPpass"] = value;
        }
    }

    public static string EmailSubject
    {
        get
        {
            if (HttpContext.Current.Session["EmailSubject"] == null)
                HttpContext.Current.Session["EmailSubject"] = "";

            return HttpContext.Current.Session["EmailSubject"] as string;
        }
        set
        {
            HttpContext.Current.Session["EmailSubject"] = value;
        }
    }
    public static string SMTPhost
    {
        get
        {
            if (HttpContext.Current.Session["SMTPhost"] == null)
                HttpContext.Current.Session["SMTPhost"] = "";

            return HttpContext.Current.Session["SMTPhost"] as string;
        }
        set
        {
            HttpContext.Current.Session["SMTPhost"] = value;
        }
    }

    public static string SMTPport
    {
        get
        {
            if (HttpContext.Current.Session["SMTPport"] == null)
                HttpContext.Current.Session["SMTPport"] = "";

            return HttpContext.Current.Session["SMTPport"] as string;
        }
        set
        {
            HttpContext.Current.Session["SMTPport"] = value;
        }
    }

    #endregion

    #region Common functions 
    public static string[] GetClientData(byte[] data, char delimeter)
    {
        string strData = ASCIIEncoding.ASCII.GetString(data);
        return strData.Split(delimeter);
    }

    public static void SendToClient(string data)
    {
        System.Web.HttpContext.Current.Response.Clear();
        System.Web.HttpContext.Current.Response.ContentType = "text/plain";
        System.Web.HttpContext.Current.Response.Write(data);
        System.Web.HttpContext.Current.Response.Flush();
        System.Web.HttpContext.Current.Response.Close();
    }

    public static void AddToActiveUsers()
    {
        ActiveUsers.Add(new LogOnUser(Common.UserName, HttpContext.Current.Session.SessionID));
        LogActivity(Common.UserName, "Log In Successful",Common.ConnString, HttpContext.Current.Request.UserHostAddress);
    }

    public static void RemoveToActiveUsers()
    {
        for (int i = 0; i < ActiveUsers.Count; i++)
        {
            if (ActiveUsers[i].UserId == Common.UserName)
            {
                ActiveUsers.Remove(ActiveUsers[i]);
                break;
            }
        }
    }

    public static void LogOut()
    {
        LogActivity(Common.UserName, "Log out Successful",Common.ConnString, HttpContext.Current.Request.UserHostAddress);

        Common.RemoveToActiveUsers();
        Common.IsUserAuthenticated = false;

        Common.TempUserID = null;

        System.Web.HttpContext.Current.Session.Clear();
        System.Web.HttpContext.Current.Session.RemoveAll();
        System.Web.HttpContext.Current.Session.Abandon();
    }

    public static void LogActivity(string userID, string activity, string ConnString, string IPAddress)
    {
        string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
        DataAccess.MicrosoftDB.ExecuteCommand(
            "SP_SaveActivityLog"
            , CommandType.StoredProcedure
            , conString
            , new SqlParameter("UserID", userID)
            , new SqlParameter("Activity", activity)
            , new SqlParameter("IPAddress", IPAddress)
            );
    }
    #endregion  
}