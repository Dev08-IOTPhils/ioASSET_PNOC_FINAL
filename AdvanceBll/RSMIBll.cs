using AdvanceDal;
using System.Data;

namespace AdvanceBll
{
    public class RSMIBll
    {
        private RSMIDal pcd = new RSMIDal();

        public DataTable GetList(string ConnString, int type, string Filter, int departmentid, string datefrom, string dateto)
        {
            try
            {
                return pcd.GetList(ConnString, type, Filter, departmentid, datefrom, dateto);
            }
            catch
            {
                throw;
            }
        }

        public DataTable getlists(string ConnString, int departmentid, string datefrom, string dateto)
        {
            try
            {
                return pcd.getlists(ConnString, departmentid, datefrom, dateto);
            }
            catch
            {
                throw;
            }
        }

        public DataTable getlists1(string ConnString, int departmentid, string datefrom, string dateto)
        {
            try
            {
                return pcd.getlists1(ConnString, departmentid, datefrom, dateto);
            }
            catch
            {
                throw;
            }
        }
    }
}