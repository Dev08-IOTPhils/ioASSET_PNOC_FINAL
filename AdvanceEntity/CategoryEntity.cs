namespace AdvanceEntity
{
    public class CategoryEntity : BaseEntity
    {
        public bool Active { get; set; }
        public int AssetInventoryAccount { get; set; }
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int Life { get; set; }
        public int LOBID { get; set; }
        public int StartCount { get; set; }
        public int SubCategoryID { get; set; }
        public string SubCategoryName { get; set; }
        public bool Taggable { get; set; }
    }
}