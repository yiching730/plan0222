using System;
using System.Collections.Generic;

#nullable disable

namespace plan02.Models
{
    public partial class BUser
    {
        public uint UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public byte[] StoredSalt { get; set; }
    }
}
