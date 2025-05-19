using AdvanceDal;
using AdvanceEntity;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web.UI.WebControls;

namespace AdvanceBll
{
    public class WarehouseBll
    {
        private ActivityLogBll activityLogBLL = new ActivityLogBll();
        private WarehouseDal warehouseDal = new WarehouseDal();
        public int DeleteWarehouse(WarehouseEntity warehouseEntity)
        {
            int returnVal = warehouseDal.DeleteWarehouse(warehouseEntity);
            try
            {
                return returnVal;
            }
            catch
            {
                return returnVal;
            }
        }

        public DataTable GetWarehouse(string ConnString)
        {
            try
            {
                return warehouseDal.GetWarehouse(ConnString);
            }
            catch
            {
                throw;
            }
        }

        //public string GetWarehouseByID(WarehouseEntity warehouseEntity)
        public List<ListItem> GetWarehouseByID(WarehouseEntity warehouseEntity)
        {
            try
            {
                // string result = "";

                DataTable dtWarehouse = warehouseDal.GetWarehouseByID(warehouseEntity);

                //result = dtWarehouse.Rows[0]["WarehouseID"].ToString() + "|" +
                //        dtWarehouse.Rows[0]["WarehouseName"].ToString() + "|" +
                //        dtWarehouse.Rows[0]["Active"].ToString();

                List<ListItem> position = new List<ListItem>();
                StringBuilder sb = new StringBuilder();
                foreach (DataRow dr in dtWarehouse.Rows)
                {
                    position.Add(new ListItem
                    {
                        Value = dr[1].ToString(),
                        Text = dr[0].ToString()
                    });
                }

                return position;
            }
            catch
            {
                throw;
            }
        }

        public string GetWarehouseByName(WarehouseEntity x)
        {
            try
            {
                string result = "";

                DataTable dtOffice = warehouseDal.GetWarehouseByName(x);

                result = dtOffice.Rows[0]["WarehouseID"].ToString() + "|" +
                        dtOffice.Rows[0]["WarehouseName"].ToString() + "|" +
                        dtOffice.Rows[0]["WarehouseCode"].ToString() + "|" +
                        dtOffice.Rows[0]["Active"].ToString();

                return result;
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetWarehouseList(string ConnString)
        {
            try
            {
                return warehouseDal.GetWarehouseList(ConnString);
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetWarehousePT(WarehouseEntity warehouseEntity)
        {
            try
            {
                DataTable dtWarehouse = warehouseDal.GetWarehouseByID(warehouseEntity);

                return dtWarehouse;
            }
            catch
            {
                throw;
            }
        }

        public int SaveWarehouse(WarehouseEntity warehouseEntity)
        {
            int id = warehouseDal.SaveWarehouse(warehouseEntity);
            try
            {
                return id;
            }
            catch
            {
                return id;
            }
        }
    }
}