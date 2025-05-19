using AdvanceDal;
using System.Data;

namespace AdvanceBll
{
    public class TaggingHistoryBll
    {
        private TaggingHistoryDal taggingHistoryDal = new TaggingHistoryDal();

        public int DeleteFilter(string id, string ConnString)
        {
            try
            {
                return taggingHistoryDal.DeleteFilter(id, ConnString);
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
                return taggingHistoryDal.GetActive(id, ConnString);
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetActiveFilter(string ReportName, string ConnString, string UserName, string IsActive)
        {
            try
            {
                return taggingHistoryDal.GetActiveFilter(ReportName, ConnString, UserName, IsActive);
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
                return taggingHistoryDal.GetFilter(ReportName, ConnString, UserName);
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetTaggingHistory(string DateFrom, string DateTo, string ConnString, string Filter)
        {
            try
            {
                return taggingHistoryDal.GetTaggingHistory(DateFrom, DateTo, ConnString, Filter);
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
                return taggingHistoryDal.SaveFilter(ReportName, Username, Logic, Filter, Operator, Value, ConnString);
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
                return taggingHistoryDal.UpdateFilter(RecId, ReportName, Username, Logic, Filter, Operator, Value, ConnString);
            }
            catch
            {
                throw;
            }
        }
    }
}