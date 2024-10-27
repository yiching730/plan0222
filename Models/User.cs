using System;
using System.Collections.Generic;

#nullable disable

namespace plan02.Models
{
    public partial class User
    {
        public uint Uid { get; set; }
        public string Identity { get; set; }
        public string Unit { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        public string CreateTime { get; set; }
        public string ChangeTime { get; set; }
        public string ChangeName { get; set; }
        public string CaptchaCode { get; set; }
        public byte[] CaptchaByteData { get; set; }
    }
}
