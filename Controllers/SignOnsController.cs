using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using plan02.Models;
using Rotativa.AspNetCore;

namespace plan02.Controllers
{
    public class SignOnsController : Controller
    {
        private readonly PlannedStaffManagementContext _context;

        public SignOnsController(PlannedStaffManagementContext context)
        {
            _context = context;
        }

        // GET: SignOns
        public async Task<IActionResult> Index()
        {
            return View(await _context.SignOns.ToListAsync());
        }



        public IActionResult Index2(/*int year, int month*/)
        {
            int a = 2022;
            int daysInJuly = System.DateTime.DaysInMonth(a, 5);
            ViewData["daysInJuly"] = daysInJuly;

            //for (int month = 1; month <= 12; month++)
            //{
            //    int daysInMonth = System.DateTime.DaysInMonth(2014, 1);
            //    for (int day = 1; day <= daysInMonth; day++)
            //    {



            //        //sso.day = day;
            //    }
            return View();
        }
           



            //public List<DateTime> getAllDates(int year, int month)
            //{
            //    year = 2021;
            //    month = 5;
            //    var ret = new List<DateTime>();
            //    for (int i = 1; i <= DateTime.DaysInMonth(year, month); i++)
            //    {
            //        ret.Add(new DateTime(year, month, i));
            //    }
            //    return ret;
            //    ViewData["ret"] = ret;
            //}


            // GET: SignOns/Details/5
            public async Task<IActionResult> Details(uint? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var signOn = await _context.SignOns
                .FirstOrDefaultAsync(m => m.Sid == id);
            if (signOn == null)
            {
                return NotFound();
            }

            return View(signOn);
        }

        // GET: SignOns/Create
        public IActionResult Create()
        {
            //string today_string = DateTime.Now.AddMonths(1).ToString("yyyy-MM-dd");
            //ViewData["today_string"] = today_string;

            //List<SelectListItem> dayss = new()
            //{
            //    new SelectListItem { Value = "2021-01-01", Text = "2021-01-01" },
            //    new SelectListItem { Value = "2021-01-02", Text = "2021-01-02" },
            //    new SelectListItem { Value = "2021-01-03", Text = "2021-01-03" }
            //};
            //ViewBag.dayss = dayss;
            return View();
        }

        // POST: SignOns/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Sid,SignIn,SignOut,Remark")] SignOn signOn)
        {
            if (ModelState.IsValid)
            {
                _context.Add(signOn);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(signOn);
        }


        public IActionResult Index3(string year, string month)
        {
            var currentDate = DateTime.Now;
            if (!string.IsNullOrEmpty(year) && !string.IsNullOrEmpty(month))
                currentDate = new DateTime(Convert.ToInt32(year), Convert.ToInt32(month), 1);



            var date = new DateTime(currentDate.Year, currentDate.Month, 1);

            var model = new CalendarModel
            {
                Title = date.ToString("yyyy MMMM").ToUpper(),
                StartDayOfWeak = (int)date.DayOfWeek,
                EndDay = date.AddMonths(1).AddSeconds(-1).Day
            };

            return View(model);
        }


        // GET: SignOns/Edit/5
        public async Task<IActionResult> Edit(uint? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var signOn = await _context.SignOns.FindAsync(id);
            if (signOn == null)
            {
                return NotFound();
            }
            return View(signOn);
        }

        // POST: SignOns/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(uint id, [Bind("Sid,SignIn,SignOut,Remark")] SignOn signOn)
        {
            if (id != signOn.Sid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(signOn);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SignOnExists(signOn.Sid))
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
            return View(signOn);
        }

        // GET: SignOns/Delete/5
        public async Task<IActionResult> Delete(uint? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var signOn = await _context.SignOns
                .FirstOrDefaultAsync(m => m.Sid == id);
            if (signOn == null)
            {
                return NotFound();
            }

            return View(signOn);
        }

        // POST: SignOns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(uint id)
        {
            var signOn = await _context.SignOns.FindAsync(id);
            _context.SignOns.Remove(signOn);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // "DemoModelPDF"
        [HttpGet]
        public async Task<IActionResult> DemoViewAsPdf2()
        {
            //var soe = await _context.BEmployees.ToListAsync();
            return new ViewAsPdf(await _context.SignOns.ToListAsync());
        }
        // "DemoModelPDF"

        private bool SignOnExists(uint id)
        {
            return _context.SignOns.Any(e => e.Sid == id);
        }
    }
}
