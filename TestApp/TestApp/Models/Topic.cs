using System;
using System.Collections.Generic;

namespace TestApp
{
    public partial class Topic
    {
        public Topic()
        {
            Task = new HashSet<Task>();
            TestAnswer = new HashSet<TestAnswer>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string ImageSource { get; set; }

        public virtual ICollection<Task> Task { get; set; }
        public virtual ICollection<TestAnswer> TestAnswer { get; set; }
    }
}
