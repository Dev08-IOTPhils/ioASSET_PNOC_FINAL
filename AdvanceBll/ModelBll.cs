using AdvanceDal;
using AdvanceEntity;
using System.Data;

namespace AdvanceBll
{
    public class ModelBll
    {
        private ActivityLogBll activityLogBLL = new ActivityLogBll();
        private ModelDal modelDal = new ModelDal();
        public int DeleteModel(ModelEntity modelEntity)
        {
            int returnVal = modelDal.DeleteModel(modelEntity);
            try
            {
                return returnVal;
            }
            catch
            {
                return returnVal;
            }
        }

        public DataTable GetModel(string ConnString, string orderby)
        {
            try
            {
                return modelDal.GetModel(ConnString, orderby);
            }
            catch
            {
                throw;
            }
        }

        public string GetModelByID(ModelEntity modelEntity)
        {
            try
            {
                string result = "";

                DataTable dtModel = modelDal.GetModelByID(modelEntity);

                result = dtModel.Rows[0]["ModelID"].ToString() + "|" +
                        dtModel.Rows[0]["ModelName"].ToString() + "|" +
                        //dtModel.Rows[0]["SubCategoryID"].ToString() + "|" +
                        dtModel.Rows[0]["Description"].ToString() + "|" +
                        dtModel.Rows[0]["Active"].ToString();
                        //dtModel.Rows[0]["UsefulLife"].ToString() + "|" +
                        //dtModel.Rows[0]["TagType"].ToString() + "|" +
                        //dtModel.Rows[0]["IsComponent"].ToString();
                return result;
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetModelList(string ConnString)
        {
            try
            {
                return modelDal.GetModelList(ConnString);
            }
            catch
            {
                throw;
            }
        }

        public int SaveModel(ModelEntity modelEntity)
        {
            int id = modelDal.SaveModel(modelEntity);
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