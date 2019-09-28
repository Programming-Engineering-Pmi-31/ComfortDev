using System;
using System.Collections.Generic;

namespace WebAPI
{
    public partial class Task
    {
        public Task()
        {
            CourseTask = new HashSet<CourseTask>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public int TopicId { get; set; }
        public string EvalCrit { get; set; }

        public virtual Topic Topic { get; set; }
        public virtual ICollection<CourseTask> CourseTask { get; set; }
    }
}
