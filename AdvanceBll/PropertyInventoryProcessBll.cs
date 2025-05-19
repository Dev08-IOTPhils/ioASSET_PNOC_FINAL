using AdvanceDal;
using AdvanceEntity;
using System.Data;

namespace AdvanceBll
{
    public class PropertyInventoryProcessBll
    {
        private PropertyInventoryProcessDal y = new PropertyInventoryProcessDal();

        public string addPropertyInventoryToList(string ConnString, string inventorycode, string propertyno, int expensetype)
        {
            string id = y.addPropertyInventoryToList(ConnString, inventorycode, propertyno, expensetype);
            try
            {
                return id;
            }
            catch
            {
                return id;
            }
        }

        /// <summary>
        /// /////
        ///
        ///
        public DataTable GetInventoryConsolidated(string ConnString, int site)
        {
            try
            {
                return y.GetInventoryConsolidated(ConnString, site);
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetInventoryList(string ConnString, string custodianid, string administrative)
        {
            try
            {
                return y.GetInventoryList(ConnString, custodianid, administrative);
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetInventoryListView(string ConnString, string custodianid, int siteid, string search, string datefrom, string dateto, string Filter, string administrative)
        {
            try
            {
                return y.GetInventoryListView(ConnString, custodianid, siteid, search, datefrom, dateto, Filter, administrative);
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetPropertyInventoryList(string ConnString, string inventorycode, int pageNo)
        {
            try
            {
                return y.GetPropertyInventoryList(ConnString, inventorycode, pageNo);
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
            try
            {
                return y.GetPropertyInventoryList(ConnString, inventorycode);
            }
            catch
            {
                throw;
            }
        }
        public DataTable GetPropertyInventoryListPrintFOUND(string ConnString, string inventorycode)
        {
            try
            {
                return y.GetPropertyInventoryListPrintFOUND(ConnString, inventorycode);
            }
            catch
            {
                throw;
            }
        }
        public DataTable GetPropertyInventoryListPrintNOTFOUND(string ConnString, string inventorycode)
        {
            try
            {
                return y.GetPropertyInventoryListPrintNOTFOUND(ConnString, inventorycode);
            }
            catch
            {
                throw;
            }
        }
        public DataTable GetPropertyInventoryListPrintMISPLACED(string ConnString, string inventorycode)
        {
            try
            {
                return y.GetPropertyInventoryListPrintMISPLACED(ConnString, inventorycode);
            }
            catch
            {
                throw;
            }
        }
        public DataTable GetPropertyInventoryListPrint(string ConnString, string inventorycode)
        {
            try
            {
                return y.GetPropertyInventoryListPrint(ConnString, inventorycode);
            }
            catch
            {
                throw;
            }
        }
        public DataTable GetPropertyInventoryListPrintPAR(string ConnString, string inventorycode)
        {
            try
            {
                return y.GetPropertyInventoryListPrintPAR(ConnString, inventorycode);
            }
            catch
            {
                throw;
            }
        }
        public DataTable GetPropertyInventoryListPrintICS(string ConnString, string inventorycode)
        {
            try
            {
                return y.GetPropertyInventoryListPrintICS(ConnString, inventorycode);
            }
            catch
            {
                throw;
            }
        }
        public DataTable GetPropertyInventoryListPrintICSH(string ConnString, string inventorycode)
        {
            try
            {
                return y.GetPropertyInventoryListPrintICSH(ConnString, inventorycode);
            }
            catch
            {
                throw;
            }
        }
        public DataTable GetStockInventoryList(string ConnString, string inventorycode, int pageNo)
        {
            try
            {
                return y.GetStockInventoryList(ConnString, inventorycode, pageNo);
            }
            catch
            {
                throw;
            }
        }
        public DataTable GetStockInventoryListPrint(string ConnString, string inventorycode)
        {
            try
            {
                return y.GetStockInventoryListPrint(ConnString, inventorycode);
            }
            catch
            {
                throw;
            }
        }

        public int SaveInventory(InventoryEntity x)
        {
            int id = y.SaveInventory(x);
            try
            {
                return id;
            }
            catch
            {
                return id;
            }
        }

        public int UpdateFound(string ConnString, int id, string inventorycode)
        {
            int returnid = y.UpdateFound(ConnString, id, inventorycode);
            try
            {
                return returnid;
            }
            catch
            {
                return returnid;
            }
        }

        public int UpdateFoundByBarcode(string ConnString, string barcode, string inventorycode)
        {
            int returnid = y.UpdateFoundByBarcode(ConnString, barcode, inventorycode);
            try
            {
                return returnid;
            }
            catch
            {
                return returnid;
            }
        }
        public int UpdateMisplaced(string ConnString, int id, string inventorycode)
        {
            int returnid = y.UpdateMisplaced(ConnString, id, inventorycode);
            try
            {
                return returnid;
            }
            catch
            {
                return returnid;
            }
        }

        public int UpdateNotFound(string ConnString, int id, string inventorycode)
        {
            int returnid = y.UpdateNotFound(ConnString, id, inventorycode);
            try
            {
                return returnid;
            }
            catch
            {
                return returnid;
            }
        }
        public int UpdateVariance(string ConnString, int recid, int firstcount, int secondcount, int thirdcount, int variance)
        {
            int returnid = y.UpdateVariance(ConnString, recid, firstcount, secondcount, thirdcount, variance);
            try
            {
                return returnid;
            }
            catch
            {
                return returnid;
            }
        }
        public int UploadInventoryCount(InventoryEntity x)
        {
            try
            {
                int id = y.UploadInventoryCount(x);
                return 1;
            }
            catch
            {
                return 1;
            }
        }
    }
}