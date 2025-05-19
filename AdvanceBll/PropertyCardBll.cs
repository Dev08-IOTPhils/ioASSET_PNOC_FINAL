using AdvanceDal;
using System.Data;

namespace AdvanceBll
{
    public class PropertyCardBll
    {
        private PropertyCardDal pcd = new PropertyCardDal();

        public DataTable GetPropertyCard(string ConnString, string PropertyNumber)
        {
            try
            {
                return pcd.GetPropertyCard(ConnString, PropertyNumber);
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetPropertyCardByItemCode(string ConnString, int ExpenseType, int PropertyID)
        {
            try
            {
                return pcd.GetPropertyCardByItemCode(ConnString, ExpenseType, PropertyID);
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetPropertyList(string ConnString, int type, string Filter)
        {
            try
            {
                return pcd.GetPropertyList(ConnString, type, Filter);
            }
            catch
            {
                throw;
            }
        }
    }
}