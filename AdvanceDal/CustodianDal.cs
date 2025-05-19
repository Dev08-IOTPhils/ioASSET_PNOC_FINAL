using AdvanceEntity;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace AdvanceDal
{
    public class CustodianDal
    {
        public DataTable CustodianbyID(CustodianEntity custodianEntity)
        {
            string conString = ConfigurationManager.ConnectionStrings[custodianEntity.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_RetrieveCustodian"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("CustodianID", custodianEntity.CustodianID)
                    );
            }
            catch
            {
                throw;
            }
        }

        public int DeleteCustodian(CustodianEntity custodianEntity)
        {
            string conString = ConfigurationManager.ConnectionStrings[custodianEntity.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_DeleteCustodian"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("CustodianID", custodianEntity.CustodianID)
                    , new SqlParameter("User", custodianEntity.CreatedBy)
                    );
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetCustodian(string ConnString)
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
                    , new SqlParameter("datasrc", "[dbo].[tbl_Custodian]")
                    , new SqlParameter("orderBy", "Active Desc, CustodianID DESC")
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

        public DataTable GetCustodianList(string ConnString)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_RetrieveCustodian"
                    , CommandType.StoredProcedure
                    , conString
                    );
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetUserCustodian(string Custodians, string administrative, string ConnString)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                if (administrative == "True")
                {
                    return DataAccess.MicrosoftDB.GetDataTable(
                        "SP_RetrieveUserCustodian"
                        , CommandType.StoredProcedure
                        , conString
                        , new SqlParameter("Administrative", administrative)
                        , new SqlParameter("Custodians", Custodians)
                        );
                }
                else
                {
                    return DataAccess.MicrosoftDB.GetDataTable(
                        "SP_RetrieveUserCustodian"
                        , CommandType.StoredProcedure
                        , conString
                        , new SqlParameter("Custodians", Custodians)
                        );
                }
            }
            catch
            {
                throw;
            }
        }

        public int SaveCustodian(CustodianEntity custodianEntity)
        {
            string conString = ConfigurationManager.ConnectionStrings[custodianEntity.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_SaveCustodian"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("CustodianID", custodianEntity.CustodianID)
                    , new SqlParameter("Custodian", custodianEntity.Custodian)
                    , new SqlParameter("Description", custodianEntity.Description)
                    , new SqlParameter("Active", custodianEntity.Active)
                    , new SqlParameter("User", custodianEntity.CreatedBy)
                    );
            }
            catch
            {
                throw;
            }
        }
    }
}