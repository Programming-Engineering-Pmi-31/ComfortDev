using System;
using System.Collections.Generic;

namespace TestApp
{
    public partial class TestQuestion
    {
        public TestQuestion()
        {
            TestAnswer = new HashSet<TestAnswer>();
        }

        public int Id { get; set; }
        public string Question { get; set; }

        public virtual ICollection<TestAnswer> TestAnswer { get; set; }
    }
}
