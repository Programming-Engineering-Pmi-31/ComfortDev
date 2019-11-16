using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ComfortDev.Common.Entities
{
    public partial class UserCourse
    {
        public UserCourse()
        {
            CourseTasks = new HashSet<CourseTask>();
        }

        [JsonIgnore]
        public int Id { get; set; }

        [JsonIgnore]
        public int UserId { get; set; }
        public DateTime EndDate { get; set; }

        [JsonIgnore]
        public virtual User User { get; set; }
        public virtual ICollection<CourseTask> CourseTasks { get; set; }
    }
}
