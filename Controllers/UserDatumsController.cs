using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using plan02.Models;


namespace plan02.Controllers
{
    public class UserDatumsController : Controller
    {
        private readonly PlannedStaffManagementContext _context;

        public UserDatumsController(PlannedStaffManagementContext context)
        {
            _context = context;
        }

        // GET: UserDatums
        public async Task<IActionResult> Index()
        {
            //var soe = await _context.UserData.ToListAsync();
            //soe.Where(u => u.Username == userid);
            //var soe = await _context.UserData.AsQueryable().Where(u => u.UserName == username).ToListAsync();
            String Bname = HttpContext.User.Claims.FirstOrDefault(m => m.Type == ClaimTypes.Name).Value;
            var soe = await _context.UserData.Where(u => u.UserName == Bname).ToListAsync();
            ViewData["Bname"] = Bname;
            //soe.Where(u => u.UserName == userid);

            //禮拜日新加
            //string abc2 = "專任助理";
            //var abc = _context.UserData.Where(u => u.Unit == abc2);
            //ViewData["abc2"] = abc2;
            //if (abc.Count() > 0)
            //{
            //    ViewBag.message = "此專任助理為一筆以上資料";
            //    return View(soe);
            //}
            return View(soe);
        }

        // GET: UserDatums/Details/5
        public async Task<IActionResult> Details(uint? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userDatum = await _context.UserData
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (userDatum == null)
            {
                return NotFound();
            }

            return View(userDatum);
        }

        // GET: UserDatums/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UserDatums/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,IdNumber,UName,Unit,Tel,UGender,USalary,UEmployeeTime,CreateTime")] UserDatum userDatum)
        {
            // w1 adding
            ViewBag.Employees = new List<SelectListItem>
            {
            new SelectListItem {Text = "專任助理", Value = "1"},
            new SelectListItem {Text = "兼任助理", Value = "2"}
            };
            if (ModelState.IsValid)
            {
                _context.Add(userDatum);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userDatum);
        }

        //public IActionResult Page1()
        //{
        //    return View();
        //}

        //public async Task<IActionResult> Page1()
        //{
        //}

            // GET: UserDatums/Edit/5
            public async Task<IActionResult> Edit(uint? id)
        {
            // w1 adding
            ViewBag.Employees = new List<SelectListItem>
            {
            new SelectListItem {Text = "專任助理", Value = "1"},
            new SelectListItem {Text = "兼任助理", Value = "2"}
            };

            if (id == null)
            {
                return NotFound();
            }

            var userDatum = await _context.UserData.FindAsync(id);
            if (userDatum == null)
            {
                return NotFound();
            }
            return View(userDatum);
        }

        // POST: UserDatums/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(uint id, [Bind("UserId,IdNumber,UName,Unit,Tel,UGender,USalary,UEmployeeTime,CreateTime")] UserDatum userDatum)
        {
            if (id != userDatum.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userDatum);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserDatumExists(userDatum.UserId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(userDatum);
        }

        // GET: UserDatums/Delete/5
        public async Task<IActionResult> Delete(uint? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userDatum = await _context.UserData
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (userDatum == null)
            {
                return NotFound();
            }

            return View(userDatum);
        }

        // POST: UserDatums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(uint id)
        {
            var userDatum = await _context.UserData.FindAsync(id);
            _context.UserData.Remove(userDatum);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserDatumExists(uint id)
        {
            return _context.UserData.Any(e => e.UserId == id);
        }
    }
}


//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Security.Claims;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.EntityFrameworkCore;
//using plan02.Models;


//namespace plan02.Controllers
//{
//    public class UserDatumsController : Controller
//    {
//        private readonly PlannedStaffManagementContext _context;

//        public UserDatumsController(PlannedStaffManagementContext context)
//        {
//            _context = context;
//        }

//        // GET: UserDatums
//        public async Task<IActionResult> Index()
//        {
//            //var soe = await _context.UserData.ToListAsync();
//            //soe.Where(u => u.Username == userid);
//            //var soe = await _context.UserData.AsQueryable().Where(u => u.UserName == username).ToListAsync();
//            String Bname = HttpContext.User.Claims.FirstOrDefault(m => m.Type == ClaimTypes.Name).Value;
//            var soe = await _context.UserData.Where(u => u.UserName == Bname).ToListAsync();
//            ViewData["Bname"] = Bname;
//            //soe.Where(u => u.UserName == userid);

//            //禮拜日新加
//            //string abc2 = "專任助理";
//            //var abc = _context.UserData.Where(u => u.Unit == abc2);
//            //ViewData["abc2"] = abc2;
//            //if (abc.Count() > 0)
//            //{
//            //    ViewBag.message = "此專任助理為一筆以上資料";
//            //    return View(soe);
//            //}
//            return View(soe);
//        }

//        // GET: UserDatums/Details/5
//        public async Task<IActionResult> Details(uint? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var userDatum = await _context.UserData
//                .FirstOrDefaultAsync(m => m.UserId == id);
//            if (userDatum == null)
//            {
//                return NotFound();
//            }

//            return View(userDatum);
//        }

//        // GET: UserDatums/Create
//        public IActionResult Create()
//        {
//            return View();
//        }

//        // POST: UserDatums/Create
//        // To protect from overposting attacks, enable the specific properties you want to bind to.
//        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Create([Bind("UserId,IdNumber,UName,Unit,Tel,UGender,USalary,UEmployeeTime,CreateTime")] UserDatum userDatum)
//        {
//            if (ModelState.IsValid)
//            {
//                _context.Add(userDatum);
//                await _context.SaveChangesAsync();
//                return RedirectToAction(nameof(Index));
//            }
//            return View(userDatum);
//        }

//        //public IActionResult Page1()
//        //{
//        //    return View();
//        //}

//        //public async Task<IActionResult> Page1()
//        //{
//        //}

//        // GET: UserDatums/Edit/5
//        public async Task<IActionResult> Edit(uint? id)
//        {

//            if (id == null)
//            {
//                return NotFound();
//            }

//            var userDatum = await _context.UserData.FindAsync(id);
//            if (userDatum == null)
//            {
//                return NotFound();
//            }
//            return View(userDatum);
//        }

//        // POST: UserDatums/Edit/5
//        // To protect from overposting attacks, enable the specific properties you want to bind to.
//        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Edit(uint id, [Bind("UserId,IdNumber,UName,Unit,Tel,UGender,USalary,UEmployeeTime,CreateTime")] UserDatum userDatum)
//        {
//            if (id != userDatum.UserId)
//            {
//                return NotFound();
//            }

//            if (ModelState.IsValid)
//            {
//                try
//                {
//                    _context.Update(userDatum);
//                    await _context.SaveChangesAsync();
//                }
//                catch (DbUpdateConcurrencyException)
//                {
//                    if (!UserDatumExists(userDatum.UserId))
//                    {
//                        return NotFound();
//                    }
//                    else
//                    {
//                        throw;
//                    }
//                }
//                return RedirectToAction(nameof(Index));
//            }
//            return View(userDatum);
//        }

//        // GET: UserDatums/Delete/5
//        public async Task<IActionResult> Delete(uint? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var userDatum = await _context.UserData
//                .FirstOrDefaultAsync(m => m.UserId == id);
//            if (userDatum == null)
//            {
//                return NotFound();
//            }

//            return View(userDatum);
//        }

//        // POST: UserDatums/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> DeleteConfirmed(uint id)
//        {
//            var userDatum = await _context.UserData.FindAsync(id);
//            _context.UserData.Remove(userDatum);
//            await _context.SaveChangesAsync();
//            return RedirectToAction(nameof(Index));
//        }

//        private bool UserDatumExists(uint id)
//        {
//            return _context.UserData.Any(e => e.UserId == id);
//        }
//    }
//}

