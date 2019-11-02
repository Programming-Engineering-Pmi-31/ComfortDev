using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ComfortDev.Common.Entities
{
    public partial class TestAnswer
    {
        [JsonIgnore]
        public int Id { get; set; }
        [JsonIgnore]
        public int QuestionId { get; set; }
        public string Answer { get; set; }
        [JsonIgnore]
        public int? TopicId { get; set; }

        [JsonIgnore]
        public virtual TestQuestion Question { get; set; }
        public virtual Topic Topic { get; set; }
    }
}
