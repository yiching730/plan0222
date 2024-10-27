using System;
using System.Collections.Generic;

#nullable disable

namespace plan02.Models
{
    public partial class TEmployee
    {
        public string FEmpId { get; set; }
        public string FName { get; set; }
        public string FGender { get; set; }
        public int? FSalary { get; set; }
        public string FEmploymentDate { get; set; }
    }
}
