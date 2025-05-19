using AdvanceDal;
using System.Data;

namespace AdvanceBll
{
    public class WidgetsBll
    {
        private WidgetsDal w = new WidgetsDal();

        public DataTable PropertyImportantSched(string ConnString, string condition, string dateStart, string dateEnd, string label, string isdet)
        {
            try
            {
                return w.PropertyImportantSched(ConnString, condition, dateStart, dateEnd, label, isdet);
            }
            catch
            {
                throw;
            }
        }

        public DataTable PropertyStatusChartDonnut(string ConnString)
        {
            try
            {
                return w.PropertyStatusChartDonnut(ConnString);
            }
            catch
            {
                throw;
            }
        }

        public DataTable StocksUnderMinLevel(string ConnString, string condition)
        {
            try
            {
                return w.StocksUnderMinLevel(ConnString, condition);
            }
            catch
            {
                throw;
            }
        }

        public DataTable StocksUnderMinLevelGraph(string ConnString, string condition, string stat)
        {
            try
            {
                return w.StocksUnderMinLevelGraph(ConnString, condition, stat);
            }
            catch
            {
                throw;
            }
        }
        public DataTable Top10MostUsedProperty(string ConnString)
        {
            try
            {
                return w.Top10MostUsedProperty(ConnString);
            }
            catch
            {
                throw;
            }
        }

        public DataTable Top10MostUsedProperty1(string ConnString, string itemcode)
        {
            try
            {
                return w.Top10MostUsedProperty1(ConnString, itemcode);
            }
            catch
            {
                throw;
            }
        }

        public DataTable Top10MostUsedStock(string ConnString)
        {
            try
            {
                return w.Top10MostUsedStock(ConnString);
            }
            catch
            {
                throw;
            }
        }

        public DataTable Top10MostUsedStock1(string ConnString, string itemcode)
        {
            try
            {
                return w.Top10MostUsedStock1(ConnString, itemcode);
            }
            catch
            {
                throw;
            }
        }
    }
}