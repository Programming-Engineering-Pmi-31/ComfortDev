using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ComfortDev.Common.Entities
{
    public partial class Task
    {
        public Task()
        {
            CourseTasks = new HashSet<CourseTask>();
        }

        public int Id { get; set; }
        public string Title { get; set; }

        [JsonIgnore]
        public int TopicId { get; set; }
        public string TaskText { get; set; }
        public string EvalCrit { get; set; }

        public virtual Topic Topic { get; set; }

        [JsonIgnore]
        public virtual ICollection<CourseTask> CourseTasks { get; set; }
    }
}
