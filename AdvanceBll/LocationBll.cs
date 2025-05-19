using AdvanceDal;
using AdvanceEntity;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web.UI.WebControls;

namespace AdvanceBll
{
    public class LocationBll
    {
        private ActivityLogBll activityLogBLL = new ActivityLogBll();
        private LocationDal locationDAL = new LocationDal();
        public int CheckLC(LocationEntity x)
        {
            try
            {
                return locationDAL.CheckLC(x);
            }
            catch
            {
                throw;
            }
        }

        public int CountSubLocation(string ConnString, int parentid)
        {
            try
            {
                return locationDAL.CountSubLocation(ConnString, parentid);
            }
            catch
            {
                throw;
            }
        }

        public int DeleteLocation(LocationEntity locationEntity)
        {
            int returnVal = locationDAL.DeleteLocation(locationEntity);
            try
            {
                return returnVal;
            }
            catch
            {
                return returnVal;
            }
        }

        public DataTable GetDynamicLocationList(LocationEntity locationEntity)
        {
            try
            {
                return locationDAL.GetDynamicLocationList(locationEntity);
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetDynamicLocationTree(LocationEntity locationEntity)
        {
            try
            {
                return locationDAL.GetDynamicLocationTree(locationEntity);
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetLocation(string ConnString)
        {
            try
            {
                return locationDAL.GetLocation(ConnString);
            }
            catch
            {
                throw;
            }
        }

        public string GetLocationByName(LocationEntity locationEntity)
        {
            try
            {
                string result = "";

                DataTable dtLocation = locationDAL.GetLocationByName(locationEntity);

                result = dtLocation.Rows[0]["LocationID"].ToString() + "|" +
                        dtLocation.Rows[0]["Location"].ToString() + "|" +
                        dtLocation.Rows[0]["WarehouseID"].ToString() + "|" +
                        dtLocation.Rows[0]["Description"].ToString() + "|" +
                        dtLocation.Rows[0]["Active"].ToString() + "|" +
                        dtLocation.Rows[0]["LocationCode"].ToString();

                return result;
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetLocationList(LocationEntity locationEntity)
        {
            try
            {
                return locationDAL.GetLocationList(locationEntity);
            }
            catch
            {
                throw;
            }
        }

        public List<ListItem> GetLocationListBySite(LocationEntity areaEntity)
        {
            List<ListItem> position = new List<ListItem>();
            try
            {
                DataTable dt = locationDAL.GetLocationListBySite(areaEntity);

                StringBuilder sb = new StringBuilder();
                foreach (DataRow dr in dt.Rows)
                {
                    //foreach (DataColumn col in dt.Columns)
                    //{
                    position.Add(new ListItem
                    {
                        Value = dr[1].ToString(),
                        Text = dr[0].ToString()
                    });
                    //}
                }
                return position;
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetLocationListBySiteIC(LocationEntity areaEntity)
        {
            List<ListItem> position = new List<ListItem>();
            try
            {
                DataTable dt = locationDAL.GetLocationListBySite(areaEntity);

                return dt;
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetLocationListBySitePT(LocationEntity areaEntity)
        {
            try
            {
                DataTable dt = locationDAL.GetLocationListBySite(areaEntity);

                return dt;
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetLocationListBySiteTo(LocationEntity areaEntity)
        {
            try
            {
                DataTable dt = locationDAL.GetLocationListBySite(areaEntity);

                return dt;
            }
            catch
            {
                throw;
            }
        }

        public List<ListItem> GetOfficeByLocation(LocationEntity areaEntity)
        {
            List<ListItem> position = new List<ListItem>();
            try
            {
                DataTable dt = locationDAL.GetOfficeByLocation(areaEntity);

                StringBuilder sb = new StringBuilder();
                foreach (DataRow dr in dt.Rows)
                {
                    //foreach (DataColumn col in dt.Columns)
                    //{
                    position.Add(new ListItem
                    {
                        Value = dr[1].ToString(),
                        Text = dr[0].ToString()
                    });
                    //}
                }
                return position;
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetOfficeByLocationAC(LocationEntity areaEntity)
        {
            try
            {
                DataTable dt = locationDAL.GetOfficeByLocation(areaEntity);

                return dt;
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetSubLocation(string ConnString, int parentid)
        {
            try
            {
                return locationDAL.GetSubLocation(ConnString, parentid);
            }
            catch
            {
                throw;
            }
        }

        public int SaveDynamicLocation(LocationEntity locationEntity)
        {
            int id = locationDAL.SaveDynamicLocation(locationEntity);
            try
            {
                return id;
            }
            catch
            {
                return id;
            }
        }

        public int SaveLocation(LocationEntity locationEntity)
        {
            int id = locationDAL.SaveLocation(locationEntity);
            try
            {
                return id;
            }
            catch
            {
                return id;
            }
        }
        //Dynamic Location
    }
}