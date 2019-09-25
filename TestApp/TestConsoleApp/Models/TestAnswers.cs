using System;
using System.Collections.Generic;

namespace TestConsoleApp
{
    public partial class TestAnswers
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public string Answer { get; set; }
        public int? TopicId { get; set; }

        public virtual TestQuestions Question { get; set; }
        public virtual Topics Topic { get; set; }
    }
}
