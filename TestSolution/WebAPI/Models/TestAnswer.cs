using System;
using System.Collections.Generic;

namespace WebAPI
{
    public partial class TestAnswer
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public string Answer { get; set; }
        public int? TopicId { get; set; }

        public virtual TestQuestion Question { get; set; }
        public virtual Topic Topic { get; set; }
    }
}
