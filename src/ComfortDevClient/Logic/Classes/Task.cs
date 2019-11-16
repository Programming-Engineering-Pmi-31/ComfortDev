using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Logic.Classes
{
    [DataContract(Name = "Task")]
    public class Task
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "taskText")]
        public string TaskText { get; set; }

        [DataMember(Name = "evalCrit")]
        public string EvaluationCriteria { get; set; }

        [DataMember(Name = "topic")]
        public virtual Topic Topic { get; set; }
    }
}
