namespace AdvanceEntity
{
    public class ComponentEntity : BaseEntity
    {
        //Request Management / Approval
        public string ComponentNo { get; set; }
        public string TransactorName { get; set; }
        public string Status { get; set; }
        public int ApproverID { get; set; }
        public string Remarks { get; set; }

    }
}