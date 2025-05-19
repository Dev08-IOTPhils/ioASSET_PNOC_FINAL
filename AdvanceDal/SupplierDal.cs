using AdvanceEntity;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace AdvanceDal
{
    public class SupplierDal
    {
        public int DeleteSupplier(SupplierEntity supplierEntity)
        {
            string conString = ConfigurationManager.ConnectionStrings[supplierEntity.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_DeleteSupplier"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("SupplierID", supplierEntity.SupplierID)
                    , new SqlParameter("User", supplierEntity.CreatedBy)
                    );
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetServiceProviderList(string ConnString)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_RetrieveServiceProvider"
                    , CommandType.StoredProcedure
                    , conString
                    );
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetSupplier(string ConnString)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string where = "";

                where = "IsDeleted = 'False'";
                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[tbl_Supplier]")
                    , new SqlParameter("orderBy", "Active DESC, SupplierID DESC")
                    , new SqlParameter("fieldlist", "*")
                    , new SqlParameter("filter", where)
                    , new SqlParameter("pageNum", 0)
                    , new SqlParameter("pageSize", 0)
                    );
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetSupplierByID(SupplierEntity supplierEntity)
        {
            string conString = ConfigurationManager.ConnectionStrings[supplierEntity.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_RetrieveSupplier"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("SupplierID", supplierEntity.SupplierID)
                    );
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetSupplierList(string ConnString)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_RetrieveSupplier"
                    , CommandType.StoredProcedure
                    , conString
                    );
            }
            catch
            {
                throw;
            }
        }

        public int SaveSupplier(SupplierEntity supplierEntity)
        {
            string conString = ConfigurationManager.ConnectionStrings[supplierEntity.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_SaveSupplier"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("SupplierID", supplierEntity.SupplierID)
                    , new SqlParameter("Address", supplierEntity.Address)
                    , new SqlParameter("ContactPerson", supplierEntity.ContactPerson)
                    , new SqlParameter("ContactNumber", supplierEntity.ContactNumber)
                    , new SqlParameter("Supplier", supplierEntity.Supplier)
                    , new SqlParameter("Description", supplierEntity.Description)
                    , new SqlParameter("Active", supplierEntity.Active)
                    , new SqlParameter("User", supplierEntity.CreatedBy)
                    , new SqlParameter("Email", supplierEntity.Email)
                    );
            }
            catch
            {
                throw;
            }
        }
    }
}