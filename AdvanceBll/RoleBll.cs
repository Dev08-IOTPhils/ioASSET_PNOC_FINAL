using AdvanceDal;
using AdvanceEntity;
using System;
using System.Collections.Generic;
using System.Data;

namespace AdvanceBll
{
    public class RoleBll
    {
        private ActivityLogBll activityLogBLL = new ActivityLogBll();
        private RoleDal roleDal = new RoleDal();
        public int AddRoleMember(RoleEntity roleEntity)
        {
            try
            {
                roleDal.AddRoleMember(roleEntity);

                return 1;
            }
            catch
            {
                return 0;
            }
        }

        public int AddRoleModule(RoleEntity roleEntity)
        {
            try
            {
                roleDal.AddRoleModule(roleEntity);

                return 1;
            }
            catch
            {
                return 0;
            }
        }

        public int AddRoleModuleA(RoleEntity roleEntity)
        {
            try
            {
                roleDal.AddRoleModuleA(roleEntity);

                return 1;
            }
            catch
            {
                return 0;
            }
        }

        public List<string> CheckA(int OperatorGroupId, string ConString)
        {
            try
            {
                List<string> ModuleAccess = new List<string>();
                DataTable dt = roleDal.CheckA(OperatorGroupId, ConString);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ModuleAccess.Add(dt.Rows[i]["Name"].ToString());
                    }
                }
                return ModuleAccess;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public string CheckR(string url, int OperatorGroupId, string ConString)
        {
            try
            {
                string result = "";

                DataTable dt = roleDal.CheckR(url, OperatorGroupId, ConString);

                result = dt.Rows[0]["ButtonAccess"].ToString();
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public int CountSubRoleModuleAccess(string ConnString, int roleID, int parentModuleID)
        {
            try
            {
                return roleDal.CountSubRoleModuleAccess(ConnString, roleID, parentModuleID);
            }
            catch
            {
                throw;
            }
        }

        public int CountSubRoleModuleAccessA(string ConnString, int roleID, int parentModuleID)
        {
            try
            {
                return roleDal.CountSubRoleModuleAccessA(ConnString, roleID, parentModuleID);
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetRole(string ConnString)
        {
            try
            {
                return roleDal.GetRole(ConnString);
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetRoleModuleAccess(string ConnString, int roleID)
        {
            try
            {
                return roleDal.GetRoleModuleAccess(ConnString, roleID);
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetRoleModuleAccessA(string ConnString, int roleID)
        {
            try
            {
                return roleDal.GetRoleModuleAccessA(ConnString, roleID);
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetSubRoleModuleAccess(string ConnString, int roleID, int parentModuleID)
        {
            try
            {
                return roleDal.GetSubRoleModuleAccess(ConnString, roleID, parentModuleID);
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetSubRoleModuleAccessA(string ConnString, int roleID, int parentModuleID)
        {
            try
            {
                return roleDal.GetSubRoleModuleAccessA(ConnString, roleID, parentModuleID);
            }
            catch
            {
                throw;
            }
        }

        public int RemoveRoleMember(RoleEntity roleEntity)
        {
            try
            {
                roleDal.RemoveRoleMember(roleEntity);

                return 1;
            }
            catch
            {
                return 0;
            }
        }

        public int RemoveRoleModule(RoleEntity roleEntity)
        {
            try
            {
                roleDal.RemoveRoleModule(roleEntity);

                return 1;
            }
            catch
            {
                return 0;
            }
        }

        public int RemoveRoleModuleA(RoleEntity roleEntity)
        {
            try
            {
                roleDal.RemoveRoleModuleA(roleEntity);

                return 1;
            }
            catch
            {
                return 0;
            }
        }

        public DataTable RetrieveExistingOperatorGroupModuleButton(RoleEntity roleEntity)
        {
            try
            {
                return roleDal.RetrieveExistingOperatorGroupModuleButton(roleEntity);
            }
            catch
            {
                throw;
            }
        }

        //member
        public DataTable RetrieveExistingRoleMember(RoleEntity roleEntity)
        {
            try
            {
                return roleDal.RetrieveExistingRoleMember(roleEntity);
            }
            catch
            {
                throw;
            }
        }

        public DataTable RetrieveExistingRoleModule(RoleEntity roleEntity)
        {
            try
            {
                return roleDal.RetrieveExistingRoleModule(roleEntity);
            }
            catch
            {
                throw;
            }
        }

        public DataTable RetrieveExistingRoleModuleA(RoleEntity roleEntity)
        {
            try
            {
                return roleDal.RetrieveExistingRoleModuleA(roleEntity);
            }
            catch
            {
                throw;
            }
        }

        public DataTable RetrieveNotExistingRoleMember(RoleEntity roleEntity)
        {
            try
            {
                return roleDal.RetrieveNotExistingRoleMember(roleEntity);
            }
            catch
            {
                throw;
            }
        }

        //access
        public DataTable RetrieveNotExistingRoleModule(RoleEntity roleEntity)
        {
            try
            {
                return roleDal.RetrieveNotExistingRoleModule(roleEntity);
            }
            catch
            {
                throw;
            }
        }

        //approver
        public DataTable RetrieveNotExistingRoleModuleA(RoleEntity roleEntity)
        {
            try
            {
                return roleDal.RetrieveNotExistingRoleModuleA(roleEntity);
            }
            catch
            {
                throw;
            }
        }

        public string RoleByID(RoleEntity roleEntity)
        {
            try
            {
                string result = "";

                DataTable dt = roleDal.RoleByID(roleEntity);

                result = dt.Rows[0]["RoleID"].ToString() + "|" +
                         dt.Rows[0]["Name"].ToString() + "|" +
                         dt.Rows[0]["Description"].ToString() + "|" +
                         dt.Rows[0]["Active"].ToString();
                return result;
            }
            catch
            {
                throw;
            }
        }

        public int SaveRole(RoleEntity roleEntity)
        {
            int id = roleDal.SaveRole(roleEntity);
            try
            {
                //if (id == 0)
                //    activityLogBLL.SaveActivityLog(roleEntity.User, "Role [" + roleEntity.RoleName + "] successfully inserted.");
                //else
                //    activityLogBLL.SaveActivityLog(roleEntity.User, "Role [" + roleEntity.RoleName + "] successfully modified.");

                return id;
            }
            catch (Exception ex)
            {
                return id;
            }
        }
        public int UpdateAddBtn(string ConnString, string OperatorGroupId, string ModuleId, string buttonaccess)
        {
            try
            {
                return roleDal.UpdateAddBtn(ConnString, OperatorGroupId, ModuleId, buttonaccess);
            }
            catch
            {
                throw;
            }
        }

        public int UpdateDelBtn(string ConnString, string OperatorGroupId, string ModuleId, string buttonaccess)
        {
            try
            {
                return roleDal.UpdateDelBtn(ConnString, OperatorGroupId, ModuleId, buttonaccess);
            }
            catch
            {
                throw;
            }
        }

        public int UpdateEditBtn(string ConnString, string OperatorGroupId, string ModuleId, string buttonaccess)
        {
            try
            {
                return roleDal.UpdateEditBtn(ConnString, OperatorGroupId, ModuleId, buttonaccess);
            }
            catch
            {
                throw;
            }
        }

        public int UpdateViewBtn(string ConnString, string OperatorGroupId, string ModuleId, string buttonaccess)
        {
            try
            {
                return roleDal.UpdateViewBtn(ConnString, OperatorGroupId, ModuleId, buttonaccess);
            }
            catch
            {
                throw;
            }
        }
    }
}