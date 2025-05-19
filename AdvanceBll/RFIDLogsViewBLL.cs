using AdvanceDal;
using System;
using System.Data;

namespace AdvanceBll
{
    public class RFIDLogsViewBLL
    {
        private string responce = "";
        private RFIDLogsViewDAL z = new RFIDLogsViewDAL();

        public DataTable GetRFIDLOGS(string connstring, string datefrom, string dateTo)
        {
            try
            {
                return z.GetRFIDLOGS(connstring, datefrom, dateTo);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}