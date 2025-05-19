using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace AdvanceDal
{
    public class WidgetsDal
    {
        public DataTable PropertyImportantSched(string ConnString, string condition, string dateStart, string dateEnd, string label, string isdet)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                //string WHERE = "WHERE convert(datetime, [Date] , 101) BETWEEN convert(datetime, '" + dateStart + "' , 101) AND convert(datetime, '" + dateEnd + "' , 101)";

                //string query = "SELECT * FROM VW_PropertyImportantSched " + WHERE;
                //string query = "SELECT DISTINCT VIP.[ACTIVITY] as 'Label',(SELECT COUNT(*) as 'value' From[ioASSET_DICT].[dbo].[VW_PropertyImportantSched] WHERE convert(datetime, [Date], 101) BETWEEN convert(datetime, '" + dateStart + "', 101) AND convert(datetime, '" + dateEnd + "', 101 ) AND[ACTIVITY] = VIP.ACTIVITY) as value FROM[ioASSET_DICT].[dbo].[VW_PropertyImportantSched] VIP WHERE convert(datetime, [Date], 101) BETWEEN convert(datetime, '02/01/2023', 101) AND convert(datetime, '02/21/2023', 101)";
                string query;
                string WHERE;
                if (condition == "table")
                {
                    WHERE = "WHERE convert(datetime, [Date] , 101) BETWEEN convert(datetime, '" + dateStart + "' , 101) AND convert(datetime, '" + dateEnd + "' , 101) ";
                    query = "SELECT * FROM VW_PropertyImportantSched " + WHERE;
                }
                else if (condition == "bar")
                {
                    if (isdet == "display")
                    {
                        query = "SELECT DISTINCT VIP.[ACTIVITY] as 'label',(SELECT COUNT(*) as 'value' From [VW_PropertyImportantSched] WHERE convert(datetime, [Date], 101) BETWEEN convert(datetime, '" + dateStart + "', 101) AND convert(datetime, '" + dateEnd + "', 101 ) AND[ACTIVITY] = VIP.ACTIVITY) as value FROM [VW_PropertyImportantSched] VIP WHERE convert(datetime, [Date], 101) BETWEEN convert(datetime, '" + dateStart + "', 101) AND convert(datetime, '" + dateEnd + "', 101)";
                    }
                    else
                    {
                        //query = "SELECT DISTINCT VIP.[ACTIVITY] as 'label',(SELECT COUNT(*) as 'value' From[ioASSET_DICT].[dbo].[VW_PropertyImportantSched] WHERE convert(datetime, [Date], 101) BETWEEN convert(datetime, '" + dateStart + "', 101) AND convert(datetime, '" + dateEnd + "', 101 ) AND[ACTIVITY] = VIP.ACTIVITY) as value FROM[ioASSET_DICT].[dbo].[VW_PropertyImportantSched] VIP WHERE convert(datetime, [Date], 101) BETWEEN convert(datetime, '02/01/2023', 101) AND convert(datetime, '02/21/2023', 101)";
                        //query = "SELECT DISTINCT VIP.[ACTIVITY] ,(SELECT COUNT(*) From[ioASSET_DICT].[dbo].[VW_PropertyImportantSched] WHERE convert(datetime, [Date], 101) BETWEEN convert(datetime, '" + dateStart + "', 101) AND convert(datetime, '" + dateEnd + "', 101 ) AND[ACTIVITY] = VIP.ACTIVITY) FROM[ioASSET_DICT].[dbo].[VW_PropertyImportantSched] VIP WHERE convert(datetime, [Date], 101) BETWEEN convert(datetime, '02/01/2023', 101) AND convert(datetime, '02/21/2023', 101)";
                        WHERE = "WHERE convert(datetime, [Date] , 101) BETWEEN convert(datetime, '" + dateStart + "' , 101) AND convert(datetime, '" + dateEnd + "' , 101) AND ACTIVITY ='" + label + "'";
                        query = "SELECT * FROM VW_PropertyImportantSched " + WHERE;
                    }
                }
                else if (condition == "pie")
                {
                    if (isdet == "display")
                    {
                        query = "SELECT DISTINCT VIP.[ACTIVITY] as 'label',(SELECT COUNT(*) as 'value' From  [VW_PropertyImportantSched] WHERE convert(datetime, [Date], 101) BETWEEN convert(datetime, '" + dateStart + "', 101) AND convert(datetime, '" + dateEnd + "', 101 ) AND[ACTIVITY] = VIP.ACTIVITY) as value FROM [VW_PropertyImportantSched] VIP WHERE convert(datetime, [Date], 101) BETWEEN convert(datetime, '" + dateStart + "', 101) AND convert(datetime, '" + dateEnd + "', 101)";
                    }
                    else
                    {
                        query = "SELECT* FROM VW_PropertyImportantSched WHERE convert(datetime, [Date], 101) BETWEEN convert(datetime, '" + dateStart + "', 101) AND convert(datetime, '" + dateEnd + "', 101) AND ACTIVITY='" + label + "'";
                    }

                    //query = "SELECT DISTINCT VIP.[ACTIVITY] as 'label',(SELECT COUNT(*) as 'value' From[ioASSET_DICT].[dbo].[VW_PropertyImportantSched] WHERE convert(datetime, [Date], 101) BETWEEN convert(datetime, '" + dateStart + "', 101) AND convert(datetime, '" + dateEnd + "', 101 ) AND[ACTIVITY] = VIP.ACTIVITY) as value FROM[ioASSET_DICT].[dbo].[VW_PropertyImportantSched] VIP WHERE convert(datetime, [Date], 101) BETWEEN convert(datetime, '02/01/2023', 101) AND convert(datetime, '02/21/2023', 101)";
                    //WHERE = "WHERE convert(datetime, [Date] , 101) BETWEEN convert(datetime, '" + dateStart + "' , 101) AND convert(datetime, '" + dateEnd + "' , 101) AND ACTIVITY = '"+label+"'";
                    //query = "SELECT (*) FROM [ioASSET_DICT].[dbo].[VW_PropertyImportantSched] " + WHERE;
                }
                else
                {
                    WHERE = "WHERE convert(datetime, [Date] , 101) BETWEEN convert(datetime, '" + dateStart + "' , 101) AND convert(datetime, '" + dateEnd + "' , 101) AND ACTIVITY ='" + label + "'";
                    query = "SELECT * FROM VW_PropertyImportantSched " + WHERE;
                }
                //string query = "SELECT ACTIVITY as 'Label', COUNT(*) as 'value' From VW_PropertyImportantSched WHERE convert(datetime, [Date] , 101) BETWEEN convert(datetime, '02/01/2023' , 101) AND convert(datetime, '02/21/2023' , 101)GROUP BY ACTIVITY ";
                //string query = "SELECT [ACTIVITY] as Label, COUNT(*) as value From [ioASSET_DICT].[dbo].[VW_PropertyImportantSched] " + WHERE + "GROUP BY ACTIVITY ";
                return DataAccess.MicrosoftDB.GetDataTable(
                                   query
                                   , CommandType.Text
                                   , conString
                                   );
            }
            catch (Exception e)
            {
                throw;
            }
        }

        //}
        public DataTable PropertyStatusChartDonnut(string ConnString)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string query = "SELECT * FROM VW_PropertyStatusCountDonnutChart";
                return DataAccess.MicrosoftDB.GetDataTable(
                    query
                    , CommandType.Text
                    , conString
                    );
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public DataTable StocksUnderMinLevel(string ConnString, string condition)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string query;
                if (condition == "pie")
                {
                    query = "SELECT ItemCode as 'label', Quantity as 'value' FROM VW_StockUnderMinLevel";
                }
                else
                {
                    query = "SELECT * FROM VW_StockUnderMinLevel";
                }
                return DataAccess.MicrosoftDB.GetDataTable(
                    query
                    , CommandType.Text
                    , conString
                    );
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public DataTable StocksUnderMinLevelGraph(string ConnString, string condition, string stat)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                string query;
                if (condition == "pie")
                {
                    query = "SELECT ItemCode as 'label', Quantity as 'value' FROM VW_StockUnderMinLevel where ItemCode ='" + stat + "'";
                }
                else
                {
                    query = "SELECT * FROM VW_StockUnderMinLevel  where ItemCode ='" + stat + "'";
                }
                return DataAccess.MicrosoftDB.GetDataTable(
                    query
                    , CommandType.Text
                    , conString
                    );
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public DataTable Top10MostUsedProperty(string ConnString)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_RetrieveTop10MostUsed"
                    , CommandType.StoredProcedure
                    , conString
                    //, new SqlParameter("StartDate", startdate)
                    //, new SqlParameter("EndDate", enddate)
                    );
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public DataTable StocksUnderMinLevelForPie(string ConnString)
        //{
        //    string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
        //    try
        //    {
        //        string query = "SELECT ItemCode as 'label', Quantity as 'value' FROM VW_StockUnderMinLevel";
        //        return DataAccess.MicrosoftDB.GetDataTable(
        //            query
        //            , CommandType.Text
        //            , conString
        //            );
        //    }
        //    catch (Exception e)
        //    {
        //        throw;
        //    }
        public DataTable Top10MostUsedProperty1(string startdate, string enddate, string ConnString)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                //string query = "select distinct(ItemCode) as ItemCode, sum(value) as value,[Description] "
                //            + "From VW_QueryByDate "
                //            + "where Convert(datetime, CONVERT(NVARCHAR(255), [Date], 105)) between '"+ startdate+ "' and '"+enddate+"' "
                //            + "Group by ItemCode,Description";
                string query = "SELECT * FROM VW_Top10MostUsedProperty";
                return DataAccess.MicrosoftDB.GetDataTable(
                    query
                    , CommandType.Text
                    , conString
                    );
            }
            catch (Exception e)
            {
                throw;
            }
        }
        public DataTable Top10MostUsedProperty1(string ConnString, string itemcode)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_RetrieveTop10MostUsed1"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("Itemcode", itemcode)
                    //, new SqlParameter("StartDate", startdate)
                    //, new SqlParameter("EndDate", enddate)
                    );
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable Top10MostUsedStock(string ConnString)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_RetrieveTop10MostUsedStock"
                    , CommandType.StoredProcedure
                    , conString
                    //, new SqlParameter("StartDate", startdate)
                    //, new SqlParameter("EndDate", enddate)
                    );
            }
            catch
            {
                throw;
            }
        }

        public DataTable Top10MostUsedStock1(string ConnString, string itemcode)
        {
            string conString = ConfigurationManager.ConnectionStrings[ConnString].ToString();
            try
            {
                return DataAccess.MicrosoftDB.GetDataTable(
                    "SP_RetrieveTop10MostUsedStock1"
                    , CommandType.StoredProcedure
                    , conString
                    , new SqlParameter("Itemcode", itemcode)
                    //, new SqlParameter("StartDate", startdate)
                    //, new SqlParameter("EndDate", enddate)
                    );
            }
            catch
            {
                throw;
            }
        }
    }
}