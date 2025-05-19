namespace AdvanceEntity
{
    public class ModuleEntity : BaseEntity
    {
        public bool Active { get; set; }
        public int Arrangement { get; set; }
        public string ArrangemenT { get; set; }
        public string Description { get; set; }
        public bool IsWebPage { get; set; }
        public int ModuleID { get; set; }
        public string Name { get; set; }
        public string ParentModule { get; set; }
        public int ParentModuleID { get; set; }
        public string URL { get; set; }
        public string User { get; set; }
        public string WebPage { get; set; }
    }
}