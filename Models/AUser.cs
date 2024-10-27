using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace plan02.Models
{
    public partial class AUser
    {
        public uint Aid { get; set; }
        [MinLength(10, ErrorMessage = "身分證字號長度為10字"), MaxLength(10,ErrorMessage="身分證字號長度為10字")]
        public string Aidentity { get; set; }
        public string Aaccount { get; set; }
        public string Apassword { get; set; }
        public byte[] StoredSalt { get; set; }
    }
}
