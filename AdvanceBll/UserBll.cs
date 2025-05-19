using AdvanceDal;
using AdvanceEntity;
using IOTPIEncrypto;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Management;
using System.Text;
using System.Web.UI.WebControls;

namespace AdvanceBll
{
    public class UserBll
    {
        private LicenseEncrpyto licenseEncrypto = new LicenseEncrpyto();
        private UserDal userDal = new UserDal();
        public int ChangePassword(string ConnString, UserEntity userEntity)
        {
            try
            {
                int id = userDal.ChangePassword(ConnString, userEntity);
                return 1;
            }
            catch
            {
                return 1;
            }
        }

        public int CheckEmail(UserEntity userEntity)
        {
            int id = userDal.CheckEmail(userEntity);
            try
            {
                return id;
            }
            catch (Exception ex)
            {
                return id;
            }
        }

        public int CheckEmpNo(UserEntity x)
        {
            try
            {
                return userDal.CheckEmpNo(x);
            }
            catch
            {
                throw;
            }
        }

        public string CheckLicense()
        {
            int numberofdays = 0;
            try
            {
                RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\IOTPIEncryto\0787-6335-7190-1115-2014");

                if (registryKey != null)
                {
                    string secretFileValue = File.ReadAllText("C:\\Windows\\INF\\41336682-3658-1422-0000-9042766-IAM001.dll");
                    string decryptedValue = Decrypt(Decrypt(Decrypt(Decrypt(Decrypt(secretFileValue)))));
                    string[] decryptedValueArray = decryptedValue.Split('|');

                    object value = registryKey.GetValue("AM001");
                    string rgstryKey = value.ToString();
                    string _LicenseKey = Decrypt(Decrypt(Decrypt(Decrypt(Decrypt(rgstryKey)))));

                    string serialNumber = string.Empty;
                    string processId = string.Empty;
                    string uuid = string.Empty;

                    string ComputerInfo = string.Empty;
                    string query = "SELECT * FROM Win32_BaseBoard";
                    ManagementObjectSearcher searcher =
                        new ManagementObjectSearcher(query);
                    foreach (ManagementObject info in searcher.Get())
                    {
                        try { ComputerInfo += (info.GetPropertyValue("SerialNumber").ToString()); break; } catch (Exception) { }
                    }
                    ComputerInfo += "|";
                    query = "Select * FROM Win32_Processor";
                    searcher = new ManagementObjectSearcher(query);
                    foreach (ManagementObject info in searcher.Get())
                    {
                        try { ComputerInfo += (info.GetPropertyValue("ProcessorId").ToString()); break; } catch (Exception) { }
                    }
                    ComputerInfo += "|";
                    query = "Select * FROM Win32_ComputerSystemProduct";
                    searcher = new ManagementObjectSearcher(query);
                    foreach (ManagementObject info in searcher.Get())
                    {
                        try { ComputerInfo += (info.GetPropertyValue("UUID").ToString()); break; } catch (Exception) { }
                    }
                    ComputerInfo += "|";

                    //Split the License Key Part
                    string[] _SplitLicenseKey = _LicenseKey.Split('|');

                    //Get Computer Details
                    string _LisenceComputerDetails = _SplitLicenseKey[0] + "|" + _SplitLicenseKey[1] + "|" + _SplitLicenseKey[2] + "|" + _SplitLicenseKey[3] + "|";
                    //string _ApplicationComputerDetails = _ClientCode + "|" + _SerialNumber + "|" + _ProcessorID + "|" + _UUID + "|";
                    string _ApplicationComputerDetails = _SplitLicenseKey[0] + "|" + ComputerInfo;
                    //
                    if (_LisenceComputerDetails == _ApplicationComputerDetails)
                    {
                        //Check License Status
                        if (_SplitLicenseKey[5] == "1")
                        {
                            //Check Expiry Date
                            if (Convert.ToDateTime(_SplitLicenseKey[4]) < DateTime.Now)
                            {
                                return "invalid";
                            }
                        }
                    }
                    else
                    {
                        return "invalid";
                    }

                    return "alright";
                }
                else
                {
                    string secretFileValue = File.ReadAllText(@"C:\Windows\INF\41336682-3658-1422-0000-9042766-IAM001.dll");

                    string decryptedValue = Decrypt(Decrypt(Decrypt(Decrypt(Decrypt(secretFileValue)))));
                    string[] decryptedValueArray = decryptedValue.Split('|');

                    DateTime dateofexpiry = DateTime.ParseExact(decryptedValueArray[10], "MM/dd/yyyy", null);
                    if (Convert.ToDateTime(decryptedValueArray[10]) >= DateTime.Now)
                    {
                        numberofdays = (Convert.ToDateTime(decryptedValueArray[10]) - DateTime.Now).Days + 1;
                    }
                    else
                    {
                        numberofdays = 404; //not found or activation failed
                    }

                    return numberofdays.ToString();
                }
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public string Decrypt(string cipherText)
        {
            try
            { return licenseEncrypto.Decrypt("Succe55", cipherText); }
            catch (Exception)
            { return "Unable to decrypt the data"; }
        }

        public int DeleteUser(UserEntity userEntity)
        {
            int id = userDal.DeleteUser(userEntity);
            try
            {
                return id;
            }
            catch
            {
                return id;
            }
        }

        public string Encrypt(string defaultText)
        {
            try
            { return licenseEncrypto.Encrypt("Succe55", defaultText); }
            catch (Exception)
            { return "Unable to encrypt the data"; }
        }

        public int ForceLogOut(UserEntity userEntity)
        {
            int id = userDal.ForceLogOut(userEntity);
            try
            {
                return id;
            }
            catch
            {
                return id;
            }
        }

        public List<ListItem> GetApproverByCustodian(UserEntity x)
        {
            List<ListItem> position = new List<ListItem>();
            try
            {
                DataTable dt = userDal.GetApproverByCustodian(x);

                StringBuilder sb = new StringBuilder();
                foreach (DataRow dr in dt.Rows)
                {
                    //foreach (DataColumn col in dt.Columns)
                    //{
                    position.Add(new ListItem
                    {
                        Value = dr[1].ToString(),
                        Text = dr[0].ToString()
                    });
                    //}
                }
                return position;
            }
            catch
            {
                throw;
            }
        }

        public List<ListItem> GetApproverForRetagging(string conString)
        {
            List<ListItem> employee = new List<ListItem>();
            try
            {
                DataTable dt = userDal.GetApproverList(conString);

                foreach (DataRow dr in dt.Rows)
                {
                    employee.Add(new ListItem
                    {
                        Value = dr[0].ToString(),
                        Text = dr[1].ToString()
                    });
                }
                return employee;
            }
            catch
            {
                throw;
            }
        }

        public List<ListItem> GetApproverInIssuance(UserEntity userEntity, int issuedto)
        {
            List<ListItem> employee = new List<ListItem>();
            try
            {
                DataTable dt = userDal.GetApproverInIssuance(userEntity, issuedto);

                foreach (DataRow dr in dt.Rows)
                {
                    employee.Add(new ListItem
                    {
                        Value = dr[0].ToString(),
                        Text = dr[1].ToString()
                    });
                }
                return employee;
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetApproverList(string ConnString)
        {
            try
            {
                return userDal.GetApproverList(ConnString);
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetApproverListBySite(string ConnString, int siteid, int userid)
        {
            try
            {
                return userDal.GetApproverListBySite(ConnString, siteid, userid);
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetApproverListBySiteCustodian(string ConnString, int siteid, int userid, int custodianid)
        {
            try
            {
                return userDal.GetApproverListBySiteCustodian(ConnString, siteid, userid, custodianid);
            }
            catch
            {
                throw;
            }
        }

        public List<ListItem> GetCertifier(UserEntity userEntity)
        {
            List<ListItem> employee = new List<ListItem>();
            try
            {
                DataTable dt = userDal.GetCertifier(userEntity);

                foreach (DataRow dr in dt.Rows)
                {
                    employee.Add(new ListItem
                    {
                        Value = dr[0].ToString(),
                        Text = dr[1].ToString()
                    });
                }
                return employee;
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetCertifierList(string ConnString)
        {
            try
            {
                return userDal.GetCertifierList(ConnString);
            }
            catch
            {
                throw;
            }
        }

        public string GetCustodianID(UserEntity user)
        {
            try
            {
                string result = "";

                DataTable dt = userDal.GetCustodianID(user);

                result = dt.Rows[0]["CustodianID"].ToString();
                return result;
            }
            catch
            {
                throw;
            }
        }

        public string GetEmail(string ConnString, int id)
        {
            string result = " | ";

            DataTable dt = userDal.GetEmail(ConnString, id);
            if (dt.Rows.Count > 0)
            {
                result = dt.Rows[0]["EmailAddress"].ToString() + "|" + dt.Rows[0]["FirstName"].ToString() + " " + dt.Rows[0]["LastName"].ToString();
                return result;
            }
            else
            {
                return result;
            }
        }

        //PropertyDisposal
        public List<ListItem> GetInspector(UserEntity userEntity)
        {
            List<ListItem> employee = new List<ListItem>();
            try
            {
                DataTable dt = userDal.GetInspector(userEntity);

                foreach (DataRow dr in dt.Rows)
                {
                    employee.Add(new ListItem
                    {
                        Value = dr[0].ToString(),
                        Text = dr[1].ToString()
                    });
                }
                return employee;
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetInspectorList(string ConnString)
        {
            try
            {
                return userDal.GetInspectorList(ConnString);
            }
            catch
            {
                throw;
            }
        }

        //Database Restore
        public DataTable GetModule(UserEntity userEntity)
        {
            try
            {
                return userDal.GetModule(userEntity);
            }
            catch
            {
                throw;
            }
        }

        public string GetPassword(string ConnString, UserEntity user)
        {
            try
            {
                string result = "";

                DataTable dt = userDal.GetPassword(ConnString, user);
                if (dt.Columns.Count == 1)
                {
                    result = dt.Rows[0]["LoginAttempt"].ToString();
                    //return result; //comment if LC is on
                }
                else
                {
                    result = dt.Rows[0]["Password"].ToString() + "|" +
                        dt.Rows[0]["LoginAttempt"].ToString();
                    //return result; //comment if LC is on
                }

                #region uncomment if LC is on

                string LC = LicenseChecker.CheckLicense();
                if (LC == "alright")
                    return result;
                else if (LC.Contains("You have"))
                    return result;
                else if (LC == "404")
                    return "404";
                else
                    return "No License Found.";

                #endregion uncomment if LC is on
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public DataTable GetUser(string ConnString)
        {
            try
            {
                return userDal.GetUser(ConnString);
            }
            catch
            {
                throw;
            }
        }

        //Property Issuance
        public List<ListItem> GetUser(UserEntity userEntity)
        {
            List<ListItem> employee = new List<ListItem>();
            try
            {
                DataTable dt = userDal.GetUser(userEntity);

                foreach (DataRow dr in dt.Rows)
                {
                    employee.Add(new ListItem
                    {
                        Value = dr[0].ToString(),
                        Text = dr[1].ToString()
                    });
                }
                return employee;
            }
            catch
            {
                throw;
            }
        }

        public List<ListItem> GetUserByCustodian(UserEntity x)
        {
            List<ListItem> position = new List<ListItem>();
            try
            {
                DataTable dt = userDal.GetUserByCustodian(x);

                StringBuilder sb = new StringBuilder();
                foreach (DataRow dr in dt.Rows)
                {
                    //foreach (DataColumn col in dt.Columns)
                    //{
                    position.Add(new ListItem
                    {
                        Value = dr[1].ToString(),
                        Text = dr[0].ToString()
                    });
                    //}
                }
                return position;
            }
            catch
            {
                throw;
            }
        }

        public string GetUserByID(UserEntity userEntity)
        {
            try
            {
                string result = "";

                DataTable dtUser = userDal.GetUserByID(userEntity);

                result = dtUser.Rows[0]["UserID"].ToString() + "|" + //0
                        dtUser.Rows[0]["FirstName"].ToString() + "|" +
                        dtUser.Rows[0]["LastName"].ToString() + "|" + //2
                        dtUser.Rows[0]["CardNo"].ToString() + "|" +
                        dtUser.Rows[0]["DepartmentID"].ToString() + "|" + //4
                        dtUser.Rows[0]["PositionID"].ToString() + "|" +
                        dtUser.Rows[0]["EmailAddress"].ToString() + "|" + //6
                        dtUser.Rows[0]["UserName"].ToString() + "|" +
                        dtUser.Rows[0]["Active"].ToString() + "|" + //8
                        dtUser.Rows[0]["IsApprover"].ToString() + "|" +
                        dtUser.Rows[0]["IsCertifier"].ToString() + "|" + //10
                        dtUser.Rows[0]["IsInspector"].ToString() + "|" +
                        dtUser.Rows[0]["CustodianID"].ToString() + "|" + //12
                        dtUser.Rows[0]["EmployeeNo"].ToString() + "|" + //13
                        dtUser.Rows[0]["SiteID"].ToString() + "|" + //14
                        dtUser.Rows[0]["IsAdministrative"].ToString() + "|" + //15
                        dtUser.Rows[0]["IsCustodian"].ToString() + "|" + // added by nero 16
                dtUser.Rows[0]["Image"].ToString(); // added by ghghe 17

                return result;
            }
            catch
            {
                throw;
            }
        }

        //Goods Received
        public DataTable GetUserList(string ConnString)
        {
            try
            {
                return userDal.GetUserList(ConnString);
            }
            catch
            {
                throw;
            }
        }

        //StockReturn
        public DataTable GetUserListBySite(string ConnString, int siteid)
        {
            try
            {
                return userDal.GetUserListBySite(ConnString, siteid);
            }
            catch
            {
                throw;
            }
        }

        public List<ListItem> GetUserNoDepartment(UserEntity userEntity)
        {
            List<ListItem> employee = new List<ListItem>();
            try
            {
                DataTable dt = userDal.GetUserNoDepartment(userEntity);

                foreach (DataRow dr in dt.Rows)
                {
                    employee.Add(new ListItem
                    {
                        Value = dr[0].ToString(),
                        Text = dr[1].ToString()
                    });
                }
                return employee;
            }
            catch
            {
                throw;
            }
        }

        public string GetUserSystemInformation(string ConnString, UserEntity user)
        {
            try
            {
                string result = "";

                DataTable dt = userDal.GetUserSystemInformation(ConnString, user);

                result = dt.Rows[0]["UserID"].ToString() + "|" +
                     dt.Rows[0]["UserName"].ToString() + "|" +
                    dt.Rows[0]["FirstName"].ToString() + "|" +
                    dt.Rows[0]["LastName"].ToString() + "|" +
                    dt.Rows[0]["RoleID"].ToString() + "|" +
                    dt.Rows[0]["CreatedDate"].ToString() + "|" +
                    dt.Rows[0]["EmailAddress"].ToString() + "|" +
                    dt.Rows[0]["IsApprover"].ToString() + "|" +
                    dt.Rows[0]["IsInspector"].ToString() + "|" +
                    dt.Rows[0]["IsCertifier"].ToString() + "|" +
                    dt.Rows[0]["Image"].ToString() + "|" +
                    dt.Rows[0]["CustodianID"].ToString() + "|" +
                    dt.Rows[0]["SiteID"].ToString() + "|" +
                    dt.Rows[0]["Password"].ToString() + "|" +
                    dt.Rows[0]["IsAdministrative"].ToString() + "|" +
                        dt.Rows[0]["IsCustodian"].ToString() + "|" +
                        dt.Rows[0]["FirstLogin"].ToString() + "|" +
                        dt.Rows[0]["IsLogin"].ToString() + "|" +
                        dt.Rows[0]["RoleName"].ToString();
                // + "|" + dt.Rows[0]["Signature"].ToString();

                return result;
            }
            catch
            {
                throw;
            }
        }

        public int ResetPassword(UserEntity userEntity)
        {
            int id = userDal.ResetPassword(userEntity);
            try
            {
                return id;
            }
            catch
            {
                return id;
            }
        }

        public int SaveUser(UserEntity userEntity, int noofminda, int nooftsuc)
        {
            int id = userDal.SaveUser(userEntity, noofminda, nooftsuc);
            try
            {
                return id;
            }
            catch (Exception ex)
            {
                return id;
            }
        }

        public int Security(string ConnString, UserEntity userEntity)
        {
            int id = userDal.Security(ConnString, userEntity);
            try
            {
                return id;
            }
            catch (Exception ex)
            {
                return id;
            }
        }

        //added for getting email
        public bool SQLValidateUser(string ConnString, UserEntity user)
        {
            return userDal.SQLValidateUser(ConnString, user);
        }
    }
}