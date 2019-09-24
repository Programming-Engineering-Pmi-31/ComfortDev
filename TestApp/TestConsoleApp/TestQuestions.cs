using System;
using System.Collections.Generic;

namespace TestConsoleApp
{
    public partial class TestQuestions
    {
        public TestQuestions()
        {
            TestAnswers = new HashSet<TestAnswers>();
        }

        public int Id { get; set; }
        public string Question { get; set; }

        public virtual ICollection<TestAnswers> TestAnswers { get; set; }
    }
}
