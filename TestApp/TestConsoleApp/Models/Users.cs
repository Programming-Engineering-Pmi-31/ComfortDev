using System;
using System.Collections.Generic;

namespace TestConsoleApp
{
    public partial class Users
    {
        public Users()
        {
            UserCourses = new HashSet<UserCourses>();
        }

        public string Name { get; set; }
        public string Hash { get; set; }

        public virtual ICollection<UserCourses> UserCourses { get; set; }
    }
}
