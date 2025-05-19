using AdvanceDal;
using AdvanceEntity;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web.UI.WebControls;

namespace AdvanceBll
{
    public class StockMergeBll
    {
        private StockMergeDal y = new StockMergeDal();

        public int CancelStockMerge(StockEntity x)
        {
            int id = y.CancelStockMerge(x);
            try
            {
                return id;
            }
            catch
            {
                return id;
            }
        }

        public DataTable GetAvailableStockForMerge(string ConnString, string search, string custodianid, int siteid)
        {
            try
            {
                return y.GetAvailableStockForMerge(ConnString, search, custodianid, siteid);
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetPendingApprovalStockMerge(string search, string ConnString, string createdby)
        {
            try
            {
                return y.GetPendingApprovalStockMerge(search, ConnString, createdby);
            }
            catch
            {
                throw;
            }
        }

        public List<ListItem> GetStockItemCode(string ConnString, string itemcode, int custodian, int site)
        {
            List<ListItem> position = new List<ListItem>();
            try
            {
                DataTable dt = y.GetStockItemCode(ConnString, itemcode, custodian, site);

                StringBuilder sb = new StringBuilder();
                foreach (DataRow dr in dt.Rows)
                {
                    //foreach (DataColumn col in dt.Columns)
                    //{
                    position.Add(new ListItem
                    {
                        Value = dr[1].ToString(),
                        Text = dr[0].ToString()
                    });
                    //}
                }
                return position;
            }
            catch
            {
                throw;
            }
        }

        public string MergeStock(StockEntity stockEntity)
        {
            string id = y.MergeStock(stockEntity);
            try
            {
                return id;
            }
            catch
            {
                return id;
            }
        }
        #region Request Management

        public int ApproveStockMerge(StockEntity x)
        {
            int id = y.ApproveStockMerge(x);
            try
            {
                return id;
            }
            catch
            {
                return id;
            }
        }

        public int DeclineStockMerge(StockEntity x)
        {
            int id = y.DeclineStockMerge(x);
            try
            {
                return id;
            }
            catch
            {
                return id;
            }
        }

        public DataTable GetPendingApprovalStockMergeRM(string ConnString, string administrative, int approver)
        {
            try
            {
                if (administrative == "True")
                {
                    return y.GetPendingApprovalStockMergeRM(ConnString);
                }
                else
                {
                    return y.GetPendingApprovalStockMergeRM(ConnString, approver);
                }
            }
            catch
            {
                throw;
            }
        }
        #endregion Request Management

        #region Stock Merge History

        public DataTable StockMergeHistory(string ConnString, int siteid, string search, string datefrom, string dateto, string Filter)
        {
            try
            {
                return y.StockMergeHistory(ConnString, siteid, search, datefrom, dateto, Filter);
            }
            catch
            {
                throw;
            }
        }

        #endregion Stock Merge History
    }
}