using System;
using System.Collections.Generic;

#nullable disable

namespace plan02.Models
{
    public partial class Assessment
    {
        public int Aid { get; set; }
        public string Identity { get; set; }
        public string AssessmentYear { get; set; }
        public string AssessmentIndex { get; set; }
        public string CreateTime { get; set; }
        public string LastChangeName { get; set; }
        public string LastChangeTime { get; set; }
    }
}
