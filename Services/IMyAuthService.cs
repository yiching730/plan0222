
using plan02.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace plan02.Services
{
    public interface IMyAuthService
    {
        Task<bool> Validate(string username, string password, byte[] storedsalt);
        Task<bool> Validate(string capt);
        
        //Task<bool> Validate(string code);
    }
}
