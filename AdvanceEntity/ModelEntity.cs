namespace AdvanceEntity
{
    public class ModelEntity : BaseEntity
    {
        public bool Active { get; set; }
        public string brand { get; set; }
        public int BrandID { get; set; }
        //components
        public string Component { get; set; }

        public int ComponentBrandID { get; set; }
        public int ComponentCategoryID { get; set; }
        public int ComponentID { get; set; }
        public int ComponentModelID { get; set; }
        public string Desc { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public bool IsComponent { get; set; }
        public bool IsTaggable { get; set; }
        public int LOBID { get; set; }
        public int ModelID { get; set; }
        public string ModelName { get; set; }
        public string Set { get; set; }
        public int SubCategoryID { get; set; }
        public string Suffix { get; set; }
        public int TagType { get; set; }
        public int UsefulLife { get; set; }
        public string User { get; set; }
    }
}