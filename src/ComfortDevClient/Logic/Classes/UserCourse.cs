using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Logic.Classes
{
    [DataContract(Name = "UserCourse")]
    public class UserCourse
    {
        [DataMember(Name = "endDate")]
        public DateTime EndDate { get; set; }

        [DataMember(Name = "courseTasks")]
        public IEnumerable<CourseTask> CourseTasks { get; set; }
    }
}
