using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Logic.Classes
{
    [DataContract(Name = "TestAnswer")]
    public class Answer
    {
        [DataMember(Name = "answer")]
        public string AnswerText { get; set; }

        [DataMember(Name = "topic")]
        public Topic Topic { get; set; }
    }
}
