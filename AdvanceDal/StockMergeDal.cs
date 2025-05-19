using AdvanceEntity;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace AdvanceDal
{
    public class StockMergeDal
    {
        public int CancelStockMerge(StockEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_CancelStockMerge"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("StockMergeNo", x.StockMergeNo)
                    , new SqlParameter("User", x.CreatedBy)
                    );
            }
            catch (Exception ex)
            {
                ex.ToString();
                throw;
            }
        }

        public DataTable GetAvailableStockForMerge(string ConnString, string search, string custodianid, int siteid)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string where = "";
                if (search != "")
                {
                    where = "(ItemCode like '%" + search + "%' or Description like '%" + search + "%' or ClassificationName like '%" + search + "%' or CategoryName like '%" + search + "%' or BrandName like '%" + search + "%' or ModelName like '%" + search + "%') and WarehouseID = '" + siteid + "' and IsTransferred = 0 and CustodianID in (" + custodianid + ")";
                }
                else
                {
                    where = "WarehouseID = '" + siteid + "' and IsTransferred = 0 and CustodianID in (" + custodianid + ")";
                }

                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_AvailableStockForMerge]")
                    , new SqlParameter("orderBy", "RecID asc")
                    , new SqlParameter("fieldlist", "*")
                    , new SqlParameter("filter", where)
                    , new SqlParameter("pageNum", 0)
                    , new SqlParameter("pageSize", 0)
                    );
            }
            catch (Exception ex)
            {
                throw;
            }
            //string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            //try
            //{
            //    return DataAccess.MicrosoftDB.GetDataTable(
            //        "SP_SearchAvailableStockForMerge"
            //        , CommandType.StoredProcedure
            //        , conString
            //        , new SqlParameter("CustodianID", custodianid)
            //        , new SqlParameter("SiteID", siteid)
            //        , new SqlParameter("Search", search)
            //        );
            //}
            //catch
            //{
            //    throw;
            //}
        }

        public DataTable GetPendingApprovalStockMerge(string search, string ConnString, string createdby)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string where = "";
                if (search == "")
                {
                    where = "CreatedBy = '" + createdby + "' and IsDone = '0'";
                }
                else
                {
                    where = "CreatedBy = '" + createdby + "' and IsDone = '0' and (StockMergeNo like '%" + search + "%' or FromIC  like '%" + search + "%' or ToIC  like '%" + search + "%' or Remarks  like '%" + search + "%')";
                }

                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_StockMergePendingUpload]")
                    , new SqlParameter("orderBy", "StockMergeID Desc")
                    , new SqlParameter("fieldlist", "*")
                    , new SqlParameter("filter", where)
                    , new SqlParameter("pageNum", 0)
                    , new SqlParameter("pageSize", 0)
                    );
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public DataTable GetStockItemCode(string ConnString, string itemcode, int custodian, int site)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_RetrieveStockItemCodeToMerge"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("ItemCode", itemcode)
                    , new SqlParameter("CustodianID", custodian)
                    , new SqlParameter("SiteID", site)
                    );
            }
            catch
            {
                throw;
            }
        }

        public string MergeStock(StockEntity stockEntity)
        {
            string conString = ConfigurationManager.ConnectionStrings[stockEntity.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<string>(
                    "SP_ConfirmStockMerge"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("ItemCode", stockEntity.ItemCode)
                    , new SqlParameter("MergeToStockID", stockEntity.StockID)
                    , new SqlParameter("ApproverID", stockEntity.ApproverID)
                    , new SqlParameter("Remarks", stockEntity.Remarks)
                    , new SqlParameter("User", stockEntity.CreatedBy)
                    );
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #region request management

        public int ApproveStockMerge(StockEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_ApproveStockMerge"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("StockMergeNo", x.StockMergeNo)
                    , new SqlParameter("User", x.CreatedBy)
                    );
            }
            catch (Exception ex)
            {
                ex.ToString();
                throw;
            }
        }

        public int DeclineStockMerge(StockEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_DeclineStockMerge"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("StockMergeNo", x.StockMergeNo)
                    , new SqlParameter("User", x.CreatedBy)
                    );
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public DataTable GetPendingApprovalStockMergeRM(string ConnString, int approver)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string where = "";
                where = "ApproverID = '" + approver + "' and IsApproved = '0'";

                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_StockMergePendingUpload]")
                    , new SqlParameter("orderBy", "StockMergeID ASC")
                    , new SqlParameter("fieldlist", "*")
                    , new SqlParameter("filter", where)
                    , new SqlParameter("pageNum", 0)
                    , new SqlParameter("pageSize", 0)
                    );
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public DataTable GetPendingApprovalStockMergeRM(string ConnString)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string where = "";
                where = "IsApproved = '0'";

                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_StockMergePendingUpload]")
                    , new SqlParameter("orderBy", "StockMergeID ASC")
                    , new SqlParameter("fieldlist", "*")
                    , new SqlParameter("filter", where)
                    , new SqlParameter("pageNum", 0)
                    , new SqlParameter("pageSize", 0)
                    );
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion request management

        #region Stock Merge History

        public DataTable StockMergeHistory(string ConnString, int siteid, string search, string datefrom, string dateto, string Filter)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();

            string filter = " SiteID = " + siteid + " and (CreatedDate between '" + datefrom + "' and DateAdd(day, 1, '" + dateto + "'))  and [Status] = 'Approved'  " + Filter;

            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_SearchStockMergeHistory"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("Filter", filter)
                    //, new SqlParameter("Search", search)
                    //, new SqlParameter("DateFrom", datefrom)
                    //, new SqlParameter("DateTo", dateto)
                    //, new SqlParameter("SiteID", siteid)
                    );
            }
            catch (Exception e)
            {
                throw;
            }
        }

        #endregion Stock Merge History
    }
}