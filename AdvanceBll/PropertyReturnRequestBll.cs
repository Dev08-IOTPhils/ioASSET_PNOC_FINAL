using AdvanceDal;
using AdvanceEntity;
using System;
using System.Data;

namespace AdvanceBll
{
    public class PropertyReturnRequestBll
    {
        private PropertyReturnRequestDal z = new PropertyReturnRequestDal();

        #region Datatables

        public int ApproveExtension(PropertyEntity x)
        {
            try
            {
                int id = z.ApproveExtension(x);
                return 1;
            }
            catch
            {
                return 1;
            }
        }

        public int DeclineExtension(PropertyEntity x)
        {
            try
            {
                int id = z.DeclineExtension(x);
                return 1;
            }
            catch
            {
                return 1;
            }
        }

        public DataTable GetPropertyAvailableForReturn(string search, string ConnString, int accountable, int custodianid, int ExpenseType)
        {
            try
            {
                return z.GetPropertyAvailableForReturn(search, ConnString, accountable, custodianid, ExpenseType);
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetPropertyReturnPendingUpload(string search, string ConnString, string accountable)
        {
            try
            {
                return z.GetPropertyReturnPendingUpload(search, ConnString, accountable);
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetPropertyReturnPendingUpload(string search, string ConnString, string custodianid, int siteid, string administrative)
        {
            try
            {
                return z.GetPropertyReturnPendingUpload(search, ConnString, custodianid, siteid, administrative);
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetReturnPropertyList(string ConnString, int accountable, int custodianid, string ExpenseType)
        {
            try
            {
                return z.GetReturnPropertyList(ConnString, accountable, custodianid, ExpenseType);
            }
            catch
            {
                throw;
            }
        }
        public DataTable PropertyExtensionDateRequestList(string search, string ConnString, string createdby)
        {
            try
            {
                return z.PropertyExtensionDateRequestList(search, ConnString, createdby);
            }
            catch
            {
                throw;
            }
        }

        //request management
        public DataTable PropertyExtensionDateRequestPendingApproval(string ConnString, string administrative, string approveruser)
        {
            try
            {
                if (administrative == "True")
                {
                    return z.PropertyExtensionDateRequestPendingApproval(ConnString);
                }
                else
                {
                    return z.PropertyExtensionDateRequestPendingApproval(ConnString, approveruser);
                }
            }
            catch
            {
                throw;
            }
        }

        public DataTable PropertyReturnRequestList(string ConnString, string returnrequestno)
        {
            try
            {
                return z.PropertyReturnRequestList(ConnString, returnrequestno);
            }
            catch
            {
                throw;
            }
        }
        #endregion Datatables

        #region Methods

        public int AddToRequestForExtensionReturnDate(PropertyEntity x)
        {
            try
            {
                int id = z.AddToRequestForExtensionReturnDate(x);
                return 1;
            }
            catch
            {
                return 1;
            }
        }

        public int CancelExtension(PropertyEntity x)
        {
            try
            {
                int id = z.CancelExtension(x);
                return 1;
            }
            catch
            {
                return 1;
            }
        }

        public int CancelReturnRequest(PropertyEntity x)
        {
            try
            {
                int id = z.CancelReturnRequest(x);
                return 1;
            }
            catch
            {
                return 1;
            }
        }

        public int ConfirmPropertyReturnRequest(PropertyEntity x)
        {
            try
            {
                int id = z.ConfirmPropertyReturnRequest(x);
                return 1;
            }
            catch (Exception ex)
            {
                return 1;
            }
        }

        public string GetCountPropertyReturnRequest(PropertyEntity x)
        {
            try
            {
                string result = "";

                DataTable dt = z.GetCountPropertyReturnRequest(x);

                result = dt.Rows[0]["Count"].ToString();
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public int RemoveAllToPropertyReturnRequest(PropertyEntity x)
        {
            try
            {
                int id = z.RemoveAllToPropertyReturnRequest(x);
                return 1;
            }
            catch
            {
                return 1;
            }
        }

        public int RemoveToPropertyReturnRequest(PropertyEntity x)
        {
            try
            {
                int id = z.RemoveToPropertyReturnRequest(x);
                return 1;
            }
            catch
            {
                return 1;
            }
        }

        public int TransferToPropertyReturnRequest(PropertyEntity x)
        {
            try
            {
                int id = z.TransferToPropertyReturnRequest(x);
                return 1;
            }
            catch
            {
                return 1;
            }
        }
        #endregion Methods

        #region Property Return History View

        public DataTable GetPropertyReturnPendingUploadView(string search, string datefrom, string dateto, string ConnString, int siteid)
        {
            try
            {
                return z.GetPropertyReturnPendingUploadView(search, datefrom, dateto, ConnString, siteid);
            }
            catch
            {
                throw;
            }
        }

        #endregion Property Return History View

        #region Property Return

        public int CancelReturn(PropertyEntity x)
        {
            try
            {
                int id = z.CancelReturn(x);
                return 1;
            }
            catch
            {
                return 1;
            }
        }

        public int ConfirmPropertyReturn(PropertyEntity x)
        {
            try
            {
                int id = z.ConfirmPropertyReturn(x);
                return 1;
            }
            catch (Exception ex)
            {
                return 1;
            }
        }

        public string GetCountPropertyReturn(PropertyEntity x)
        {
            try
            {
                string result = "";

                DataTable dt = z.GetCountPropertyReturn(x);

                result = dt.Rows[0]["Count"].ToString();
                return result;
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetForReturnPropertyList(string ConnString, int employeeid, int custodian, string ExpenseType)// add ExpenseType by nero 022624
        {
            try
            {
                return z.GetForReturnPropertyList(ConnString, employeeid, custodian, ExpenseType);// add ExpenseType by nero 022624
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetPropertyReturnPending(string search, string ConnString, int siteid, string custodian, string administrative)
        {
            try
            {
                return z.GetPropertyReturnPending(search, ConnString, siteid, custodian, administrative);
            }
            catch
            {
                throw;
            }
        }

        public DataTable PropertyReturnList(string ConnString, string returnno)
        {
            try
            {
                return z.PropertyReturnList(ConnString, returnno);
            }
            catch
            {
                throw;
            }
        }


        public int RemoveAllToPropertyReturn(PropertyEntity x)
        {
            try
            {
                int id = z.RemoveAllToPropertyReturn(x);
                return 1;
            }
            catch
            {
                return 1;
            }
        }

        public int RemoveToPropertyReturn(PropertyEntity x)
        {
            try
            {
                int id = z.RemoveToPropertyReturn(x);
                return 1;
            }
            catch
            {
                return 1;
            }
        }

        public int TransferToPropertyReturn(PropertyEntity x)
        {
            try
            {
                int id = z.TransferToPropertyReturn(x);
                return 1;
            }
            catch
            {
                return 1;
            }
        }

        //public int changeCondition(PropertyEntity x)
        //{
        //    try
        //    {
        //        int id = PropertyReturnRequestDal.changeCondition(x);
        //        return 1;
        //    }
        //    catch
        //    {
        //        return 1;
        //    }
        //}
        #endregion Property Return
    }
}