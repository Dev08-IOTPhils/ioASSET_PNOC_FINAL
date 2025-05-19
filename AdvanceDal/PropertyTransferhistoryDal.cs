using AdvanceEntity;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace AdvanceDal
{
    public class PropertyTransferhistoryDal
    {
        public DataTable PropertyListTransferHistory(PropertyEntity x)
        {
            string conString = ConfigurationManager.ConnectionStrings[x.ConnectionString].ToString();
            try
            {
                string where = "";
                where = "TransferNumber = '" + x.TransferNumber + "'";

                return DataAccess.MicrosoftDB.GetDataTable(
                "UtilPaging"
                , CommandType.StoredProcedure
                , conString
                , new SqlParameter("datasrc", "[dbo].[VW_PILPendingUpload]")
                , new SqlParameter("orderBy", "RecordID asc")
                , new SqlParameter("fieldlist", "*")
                , new SqlParameter("filter", where)
                , new SqlParameter("pageNum", 0)
                , new SqlParameter("pageSize", 0)
                );
            }
            catch
            {
                throw;
            }
        }
          
        public DataTable PropertyTransferHistory(string ConnString, string custodianid, int siteid, string search, string datefrom, string dateto, string Filter, string administrative)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            custodianid = custodianid.Replace(';', ',');

            string filter = "";
            if (administrative == "True")
            {
                filter = "(CreatedDate between '" + datefrom + "' and DateAdd(day, 1, '" + dateto + "')) and (Status != 'Pending Approval' or Status != 'Pending Upload' or Status != 'Pending Verification')  " + Filter;

            }
            else
            {
                filter = "CustodianID in (" + custodianid + " ) and (CreatedDate between '" + datefrom + "' and DateAdd(day, 1, '" + dateto + "')) and (Status != 'Pending Approval' or Status != 'Pending Upload' or Status != 'Pending Verification')  " + Filter;

            }


            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_SearchPropertyTransferHistory"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("Filter", filter)

                    //, new SqlParameter("CustodianID", custodianid)
                    //, new SqlParameter("SiteID", siteid)
                    //, new SqlParameter("Search", search)
                    //, new SqlParameter("DateFrom", datefrom)
                    //, new SqlParameter("DateTo", dateto)
                    );
            }
            catch (Exception e)
            {
                throw;
            }
            //try
            //{
            //    string where = "Status = 'Deployed' and CustodianID = '"+custodianid+"' and SiteID = '"+siteid+"'";

            //    return DataAccess.MicrosoftDB.GetDataTable(
            //        "UtilPaging"
            //        , CommandType.StoredProcedure
            //        , conString
            //        , new SqlParameter("datasrc", "[dbo].[VW_PropertyTransferPendingUpload]")
            //        , new SqlParameter("orderBy", "RecID ASC")
            //        , new SqlParameter("fieldlist", "*")
            //        , new SqlParameter("filter", where)
            //        , new SqlParameter("pageNum", 0)
            //        , new SqlParameter("pageSize", 0)
            //        );
            //}
            //catch
            //{
            //    throw;
            //}
        }
    }
}