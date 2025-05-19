using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace AdvanceDal
{
    public class RSMIDal
    {
        public DataTable GetList(string ConnString, int type, string Filter, int departmentid, string datefrom, string dateto)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();

            try
            {
                string where = "";
                if (departmentid == 0)
                {
                    where = " (CreatedDate between '" + datefrom + "' and DateAdd(day, 1, '" + dateto + "')) " + Filter;
                }
                else
                {
                    where = " DepartmentID = '" + departmentid + "'  and (CreatedDate between '" + datefrom + "' and DateAdd(day, 1, '" + dateto + "')) " + Filter;
                }

                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_DICT_RSMI_Stock]")
                    , new SqlParameter("orderBy", "ItemCode ASC")
                    , new SqlParameter("fieldlist", "*")
                    , new SqlParameter("filter", where)
                    , new SqlParameter("pageNum", 0)
                    , new SqlParameter("pageSize", 0)
                    );
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public DataTable getlists(string ConnString, int departmentid, string datefrom, string dateto)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string where = "";
                if (departmentid == 0)
                {
                    where = " (CreatedDate between '" + datefrom + "' and DateAdd(day, 1, '" + dateto + "')) ";
                }
                else
                {
                    where = " DepartmentID = '" + departmentid + "'  and (CreatedDate between '" + datefrom + "' and DateAdd(day, 1, '" + dateto + "')) ";
                }

                return DataAccess.MicrosoftDB.GetDataTable(
                    "UtilPaging"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("datasrc", "[dbo].[VW_DICT_RSMI_Stock]")
                    , new SqlParameter("orderBy", "ItemCode ASC")
                    , new SqlParameter("fieldlist", "*")
                    , new SqlParameter("filter", where)
                    , new SqlParameter("pageNum", 0)
                    , new SqlParameter("pageSize", 0)
                    );
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public DataTable getlists1(string ConnString, int departmentid, string datefrom, string dateto)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_DICT_RSMI_Recapitulation"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("DateFrom", datefrom)
                    , new SqlParameter("DateTo", dateto)
                    , new SqlParameter("DepartmentID", departmentid)
                    );
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}