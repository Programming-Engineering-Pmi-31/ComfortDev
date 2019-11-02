using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Logic.Classes
{
    [DataContract(Name = "TestQuestion")]
    public class Question
    {
        [DataMember(Name = "question")]
        public string QuestionText { get; set; }

        [DataMember(Name = "testAnswers")]
        public IEnumerable<Answer> Answers { get; set; }
    }
}
