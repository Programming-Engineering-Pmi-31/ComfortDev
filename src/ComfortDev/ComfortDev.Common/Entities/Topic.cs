using System;
using System.Collections.Generic;

namespace ComfortDev.Common.Entities
{
    public partial class Topic
    {
        public Topic()
        {
            Tasks = new HashSet<Task>();
            TestAnswers = new HashSet<TestAnswer>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string ImageSource { get; set; }

        public virtual ICollection<Task> Tasks { get; set; }
        public virtual ICollection<TestAnswer> TestAnswers { get; set; }
    }
}
