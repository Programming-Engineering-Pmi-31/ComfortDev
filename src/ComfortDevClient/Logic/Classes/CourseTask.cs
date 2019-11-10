using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Logic.Classes
{
    [DataContract(Name = "CourseTask")]
    public class CourseTask
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "compPer")]
        public int CompletionPercent { get; set; }

        [DataMember(Name = "task")]
        public Task Task { get; set; }
    }
}
