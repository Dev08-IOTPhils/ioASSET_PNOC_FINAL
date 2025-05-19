using AdvanceDal;
using AdvanceEntity;
using System.Data;

namespace AdvanceBll
{
    public class PropertyTaggingBll
    {
        private PropertyTaggingDal propertyTaggingDal = new PropertyTaggingDal();

        #region Property tagging

        public int AddToPrint(PropertyEntity x)
        {
            try
            {
                int id = propertyTaggingDal.AddToPrint(x);
                return 1;
            }
            catch
            {
                return 1;
            }
        }

        public DataTable AvailableForTaggingPerGR(string ConnString, int tagtype)
        {
            try
            {
                return propertyTaggingDal.AvailableForTaggingPerGR(ConnString, tagtype);
            }
            catch
            {
                throw;
            }
        }

        public int ConfirmPrint(PropertyEntity x)
        {
            try
            {
                int id = propertyTaggingDal.ConfirmPrint(x);
                return 1;
            }
            catch
            {
                return 1;
            }
        }

        public string GetCount(PropertyEntity x)
        {
            try
            {
                string result = "";

                DataTable dt = propertyTaggingDal.GetCount(x);
                result = dt.Rows[0]["Count"].ToString();
                return result;
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetPrintJobs(PropertyEntity x)
        {
            try
            {
                return propertyTaggingDal.GetPrintJobs(x);
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetPrintList(PropertyEntity x)
        {
            try
            {
                return propertyTaggingDal.GetPrintList(x);
            }
            catch
            {
                throw;
            }
        }

        public DataTable PropertyAvailableForTagging(string ConnString, int tagtype)
        {
            try
            {
                return propertyTaggingDal.PropertyAvailableForTagging(ConnString, tagtype);
            }
            catch
            {
                throw;
            }
        }

        public int RemoveAllToPrint(PropertyEntity x)
        {
            try
            {
                int id = propertyTaggingDal.RemoveAllToPrint(x);
                return 1;
            }
            catch
            {
                return 1;
            }
        }

        public int RemoveToPrint(PropertyEntity x)
        {
            try
            {
                int id = propertyTaggingDal.RemoveToPrint(x);
                return 1;
            }
            catch
            {
                return 1;
            }
        }

        public int TagProperty(PropertyEntity x)
        {
            try
            {
                int id = propertyTaggingDal.TagProperty(x);
                return 1;
            }
            catch
            {
                return 1;
            }
        }

        //
        public DataTable ViewPropertyTaggingPerGR(string ConnString, int tagtype, string GRNumber)
        {
            try
            {
                return propertyTaggingDal.ViewPropertyTaggingPerGR(ConnString, tagtype, GRNumber);
            }
            catch
            {
                throw;
            }
        }
        public int WriteTags(PropertyEntity x)
        {
            try
            {
                int id = propertyTaggingDal.WriteTags(x);
                return 1;
            }
            catch
            {
                return 1;
            }
        }
        #endregion Property tagging

        #region stock tagging

        public int AddToPrintStock(PropertyEntity x)
        {
            try
            {
                int id = propertyTaggingDal.AddToPrintStock(x);
                return 1;
            }
            catch
            {
                return 1;
            }
        }

        public int ConfirmPrintStock(PropertyEntity x)
        {
            try
            {
                int id = propertyTaggingDal.ConfirmPrintStock(x);
                return 1;
            }
            catch
            {
                return 1;
            }
        }

        public string GetCountStock(PropertyEntity x)
        {
            try
            {
                string result = "";

                DataTable dt = propertyTaggingDal.GetCount(x);
                result = dt.Rows[0]["Count"].ToString();
                return result;
            }
            catch
            {
                throw;
            }
        }

        public int RemoveAllToPrintStock(PropertyEntity x)
        {
            try
            {
                int id = propertyTaggingDal.RemoveAllToPrintStock(x);
                return 1;
            }
            catch
            {
                return 1;
            }
        }

        public int RemoveToPrintStock(PropertyEntity x)
        {
            try
            {
                int id = propertyTaggingDal.RemoveToPrintStock(x);
                return 1;
            }
            catch
            {
                return 1;
            }
        }

        public DataTable StockAvailableForRetagging(int tagtype, string ConnString)
        {
            try
            {
                return propertyTaggingDal.StockAvailableForRetagging(tagtype, ConnString);
            }
            catch
            {
                throw;
            }
        }

        public DataTable StockAvailableForTagging(int tagtype, string ConnString)
        {
            try
            {
                return propertyTaggingDal.StockAvailableForTagging(tagtype, ConnString);
            }
            catch
            {
                throw;
            }
        }
        public int WriteTagsStock(PropertyEntity x)
        {
            try
            {
                int id = propertyTaggingDal.WriteTags(x);
                return 1;
            }
            catch
            {
                return 1;
            }
        }
        #endregion stock tagging

        #region retagging

        public DataTable AvailableForRetaggingPerGR(string ConnString, int tagtype)
        {
            try
            {
                return propertyTaggingDal.AvailableForRetaggingPerGR(ConnString, tagtype);
            }
            catch
            {
                throw;
            }
        }

        public DataTable PropertyAvailableForRetagging(string ConnString, int tagtype)
        {
            try
            {
                return propertyTaggingDal.PropertyAvailableForRetagging(ConnString, tagtype);
            }
            catch
            {
                throw;
            }
        }

        //
        public DataTable ViewPropertyForRetaggingPerGR(string ConnString, int tagtype, string GRNumber)
        {
            try
            {
                return propertyTaggingDal.ViewPropertyForRetaggingPerGR(ConnString, tagtype, GRNumber);
            }
            catch
            {
                throw;
            }
        }
        #endregion retagging
    }
}