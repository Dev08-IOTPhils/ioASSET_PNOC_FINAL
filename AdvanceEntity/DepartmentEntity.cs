namespace AdvanceEntity
{
    public class DepartmentEntity : BaseEntity
    {
        public bool Active { get; set; }
        public string Department { get; set; }
        public int DepartmentID { get; set; }
        public string Description { get; set; }
        public int PositionID { get; set; }
        public string User { get; set; }
    }
}