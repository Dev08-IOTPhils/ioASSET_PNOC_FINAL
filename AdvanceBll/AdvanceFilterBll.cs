using AdvanceDal;
using System.Data;

namespace AdvanceBll
{
    public class AdvanceFilterBll
    {
        private AdvanceFilterDal x = new AdvanceFilterDal();

        public int AddAdvanceFilter(string ReportID, string Logic, string FilterBY, string Operator, string val, string trx, string connstring, string UserID)
        {
            int ret = 0;
            ret = x.AddAdvanceFilter(ReportID, Logic, FilterBY, Operator, val, trx, connstring, UserID);

            return ret;
        }

        public int ADVfilterModification(string RecID, string trx, string connString)
        {
            return x.ADVfilterModification(RecID, trx, connString);
        }

        public DataTable GetAdvancefilterperUser(string reportID, string trx, string connString, string UserID)
        {
            return x.GetAdvancefilterperUser(reportID, trx, connString, UserID);
        }

        public string GetFilesDataForFilter(string Constring)
        {
            var ret = "";
            ret = x.GetFilesDataForFilter(Constring);

            return "";
        }
        //ADVfilterModification
    }
}