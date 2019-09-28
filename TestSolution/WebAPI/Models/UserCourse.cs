using System;
using System.Collections.Generic;

namespace WebAPI
{
    public partial class UserCourse
    {
        public UserCourse()
        {
            CourseTask = new HashSet<CourseTask>();
        }

        public int Id { get; set; }
        public string UserName { get; set; }
        public DateTime EndDate { get; set; }

        public virtual User UserNameNavigation { get; set; }
        public virtual ICollection<CourseTask> CourseTask { get; set; }
    }
}
