using AdvanceDal;
using AdvanceEntity;
using System;
using System.Data;

namespace AdvanceBll
{
    public class PropertyBll
    {
        private PropertyDal propertyDal = new PropertyDal();

        #region FILES DATA PROPERTY

        public int DeleteProperty(PropertyEntity x)
        {
            int returnVal = propertyDal.DeleteProperty(x);
            try
            {
                return returnVal;
            }
            catch
            {
                return returnVal;
            }
        }

        public DataTable GetProperty(string ConnString)
        {
            try
            {
                return propertyDal.GetProperty(ConnString);
            }
            catch
            {
                throw;
            }
        }

        public string PropertyByID(PropertyEntity x)
        {
            try
            {
                string result = "";

                DataTable dt = propertyDal.PropertyByID(x);

                result = dt.Rows[0]["PropertyID"].ToString() + "|" +
                         dt.Rows[0]["ItemCode"].ToString() + "|" +
                         dt.Rows[0]["Description"].ToString() + "|" +
                         dt.Rows[0]["ClassificationID"].ToString() + "|" +
                         dt.Rows[0]["CategoryID"].ToString() + "|" +
                         dt.Rows[0]["BrandID"].ToString() + "|" +
                         dt.Rows[0]["ModelID"].ToString() + "|" +
                         dt.Rows[0]["UnitOfMeasurementID"].ToString() + "|" +
                         dt.Rows[0]["AssetInventoryAccountID"].ToString() + "|" +
                         dt.Rows[0]["IncomeAccountID"].ToString() + "|" +
                         dt.Rows[0]["ExpenseAccountID"].ToString() + "|" +
                         dt.Rows[0]["PropertyImage"].ToString() + "|" +
                         dt.Rows[0]["Active"].ToString() + "|" +
                         dt.Rows[0]["MaintenanceThreshold"].ToString() + "|" +
                         dt.Rows[0]["TagID"].ToString() + "|" +
                         dt.Rows[0]["UsefulLife"].ToString() + "|" +
                         dt.Rows[0]["DepreciationMethod"].ToString();
                return result;
            }
            catch
            {
                throw;
            }
        }

        public int SaveProperty(PropertyEntity x)
        {
            int id = propertyDal.SaveProperty(x);
            try
            {
                return id;
            }
            catch
            {
                return id;
            }
        }
        #endregion FILES DATA PROPERTY

        #region PROPERTY GOODS RECEIVE

        public int AddToGroupPropertyList(PropertyEntity x)
        {
            int id = propertyDal.AddToGroupPropertyList(x);
            try
            {
                return id;
            }
            catch
            {
                return id;
            }
        }

        public int AddToPropertyList(PropertyEntity x)
        {
            int id = propertyDal.AddToPropertyList(x);
            try
            {
                return id;
            }
            catch
            {
                return id;
            }
        }

        public int CancelGoodsReceive(PropertyEntity x)
        {
            int id = propertyDal.CancelGoodsReceive(x);
            try
            {
                return id;
            }
            catch
            {
                return id;
            }
        }

        public int ConfirmGoodsReceive(PropertyEntity x, MiscEntity me)
        {
            int id = propertyDal.ConfirmGoodsReceive(x, me);
            try
            {
                return id;
            }
            catch
            {
                return id;
            }
        }

        public string GetCountGoodsReceive(PropertyEntity x)
        {
            try
            {
                string result = "";

                DataTable dt = propertyDal.GetCountGoodsReceive(x);
                result = dt.Rows[0]["Count"].ToString();
                return result;
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetGoodsReceiveDetails(string ConnString, int goodreceiveid)
        {
            try
            {
                return propertyDal.GetGoodsReceiveDetails(ConnString, goodreceiveid);
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetGoodsReceiveHeader(string search, string ConnString, string user)
        {
            try
            {
                return propertyDal.GetGoodsReceiveHeader(search, ConnString, user);
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetPropertyAddedList(PropertyEntity xy)
        {
            try
            {
                return propertyDal.GetPropertyAddedList(xy);
            }
            catch
            {
                throw;
            }
        }

        public DataTable getPropertyDetails(string ConnString, int grpropertyid, int propertyid)
        {
            try
            {
                return propertyDal.getPropertyDetails(ConnString, grpropertyid, propertyid);
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetPropertyViewGoodsReceive(string search, string ConnString, string administrative)
        {
            try
            {
                return propertyDal.GetPropertyViewGoodsReceive(search, ConnString, administrative);
            }
            catch
            {
                throw;
            }
        }

        public int RemoveRMGoodsReceive(PropertyEntity x)
        {
            int id = propertyDal.RemoveRMGoodsReceive(x);
            try
            {
                return id;
            }
            catch
            {
                return id;
            }
        }

        

        public string GoodsReceiveDetails(string ConnString, int grid)
        {
            try
            {
                string result = "";

                DataTable dt = propertyDal.GoodsReceiveDetails(ConnString, grid);
                result = dt.Rows[0]["GoodsReceiveNumber"].ToString() + "|" +
                    dt.Rows[0]["TransactorName"].ToString() + "|" +
                    dt.Rows[0]["TransactorEmail"].ToString() + "|" +
                    dt.Rows[0]["Approver"].ToString() + "|" +
                    dt.Rows[0]["ApproverEmail"].ToString() + "|" +
                    dt.Rows[0]["Custodian"].ToString() + "|" +
                    dt.Rows[0]["CreatedDate"].ToString() + "|" +
                    dt.Rows[0]["PONumber"].ToString() + "|" +
                    dt.Rows[0]["SupplierName"].ToString() + "|" +
                    dt.Rows[0]["DRNumber"].ToString() + "|" +
                    dt.Rows[0]["Receiver"].ToString() + "|" +
                    dt.Rows[0]["WarehouseName"].ToString() + "|" +
                    dt.Rows[0]["ApproverPosition"].ToString() + "|" +
                    dt.Rows[0]["ReceiverPosition"].ToString();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int RemoveAllToPropertyList(PropertyEntity x)
        {
            int id = propertyDal.RemoveAllToPropertyList(x);
            try
            {
                return id;
            }
            catch
            {
                return id;
            }
        }

        public int RemoveToPropertyList(PropertyEntity x)
        {
            int id = propertyDal.RemoveToPropertyList(x);
            try
            {
                return id;
            }
            catch
            {
                return id;
            }
        }

        public int RemoveToGoodsReceiveList(PropertyEntity x)
        {
            int id = propertyDal.RemoveToGoodsReceiveList(x);
            try
            {
                return id;
            }
            catch
            {
                return id;
            }
        }
        public int UploadDocumentsGR(PropertyEntity x)
        {
            int id = propertyDal.UploadDocumentsGR(x);
            try
            {
                return id;
            }
            catch
            {
                return id;
            }
        }
        #endregion PROPERTY GOODS RECEIVE

        #region Goods Receive For Approval

        public int ApproveGoodsReceive(PropertyEntity x)
        {
            int id = propertyDal.ApproveGoodsReceive(x);
            try
            {
                return id;
            }
            catch
            {
                return id;
            }
        }

        public int DeclineGoodsReceive(PropertyEntity x)
        {
            int id = propertyDal.DeclineGoodsReceive(x);
            try
            {
                return id;
            }
            catch
            {
                return id;
            }
        }

        public DataTable GetGoodsReceiveForApproval(string ConnString, string administrative, string user)
        {
            try
            {
                if (administrative == "True")
                {
                    return propertyDal.GetGoodsReceiveForApproval(ConnString);
                }
                else
                {
                    return propertyDal.GetGoodsReceiveForApproval(ConnString, user);
                }
            }
            catch
            {
                throw;
            }
        }
        #endregion Goods Receive For Approval

        #region Property Information

        public string GetPropertyInformation(PropertyEntity x)
        {
            try
            {
                string result = "";

                DataTable dt = propertyDal.GetPropertyInformation(x);

                string ED = dt.Rows[0]["ExpiryDate"].ToString();
                if (dt.Rows[0]["ExpiryDate"].ToString() != "")
                {
                    ED = Convert.ToDateTime(dt.Rows[0]["ExpiryDate"].ToString()).ToString("MM/dd/yyyy");
                }

                string WSD = dt.Rows[0]["WarrantyStartDate"].ToString();
                if (dt.Rows[0]["WarrantyStartDate"].ToString() != "")
                {
                    WSD = Convert.ToDateTime(dt.Rows[0]["WarrantyStartDate"].ToString()).ToString("MM/dd/yyyy");
                }

                string WED = dt.Rows[0]["WarrantyEndDate"].ToString();
                if (dt.Rows[0]["WarrantyEndDate"].ToString() != "")
                {
                    WED = Convert.ToDateTime(dt.Rows[0]["WarrantyEndDate"].ToString()).ToString("MM/dd/yyyy");
                }

                string ISD = dt.Rows[0]["InsuranceStartDate"].ToString();
                if (dt.Rows[0]["InsuranceStartDate"].ToString() != "")
                {
                    ISD = Convert.ToDateTime(dt.Rows[0]["InsuranceStartDate"].ToString()).ToString("MM/dd/yyyy");
                }

                string IED = dt.Rows[0]["InsuranceEndDate"].ToString();
                if (dt.Rows[0]["InsuranceEndDate"].ToString() != "")
                {
                    IED = Convert.ToDateTime(dt.Rows[0]["InsuranceEndDate"].ToString()).ToString("MM/dd/yyyy");
                }

                result = dt.Rows[0]["PropertyNumber"].ToString() + "|" +
                         dt.Rows[0]["ItemCode"].ToString() + "|" +
                         dt.Rows[0]["SerialNumber"].ToString() + "|" +
                         dt.Rows[0]["LabelToPrint"].ToString() + "|" +
                         dt.Rows[0]["LongDescription"].ToString() + "|" +
                         ED + "|" +
                         WSD + "|" +
                         WED + "|" +
                         ISD + "|" +
                         IED + "|" +
                         dt.Rows[0]["AcquiredCost"].ToString() + "|" +
                         dt.Rows[0]["CustodianID"].ToString() + "|" +
                         dt.Rows[0]["Status"].ToString();
                return result;
            }
            catch
            {
                throw;
            }
        }

        public int UpdatePropertyInformation(PropertyEntity x)
        {
            int id = propertyDal.UpdatePropertyInformation(x);
            try
            {
                return id;
            }
            catch
            {
                return id;
            }
        }

        #endregion Property Information

        #region Dashboard

        public int getCountGR(int custodianid, int siteid, string ConnString)
        {
            try
            {
                return propertyDal.getCountGR(custodianid, siteid, ConnString);
            }
            catch
            {
                throw;
            }
        }

        public int GetDisposalPropertyVerification(string custodianid, int siteid, string ConnString)
        {
            try
            {
                return propertyDal.GetDisposalPropertyVerification(ConnString, custodianid, siteid);
            }
            catch
            {
                throw;
            }
        }

        public int GetGRPropertyVerification(string custodianid, int siteid, string ConnString)
        {
            try
            {
                return propertyDal.GetGRPropertyVerification(ConnString, custodianid, siteid);
            }
            catch
            {
                throw;
            }
        }

        public int GetIssuancePropertyVerification(string custodianid, int siteid, string ConnString)
        {
            try
            {
                return propertyDal.GetIssuancePropertyVerification(ConnString, custodianid, siteid);
            }
            catch
            {
                throw;
            }
        }

        public int GetRequestPropertyVerification(string custodianid, int siteid, string ConnString)
        {
            try
            {
                return propertyDal.GetRequestPropertyVerification(ConnString, custodianid, siteid);
            }
            catch
            {
                throw;
            }
        }

        public int GetReturnPropertyVerification(string custodianid, int siteid, string ConnString)
        {
            try
            {
                return propertyDal.GetReturnPropertyVerification(ConnString, custodianid, siteid);
            }
            catch
            {
                throw;
            }
        }

        public int GetTransferPropertyVerification(string custodianid, int siteid, string ConnString)
        {
            try
            {
                return propertyDal.GetTransferPropertyVerification(ConnString, custodianid, siteid);
            }
            catch
            {
                throw;
            }
        }
        #endregion Dashboard

        #region Pending Approval

        public int GetDatebaseRestorePendingApproval(string administrative, string userid, int siteid, string ConnString)
        {
            try
            {
                return propertyDal.GetDatebaseRestorePendingApproval(administrative, userid, siteid, ConnString);
            }
            catch
            {
                throw;
            }
        }

        public int GetFileUploadPendingApproval(string administrative, string userid, int siteid, string ConnString)
        {
            try
            {
                return propertyDal.GetFileUploadPendingApproval(administrative, userid, siteid, ConnString);
            }
            catch
            {
                throw;
            }
        }

        public int GetGRPendingApproval(string administrative, string userid, int siteid, string ConnString)
        {
            try
            {
                return propertyDal.GetGRPendingApproval(administrative, userid, siteid, ConnString);
            }
            catch
            {
                throw;
            }
        }

        public int GetPropertyDayPassPendingApproval(string administrative, string userid, int siteid, string ConnString)
        {
            try
            {
                return propertyDal.GetPropertyDayPassPendingApproval(administrative, userid, siteid, ConnString);
            }
            catch
            {
                throw;
            }
        }

        public int GetPropertyDisposalPendingApproval(string administrative, string userid, int siteid, string ConnString)
        {
            try
            {
                return propertyDal.GetPropertyDisposalPendingApproval(administrative, userid, siteid, ConnString);
            }
            catch
            {
                throw;
            }
        }

        public int GetPropertyDisposalRequestPendingApproval(string administrative, string userid, int siteid, string ConnString)
        {
            try
            {
                return propertyDal.GetPropertyDisposalRequestPendingApproval(administrative, userid, siteid, ConnString);
            }
            catch
            {
                throw;
            }
        }

        public int GetPropertyGatePassPendingApproval(string administrative, string userid, int siteid, string ConnString)
        {
            try
            {
                return propertyDal.GetPropertyGatePassPendingApproval(administrative, userid, siteid, ConnString);
            }
            catch
            {
                throw;
            }
        }

        public int GetPropertyIssuancePendingApproval(string administrative, string userid, int siteid, string ConnString)
        {
            try
            {
                return propertyDal.GetPropertyIssuancePendingApproval(administrative, userid, siteid, ConnString);
            }
            catch
            {
                throw;
            }
        }

        public int GetPropertyRequestPendingApproval(string administrative, string userid, int siteid, string ConnString)
        {
            try
            {
                return propertyDal.GetPropertyRequestPendingApproval(administrative, userid, siteid, ConnString);
            }
            catch
            {
                throw;
            }
        }

        public int GetPropertyReturnDateExtensionPendingApproval(string administrative, string userid, int siteid, string ConnString)
        {
            try
            {
                return propertyDal.GetPropertyReturnDateExtensionPendingApproval(administrative, userid, siteid, ConnString);
            }
            catch
            {
                throw;
            }
        }

        public int GetPropertyTransferPendingApproval(string administrative, string userid, int siteid, string ConnString)
        {
            try
            {
                return propertyDal.GetPropertyTransferPendingApproval(administrative, userid, siteid, ConnString);
            }
            catch
            {
                throw;
            }
        }
        public int GetStockDisposalPendingApproval(string administrative, string userid, int siteid, string ConnString)
        {
            try
            {
                return propertyDal.GetStockDisposalPendingApproval(administrative, userid, siteid, ConnString);
            }
            catch
            {
                throw;
            }
        }

        public int GetStockIssuancePendingApproval(string administrative, string userid, int siteid, string ConnString)
        {
            try
            {
                return propertyDal.GetStockIssuancePendingApproval(administrative, userid, siteid, ConnString);
            }
            catch
            {
                throw;
            }
        }

        public int GetStockMergePendingApproval(string administrative, string userid, int siteid, string ConnString)
        {
            try
            {
                return propertyDal.GetStockMergePendingApproval(administrative, userid, siteid, ConnString);
            }
            catch
            {
                throw;
            }
        }
        public int GetStockRequestPendingApproval(string administrative, string userid, int siteid, string ConnString)
        {
            try
            {
                return propertyDal.GetStockRequestPendingApproval(administrative, userid, siteid, ConnString);
            }
            catch
            {
                throw;
            }
        }
        #endregion Pending Approval

        #region InventoryCount

        public int getCountFound(string inventorycode, string ConnString)
        {
            try
            {
                return propertyDal.getCountFound(inventorycode, ConnString);
            }
            catch
            {
                throw;
            }
        }

        public int getCountMisplaced(string inventorycode, string ConnString)
        {
            try
            {
                return propertyDal.getCountMisplaced(inventorycode, ConnString);
            }
            catch
            {
                throw;
            }
        }

        public int getCountMissing(string inventorycode, string ConnString)
        {
            try
            {
                return propertyDal.getCountMissing(inventorycode, ConnString);
            }
            catch
            {
                throw;
            }
        }
        public string getCountAcquiredCost(string inventorycode, string ConnString)
        {
            try
            {
                return propertyDal.getAcquiredCost(inventorycode, ConnString);
            }
            catch
            {
                throw;
            }
        }
        public string getCountAcquiredCostFound(string inventorycode, string ConnString)
        {
            try
            {
                return propertyDal.getAcquiredCostFound(inventorycode, ConnString);
            }
            catch
            {
                throw;
            }
        }
        public string getCountAcquiredCostNotFound(string inventorycode, string ConnString)
        {
            try
            {
                return propertyDal.getAcquiredCostNotFound(inventorycode, ConnString);
            }
            catch
            {
                throw;
            }
        }
        public string getCountAcquiredCostMisplaced(string inventorycode, string ConnString)
        {
            try
            {
                return propertyDal.getAcquiredCostMisplaced(inventorycode, ConnString);
            }
            catch
            {
                throw;
            }
        }
        //added by nero GetPropertyAcknowledgementCount
        public int getOpenInventoryCount(string custodian, string ConnString, string admin)
        {
            try
            {
                return propertyDal.getOpenInventoryCount(custodian, ConnString, admin);
            }
            catch
            {
                throw;
            }
        }

        public int GetPropertyAcknowledgementCount(int UserID, string ConnString)
        {
            try
            {
                return propertyDal.GetPropertyAcknowledgementCount(UserID, ConnString);
            }
            catch
            {
                throw;
            }
        }

        #endregion InventoryCount

        #region Property Approval

        public int ApprovePropertyRetagging(PropertyEntity x)
        {
            int id = propertyDal.ApprovePropertyRetagging(x);
            try
            {
                return id;
            }
            catch
            {
                return id;
            }
        }

        public int DeclinePropertyRetagging(PropertyEntity x)
        {
            int id = propertyDal.DeclinePropertyRetagging(x);
            try
            {
                return id;
            }
            catch
            {
                return id;
            }
        }

        public DataTable GetPropertyRetaggingApprovalDetails(string ConnString, string trxNumber)
        {
            try
            {
                return propertyDal.GetPropertyRetaggingApprovalDetails(ConnString, trxNumber);
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetPropertyRetaggingHeader(string ConnString, string administrative, string user)
        {
            try
            {
                return propertyDal.GetPropertyRetaggingHeader(ConnString, administrative, user);
            }
            catch
            {
                throw;
            }
        }
        #endregion Property Approval

        public DataTable GetITemList(string ConnString, string search)
        {
            try
            {
                return propertyDal.GetITemList(ConnString, search);
            }
            catch
            {
                throw;
            }
        }

        //
        public DataTable GetITemListGSISForm(string ConnString, string SiteID, string PropertyCostType, bool floater)
        {
            try
            {
                return propertyDal.GetITemListGSISForm(ConnString, SiteID, PropertyCostType, floater);
            }
            catch
            {
                throw;
            }
        }
    }
}