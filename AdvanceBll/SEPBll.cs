using AdvanceDal;
using AdvanceEntity;
using System.Data;

namespace AdvanceBll
{
    public class SEPBll
    {
        private SEPDal pcd = new SEPDal();

        public DataTable GetList(string ConnString, int type, string Filter)
        {
            try
            {
                return pcd.GetList(ConnString, type, Filter);
            }
            catch
            {
                throw;
            }
        }

        public DataTable getlists(LedgerCardEntity x)
        {
            try
            {
                return pcd.getlists(x);
            }
            catch
            {
                throw;
            }
        }
    }
}