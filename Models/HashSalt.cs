using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace plan02.Models
{
    public class HashSalt
    {
        public string Hash { get; set; }
        public byte[] Salt { get; set; }
    }
}
