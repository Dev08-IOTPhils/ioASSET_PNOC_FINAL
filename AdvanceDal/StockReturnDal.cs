using AdvanceEntity;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace AdvanceDal
{
    public class StockReturnDal
    {
        public int CancelStockReturn(StockEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_CancelStockReturn"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("ReturnNo", x.ReturnNo)
                    , new SqlParameter("User", x.CreatedBy)
                    );
            }
            catch (Exception ex)
            {
                ex.ToString();
                throw;
            }
        }

        public int ConfirmStockReturn(StockEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_ConfirmStockReturn"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("EmployeeID", x.EmployeeID)
                    , new SqlParameter("Remarks", x.Remarks)
                    , new SqlParameter("User", x.CreatedBy)
                    );
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetReturnStockList(string ConnString, int accountable, string createdby)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string where = "EmployeeID = '" + accountable + "' and IsTransferred = 1 and CreatedBy = '" + createdby + "'";

                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_StockForReturn]")
                    , new SqlParameter("orderBy", "RecordID ASC")
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

        public DataTable GetStockAvailableForReturn(string search, string ConnString, int accountable)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string where = "";
                if (search != "")
                {
                    where = "EmployeeID = '" + accountable + "' and (ItemCode like '%" + search + "%' or Description like '%" + search + "%')";
                }
                else
                {
                    where = "EmployeeID = '" + accountable + "'";
                }
                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_AvailableStockForReturn]")
                    , new SqlParameter("orderBy", "RecID ASC")
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
        public int RemoveAllToStockReturn(StockEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_StockRemoveAllToReturn"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("EmployeeID", x.EmployeeID)
                    , new SqlParameter("User", x.CreatedBy)
                    );
            }
            catch
            {
                throw;
            }
        }

        public int RemoveToStockReturn(StockEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_StockRemoveToReturn"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("RecID", x.RecID)
                    , new SqlParameter("User", x.CreatedBy)
                    );
            }
            catch (Exception ex)
            {
                ex.ToString();
                throw;
            }
        }

        public DataTable StockDetailsPendingUpload(string ConnString, string returnno)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string where = "";
                where = "ReturnNo = '" + returnno + "'";

                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_StockReturnDetailsPendingUpload]")
                    , new SqlParameter("orderBy", "RecordID asc")
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

        public DataTable StockReturnPendingUpload(string search, string ConnString, string createdby)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string where = "";
                if (search == "")
                {
                    where = "CreatedBy = '" + createdby + "' and IsUploaded = 0 and [Status] = 'Pending Upload'";
                }
                else
                {
                    where = "CreatedBy = '" + createdby + "' and IsUploaded = 0 and [Status] = 'Pending Upload' and (ReturnNo like '%" + search + "%' or Remarks like '%" + search + "%')";
                }
                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_StockReturnPendingUpload]")
                    , new SqlParameter("orderBy", "RecID Desc")
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
        public int TransferToReturn(StockEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_StockTransferToReturn"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("RecID", x.RecID)
                    , new SqlParameter("EmployeeID", x.EmployeeID)
                    , new SqlParameter("Quantity", x.Quantity)
                    , new SqlParameter("User", x.CreatedBy)
                    );
            }
            catch (Exception ex)
            {
                ex.ToString();
                throw;
            }
        }
        public string UploadStockReturn(string ConnString, string returnno, string createdby)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<string>(
                    "SP_UploadStockReturn"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("ReturnNo", returnno)
                    , new SqlParameter("User", createdby)
                    );
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public DataTable UploadStockReturnDT(string ConnString, string returnno, string createdby)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_UploadStockReturn"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("ReturnNo", returnno)
                    , new SqlParameter("User", createdby)
                    );
            }
            catch
            {
                throw;
            }
        }
    }
}