namespace AdvanceEntity
{
    public class CustodianEntity : BaseEntity
    {
        public bool Active { get; set; }
        public string Custodian { get; set; }
        public int CustodianID { get; set; }
        public string Description { get; set; }
    }
}