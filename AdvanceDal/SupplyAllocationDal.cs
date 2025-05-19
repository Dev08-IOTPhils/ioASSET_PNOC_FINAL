using AdvanceEntity;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace AdvanceDal
{
    public class SupplyAllocationDal
    {
        #region Property Transfer

        public int StockTransferToAllocation(StockEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_StockTransferToAllocation"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("SMLID", x.RecID)
                    , new SqlParameter("FromSiteID", x.FromSite)
                    , new SqlParameter("ToSiteID", x.ToSite)
                    , new SqlParameter("CustodianID", x.CustodianID)
                    , new SqlParameter("Quantity", x.Quantity)
                    , new SqlParameter("User", x.CreatedBy)
                    );
            }
            catch
            {
                throw;
            }
        }
        public DataTable GetAvailableSupplyForTransfer(StockEntity x, int site, string search, int custodianid)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {

                string where = "";
                where = "WarehouseID = '" + site + "' and CustodianID = '" + custodianid + "'";

                return DataAccess.MicrosoftDB.GetDataTable(
                "UtilPaging"
                , CommandType.StoredProcedure
                , conString
                , new SqlParameter("datasrc", "[dbo].[VW_AvailableStockForAllocation]")
                , new SqlParameter("orderBy", "RecID asc")
               , new SqlParameter("filter", where)
                , new SqlParameter("fieldlist", "*")
                , new SqlParameter("pageNum", 0)
                , new SqlParameter("pageSize", 0)
                );
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetAvailablePropertyForTransferNotIssued(PropertyEntity x, string search, int custodianid)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_SearchAvailablePropertyForTransferNotIssued"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("CustodianID", custodianid)
                    , new SqlParameter("Search", search)
                    , new SqlParameter("SiteID", x.SiteID)
                    , new SqlParameter("LocationID", x.LocationID)
                    , new SqlParameter("AreaID", x.AreaID)
                    );
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetToTransferAssetList(StockEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                string dataSrc = "[dbo].[VW_StockForAllocation]";
                string where = "";

                x.SiteID = x.SiteID == 0 ? 0 : x.SiteID;
                x.ToSite = x.ToSite == 0 ? 0 : x.ToSite;

                x.CustodianID = x.CustodianID == 0 ? 0 : x.CustodianID;

                where = "FromSiteID = '" + x.SiteID + "' and ToSiteID = '" + x.ToSite + "' and CreatedBy = '" + x.CreatedBy + "' and IsTransferred = 'True' and CustodianID = '" + x.CustodianID + "'";

                return DataAccess.MicrosoftDB.GetDataTable(
               "UtilPaging"
               , CommandType.StoredProcedure
               , conString
               , new SqlParameter("datasrc", dataSrc)
               , new SqlParameter("orderBy", "SMLID ASC")
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

        public int CancelPropertyTransfer(StockEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_CancelStockAllocation"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("StockAllocationNo", x.StockAllocationNo)
                    , new SqlParameter("User", x.CreatedBy)
                    , new SqlParameter("IPAddress", "")
                    , new SqlParameter("Remarks", x.Description)

                    
                    );
            }
            catch (Exception ex)
            {
                ex.ToString();
                throw;
            }
        }

        public DataTable PLPendingApproval(string ConnString, string transferno)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string where = "";
                where = "StockAllocationNo = '" + transferno + "'";

                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_StockForAllocation]")
                    , new SqlParameter("orderBy", "StockAllocationNo asc")
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

        public DataTable PLPendingUpload(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                string where = "";
                where = "PMLID = '" + x.RecID + "'";

                return DataAccess.MicrosoftDB.GetDataTable(
                "UtilPaging"
                , CommandType.StoredProcedure
                , conString
                , new SqlParameter("datasrc", "[dbo].[VW_PropertyTransferPerson]")
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

        public DataTable PropertyTransferDetails(string ConnString, string x)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_RetrievePropertyTransferDetails"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("TransferNo", x)
                    );
            }
            catch
            {
                throw;
            }
        }

        public DataTable PropertyTransferDetailsView(string ConnString, string x)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_RetrievePropertyTransferDetailsView"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("TransferNo", x)
                    );
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public DataTable SupplyTransferGetCount(StockEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_StockAllocationGetCount"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("FromSite", x.SiteID)
                    , new SqlParameter("ToSite", x.ToSite)
                    , new SqlParameter("User", x.CreatedBy)
                    , new SqlParameter("CustodianID", x.CustodianID)
                    );
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public string ConfirmPropertyTransfer(StockEntity stockEntity)
        {
            string conString = ConfigurationManager.ConnectionStrings[stockEntity.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<string>(
                    "SP_ConfirmStockAllocation"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("FromSiteID", stockEntity.SiteID)
                    , new SqlParameter("ToSiteID", stockEntity.ToSite)
                    , new SqlParameter("User", stockEntity.CreatedBy)
                    , new SqlParameter("Remarks", stockEntity.Remarks)
                    , new SqlParameter("ApproverID", stockEntity.ApproverID)
                    , new SqlParameter("CustodianID", stockEntity.CustodianID)
                    );
            }
            catch (Exception ex)
            {
                ex.ToString();
                throw;
            }
        }
        public DataTable PropertyTransferPendingApproval(string search, string ConnString, string custodianid, int siteid, string administrative)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            custodianid = custodianid.Replace(';', ',');
            try
            {
                string where = "";
                if (administrative == "True")
                {
                    if (search == "")
                    {
                        where = "([Status] = 'Pending Approval' or [Status] = 'Pending Verification' or [Status] = 'Pending Upload')";
                    }
                    else
                    {
                        where = "([Status] = 'Pending Approval' or [Status] = 'Pending Verification' or [Status] = 'Pending Upload') and (TransferNumber like '%" + search + "%' or Remarks like '%" + search + "%')";
                    }
                }
                else
                {
                    if (search == "")
                    {
                        where = "CustodianID in (" + custodianid + ") and ([Status] = 'Pending Approval' or [Status] = 'Pending Verification' or [Status] = 'Pending Upload')";
                    }
                    else
                    {
                        where = "CustodianID in (" + custodianid + ") and ([Status] = 'Pending Approval' or [Status] = 'Pending Verification' or [Status] = 'Pending Upload') and (TransferNumber like '%" + search + "%' or Remarks like '%" + search + "%')";
                    }
                }
                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"    
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_StockAllocationHeader]")
                    , new SqlParameter("orderBy", "StockAllocationID Desc")
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
        public int RemoveAllToTransfer(StockEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_StockRemoveAllToAllocation" 
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("FromSite", x.SiteID)
                    , new SqlParameter("ToSite", x.ToSite)
                    , new SqlParameter("CreatedBy", x.CreatedBy)
                    , new SqlParameter("CustodianID", x.CustodianID)
                    );
            }
            catch
            {
                throw;
            }
        }

        public int RemoveToTransfer(StockEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_StockRemoveToAllocation"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("SMLID", x.RecID)
                    , new SqlParameter("User", x.CreatedBy)
                    );
            }
            catch
            {
                throw;
            }
        }

        public int TransferToLocationTempTable(PropertyEntity x, int ExpenseType)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_PropertyTransferToList"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("PMLID", x.RecID)
                    , new SqlParameter("FromSite", x.SiteID)
                    , new SqlParameter("FromLocation", x.LocationID)
                    , new SqlParameter("FromArea", x.AreaID)
                    , new SqlParameter("ToSite", x.ToSiteID)
                    , new SqlParameter("ToLocation", x.ToLocationID)
                    , new SqlParameter("ToArea", x.ToAreaID)
                    , new SqlParameter("CreatedBy", x.CreatedBy)
                    , new SqlParameter("CustodianID", x.CustodianID)
                    , new SqlParameter("TransactionType", x.TransferType)
                    , new SqlParameter("FromEmployeeID", x.FromEmployeeID)
                    , new SqlParameter("ToEmployeeID", x.ToEmployeeID)
                    , new SqlParameter("PARICS", ExpenseType)
                    );
            }
            catch
            {
                throw;
            }
        }

        //
        //public int UploadSupplyAllocation(StockEntity x)
        //{
        //    string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
        //    try
        //    {
        //        return DataAccess.MicrosoftDB.ExecuteScalar<int>(
        //            "SP_UploadStockAllocation"
        //            , CommandType.StoredProcedure
        //            , conString
        //            , new SqlParameter("AllocationNo", x.StockAllocationNo)
        //            , new SqlParameter("User", x.CreatedBy)
        //            );
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}

        public DataTable UploadSupplyAllocation(string ConnString, string transactionno, string createdby)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_UploadStockAllocation"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("AllocationNo", transactionno)
                    , new SqlParameter("User", createdby)
                    );
            }
            catch
            {
                throw;
            }
        }
        #endregion Property Transfer

        #region Request Management

        public int ApprovePropertyTransfer(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_ApprovePropertyTransfer"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("TransferNo", x.TransferNumber)
                    , new SqlParameter("User", x.CreatedBy)
                    );
            }
            catch
            {
                throw;
            }
        }

        public int DeclinePropertyTransfer(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_DeclinePropertyTransfer"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("TransferNo", x.TransferNumber)
                    , new SqlParameter("Remarks", x.Remarks)
                    , new SqlParameter("User", x.CreatedBy)
                    );
            }
            catch
            {
                throw;
            }
        }

        //Request Management --------------------------------------------------------------------------------
        public DataTable GetSAPendingApproval(string connstring)
        {
            string conString = ConfigurationManager.ConnectionStrings[connstring].ToString();
            try
            {
                string where = "";
                where = "Status = 'Pending Approval'";
                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_StockAllocationHeader]")
                    , new SqlParameter("orderBy", "StockAllocationNo Desc")
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

        public DataTable GetSAPendingApproval(string approver, string connstring)
        {
            string conString = ConfigurationManager.ConnectionStrings[connstring].ToString();
            try
            {
                string where = "";
                where = "ApproverUser ='" + approver + "' and Status = 'Pending Approval'";
                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_StockAllocationHeader]")
                    , new SqlParameter("orderBy", "StockAllocationNo Desc")
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

        public DataTable SALPendingUpload(StockEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                string where = "";
                where = "StockAllocationNo = '" + x.DisposalNumber + "'";

                return DataAccess.MicrosoftDB.GetDataTable(
                "UtilPaging"
                , CommandType.StoredProcedure
                , conString
                , new SqlParameter("datasrc", "[dbo].[VW_StockForAllocation]")
                , new SqlParameter("orderBy", "StockAllocationNo asc")
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

        public int ApproveStockAllocation(StockEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_ApproveStockAllocation"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("StockAllocationNo", x.DisposalNumber)
                    , new SqlParameter("User", x.CreatedBy)
                    );
            }
            catch
            {
                throw;
            }
        }

        public int DeclineStockAllocation(StockEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_DeclineStockAllocation"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("StockAllocationNo", x.DisposalNumber)
                    , new SqlParameter("Remarks", x.Remarks)
                    , new SqlParameter("User", x.CreatedBy)
                    );
            }
            catch
            {
                throw;
            }
        }

        //STOCK ALLOCATION HISTORY ------------------------------------------------------------------------------------

        public DataTable StockAllocationHistory(string ConnString, int siteid, string custodianid, string search, string datefrom, string dateto, string Filter, string administrative)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            //try
            //{
            //    return DataAccess.MicrosoftDB.GetDataTable(
            //       "SP_SearchStockIssuanceHistory"
            //        , CommandType.StoredProcedure
            //        , conString
            //        , new SqlParameter("SiteID", siteid)
            //        , new SqlParameter("CustodianID", custodianid)
            //        , new SqlParameter("Search", search)
            //        , new SqlParameter("DateFrom", datefrom)
            //        , new SqlParameter("DateTo", dateto)
            //        );
            //}
            //catch
            //{
            //    throw;
            //}
            try
            {
                string where = "";
                custodianid = custodianid.Replace(';', ',');

                if (administrative == "True")
                {
                    if (search != "")
                    {
                        where = "(TransactorName like '%" + search + "%' or ApproverName like '%" + search + "%' or StockAllocationNo like '%" + search + "%') and CustodianID in (" + custodianid + ") and (CreatedDate between '" + datefrom + "' and DateAdd(day,1,'" + dateto + "')) and [Status] != 'Pending Approval' and Status != 'Pending Upload' and Status != 'Pending Quantity Issuance' " + Filter;
                    }
                    else
                    {
                        where = "(CreatedDate between '" + datefrom + "' and DateAdd(day,1,'" + dateto + "')) and [Status] != 'Pending Approval' and Status != 'Pending Upload' and Status != 'Pending Quantity Issuance' " + Filter;
                    }
                }
                else
                {
                    if (search != "")
                    {
                        where = "(TransactorName like '%" + search + "%' or ApproverName like '%" + search + "%' or StockAllocationNo like '%" + search + "%') and CustodianID in (" + custodianid + ") and (CreatedDate between '" + datefrom + "' and DateAdd(day,1,'" + dateto + "')) and [Status] != 'Pending Approval' and Status != 'Pending Upload' and Status != 'Pending Quantity Issuance' " + Filter;
                    }
                    else
                    {
                        where = "CustodianID in (" + custodianid + ") and (CreatedDate between '" + datefrom + "' and DateAdd(day,1,'" + dateto + "')) and [Status] != 'Pending Approval' and Status != 'Pending Upload' and Status != 'Pending Quantity Issuance' " + Filter;
                    }
                }



                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_StockAllocationHeader]")
                    , new SqlParameter("orderBy", "StockAllocationNo ASC")
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




        //public DataTable PropertyTransferPendingApproval(string ConnString, string approver)
        //{
        //    string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
        //    try
        //    {
        //        string where = "HasDocument = 'False' and ApproverUser = '" + approver + "' and [Status] = 'Pending Approval'";

        //        return DataAccess.MicrosoftDB.GetDataTable(
        //            "UtilPaging"
        //            , CommandType.StoredProcedure
        //            , conString
        //            , new SqlParameter("datasrc", "[dbo].[VW_PropertyTransferPendingUpload]")
        //            , new SqlParameter("orderBy", "RecID ASC")
        //            , new SqlParameter("fieldlist", "*")
        //            , new SqlParameter("filter", where)
        //            , new SqlParameter("pageNum", 0)
        //            , new SqlParameter("pageSize", 0)
        //            );
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}

        //public DataTable PropertyTransferPendingApproval(string ConnString)
        //{
        //    string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
        //    try
        //    {
        //        string where = "HasDocument = 'False' and [Status] = 'Pending Approval'";

        //        return DataAccess.MicrosoftDB.GetDataTable(
        //            "UtilPaging"
        //            , CommandType.StoredProcedure
        //            , conString
        //            , new SqlParameter("datasrc", "[dbo].[VW_PropertyTransferPendingUpload]")
        //            , new SqlParameter("orderBy", "RecID ASC")
        //            , new SqlParameter("fieldlist", "*")
        //            , new SqlParameter("filter", where)
        //            , new SqlParameter("pageNum", 0)
        //            , new SqlParameter("pageSize", 0)
        //            );
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}
        #endregion Request Management
    }
}