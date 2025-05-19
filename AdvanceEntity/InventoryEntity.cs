namespace AdvanceEntity
{
    public class InventoryEntity : BaseEntity
    {
        public int CustodianID { get; set; }
        public string DateCreated { get; set; }
        public string Description { get; set; }
        public string InventoryCode { get; set; }
        public int InventoryID { get; set; }
        public string InventoryName { get; set; }
        public int InventoryType { get; set; }
        public string InventoryTypeString { get; set; }
        public string Location { get; set; }
        public int LocationID { get; set; }
        public string Site { get; set; }
        public int SiteID { get; set; }
        public string SupportingDocument { get; set; }
        public int employeeID { get; set; }
        public string CustodianIDStr { get; set; }
    }
}