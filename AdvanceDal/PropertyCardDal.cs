using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace AdvanceDal
{
    public class PropertyCardDal
    {
        public DataTable GetPropertyCard(string ConnString, string PropertyNumber)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string where = "";
                where = " PropertyNumber = '" + PropertyNumber + "'";

                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_PropertyCardConsolidated]")
                    , new SqlParameter("orderBy", "CreatedDate")
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

        public DataTable GetPropertyCardByItemCode(string ConnString, int ExpenseType, int PropertyID)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_RetrievePropertyByPARorICS"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("ExpenseType", ExpenseType)
                    , new SqlParameter("PropertyID", PropertyID)
                    );
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public DataTable GetPropertyList(string ConnString, int type, string Filter)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            string filter = "";
            if (type != 0)
            {
                filter = " RecID>0  " + Filter;
            }
            else
            {
                filter = " PropertyID>0  " + Filter;
            }
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_RetrievePropertyCard"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("Type", type)
                    , new SqlParameter("Filter", filter)
                    );
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}