using System;
using System.Collections.Generic;

namespace WebAPI
{
    public partial class User
    {
        public User()
        {
            UserCourse = new HashSet<UserCourse>();
        }

        public string Name { get; set; }
        public string Hash { get; set; }

        public virtual ICollection<UserCourse> UserCourse { get; set; }
    }
}
