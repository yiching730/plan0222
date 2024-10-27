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
            //����r
            services.AddSingleton(HtmlEncoder.Create(UnicodeRanges.All));
            //����
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddMemoryCache();
            services.AddMvc();

            //�q�պAŪ���n�J�O�ɳ]�w
            double LoginExpireMinute = this.Configuration.GetValue<double>("LoginExpireMinute");
            //���U CookieAuthentication�AScheme����

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(option =>
            {
                //�γ\�n�q�պA��Ū���A�ۤv�r�u�M�w
                option.LoginPath = new PathString("/Home/Login");//�n�J��
                option.LogoutPath = new PathString("/Home/Logout");//�n�XAction

                ////�Τ᭶�����d�Ӥ[�A�n�J�O���A��Controller��Action�̥Τ�n�J�ɡA�]�i�H�]�w��
                //option.ExpireTimeSpan = TimeSpan.FromMinutes(LoginExpireMinute);//�S���w�]14��
                ////����w��ĳfalse�A�սc�z���n��|�n�Dcookie���ੵ�i�Ĵ��A�o�ɳ]false�ܦ�����O���ɶ�
                ////���p�G�A���Ȥ���������@���b�ϥΨt�Ϋo�e���Q�۰ʵn�X���ܡA�A�A�]��true(�M��z��policy�ЫȤᲤ�L�����ˬd) 
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
