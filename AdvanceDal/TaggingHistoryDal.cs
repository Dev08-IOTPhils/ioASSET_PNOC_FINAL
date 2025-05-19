using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace AdvanceDal
{
    public class TaggingHistoryDal
    {
        public int DeleteFilter(string id, string ConnString)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                   "SP_SaveAdvanceFilter"
                   , CommandType.StoredProcedure
                   , conString
                   , new SqlParameter("Trx", "DELETE")
                   , new SqlParameter("RecID", id)
                   );
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //filter
        public int GetActive(string id, string ConnString)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                   "SP_SaveAdvanceFilter"
                   , CommandType.StoredProcedure
                   , conString
                   , new SqlParameter("Trx", "USE")
                   , new SqlParameter("RecID", id)
                   );
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetActiveFilter(string ReportName, string ConnString, string UserName)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string where = "";
                where = " isActive = 1 AND [User] = '" + UserName + "' AND ReportName = '" + ReportName + "' ";

                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[tbl_AdvFilter]")
                    , new SqlParameter("orderBy", "ReportName ASC, isActive Desc, RecID Desc")
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

        public DataTable GetActiveFilter(string ReportName, string ConnString, string UserName, string IsActive)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                   "SP_SaveAdvanceFilter"
                   , CommandType.StoredProcedure
                   , conString
                   , new SqlParameter("Trx", "GETACTIVEFILTER")
                   , new SqlParameter("ReportName", ReportName)
                   , new SqlParameter("Active", IsActive)
                   , new SqlParameter("User", UserName)
                   );
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetFilter(string ReportName, string ConnString, string UserName)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                   "SP_SaveAdvanceFilter"
                   , CommandType.StoredProcedure
                   , conString
                   , new SqlParameter("Trx", "GETDATAFILTER")
                   , new SqlParameter("ReportName", ReportName)
                   , new SqlParameter("User", UserName)
                   );
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetTaggingHistory(string DateFrom, string DateTo, string ConnString, string Filter)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            string filter = "[PrintStatus] = 'OK' AND CONVERT(date, DateCreated) BETWEEN '" + DateFrom + "' AND  DATEADD(day, 1, '" + DateTo + "') " + Filter;
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "[SP_TaggingHistory]"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("@Filter", filter)
                    );
            }
            catch (Exception ex)
            {
                string xx = ex.Message;
                throw;
            }
        }

        public int SaveFilter(string ReportName, string Username, string Logic, string Filter, string Operator, string Value, string ConnString)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                   "SP_SaveAdvanceFilter"
                   , CommandType.StoredProcedure
                   , conString
                   , new SqlParameter("Trx", "INSERT")
                   , new SqlParameter("ReportName", ReportName)
                   , new SqlParameter("Logic", Logic)
                   , new SqlParameter("FilterBy", Filter)
                   , new SqlParameter("Operator", Operator)
                   , new SqlParameter("Val", Value)
                   , new SqlParameter("User", Username)
                   );
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int UpdateFilter(string RecId, string ReportName, string Username, string Logic, string Filter, string Operator, string Value, string ConnString)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                   "SP_SaveAdvanceFilter"
                   , CommandType.StoredProcedure
                   , conString
                   , new SqlParameter("Trx", "UPDATE")
                   , new SqlParameter("Logic", Logic)
                   , new SqlParameter("FilterBy", Filter)
                   , new SqlParameter("Operator", Operator)
                   , new SqlParameter("Val", Value)
                   , new SqlParameter("RecId", RecId)
                   );
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private DateTime ConverttoString(string sVal)
        {
            DateTime myout = DateTime.Now;

            DateTime.TryParse(sVal, out myout);
            return myout;
        }
    }
}