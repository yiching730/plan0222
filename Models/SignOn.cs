using System;
using System.Collections.Generic;

#nullable disable

namespace plan02.Models
{
    public partial class SignOn
    {
        public uint Sid { get; set; }
        public DateTime? SignIn { get; set; }
        public DateTime? SignOut { get; set; }
        public string Remark { get; set; }
    }
}


//using System;
//using System.Collections.Generic;

//#nullable disable

//namespace plan02.Models
//{
//    public partial class SignOn
//    {
//        public uint Sid { get; set; }
//        public DateTime? SignIn { get; set; }
//        public DateTime? SignOut { get; set; }
//        public string Remark { get; set; }
//    }
//}
