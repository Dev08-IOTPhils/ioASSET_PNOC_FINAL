using AdvanceDal;
using AdvanceEntity;
using System;
using System.Data;

namespace AdvanceBll
{
    public class UOMBll
    {
        private ActivityLogBll activityLogBLL = new ActivityLogBll();
        private UOMDal uomDal = new UOMDal();
        public int checkUOMCode(UOMEntity uomEntity)
        {
            try
            {
                return uomDal.checkUOMCode(uomEntity);
            }
            catch
            {
                throw;
            }
        }

        public int DeleteUOM(UOMEntity uomEntity)
        {
            int returnVal = uomDal.DeleteUnitOfMeasurement(uomEntity);
            try
            {
                return returnVal;
            }
            catch
            {
                return returnVal;
            }
        }

        public DataTable GetUOM(string ConnString)
        {
            try
            {
                return uomDal.GetUnitOfMeasurement(ConnString);
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetUOMlist(string ConnString)
        {
            try
            {
                return uomDal.GetUOMlist(ConnString);
            }
            catch
            {
                throw;
            }
        }

        public int SaveUOM(UOMEntity uomEntity)
        {
            int id = uomDal.SaveUnitOfMeasurement(uomEntity);
            try
            {
                return id;
            }
            catch (Exception e)
            {
                return id;
            }
        }

        public string UnitOfMeasurementByID(UOMEntity uomEntity)
        {
            try
            {
                string result = "";

                DataTable dt = uomDal.UnitOfMeasurementByID(uomEntity);

                result = dt.Rows[0]["UnitOfMeasurementID"].ToString() + "|" +
                         dt.Rows[0]["UnitOfMeasurement"].ToString() + "|" +
                         dt.Rows[0]["Description"].ToString() + "|" +
                         dt.Rows[0]["Active"].ToString() + "|" +
                         dt.Rows[0]["UOMCode"].ToString();
                return result;
            }
            catch
            {
                throw;
            }
        }
    }
}