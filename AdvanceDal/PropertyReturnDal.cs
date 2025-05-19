using AdvanceEntity;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace AdvanceDal
{
    public class PropertyReturnDal
    {

        public int changeConditionReturn(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_PropertyChangeReason"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("RecID", x.RecID)
                    , new SqlParameter("Reason", x.Condition)
                    , new SqlParameter("User", x.CreatedBy)
                    );
            }
            catch
            {
                throw;
            }
        }

        public int changeConditionReturnRequest(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_PropertyChangeReasonRequest"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("RecID", x.RecID)
                    , new SqlParameter("Reason", x.Condition)
                    , new SqlParameter("User", x.CreatedBy)
                    );
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetPropertyAvailableForReturn(string search, int employeeid, string ConnString, int siteid, int custodianid, int ExpenseType)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_SearchAvailablePropertyForReturn"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("WarehouseID", siteid)
                    , new SqlParameter("CustodianID", custodianid)
                    , new SqlParameter("Search", search)
                    , new SqlParameter("EmployeeID", employeeid)
                    , new SqlParameter("ExpenseType", ExpenseType)
                    );
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public int UploadReturnRequest(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_UploadReturnRequest"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("ReturnRequestNo", x.ReturnRequestNumber)
                    , new SqlParameter("Remarks", x.Remarks)
                    , new SqlParameter("User", x.CreatedBy)
                    );
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static DataTable GetReturnHistory(string ConnString, int siteid, string search, string datefrom, string dateto, string Filter)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            string filter = " Status != 'Pending Upload' and Status!= 'Pending Verification' and Status != 'On Maintenance' and (CreatedDate between '" + datefrom + "' and DateAdd(day, 1, '" + dateto + "'))  " + Filter;

            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_SearchPropertyReturnHistory"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("Filter", filter)
                    //, new SqlParameter("SiteID", siteid)
                    //, new SqlParameter("Search", search)
                    //, new SqlParameter("DateFrom", datefrom)
                    //, new SqlParameter("DateTo", dateto)
                    );
            }
            catch (Exception e)
            {
                throw;
            }
            //try
            //{
            //    string where = "SiteID = '" + siteid + "' and HasDocument = 1";

            //    return DataAccess.MicrosoftDB.GetDataTable(
            //        "UtilPaging"
            //        , CommandType.StoredProcedure
            //        , conString
            //        , new SqlParameter("datasrc", "[dbo].[VW_PropertyDisposalPendingUpload]")
            //        , new SqlParameter("orderBy", "PropertyDisposalID ASC")
            //        , new SqlParameter("fieldlist", "*")
            //        , new SqlParameter("filter", where)
            //        , new SqlParameter("pageNum", 0)
            //        , new SqlParameter("pageSize", 0)
            //        );
            //}
            //catch
            //{
            //    throw;
            //}
        }

        public static DataTable PDLPendingUpload(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                string where = "";
                where = "ReturnNo = '" + x.ReturnNumber + "'";

                return DataAccess.MicrosoftDB.GetDataTable(
                "UtilPaging"
                , CommandType.StoredProcedure
                , conString
                , new SqlParameter("datasrc", "[dbo].[VW_AllReturnDetailsHistory]")
                , new SqlParameter("orderBy", "ReturnNo asc")
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
    }
}