using System.Collections.Generic;

namespace AdvanceEntity
{
    public class RFIDnumberEntity : BaseEntity
    {
        public List<RFIDNumbers> GetSetRFIDnumber
        {
            set { RFIDNumber = value; }
            get { return RFIDNumber; }
        }

        public List<RFIDNumbers> RFIDNumber { get; set; }
    }

    public class RFIDNumbers
    {
        public List<RFIDNumbers> getSetRFIDnumber { get; set; }
        public string RFIDnumber { get; set; }
    }
}