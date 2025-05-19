namespace AdvanceEntity
{
    public class ClassificationEntity : BaseEntity
    {
        public bool Active { get; set; }
        public string ClassificationCode { get; set; }
        public int ClassificationID { get; set; }
        public string ClassificationName { get; set; }
        public string Count { get; set; }
        public string Description { get; set; }
        public int StartingCount { get; set; }

        public string IsFloater { get; set; }

    }
}