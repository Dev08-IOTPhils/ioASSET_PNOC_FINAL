using AdvanceDal;
using AdvanceEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web.UI.WebControls;

namespace AdvanceBll
{
    public class AreaBll
    {
        private ActivityLogBll activityLogBLL = new ActivityLogBll();
        private AreaDal areaDal = new AreaDal();
        public int DeleteArea(AreaEntity areaEntity)
        {
            int returnVal = areaDal.DeleteArea(areaEntity);
            try
            {
                return returnVal;
            }
            catch (Exception ex)
            {
                return returnVal;
            }
        }

        public DataTable GetArea(string ConnString)
        {
            try
            {
                return areaDal.GetArea(ConnString);
            }
            catch
            {
                throw;
            }
        }

        public string GetAreaByName(AreaEntity areaEntity)
        {
            try
            {
                string result = "";

                DataTable dtOffice = areaDal.GetAreaByName(areaEntity);

                result = dtOffice.Rows[0]["AreaID"].ToString() + "|" +
                        dtOffice.Rows[0]["Area"].ToString() + "|" +
                        dtOffice.Rows[0]["Description"].ToString() + "|" +
                        dtOffice.Rows[0]["Active"].ToString() + "|" +
                        dtOffice.Rows[0]["LocationID"].ToString();

                return result;
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetAreaList(AreaEntity areaEntity)
        {
            try
            {
                return areaDal.GetAreaList(areaEntity);
            }
            catch
            {
                throw;
            }
        }

        public List<ListItem> GetAreaListByLocation(AreaEntity areaEntity)
        {
            List<ListItem> position = new List<ListItem>();
            try
            {
                DataTable dt = areaDal.GetAreaListByLocation(areaEntity);

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

        public DataTable GetAreaListByLocationPT(AreaEntity areaEntity)
        {
            try
            {
                DataTable dt = areaDal.GetAreaListByLocation(areaEntity);

                return dt;
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetAreaListByLocationTo(AreaEntity areaEntity)
        {
            try
            {
                DataTable dt = areaDal.GetAreaListByLocation(areaEntity);

                return dt;
            }
            catch
            {
                throw;
            }
        }

        public int SaveArea(AreaEntity areaEntity)
        {
            int id = areaDal.SaveArea(areaEntity);
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