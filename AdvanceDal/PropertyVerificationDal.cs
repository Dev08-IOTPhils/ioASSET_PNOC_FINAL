using AdvanceEntity;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace AdvanceDal
{
    public class PropertyVerificationDal
    {
        public DataTable GetPropertyVerification(string ConnString, string search, int transactiontype, int siteid)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_SearchPropertyVerification"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("Search", search)
                    , new SqlParameter("TransactionType", transactiontype)
                    //, new SqlParameter("SiteID", siteid)
                    );
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public int VerifyProperty(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_VerifyProperty"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("PMLID", x.RecID)
                    , new SqlParameter("User", x.CreatedBy)
                    );
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public int VerifyPropertyByBarcode(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_VerifyPropertyByBarcode"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("BarcodeNumber", x.BarcodeNumber)
                    , new SqlParameter("User", x.CreatedBy)
                    );
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public int VerifyPropertyByTM(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_VerifyPropertyByTM"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("DeviceID", x.DeviceID)
                    , new SqlParameter("PropertyNumber", x.PropertyNumber)
                    , new SqlParameter("User", x.CreatedBy)
                    );
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}