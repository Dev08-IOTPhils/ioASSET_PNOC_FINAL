using AdvanceDal;
using System.Data;

namespace AdvanceBll
{
    public class PropertyRequestHistoryBll
    {
        private PropertyRequestHistoryDal z = new PropertyRequestHistoryDal();

        public DataTable RequestPendingUpload(string ConnString, int siteid, string search, string datefrom, string dateto, string Filter)
        {
            try
            {
                return z.RequestPendingUpload(ConnString, siteid, search, datefrom, dateto, Filter);
            }
            catch
            {
                throw;
            }
        }
    }
}