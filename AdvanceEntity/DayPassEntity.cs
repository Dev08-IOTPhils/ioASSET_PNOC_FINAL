namespace AdvanceEntity
{
    public class DayPassEntity : BaseEntity
    {
        public string Approver { get; set; }
        public int ApproverID { get; set; }
        public string Brand { get; set; }
        public string Category { get; set; }
        public string DateOfTransaction { get; set; }
        public string DayPassNumber { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string isIn { get; set; }
        public string isInString { get; set; }
        public string isOut { get; set; }
        public string isOutString { get; set; }
        public string Purpose { get; set; }
        public int RecID { get; set; }
        public string RecIDstring { get; set; }
        public string RequestedBy { get; set; }
        public string SerialNumber { get; set; }
        public string Status { get; set; }
        public string Remarks { get; set; }
    }
}