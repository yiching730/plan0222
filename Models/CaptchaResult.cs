using System;
using System.Collections.Generic;

#nullable disable

namespace plan02.Models
{
    public partial class CaptchaResult
    {
        public string CaptchaCode { get; set; }
        public byte[] CaptchaByteData { get; set; }
        public string CaptchBase64Data { get; set; }
        public DateTime? Timestamp { get; set; }
    }
}
