using AdvanceDal;
using System.Data;

namespace AdvanceBll
{
    public class PropertyMovementBll
    {
        private PropertyMovementDal z = new PropertyMovementDal();

        public DataTable GetPropertyMovement(string ConnString, string search, string datefrom, string dateto, int siteid, string Filter)
        {
            try
            {
                return z.GetPropertyMovement(ConnString, search, datefrom, dateto, siteid, Filter);
            }
            catch
            {
                throw;
            }
        }
    }
}