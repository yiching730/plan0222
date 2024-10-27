using System;
using System.Collections.Generic;

#nullable disable

namespace plan02.Models
{
    public partial class SalaryAssessment
    {
        public int Said { get; set; }
        public string Identity { get; set; }
        public string SalarYear { get; set; }
        public string Salaryscale { get; set; }
        public string SalaryIndex { get; set; }
        public string AssessmentYear { get; set; }
        public string AssessmentIndex { get; set; }
        public string CreateTime { get; set; }
        public string LastChangeName { get; set; }
        public string LastChangeTime { get; set; }
    }
}
