using AdvanceDal;
using AdvanceEntity;
using System.Data;

namespace AdvanceBll
{
    public class PropertyIssuanceHistoryBll
    {
        private PropertyIssuanceHistoryDal y = new PropertyIssuanceHistoryDal();

        public DataTable IssuancePendingUpload(string ConnString, int siteid, string search, string datefrom, string dateto, string Filter)
        {
            try
            {
                return y.IssuancePendingUpload(ConnString, siteid, search, datefrom, dateto, Filter);
            }
            catch
            {
                throw;
            }
        }

        public DataTable PLPendingUpload(PropertyEntity x)
        {
            try
            {
                return y.PLPendingUpload(x);
            }
            catch
            {
                throw;
            }
        }
    }
}