using System;
using System.Collections.Generic;

#nullable disable

namespace plan02.Models
{
    public partial class PlanTable
    {
        public uint Aid { get; set; }
        public string Bid { get; set; }
        public string Cid { get; set; }
        public string Teacher { get; set; }
        public string PlanName { get; set; }
        public string Assistant { get; set; }
        public string Degree { get; set; }
        public string Work { get; set; }
        public DateTime Workstart { get; set; }
        public DateTime Workend { get; set; }
        public int Salary { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
