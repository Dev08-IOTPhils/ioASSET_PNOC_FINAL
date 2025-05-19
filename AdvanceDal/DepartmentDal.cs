using AdvanceEntity;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace AdvanceDal
{
    public class DepartmentDal
    {
        public int DeleteDepartment(DepartmentEntity departmentEntity)
        {
            string conString = ConfigurationManager.ConnectionStrings[departmentEntity.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_DeleteDepartment"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("DepartmentID", departmentEntity.DepartmentID)
                    , new SqlParameter("User", departmentEntity.User)
                    );
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetDepartment(string ConnString)
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
                    , new SqlParameter("datasrc", "[dbo].[tbl_Department]")
                    , new SqlParameter("orderBy", "Active Desc, DepartmentID DESC")
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

        public DataTable GetDepartmentByName(DepartmentEntity departmentEntity)
        {
            string conString = ConfigurationManager.ConnectionStrings[departmentEntity.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_RetrieveDepartment"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("DepartmentID", departmentEntity.DepartmentID)
                    );
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetDepartmentID(DepartmentEntity departmentEntity)
        {
            string conString = ConfigurationManager.ConnectionStrings[departmentEntity.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_RetrieveDepartmentID"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("PositionID", departmentEntity.PositionID)
                    );
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetDepartmentList(DepartmentEntity departmentEntity)
        {
            string conString = ConfigurationManager.ConnectionStrings[departmentEntity.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_RetrieveDepartment"
                    , CommandType.StoredProcedure
                    , conString
                    );
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetDepartmentList1(DepartmentEntity departmentEntity)
        {
            string conString = ConfigurationManager.ConnectionStrings[departmentEntity.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_RetrieveDepartment"
                    , CommandType.StoredProcedure
                    , conString
                    );
            }
            catch
            {
                throw;
            }
        }

        public int SaveDepartment(DepartmentEntity departmentEntity)
        {
            string conString = ConfigurationManager.ConnectionStrings[departmentEntity.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_SaveDepartment"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("DepartmentID", departmentEntity.DepartmentID)
                    , new SqlParameter("Department", departmentEntity.Department)
                    , new SqlParameter("Description", departmentEntity.Description)
                    , new SqlParameter("Active", departmentEntity.Active)
                    , new SqlParameter("User", departmentEntity.User)
                    );
            }
            catch
            {
                throw;
            }
        }
    }
}