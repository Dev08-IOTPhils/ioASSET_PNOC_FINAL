using AdvanceEntity;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace AdvanceDal
{
    public class ChartOfAccountsDal
    {
        public DataTable ChartOfAccountsbyID(ChartOfAccountsEntity coaEntity)
        {
            string conString = ConfigurationManager.ConnectionStrings[coaEntity.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_RetrieveChartOfAccounts"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("ChartOfAccountsID", coaEntity.ChartOfAccountsID)
                    );
            }
            catch
            {
                throw;
            }
        }

        public int DeleteChartOfAccounts(ChartOfAccountsEntity coaEntity)
        {
            string conString = ConfigurationManager.ConnectionStrings[coaEntity.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_DeleteChartOfAccounts"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("ChartOfAccountsID", coaEntity.ChartOfAccountsID)
                    , new SqlParameter("User", coaEntity.CreatedBy)
                    );
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetAssetInventoryAccount(string ConnString)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_RetrieveAssetInventoryAccount"
                    , CommandType.StoredProcedure
                    , conString
                    );
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetChartOfAccounts(string ConnString)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string where = "";
                where = "IsDeleted = 'False'";

                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[tbl_ChartOfAccounts]")
                    , new SqlParameter("orderBy", "Active Desc, ChartOfAccountsID DESC")
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

        public DataTable GetChartOfAccountsList(string ConnString)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_RetrieveChartOfAccounts"
                    , CommandType.StoredProcedure
                    , conString
                    );
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetExpenseAccount(string ConnString)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_RetrieveExpenseAccount"
                    , CommandType.StoredProcedure
                    , conString
                    );
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetIncomeAccount(string ConnString)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_RetrieveIncomeAccount"
                    , CommandType.StoredProcedure
                    , conString
                    );
            }
            catch
            {
                throw;
            }
        }

        public int SaveChartOfAccounts(ChartOfAccountsEntity coaEntity)
        {
            string conString = ConfigurationManager.ConnectionStrings[coaEntity.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_SaveChartOfAccounts"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("ChartOfAccountsID", coaEntity.ChartOfAccountsID)
                    , new SqlParameter("AccountNumber", coaEntity.AccountNumber)
                    , new SqlParameter("Description", coaEntity.Description)
                    , new SqlParameter("AccountHeader", coaEntity.AccountHeader)
                    , new SqlParameter("Balance", coaEntity.Balance)
                    , new SqlParameter("Active", coaEntity.Active)
                    , new SqlParameter("User", coaEntity.CreatedBy)
                    );
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}