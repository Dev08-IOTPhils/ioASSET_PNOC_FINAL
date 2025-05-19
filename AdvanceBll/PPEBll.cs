using AdvanceDal;
using AdvanceEntity;
using System.Data;

namespace AdvanceBll
{
    public class PPEBll
    {
        private PPEDal pcd = new PPEDal();

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