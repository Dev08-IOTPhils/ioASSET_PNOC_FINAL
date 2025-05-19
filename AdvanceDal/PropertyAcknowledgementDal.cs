using AdvanceEntity;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace AdvanceDal
{
    public class PropertyAcknowledgementDal
    {
        public int AcknowledgedProperty(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.ExecuteScalar<int>(
                    "SP_AcknowledgeProperty"
                    , CommandType.StoredProcedure
                    , conString
                    //, new SqlParameter("PropertyNumber", x.PropertyNumber)
                    , new SqlParameter("User", x.CreatedBy)
                    , new SqlParameter("PMLID", x.RecID)
                    );
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public DataTable GetPropertyAcknowledge(string ConnString, string Action, int UserID)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_PropertyForAcknowledgement"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("filter", UserID)
                    , new SqlParameter("Action", Action)
                    );
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}