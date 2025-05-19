using AdvanceEntity;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace AdvanceDal
{
    public class UserDal
    {
        public int ChangePassword(string ConnString, UserEntity userEntity)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_ChangePassword"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("NewPassword", userEntity.NewPassword)
                    , new SqlParameter("User", userEntity.UserName)
                    );
            }
            catch
            {
                throw;
            }
        }

        public int CheckEmail(UserEntity userEntity)
        {
            string conString = ConfigurationManager.ConnectionStrings[userEntity.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_CheckEmail"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("EmailAddress", userEntity.EmailAddress)
                    );
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public int CheckEmpNo(UserEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_CheckEmpNo"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("UserID", x.UserID)
                    , new SqlParameter("EmployeeNo", x.EmployeeNo)
                    );
            }
            catch
            {
                throw;
            }
        }

        public int DeleteUser(UserEntity userEntity)
        {
            string conString = ConfigurationManager.ConnectionStrings[userEntity.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_DeleteUser"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("UserID", userEntity.UserID)
                    , new SqlParameter("User", userEntity.CreatedBy)
                    );
            }
            catch
            {
                throw;
            }
        }

        public int ForceLogOut(UserEntity userEntity)
        {
            string conString = ConfigurationManager.ConnectionStrings[userEntity.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_ForceLogout"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("UserID", userEntity.UserID)
                    , new SqlParameter("User", userEntity.UserName)
                    );
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetApproverByCustodian(UserEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_RetrieveApproverByCustodian"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("CustodianID", x.IntegratorID)
                    , new SqlParameter("WarehouseID", x.SiteID)
                    , new SqlParameter("UserID", x.UserID)
                    );
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetApproverInIssuance(UserEntity userEntity, int issuedto)
        {
            string conString = ConfigurationManager.ConnectionStrings[userEntity.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_RetrieveApproverIssuance"
                    , CommandType.StoredProcedure
                    , conString
                    //, new SqlParameter("CustodianID", userEntity.IntegratorID)
                    //, new SqlParameter("SiteID", userEntity.SiteID)
                    , new SqlParameter("UserID", userEntity.UserID)
                    , new SqlParameter("IssuedTo", issuedto)
                    );
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetApproverList(string ConnString)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_RetrieveApproverList"
                    , CommandType.StoredProcedure
                    , conString
                    );
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetApproverListBySite(string ConnString, int siteid, int userid)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_RetrieveApproverListBySite"
                    , CommandType.StoredProcedure
                    , conString
                    //, new SqlParameter("SiteID", siteid)
                    , new SqlParameter("UserID", userid)
                    );
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetApproverListBySiteCustodian(string ConnString, int siteid, int userid, int custodianid)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_RetrieveApproverListBySiteCustodian"
                    , CommandType.StoredProcedure
                    , conString
                    //, new SqlParameter("SiteID", siteid)
                    , new SqlParameter("UserID", userid)
                    , new SqlParameter("CustodianID", custodianid)
                    );
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetCertifier(UserEntity userEntity)
        {
            string conString = ConfigurationManager.ConnectionStrings[userEntity.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_RetrieveCertifier"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("SiteID", userEntity.SiteID)
                    , new SqlParameter("UserID", userEntity.UserID)
                    , new SqlParameter("ApproverID", userEntity.ApproverID)
                    //, new SqlParameter("CustodianID", userEntity.IntegratorID)
                    );
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetCertifierList(string ConnString)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_RetrieveCertifierList"
                    , CommandType.StoredProcedure
                    , conString
                    );
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetCustodianID(UserEntity user)
        {
            string conString = ConfigurationManager.ConnectionStrings[user.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                       "SP_RetrieveCustodianID"
                       , CommandType.StoredProcedure
                       , conString
                       , new SqlParameter("UserID", user.UserName)
                   );
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetEmail(string ConnString, int id)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                       "SP_RetrieveUser"
                       , CommandType.StoredProcedure
                       , conString
                       , new SqlParameter("UserID", id)
                   );
            }
            catch (Exception ex)
            {
                ex.ToString();
                throw;
            }
        }

        //Property Disposal
        public DataTable GetInspector(UserEntity userEntity)
        {
            string conString = ConfigurationManager.ConnectionStrings[userEntity.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_RetrieveInspector"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("SiteID", userEntity.SiteID)
                    , new SqlParameter("UserID", userEntity.UserID)
                    , new SqlParameter("ApproverID", userEntity.ApproverID)
                    //, new SqlParameter("CustodianID", userEntity.IntegratorID)
                    );
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetInspectorList(string ConnString)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_RetrieveInspectorList"
                    , CommandType.StoredProcedure
                    , conString
                    );
            }
            catch
            {
                throw;
            }
        }

        //database restore
        public DataTable GetModule(UserEntity userEntity)
        {
            string conString = ConfigurationManager.ConnectionStrings[userEntity.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_RetrieveRoles"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("UserName", userEntity.UserName)
                    );
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetPassword(string ConnString, UserEntity user)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                       "SP_RetrievePassword"
                       , CommandType.StoredProcedure
                       , conString
                       , new SqlParameter("UserName", user.UserName)
                   );
            }
            catch (Exception ex)
            {
                ex.ToString();
                throw;
            }
        }
        public DataTable GetUser(string ConnString)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string where = "";
                where = "IsDeleted IS NULL or IsDeleted = 0";
                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_User]")
                    , new SqlParameter("orderBy", "Active DESC, CreatedDate DESC")
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

        //Property Issuance
        public DataTable GetUser(UserEntity userEntity)
        {
            string conString = ConfigurationManager.ConnectionStrings[userEntity.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_RetrieveUserIssuance"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("DepartmentID", userEntity.DepartmentID)
                    , new SqlParameter("SiteID", userEntity.SiteID)
                    , new SqlParameter("UserID", userEntity.UserID)
                    );
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetUserByCustodian(UserEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_RetrieveUserByCustodian"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("CustodianID", x.IntegratorID)
                    , new SqlParameter("WarehouseID", x.SiteID)
                    , new SqlParameter("UserID", x.UserID)
                    );
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetUserByID(UserEntity userEntity)
        {
            string conString = ConfigurationManager.ConnectionStrings[userEntity.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_RetrieveUser"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("UserID", userEntity.UserID)
                    );
            }
            catch
            {
                throw;
            }
        }

        //Goods Received
        public DataTable GetUserList(string ConnString)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_RetrieveUserList"
                    , CommandType.StoredProcedure
                    , conString
                    );
            }
            catch
            {
                throw;
            }
        }

        //stock return
        public DataTable GetUserListBySite(string ConnString, int siteid)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_RetrieveUserStockIssuance"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("SiteID", siteid)
                    );
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetUserNoDepartment(UserEntity userEntity)
        {
            string conString = ConfigurationManager.ConnectionStrings[userEntity.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_RetrieveUserIssuance"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("SiteID", userEntity.SiteID)
                    , new SqlParameter("UserID", userEntity.UserID)
                    );
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetUserSystemInformation(string ConnString, UserEntity user)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                       "SP_RetrieveUserInfo"
                       , CommandType.StoredProcedure
                       , conString
                       , new SqlParameter("UserID", user.UserName)
                   );
            }
            catch
            {
                throw;
            }
        }

        //DICTchange
        public int ResetPassword(UserEntity userEntity)
        {
            string conString = ConfigurationManager.ConnectionStrings[userEntity.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_ResetPassword"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("UserID", userEntity.UserID)
                    , new SqlParameter("User", userEntity.CreatedBy)
                    , new SqlParameter("Password", userEntity.Password)
                    );
            }
            catch
            {
                throw;
            }
        }

        public int SaveUser(UserEntity userEntity, int noofminda, int nooftsuc)
        {
            string conString = ConfigurationManager.ConnectionStrings[userEntity.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_SaveUser"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("UserID", userEntity.UserID)
                    , new SqlParameter("FirstName", userEntity.FirstName)
                    , new SqlParameter("LastName", userEntity.LastName)
                    , new SqlParameter("EmployeeNo", userEntity.EmployeeNo)
                    , new SqlParameter("EmailAddress", userEntity.EmailAddress)
                    , new SqlParameter("Username", userEntity.UserName)
                    , new SqlParameter("DepartmentID", userEntity.DepartmentID)
                    , new SqlParameter("PositionID", userEntity.PositionID)
                    , new SqlParameter("IsCustodian", userEntity.IsCustodian)
                    , new SqlParameter("CustodianID", userEntity.Custodians)
                    , new SqlParameter("ImageExtension", userEntity.ImageExtension)
                    , new SqlParameter("IsAdministrative", userEntity.IsAdministrative)
                    , new SqlParameter("IsApprover", userEntity.IsApprover)
                    , new SqlParameter("IsInspector", userEntity.IsInspector)
                    , new SqlParameter("IsCertifier", userEntity.IsCertifier)
                    , new SqlParameter("Active", userEntity.Active)
                    , new SqlParameter("SiteID", userEntity.SiteID)
                    , new SqlParameter("User", userEntity.CreatedBy)
                    , new SqlParameter("Password", userEntity.Password)
                    , new SqlParameter("noofminda", noofminda)
                    , new SqlParameter("nooftsuc", nooftsuc)
                    );
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public int Security(string ConnString, UserEntity userEntity)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_LoginAttempt"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("UserName", userEntity.UserName)
                    );
            }
            catch
            {
                throw;
            }
        }

        public bool SQLValidateUser(string ConnString, UserEntity user)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                //SP check if user exist users table
                int result = DataAccess.MicrosoftDB.ExecuteScalar<int>(
                        "SP_CheckUserID"
                        , CommandType.StoredProcedure
                        , conString
                        , new SqlParameter("UserID", user.UserName)
                        , new SqlParameter("Password", user.Password)
                    );

                if (result == 1)
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }
    }
}