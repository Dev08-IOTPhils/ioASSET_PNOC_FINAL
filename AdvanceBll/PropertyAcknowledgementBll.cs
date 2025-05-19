using AdvanceDal;
using AdvanceEntity;
using System.Data;

namespace AdvanceBll
{
    public class PropertyAcknowledgementBll
    {
        private PropertyAcknowledgementDal z = new PropertyAcknowledgementDal();

        public int AcknowledgedProperty(PropertyEntity x)
        {
            int ID = z.AcknowledgedProperty(x);
            try
            {
                return ID;
            }
            catch
            {
                return ID;
            }
        }

        public DataTable GetPropertyAcknowledge(string ConnString, string action, int UserID)
        {
            try
            {
                return z.GetPropertyAcknowledge(ConnString, action, UserID);
            }
            catch
            {
                throw;
            }
        }
    }
}