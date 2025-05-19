namespace AdvanceEntity
{
    public class TagTypeEntity : BaseEntity
    {
        public bool Active { get; set; }
        public string Description { get; set; }
        public string Tag { get; set; }
        public int TagID { get; set; }
        public string TagType { get; set; }
        public int TagTypeID { get; set; }
    }
}