using AdvanceEntity;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace AdvanceDal
{
    public class MiscDal
    {
        #region Threshold

        public DataTable RetrieveThreshold(string ConnString)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_RetrieveThreshold"
                    , CommandType.StoredProcedure
                    , conString
                    );
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetFormHeader(string ConnString)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_RetrieveFormHeader"
                    , CommandType.StoredProcedure
                    , conString
                    );
            }
            catch
            {
                throw;
            }
        }
        public int SetThreshold(MiscEntity miscEntity)
        {
            string conString = ConfigurationManager.ConnectionStrings[miscEntity.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_SetThreshold"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("AgingThreshold", miscEntity.AgingThreshold)
                    , new SqlParameter("HandheldThreshold", miscEntity.HandHeldThreshold)
                    , new SqlParameter("MaintenanceThreshold", miscEntity.MaintenanceThreshold)
                    , new SqlParameter("User", miscEntity.CreatedBy)
                    );
            }
            catch
            {
                throw;
            }
        }
        #endregion Threshold

        #region home page image

        public DataTable GetHomePageImage(string ConnString)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_RetrieveHomePageImage"
                    , CommandType.StoredProcedure
                    , conString
                    );
            }
            catch
            {
                throw;
            }
        }

        public int SaveHomePageImage(MiscEntity miscEntity)
        {
            string conString = ConfigurationManager.ConnectionStrings[miscEntity.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_SetHomePageImage"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("HomePageImage", miscEntity.HomePageImage)
                    , new SqlParameter("User", miscEntity.CreatedBy)
                    );
            }
            catch
            {
                throw;
            }
        }
        #endregion home page image

        #region Company Information

        public DataTable GetCompanyInformation(string ConnString)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_RetrieveCompanyInformation"
                    , CommandType.StoredProcedure
                    , conString
                    );
            }
            catch
            {
                throw;
            }
        }

        public int SetCompanyInformation(MiscEntity miscEntity)
        {
            string conString = ConfigurationManager.ConnectionStrings[miscEntity.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_SetCompanyInformation"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("CompanyName", miscEntity.CompanyName)
                    , new SqlParameter("TradingName", miscEntity.TradingName)
                    , new SqlParameter("ContactPerson", miscEntity.ContactPerson)
                    , new SqlParameter("ContactNumber", miscEntity.ContactNumber)
                    , new SqlParameter("Address", miscEntity.Address)
                    , new SqlParameter("EmailAddress", miscEntity.EmailAddress)
                    , new SqlParameter("CompanyLogo", miscEntity.CompanyLogo)
                    , new SqlParameter("FormLogo", miscEntity.FormLogo)
                    , new SqlParameter("WebIcon", miscEntity.WebIcon)
                    , new SqlParameter("LogoPath", miscEntity.LogoPath)
                    , new SqlParameter("User", miscEntity.CreatedBy)
                    );
            }
            catch
            {
                throw;
            }
        }
        #endregion Company Information

        #region Depreciation

        public DataTable GetDepreciation(string ConnString)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_RetrieveDepreciation"
                    , CommandType.StoredProcedure
                    , conString
                    );
            }
            catch
            {
                throw;
            }
        }

        public int SetDepreciation(MiscEntity miscEntity)
        {
            string conString = ConfigurationManager.ConnectionStrings[miscEntity.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_SetDepreciation"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("DepreciationFormula", miscEntity.DepreciationFormula)
                    , new SqlParameter("UsefulLife", miscEntity.UsefulLife)
                    , new SqlParameter("DaysPerYear", miscEntity.DaysPerYear)
                    , new SqlParameter("ResidualValue", miscEntity.ResidualValue)
                    , new SqlParameter("SalvageValue", miscEntity.SalvageValue)
                    , new SqlParameter("RateOfDepreciation", miscEntity.RateOfDepreciation)
                    , new SqlParameter("User", miscEntity.CreatedBy)
                    );
            }
            catch
            {
                throw;
            }
        }
        #endregion Depreciation

        #region RFID and Barcode Generation

        public string CheckDuplicatePropertyNo(string Connstring, string propertyno)
        {
            string conString = ConfigurationManager.ConnectionStrings[Connstring].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<string>(
                    "SP_CheckDuplicatePropertyNo"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("PropertyNumber", propertyno)
                    );
            }
            catch
            {
                throw;
            }
        }

        public string CheckDuplicateSerial(string Connstring, string serialno)
        {
            string conString = ConfigurationManager.ConnectionStrings[Connstring].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<string>(
                    "SP_CheckDuplicateSerial"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("SerialNumber", serialno)
                    );
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetRFIDandBarcodeGeneration(string ConnString)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_RetrieveRFIDandBarcodeGeneration"
                    , CommandType.StoredProcedure
                    , conString
                    );
            }
            catch
            {
                throw;
            }
        }

        public DataTable getRFIDfromFile(string ConnString)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_RetrieveRFIDfromFile"
                    , CommandType.StoredProcedure
                    , conString
                    );
            }
            catch
            {
                throw;
            }
        }

        public int RevertRFIDNumbers(RFIDnumberEntity rfid)
        {
            string conString = ConfigurationManager.ConnectionStrings[rfid.ConnectionString].ToString();
            try
            {
                foreach (RFIDNumbers rfidnumber in rfid.GetSetRFIDnumber)
                {
                    DataAccess.MicrosoftDB.ExecuteCommand(
                        "SP_RevertRFID"
                        , CommandType.StoredProcedure
                        , conString
                        , new SqlParameter("EPC", rfidnumber.RFIDnumber)
                    );
                }

                return 1;
            }
            catch
            {
                return 0;
            }
        }

        public int SetRFIDandBarcodeCount(string ConnString, bool rfidinc, bool barcodeinc, int increment)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_SetRFIDandBarcodeCount"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("RFIDinc", rfidinc)
                    , new SqlParameter("BARCODEinc", barcodeinc)
                    , new SqlParameter("INCREMENT", increment)
                    );
            }
            catch
            {
                throw;
            }
        }

        public int SetRFIDandBarcodeGeneration(MiscEntity miscEntity)
        {
            string conString = ConfigurationManager.ConnectionStrings[miscEntity.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_SetRFIDandBarcodeGeneration"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("RFIDNumberGeneration", miscEntity.RFIDNumberGeneration)
                    , new SqlParameter("BarcodeNumberGeneration", miscEntity.BarcodeNumberGeneration)
                    , new SqlParameter("User", miscEntity.CreatedBy)
                    );
            }
            catch
            {
                throw;
            }
        }
        #endregion RFID and Barcode Generation

        #region System Setting

        public DataTable GetSystemSetting(string ConnString)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_RetrieveSystemSetting"
                    , CommandType.StoredProcedure
                    , conString
                    );
            }
            catch
            {
                throw;
            }
        }

        public int SetSystemSetting(MiscEntity miscEntity)
        {
            string conString = ConfigurationManager.ConnectionStrings[miscEntity.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_SetSystemSetting"
                    , CommandType.StoredProcedure
                    , conString

                    , new SqlParameter("VerificationMode", miscEntity.VerificationMode)
                    , new SqlParameter("ICS", miscEntity.ICS)
                    , new SqlParameter("PAR", miscEntity.PAR)
                    , new SqlParameter("CapexOpex", miscEntity.CapexOpex)
                    , new SqlParameter("ExpenseType", miscEntity.ExpenseType)
                    , new SqlParameter("VersionMode", miscEntity.VersionMode)
                    , new SqlParameter("CreationApproval", miscEntity.CreationApproval)
                    , new SqlParameter("StockMergeApproval", miscEntity.StockMergeApproval)
                    , new SqlParameter("PropertyIssuanceApproval", miscEntity.PropertyIssuanceApproval)
                    , new SqlParameter("PropertyTransferApproval", miscEntity.PropertyTransferApproval)
                    , new SqlParameter("TurnOverStockApproval", miscEntity.TurnOverStockApproval)
                    , new SqlParameter("PropertyDisposalApproval", miscEntity.PropertyDisposalApproval)
                    , new SqlParameter("GatePassApproval", miscEntity.GatePassApproval)
                    , new SqlParameter("CSVUploadApproval", miscEntity.CSVUploadApproval)
                    , new SqlParameter("DBRestoreApproval", miscEntity.DBRestoreApproval)
                    , new SqlParameter("RetaggingApproval", miscEntity.RetaggingApproval)
                    , new SqlParameter("SupplyAllocationApproval", miscEntity.SupplyAllocationApproval)

                    , new SqlParameter("StockDisposalApproval", miscEntity.StockDisposalApproval)
                    , new SqlParameter("StockIssuanceApproval", miscEntity.StockIssuanceApproval)

                    , new SqlParameter("CreationScanner", miscEntity.CreationScanner)
                    , new SqlParameter("PropertyIssuanceScanner", miscEntity.PropertyIssuanceScanner)
                    , new SqlParameter("PropertyTransferScanner", miscEntity.PropertyTransferScanner)
                    , new SqlParameter("StockTaking", miscEntity.StockTaking)

                    , new SqlParameter("RFIDprinting", miscEntity.RFIDprinting)

                    , new SqlParameter("PropertyRequestApproval", miscEntity.PropertyRequestApproval)
                    , new SqlParameter("PropertyRequestScanner", miscEntity.PropertyRequestScanner)
                    , new SqlParameter("PropertyReturnRequestScanner", miscEntity.PropertyReturnRequestScanner)
                    , new SqlParameter("PropertyDisposalScanner", miscEntity.PropertyDisposalScanner)
                    , new SqlParameter("IssuanceAcknowledgement", miscEntity.IssuanceAcknowledgement)
                    , new SqlParameter("AcknowledgementMode", miscEntity.AcknowledgementMode)
                    , new SqlParameter("RequireMaintenance", miscEntity.RequireMaintenance)

                    , new SqlParameter("PropertyNoGeneration", miscEntity.PropertyNoGeneration)
                    , new SqlParameter("StockRequestRemainingQuantity", miscEntity.StockRequestRemainingQuantity)

                    , new SqlParameter("User", miscEntity.CreatedBy)
                    );
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion System Setting

        #region Database Back up directory

        public int SetDBDirectory(MiscEntity miscEntity)
        {
            string conString = ConfigurationManager.ConnectionStrings[miscEntity.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_SetDBDirectory"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("Directory", miscEntity.DBDirectory)
                    , new SqlParameter("User", miscEntity.CreatedBy)
                    );
            }
            catch
            {
                throw;
            }
        }

        #endregion Database Back up directory

        #region Uploaded FIles Directory

        public int SetUploadedFilesDirectoryPath(MiscEntity miscEntity)
        {
            string conString = ConfigurationManager.ConnectionStrings[miscEntity.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_SetUploadedFilesDirectoryPath"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("Directory", miscEntity.UploadedFilesDirectory)
                    , new SqlParameter("User", miscEntity.CreatedBy)
                    );
            }
            catch
            {
                throw;
            }
        }

        #endregion Uploaded FIles Directory

        #region Mail Setting

        public DataTable GetSMTP(string ConnString)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_RetrieveSMTP"
                    , CommandType.StoredProcedure
                    , conString
                    );
            }
            catch
            {
                throw;
            }
        }

        public int SetMail(MiscEntity miscEntity)
        {
            string conString = ConfigurationManager.ConnectionStrings[miscEntity.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_SetMail"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("SMTPhost", miscEntity.SMTPhost)
                    , new SqlParameter("SMTPport", miscEntity.SMTPport)
                    , new SqlParameter("Email", miscEntity.Email)
                    , new SqlParameter("Password", miscEntity.Password)
                    , new SqlParameter("EmailSubject", miscEntity.EmailSubject)
                    , new SqlParameter("User", miscEntity.CreatedBy)
                    );
            }
            catch
            {
                throw;
            }
        }
        #endregion Mail Setting

        #region User Default Password

        public DataTable GetUserDefaultPassword(string ConnString)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                       "SP_RetrieveDefaultPassword"
                       , CommandType.StoredProcedure
                       , conString
                   );
            }
            catch
            {
                throw;
            }
        }

        public int SetDefaultPassword(MiscEntity miscEntity)
        {
            string conString = ConfigurationManager.ConnectionStrings[miscEntity.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_SetUserDefaultPassword"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("DefaultPassword", miscEntity.UserDefaultPassword)
                    , new SqlParameter("User", miscEntity.CreatedBy)
                    );
            }
            catch
            {
                throw;
            }
        }

        public int SetUserAcccountSettings(MiscEntity miscEntity)
        {
            string conString = ConfigurationManager.ConnectionStrings[miscEntity.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_SetUserPasswordPolicy"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("UsernameOrEmail", miscEntity.UsernameOrEmail)
                    , new SqlParameter("DefaultOrGenerated", miscEntity.DefaultOrGenerated)
                    , new SqlParameter("DefaultPassword", miscEntity.UserDefaultPassword)
                    , new SqlParameter("PasswordSize", miscEntity.PasswordSize)
                    , new SqlParameter("UpperCaseSize", miscEntity.UpperCaseSize)
                    , new SqlParameter("LowerCaseSize", miscEntity.LowerCaseSize)
                    , new SqlParameter("NumberSize", miscEntity.NumberSize)
                    , new SqlParameter("SymbolSize", miscEntity.SymbolSize)
                    , new SqlParameter("LoginAttempt", miscEntity.LoginAttempt)
                    , new SqlParameter("User", miscEntity.CreatedBy)
                    , new SqlParameter("WithLoginAttempt", miscEntity.WithLoginAttempt)
                    , new SqlParameter("WithUpperCase", miscEntity.WithUpperCase)
                    , new SqlParameter("WithLowerCase", miscEntity.WithLowerCase)
                    , new SqlParameter("WithNumber", miscEntity.WithNumber)
                    , new SqlParameter("WithSymbol", miscEntity.WithSymbol)

                    );
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion User Default Password

        #region Restore Database

        public int DeclineDatabaseRestore(MiscEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_DeclineDatabaseRestore"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("RestoreID", x.RestoreID)
                    , new SqlParameter("User", x.CreatedBy)
                    );
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetDatabaseRestoreApproval(string ConnString)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string where = "";
                where = "IsApproved = 'false'";

                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_RestoreDatabase]")
                    , new SqlParameter("orderBy", "RecordID DESC")
                    , new SqlParameter("fieldlist", "*")
                    , new SqlParameter("filter", where)
                    , new SqlParameter("pageNum", 0)
                    , new SqlParameter("pageSize", 0)
                    );
            }
            catch (Exception ex)
            {
                ex.ToString();
                throw;
            }
        }

        public int RestoreDatabase(MiscEntity miscEntity)
        {
            string conString = ConfigurationManager.ConnectionStrings[miscEntity.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_SaveDatabaseRestore"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("RestoreName", miscEntity.RestoreName)
                    , new SqlParameter("FileName", miscEntity.BakFileName)
                    , new SqlParameter("User", miscEntity.CreatedBy)
                    );
            }
            catch
            {
                throw;
            }
        }
        #endregion Restore Database

        #region Accounting Setting

        public DataTable GetAccountingSetting(string ConnString)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_RetrieveAccountingSetting"
                    , CommandType.StoredProcedure
                    , conString
                    );
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetUserAcccountSettings(string ConnString)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_RetrieveUserPasswordPolicy"
                    , CommandType.StoredProcedure
                    , conString
                    );
            }
            catch
            {
                throw;
            }
        }

        public int SetAccountingSetting(MiscEntity miscEntity)
        {
            string conString = ConfigurationManager.ConnectionStrings[miscEntity.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_SetAccountingSetting"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("AccountingPeriod", miscEntity.AccountingPeriod)
                    , new SqlParameter("ConversionMonth", miscEntity.ConversionMonth)
                    , new SqlParameter("EndMonth", miscEntity.EndMonth)
                    , new SqlParameter("TransactionYear", miscEntity.TransactionYear)
                    , new SqlParameter("TransactionMonth", miscEntity.TransactionMonth)
                    , new SqlParameter("User", miscEntity.CreatedBy)
                    );
            }
            catch
            {
                throw;
            }
        }
        #endregion Accounting Setting

        #region Data Upload

        public int ApprovePropertyUpload(string batchname, string user, string connstring)
        {
            string conString = ConfigurationManager.ConnectionStrings[connstring].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_UploadPropertyApprove"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("User", user)
                    , new SqlParameter("BatchName", batchname)
                    );
            }
            catch (Exception ex)
            {
                ex.ToString();
                throw;
            }
        }

        //Request management ApprovePropertyUpload
        public int ApproveStockUpload(string batchname, string user, string connstring)
        {
            string conString = ConfigurationManager.ConnectionStrings[connstring].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_UploadStockApprove"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("User", user)
                    , new SqlParameter("BatchName", batchname)
                    );
            }
            catch
            {
                throw;
            }
        }

        public int ApproveUserUpload(string batchname, string user, string connstring, int noofcustodian, int noofadmin)
        {
            string conString = ConfigurationManager.ConnectionStrings[connstring].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_UploadUserApprove"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("User", user)
                    , new SqlParameter("BatchName", batchname)
                    , new SqlParameter("CustodianUser", noofcustodian)
                    , new SqlParameter("AdminUser", noofadmin)
                    );
            }
            catch
            {
                throw;
            }
        }

        public int CheckIfExists(MiscEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_UploadPropertyCheckIfExists"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("FileName", x.DataUploadFileNameProperty)
                    , new SqlParameter("User", x.CreatedBy)
                    );
            }
            catch
            {
                throw;
            }
        }

        public int CheckIfExistsStock(MiscEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_UploadStockCheckIfExists"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("FileName", x.DataUploadFileNameStock)
                    , new SqlParameter("User", x.CreatedBy)
                    );
            }
            catch
            {
                throw;
            }
        }

        public int CheckIfExistsUser(MiscEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_UploadUserCheckIfExists"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("FileName", x.DataUploadFileNameUser)
                    , new SqlParameter("User", x.CreatedBy)
                    );
            }
            catch
            {
                throw;
            }
        }

        public int DeclinePropertyUpload(string batchname, string user, string connstring)
        {
            string conString = ConfigurationManager.ConnectionStrings[connstring].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_UploadPropertyDecline"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("User", user)
                    , new SqlParameter("BatchName", batchname)
                    );
            }
            catch
            {
                throw;
            }
        }

        public int DeclineStockUpload(string batchname, string user, string connstring)
        {
            string conString = ConfigurationManager.ConnectionStrings[connstring].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_UploadStockDecline"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("User", user)
                    , new SqlParameter("BatchName", batchname)
                    );
            }
            catch
            {
                throw;
            }
        }

        public int DeclineUserUpload(string batchname, string user, string connstring)
        {
            string conString = ConfigurationManager.ConnectionStrings[connstring].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_UploaduserDecline"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("User", user)
                    , new SqlParameter("BatchName", batchname)
                    );
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetPropertyUploadApproval(string ConnString, int SiteID)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string where = "";
                where = "IsApproved = 'false' and Status = 'Pending Approval'";

                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_PropertyUploadPendingApproval]")
                    , new SqlParameter("orderBy", "RecID DESC")
                    , new SqlParameter("fieldlist", "*")
                    , new SqlParameter("filter", where)
                    , new SqlParameter("pageNum", 0)
                    , new SqlParameter("pageSize", 0)
                    );
            }
            catch (Exception ex)
            {
                ex.ToString();
                throw;
            }
        }

        public DataTable GetPropertyUploadApprovalDetails(string ConnString, string batchname)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_SearchUploadPropertyForApproval"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("BatchName", batchname)
                    );
            }
            catch (Exception e)
            {
                throw;
            }
            //string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            //try
            //{
            //    string where = "";
            //    where = "BatchName = '" + batchname + "'";

            //    return DataAccess.MicrosoftDB.GetDataTable(
            //        "UtilPaging"
            //        , CommandType.StoredProcedure
            //        , conString
            //        , new SqlParameter("datasrc", "[dbo].[VW_PropertyUploadPendingApprovalList]")
            //        , new SqlParameter("orderBy", "No ASC")
            //        , new SqlParameter("fieldlist", "*")
            //        , new SqlParameter("filter", where)
            //        , new SqlParameter("pageNum", 0)
            //        , new SqlParameter("pageSize", 0)
            //        );
            //}
            //catch
            //{
            //    throw;
            //}
        }

        public DataTable GetStockUploadApproval(string ConnString, int SiteID)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string where = "";
                where = "IsApproved = 'false' and SiteID = '" + SiteID + "' and Status = 'Pending Approval'";

                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_StockUploadPendingApproval]")
                    , new SqlParameter("orderBy", "RecID DESC")
                    , new SqlParameter("fieldlist", "*")
                    , new SqlParameter("filter", where)
                    , new SqlParameter("pageNum", 0)
                    , new SqlParameter("pageSize", 0)
                    );
            }
            catch (Exception ex)
            {
                ex.ToString();
                throw;
            }
        }

        public DataTable GetStockUploadApprovalDetails(string ConnString, string batchname)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string where = "";
                where = "BatchName = '" + batchname + "'";

                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_StockUploadPendingApprovalList]")
                    , new SqlParameter("orderBy", "No ASC")
                    , new SqlParameter("fieldlist", "*")
                    , new SqlParameter("filter", where)
                    , new SqlParameter("pageNum", 0)
                    , new SqlParameter("pageSize", 0)
                    );
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetUserUploadApproval(string ConnString, int SiteID)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string where = "";
                where = "IsApproved = 'false' and SiteID = '" + SiteID + "' and Status = 'Pending Approval'";

                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_UserUploadPendingApproval]")
                    , new SqlParameter("orderBy", "RecID DESC")
                    , new SqlParameter("fieldlist", "*")
                    , new SqlParameter("filter", where)
                    , new SqlParameter("pageNum", 0)
                    , new SqlParameter("pageSize", 0)
                    );
            }
            catch (Exception ex)
            {
                ex.ToString();
                throw;
            }
        }

        public DataTable GetUserUploadApprovalDetails(string ConnString, string batchname)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string where = "";
                where = "BatchName = '" + batchname + "'";

                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_UserUploadPendingApprovalList]")
                    , new SqlParameter("orderBy", "No ASC")
                    , new SqlParameter("fieldlist", "*")
                    , new SqlParameter("filter", where)
                    , new SqlParameter("pageNum", 0)
                    , new SqlParameter("pageSize", 0)
                    );
            }
            catch
            {
                throw;
            }
        }

        //PROPERTY
        public int UploadPropertyForApproval(MiscEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_UploadPropertyForApproval"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("FileName", x.DataUploadFileNameProperty)
                    , new SqlParameter("User", x.CreatedBy)
                    );
            }
            catch
            {
                throw;
            }
        }
        //STOCK
        public int UploadStockForApproval(MiscEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_UploadStockForApproval"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("FileName", x.DataUploadFileNameStock)
                    , new SqlParameter("User", x.CreatedBy)
                    );
            }
            catch
            {
                throw;
            }
        }
        //User Upload
        public int UploadUserForApproval(MiscEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_UploadUserForApproval"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("FileName", x.DataUploadFileNameUser)
                    , new SqlParameter("User", x.CreatedBy)
                    );
            }
            catch
            {
                throw;
            }
        }
        #endregion Data Upload

        //Notification
        public DataTable GetNotification(string ConnString, string user)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string where = "";
                where = "ToUser = '" + user + "'";

                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[tbl_Notification]")
                    , new SqlParameter("orderBy", "NotificationID Desc")
                    , new SqlParameter("fieldlist", "*")
                    , new SqlParameter("filter", where)
                    , new SqlParameter("pageNum", 0)
                    , new SqlParameter("pageSize", 0)
                    );
            }
            catch
            {
                throw;
            }
        }

        //Roll Over
        public int TruncateTables(string ConnString, string databasename)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_TruncateTables"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("DatabaseName", databasename)
                    );
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}