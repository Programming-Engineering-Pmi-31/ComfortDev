using System;
using System.Collections.Generic;

namespace TestConsoleApp
{
    public partial class UserCourses
    {
        public UserCourses()
        {
            CourseTasks = new HashSet<CourseTasks>();
        }

        public int Id { get; set; }
        public string UserName { get; set; }
        public DateTime EndDate { get; set; }

        public virtual Users UserNameNavigation { get; set; }
        public virtual ICollection<CourseTasks> CourseTasks { get; set; }
    }
}
