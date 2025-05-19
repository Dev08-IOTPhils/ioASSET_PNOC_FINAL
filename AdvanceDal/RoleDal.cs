using AdvanceEntity;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace AdvanceDal
{
    public class RoleDal
    {
        public int AddRoleMember(RoleEntity roleEntity)
        {
            string conString = ConfigurationManager.ConnectionStrings[roleEntity.ConnectionString].ToString();
            try
            {
                foreach (RoleMember roleMember in roleEntity.GetSetRoleMember)
                {
                    DataAccess.MicrosoftDB.ExecuteCommand(
                        "SP_AddRoleMember_"
                        , CommandType.StoredProcedure
                        , conString
                        , new SqlParameter("RoleID", roleEntity.RoleID)
                        , new SqlParameter("UserID", roleMember.UserID)
                    );
                }

                return 1;
            }
            catch
            {
                return 0;
            }
        }

        public int AddRoleModule(RoleEntity roleEntity)
        {
            string conString = ConfigurationManager.ConnectionStrings[roleEntity.ConnectionString].ToString();
            try
            {
                foreach (RoleAccess roleAccess in roleEntity.GetSetRoleAccess)
                {
                    DataAccess.MicrosoftDB.ExecuteCommand(
                        "SP_AddRoleAccess"
                        , CommandType.StoredProcedure
                        , conString
                        , new SqlParameter("RoleID", roleEntity.RoleID)
                        , new SqlParameter("ModuleID", roleAccess.ModuleID)
                    );
                }
                return 1;
            }
            catch
            {
                return 0;
            }
        }

        public int AddRoleModuleA(RoleEntity roleEntity)
        {
            string conString = ConfigurationManager.ConnectionStrings[roleEntity.ConnectionString].ToString();
            try
            {
                foreach (RoleAccess roleAccess in roleEntity.GetSetRoleAccess)
                {
                    DataAccess.MicrosoftDB.ExecuteCommand(
                        "SP_AddApproverAccess"
                        , CommandType.StoredProcedure
                        , conString
                        , new SqlParameter("RoleID", roleEntity.RoleID)
                        , new SqlParameter("ModuleID", roleAccess.ModuleID)
                    );
                }
                return 1;
            }
            catch
            {
                return 0;
            }
        }

        public DataTable CheckA(int OperatorGroupId, string ConString)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_RetrieveOperatorGroupModuleAccess"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("RoleID", OperatorGroupId)
                    );
            }
            catch
            {
                throw;
            }
        }

        public DataTable CheckR(string url, int OperatorGroupId, string ConString)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_RetrieveOperatorGroupButtonAccess"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("RoleID", OperatorGroupId)
                    , new SqlParameter("URL", url)
                    );
            }
            catch
            {
                throw;
            }
        }

        public int CountSubRoleModuleAccess(string ConnString, int roleID, int parentModuleID)
        {
            string conString1 = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            return DataAccess.MicrosoftDB.ExecuteScalar<int>(
             "SP_CountSubRoleModuleAccess"
             , CommandType.StoredProcedure
             , conString1
             , new SqlParameter("RoleID", roleID)
             , new SqlParameter("ParentModuleID", parentModuleID)
             );
        }

        public int CountSubRoleModuleAccessA(string ConnString, int roleID, int parentModuleID)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            return DataAccess.MicrosoftDB.ExecuteScalar<int>(
             "SP_CountSubRoleModuleAccess"
             , CommandType.StoredProcedure
             , conString
             , new SqlParameter("RoleID", roleID)
             , new SqlParameter("ParentModuleID", parentModuleID)
             );
        }

        public DataTable GetRole(string ConnString)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[tbl_Role]")
                    , new SqlParameter("orderBy", "Active DESC, Name ASC")
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

        //Module Access
        public DataTable GetRoleModuleAccess(string ConnString, int roleID)
        {
            string conString1 = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            return DataAccess.MicrosoftDB.GetDataTable(
               "SP_RetrieveRoleModuleAccess",
               CommandType.StoredProcedure,
               conString1,
               new SqlParameter("RoleID", roleID)
            );
        }

        public DataTable GetRoleModuleAccessA(string ConnString, int roleID)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            return DataAccess.MicrosoftDB.GetDataTable(
               "SP_RetrieveRoleModuleAccess",
               CommandType.StoredProcedure,
               conString,
               new SqlParameter("RoleID", roleID)
            );
        }

        public DataTable GetSubRoleModuleAccess(string ConnString, int roleID, int parentModuleID)
        {
            string conString1 = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            return DataAccess.MicrosoftDB.GetDataTable(
               "SP_RetrieveRoleModuleAccess",
               CommandType.StoredProcedure,
               conString1,
               new SqlParameter("RoleID", roleID),
               new SqlParameter("ParentModuleID", parentModuleID)
            );
        }

        public DataTable GetSubRoleModuleAccessA(string ConnString, int roleID, int parentModuleID)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            return DataAccess.MicrosoftDB.GetDataTable(
               "SP_RetrieveRoleModuleAccess",
               CommandType.StoredProcedure,
               conString,
               new SqlParameter("RoleID", roleID),
               new SqlParameter("ParentModuleID", parentModuleID)
            );
        }

        public int RemoveRoleMember(RoleEntity roleEntity)
        {
            string conString = ConfigurationManager.ConnectionStrings[roleEntity.ConnectionString].ToString();
            try
            {
                foreach (RoleMember roleMember in roleEntity.GetSetRoleMember)
                {
                    DataAccess.MicrosoftDB.ExecuteCommand(
                        "SP_RemoveRoleMember"
                        , CommandType.StoredProcedure
                        , conString
                        , new SqlParameter("RoleID", roleEntity.RoleID)
                        , new SqlParameter("UserID", roleMember.UserID)
                    );
                }
                return 1;
            }
            catch
            {
                return 0;
            }
        }

        //module
        public int RemoveRoleModule(RoleEntity roleEntity)
        {
            string conString = ConfigurationManager.ConnectionStrings[roleEntity.ConnectionString].ToString();
            try
            {
                foreach (RoleAccess roleAccess in roleEntity.GetSetRoleAccess)
                {
                    DataAccess.MicrosoftDB.ExecuteCommand(
                        "SP_RemoveRoleAccess"
                        , CommandType.StoredProcedure
                        , conString
                        , new SqlParameter("RoleID", roleEntity.RoleID)
                        , new SqlParameter("ModuleID", roleAccess.ModuleID)
                    );
                }
                return 1;
            }
            catch
            {
                return 0;
            }
        }

        public int RemoveRoleModuleA(RoleEntity roleEntity)
        {
            string conString = ConfigurationManager.ConnectionStrings[roleEntity.ConnectionString].ToString();
            try
            {
                foreach (RoleAccess roleAccess in roleEntity.GetSetRoleAccess)
                {
                    DataAccess.MicrosoftDB.ExecuteCommand(
                        "SP_RemoveApproverAccess"
                        , CommandType.StoredProcedure
                        , conString
                        , new SqlParameter("RoleID", roleEntity.RoleID)
                        , new SqlParameter("ModuleID", roleAccess.ModuleID)
                    );
                }
                return 1;
            }
            catch
            {
                return 0;
            }
        }

        public DataTable RetrieveExistingOperatorGroupModuleButton(RoleEntity roleEntity)
        {
            string conString = ConfigurationManager.ConnectionStrings[roleEntity.ConnectionString].ToString();
            return DataAccess.MicrosoftDB.GetDataTable(
               "SP_RetrieveExistingRoleModuleButton",
               CommandType.StoredProcedure,
               conString,
               new SqlParameter("RoleID", roleEntity.RoleID)
            );
        }

        public DataTable RetrieveExistingRoleMember(RoleEntity roleEntity)
        {
            string conString = ConfigurationManager.ConnectionStrings[roleEntity.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_RetrieveExistingRoleMember"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("RoleID", roleEntity.RoleID)
                    );
            }
            catch
            {
                throw;
            }
        }

        public DataTable RetrieveExistingRoleModule(RoleEntity roleEntity)
        {
            string conString = ConfigurationManager.ConnectionStrings[roleEntity.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_RetrieveExistingRoleModule"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("RoleID", roleEntity.RoleID)
                    );
            }
            catch
            {
                throw;
            }
        }

        public DataTable RetrieveExistingRoleModuleA(RoleEntity roleEntity)
        {
            string conString = ConfigurationManager.ConnectionStrings[roleEntity.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_RetrieveExistingRoleModuleA"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("UserID", roleEntity.RoleID)
                    );
            }
            catch
            {
                throw;
            }
        }

        //member
        public DataTable RetrieveNotExistingRoleMember(RoleEntity roleEntity)
        {
            string conString = ConfigurationManager.ConnectionStrings[roleEntity.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_RetrieveNotExistingRoleMember"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("RoleID", roleEntity.RoleID)
                    );
            }
            catch
            {
                throw;
            }
        }

        public DataTable RetrieveNotExistingRoleModule(RoleEntity roleEntity)
        {
            string conString = ConfigurationManager.ConnectionStrings[roleEntity.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_RetrieveNotExistingRoleModule"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("RoleID", roleEntity.RoleID)
                    );
            }
            catch
            {
                throw;
            }
        }

        //approver
        public DataTable RetrieveNotExistingRoleModuleA(RoleEntity roleEntity)
        {
            string conString = ConfigurationManager.ConnectionStrings[roleEntity.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_RetrieveNotExistingRoleModuleA"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("UserID", roleEntity.RoleID)
                    );
            }
            catch
            {
                throw;
            }
        }

        public DataTable RoleByID(RoleEntity roleEntity)
        {
            string conString = ConfigurationManager.ConnectionStrings[roleEntity.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_RetrieveRole"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("RoleID", roleEntity.RoleID)
                    );
            }
            catch
            {
                throw;
            }
        }

        public int SaveRole(RoleEntity roleEntity)
        {
            string conString = ConfigurationManager.ConnectionStrings[roleEntity.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_SaveRole"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("RoleID", roleEntity.RoleID)
                    , new SqlParameter("Name", roleEntity.RoleName)
                    , new SqlParameter("Description", roleEntity.Description)
                    , new SqlParameter("Active", roleEntity.Active)
                    , new SqlParameter("User", roleEntity.User)
                    );
            }
            catch (Exception ex)
            {
                return 1;
            }
        }
        public int UpdateAddBtn(string Connstring, string OperatorGroupId, string ModuleId, string buttonaccess)
        {
            string conString = ConfigurationManager.ConnectionStrings[Connstring].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_UpdateViewBtn"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("RoleID", OperatorGroupId)
                    , new SqlParameter("ModuleID", ModuleId)
                    , new SqlParameter("ButtonAccess", buttonaccess)
                    );
            }
            catch (Exception ex)
            {
                return 1;
            }
        }

        public int UpdateDelBtn(string Connstring, string OperatorGroupId, string ModuleId, string buttonaccess)
        {
            string conString = ConfigurationManager.ConnectionStrings[Connstring].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_UpdateViewBtn"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("RoleID", OperatorGroupId)
                    , new SqlParameter("ModuleID", ModuleId)
                    , new SqlParameter("ButtonAccess", buttonaccess)
                    );
            }
            catch (Exception ex)
            {
                return 1;
            }
        }

        public int UpdateEditBtn(string Connstring, string OperatorGroupId, string ModuleId, string buttonaccess)
        {
            string conString = ConfigurationManager.ConnectionStrings[Connstring].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_UpdateViewBtn"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("RoleID", OperatorGroupId)
                    , new SqlParameter("ModuleID", ModuleId)
                    , new SqlParameter("ButtonAccess", buttonaccess)
                    );
            }
            catch (Exception ex)
            {
                return 1;
            }
        }

        public int UpdateViewBtn(string Connstring, string OperatorGroupId, string ModuleId, string buttonaccess)
        {
            string conString = ConfigurationManager.ConnectionStrings[Connstring].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_UpdateViewBtn"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("RoleID", OperatorGroupId)
                    , new SqlParameter("ModuleID", ModuleId)
                    , new SqlParameter("ButtonAccess", buttonaccess)
                    );
            }
            catch (Exception ex)
            {
                return 1;
            }
        }
    }
}