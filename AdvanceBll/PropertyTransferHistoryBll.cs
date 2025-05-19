using AdvanceDal;
using AdvanceEntity;
using System.Data;

namespace AdvanceBll
{
    public class PropertyTransferHistoryBll
    {
        private PropertyTransferhistoryDal y = new PropertyTransferhistoryDal();

        public DataTable PropertyListTransferHistory(PropertyEntity x)
        {
            try
            {
                return y.PropertyListTransferHistory(x);
            }
            catch
            {
                throw;
            }
        }

        public DataTable PropertyTransferHistory(string ConnString, string custodianid, int siteid, string search, string datefrom, string dateto, string Filter, string administrative)
        {
            try
            {
                return y.PropertyTransferHistory(ConnString, custodianid, siteid, search, datefrom, dateto, Filter, administrative);
            }
            catch
            {
                throw;
            }
        }
    }
}