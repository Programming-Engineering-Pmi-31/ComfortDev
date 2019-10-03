using System;
using System.Collections.Generic;

namespace ComfortDev.DAL.Entities
{
    public partial class UserCourse
    {
        public UserCourse()
        {
            CourseTasks = new HashSet<CourseTask>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime EndDate { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<CourseTask> CourseTasks { get; set; }
    }
}
