using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace AdvanceDal
{
    public class PropertyMasterlist
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

        public DataTable GetFilterColumn(string ConnString, string ModuleViewName)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_ViewColumnName"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("ModuleViewName", ModuleViewName)
                    //, new SqlParameter("EndDate", enddate)
                    );
            }
            catch (Exception e)
            {
                throw;
            }
        }

        //widgets
        public DataTable GetPropertyByStatus(string ConnString, string status)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_SearchPropertyByStatus"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("Status", status)
                    );
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public DataTable GetPropertyInformation(string ConnString, string search)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_SearchPropertyInformation"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("Search", search)
                    );
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public DataTable GetPropertyMasterlist(string ConnString, string search, string Filter)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_SearchPropertyMasterlist"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("Filter", Filter)
                    );
            }
            catch (Exception e)
            {
                throw;
            }
        }
        public DataTable GetTop10Property(string ConnString, string itemcode)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_SearchTop10Property"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("ItemCode", itemcode)
                    //, new SqlParameter("StartDate", startdate)
                    //, new SqlParameter("EndDate", enddate)
                    );
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public DataTable GetTop10Stock(string ConnString, string itemcode)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_SearchTop10Stock"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("ItemCode", itemcode)
                    //, new SqlParameter("StartDate", startdate)
                    //, new SqlParameter("EndDate", enddate)
                    );
            }
            catch (Exception e)
            {
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
    }
}