namespace AdvanceEntity
{
    public class UOMEntity : BaseEntity
    {
        public bool Active { get; set; }
        public string Description { get; set; }
        public string UnitOfMeasurement { get; set; }
        public int UnitOfMeasurementID { get; set; }
        public string UOMCode { get; set; }
    }
}