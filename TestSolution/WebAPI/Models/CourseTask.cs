using System;
using System.Collections.Generic;

namespace WebAPI
{
    public partial class CourseTask
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public int TaskId { get; set; }
        public int CompPer { get; set; }

        public virtual UserCourse Course { get; set; }
        public virtual Task Task { get; set; }
    }
}
