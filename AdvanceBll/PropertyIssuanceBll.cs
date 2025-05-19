using AdvanceDal;
using AdvanceEntity;
using System.Data;

namespace AdvanceBll
{
    public class PropertyIssuanceBll
    {
        private PropertyIssuanceDal propertyissuanceDal = new PropertyIssuanceDal();

        #region Property Issuance

        public int CancelPropertyIssuance(PropertyEntity x)
        {
            int id = propertyissuanceDal.CancelPropertyIssuance(x);
            try
            {
                return id;
            }
            catch
            {
                return id;
            }
        }

        public string ConfirmPropertyIssuance(PropertyEntity x)
        {
            string result = propertyissuanceDal.ConfirmPropertyIssuance(x);
            try
            {
                return result;
            }
            catch
            {
                return result;
            }
        }

        public DataTable GetAvailablePropertyForIssuance(string search, int custodianid, int siteid, string ConnString, int ExpenseType, int EmployeeID, string notlike)
        {
            try
            {
                return propertyissuanceDal.GetAvailablePropertyForIssuance(search, custodianid, siteid, ConnString, ExpenseType, EmployeeID, notlike);
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetPropertyIssuanceList(PropertyEntity x)
        {
            try
            {
                return propertyissuanceDal.GetPropertyIssuanceList(x);
            }
            catch
            {
                throw;
            }
        }

        public DataTable IssuancePendingUpload(string search, string ConnString, string custodianid, int siteid, string administrative)
        {
            try
            {
                return propertyissuanceDal.IssuancePendingUpload(search, custodianid, ConnString, siteid, administrative);
            }
            catch
            {
                throw;
            }
        }

        public DataTable PLPendingUpload(PropertyEntity x)
        {
            try
            {
                return propertyissuanceDal.PLPendingUpload(x);
            }
            catch
            {
                throw;
            }
        }

        public DataTable PLPendingUploadWithComponent(PropertyEntity x)
        {
            try
            {
                return propertyissuanceDal.PLPendingUploadWithComponent(x);
            }
            catch
            {
                throw;
            }
        }

        public string PropertyGetCount(PropertyEntity x)
        {
            try
            {
                string result = "";

                DataTable dt = propertyissuanceDal.PropertyGetCount(x);
                result = dt.Rows[0]["Count"].ToString();
                return result;
            }
            catch
            {
                throw;
            }
        }

        public string PropertyIssuanceDetails(PropertyEntity x)
        {
            try
            {
                string result = "";

                DataTable dt = propertyissuanceDal.PropertyIssuanceDetails(x);
                result = dt.Rows[0]["IssuanceNo"].ToString() + "|" +
                    dt.Rows[0]["EmployeeName"].ToString() + "|" +
                    dt.Rows[0]["EmployeeDepartment"].ToString() + "|" +
                    dt.Rows[0]["EmployeePosition"].ToString() + "|" +
                    dt.Rows[0]["ApproverName"].ToString() + "|" +
                    dt.Rows[0]["TransactorName"].ToString() + "|" +
                    dt.Rows[0]["TransactorDepartment"].ToString() + "|" +
                    dt.Rows[0]["TransactorPosition"].ToString() + "|" +
                    dt.Rows[0]["Custodian"].ToString() + "|" +
                    dt.Rows[0]["Remarks"].ToString() + "|" +
                    dt.Rows[0]["CreatedDate"].ToString() + "|" +
                    dt.Rows[0]["ApproverEmail"].ToString() + "|" +
                    dt.Rows[0]["TransactorEmail"].ToString() + "|" +
                    dt.Rows[0]["SiteName"].ToString() + "|" +
                    dt.Rows[0]["ApproverPosition"].ToString() + "|" +
                    dt.Rows[0]["IssuedBy"].ToString() + "|" +
                    dt.Rows[0]["IssuedPos"].ToString();
                return result;
            }
            catch
            {
                throw;
            }
        }

        public int PropertyRemoveAllToIssuance(PropertyEntity x)
        {
            try
            {
                int id = propertyissuanceDal.PropertyRemoveAllToIssuance(x);
                return 1;
            }
            catch
            {
                return 1;
            }
        }

        public int PropertyRemoveToIssuance(PropertyEntity x)
        {
            try
            {
                int id = propertyissuanceDal.PropertyRemoveToIssuance(x);
                return 1;
            }
            catch
            {
                return 1;
            }
        }

        public string PropertyRequestDetails(PropertyEntity x)
        {
            try
            {
                string result = "";

                DataTable dt = propertyissuanceDal.PropertyRequestDetails(x);
                result = dt.Rows[0]["RequestNo"].ToString() + "|" + /*0*/
                    dt.Rows[0]["RequestorName"].ToString() + "|" + /*1*/
                    dt.Rows[0]["RequestorDepartment"].ToString() + "|" +  /*2*/
                    dt.Rows[0]["RequestorPosition"].ToString() + "|" + /*3*/
                    dt.Rows[0]["ApproverName"].ToString() + "|" + /*4*/
                    dt.Rows[0]["TransactorName"].ToString() + "|" + /*5*/
                    dt.Rows[0]["TransactorDepartment"].ToString() + "|" + /*6*/
                    dt.Rows[0]["TransactorPosition"].ToString() + "|" + /*7*/
                    dt.Rows[0]["Custodian"].ToString() + "|" + /*8*/
                    dt.Rows[0]["Remarks"].ToString() + "|" + /*9*/
                    dt.Rows[0]["CreatedDate"].ToString() + "|" + /*10*/
                    dt.Rows[0]["ApproverEmail"].ToString() + "|" + /*11*/
                    dt.Rows[0]["DateOfReturn"].ToString() + "|" + /*12*/
                     dt.Rows[0]["SiteName"].ToString() + "|" + /*13*/
                    dt.Rows[0]["ApproverPosition"].ToString() + "|" + /*14*/
                    dt.Rows[0]["IssuedBy"].ToString() + "|" + /*15*/
                    dt.Rows[0]["IssuedPos"].ToString(); /*16*/

                return result;
            }
            catch
            {
                throw;
            }
        }

        public int PropertyTransferToIssuance(PropertyEntity x)
        {
            try
            {
                int id = propertyissuanceDal.PropertyTransferToIssuance(x);
                return 1;
            }
            catch
            {
                return 1;
            }
        }
        public int UploadPropertyIssuance(PropertyEntity x)
        {
            try
            {
                int id = propertyissuanceDal.UploadPropertyIssuance(x);
                return 1;
            }
            catch
            {
                return 1;
            }
        }
        #endregion Property Issuance

        #region request management

        public int ApprovePropertyIssuance(PropertyEntity x)
        {
            int id = propertyissuanceDal.ApprovePropertyIssuance(x);
            try
            {
                return id;
            }
            catch
            {
                return id;
            }
        }

        public int DeclinePropertyIssuance(PropertyEntity x)
        {
            int id = propertyissuanceDal.DeclinePropertyIssuance(x);
            try
            {
                return id;
            }
            catch
            {
                return id;
            }
        }

        public DataTable GetPendingApprovalPropertyIssuance(string approver, string isadministrative, string ConnString)
        {
            try
            {
                if (isadministrative == "True")
                {
                    return propertyissuanceDal.GetPendingApprovalPropertyIssuance(ConnString);
                }
                else
                {
                    return propertyissuanceDal.GetPendingApprovalPropertyIssuance(approver, ConnString);
                }
            }
            catch
            {
                throw;
            }
        }
        #endregion request management
    }
}