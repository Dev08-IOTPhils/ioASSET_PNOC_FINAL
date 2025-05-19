namespace AdvanceEntity
{
    public class PositionEntity : BaseEntity
    {
        public bool Active { get; set; }
        public string Department { get; set; }
        public int DepartmentID { get; set; }
        public string Description { get; set; }
        public string Position { get; set; }
        public int PositionID { get; set; }
    }
}