using AdvanceDal;
using AdvanceEntity;
using System.Data;

namespace AdvanceBll
{
    public class SupplierBll
    {
        private ActivityLogBll activityLogBLL = new ActivityLogBll();
        private SupplierDal supplierDal = new SupplierDal();
        public int DeleteSupplier(SupplierEntity supplierEntity)
        {
            int returnVal = supplierDal.DeleteSupplier(supplierEntity);
            try
            {
                return returnVal;
            }
            catch
            {
                return returnVal;
            }
        }

        public DataTable GetServiceProviderList(string ConnString)
        {
            try
            {
                return supplierDal.GetServiceProviderList(ConnString);
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetSupplier(string ConnString)
        {
            try
            {
                return supplierDal.GetSupplier(ConnString);
            }
            catch
            {
                throw;
            }
        }

        public string GetSupplierByID(SupplierEntity supplierEntity)
        {
            try
            {
                string result = "";

                DataTable dtSupplier = supplierDal.GetSupplierByID(supplierEntity);

                result = dtSupplier.Rows[0]["SupplierID"].ToString() + "|" +
                        dtSupplier.Rows[0]["SupplierName"].ToString() + "|" +
                        dtSupplier.Rows[0]["Address"].ToString() + "|" +
                        dtSupplier.Rows[0]["ContactPerson"].ToString() + "|" +
                        dtSupplier.Rows[0]["ContactNumber"].ToString() + "|" +
                        dtSupplier.Rows[0]["Description"].ToString() + "|" +
                        dtSupplier.Rows[0]["Active"].ToString() + "|" +
                        dtSupplier.Rows[0]["Email"].ToString();

                return result;
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetSupplierList(string ConnString)
        {
            try
            {
                return supplierDal.GetSupplierList(ConnString);
            }
            catch
            {
                throw;
            }
        }

        public int SaveSupplier(SupplierEntity supplierEntity)
        {
            int id = supplierDal.SaveSupplier(supplierEntity);
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