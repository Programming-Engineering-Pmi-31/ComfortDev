using System;
using System.Collections.Generic;

namespace ComfortDev.DAL.Entities
{
    public partial class User
    {
        public User()
        {
            UserCourses = new HashSet<UserCourse>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Hash { get; set; }

        public virtual ICollection<UserCourse> UserCourses { get; set; }
    }
}
