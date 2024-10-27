using System;
using System.Collections.Generic;

#nullable disable

namespace plan02.Models
{
    public partial class Transfer
    {
        public int Tid { get; set; }
        public string Identity { get; set; }
        public string TransferYear { get; set; }
        public string TransferIndex { get; set; }
        public string CreateTime { get; set; }
        public string LastChangeName { get; set; }
        public string LastChangeTime { get; set; }
    }
}
