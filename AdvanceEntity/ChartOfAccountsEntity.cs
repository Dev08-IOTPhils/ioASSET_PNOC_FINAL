namespace AdvanceEntity
{
    public class ChartOfAccountsEntity : BaseEntity
    {
        public string AccountHeader { get; set; }
        public string AccountNumber { get; set; }
        public bool Active { get; set; }
        public decimal Balance { get; set; }
        public int ChartOfAccountsID { get; set; }
        public string Description { get; set; }
        public string StringBalance { get; set; }
    }
}