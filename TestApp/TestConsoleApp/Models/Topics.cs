using System;
using System.Collections.Generic;

namespace TestConsoleApp
{
    public partial class Topics
    {
        public Topics()
        {
            Tasks = new HashSet<Tasks>();
            TestAnswers = new HashSet<TestAnswers>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string ImageSource { get; set; }

        public virtual ICollection<Tasks> Tasks { get; set; }
        public virtual ICollection<TestAnswers> TestAnswers { get; set; }
    }
}
