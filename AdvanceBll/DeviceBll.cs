using AdvanceDal;
using AdvanceEntity;
using System;
using System.Data;

namespace AdvanceBll
{
    public class DeviceBll
    {
        private ActivityLogBll activityLogBLL = new ActivityLogBll();
        private DeviceDal DeviceDal = new DeviceDal();
        #region Device management

        public int DeleteDevice(DeviceEntity DeviceEntity)
        {
            try
            {
                DeviceDal.DeleteDevice(DeviceEntity);
                return 1;
            }
            catch
            {
                return 1;
            }
        }

        public string DevicebyID(DeviceEntity DeviceEntity)
        {
            try
            {
                string result = "";

                DataTable dt = DeviceDal.DevicebyID(DeviceEntity);

                result = dt.Rows[0]["DeviceID"].ToString() + "|" +
                         dt.Rows[0]["DeviceName"].ToString() + "|" +
                         dt.Rows[0]["IPaddress"].ToString() + "|" +
                         dt.Rows[0]["Port"].ToString() + "|" +
                         dt.Rows[0]["Description"].ToString() + "|" +
                         dt.Rows[0]["DeviceTypeID"].ToString() + "|" +
                         dt.Rows[0]["Active"].ToString() + "|" +
                         dt.Rows[0]["DeviceStatus"].ToString();
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public DataTable GetDevice(string ConnString)
        {
            try
            {
                return DeviceDal.GetDevice(ConnString);
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetDeviceList(string ConnString)
        {
            try
            {
                return DeviceDal.GetDeviceList(ConnString);
            }
            catch
            {
                throw;
            }
        }

        public int SaveDevice(DeviceEntity DeviceEntity)
        {
            int id = DeviceDal.SaveDevice(DeviceEntity);
            try
            {
                return id;
            }
            catch
            {
                return id;
            }
        }
        #endregion Device management

        #region Device type

        public DataTable GetDeviceTypeList(string ConnString)
        {
            try
            {
                return DeviceDal.GetDeviceTypeList(ConnString);
            }
            catch
            {
                throw;
            }
        }

        #endregion Device type
    }
}