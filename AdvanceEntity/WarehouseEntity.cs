namespace AdvanceEntity
{
    public class WarehouseEntity : BaseEntity
    {
        public bool Active { get; set; }
        public string Code { get; set; }
        public bool DirectReturnToSupplier { get; set; }
        public int MotherWarehouseID { get; set; }
        public string User { get; set; }
        public string WarehouseAdmin { get; set; }
        public int WarehouseID { get; set; }
        public string WarehouseName { get; set; }
        public string WarehouseCode { get; set; }

    }
}