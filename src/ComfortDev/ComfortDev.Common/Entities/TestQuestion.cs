using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ComfortDev.Common.Entities
{
    public partial class TestQuestion
    {
        public TestQuestion()
        {
            TestAnswers = new HashSet<TestAnswer>();
        }

        [JsonIgnore]
        public int Id { get; set; }
        public string Question { get; set; }

        public virtual ICollection<TestAnswer> TestAnswers { get; set; }
    }
}
