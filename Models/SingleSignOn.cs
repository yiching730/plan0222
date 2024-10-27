using System;
using System.Collections.Generic;

#nullable disable

namespace plan02.Models
{
    public partial class SingleSignOn
    {
        public string Cn { get; set; }
        public string sAMAccountName { get; set; }
        public string LoginDisable { get; set; }
        public string LastChangeTime { get; set; }
    }
}
