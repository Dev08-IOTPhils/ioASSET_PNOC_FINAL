namespace AdvanceEntity
{
    public class BrandEntity : BaseEntity
    {
        public bool Active { get; set; }
        public int BrandID { get; set; }
        public string BrandName { get; set; }
        public string Description { get; set; }
    }
}