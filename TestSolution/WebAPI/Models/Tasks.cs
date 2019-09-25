using System;
using System.Collections.Generic;

namespace WebAPI
{
    public partial class Tasks
    {
        public Tasks()
        {
            CourseTasks = new HashSet<CourseTasks>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public int TopicId { get; set; }
        public string EvalCrit { get; set; }

        public virtual Topics Topic { get; set; }
        public virtual ICollection<CourseTasks> CourseTasks { get; set; }
    }
}
