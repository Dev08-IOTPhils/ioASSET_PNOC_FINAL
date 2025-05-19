using AdvanceDal;
using AdvanceEntity;
using System.Data;

namespace AdvanceBll
{
    public class BrandBll
    {
        private ActivityLogBll activityLogBLL = new ActivityLogBll();
        private BrandDal brandDAL = new BrandDal();
        public string BrandbyID(BrandEntity brandEntity)
        {
            try
            {
                string result = "";

                DataTable dt = brandDAL.BrandbyID(brandEntity);

                result = dt.Rows[0]["BrandID"].ToString() + "|" +
                         dt.Rows[0]["BrandName"].ToString() + "|" +
                         dt.Rows[0]["Description"].ToString() + "|" +
                         dt.Rows[0]["Active"].ToString();
                return result;
            }
            catch
            {
                throw;
            }
        }

        public int DeleteBrand(BrandEntity brandEntity)
        {
            int returnval = brandDAL.DeleteBrand(brandEntity);
            try
            {
                return returnval;
            }
            catch
            {
                return returnval;
            }
        }

        public DataTable GetBrand(string ConnString)
        {
            try
            {
                return brandDAL.GetBrand(ConnString);
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetBrandList(string ConnString)
        {
            try
            {
                return brandDAL.GetBrandList(ConnString);
            }
            catch
            {
                throw;
            }
        }

        public int SaveBrand(BrandEntity brandEntity)
        {
            int id = brandDAL.SaveBrand(brandEntity);
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