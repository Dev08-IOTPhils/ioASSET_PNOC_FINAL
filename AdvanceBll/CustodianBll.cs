using AdvanceDal;
using AdvanceEntity;
using System.Data;

namespace AdvanceBll
{
    public class CustodianBll
    {
        private ActivityLogBll activityLogBLL = new ActivityLogBll();
        private CustodianDal CustodianDAL = new CustodianDal();
        public string CustodianbyID(CustodianEntity custodianEntity)
        {
            try
            {
                string result = "";

                DataTable dt = CustodianDAL.CustodianbyID(custodianEntity);

                result = dt.Rows[0]["CustodianID"].ToString() + "|" +
                         dt.Rows[0]["Custodian"].ToString() + "|" +
                         dt.Rows[0]["Description"].ToString() + "|" +
                         dt.Rows[0]["Active"].ToString();
                return result;
            }
            catch
            {
                throw;
            }
        }

        public int DeleteCustodian(CustodianEntity custodianEntity)
        {
            int returnVal = CustodianDAL.DeleteCustodian(custodianEntity);
            try
            {
                return returnVal;
            }
            catch
            {
                return returnVal;
            }
        }

        public DataTable GetCustodian(string ConnString)
        {
            try
            {
                return CustodianDAL.GetCustodian(ConnString);
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetCustodianList(string ConnString)
        {
            try
            {
                return CustodianDAL.GetCustodianList(ConnString);
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetUserCustodian(string Custodians, string administrative, string ConnString)
        {
            try
            {
                return CustodianDAL.GetUserCustodian(Custodians, administrative, ConnString);
            }
            catch
            {
                throw;
            }
        }

        public int SaveCustodian(CustodianEntity custodianEntity)
        {
            int id = CustodianDAL.SaveCustodian(custodianEntity);
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