using AdvanceDal;
using AdvanceEntity;
using System;
using System.Data;

namespace AdvanceBll
{
    public class TagTypeBll
    {
        private TagTypeDal tagTypeDal = new TagTypeDal();

        public int DeleteTag(TagTypeEntity x)
        {
            int returnVal = tagTypeDal.DeleteTag(x);
            try
            {
                return returnVal;
            }
            catch (Exception ex)
            {
                ex.ToString();
                return returnVal;
            }
        }

        public int DeleteTagType(TagTypeEntity x)
        { 
            int returnVal = tagTypeDal.DeleteTag(x);

            try
            {
                return returnVal;
            }
            catch (Exception ex)
            {
                ex.ToString();
                return returnVal;
            }
        }

        //Printers and Writers
        public DataTable GetDeviceList(string ConnString)
        {
            try
            {
                return tagTypeDal.GetDeviceList(ConnString);
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetDeviceListUSBWriter(string ConnString)
        {
            try
            {
                return tagTypeDal.GetDeviceListUSBWriter(ConnString);
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetTag(string ConnString)
        {
            try
            {
                return tagTypeDal.GetTag(ConnString);
            }
            catch
            {
                throw;
            }
        }

        public string GetTagByID(TagTypeEntity areaEntity)
        {
            try
            {
                string result = "";

                DataTable dtOffice = tagTypeDal.GetTagByID(areaEntity);

                result = dtOffice.Rows[0]["TagID"].ToString() + "|" +
                        dtOffice.Rows[0]["TagTypeID"].ToString() + "|" +
                        dtOffice.Rows[0]["TagName"].ToString() + "|" +
                        dtOffice.Rows[0]["Description"].ToString() + "|" +
                        dtOffice.Rows[0]["Active"].ToString();

                return result;
            }
            catch
            {
                throw;
            }
        }

        //Tag
        public DataTable GetTagList(string ConnString)
        {
            try
            {
                return tagTypeDal.GetTagList(ConnString);
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetTagType(string ConnString)
        {
            try
            {
                return tagTypeDal.GetTagType(ConnString);
            }
            catch
            {
                throw;
            }
        }

        public string GetTagTypeByName(TagTypeEntity areaEntity)
        {
            try
            {
                string result = "";

                DataTable dtOffice = tagTypeDal.GetTagTypeByName(areaEntity);

                result = dtOffice.Rows[0]["TagTypeID"].ToString() + "|" +
                        dtOffice.Rows[0]["TagType"].ToString() + "|" +
                        dtOffice.Rows[0]["Description"].ToString() + "|" +
                        dtOffice.Rows[0]["IsActive"].ToString();

                return result;
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetTagTypeList(string ConnString)
        {
            try
            {
                return tagTypeDal.GetTagTypeList(ConnString);
            }
            catch
            {
                throw;
            }
        }

        public int SaveTag(TagTypeEntity x)
        {
            int id = tagTypeDal.SaveTag(x);
            try
            {
                return id;
            }
            catch
            {
                return id;
            }
        }

        public int SaveTagType(TagTypeEntity x)
        {
            int id = tagTypeDal.SaveTagType(x);
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