using AdvanceDal;
using AdvanceEntity;
using System.Data;

namespace AdvanceBll
{
    public class PropertyVerificationBll
    {
        private PropertyVerificationDal z = new PropertyVerificationDal();

        public DataTable GetPropertyVerification(string ConnString, string search, int transactiontype, int siteid)
        {
            try
            {
                return z.GetPropertyVerification(ConnString, search, transactiontype, siteid);
            }
            catch
            {
                throw;
            }
        }

        public int VerifyProperty(PropertyEntity x)
        {
            int id = z.VerifyProperty(x);
            try
            {
                return id;
            }
            catch
            {
                return id;
            }
        }

        public int VerifyPropertyByBarcode(PropertyEntity x)
        {
            int id = z.VerifyPropertyByBarcode(x);
            try
            {
                return id;
            }
            catch
            {
                return id;
            }
        }

        public int VerifyPropertyByTM(PropertyEntity x)
        {
            int id = z.VerifyPropertyByTM(x);
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