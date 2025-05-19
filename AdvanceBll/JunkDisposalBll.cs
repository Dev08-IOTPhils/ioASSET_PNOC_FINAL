using AdvanceDal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvanceBll
{
    public class JunkDisposalBll
    {
        private JunkDisposalDal JD = new JunkDisposalDal();
        public DataTable getJunkDisposal(string ConnString)
        {
            try
            {
                return JD.getJunkDisposal(ConnString);
            }
            catch
            {
                throw;
            }
        }

        public string SaveJD(int JDid,string Mod, int approver, string remarks,string user, string ConnString)
        {
            return JD.SaveJD(JDid,Mod, approver, remarks, user, ConnString).ToString();
      
        }
        public string CompleteJD(string TransactionNo, string user, string ConnString)
        {
            return JD.CompleteJD(TransactionNo, user, ConnString).ToString();

        }
    }
}
