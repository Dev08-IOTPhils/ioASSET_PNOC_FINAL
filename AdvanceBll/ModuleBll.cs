using AdvanceDal;
using AdvanceEntity;
using System.Data;

namespace AdvanceBll
{
    public class ModuleBll
    {
        private ActivityLogBll activityLogBLL = new ActivityLogBll();
        private ModuleDal moduleDAL = new ModuleDal();
        public string CheckArrangement(ModuleEntity module)
        {
            try
            {
                string result = "";

                DataTable dt = moduleDAL.CheckArrangement(module);

                result = dt.Rows[0][0].ToString();

                return result;
            }
            catch
            {
                throw;
            }
        }

        public int DeleteModule(ModuleEntity module)
        {
            try
            {
                string id = moduleDAL.DeleteModule(module);
                return 1;
            }
            catch
            {
                return 1;
            }
        }

        public DataTable GetModule(string ConnString)
        {
            try
            {
                return moduleDAL.GetModule(ConnString);
            }
            catch
            {
                throw;
            }
        }

        public string GetModuleById(ModuleEntity module)
        {
            try
            {
                string result = "";

                DataTable dt = moduleDAL.GetModuleById(module);

                result = dt.Rows[0]["ModuleID"].ToString() + "|" +
                         dt.Rows[0]["Name"].ToString() + "|" +
                         dt.Rows[0]["Description"].ToString() + "|" +
                         dt.Rows[0]["Arrangement"].ToString() + "|" +
                         dt.Rows[0]["URL"].ToString() + "|" +
                         dt.Rows[0]["ParentModuleID"].ToString() + "|" +
                         dt.Rows[0]["IsWebPage"].ToString() + "|" +
                         dt.Rows[0]["Active"].ToString();
                return result;
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetModuleWithPaging(string ConnString)
        {
            try
            {
                return moduleDAL.GetModuleWithPaging(ConnString);
            }
            catch
            {
                throw;
            }
        }

        public int SaveModule(ModuleEntity module)
        {
            int id = moduleDAL.SaveModule(module);
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