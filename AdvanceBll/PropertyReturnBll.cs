using AdvanceDal;
using AdvanceEntity;
using System.Data;

namespace AdvanceBll
{
    public class PropertyReturnBll
    {


        private PropertyReturnDal z = new PropertyReturnDal();

        public int changeConditionReturn(PropertyEntity x)
        {
            try
            {
                int id = z.changeConditionReturn(x);
                return 1;
            }
            catch
            {
                return 1;
            }
        }

        public int changeConditionReturnRequest(PropertyEntity x)
        {
            try
            {
                int id = z.changeConditionReturnRequest(x);
                return 1;
            }
            catch
            {
                return 1;
            }
        }

        public DataTable GetPropertyAvailableForReturn(string search, int employeeid, string ConnString, int siteid, int custodianid, int ExpenseType)
        {
            try
            {
                return z.GetPropertyAvailableForReturn(search, employeeid, ConnString, siteid, custodianid, ExpenseType);
            }
            catch
            {
                throw;
            }
        }

        public int UploadReturnRequest(PropertyEntity x)
        {
            try
            {
                int id = z.UploadReturnRequest(x);
                return 1;
            }
            catch
            {
                return 1;
            }
        }

        public DataTable GetReturnHistory(string ConnString, int siteid, string search, string datefrom, string dateto, string Filter)
        {
            try
            {
                return PropertyReturnDal.GetReturnHistory(ConnString, siteid, search, datefrom, dateto, Filter);
            }
            catch
            {
                throw;
            }
        }

        public DataTable PDLPendingUpload(PropertyEntity x)
        {
            try
            {
                return PropertyReturnDal.PDLPendingUpload(x);
            }
            catch
            {
                throw;
            }
        }
    }
}