using plan02.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace plan02.Services
{
    public class MyAuthService : IMyAuthService
    {
        readonly PlannedStaffManagementContext context;

        public MyAuthService(PlannedStaffManagementContext context)
        {
            this.context = context;
        }

        public async Task<bool> Validate(string username, string password, byte[] storedsalt)
        {
            return (await context.BUsers.FirstOrDefaultAsync(u => u.UserName == username && u.Password == password && u.StoredSalt == storedsalt)) != null;
        }

        public async Task<bool> Validate(string capt)
        {
            return (await context.CaptchaResults.FirstOrDefaultAsync(u => u.CaptchaCode == capt)) != null;
        }



        //public async Task<bool> Validate(string code)
        //{
        //    return (await context.SingleSignOns.FirstOrDefaultAsync(u => u.Cn == code)) != null;
        //}

    }
}