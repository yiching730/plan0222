using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace plan02.Models
{
    public class RegisterViewModel
    {
        public string Email { get; set; }
        //密碼
        public string Password { get; set; }
        //確認密碼
        public string ConfirmedPassword { get; set; }
    }
}
