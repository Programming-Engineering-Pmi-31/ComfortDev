using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ComfortDev.Common.Entities
{
    public partial class Topic
    {
        public Topic()
        {
            Tasks = new HashSet<Task>();
            TestAnswers = new HashSet<TestAnswer>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        [JsonIgnore]
        public string ImageSource { get; set; }

        [JsonIgnore]
        public virtual ICollection<Task> Tasks { get; set; }
        [JsonIgnore]
        public virtual ICollection<TestAnswer> TestAnswers { get; set; }
    }
}
