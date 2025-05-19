namespace AdvanceEntity
{
    public class LocationEntity : BaseEntity
    {
        public bool Active { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string Extension { get; set; }
        public string id { get; set; }
        public string level { get; set; }
        public string Location { get; set; }
        public string LocationCode { get; set; }
        public int LocationID { get; set; }
        public string LocationIDstring { get; set; }
        public string LocationType { get; set; }
        public int ParentLocationID { get; set; }
        public string ParentLocationIDstring { get; set; }
        public string text { get; set; }
        public string User { get; set; }
        public int Value { get; set; }
        public string Warehouse { get; set; }
        public int WarehouseID { get; set; }
    }
}