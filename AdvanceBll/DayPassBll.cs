using AdvanceDal;
using AdvanceEntity;
using System;
using System.Data;

namespace AdvanceBll
{
    public class DayPassBll
    {
        private DayPassDal z = new DayPassDal();

        public int CancelDayPassRequest(DayPassEntity x)
        {
            try
            {
                int id = z.CancelDayPassRequest(x);
                return 1;
            }
            catch
            {
                return 1;
            }
        }

        public string ConfirmPropertyDayPassRequest(DayPassEntity x)
        {
            try
            {
                return z.ConfirmPropertyDayPassRequest(x);
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public string GetCountPropertyDayPassRequest(DayPassEntity x)
        {
            try
            {
                string result = "";

                DataTable dt = z.GetCountPropertyDayPassRequest(x);

                result = dt.Rows[0]["Count"].ToString();
                return result;
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetPropertyDayPass(string ConnString, string user)
        {
            try
            {
                return z.GetPropertyDayPass(ConnString, user);
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetPropertyDayPassPendingApproval(string ConnString, string accountable)
        {
            try
            {
                return z.GetPropertyDayPassPendingApproval(ConnString, accountable);
            }
            catch
            {
                throw;
            }
        }

        public int PropertyAddToDayPass(DayPassEntity x)
        {
            int id = z.PropertyAddToDayPass(x);
            try
            {
                return id;
            }
            catch
            {
                return id;
            }
        }

        public string PropertyDayPassDetails(DayPassEntity x)
        {
            try
            {
                string result = "";

                DataTable dt = z.PropertyDayPassDetails(x);
                result = dt.Rows[0]["DayPassNumber"].ToString() + "|" +
                    dt.Rows[0]["TransactorName"].ToString() + "|" +
                    dt.Rows[0]["ApproverName"].ToString() + "|" +
                    dt.Rows[0]["Remarks"].ToString() + "|" +
                    dt.Rows[0]["CreatedDate"].ToString() + "|" +
                    dt.Rows[0]["TransactorEmail"].ToString() + "|" +
                    dt.Rows[0]["ApproverEmail"].ToString();
                return result;
            }
            catch
            {
                throw;
            }
        }

        public DataTable PropertyDayPassRequestList(string ConnString, string daypassno)
        {
            try
            {
                return z.PropertyDayPassRequestList(ConnString, daypassno);
            }
            catch
            {
                throw;
            }
        }
        public int PropertyRemoveToDayPass(DayPassEntity x)
        {
            int id = z.PropertyRemoveToDayPass(x);
            try
            {
                return id;
            }
            catch
            {
                return id;
            }
        }
        #region Request Management

        public int ApprovePropertyDayPass(DayPassEntity x)
        {
            int id = z.ApprovePropertyDayPass(x);
            try
            {
                return id;
            }
            catch
            {
                return id;
            }
        }

        public int DeclinePropertyDayPass(DayPassEntity x)
        {
            int id = z.DeclinePropertyDayPass(x);
            try
            {
                return id;
            }
            catch
            {
                return id;
            }
        }

        public DataTable GetPropertyDayPassPendingApprovalRM(string ConnString, string administrative, string approver)
        {
            try
            {
                if (administrative == "True")
                {
                    return z.GetPropertyDayPassPendingApprovalRM(ConnString);
                }
                else
                {
                    return z.GetPropertyDayPassPendingApprovalRM(ConnString, approver);
                }
            }
            catch
            {
                throw;
            }
        }
        #endregion Request Management

        #region DayPass View

        public int CheckIn(DayPassEntity x)
        {
            try
            {
                int id = z.CheckIn(x);
                return 1;
            }
            catch
            {
                return 1;
            }
        }

        public int CheckOut(DayPassEntity x)
        {
            try
            {
                int id = z.CheckOut(x);
                return 1;
            }
            catch
            {
                return 1;
            }
        }

        public DataTable GetPropertyDayPassApproved(string ConnString, int site, string search, string Filter)
        {
            try
            {
                return z.GetPropertyDayPassApproved(ConnString, site, search, Filter);
            }
            catch
            {
                throw;
            }
        }
        #endregion DayPass View
    }
}