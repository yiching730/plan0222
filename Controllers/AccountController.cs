//using plan02.Services;
//using Microsoft.AspNetCore.Authentication;
//using Microsoft.AspNetCore.Authentication.Cookies;
//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Security.Claims;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Authorization;
//using plan02.BLL;
//using Microsoft.AspNetCore.Http;
//using System.IO;
//using plan02.Models;


//namespace plan02.Controllers
//{
//    public class AccountController : Controller
//    {

//        private readonly IMyAuthService authService;

//        public string capt { get; private set; }

//        public AccountController(IMyAuthService authService)
//        {
//            this.authService = authService;
//        }


//        public IActionResult Index()
//        {
//            return View();
//        }
//        public IActionResult Login(string returnUrl)
//        {
//            ViewBag.ReturnUrl = returnUrl;
//            return View();
//        }

//        [HttpPost("login")]
//        public async Task<IActionResult> Login(string username, string password, string returnUrl, string capt)
//        {

//            var user = await authService.Validate(username, password);
//            if (user)
//            {
//                var claims = new List<Claim> { new Claim(ClaimTypes.Name, username) };

//                if (username == "admin")
//                {
//                    claims.Add(new Claim(ClaimTypes.Role, "admin"));
//                }

//                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
//                var principal = new ClaimsPrincipal(identity);
//                await HttpContext.SignInAsync(principal);

//                if (ModelState.IsValid)
//                {

//                    Validate Captcha Code
//                    if (!Captcha.ValidateCaptchaCode(capt, HttpContext))
//                    {
//                        TempData["Message"] = "驗證碼失敗";
//                        return View(nameof(Login));
//                    }
//                    TempData["Message"] = "驗證碼成功";
//                }


//                if (!string.IsNullOrEmpty(returnUrl))
//                {
//                    return Redirect(returnUrl);
//                }
//                else
//                {
//                    return RedirectToAction("Index", "Home");
//                }
//            }
//            TempData["Message"] = "Login failed.";
//            return View(nameof(Login));
//        }


//        Yiching adding captcha 1
//        [Route("get-captcha-image")]
//        public IActionResult GetCaptchaImage()
//        {
//            int width = 100;
//            int height = 36;
//            var captchaCode = Captcha.GenerateCaptchaCode();
//            var result = Captcha.GenerateCaptchaImage(width, height, captchaCode);
//            HttpContext.Session.SetString("CaptchaCode", result.CaptchaCode);
//            Stream s = new MemoryStream(result.CaptchaByteData);
//            return new FileStreamResult(s, "image/png");
//        }
//        Yiching adding captcha 1

//        [Authorize]
//        public async Task<IActionResult> Logout()
//        {
//            await HttpContext.SignOutAsync();
//            return View(nameof(Logout));
//        }
//        public IActionResult AccessDenied()
//        {
//            return View();
//        }
//    }
//}



//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using plan02.Services;
//using Microsoft.AspNetCore.Authentication;
//using Microsoft.AspNetCore.Authentication.Cookies;
//using System.Security.Claims;

//namespace plan02.Controllers
//{
//    public class AccountController : Controller
//    {

//        public IActionResult Index()
//        {
//            return View();
//        }

//        public IActionResult Login(string returnUrl)
//        {
//            ViewBag.ReturnUrl = returnUrl;
//            return View();
//        }
//        [HttpPost("login")]
//        public async Task<IActionResult> Login(string username, string password, string returnUrl)
//        {
//            var user = await authService.Validate(username, password);
//            if (user)
//            {
//                var claims = new List<Claim> { new Claim(ClaimTypes.Name, username) };
//                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
//                var principal = new ClaimsPrincipal(identity);
//                await HttpContext.SignInAsync(principal);
//                if (!string.IsNullOrEmpty(returnUrl))
//                {
//                    return Redirect(returnUrl);
//                }
//                else
//                {
//                    return RedirectToAction("Index", "Home");
//                }
//            }
//            TempData["Message"] = "Login failed.";
//            return View(nameof(Login));
//            }
//        }
//}
