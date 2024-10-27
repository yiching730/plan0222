using System;
using System.Collections.Generic;

#nullable disable

namespace plan02.Models
{
    public partial class LoginViewModel
    {
        public uint Lid { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
