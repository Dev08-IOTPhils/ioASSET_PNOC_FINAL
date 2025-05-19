using AdvanceEntity;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace AdvanceDal
{
    public class ModuleDal
    {
        public DataTable CheckArrangement(ModuleEntity module)
        {
            string conString = ConfigurationManager.ConnectionStrings[module.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_CheckArrangementModule"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("ModuleID", module.ModuleID)
                    , new SqlParameter("Arrangement", module.Arrangement)
                    , new SqlParameter("ParentModuleID", module.ParentModuleID)
                    );
            }
            catch
            {
                throw;
            }
        }

        public string DeleteModule(ModuleEntity moduleEntity)
        {
            string conString = ConfigurationManager.ConnectionStrings[moduleEntity.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<string>(
                    "SP_DeleteModule"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("ModuleID", moduleEntity.ModuleID)
                    , new SqlParameter("User", moduleEntity.User)
                    );
            }
            catch
            {
                return "";
            }
        }

        public DataTable GetModule(string ConnString)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                   "SP_RetrieveModuleList"
                   , CommandType.StoredProcedure
                   , conString
                   );
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetModuleById(ModuleEntity module)
        {
            string conString = ConfigurationManager.ConnectionStrings[module.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_RetrieveModule"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("ModuleID", module.ModuleID)
                    );
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetModuleWithPaging(string ConnString)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "VW_Module")
                    , new SqlParameter("orderBy", "Active DESC, ParentModuleID ASC, Arrangement ASC")
                    , new SqlParameter("fieldlist", "*")
                    , new SqlParameter("filter", "")
                    , new SqlParameter("pageNum", 0)
                    , new SqlParameter("pageSize", 0)
                    );
            }
            catch
            {
                throw;
            }
        }

        public int SaveModule(ModuleEntity module)
        {
            string conString = ConfigurationManager.ConnectionStrings[module.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_SaveModule"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("ModuleID", module.ModuleID)
                    , new SqlParameter("Name", module.Name)
                    , new SqlParameter("Description", module.Description)
                    , new SqlParameter("Arrangement", module.Arrangement)
                    , new SqlParameter("URL", module.URL)
                    , new SqlParameter("ParentModuleID", module.ParentModuleID)
                    , new SqlParameter("WebPage", module.IsWebPage)
                    , new SqlParameter("Active", module.Active)
                    , new SqlParameter("User", module.User)
                    );
            }
            catch
            {
                return 1;
            }
        }
    }
}