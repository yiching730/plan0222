using System;
using System.Collections.Generic;

#nullable disable

namespace plan02.Models
{
    public partial class Perseduiview
    {
        public int Eid { get; set; }
        public string Identity { get; set; }
        public string Unit { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public string EducationYear { get; set; }
        public string EducationName { get; set; }
        public string EducationDepartment { get; set; }
        public string EducationLevel { get; set; }
        public string EducationStart { get; set; }
        public string EducationEnd { get; set; }
        public string CreateTime { get; set; }
        public string LastChangeName { get; set; }
        public string LastChangeTime { get; set; }
    }
}
