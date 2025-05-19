using AdvanceDal;
using AdvanceEntity;
using System;
using System.Data;
using System.Text;

namespace AdvanceBll
{

    public class MiscBll
    {
        private MiscDal miscDal = new MiscDal();

        #region Threshold

        public string RetrieveThreshold(string ConnString)
        {
            try
            {
                string result = "";

                DataTable dt = miscDal.RetrieveThreshold(ConnString);

                result = dt.Rows[0]["RecordID"].ToString() + "|" +
                         dt.Rows[0]["AgingThreshold"].ToString() + "|" +
                         dt.Rows[0]["HandheldThreshold"].ToString() + "|" +
                         dt.Rows[0]["MaintenanceThreshold"].ToString();
                return result;
            }
            catch
            {
                throw;
            }
        }

        public string GetFormHeader(string ConnString)
        {
            try
            {
                string result = " | | | ";

                DataTable dt = miscDal.GetFormHeader(ConnString);
                result = dt.Rows[0]["Value"].ToString() + "|" +
                         dt.Rows[1]["Value"].ToString() + "|" +
                         dt.Rows[2]["Value"].ToString() + "|" +
                         dt.Rows[3]["Value"].ToString();
                return result;
            }
            catch
            {
                throw;
            }
        }

        public int SetThreshold(MiscEntity miscEntity)
        {
            int id = miscDal.SetThreshold(miscEntity);
            try
            {
                return id;
            }
            catch
            {
                return id;
            }
        }
        #endregion Threshold

        #region Home Page Image

        public string GetHomePageImage(string ConnString)
        {
            try
            {
                string result = "";

                DataTable dtMisc = miscDal.GetHomePageImage(ConnString);

                result = dtMisc.Rows[0]["HomePageImage"].ToString();

                return result;
            }
            catch
            {
                throw;
            }
        }

        public int SaveHomePageImage(MiscEntity miscEntity)
        {
            int id = miscDal.SaveHomePageImage(miscEntity);
            try
            {
                return id;
            }
            catch
            {
                return id;
            }
        }
        #endregion Home Page Image

        #region Company Information

        public string GetCompanyInformation(string ConnString)
        {
            try
            {
                string result = "";

                DataTable dt = miscDal.GetCompanyInformation(ConnString);

                result = dt.Rows[0]["CompanyName"].ToString() + "|" +
                         dt.Rows[0]["TradingName"].ToString() + "|" +
                         dt.Rows[0]["ContactPerson"].ToString() + "|" +
                         dt.Rows[0]["ContactNumber"].ToString() + "|" +
                         dt.Rows[0]["EmailAddress"].ToString() + "|" +
                         dt.Rows[0]["Address"].ToString() + "|" +
                         dt.Rows[0]["CompanyLogo"].ToString();
                return result;
            }
            catch
            {
                throw;
            }
        }

        public int SetCompanyInformation(MiscEntity miscEntity)
        {
            int id = miscDal.SetCompanyInformation(miscEntity);
            try
            {
                return id;
            }
            catch
            {
                return id;
            }
        }
        #endregion Company Information

        #region Depreciation

        public string GetDepreciation(string ConnString)
        {
            try
            {
                string result = "";

                DataTable dt = miscDal.GetDepreciation(ConnString);

                result = dt.Rows[0]["DepreciationFormula"].ToString() + "|" +
                         dt.Rows[0]["UsefulLife"].ToString() + "|" +
                         dt.Rows[0]["DaysInAYear"].ToString() + "|" +
                         dt.Rows[0]["DefaultResidualValue"].ToString() + "|" +
                         dt.Rows[0]["DefaultSalvageValue"].ToString() + "|" +
                         dt.Rows[0]["DefaultRateOfDepreciation"].ToString();
                return result;
            }
            catch
            {
                throw;
            }
        }

        public int SetDepreciation(MiscEntity miscEntity)
        {
            int id = miscDal.SetDepreciation(miscEntity);
            try
            {
                return id;
            }
            catch
            {
                return id;
            }
        }
        #endregion Depreciation

        #region RFID and Barcode Generation

        public string CheckDuplicatePropertyNo(string ConnString, string propertyno)
        {
            string id = miscDal.CheckDuplicatePropertyNo(ConnString, propertyno);
            try
            {
                return id;
            }
            catch
            {
                return id;
            }
        }

        public string CheckDuplicateSerial(string ConnString, string serialno)
        {
            string id = miscDal.CheckDuplicateSerial(ConnString, serialno);
            try
            {
                return id;
            }
            catch
            {
                return id;
            }
        }

        public string ConvertHexToASCII(string hexString)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hexString.Length; i += 2)
            {
                string hs = hexString.Substring(i, 2);
                sb.Append(Convert.ToChar(Convert.ToUInt32(hs, 16)));
            }
            return sb.ToString();
        }

        public string GetRFIDandBarcodeGeneration(string ConnString)
        {
            try
            {
                string result = "";

                DataTable dt = miscDal.GetRFIDandBarcodeGeneration(ConnString);

                result = dt.Rows[0]["RFIDNumberGeneration"].ToString() + "|" +
                         dt.Rows[0]["BarcodeNumberGeneration"].ToString() + "|" +
                         dt.Rows[0]["RFIDNumberCount"].ToString() + "|" +
                         dt.Rows[0]["BarcodeNumberCount"].ToString();
                return result;
            }
            catch
            {
                throw;
            }
        }

        public string getRFIDfromFile(string ConnString)
        {
            try
            {
                string result = "";

                DataTable dt = miscDal.getRFIDfromFile(ConnString);

                result = ConvertHexToASCII(dt.Rows[0]["EPC"].ToString());
                return result;
            }
            catch
            {
                throw;
            }
        }

        public int RevertRFIDNumbers(RFIDnumberEntity rfid)
        {
            try
            {
                miscDal.RevertRFIDNumbers(rfid);

                return 1;
            }
            catch
            {
                return 0;
            }
        }

        public int SetRFIDandBarcodeCount(string ConnString, bool rfidinc, bool barcodeinc, int increment)
        {
            int id = miscDal.SetRFIDandBarcodeCount(ConnString, rfidinc, barcodeinc, increment);
            try
            {
                return id;
            }
            catch
            {
                return id;
            }
        }

        public int SetRFIDandBarcodeGeneration(MiscEntity miscEntity)
        {
            int id = miscDal.SetRFIDandBarcodeGeneration(miscEntity);
            try
            {
                return id;
            }
            catch
            {
                return id;
            }
        }
        #endregion RFID and Barcode Generation

        #region System Setting

        public string GetSystemSetting(string ConnString)
        {
            try
            {
                string result = "";

                DataTable dt = miscDal.GetSystemSetting(ConnString);

                result = dt.Rows[0]["CreationApproval"].ToString() + "|" + //0
                         dt.Rows[0]["StockMergeApproval"].ToString() + "|" + //1
                         dt.Rows[0]["PropertyIssuanceApproval"].ToString() + "|" +//2
                         dt.Rows[0]["PropertyTransferApproval"].ToString() + "|" +//3
                         dt.Rows[0]["TurnOverStockApproval"].ToString() + "|" +//4
                         dt.Rows[0]["PropertyDisposalApproval"].ToString() + "|" +//5
                         dt.Rows[0]["GatePassApproval"].ToString() + "|" +//6
                         dt.Rows[0]["CSVUploadApproval"].ToString() + "|" +//7
                         dt.Rows[0]["DBRestoreApproval"].ToString() + "|" +//8

                         dt.Rows[0]["CreationScanner"].ToString() + "|" +//9
                         dt.Rows[0]["PropertyIssuanceScanner"].ToString() + "|" +//10
                         dt.Rows[0]["PropertyTransferScanner"].ToString() + "|" +//11
                         dt.Rows[0]["StockTaking"].ToString() + "|" +//12

                         dt.Rows[0]["RFIDprinting"].ToString() + "|" +//13

                         dt.Rows[0]["PropertyRequestApproval"].ToString() + "|" +//14
                         dt.Rows[0]["PropertyRequestScanner"].ToString() + "|" +//15
                         dt.Rows[0]["PropertyReturnRequestScanner"].ToString() + "|" +//16
                         dt.Rows[0]["PropertyDisposalScanner"].ToString() + "|" +//17

                         dt.Rows[0]["VerificationMode"].ToString() + "|" +//18
                         dt.Rows[0]["IssuanceAcknowledgement"].ToString() + "|" +//19
                         dt.Rows[0]["AcknowledgementMode"].ToString() + "|" +//20

                         //added for stock issuance/disposal approval
                         dt.Rows[0]["StockDisposalApproval"].ToString() + "|" +//21
                         dt.Rows[0]["StockIssuanceApproval"].ToString() + "|" +//22
                                                                               //added for property return
                         dt.Rows[0]["RequireMaintenance"].ToString() + "|" +//23
                                                                            //added for property no generation
                         dt.Rows[0]["PropertyNoGeneration"].ToString() + "|" +//24
                                                                              //added for property no generation
                         dt.Rows[0]["VersionMode"].ToString() + "|" +//25
                                                                     //added for property no generation
                         dt.Rows[0]["ExpenseType"].ToString() + "|" +//26
                                                                     //added for property no versionmode
                         dt.Rows[0]["ICS"].ToString() + "|" +//27
                                                             //added for property no versionmode
                         dt.Rows[0]["PAR"].ToString() + "|" +//28
                                                             //added for property no versionmode
                        dt.Rows[0]["CapexOpex"].ToString() + "|" + //29

                        dt.Rows[0]["RetaggingApproval"].ToString() + "|" + //30

                        dt.Rows[0]["StockRequestRemainingQuantity"].ToString() + "|" +//31

                        dt.Rows[0]["SupplyAllocation"].ToString(); //32

                return result;
            }
            catch
            {
                throw;
            }
        }

        public int SetSystemSetting(MiscEntity miscEntity)
        {
            int id = miscDal.SetSystemSetting(miscEntity);
            try
            {
                return id;
            }
            catch
            {
                return id;
            }
        }
        #endregion System Setting

        #region Database back-up Directory

        public int SetDBDirectory(MiscEntity miscEntity)
        {
            int id = miscDal.SetDBDirectory(miscEntity);
            try
            {
                return id;
            }
            catch
            {
                return id;
            }
        }

        #endregion Database back-up Directory

        #region Uploaded Files Directory

        public int SetUploadedFilesDirectoryPath(MiscEntity miscEntity)
        {
            int id = miscDal.SetUploadedFilesDirectoryPath(miscEntity);
            try
            {
                return id;
            }
            catch
            {
                return id;
            }
        }

        #endregion Uploaded Files Directory

        #region Mail Setting

        public string GetCompanyInfo(string ConnString)
        {
            string result = "";
            try
            {
                DataTable dt = miscDal.GetCompanyInformation(ConnString);
                result = dt.Rows[0]["CompanyName"].ToString();
                return result;
            }
            catch
            {
                return result;
            }
        }

        public string GetSMTP(string ConnString)
        {
            string result = "";
            try
            {
                DataTable dt = miscDal.GetSMTP(ConnString);
                result = dt.Rows[0]["SMTPhost"].ToString() + "|" +
                        dt.Rows[0]["SMTPport"].ToString() + "|" +
                        dt.Rows[0]["Email"].ToString() + "|" +
                        dt.Rows[0]["Password"].ToString() + "|" +
                        dt.Rows[0]["EmailSubject"].ToString();
                return result;
            }
            catch
            {
                return result;
            }
        }

        public int SetMail(MiscEntity miscEntity)
        {
            int id = miscDal.SetMail(miscEntity);
            try
            {
                return id;
            }
            catch
            {
                return id;
            }
        }
        #endregion Mail Setting

        #region User Default Password

        public string GetUserDefaultPassword(string ConnString)
        {
            try
            {
                string result = "";

                DataTable dt = miscDal.GetUserDefaultPassword(ConnString);

                result = dt.Rows[0]["UserDefaultPassword"].ToString();

                return result;
            }
            catch
            {
                throw;
            }
        }

        public int SetDefaultPassword(MiscEntity miscEntity)
        {
            int id = miscDal.SetDefaultPassword(miscEntity);
            try
            {
                return id;
            }
            catch
            {
                return id;
            }
        }

        public int SetUserAcccountSettings(MiscEntity miscEntity)
        {
            int id = miscDal.SetUserAcccountSettings(miscEntity);
            try
            {
                return id;
            }
            catch
            {
                return id;
            }
        }
        #endregion User Default Password

        #region Database Restoration

        public int DeclineDatabaseRestore(MiscEntity x)
        {
            int id = miscDal.DeclineDatabaseRestore(x);
            try
            {
                return id;
            }
            catch
            {
                return id;
            }
        }

        public DataTable GetDatabaseRestoreApproval(string ConnString)
        {
            try
            {
                return miscDal.GetDatabaseRestoreApproval(ConnString);
            }
            catch
            {
                throw;
            }
        }

        public int RestoreDatabase(MiscEntity miscEntity)
        {
            int id = miscDal.RestoreDatabase(miscEntity);
            try
            {
                return id;
            }
            catch
            {
                return id;
            }
        }
        #endregion Database Restoration

        #region Accounting Setting

        public string GetAccountingSetting(string ConnString)
        {
            try
            {
                string result = "";

                DataTable dt = miscDal.GetAccountingSetting(ConnString);

                result = dt.Rows[0]["AccountingPeriod"].ToString() + "|" +
                         dt.Rows[0]["ConversionMonth"].ToString() + "|" +
                         dt.Rows[0]["EndMonth"].ToString() + "|" +
                         dt.Rows[0]["TransactionYear"].ToString() + "|" +
                         dt.Rows[0]["TransactionMonth"].ToString();
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public string GetUserAcccountSettings(string ConnString)
        {
            try
            {
                string result = "";

                DataTable dt = miscDal.GetUserAcccountSettings(ConnString);

                result = dt.Rows[0]["UsernameOrEmail"].ToString() + "|" +                //0
                         dt.Rows[0]["DefaultOrGenerated"].ToString() + "|" +               //1
                         dt.Rows[0]["DefaultPassword"].ToString() + "|" +               //2
                         dt.Rows[0]["PasswordSize"].ToString() + "|" +               //3
                         dt.Rows[0]["WithUpperCase"].ToString() + "|" +               //4
                         dt.Rows[0]["UpperCaseSize"].ToString() + "|" +               //5
                         dt.Rows[0]["WithLowerCase"].ToString() + "|" +               //6
                         dt.Rows[0]["LowerCaseSize"].ToString() + "|" +               //7
                         dt.Rows[0]["WithNumber"].ToString() + "|" +               //8
                         dt.Rows[0]["NumberSize"].ToString() + "|" +               //9
                         dt.Rows[0]["WithSymbol"].ToString() + "|" +               //10
                         dt.Rows[0]["SymbolSize"].ToString() + "|" +               //11
                         dt.Rows[0]["WithLoginAttempt"].ToString() + "|" +               //12
                         dt.Rows[0]["LoginAttempt"].ToString();               //13
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public int SetAccountingSetting(MiscEntity miscEntity)
        {
            int id = miscDal.SetAccountingSetting(miscEntity);
            try
            {
                return id;
            }
            catch
            {
                return id;
            }
        }
        #endregion Accounting Setting

        #region Data Upload

        //Request Management
        public int ApprovePropertyUpload(string batchname, string user, string connstring)
        {
            int id = miscDal.ApprovePropertyUpload(batchname, user, connstring);
            try
            {
                return id;
            }
            catch
            {
                return id;
            }
        }

        public int ApproveStockUpload(string batchname, string user, string connstring)
        {
            int id = miscDal.ApproveStockUpload(batchname, user, connstring);
            try
            {
                return id;
            }
            catch
            {
                return id;
            }
        }

        public int ApproveUserUpload(string batchname, string user, string connstring, int noofcustodian, int noofadmin)
        {
            int id = miscDal.ApproveUserUpload(batchname, user, connstring, noofcustodian, noofadmin);
            try
            {
                return id;
            }
            catch
            {
                return id;
            }
        }

        public int CheckIfExists(MiscEntity x)
        {
            int id = miscDal.CheckIfExists(x);
            try
            {
                return id;
            }
            catch
            {
                return id;
            }
        }

        public int CheckIfExistsStock(MiscEntity x)
        {
            int id = miscDal.CheckIfExistsStock(x);
            try
            {
                return id;
            }
            catch
            {
                return id;
            }
        }

        public int CheckIfExistsUser(MiscEntity x)
        {
            int id = miscDal.CheckIfExistsUser(x);
            try
            {
                return id;
            }
            catch
            {
                return id;
            }
        }

        public int DeclinePropertyUpload(string batchname, string user, string connstring)
        {
            int id = miscDal.DeclinePropertyUpload(batchname, user, connstring);
            try
            {
                return id;
            }
            catch
            {
                return id;
            }
        }

        public int DeclineStockUpload(string batchname, string user, string connstring)
        {
            int id = miscDal.DeclineStockUpload(batchname, user, connstring);
            try
            {
                return id;
            }
            catch
            {
                return id;
            }
        }

        public int DeclineUserUpload(string batchname, string user, string connstring)
        {
            int id = miscDal.DeclineUserUpload(batchname, user, connstring);
            try
            {
                return id;
            }
            catch
            {
                return id;
            }
        }

        public DataTable GetPropertyUploadApproval(string ConnString, int SiteID)
        {
            try
            {
                return miscDal.GetPropertyUploadApproval(ConnString, SiteID);
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetPropertyUploadApprovalDetails(string ConnString, string batchname)
        {
            try
            {
                return miscDal.GetPropertyUploadApprovalDetails(ConnString, batchname);
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetStockUploadApproval(string ConnString, int SiteID)
        {
            try
            {
                return miscDal.GetStockUploadApproval(ConnString, SiteID);
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetStockUploadApprovalDetails(string ConnString, string batchname)
        {
            try
            {
                return miscDal.GetStockUploadApprovalDetails(ConnString, batchname);
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetUserUploadApproval(string ConnString, int SiteID)
        {
            try
            {
                return miscDal.GetUserUploadApproval(ConnString, SiteID);
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetUserUploadApprovalDetails(string ConnString, string batchname)
        {
            try
            {
                return miscDal.GetUserUploadApprovalDetails(ConnString, batchname);
            }
            catch
            {
                throw;
            }
        }

        //PROPERTY
        public int UploadPropertyForApproval(MiscEntity x)
        {
            int id = miscDal.UploadPropertyForApproval(x);
            try
            {
                return id;
            }
            catch
            {
                return id;
            }
        }
        //STOCK

        public int UploadStockForApproval(MiscEntity x)
        {
            int id = miscDal.UploadStockForApproval(x);
            try
            {
                return id;
            }
            catch
            {
                return id;
            }
        }
        //User Upload
        public int UploadUserForApproval(MiscEntity x)
        {
            int id = miscDal.UploadUserForApproval(x);
            try
            {
                return id;
            }
            catch
            {
                return id;
            }
        }
        #endregion Data Upload

        //Notification
        public DataTable GetNotification(string ConnString, string user)
        {
            try
            {
                return miscDal.GetNotification(ConnString, user);
            }
            catch
            {
                throw;
            }
        }

        //Roll Over
        public int TruncateTables(string ConnString, string databasename)
        {
            int id = miscDal.TruncateTables(ConnString, databasename);
            try
            {
                return id;
            }
            catch
            {
                return id;
            }
        }
    }
}