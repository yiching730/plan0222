using System;
using System.Collections.Generic;

#nullable disable

namespace plan02.Models
{
    public partial class AEmployee
    {
        public int AId { get; set; }
        public string UserName { get; set; }
        public string AName { get; set; }
        public string AGender { get; set; }
        public string AMail { get; set; }
        public int? ASalary { get; set; }
        public DateTime? AEmploymentDate { get; set; }
    }
}
