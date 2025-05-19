using System.Collections.Generic;

namespace AdvanceEntity
{
    public class Steps
    {
        public List<Steps> GetSteps { get; set; }
        public string StepsList { get; set; }
    }

    public class TaskEntity : BaseEntity
    {
        private List<Steps> ListSteps = new List<Steps>();
        public bool Active { get; set; }
        public string Description { get; set; }
        public List<Steps> GetSetSteps
        {
            set { ListSteps = value; }
            get { return ListSteps; }
        }

        public int NumberOfSteps { get; set; }
        public string Steps { get; set; }
        public string StringNumberOfSteps { get; set; }
        public int TaskID { get; set; }
        public string TaskName { get; set; }
    }
}