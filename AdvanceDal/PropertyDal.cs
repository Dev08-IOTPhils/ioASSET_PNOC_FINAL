using AdvanceEntity;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace AdvanceDal
{
    public class PropertyDal
    {
        #region FILES DATA PROPERTY

        public int DeleteProperty(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_DeleteProperty"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("PropertyID", x.PropertyID)
                    , new SqlParameter("User", x.CreatedBy)
                    );
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetProperty(string ConnString)
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
                    , new SqlParameter("datasrc", "[dbo].[VW_Property]")
                    , new SqlParameter("orderBy", "Active Desc, PropertyID DESC")
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

        //public int SaveProperty(PropertyEntity x)
        //{
        //    string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
        //    try
        //    {
        //        return DataAccess.MicrosoftDB.ExecuteScalar<int>(
        //            "SP_SaveProperty"
        //            , CommandType.StoredProcedure
        //            , conString
        //            , new SqlParameter("PropertyID", x.PropertyID)
        //            , new SqlParameter("ItemCode", x.ItemCode)
        //            , new SqlParameter("ClassificationID", x.ClassificationID)
        //            , new SqlParameter("CategoryID", x.CategoryID)
        //            , new SqlParameter("BrandID", x.BrandID)
        //            , new SqlParameter("ModelID", x.ModelID)
        //            , new SqlParameter("UnitOfMeasurementID", x.UnitOfMeasurementID)
        //            , new SqlParameter("AssetInventoryAccount", x.AssetInventoryAccount)
        //            , new SqlParameter("IncomeAccount", x.IncomeAccount)
        //            , new SqlParameter("ExpenseAccount", x.ExpenseAccount)
        //            , new SqlParameter("PropertyImage", x.PropertyImage)
        //            , new SqlParameter("Description", x.Description)
        //            , new SqlParameter("MaintenanceThreshold", x.MaintenanceThreshold)
        //            , new SqlParameter("TagID", x.TagID)
        //            , new SqlParameter("UsefulLife", x.UsefulLife)
        //            , new SqlParameter("DepreciationMethod", x.DepreciationMethod)
        //            , new SqlParameter("Active", x.Active)
        //            , new SqlParameter("User", x.CreatedBy)
        //            );
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}
        public DataTable PropertyByID(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_RetrieveProperty"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("PropertyID", x.PropertyID)
                    );
            }
            catch
            {
                throw;
            }
        }

        public int SaveProperty(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_SaveProperty"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("PropertyID", x.PropertyID)
                    , new SqlParameter("ItemCode", x.ItemCode)
                    , new SqlParameter("ClassificationID", x.ClassificationID)
                    , new SqlParameter("CategoryID", x.CategoryID)
                    , new SqlParameter("BrandID", x.BrandID)
                    , new SqlParameter("ModelID", x.ModelID)
                    , new SqlParameter("UnitOfMeasurementID", x.UnitOfMeasurementID)
                    //, new SqlParameter("AssetInventoryAccount", x.AssetInventoryAccount)
                    , new SqlParameter("IncomeAccount", x.IncomeAccount)
                    , new SqlParameter("ExpenseAccount", x.ExpenseAccount)
                    , new SqlParameter("PropertyImage", x.PropertyImage)
                    , new SqlParameter("Description", x.Description)
                    , new SqlParameter("MaintenanceThreshold", x.MaintenanceThreshold)
                    , new SqlParameter("TagID", x.TagID)
                    , new SqlParameter("UsefulLife", x.UsefulLife)
                    , new SqlParameter("DepreciationMethod", x.DepreciationMethod)
                    , new SqlParameter("Active", x.Active)
                    , new SqlParameter("User", x.CreatedBy)
                    );
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion FILES DATA PROPERTY

        #region PROPERTY GOODS RECEIVE

        public int AddToGroupPropertyList(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_AddToGroupPropertyList"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("PropertyID", x.PropertyID)
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
                    , new SqlParameter("SalePrice", x.SalePrice)
                    , new SqlParameter("User", x.CreatedBy)

                    , new SqlParameter("SiteID", x.SiteID)
                    , new SqlParameter("LocationID", x.LocationID)
                    , new SqlParameter("AssignedPerson", x.AssignedPerson)
                    );
            }
            catch
            {
                throw;
            }
        }

        public int AddToPropertyList(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_AddToPropertyList"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("GRPropertyID", x.GRPropertyID)
                    , new SqlParameter("PropertyID", x.PropertyID)
                    , new SqlParameter("PropertyNumber", x.PropertyNumber)
                    , new SqlParameter("SerialNo", x.SerialNo)
                    , new SqlParameter("LongDescription", x.LongDescription)
                    , new SqlParameter("WarrantyStartDate", x.WarrantyStartDate)
                    , new SqlParameter("WarrantyEndDate", x.WarrantyEndDate)
                    , new SqlParameter("InsuranceStartDate", x.InsuranceStartDate)
                    , new SqlParameter("InsuranceEndDate", x.InsuranceEndDate)
                    , new SqlParameter("ExpiryDate", x.ExpiryDate)
                    , new SqlParameter("LabelToPrint", x.LabelToPrint)
                    , new SqlParameter("RFIDNumber", x.RFIDNumber)
                    , new SqlParameter("Barcode", x.BarcodeNumber)
                    , new SqlParameter("AcquiredCost", x.AcquiredCost)
                    , new SqlParameter("User", x.CreatedBy)
                    );
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public int CancelGoodsReceive(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_CancelGoodsReceive"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("GoodsReceiveID", x.GoodsReceiveID)
                    , new SqlParameter("Remarks", x.Remarks)
                    , new SqlParameter("User", x.CreatedBy)
                    );
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public int ConfirmGoodsReceive(PropertyEntity x, MiscEntity me)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_ConfirmGoodsReceive"
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
                    , new SqlParameter("SiteID", x.SiteID)
                    , new SqlParameter("LocationID", x.LocationID)
                    , new SqlParameter("AssignedPerson", x.AssignedPerson)
                    );
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetCountGoodsReceive(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_GoodsReceiveGetCount"
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
                    , new SqlParameter("SiteID", x.SiteID)
                    , new SqlParameter("LocationID", x.LocationID)
                    , new SqlParameter("AssignedPerson", x.AssignedPerson)
                    );
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetGoodsReceiveDetails(string ConnString, int goodsreceiveid)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string where = "";
                where = "GoodsReceiveID = '" + goodsreceiveid + "'";

                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_GoodsReceiveDetailsCombine]")
                    , new SqlParameter("orderBy", "GoodsReceiveID ASC")
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

        public DataTable GetGoodsReceiveHeader(string search, string ConnString, string user)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string where = "";
                if (search != "")
                {
                    where = "CreatedBy = '" + user + "' and HasDocument = 'false' and (GoodsReceiveNumber like '%" + search + "%' or Receiver  like '%" + search + "%' or Status  like '%" + search + "%')";
                }
                else
                {
                    where = "CreatedBy = '" + user + "' and HasDocument = 'false'";
                }
                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_GoodsReceiveHeader]")
                    , new SqlParameter("orderBy", "GoodsReceiveID DESC")
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

        public DataTable GetPropertyAddedList(PropertyEntity xy)
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
                    , new SqlParameter("datasrc", "[dbo].[VW_CombineAddedToList]")
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

        public DataTable getPropertyDetails(string ConnString, int grpropertyid, int propertyid)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string where = "";
                where = "GRPropertyID = '" + grpropertyid + "' and PropertyID = '" + propertyid + "'";
                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_GRPropertyDetails]")
                    , new SqlParameter("orderBy", "DetailsID ASC")
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

        public DataTable GetPropertyViewGoodsReceive(string search, string ConnString, string administrative)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string where = "";

                if (administrative == "true")
                {
                    if (search != "")
                    {
                        where = "Active = 'True' and (ItemCode like '%" + search + "%' or Description  like '%" + search + "%' or ClassificationName like '%" + search + "%'  or CategoryName like '%" + search + "%' or BrandName like '%" + search + "%' or ModelName like '%" + search + "%')";
                    }
                    else
                    {
                        where = "Active = 'True'";
                    }
                }
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
                    , new SqlParameter("datasrc", "[dbo].[VW_Property]")
                    , new SqlParameter("orderBy", "PropertyID DESC")
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
        public DataTable GoodsReceiveDetails(string ConnString, int grid)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_RetrieveGoodsReceiveDetails"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("GoodsReceiveID", grid)
                    );
            }
            catch
            {
                throw;
            }
        }

        public int RemoveAllToPropertyList(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_RemoveAllToPropertyList"
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

        public int RemoveToPropertyList(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_RemoveToPropertyList"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("GRPropertyID", x.GRPropertyID)
                    , new SqlParameter("User", x.CreatedBy)
                    );
            }
            catch
            {
                throw;
            }
        }

        public int RemoveToGoodsReceiveList(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_RemoveToPropertyListConfirmed"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("GRPropertyID", x.GRPropertyID)
                    , new SqlParameter("GoodsReceiveNumber", x.GoodsReceiveNumber)
                    , new SqlParameter("DetailsID", x.DetailsID)
                    , new SqlParameter("User", x.CreatedBy)
                    );


            }
            catch
            {
                throw;
            }
        }

        public int RemoveRMGoodsReceive(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_RemoveToPropertyListConfirmed"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("GRPropertyID", x.GRPropertyID)
                    , new SqlParameter("GoodsReceiveNumber", x.GoodsReceiveNumber)
                    , new SqlParameter("DetailsID", x.DetailsID)
                    , new SqlParameter("User", x.CreatedBy)
                    );


            }
            catch
            {
                throw;
            }
        }


        public int UploadDocumentsGR(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_UploadDocumentsGR"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("GoodsReceiveID", x.GoodsReceiveID)
                    , new SqlParameter("User", x.CreatedBy)
                    );
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion PROPERTY GOODS RECEIVE

        #region Goods Receive For Approval

        public int ApproveGoodsReceive(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_ApproveGoodsReceive"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("GoodsReceiveID", x.GoodsReceiveID)
                    , new SqlParameter("User", x.CreatedBy)
                    );
            }
            catch
            {
                throw;
            }
        }

        public int DeclineGoodsReceive(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_DeclineGoodsReceive"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("GoodsReceiveID", x.GoodsReceiveID)
                    , new SqlParameter("Remarks", x.Remarks)
                    , new SqlParameter("User", x.CreatedBy)
                    );
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetGoodsReceiveForApproval(string ConnString)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string where = "";
                where = "IsApproved = 'false' and Status = 'Pending Approval'";

                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_GoodsReceiveHeader]")
                    , new SqlParameter("orderBy", "GoodsReceiveID ASC")
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

        public DataTable GetGoodsReceiveForApproval(string ConnString, string user)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string where = "";
                where = "ApproverUser = '" + user + "' and IsApproved = 'false' and Status = 'Pending Approval'";

                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_GoodsReceiveHeader]")
                    , new SqlParameter("orderBy", "GoodsReceiveID ASC")
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
        #endregion Goods Receive For Approval

        #region Property Information

        public DataTable GetPropertyInformation(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_RetrievePropertyInformation"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("PropertyNumber", x.PropertyNumber)
                    );
            }
            catch
            {
                throw;
            }
        }

        public int UpdatePropertyInformation(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_UpdatePropertyInformation"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("PropertyNumber", x.PropertyNumber)
                    , new SqlParameter("SerialNo", x.SerialNo)
                    , new SqlParameter("LabelToPrint", x.LabelToPrint)
                    , new SqlParameter("LongDescription", x.LongDescription)
                    , new SqlParameter("WarrantyStartDate", x.WarrantyStartDate)
                    , new SqlParameter("WarrantyEndDate", x.WarrantyEndDate)
                    , new SqlParameter("InsuranceStartDate", x.InsuranceStartDate)
                    , new SqlParameter("InsuranceEndDate", x.InsuranceEndDate)
                    , new SqlParameter("ExpiryDate", x.ExpiryDate)
                    , new SqlParameter("User", x.CreatedBy)
                    , new SqlParameter("NewPropertyNumber", x.NewPropertyNumber)
                    , new SqlParameter("AcquiredCost", x.AcquiredCost)
                    , new SqlParameter("CustodianID", x.CustodianID)
                    );
            }
            catch (Exception e)
            {
                throw;
            }
        }

        #endregion Property Information

        #region DashBoard

        public int getCountGR(int custodianid, int siteid, string ConnString)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string query = "SELECT COUNT([GoodsReceiveID]) AS 'grcount' FROM [dbo].[tbl_GoodsReceive] where HasDocument = 0";
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    query
                    , CommandType.Text
                    , conString
                    );
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public int GetDisposalPropertyVerification(string ConnString, string custodianid, int siteid)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            if (custodianid == "")
            {
                return 0;
            }
            else
            {
                custodianid = custodianid.Replace(';', ',');
                try
                {
                    string query = "SELECT COUNT([RecID]) AS 'disposecount' FROM [dbo].[VW_PropertyForVerification] where WarehouseID = '" + siteid + "' and CustodianID in (" + custodianid + ") and IsVerified = 6";
                    return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                        query
                        , CommandType.Text
                        , conString
                        );
                }
                catch (Exception e)
                {
                    throw;
                }
            }
        }

        public int GetGRPropertyVerification(string ConnString, string custodianid, int siteid)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            if (custodianid == "")
            {
                return 0;
            }
            else
            {
                custodianid = custodianid.Replace(';', ',');
                try
                {
                    string query = "SELECT COUNT([RecID]) AS 'grcount' FROM [dbo].[VW_PropertyForVerification] where WarehouseID = '" + siteid + "' and CustodianID in (" + custodianid + ") and IsVerified = 1";
                    return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                        query
                        , CommandType.Text
                        , conString
                        );
                }
                catch (Exception e)
                {
                    throw;
                }
            }
        }

        public int GetIssuancePropertyVerification(string ConnString, string custodianid, int siteid)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            if (custodianid == "")
            {
                return 0;
            }
            else
            {
                custodianid = custodianid.Replace(';', ',');
                try
                {
                    string query = "SELECT COUNT([RecID]) AS 'issuancecount' FROM [dbo].[VW_PropertyForVerification] where WarehouseID = '" + siteid + "' and CustodianID in (" + custodianid + ") and IsVerified = 2";
                    return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                        query
                        , CommandType.Text
                        , conString
                        );
                }
                catch (Exception e)
                {
                    throw;
                }
            }
        }

        public int GetRequestPropertyVerification(string ConnString, string custodianid, int siteid)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            if (custodianid == "")
            {
                return 0;
            }
            else
            {
                custodianid = custodianid.Replace(';', ',');
                try
                {
                    string query = "SELECT COUNT([RecID]) AS 'requestcount' FROM [dbo].[VW_PropertyForVerification] where WarehouseID = '" + siteid + "' and CustodianID in (" + custodianid + ") and IsVerified = 4";
                    return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                        query
                        , CommandType.Text
                        , conString
                        );
                }
                catch (Exception e)
                {
                    throw;
                }
            }
        }

        public int GetReturnPropertyVerification(string ConnString, string custodianid, int siteid)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            if (custodianid == "")
            {
                return 0;
            }
            else
            {
                custodianid = custodianid.Replace(';', ',');
                try
                {
                    string query = "SELECT COUNT([RecID]) AS 'returncount' FROM [dbo].[VW_PropertyForVerification] where WarehouseID = '" + siteid + "' and CustodianID in (" + custodianid + ") and IsVerified = 5 and [Status] != 'On Maintenance'";
                    return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                        query
                        , CommandType.Text
                        , conString
                        );
                }
                catch (Exception e)
                {
                    throw;
                }
            }
        }

        public int GetTransferPropertyVerification(string ConnString, string custodianid, int siteid)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            if (custodianid == "")
            {
                return 0;
            }
            else
            {
                custodianid = custodianid.Replace(';', ',');
                try
                {
                    string query = "SELECT COUNT([RecID]) AS 'transfercount' FROM [dbo].[VW_PropertyForVerification] where WarehouseID = '" + siteid + "' and CustodianID in (" + custodianid + ") and IsVerified = 3";
                    return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                        query
                        , CommandType.Text
                        , conString
                        );
                }
                catch (Exception e)
                {
                    throw;
                }
            }
        }
        #endregion DashBoard

        #region Pending Approval

        public int GetDatebaseRestorePendingApproval(string administrative, string userid, int siteid, string ConnString)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            //custodianid = custodianid.Replace(';', ',');
            try
            {
                if (administrative == "True")
                {
                    string query = "SELECT COUNT([TransactionNumber]) AS 'databaserestorecount' FROM [dbo].[VW_PendingApprovalTransaction] where TransactionType = 'Database Restore'";
                    return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                        query
                        , CommandType.Text
                        , conString
                        );
                }
                else
                {
                    string query = "SELECT COUNT([TransactionNumber]) AS 'databaserestorecount' FROM [dbo].[VW_PendingApprovalTransaction] where TransactionType = 'Database Restore' and ApproverID =" + userid + "";
                    return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                        query
                        , CommandType.Text
                        , conString
                        );
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public int GetFileUploadPendingApproval(string administrative, string userid, int siteid, string ConnString)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            //custodianid = custodianid.Replace(';', ',');
            try
            {
                if (administrative == "True")
                {
                    string query = "SELECT COUNT([TransactionNumber]) AS 'fileuploadcount' FROM [dbo].[VW_PendingApprovalTransaction] where TransactionType = 'File Upload'";
                    return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                        query
                        , CommandType.Text
                        , conString
                        );
                }
                else
                {
                    string query = "SELECT COUNT([TransactionNumber]) AS 'fileuploadcount' FROM [dbo].[VW_PendingApprovalTransaction] where TransactionType = 'File Upload' and ApproverID =" + userid + "";
                    return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                        query
                        , CommandType.Text
                        , conString
                        );
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public int GetGRPendingApproval(string administrative, string userid, int siteid, string ConnString)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            //custodianid = custodianid.Replace(';', ',');
            try
            {
                if (administrative == "True")
                {
                    string query = "SELECT COUNT([TransactionNumber]) AS 'grcount' FROM [dbo].[VW_PendingApprovalTransaction] where TransactionType = 'Goods Receive'";
                    return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                        query
                        , CommandType.Text
                        , conString
                        );
                }
                else
                {
                    string query = "SELECT COUNT([TransactionNumber]) AS 'grcount' FROM [dbo].[VW_PendingApprovalTransaction] where TransactionType = 'Goods Receive' and ApproverID = " + userid + "";
                    return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                        query
                        , CommandType.Text
                        , conString
                        );
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public int GetPropertyDayPassPendingApproval(string administrative, string userid, int siteid, string ConnString)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            //custodianid = custodianid.Replace(';', ',');
            try
            {
                if (administrative == "True")
                {
                    string query = "SELECT COUNT([TransactionNumber]) AS 'daypasscount' FROM [dbo].[VW_PendingApprovalTransaction] where TransactionType = 'Day Pass'";
                    return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                        query
                        , CommandType.Text
                        , conString
                        );
                }
                else
                {
                    string query = "SELECT COUNT([TransactionNumber]) AS 'daypasscount' FROM [dbo].[VW_PendingApprovalTransaction] where TransactionType = 'Day Pass' and ApproverID = " + userid + "";
                    return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                        query
                        , CommandType.Text
                        , conString
                        );
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public int GetPropertyDisposalPendingApproval(string administrative, string userid, int siteid, string ConnString)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            //custodianid = custodianid.Replace(';', ',');
            try
            {
                if (administrative == "True")
                {
                    string query = "SELECT COUNT([TransactionNumber]) AS 'disposalcount' FROM [dbo].[VW_PendingApprovalTransaction] where TransactionType = 'Property Disposal'";
                    return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                        query
                        , CommandType.Text
                        , conString
                        );
                }
                else
                {
                    string query = "SELECT COUNT([TransactionNumber]) AS 'disposalcount' FROM [dbo].[VW_PendingApprovalTransaction] where TransactionType = 'Property Disposal' and ApproverID = " + userid + "";
                    return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                        query
                        , CommandType.Text
                        , conString
                        );
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public int GetPropertyDisposalRequestPendingApproval(string administrative, string userid, int siteid, string ConnString)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            //custodianid = custodianid.Replace(';', ',');
            try
            {
                if (administrative == "True")
                {
                    string query = "SELECT COUNT([TransactionNumber]) AS 'disposalcount' FROM [dbo].[VW_PendingApprovalTransaction] where TransactionType = 'Property Disposal Request'";
                    return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                        query
                        , CommandType.Text
                        , conString
                        );
                }
                else
                {
                    string query = "SELECT COUNT([TransactionNumber]) AS 'disposalcount' FROM [dbo].[VW_PendingApprovalTransaction] where TransactionType = 'Property Disposal Request' and ApproverID = " + userid + "";
                    return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                        query
                        , CommandType.Text
                        , conString
                        );
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public int GetPropertyGatePassPendingApproval(string administrative, string userid, int siteid, string ConnString)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            //custodianid = custodianid.Replace(';', ',');
            try
            {
                if (administrative == "True")
                {
                    string query = "SELECT COUNT([TransactionNumber]) AS 'gatepasscount' FROM [dbo].[VW_PendingApprovalTransaction] where TransactionType = 'Gate Pass'";
                    return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                        query
                        , CommandType.Text
                        , conString
                        );
                }
                else
                {
                    string query = "SELECT COUNT([TransactionNumber]) AS 'gatepasscount' FROM [dbo].[VW_PendingApprovalTransaction] where TransactionType = 'Gate Pass' and ApproverID = " + userid + "";
                    return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                        query
                        , CommandType.Text
                        , conString
                        );
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public int GetPropertyIssuancePendingApproval(string administrative, string userid, int siteid, string ConnString)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            //custodianid = custodianid.Replace(';', ',');
            try
            {
                if (administrative == "True")
                {
                    string query = "SELECT COUNT([TransactionNumber]) AS 'issuancecount' FROM [dbo].[VW_PendingApprovalTransaction] where TransactionType = 'Property Issuance'";
                    return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                        query
                        , CommandType.Text
                        , conString
                        );
                }
                else
                {
                    string query = "SELECT COUNT([TransactionNumber]) AS 'issuancecount' FROM [dbo].[VW_PendingApprovalTransaction] where TransactionType = 'Property Issuance' and ApproverID = " + userid + "";
                    return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                        query
                        , CommandType.Text
                        , conString
                        );
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public int GetPropertyRequestPendingApproval(string administrative, string userid, int siteid, string ConnString)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            //custodianid = custodianid.Replace(';', ',');
            try
            {
                if (administrative == "True")
                {
                    string query = "SELECT COUNT([TransactionNumber]) AS 'issuancecount' FROM [dbo].[VW_PendingApprovalTransaction] where TransactionType = 'Property Request'";
                    return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                        query
                        , CommandType.Text
                        , conString
                        );
                }
                else
                {
                    string query = "SELECT COUNT([TransactionNumber]) AS 'issuancecount' FROM [dbo].[VW_PendingApprovalTransaction] where TransactionType = 'Property Request' and ApproverID = " + userid + "";
                    return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                        query
                        , CommandType.Text
                        , conString
                        );
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public int GetPropertyReturnDateExtensionPendingApproval(string administrative, string userid, int siteid, string ConnString)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            //custodianid = custodianid.Replace(';', ',');
            try
            {
                if (administrative == "True")
                {
                    string query = "SELECT COUNT([TransactionNumber]) AS 'returnextensioncount' FROM [dbo].[VW_PendingApprovalTransaction] where TransactionType = 'Return Date Extension'";
                    return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                        query
                        , CommandType.Text
                        , conString
                        );
                }
                else
                {
                    string query = "SELECT COUNT([TransactionNumber]) AS 'returnextensioncount' FROM [dbo].[VW_PendingApprovalTransaction] where TransactionType = 'Return Date Extension' and ApproverID = " + userid + "";
                    return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    query
                    , CommandType.Text
                    , conString
                    );
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public int GetPropertyTransferPendingApproval(string administrative, string userid, int siteid, string ConnString)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            //custodianid = custodianid.Replace(';', ',');
            try
            {
                if (administrative == "True")
                {
                    string query = "SELECT COUNT([TransactionNumber]) AS 'transferount' FROM [dbo].[VW_PendingApprovalTransaction] where TransactionType = 'Property Transfer'";
                    return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                        query
                        , CommandType.Text
                        , conString
                        );
                }
                else
                {
                    string query = "SELECT COUNT([TransactionNumber]) AS 'transferount' FROM [dbo].[VW_PendingApprovalTransaction] where TransactionType = 'Property Transfer' and ApproverID = " + userid + "";
                    return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                        query
                        , CommandType.Text
                        , conString
                        );
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }
        public int GetStockDisposalPendingApproval(string administrative, string userid, int siteid, string ConnString)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            //custodianid = custodianid.Replace(';', ',');
            try
            {
                if (administrative == "True")
                {
                    string query = "SELECT COUNT([TransactionNumber]) AS 'issuancecount' FROM [dbo].[VW_PendingApprovalTransaction] where TransactionType = 'Stock Disposal'";
                    return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                        query
                        , CommandType.Text
                        , conString
                        );
                }
                else
                {
                    string query = "SELECT COUNT([TransactionNumber]) AS 'issuancecount' FROM [dbo].[VW_PendingApprovalTransaction] where TransactionType = 'Stock Disposal' and ApproverID = " + userid + "";
                    return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                        query
                        , CommandType.Text
                        , conString
                        );
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public int GetStockIssuancePendingApproval(string administrative, string userid, int siteid, string ConnString)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            //custodianid = custodianid.Replace(';', ',');
            try
            {
                if (administrative == "True")
                {
                    string query = "SELECT COUNT([TransactionNumber]) AS 'issuancecount' FROM [dbo].[VW_PendingApprovalTransaction] where TransactionType = 'Stock Issuance'";
                    return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                        query
                        , CommandType.Text
                        , conString
                        );
                }
                else
                {
                    string query = "SELECT COUNT([TransactionNumber]) AS 'issuancecount' FROM [dbo].[VW_PendingApprovalTransaction] where TransactionType = 'Stock Issuance' and ApproverID = " + userid + "";
                    return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                        query
                        , CommandType.Text
                        , conString
                        );
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public int GetStockMergePendingApproval(string administrative, string userid, int siteid, string ConnString)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            //custodianid = custodianid.Replace(';', ',');
            try
            {
                if (administrative == "True")
                {
                    string query = "SELECT COUNT([TransactionNumber]) AS 'stockmergecount' FROM [dbo].[VW_PendingApprovalTransaction] where TransactionType = 'Stock Merge'";
                    return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                        query
                        , CommandType.Text
                        , conString
                        );
                }
                else
                {
                    string query = "SELECT COUNT([TransactionNumber]) AS 'stockmergecount' FROM [dbo].[VW_PendingApprovalTransaction] where TransactionType = 'Stock Merge' and ApproverID = " + userid + "";
                    return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                        query
                        , CommandType.Text
                        , conString
                        );
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }
        public int GetStockRequestPendingApproval(string administrative, string userid, int siteid, string ConnString)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            //custodianid = custodianid.Replace(';', ',');
            try
            {
                if (administrative == "True")
                {
                    string query = "SELECT COUNT([TransactionNumber]) AS 'issuancecount' FROM [dbo].[VW_PendingApprovalTransaction] where TransactionType = 'Stock Request'";
                    return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                        query
                        , CommandType.Text
                        , conString
                        );
                }
                else
                {
                    string query = "SELECT COUNT([TransactionNumber]) AS 'issuancecount' FROM [dbo].[VW_PendingApprovalTransaction] where TransactionType = 'Stock Request' and ApproverID = " + userid + "";
                    return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                        query
                        , CommandType.Text
                        , conString
                        );
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }
        #endregion Pending Approval

        #region Property Retagging

        public int ApprovePropertyRetagging(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_ApproveRetagging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("TransactionNumber", x.TransactionNumber)
                    , new SqlParameter("User", x.CreatedBy)
                    );
            }
            catch
            {
                throw;
            }
        }

        public int DeclinePropertyRetagging(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_DeclineRetagging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("TransactionNumber", x.TransactionNumber)
                    , new SqlParameter("User", x.CreatedBy)
                    );
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetPropertyRetaggingApprovalDetails(string ConnString, string trxNumber)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string where = "";
                where = "TransactionNumber = '" + trxNumber + "'";

                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_RetaggingPendingApprovalList]")
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

        public DataTable GetPropertyRetaggingHeader(string ConnString, string administrative, string user)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string where = "";
                if (administrative == "True")
                {
                    where = "IsApproved = 'false'";
                }
                else
                {
                    where = "IsApproved = 'false' and Status = 'Pending Approval'";
                }

                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_RetaggingHeader]")
                    , new SqlParameter("orderBy", "RetaggingID ASC")
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
        #endregion Property Retagging

        //GetITemList
        public DataTable GetITemList(string ConnString, string search)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string query = "SELECT * FROM [dbo].[VW_IssuedItem] where EmployeeID = '" + search + "'";
                return DataAccess.MicrosoftDB.GetDataTable(
                    query
                    , CommandType.Text
                    , conString
                    );
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public DataTable GetITemListGSISForm(string ConnString, string SiteID, string costType, bool floater)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string query = "";
                if (floater == true)
                {
                    query = "SELECT * FROM [dbo].[VW_GSIS_Forms] where WarehouseID = '" + SiteID + "'  and IsFloater = '" + floater + "'";
                }
                else
                {
                    query = "SELECT * FROM [dbo].[VW_GSIS_Forms] where WarehouseID = '" + SiteID + "' and PARICS = '" + costType + "' and IsFloater = '" + floater + "'";
                }
                //string query = "SELECT * FROM [dbo].[VW_GSIS_Forms]";
                return DataAccess.MicrosoftDB.GetDataTable(
                    query
                    , CommandType.Text
                    , conString
                    );

            }
            catch (Exception e)
            {
                throw;
            }
        }

        #region inventory count

        public int getCountFound(string inventorycode, string ConnString)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string query = "SELECT COUNT([RecID]) FROM [dbo].[VW_InventoryPropertyDetails] where Status = '1' and InventoryCode = '" + inventorycode + "'";
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    query
                    , CommandType.Text
                    , conString
                    );
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public int getCountMisplaced(string inventorycode, string ConnString)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string query = "SELECT COUNT([RecID])  FROM [dbo].[VW_InventoryPropertyDetails] where Status = '2'  and InventoryCode = '" + inventorycode + "'";
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    query
                    , CommandType.Text
                    , conString
                    );
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public int getCountMissing(string inventorycode, string ConnString)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string query = "SELECT COUNT([RecID])  FROM [dbo].[VW_InventoryPropertyDetails] where Status = '0'  and InventoryCode = '" + inventorycode + "'";
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    query
                    , CommandType.Text
                    , conString
                    );
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public string getAcquiredCost(string inventorycode, string ConnString)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string query = "SELECT SUM([AcquiredCost]) FROM [dbo].[VW_InventoryPropertyDetails] where InventoryCode = '" + inventorycode + "'";
                //string query = "SELECT COUNT([RecID])  FROM [dbo].[VW_InventoryPropertyDetails] where Status = '0'  and InventoryCode = '" + inventorycode + "'";
                return DataAccess.MicrosoftDB.ExecuteScalar<decimal>(
                    query
                    , CommandType.Text
                    , conString
                    ).ToString();
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public string getAcquiredCostFound(string inventorycode, string ConnString)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string query = "SELECT SUM([AcquiredCost]) FROM [dbo].[VW_InventoryPropertyDetails] where InventoryCode = '" + inventorycode + "'  AND Status=1 ";
                //string query = "SELECT COUNT([RecID])  FROM [dbo].[VW_InventoryPropertyDetails] where Status = '0'  and InventoryCode = '" + inventorycode + "'";
                return DataAccess.MicrosoftDB.ExecuteScalar<decimal>(
                    query
                    , CommandType.Text
                    , conString
                    ).ToString();
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public string getAcquiredCostNotFound(string inventorycode, string ConnString)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string query = "SELECT SUM([AcquiredCost]) FROM [dbo].[VW_InventoryPropertyDetails] where InventoryCode = '" + inventorycode + "'  AND Status=0 ";
                //string query = "SELECT COUNT([RecID])  FROM [dbo].[VW_InventoryPropertyDetails] where Status = '0'  and InventoryCode = '" + inventorycode + "'";
                return DataAccess.MicrosoftDB.ExecuteScalar<decimal>(
                    query
                    , CommandType.Text
                    , conString
                    ).ToString();
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public string getAcquiredCostMisplaced(string inventorycode, string ConnString)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string query = "SELECT SUM([AcquiredCost]) FROM [dbo].[VW_InventoryPropertyDetails] where InventoryCode = '" + inventorycode + "' AND (Status != 1 AND Status != 0) ";
                //string query = "SELECT COUNT([RecID])  FROM [dbo].[VW_InventoryPropertyDetails] where Status = '0'  and InventoryCode = '" + inventorycode + "'";
                return DataAccess.MicrosoftDB.ExecuteScalar<decimal>(
                    query
                    , CommandType.Text
                    , conString
                    ).ToString();
            }
            catch (Exception e)
            {
                throw;
            }
        }
        //added by nero GetPropertyAcknowledgementCount
        public int getOpenInventoryCount(string Custodian, string ConnString, string admin)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            if (admin == "True")
            {
                try
                {
                    string query = "Select count(*) From VW_InventoryHeader where IsDone = 0";
                    return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                        query
                        , CommandType.Text
                        , conString
                        );
                }
                catch (Exception e)
                {
                    throw;
                }
            }
            else
            {
                if (Custodian == "")
                {
                    return 0;
                }
                else
                {
                    Custodian = Custodian.Replace(';', ',');
                    string input = Custodian;
                    string[] parts = input.Split(','); // Split the string by commas
                    Custodian = "'" + string.Join("','", parts) + "'";

                    try
                    {
                        string query = "Select count(*) From VW_InventoryHeader where IsDone = 0 and Custodian in (SELECT Custodian FROM tbl_Custodian where CustodianID in (" + Custodian + "))";
                        return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                            query
                            , CommandType.Text
                            , conString
                            );
                    }
                    catch (Exception e)
                    {
                        throw;
                    }
                }
            }
        }

        public int GetPropertyAcknowledgementCount(int UserID, string ConnString)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string query = "Select count(*) From VW_PropertyForAcknowledgement where EmployeeID = '" + UserID + "'";
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    query
                    , CommandType.Text
                    , conString
                    );
            }
            catch (Exception e)
            {
                throw;
            }
        }

        #endregion inventory count
    }
}