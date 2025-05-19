using AdvanceDal;
using AdvanceEntity;
using System.Data;

namespace AdvanceBll
{
    public class ChartOfAccountsBll
    {
        private ActivityLogBll activityLogBLL = new ActivityLogBll();
        private ChartOfAccountsDal ChartOfAccountsDal = new ChartOfAccountsDal();
        public string ChartOfAccountsbyID(ChartOfAccountsEntity coaEntity)
        {
            try
            {
                string result = "";

                DataTable dt = ChartOfAccountsDal.ChartOfAccountsbyID(coaEntity);

                result = dt.Rows[0]["ChartOfAccountsID"].ToString() + "|" +
                         dt.Rows[0]["AccountNumber"].ToString() + "|" +
                         dt.Rows[0]["Description"].ToString() + "|" +
                         dt.Rows[0]["AccountHeader"].ToString() + "|" +
                         dt.Rows[0]["Balance"].ToString() + "|" +
                         dt.Rows[0]["Active"].ToString();
                return result;
            }
            catch
            {
                throw;
            }
        }

        public int DeleteChartOfAccounts(ChartOfAccountsEntity coaEntity)
        {
            int returnVal = ChartOfAccountsDal.DeleteChartOfAccounts(coaEntity);
            try
            {
                return returnVal;
            }
            catch
            {
                return returnVal;
            }
        }

        public DataTable GetAssetInventoryAccount(string ConnString)
        {
            try
            {
                return ChartOfAccountsDal.GetAssetInventoryAccount(ConnString);
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetChartOfAccounts(string ConnString)
        {
            try
            {
                return ChartOfAccountsDal.GetChartOfAccounts(ConnString);
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetChartOfAccountsList(string ConnString)
        {
            try
            {
                return ChartOfAccountsDal.GetChartOfAccountsList(ConnString);
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetExpenseAccount(string ConnString)
        {
            try
            {
                return ChartOfAccountsDal.GetExpenseAccount(ConnString);
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetIncomeAccount(string ConnString)
        {
            try
            {
                return ChartOfAccountsDal.GetIncomeAccount(ConnString);
            }
            catch
            {
                throw;
            }
        }

        public int SaveChartOfAccounts(ChartOfAccountsEntity coaEntity)
        {
            int id = ChartOfAccountsDal.SaveChartOfAccounts(coaEntity);
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