using AdvanceDal;
using AdvanceEntity;
using System.Data;

namespace AdvanceBll
{
    public class PositionBll
    {
        private ActivityLogBll activityLogBLL = new ActivityLogBll();
        private PositionDal positionDAL = new PositionDal();
        public int DeletePosition(PositionEntity positionEntity)
        {
            int returnVal = positionDAL.DeletePosition(positionEntity);
            try
            {
                return returnVal;
            }
            catch
            {
                return returnVal;
            }
        }

        public DataTable GetPosition(string ConnString)
        {
            try
            {
                return positionDAL.GetPosition(ConnString);
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetPosition(PositionEntity positionEntity)
        {
            try
            {
                return positionDAL.GetPosition(positionEntity);
            }
            catch
            {
                throw;
            }
        }

        public string GetPositionByID(PositionEntity positionEntity)
        {
            try
            {
                string result = "";

                DataTable dtPosition = positionDAL.GetPositionByID(positionEntity);

                result = dtPosition.Rows[0]["PositionID"].ToString() + "|" +
                        dtPosition.Rows[0]["PositionName"].ToString() + "|" +
                        dtPosition.Rows[0]["DepartmentID"].ToString() + "|" +
                        dtPosition.Rows[0]["Description"].ToString() + "|" +
                        dtPosition.Rows[0]["Active"].ToString();

                return result;
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetPositionList(PositionEntity positionEntity)
        {
            try
            {
                return positionDAL.GetPositionList(positionEntity);
            }
            catch
            {
                throw;
            }
        }

        public int SavePosition(PositionEntity positionEntity)
        {
            int id = positionDAL.SavePosition(positionEntity);
            try
            {
                return id;
            }
            catch
            {
                return id;
            }
        }
        //public List<ListItem> GetPosition(PositionEntity positionEntity)
        //{
        //    List<ListItem> subcategory = new List<ListItem>();
        //    try
        //    {
        //        DataTable dt = positionDAL.GetPosition(positionEntity);

        //        foreach (DataRow dr in dt.Rows)
        //        {
        //            subcategory.Add(new ListItem
        //            {
        //                Value = dr[0].ToString(),
        //                Text = dr[1].ToString()
        //            });
        //        }
        //        return subcategory;
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}
    }
}