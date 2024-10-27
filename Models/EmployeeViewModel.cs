using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace plan02.Models
{
    public class EmployeeViewModel
    {
        public string Name { get; set; }
        public Dictionary<DateTime, string> Attendance { get; set; }
    }
}
