using AdvanceDal;
using System.Data;

namespace AdvanceBll
{
    public class PropertyMasterlistBll
    {
        private PropertyMasterlist z = new PropertyMasterlist();

        public int DeleteFilter(string id, string ConnString)
        {
            try
            {
                return z.DeleteFilter(id, ConnString);
            }
            catch
            {
                throw;
            }
        }

        public int GetActive(string id, string ConnString)
        {
            try
            {
                return z.GetActive(id, ConnString);
            }
            catch
            {
                throw;
            }
        }

        //filter
        public DataTable GetActiveFilter(string ReportName, string ConnString, string UserName, string IsActive)
        {
            try
            {
                return z.GetActiveFilter(ReportName, ConnString, UserName, IsActive);
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetFilter(string ReportName, string ConnString, string UserName)
        {
            try
            {
                return z.GetFilter(ReportName, ConnString, UserName);
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetFilterColumn(string ConnString, string ModuleViewName)
        {
            try
            {
                return z.GetFilterColumn(ConnString, ModuleViewName);
            }
            catch
            {
                throw;
            }
        }

        //widget
        public DataTable GetPropertyByStatus(string ConnString, string status)
        {
            try
            {
                return z.GetPropertyByStatus(ConnString, status);
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetPropertyInformation(string ConnString, string search)
        {
            try
            {
                return z.GetPropertyInformation(ConnString, search);
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetPropertyMasterlist(string ConnString, string search, string Filter)
        {
            try
            {
                return z.GetPropertyMasterlist(ConnString, search, Filter);
            }
            catch
            {
                throw;
            }
        }
        public DataTable GetTop10Property(string ConnString, string itemcode)
        {
            try
            {
                return z.GetTop10Property(ConnString, itemcode);
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetTop10Stock(string ConnString, string itemcode)
        {
            try
            {
                return z.GetTop10Stock(ConnString, itemcode);
            }
            catch
            {
                throw;
            }
        }

        public int SaveFilter(string ReportName, string Username, string Logic, string Filter, string Operator, string Value, string ConnString)
        {
            try
            {
                return z.SaveFilter(ReportName, Username, Logic, Filter, Operator, Value, ConnString);
            }
            catch
            {
                throw;
            }
        }

        public int UpdateFilter(string RecId, string ReportName, string Username, string Logic, string Filter, string Operator, string Value, string ConnString)
        {
            try
            {
                return z.UpdateFilter(RecId, ReportName, Username, Logic, Filter, Operator, Value, ConnString);
            }
            catch
            {
                throw;
            }
        }
    }
}