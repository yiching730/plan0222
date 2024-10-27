using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using plan02.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Net;
using System.Collections.Specialized;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using plan02.Services;
using Microsoft.Extensions.Configuration;
using plan02.BLL;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;


namespace plan02.Controllers
{
    public class HomeController : Controller
    {
        public readonly ILogger<HomeController> _logger;
        private readonly PlannedStaffManagementContext _context;
        private readonly IMyAuthService authService;
        private readonly IConfiguration _config;
        

        public HomeController(ILogger<HomeController> logger, PlannedStaffManagementContext context, IConfiguration config, IMyAuthService authService)
        {
            _logger = logger;
            _context = context;
            _config = config;
            this.authService = authService;
          
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}

        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> Index(string code)
        {

            ViewData["code"] = code;
            string code_url = "http://163.17.100.48/oauth2ServerToken.do";
            string redirect_uri = "https://pgmt.ntus.edu.tw";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(code_url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";

            //必須透過ParseQueryString()來建立NameValueCollection物件，之後.ToString()才能轉換成queryString
            NameValueCollection postParams = System.Web.HttpUtility.ParseQueryString(string.Empty);
            postParams.Add("grant_type", "authorization_code");
            postParams.Add("client_id", "kjradojq");
            postParams.Add("client_secret", "4B59xDdw");
            postParams.Add("code", code);
            postParams.Add("redirect_uri", redirect_uri);

            //要發送的字串轉為byte[] 
            byte[] byteArray = Encoding.UTF8.GetBytes(postParams.ToString());
            using (Stream reqStream = request.GetRequestStream())
            {
                reqStream.Write(byteArray, 0, byteArray.Length);
            }
            //API回傳的字串
            string responseStr = "", token = "";
            //發出Request
            using (WebResponse response = request.GetResponse())
            {
                using (StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    responseStr = sr.ReadLine();
                    ViewData["responseStr"] = responseStr;

                }
            }
            if (responseStr.Length > 18)
            {
                token = responseStr.Trim().Substring(18, responseStr.Length - 21);
            }
            ViewData["token"] = token;
            var token_url = "http://163.17.100.48/oauth2ServerInfo.do";
            var httpRequest = (HttpWebRequest)WebRequest.Create(token_url);
            httpRequest.Method = "POST";
            httpRequest.Headers["Authorization"] = "Bearer " + token;
            httpRequest.ContentType = "";
            httpRequest.Headers["Content-Length"] = "0";

            var account = "";
            var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                account = streamReader.ReadToEnd();
            }
            //讀取Json寫法
            //JObject obj = (JObject)JsonConvert.DeserializeObject(responseStr); 
            //Console.WriteLine(Convert.ToString(obj["isExist"]));
            SingleSignOn SSO = JsonConvert.DeserializeObject<SingleSignOn>(account);
            ViewData["account"] = account;
            if (SSO == null)
            {
                return View();
            }
            ViewData["acc"] = SSO.Cn;
            TempData["Message"] = SSO.sAMAccountName;
            //ViewData["LDB"] = SSO.LoginDisable;
            //Console.WriteLine(httpResponse.StatusCode);
            //if (SSO.LoginDisable == null || SSO.LoginDisable == "false")
            //{
            //    return View();
            //}
            if (SSO.sAMAccountName != null && !SSO.sAMAccountName.Equals(""))
            {
                HttpContext.Session.SetString("acc", SSO.sAMAccountName);


                Claim[] claims = new[] { new Claim(ClaimTypes.Name, SSO.sAMAccountName) };
                ClaimsIdentity claimsEmployeeID = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);//Scheme必填
                ClaimsPrincipal principal = new ClaimsPrincipal(claimsEmployeeID);
                //string Account = HttpContext.User.Claims.FirstOrDefault(m => m.Type == ClaimTypes.Name).Value;

                //從組態讀取登入逾時設定
                double loginExpireMinute = this._config.GetValue<double>("LoginExpireMinute");
                //執行登入，相當於以前的FormsAuthentication.SetAuthCookie()
                await HttpContext.SignInAsync(principal,
                    new AuthenticationProperties()
                    {
                        IsPersistent = false, //IsPersistent = false：瀏覽器關閉立馬登出；IsPersistent = true 就變成常見的Remember Me功能
                                              //用戶頁面停留太久，逾期時間，在此設定的話會覆蓋Startup.cs裡的逾期設定
                        /* ExpiresUtc = DateTime.UtcNow.AddMinutes(loginExpireMinute) */
                    });
            }
            else
            {
                TempData["Alert"] = ("error");
                return View();
            }

            return RedirectToAction("Index", "Home");
        }

       

        //        //[HttpGet]
        //        //public async Task<IActionResult> Index(string code)
        //        //{

        //        //    ViewData["code"] = code;
        //        //    string code_url = "http://163.17.100.48/oauth2ServerToken.do";
        //        //    string redirect_uri = "https://pgmt.ntus.edu.tw/";

        //        //    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(code_url);
        //        //    request.Method = "POST";
        //        //    request.ContentType = "application/x-www-form-urlencoded";

        //        //    //必須透過ParseQueryString()來建立NameValueCollection物件，之後.ToString()才能轉換成queryString
        //        //    NameValueCollection postParams = System.Web.HttpUtility.ParseQueryString(string.Empty);
        //        //    postParams.Add("grant_type", "authorization_code");
        //        //    postParams.Add("client_id", "kjradojq");
        //        //    postParams.Add("client_secret", "4B59xDdw");
        //        //    postParams.Add("code", code);
        //        //    postParams.Add("redirect_uri", redirect_uri);

        //        //    //要發送的字串轉為byte[] 
        //        //    byte[] byteArray = Encoding.UTF8.GetBytes(postParams.ToString());
        //        //    using (Stream reqStream = request.GetRequestStream())
        //        //    {
        //        //        reqStream.Write(byteArray, 0, byteArray.Length);
        //        //    }
        //        //    //API回傳的字串
        //        //    string responseStr = "", token = "";
        //        //    //發出Request
        //        //    using (WebResponse response = request.GetResponse())
        //        //    {
        //        //        using (StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
        //        //        {
        //        //            responseStr = sr.ReadLine();

        //        //        }
        //        //    }
        //        //    if (responseStr.Length > 18)
        //        //    {
        //        //        token = responseStr.Trim().Substring(18, responseStr.Length - 21);
        //        //    }
        //        //    ViewData["token"] = token;
        //        //    var token_url = "http://163.17.100.48/oauth2ServerInfo.do";
        //        //    var httpRequest = (HttpWebRequest)WebRequest.Create(token_url);
        //        //    httpRequest.Method = "POST";
        //        //    httpRequest.Headers["Authorization"] = "Bearer " + token;
        //        //    httpRequest.ContentType = "";
        //        //    httpRequest.Headers["Content-Length"] = "0";

        //        //    var account = "";
        //        //    var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
        //        //    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
        //        //    {
        //        //        account = streamReader.ReadToEnd();
        //        //        TempData["Message"] = code;
        //        //        //TempData["Message"] = user;
        //        //    }
        //        //    //讀取Json寫法
        //        //    //JObject obj = (JObject)JsonConvert.DeserializeObject(responseStr); 
        //        //    //Console.WriteLine(Convert.ToString(obj["isExist"]));
        //        //    SingleSignOn SSO = JsonConvert.DeserializeObject<SingleSignOn>(account);
        //            if (SSO == null)
        //            {
        //                return View();
        //    }
        //    ViewData["acc"] = SSO.Cn;
        //            TempData["Message"] = SSO.UserName;
        //            //ViewData["LDB"] = SSO.LoginDisable;
        //            //Console.WriteLine(httpResponse.StatusCode);
        //            // if (SSO.LoginDisable == null || SSO.LoginDisable == "false")
        //            // {
        //            //     return View();
        //            // }
        //            if (SSO.UserName != null && !SSO.UserName.Equals(""))
        //            {
        //                //var Cncount = (from m in _context.Users
        //                           //    select m).Where(s => s.Account == SSO.Cn).Count();
        //               // if (Cncount == 0)
        //               // {
        //               //     TempData["Alert"] = ("你無權使用此系統！請聯絡系統管理員");
        //               //     return RedirectToAction("Login", "Home");
        //               // }
        //                //var org = (from m in _context.Users
        //                //           where m.Account == SSO.Cn
        //                //           select m.Account).FirstOrDefault();
        //                //if (org == "admin")
        //                //{
        //                //    HttpContext.Session.SetString("admin", org);
        //                //}
        //                //if (org == "user")
        //                //{
        //                //    HttpContext.Session.SetString("user", org);
        //                //}
        //                //if (org == "manager")
        //                //{
        //                //    HttpContext.Session.SetString("manager", org);
        //                //}
        //                HttpContext.Session.SetString("acc", SSO.UserName);
        //                //var user = (from m in _context.Users
        //                //            orderby m.Uid
        //                //            where m.Account == SSO.Cn
        //                //            select m).FirstOrDefault();
        //                //user.ChangeTime = DateTime.Now.ToString();
        //                //_context.Update(user);
        //                //await _context.SaveChangesAsync();

        //                Claim[] claims = new[] { new Claim(ClaimTypes.Name, SSO.UserName) };
        //    ClaimsIdentity claimsEmployeeID = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);//Scheme必填
        //    ClaimsPrincipal principal = new ClaimsPrincipal(claimsEmployeeID);
        //    //string Account = HttpContext.User.Claims.FirstOrDefault(m => m.Type == ClaimTypes.Name).Value;

        //    //從組態讀取登入逾時設定
        //    double loginExpireMinute = this._config.GetValue<double>("LoginExpireMinute");
        //    //執行登入，相當於以前的FormsAuthentication.SetAuthCookie()
        //    await HttpContext.SignInAsync(principal,
        //                    new AuthenticationProperties()
        //    {
        //        IsPersistent = false, //IsPersistent = false：瀏覽器關閉立馬登出；IsPersistent = true 就變成常見的Remember Me功能
        //                                              //用戶頁面停留太久，逾期時間，在此設定的話會覆蓋Startup.cs裡的逾期設定
        //                        /* ExpiresUtc = DateTime.UtcNow.AddMinutes(loginExpireMinute) */
        //                    });
        //            }
        //            else
        //{

        //    TempData["Alert"] = ("error");
        //    return View();
        //}
        //TempData["Message"] = account + "測試";
        //return RedirectToAction("Index", "Home");
        //        }


        public IActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }



        //修改版
        [HttpPost("login")]
        public async Task<IActionResult> Login(byte[] StoredSalt, string Password, string returnUrl, string capt, AUser loginUser, string Aaccount)
        {

            var us = _context.AUsers.FirstOrDefault(u => u.Aaccount == loginUser.Aaccount);
            if (Aaccount == null)
            {
                TempData["Message"] = "帳號不可為空白";
                return View(nameof(Login));
            }
            if (us == null)
            {
                TempData["Message"] = "無此帳號";
                return View(nameof(Login));
            }
            //var uss = _context.BUsers.FirstOrDefault(u => u.Password == loginUser.Password);
            if (loginUser.Apassword == null)
            {
                TempData["Message"] = "密碼不可為空白";
                return View(nameof(Login));
            }

            //ViewData["us.StoredSalt"] = us.StoredSalt;
            //ViewData["us.Password"] = us.Password;
            //if (us.StoredSalt == null || us.Password == null)
            //{
            //    TempData["Message"] = "帳密錯誤";
            //    return View(nameof(Login));
            //}
            var isPasswordMatched = VerifyPassword(loginUser.Apassword, us.StoredSalt, us.Apassword);
            //ViewData["loginUser.Password"] = loginUser.Password;
            //ViewData["us.StoredSalt"] = us.StoredSalt;
            //ViewData["us.Password"] = us.Password;

            if (isPasswordMatched)
            {
                //var claims = new List<Claim> { new Claim(ClaimTypes.Name, loginUser.UserName) };
                //var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                //var principal = new ClaimsPrincipal(identity);
                //await HttpContext.SignInAsync(principal);
                Claim[] claims = new[] { new Claim(ClaimTypes.Name, loginUser.Aaccount) };
                ClaimsIdentity claimsEmployeeID = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);//Scheme必填
                ClaimsPrincipal principal = new ClaimsPrincipal(claimsEmployeeID);
                await HttpContext.SignInAsync(principal);
                if (ModelState.IsValid)
                {

                    // Validate Captcha Code
                    if (!Captcha.ValidateCaptchaCode(capt, HttpContext))
                    {
                        TempData["Message"] = "驗證碼失敗";
                        return View(nameof(Login));
                    }
                    //TempData["Message"] = "驗證碼成功";
                }

                if (!string.IsNullOrEmpty(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    HttpContext.Session.SetString("Aaccount", loginUser.Aaccount);
                    return RedirectToAction("Index", "Home");
                }
            }
            TempData["Message"] = "登入失敗，帳號或密碼錯誤";
            return View(nameof(Login));
        }
        //修改版



        //// 正確版
        //[HttpPost("login")]
        //public async Task<IActionResult> Login(string username, string password, string returnUrl, string capt, string code)
        //{

        //        //if (code != null)
        //        //{
        //        //var claims = new List<Claim> { new Claim(ClaimTypes.Name, code) };
        //        //var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        //        //var principal = new ClaimsPrincipal(identity);
        //        //await HttpContext.SignInAsync(principal);
        //        //return RedirectToAction("Index", "Home");
        //        //}

        //    var user = await authService.Validate(username, password);
        //    if (user)
        //    {
        //        var claims = new List<Claim> { new Claim(ClaimTypes.Name, username) };

        //        if (username == "admin")
        //        {
        //            claims.Add(new Claim(ClaimTypes.Role, "admin"));
        //        }

        //        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        //        var principal = new ClaimsPrincipal(identity);
        //        await HttpContext.SignInAsync(principal);

        //        if (ModelState.IsValid)
        //        {

        //            // Validate Captcha Code
        //            if (!Captcha.ValidateCaptchaCode(capt, HttpContext))
        //            {
        //                TempData["Message"] = "驗證碼失敗";
        //                return View(nameof(Login));
        //            }
        //            //TempData["Message"] = "驗證碼成功";
        //        }


        //        if (!string.IsNullOrEmpty(returnUrl))
        //        {
        //            return Redirect(returnUrl);
        //        }
        //        else
        //        {
        //            return RedirectToAction("Index", "Home");
        //        }
        //    }
        //    TempData["Message"] = "登入失敗.";
        //    return View(nameof(Login));
        //}
        //// 正確版

        // Yiching adding captcha 1
        [Route("get-captcha-image")]
        public IActionResult GetCaptchaImage()
        {
            int width = 100;
            int height = 36;
            var captchaCode = Captcha.GenerateCaptchaCode();
            var result = Captcha.GenerateCaptchaImage(width, height, captchaCode);
            HttpContext.Session.SetString("CaptchaCode", result.CaptchaCode);
            Stream s = new MemoryStream(result.CaptchaByteData);
            return new FileStreamResult(s, "image/png");
        }
        // Yiching adding captcha 1

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return View(nameof(Logout));
        }
        public IActionResult AccessDenied()
        {
            return View();
        }

        public IActionResult Privacy()
        {          
            return View();
        }

       


        [Authorize(Roles = "admin")]
        public IActionResult AdminStaff()
        {
            TempData["Message"] = "管理者才有權限";
            return View();
        }


        public IActionResult Register()
        {
            return View();
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Register(BUser uc)
        //{
        //    _context.Add(uc);
        //    _context.SaveChanges();
        //    ViewBag.message = "帳號 " + uc.UserName + " 新增成功，請登入";
        //    return View();
        //}


        [HttpPost]
        public IActionResult Register(AUser user)
        {
            if (_context.AUsers.Any(u => u.Aaccount == user.Aaccount))
            {
                ViewBag.message = "帳號 " + user.Aaccount + "已有人註冊";
                return View();
            }
            if (_context.AUsers.Any(u => u.Aidentity == user.Aidentity))
            {
                ViewBag.message = "身分證字號 " + user.Aidentity + "已有人註冊";
                return View();
            }
            if(user.Aaccount != "test3" )
            {
                ViewBag.message = "只有管理者才可註冊 ";
                return View();
            }
            var hashsalt = EncryptPassword(user.Apassword);
            user.Apassword = hashsalt.Hash;
            user.StoredSalt = hashsalt.Salt;
            _context.AUsers.Add(user);
            _context.SaveChanges();
            ViewBag.message = "帳號 " + user.Aaccount + " 新增成功，請登入";
            return View();
        }


        //正確版1，無加身分證字號
        //[HttpPost]
        //public IActionResult Register(BUser user)
        //{
        //    if (_context.BUsers.Any(u => u.UserName == user.UserName))
        //    {
        //        ViewBag.message = "帳號 " + user.UserName + "已有人註冊";
        //        return View();
        //    }
        //    var hashsalt = EncryptPassword(user.Password);
        //    user.Password = hashsalt.Hash;
        //    user.StoredSalt = hashsalt.Salt;
        //    _context.BUsers.Add(user);
        //    _context.SaveChanges();
        //    ViewBag.message = "帳號 " + user.UserName + " 新增成功，請登入";
        //    return View();
        //}

        public HashSalt EncryptPassword(string password)
        {
            byte[] salt = new byte[128 / 8]; // Generate a 128-bit salt using a secure PRNG
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            string encryptedPassw = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8
            ));
            return new HashSalt { Hash = encryptedPassw, Salt = salt };
        }

        public bool VerifyPassword(string enteredPassword, byte[] salt, string storedPassword)
        {
            string encryptedPassw = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: enteredPassword,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8
            ));
            return encryptedPassw == storedPassword;

        }

        public IActionResult ResetPassword()
        {
            return View();
        }

        // GET
        //public async Task<IActionResult> ResetPassword(uint? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var auser = await _context.AUsers.FindAsync(id);
        //    if (auser == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(auser);
        //}

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(uint id, [Bind("Aid,Aidentity,Aaccount,Apassword")] AUser auser)
        {
            if (id != auser.Aid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
             
                try
                {
                    _context.Update(auser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AUserExists(auser.Aid))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                ViewBag.message = "重設密碼失敗，無此帳號";
                return RedirectToAction("ResetPassword", "Home");
            }
            return View(auser);
        }

        private bool AUserExists(uint id)
        {
            //throw new NotImplementedException();
            return _context.AUsers.Any(e => e.Aid == id);
        }

        public IActionResult Upload2()
        {
            SelectList customers = new SelectList(_context.Customers.ToList(), "CustomerId", "Name");
            return View(customers);
        }

        [HttpPost]
        public IActionResult Upload2(string customerId, string customerName)
        {
            ViewBag.Message = "Name: " + customerName;
            ViewBag.Message += "\\nID: " + customerId;
            return View();
        }

        
    


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Logging;
//using plan02.Models;
//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Authorization;
//using System.Net;
//using System.Collections.Specialized;
//using System.Text;
//using System.IO;
//using Newtonsoft.Json;
//using Microsoft.AspNetCore.Http;
//using System.Security.Claims;
//using Microsoft.AspNetCore.Authentication.Cookies;
//using Microsoft.AspNetCore.Authentication;
//using plan02.Services;
//using Microsoft.Extensions.Configuration;

//namespace plan02.Controllers
//{
//    public class HomeController : Controller
//    {
//        public readonly ILogger<HomeController> _logger;
//        private readonly PlannedStaffManagementContext _context;

//        private readonly IConfiguration _config;

//        public HomeController(ILogger<HomeController> logger, PlannedStaffManagementContext context, IConfiguration config)
//        {
//            _logger = logger;
//            _context = context;

//            _config = config;
//        }

//        //public IActionResult Index()
//        //{
//        //    return View();
//        //}


//        [HttpGet]
//        public async Task<IActionResult> Index(string code)
//        {

//            ViewData["code"] = code;
//            string code_url = "http://163.17.100.48/oauth2ServerToken.do";
//            string redirect_uri = "https://pgmt.ntus.edu.tw/";

//            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(code_url);
//            request.Method = "POST";
//            request.ContentType = "application/x-www-form-urlencoded";

//            //必須透過ParseQueryString()來建立NameValueCollection物件，之後.ToString()才能轉換成queryString
//            NameValueCollection postParams = System.Web.HttpUtility.ParseQueryString(string.Empty);
//            postParams.Add("grant_type", "authorization_code");
//            postParams.Add("client_id", "kjradojq");
//            postParams.Add("client_secret", "4B59xDdw");
//            postParams.Add("code", code);
//            postParams.Add("redirect_uri", redirect_uri);

//            //要發送的字串轉為byte[] 
//            byte[] byteArray = Encoding.UTF8.GetBytes(postParams.ToString());
//            using (Stream reqStream = request.GetRequestStream())
//            {
//                reqStream.Write(byteArray, 0, byteArray.Length);
//            }
//            //API回傳的字串
//            string responseStr = "", token = "";
//            //發出Request
//            using (WebResponse response = request.GetResponse())
//            {
//                using (StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
//                {
//                    responseStr = sr.ReadLine();
//                }
//            }
//            if (responseStr.Length > 18)
//            {
//                token = responseStr.Trim().Substring(18, responseStr.Length - 21);
//            }
//            ViewData["token"] = token;
//            var token_url = "http://163.17.100.48/oauth2ServerInfo.do";
//            var httpRequest = (HttpWebRequest)WebRequest.Create(token_url);
//            httpRequest.Method = "POST";
//            httpRequest.Headers["Authorization"] = "Bearer " + token;
//            httpRequest.ContentType = "";
//            httpRequest.Headers["Content-Length"] = "0";

//            var account = "";
//            var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
//            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
//            {
//                account = streamReader.ReadToEnd();
//            }
//            //讀取Json寫法
//            //JObject obj = (JObject)JsonConvert.DeserializeObject(responseStr); 
//            //Console.WriteLine(Convert.ToString(obj["isExist"]));
//            SingleSignOn SSO = JsonConvert.DeserializeObject<SingleSignOn>(account);
//            if (SSO == null)
//            {
//                TempData["Alert"] = ("你無權使用此系統！請聯絡系統管理員");
//                return View();
//            }
//            ViewData["acc"] = SSO.Cn;
//            //ViewData["LDB"] = SSO.LoginDisable;
//            //Console.WriteLine(httpResponse.StatusCode);
//            if (SSO.LoginDisable == null || SSO.LoginDisable == "false")
//            {
//                TempData["Alert"] = ("你無權使用此系統！請聯絡系統管理員");
//                return View();
//            }
//            if (SSO.Cn != null && !SSO.Cn.Equals(""))
//            {
//                var Cncount = (from m in _context.BUsers
//                               select m).Where(s => s.UserName == SSO.Cn).Count();
//                //if (Cncount == 0)
//                //{
//                //    TempData["Alert"] = ("你無權使用此系統！請聯絡系統管理員");
//                //    return RedirectToAction("Login", "LoginViewModels");
//                //}
//                //var org = (from m in _context.LoginViewModels

//                //           where m.Username == SSO.Cn
//                //           select m.Username).FirstOrDefault();
//                //if (org == "admin")
//                //{
//                //    HttpContext.Session.SetString("admin", org);
//                //}
//                //if (org == "user")
//                //{
//                //    HttpContext.Session.SetString("user", org);
//                //}
//                //if (org == "manager")
//                //{
//                //    HttpContext.Session.SetString("manager", org);
//                //}
//                HttpContext.Session.SetString("acc", SSO.Cn);
//                var user = (from m in _context.BUsers
//                            orderby m.UserId
//                            where m.UserName == SSO.Cn
//                            select m).FirstOrDefault();
//                //user.ChangeTime = DateTime.Now.ToString();
//                _context.Update(user);
//                await _context.SaveChangesAsync();

//                Claim[] claims = new[] { new Claim(ClaimTypes.Name, SSO.Cn) };
//                ClaimsIdentity claimsEmployeeID = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);//Scheme必填
//                ClaimsPrincipal principal = new ClaimsPrincipal(claimsEmployeeID);
//                //string Account = HttpContext.User.Claims.FirstOrDefault(m => m.Type == ClaimTypes.Name).Value;

//                //從組態讀取登入逾時設定
//                double loginExpireMinute = this._config.GetValue<double>("LoginExpireMinute");
//                //執行登入，相當於以前的FormsAuthentication.SetAuthCookie()
//                await HttpContext.SignInAsync(principal,
//                    new AuthenticationProperties()
//                    {
//                        IsPersistent = false, //IsPersistent = false：瀏覽器關閉立馬登出；IsPersistent = true 就變成常見的Remember Me功能
//                                              //用戶頁面停留太久，逾期時間，在此設定的話會覆蓋Startup.cs裡的逾期設定
//                        /* ExpiresUtc = DateTime.UtcNow.AddMinutes(loginExpireMinute) */
//                    });

//            }
//            else
//            {

//                TempData["Alert"] = ("error");
//                return View();
//            }
//            return RedirectToAction("Index", "Home");
//            //return Redirect(returnUrl);
//        }

//        [Authorize]
//        public async Task<IActionResult> Logout()
//        {
//            await HttpContext.SignOutAsync();
//            return View(nameof(Logout));
//        }


//        public IActionResult Privacy()
//        {
//            return View();
//        }

//        [Authorize(Roles = "admin")]
//        public IActionResult AdminStaff()
//        {
//            return View();
//        }

//        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
//        public IActionResult Error()
//        {
//            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
//        }
//    }
//}


//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Logging;
//using plan02.Models;
//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Authorization;


//namespace plan02.Controllers
//{
//    public class HomeController : Controller
//    {
//        public readonly ILogger<HomeController> _logger;

//        public HomeController(ILogger<HomeController> logger)
//        {
//            _logger = logger;
//        }

//        public IActionResult Index()
//        {
//            return View();
//        }

//        public IActionResult Privacy()
//        {
//            return View();
//        }

//        [Authorize(Roles = "admin")]
//        public IActionResult AdminStaff()
//        {
//            return View();
//        }

//        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
//        public IActionResult Error()
//        {
//            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
//        }
//    }
//}

//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Logging;
//using plan02.Models;
//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Authorization;

//namespace plan02.Controllers
//{
//    public class HomeController : Controller
//    {
//        public readonly ILogger<HomeController> _logger;

//        public HomeController(ILogger<HomeController> logger)
//        {
//            _logger = logger;
//        }

//        public IActionResult Index()
//        {
//            return View();
//        }

//        public IActionResult Privacy()
//        {
//            return View();
//        }

//        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
//        public IActionResult Error()
//        {
//            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
//        }
//    }
//}













