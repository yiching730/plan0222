using System;
using System.Collections.Generic;

#nullable disable

namespace plan02.Models
{
    public partial class UserDatum
    {
        public uint UserId { get; set; }
        public string UserName { get; set; }
        public string IdNumber { get; set; }
        public string UName { get; set; }
        public string Unit { get; set; }
        public string Tel { get; set; }
        public string UGender { get; set; }
        public string USalary { get; set; }
        public DateTime UEmployeeTime { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
