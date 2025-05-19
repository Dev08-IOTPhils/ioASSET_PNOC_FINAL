using AdvanceEntity;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace AdvanceDal
{
    public class PropertyTaggingDal
    {
        #region Property Tagging

        public int AddToPrint(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_TransferToPrintList"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("TagTypeID", x.TagTypeID)
                    , new SqlParameter("AssetID", x.RecID)
                    , new SqlParameter("DeviceID", x.DeviceID)
                    , new SqlParameter("User", x.CreatedBy)
                    , new SqlParameter("AssetType", x.AssetType)
                    , new SqlParameter("Quantity", x.Quantity)
                    , new SqlParameter("TaggingType", x.TaggingType)
                    );
            }
            catch
            {
                throw;
            }
        }

        public DataTable AvailableForTaggingPerGR(string ConnString, int tagtype)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string where = "";
                where = "TagID ='" + tagtype + "'";

                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_PropertyTagging]")
                    , new SqlParameter("orderBy", "GRNumber asc")
                    , new SqlParameter("fieldlist", "DISTINCT GRNumber,[STATUS],TagType,TagID,WarehouseID,PRNumber,PONumber,DRnumber,InvoiceNumber,Remarks")
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

        public int ConfirmPrint(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                if (x.TaskID == 1)
                {
                    return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_ConfirmPrintAsset"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("User", x.CreatedBy)
                    , new SqlParameter("TagType", x.TagTypeID)
                    , new SqlParameter("DeviceID", x.DeviceID)
                    , new SqlParameter("ApproverID", x.ApproverID)
                    , new SqlParameter("Remarks", x.Remarks)
                    , new SqlParameter("TaggingType", x.TaggingType)
                    );
                }
                else
                {
                    return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_ConfirmPrintAsset"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("User", x.CreatedBy)
                    , new SqlParameter("TagType", x.TagTypeID)
                    , new SqlParameter("DeviceID", x.DeviceID)
                    , new SqlParameter("TaggingType", x.TaggingType)
                    );
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public DataTable GetCount(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_PropertyTaggingGetCount"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("TagType", x.TagTypeID)
                    , new SqlParameter("DeviceID", x.DeviceID)
                    , new SqlParameter("User", x.CreatedBy)
                    , new SqlParameter("TaggingType", x.TaggingType)
                    );
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetPrintJobs(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                string where = "";

                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_PrintAllPending]")
                    , new SqlParameter("orderBy", "PrintJobID asc")
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

        public DataTable GetPrintList(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                string where = "";
                where = "TagType = '" + x.TagTypeID + "' and [User] = '" + x.CreatedBy + "' and DeviceID = '" + x.DeviceID + "'";

                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_PrintListAll]")
                    , new SqlParameter("orderBy", "RecID asc")
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

        public DataTable PropertyAvailableForTagging(string ConnString, int tagtype)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string where = "";
                where = "TagID ='" + tagtype + "'";

                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_PropertyTagging]")
                    , new SqlParameter("orderBy", "RecID asc")
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

        public int RemoveAllToPrint(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_RemoveAllToPrintList"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("User", x.CreatedBy)
                    , new SqlParameter("DeviceID", x.DeviceID)
                    , new SqlParameter("TagType", x.TagTypeID)
                    , new SqlParameter("TaggingType", x.TaggingType)
                    );
            }
            catch
            {
                throw;
            }
        }

        public int RemoveToPrint(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_RemoveToPrintList"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("AssetID", x.RecID)
                    , new SqlParameter("User", x.CreatedBy)
                    , new SqlParameter("AssetType", x.AssetType)
                    , new SqlParameter("TaggingType", x.TaggingType)
                    );
            }
            catch
            {
                throw;
            }
        }

        public int TagProperty(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_TagProperty"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("User", x.CreatedBy)
                    , new SqlParameter("RecID", x.RecID)
                    , new SqlParameter("DeviceID", x.DeviceID)
                    );
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public DataTable ViewPropertyTaggingPerGR(string ConnString, int tagtype, string GRNumber)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string where = "";
                where = "TagID ='" + tagtype + "' and GRNumber = '" + GRNumber + "'";

                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_PropertyTagging]")
                    , new SqlParameter("orderBy", "RecID asc")
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
        public int WriteTags(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_TagAsset"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("User", x.CreatedBy)
                    , new SqlParameter("TagType", x.TagTypeID)
                    );
            }
            catch
            {
                throw;
            }
        }
        #endregion Property Tagging

        #region stock tagging

        public int AddToPrintStock(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_TransferToPrintListStock"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("RecID", x.RecID)
                    , new SqlParameter("User", x.CreatedBy)
                    );
            }
            catch
            {
                throw;
            }
        }

        public int ConfirmPrintStock(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_ConfirmPrintStock"
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

        public DataTable GetCountStock(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_StockTaggingGetCount"
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

        public int RemoveAllToPrintStock(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_RemoveAllToPrintListStock"
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

        public int RemoveToPrintStock(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_RemoveToPrintListStock"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("RecID", x.RecID)
                    , new SqlParameter("User", x.CreatedBy)
                    );
            }
            catch
            {
                throw;
            }
        }

        public DataTable StockAvailableForRetagging(int tagtype, string ConnString)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string where = "";
                where = "TagID = '" + tagtype + "'";

                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_StockRetagging]")
                    , new SqlParameter("orderBy", "StockID asc")
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

        public DataTable StockAvailableForTagging(int tagtype, string ConnString)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string where = "";
                where = "TagID = '" + tagtype + "'";

                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_StockTagging]")
                    , new SqlParameter("orderBy", "StockID asc")
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
        public int WriteTagsStock(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_TagAssetStock"
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
        #endregion stock tagging

        #region retagging

        public DataTable AvailableForRetaggingPerGR(string ConnString, int tagtype)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string where = "";
                where = "TagID ='" + tagtype + "'";

                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_PropertyRetagging]")
                    , new SqlParameter("orderBy", "GRNumber asc")
                    , new SqlParameter("fieldlist", "DISTINCT GRNumber,[STATUS],TagType,TagID,WarehouseID,PRNumber,PONumber,DRnumber,InvoiceNumber,Remarks ")
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

        public DataTable PropertyAvailableForRetagging(string ConnString, int tagtype)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string where = "";
                where = "TagID ='" + tagtype + "'";

                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_PropertyRetagging]")
                    , new SqlParameter("orderBy", "RecID asc")
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

        //
        public DataTable ViewPropertyForRetaggingPerGR(string ConnString, int tagtype, string GRNumber)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string where = "";
                where = "TagID ='" + tagtype + "'  and GRNumber = '" + GRNumber + "'";

                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_PropertyRetagging]")
                    , new SqlParameter("orderBy", "RecID asc")
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
        #endregion retagging
    }
}