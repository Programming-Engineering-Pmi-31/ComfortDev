using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Logic.Classes
{
    [DataContract(Name = "Topic")]
    public class Topic
    {
        [DataMember(Name = "title")]
        public string Title { get; set; }
    }
}
