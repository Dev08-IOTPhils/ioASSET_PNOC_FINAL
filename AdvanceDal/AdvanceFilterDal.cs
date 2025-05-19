using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace AdvanceDal
{
    public class AdvanceFilterDal
    {
        public int AddAdvanceFilter(string ReportID, string Logic, string FilterBY, string Operator, string val, string trx, string connString, string UserID)
        {
            string conString = ConfigurationManager.ConnectionStrings[connString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                  "SP_AdvanceFilter"
                  , CommandType.StoredProcedure
                  , conString
                  , new SqlParameter("ReportID", ReportID)
                  , new SqlParameter("Logic", Logic)
                  , new SqlParameter("FilterBy", FilterBY)
                  , new SqlParameter("Operator", Operator)
                  , new SqlParameter("Val", val)
                  , new SqlParameter("Trx", trx)
                  , new SqlParameter("UserID", UserID)
                );
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        //ADVfilterModification
        public int ADVfilterModification(string RecID, string trx, string connString)
        {
            string conString = ConfigurationManager.ConnectionStrings[connString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                  "SP_AdvanceFilter"
                  , CommandType.StoredProcedure
                  , conString
                  , new SqlParameter("RecID", RecID)
                  , new SqlParameter("Trx", trx)
                );
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public DataTable GetAdvancefilterperUser(string ReportID, string trx, string connstring, string userID)
        {
            string conString = ConfigurationManager.ConnectionStrings[connstring].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                      "SP_AdvanceFilter"
                      , CommandType.StoredProcedure
                      , conString
                      , new SqlParameter("ReportID", ReportID)
                      , new SqlParameter("Trx", trx)
                      , new SqlParameter("UserID", userID)
                  );
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string GetFilesDataForFilter(string connString)
        {
            string conString = ConfigurationManager.ConnectionStrings[connString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<string>(
                    "sp_ADVFilterMod"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("NewPassword", "")
                    );
            }
            catch
            {
                throw;
            }
        }
    }
}