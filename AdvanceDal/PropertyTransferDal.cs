using AdvanceEntity;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace AdvanceDal
{
    public class PropertyTransferDal
    {
        #region Property Transfer

        public int CancelPropertyTransfer(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_CancelPropertyTransfer"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("TransferNumber", x.TransferNumber)
                    , new SqlParameter("User", x.CreatedBy)
                    , new SqlParameter("Remarks", x.Remarks)
                    );
            }
            catch (Exception ex)
            {
                ex.ToString();
                throw;
            }
        }

        public string ConfirmPropertyTransfer(PropertyEntity propertyEntity)
        {
            string conString = ConfigurationManager.ConnectionStrings[propertyEntity.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<string>(
                    "SP_ConfirmPropertyTransfer"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("FromSite", propertyEntity.SiteID)
                    , new SqlParameter("FromLocation", propertyEntity.LocationID)
                    , new SqlParameter("FromArea", propertyEntity.AreaID)
                    , new SqlParameter("ToSite", propertyEntity.ToSiteID)
                    , new SqlParameter("ToLocation", propertyEntity.ToLocationID)
                    , new SqlParameter("ToArea", propertyEntity.ToAreaID)
                    , new SqlParameter("User", propertyEntity.CreatedBy)
                    , new SqlParameter("Purpose", propertyEntity.Remarks)
                    , new SqlParameter("ApproverID", propertyEntity.ApproverID)
                    , new SqlParameter("CustodianID", propertyEntity.CustodianID)

                    , new SqlParameter("FromEmployeeID", propertyEntity.FromEmployeeID)
                    , new SqlParameter("ToEmployeeID", propertyEntity.ToEmployeeID)
                    , new SqlParameter("TransactionType", propertyEntity.TransferType)
                    , new SqlParameter("PARICS", propertyEntity.ExpenseType)
                    );
            }
            catch (Exception ex)
            {
                ex.ToString();
                throw;
            }
        }

        public DataTable GetAvailablePropertyForTransfer(PropertyEntity x, string search, int custodianid, int ExpenseType)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_SearchAvailablePropertyForTransfer"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("CustodianID", custodianid)
                    , new SqlParameter("Search", search)
                    , new SqlParameter("SiteID", x.SiteID)
                    , new SqlParameter("LocationID", x.LocationID)
                    , new SqlParameter("AreaID", x.AreaID)
                    //, new SqlParameter("TransferType", x.AreaID)
                    , new SqlParameter("EmployeeID", x.EmployeeID)
                    , new SqlParameter("PARICS", ExpenseType)
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

        public DataTable GetToTransferAssetList(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                string dataSrc = "[dbo].[VW_PropertyForTransfer]";
                string where = "";

                x.SiteID = x.SiteID == 0 ? 0 : x.SiteID;
                x.LocationID = x.LocationID == 0 ? 0 : x.LocationID;
                x.AreaID = x.AreaID == 0 ? 0 : x.AreaID;
                x.ToSiteID = x.ToSiteID == 0 ? 0 : x.ToSiteID;
                x.ToLocationID = x.ToLocationID == 0 ? 0 : x.ToLocationID;
                x.ToAreaID = x.ToAreaID == 0 ? 0 : x.ToAreaID;
                x.CustodianID = x.CustodianID == 0 ? 0 : x.CustodianID;
                x.ExpenseType = x.ExpenseType == 0 ? 0 : x.ExpenseType;

                where = "FromSite = '" + x.SiteID + "' and FromLocation = '" + x.LocationID + "' and FromArea = '" + x.AreaID + "' and ToSite = '" + x.ToSiteID + "' and ToLocation = '" + x.ToLocationID + "' and ToArea = '" + x.ToAreaID + "' and CreatedBy = '" + x.CreatedBy + "' and IsTransferred = 'True' and CustodianID = '" + x.CustodianID + "' and FromEmployeeID = '" + x.FromEmployeeID + "' and ToEmployeeID = '" + x.ToEmployeeID + "' and TransactionType = '" + x.TransferType + "' and PARICS = '" + x.ExpenseType + "'";

                return DataAccess.MicrosoftDB.GetDataTable(
               "UtilPaging"
               , CommandType.StoredProcedure
               , conString
               , new SqlParameter("datasrc", dataSrc)
               , new SqlParameter("orderBy", "PropertyNumber ASC")
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

        public DataTable PLPendingApproval(string ConnString, string transferno)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string where = "";
                where = "TransferNumber = '" + transferno + "'";

                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_PropertyTransferListPendingApproval]")
                    , new SqlParameter("orderBy", "PropertyNumber asc")
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

        public DataTable PLPendingApprovalWithComponent(string ConnString, string transferno)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string where = "";
                where = "TransferNumber = '" + transferno + "'";

                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_PropertyTransferListPendingApprovalWithComponent]")
                    , new SqlParameter("orderBy", "PropertyNumber asc")
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

        public DataTable PropertyTransferGetCount(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_PropertyTransferGetCount"
                    , CommandType.StoredProcedure
                    , conString
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
                    , new SqlParameter("PARICS", x.ExpenseType)
                    );
            }
            catch
            {
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
                        where = "HasDocument = 'False' and SiteID = '" + siteid + "' and ([Status] = 'Pending Approval' or [Status] = 'Pending Verification' or [Status] = 'Pending Upload')";
                    }
                    else
                    {
                        where = "HasDocument = 'False' and SiteID = '" + siteid + "' and ([Status] = 'Pending Approval' or [Status] = 'Pending Verification' or [Status] = 'Pending Upload') and (TransferNumber like '%" + search + "%' or Remarks like '%" + search + "%')";
                    }
                }
                else
                {
                    if (search == "")
                    {
                        where = "HasDocument = 'False' and CustodianID in (" + custodianid + ") and SiteID = '" + siteid + "' and ([Status] = 'Pending Approval' or [Status] = 'Pending Verification' or [Status] = 'Pending Upload')";
                    }
                    else
                    {
                        where = "HasDocument = 'False' and CustodianID in (" + custodianid + ") and SiteID = '" + siteid + "' and ([Status] = 'Pending Approval' or [Status] = 'Pending Verification' or [Status] = 'Pending Upload') and (TransferNumber like '%" + search + "%' or Remarks like '%" + search + "%')";
                    }
                }
                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_PropertyTransferPendingUpload]")
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
        public int RemoveAllToTransfer(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_PropertyTransferRemoveAll"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("FromSite", x.SiteID)
                    , new SqlParameter("FromLocation", x.LocationID)
                    , new SqlParameter("FromArea", x.AreaID)
                    , new SqlParameter("ToSite", x.ToSiteID)
                    , new SqlParameter("ToLocation", x.ToLocationID)
                    , new SqlParameter("ToArea", x.ToAreaID)
                    , new SqlParameter("CreatedBy", x.CreatedBy)
                    , new SqlParameter("CustodianID", x.CustodianID)
                    , new SqlParameter("FromEmployeeID", x.FromEmployeeID)
                    , new SqlParameter("ToEmployeeID", x.ToEmployeeID)
                    , new SqlParameter("TransactionType", x.TransferType)
                    , new SqlParameter("PARICS", x.ExpenseType)
                    );
            }
            catch
            {
                throw;
            }
        }

        public int RemoveToTransfer(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_PropertyTransferRemove"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("PMLID", x.RecID)
                    , new SqlParameter("CreatedBy", x.CreatedBy)
                    , new SqlParameter("TransactionType", x.TransferType)
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
        public int UploadPropertyTransfer(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_UploadPropertyTransfer"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("TransferNumber", x.TransferNumber)
                    , new SqlParameter("User", x.CreatedBy)
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

        public DataTable PropertyTransferPendingApproval(string ConnString, string approver)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string where = "HasDocument = 'False' and ApproverUser = '" + approver + "' and [Status] = 'Pending Approval'";

                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_PropertyTransferPendingUpload]")
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

        public DataTable PropertyTransferPendingApproval(string ConnString)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string where = "HasDocument = 'False' and [Status] = 'Pending Approval'";

                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_PropertyTransferPendingUpload]")
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
        #endregion Request Management
    }
}