using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using AdvanceEntity;
using AdvanceDal;

namespace AdvanceBll
{
    public class GoodsReceiveHistoryBll
    {
        GoodsReceiveHistoryDal y = new GoodsReceiveHistoryDal();
        public DataTable GetGoodsReceiveHeader(string ConnString, int siteid, string search, string datefrom, string dateto, string Filter)
        {
            try
            {
                return y.GetGoodsReceiveHeader(ConnString, siteid, search, datefrom, dateto, Filter);
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetGoodsReceiveDetails(string ConnString, int goodreceiveid)
        {
            try
            {
                return y.GetGoodsReceiveDetails(ConnString, goodreceiveid);
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetGRHeader(string ConnString, int goodreceiveid)
        {
            try
            {
                return y.GetGRHeader(ConnString, goodreceiveid);
            }
            catch
            {
                throw;
            }
        }

    }
}
