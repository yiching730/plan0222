using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using plan02.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using plan02.Services;
using plan02.BLL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using Rotativa.AspNetCore;


namespace plan02
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<PlannedStaffManagementContext>(options =>
            {
                options.UseMySql(Configuration.GetConnectionString("DefaultConnectionString"), MySqlServerVersion.AutoDetect(Configuration.GetConnectionString("DefaultConnectionString")));
            });
            services.AddControllersWithViews();


            // Yiching adding 2


            // oauth 2
            //中文字
            services.AddSingleton(HtmlEncoder.Create(UnicodeRanges.All));
            //驗證
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddMemoryCache();
            services.AddMvc();

            //從組態讀取登入逾時設定
            double LoginExpireMinute = this.Configuration.GetValue<double>("LoginExpireMinute");
            //註冊 CookieAuthentication，Scheme必填

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(option =>
            {
                //或許要從組態檔讀取，自己斟酌決定
                option.LoginPath = new PathString("/Home/Login");//登入頁
                option.LogoutPath = new PathString("/Home/Logout");//登出Action

                ////用戶頁面停留太久，登入逾期，或Controller的Action裡用戶登入時，也可以設定↓
                //option.ExpireTimeSpan = TimeSpan.FromMinutes(LoginExpireMinute);//沒給預設14天
                ////↓資安建議false，白箱弱掃軟體會要求cookie不能延展效期，這時設false變成絕對逾期時間
                ////↓如果你的客戶反應明明一直在使用系統卻容易被自動登出的話，你再設為true(然後弱掃policy請客戶略過此項檢查) 
                //option.SlidingExpiration = false;
            });

            
            services.AddScoped<IMyAuthService, MyAuthService>();



            // Yiching adding 2

            // Yiching adding 1 captcha
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(20);
                options.Cookie.HttpOnly = true;
            });
            // Yiching adding 1 captcha
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
           


            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication(); //Yiching adding login Authentication 1
            
            // Yiching adding 2 captcha
            app.UseSession();
            // Yiching adding 2 captcha
            app.UseAuthorization();



            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                //pattern: "{controller=home}/{action=sso2}/{id?}");
                //pattern: "{controller=Home}/{action=Login}/{id?}");                   
                //pattern: "{controller=Account}/{action=Login}/{id?}");
                pattern: "{controller=Home}/{action=Index}/{id?}");
                //Yiching adding 3
            });
            //RotativaConfiguration.Setup((Microsoft.AspNetCore.Hosting.IHostingEnvironment)env);
            RotativaConfiguration.Setup(env.WebRootPath, "Rotativa");

        }
    }
}




//using Microsoft.AspNetCore.Builder;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.HttpsPolicy;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Hosting;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.EntityFrameworkCore;
//using plan02.Models;
//using Microsoft.AspNetCore.Authentication.Cookies;
//using plan02.Services;
//using plan02.BLL;
//using Microsoft.AspNetCore.Http;

//namespace plan02
//{
//    public class Startup
//    {
//        public Startup(IConfiguration configuration)
//        {
//            Configuration = configuration;
//        }

//        public IConfiguration Configuration { get; }

//        // This method gets called by the runtime. Use this method to add services to the container.
//        public void ConfigureServices(IServiceCollection services)
//        {
//            services.AddDbContext<PlannedStaffManagementContext>(options =>
//            {
//                options.UseMySql(Configuration.GetConnectionString("DefaultConnectionString"), MySqlServerVersion.AutoDetect(Configuration.GetConnectionString("DefaultConnectionString")));
//            });
//            services.AddControllersWithViews();

//            // Yiching adding 2

//            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
//            .AddCookie(options =>
//            {
//                options.LoginPath = "/Account/Login";
//                options.AccessDeniedPath = "/Account/AccessDenied";
//            });
//            services.AddScoped<IMyAuthService, MyAuthService>();


//            // Yiching adding 2

//            // Yiching adding 1 captcha
//            services.AddSession(options =>
//            {
//                options.IdleTimeout = TimeSpan.FromMinutes(20);
//                options.Cookie.HttpOnly = true;
//            });
//            // Yiching adding 1 captcha
//        }

//        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
//        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
//        {
//            if (env.IsDevelopment())
//            {
//                app.UseDeveloperExceptionPage();
//            }
//            else
//            {
//                app.UseExceptionHandler("/Home/Error");
//                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//                app.UseHsts();
//            }
//            app.UseHttpsRedirection();
//            app.UseStaticFiles();

//            app.UseRouting();

//            // Yiching adding 2 captcha
//            app.UseSession();
//            // Yiching adding 2 captcha

//            app.UseAuthentication(); //Yiching adding login Authentication 1

//            app.UseAuthorization();


//            app.UseEndpoints(endpoints =>
//            {
//                endpoints.MapControllerRoute(
//                    name: "default",
//                //pattern: "{controller=Account}/{action=Login}/{id?}");
//                pattern: "{controller=Home}/{action=Index}/{id?}");
//                //Yiching adding 3
//            });
//        }
//    }
//}
