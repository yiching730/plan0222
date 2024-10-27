using System;
using System.Collections.Generic;

#nullable disable

namespace plan02.Models
{
    public partial class Persassessview
    {
        public int Aid { get; set; }
        public string Identity { get; set; }
        public string Unit { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public string AssessmentYear { get; set; }
        public string AssessmentIndex { get; set; }
        public string CreateTime { get; set; }
        public string LastChangeName { get; set; }
        public string LastChangeTime { get; set; }
    }
}
