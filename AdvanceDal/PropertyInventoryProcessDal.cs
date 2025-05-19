using AdvanceEntity;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace AdvanceDal
{
    public class PropertyInventoryProcessDal
    {
        #region Inventory Count Process

        public string addPropertyInventoryToList(string ConnString, string inventorycode, string propertyno, int expensetype)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<string>(
                    "SP_PropertyInventoryAddToList"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("InventoryCode", inventorycode)
                    , new SqlParameter("PropertyNumber", propertyno)
                    , new SqlParameter("ExpenseType", expensetype)
                    );
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// /////////////////
        ///
        public DataTable GetInventoryConsolidated(string ConnString, int site)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string where = "";
                where = "WarehouseID = '" + site + "'";
                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_InventoryPropertyDetails]")
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

        public DataTable GetInventoryList(string ConnString, string custodianid, string administrative)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();


            custodianid = custodianid.Replace(';', ',');
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_SearchForInventory"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("CustodianID", custodianid)
                    , new SqlParameter("Administrative", administrative)
                    );
            }
            catch (Exception ex)
            {
                throw;
            }
            //string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            //custodianid = custodianid.Replace(';', ',');
            //string where = "";
            //if (administrative == "True")

            //{
            //    where = "IsDone = 0";
            //}
            //else
            //{
            //    where = "IsDone = 0 and CustodianID in (" + custodianid + ")";
            //}
            //try
            //{

            //    return DataAccess.MicrosoftDB.GetDataTable(
            //        "UtilPaging"
            //        , CommandType.StoredProcedure
            //        , conString
            //        , new SqlParameter("datasrc", "[dbo].[VW_InventoryHeader]")
            //        , new SqlParameter("orderBy", "InventoryID desc")
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

        public DataTable GetPropertyInventoryList(string ConnString, string inventorycode, int pageNo)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string where = "";
                where = "InventoryCode = '" + inventorycode + "'";
                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_InventoryPropertyDetails]")
                    , new SqlParameter("orderBy", "RecID asc")
                    , new SqlParameter("fieldlist", "*")
                    , new SqlParameter("filter", where)
                    , new SqlParameter("pageNum", pageNo)
                    , new SqlParameter("pageSize", 20)
                    );
            }
            catch
            {
                throw;
            }
        }
        /// </summary>
        /// <param name="ConnString"></param>
        /// <param name="inventorycode"></param>
        /// <returns></returns>

        //csv file
        public DataTable GetPropertyInventoryList(string ConnString, string inventorycode)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string where = "";
                where = "InventoryCode = '" + inventorycode + "'";
                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_InventoryPropertyDetails]")
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
        public DataTable GetPropertyInventoryListPrintPAR(string ConnString, string inventorycode)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string where = "";
                where = "InventoryCode = '" + inventorycode + "' and PARICS='1'";
                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_InventoryPropertyDetails]")
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
        public DataTable GetPropertyInventoryListPrintICS(string ConnString, string inventorycode)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string where = "";
                where = "InventoryCode = '" + inventorycode + "' and PARICS='3' ";
                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_InventoryPropertyDetails]")
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
        public DataTable GetPropertyInventoryListPrintICSH(string ConnString, string inventorycode)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string where = "";
                where = "InventoryCode = '" + inventorycode + "' and (PARICS='2' OR PARICS='3') ";
                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_InventoryPropertyDetails]")
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
        public DataTable GetPropertyInventoryListPrint(string ConnString, string inventorycode)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string where = "";
                where = "InventoryCode = '" + inventorycode + "'";
                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_InventoryPropertyDetails]")
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
        public DataTable GetPropertyInventoryListPrintFOUND(string ConnString, string inventorycode)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string where = "";
                where = "InventoryCode = '" + inventorycode + "' AND Status=1 ";
                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_InventoryPropertyDetails]")
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
        public DataTable GetPropertyInventoryListPrintNOTFOUND(string ConnString, string inventorycode)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string where = "";
                where = "InventoryCode = '" + inventorycode + "' AND Status=0 ";
                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_InventoryPropertyDetails]")
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
        public DataTable GetPropertyInventoryListPrintMISPLACED(string ConnString, string inventorycode)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string where = "";
                where = "InventoryCode = '" + inventorycode + "' AND (Status != 1 AND Status != 0) ";
                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_InventoryPropertyDetails]")
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

        public DataTable GetStockInventoryList(string ConnString, string inventorycode, int pageNo)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string where = "";
                where = "InventoryCode = '" + inventorycode + "'";
                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_InventoryStockDetails]")
                    , new SqlParameter("orderBy", "RecID asc")
                    , new SqlParameter("fieldlist", "*")
                    , new SqlParameter("filter", where)
                    , new SqlParameter("pageNum", pageNo)
                    , new SqlParameter("pageSize", 20)
                    );
            }
            catch
            {
                throw;
            }
        }
        public DataTable GetStockInventoryListPrint(string ConnString, string inventorycode)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string where = "";
                where = "InventoryCode = '" + inventorycode + "'";
                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_InventoryStockDetails]")
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

        public int SaveInventory(InventoryEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_SaveInventory"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("InventoryID", x.InventoryID)
                    , new SqlParameter("InventoryName", x.InventoryName)
                    , new SqlParameter("SiteID", x.SiteID)
                    , new SqlParameter("LocationID", x.LocationID)
                    , new SqlParameter("InventoryType", x.InventoryType)
                    , new SqlParameter("CustodianID", x.CustodianIDStr)
                    , new SqlParameter("Description", x.Description)
                    , new SqlParameter("User", x.CreatedBy)
                    , new SqlParameter("ExpenseType", x.ExpenseType)
                    , new SqlParameter("AreaID", x.AreaID)
                    );
            }
            catch (Exception ex)
            {
                string msg = ex.Message.ToString();
                throw;
            }
        }

        public int UpdateFound(string ConnString, int id, string inventorycode)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteCommand(
                    "SP_InventoryUpdateFound"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("RecID", id)
                    , new SqlParameter("InvCode", inventorycode)
                    );
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public int UpdateFoundByBarcode(string ConnString, string barcode, string inventorycode)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteCommand(
                    "SP_InventoryUpdateFoundByBarcode"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("Barcode", barcode)
                    , new SqlParameter("InvCode", inventorycode)
                    );
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public int UpdateMisplaced(string ConnString, int id, string inventorycode)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteCommand(
                    "SP_InventoryUpdateMisplaced"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("RecID", id)
                    , new SqlParameter("InvCode", inventorycode)
                    );
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public int UpdateNotFound(string ConnString, int id, string inventorycode)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteCommand(
                    "SP_InventoryUpdateNotFound"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("RecID", id)
                    , new SqlParameter("InvCode", inventorycode)
                    );
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public int UpdateVariance(string ConnString, int recid, int firstcount, int secondcount, int thirdcount, int variance)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteCommand(
                    "SP_InventoryUpdateVariance"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("RecID", recid)
                    , new SqlParameter("FirstCount", firstcount)
                    , new SqlParameter("SecondCount", secondcount)
                    , new SqlParameter("ThirdCount", thirdcount)
                    , new SqlParameter("Variance", variance)
                    );
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public int UploadInventoryCount(InventoryEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_UploadInventoryCount"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("InventoryCode", x.InventoryCode)
                    , new SqlParameter("User", x.CreatedBy)
                    );
            }
            catch
            {
                throw;
            }
        }

        #endregion Inventory Count Process

        #region Inventory Count View

        public DataTable GetInventoryListView(string ConnString, string custodianid, int siteid, string search, string datefrom, string dateto, string Filter, string administrative)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            custodianid = custodianid.Replace(';', ',');
            // DateTime startdate = Convert.ToDateTime(datefrom);
            //DateTime enddate = Convert.ToDateTime(dateto);

            //enddate.AddDays(1);
            try
            {
                string where = "";
                if (administrative == "True")
                {
                    if (search != "")
                    {
                        where = "IsDone = 1 and (InvevntoryName like '%" + search + "%' or Description  like '%" + search + "%') and (CreatedDate between '" + datefrom + "' and dateadd(day,1,'" + dateto + "')) " + Filter;
                    }
                    else
                    {
                        where = "IsDone = 1 and (CreatedDate between '" + datefrom + "' and dateadd(day,1,'" + dateto + "')) " + Filter;
                    }
                }
                else
                {
                    if (search != "")
                    {
                        where = "CustodianID in (" + custodianid + ")  and IsDone = 1 and (InvevntoryName like '%" + search + "%' or Description  like '%" + search + "%') and (CreatedDate between '" + datefrom + "' and dateadd(day,1,'" + dateto + "')) " + Filter;
                    }
                    else
                    {
                        where = "CustodianID in (" + custodianid + ")  and IsDone = 1 and (CreatedDate between '" + datefrom + "' and dateadd(day,1,'" + dateto + "')) " + Filter;
                    }
                }

                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_InventoryHeader]")
                    , new SqlParameter("orderBy", "InventoryID Desc")
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

        #endregion Inventory Count View
    }
}