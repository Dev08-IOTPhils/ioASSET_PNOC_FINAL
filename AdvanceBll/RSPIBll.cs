using AdvanceDal;
using AdvanceEntity;
using System.Data;

namespace AdvanceBll
{
    public class RSPIBll
    {
        private RSPIDal pcd = new RSPIDal();

        public DataTable GetList(string ConnString, int type, string Filter, string datefrom, string dateto)
        {
            try
            {
                return pcd.GetList(ConnString, type, Filter, datefrom, dateto);
            }
            catch
            {
                throw;
            }
        }

        public DataTable getlists(RSPIEntity x, string datefrom, string dateto)
        {
            try
            {
                return pcd.getlists(x, datefrom, dateto);
            }
            catch
            {
                throw;
            }
        }
    }
}