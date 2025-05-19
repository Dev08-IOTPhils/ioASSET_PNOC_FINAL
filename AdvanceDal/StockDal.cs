using AdvanceEntity;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace AdvanceDal
{
    public class StockDal
    {
        public int DeleteStock(StockEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_DeleteStock"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("StockID", x.StockID)
                    , new SqlParameter("User", x.CreatedBy)
                    );
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetStock(string ConnString)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string where = "";
                where = "IsDeleted = 'False'";

                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_Stock]")
                    , new SqlParameter("orderBy", "Active Desc, StockID DESC")
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

        //public int SaveStock(StockEntity x)
        //{
        //    string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
        //    try
        //    {
        //        return DataAccess.MicrosoftDB.ExecuteScalar<int>(
        //            "SP_SaveStock"                   , CommandType.StoredProcedure
        //            , conString
        //            , new SqlParameter("StockID", x.StockID)
        //            , new SqlParameter("ItemCode", x.ItemCode)
        //            , new SqlParameter("ClassificationID", x.ClassificationID)
        //            , new SqlParameter("CategoryID", x.CategoryID)
        //            , new SqlParameter("BrandID", x.BrandID)
        //            , new SqlParameter("ModelID", x.ModelID)
        //            , new SqlParameter("UnitOfMeasurementID", x.UnitOfMeasurementID)
        //            , new SqlParameter("Description", x.Description)
        //            , new SqlParameter("Active", x.Active)
        //            , new SqlParameter("IsReturnable", x.Returnable)
        //            , new SqlParameter("User", x.CreatedBy)
        //            , new SqlParameter("AssetInventoryAccount", x.AssetInventoryAccount)
        //            , new SqlParameter("IncomeAccount", x.IncomeAccount)
        //            , new SqlParameter("ExpenseAccount", x.ExpenseAccount)
        //            , new SqlParameter("StockImage", x.StockImage)
        //            , new SqlParameter("CriticalLevel", x.CriticalLevel)
        //            , new SqlParameter("TagTypeID", x.TagTypeID)
        //            , new SqlParameter("RFIDNumber", x.RFIDNumber)
        //            , new SqlParameter("Barcode", x.BarcodeNumber)
        //            );
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}
        public int SaveStock(StockEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_SaveStock", CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("StockID", x.StockID)
                    , new SqlParameter("ItemCode", x.ItemCode)
                    , new SqlParameter("ClassificationID", x.ClassificationID)
                    , new SqlParameter("CategoryID", x.CategoryID)
                    , new SqlParameter("BrandID", x.BrandID)
                    , new SqlParameter("ModelID", x.ModelID)
                    , new SqlParameter("UnitOfMeasurementID", x.UnitOfMeasurementID)
                    , new SqlParameter("Description", x.Description)
                    , new SqlParameter("Active", x.Active)
                    , new SqlParameter("IsReturnable", x.Returnable)
                    , new SqlParameter("User", x.CreatedBy)
                    //, new SqlParameter("AssetInventoryAccount", x.AssetInventoryAccount)
                    , new SqlParameter("IncomeAccount", x.IncomeAccount)
                    , new SqlParameter("ExpenseAccount", x.ExpenseAccount)
                    , new SqlParameter("StockImage", x.StockImage)
                    , new SqlParameter("CriticalLevel", x.CriticalLevel)
                    , new SqlParameter("TagTypeID", x.TagTypeID)
                    , new SqlParameter("RFIDNumber", x.RFIDNumber)
                    , new SqlParameter("Barcode", x.BarcodeNumber)
                    );
            }
            catch
            {
                throw;
            }
        }
        public DataTable StockByID(StockEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_RetrieveStock"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("StockID", x.StockID)
                    );
            }
            catch
            {
                throw;
            }
        }
        #region STOCK GOODS RECEIVE

        public int AddToStockList(StockEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_AddToStockList"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("StockID", x.StockID)
                    , new SqlParameter("SupplierID", x.SupplierID)
                    , new SqlParameter("DateOfAcquisition", x.DateOfAcquisition)
                    , new SqlParameter("PRNumber", x.PRNumber)
                    , new SqlParameter("ReceivedBy", x.ReceivedBy)
                    , new SqlParameter("PONumber", x.PONumber)
                    , new SqlParameter("CustodianID", x.CustodianID)
                    , new SqlParameter("InvoiceNumber", x.InvoiceNumber)
                    , new SqlParameter("ApproverID", x.ApproverID)
                    , new SqlParameter("DRNumber", x.DRNumber)
                    , new SqlParameter("Remarks", x.Remarks)
                    , new SqlParameter("Quantity", x.Quantity)
                    , new SqlParameter("AcquiredCost", x.AcquiredCost)
                    , new SqlParameter("User", x.CreatedBy)
                    , new SqlParameter("LocationID", x.LocationID)
                    , new SqlParameter("SiteID", x.SiteID)
                    , new SqlParameter("AssignedPerson", x.AssignedPerson)
                    );
            }
            catch
            {
                throw;
            }
        }

        public int ConfirmGoodsReceiveStock(StockEntity x, MiscEntity me)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_ConfirmGoodsReceiveStock"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("SupplierID", x.SupplierID)
                    , new SqlParameter("DateOfAcquisition", x.DateOfAcquisition)
                    , new SqlParameter("PRNumber", x.PRNumber)
                    , new SqlParameter("ReceivedBy", x.ReceivedBy)
                    , new SqlParameter("PONumber", x.PONumber)
                    , new SqlParameter("CustodianID", x.CustodianID)
                    , new SqlParameter("InvoiceNumber", x.InvoiceNumber)
                    , new SqlParameter("ApproverID", x.ApproverID)
                    , new SqlParameter("DRNumber", x.DRNumber)
                    , new SqlParameter("Remarks", x.Remarks)
                    , new SqlParameter("User", x.CreatedBy)
                    , new SqlParameter("CreationApproval", me.CreationApproval)
                    );
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetStockAddedList(StockEntity xy)
        {
            string conString = ConfigurationManager.ConnectionStrings[xy.ConnectionString].ToString();
            try
            {
                string where = "";
                where = "IsConfirmed = 'False' and CreatedBy = '" + xy.CreatedBy + "' and SupplierID = '" + xy.SupplierID + "' and DateOfAcquisition = '" + xy.DateOfAcquisition +
                    "' and PRNumber = '" + xy.PRNumber + "' and ReceivedBy = '" + xy.ReceivedBy + "' and PONumber = '" + xy.PONumber + "' and CustodianID = '" + xy.CustodianID +
                    "' and InvoiceNumber = '" + xy.InvoiceNumber + "' and ApproverID = '" + xy.ApproverID + "' and DRNumber = '" + xy.DRNumber + "' and Remarks = '" + xy.Remarks + "'";

                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_StockAddedList]")
                    , new SqlParameter("orderBy", "ItemCode ASC")
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

        public DataTable GetStockViewGoodsReceive(string search, string ConnString)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string where = "";
                if (search != "")
                {
                    where = "Active = 'True' and (ItemCode like '%" + search + "%' or Description  like '%" + search + "%' or ClassificationName like '%" + search + "%'  or CategoryName like '%" + search + "%' or BrandName like '%" + search + "%' or ModelName like '%" + search + "%')";
                }
                else
                {
                    where = "Active = 'True'";
                }

                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_Stock]")
                    , new SqlParameter("orderBy", "StockID DESC")
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
        public int RemoveAllToStockList(StockEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_RemoveAllToStockList"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("User", x.CreatedBy)
                    );
            }
            catch
            {
                throw;
            }
        }

        public int RemoveToStockList(StockEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_RemoveToStockList"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("GRStockID", x.GRStockID)
                    , new SqlParameter("User", x.CreatedBy)
                    );
            }
            catch
            {
                throw;
            }
        }
        #endregion STOCK GOODS RECEIVE
    }
}