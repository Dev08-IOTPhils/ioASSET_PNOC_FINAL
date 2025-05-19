using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvanceDal
{
    public class JunkDisposalDal
    {
        public DataTable getJunkDisposal(string ConnString)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string where = " (Status = 'Pending Approval' or Status='Pending Verification' or Status = 'Pending Upload')";

                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "VW_JunkDisposalHeader")
                    , new SqlParameter("orderBy", "JunkDisposalID DESC")
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


        public int SaveJD(int JDid,string Mod, int approver, string remarks, string user, string ConnString)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_ConfirmJunkDisposal"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("DisposalID", JDid)
                    , new SqlParameter("ModeOfDisposal", Mod)
                    , new SqlParameter("Remarks", remarks)
                    , new SqlParameter("ApproverID", approver)
                    , new SqlParameter("User", user)
                    );
            }
            catch (Exception ex)
            {
                return 1;
            }
        }
        public int CompleteJD(string TransactionNo, string user, string ConnString)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_CompleteJunkDisposal"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("DisposalNo", TransactionNo)
                    , new SqlParameter("User", user)
                    );
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}
