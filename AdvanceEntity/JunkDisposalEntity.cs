using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvanceEntity 
{
    public class JunkDisposalEntity : BaseEntity
    {
        public string ApprovedBy { get; set; }
        public string TransactionBy { get; set; }
        public string TransactionNo { get; set; }
        public string Remarks { get; set; }
        public string Status { get; set; }
        public string SupportingDocument { get; set; }

        
    }
}
