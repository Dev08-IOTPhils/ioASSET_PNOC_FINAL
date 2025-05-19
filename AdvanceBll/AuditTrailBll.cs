using AdvanceDal;
using System.Data;

namespace AdvanceBll
{
    public class AuditTrailBll
    {
        private AuditTrailDal z = new AuditTrailDal();

        public DataTable GetAuditTrail(string ConnString, string search, string datefrom, string dateto, int siteid, string IpAddress, string Filter)
        {
            try
            {
                return z.GetAuditTrail(ConnString, search, datefrom, dateto, siteid, IpAddress, Filter);
            }
            catch
            {
                throw;
            }
        }
    }
}