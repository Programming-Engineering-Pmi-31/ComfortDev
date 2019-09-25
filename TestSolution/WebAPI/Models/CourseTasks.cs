using System;
using System.Collections.Generic;

namespace WebAPI
{
    public partial class CourseTasks
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public int TaskId { get; set; }
        public int CompPer { get; set; }

        public virtual UserCourses Course { get; set; }
        public virtual Tasks Task { get; set; }
    }
}
