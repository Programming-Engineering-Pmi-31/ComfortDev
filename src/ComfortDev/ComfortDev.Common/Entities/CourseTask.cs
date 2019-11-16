using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ComfortDev.Common.Entities
{
    public partial class CourseTask
    {
        public int Id { get; set; }

        [JsonIgnore]
        public int CourseId { get; set; }

        [JsonIgnore]
        public int TaskId { get; set; }
        public int CompPer { get; set; }

        [JsonIgnore]
        public virtual UserCourse Course { get; set; }
        public virtual Task Task { get; set; }
    }
}
