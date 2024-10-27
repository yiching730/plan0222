using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using plan02.Models;
using Rotativa.AspNetCore;

namespace plan02.Controllers
{
    public class PunchInsController : Controller
    {
        private readonly PlannedStaffManagementContext _context;

        public PunchInsController(PlannedStaffManagementContext context)
        {
            _context = context;
        }

        // GET: PunchIns
        public async Task<IActionResult> Index()
        {
            return View(await _context.PunchIns.OrderBy(s => s.Year).ThenBy(s => s.Month).ThenBy(s => s.Day).ToListAsync());
        }

        public IActionResult Index2(string year, string month)
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


        public ActionResult _Header()
        {
            PunchIn DateVM = new PunchIn();
            return View(DateVM);
        }

        [HttpPost]
        public ActionResult _Header(PunchIn vm)
        {
            if (ModelState.IsValid)
            {
                // save vm to database, etc.
            }
            return View(vm);
        }

        // GET: PunchIns/Details/5
        public async Task<IActionResult> Details(uint? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var punchIn = await _context.PunchIns
                .FirstOrDefaultAsync(m => m.Pid == id);
            if (punchIn == null)
            {
                return NotFound();
            }

            return View(punchIn);
        }

        public async Task<IActionResult> Index3(string searchString)
        {
            var movies = from m in _context.PunchIns
                         select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(s => s.Month.ToString().Contains(searchString));
            }

            return View(await movies.ToListAsync());
        }

        //public IActionResult Index4()
        //{                            
                //currentDate = new DateTime(Convert.ToInt32(year), Convert.ToInt32(month), 1);

            //DateTime daySet2 = new DateTime(2022, 7, 30);
            //string[] weeks = new string[] { "星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六" };
            //string week = weeks[Convert.ToInt32(daySet2.DayOfWeek.ToString("d"))].ToString();
            //ViewData["week"] = week;

            //string[] weeks = new string[] { "星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六" };
            //string week = weeks[Convert.ToInt32(DateTime.Now.DayOfWeek.ToString("d"))].ToString();
            //ViewData["week"] = week;

        //    return View();
        //}

        public IActionResult Index4()
        {
            List<SelectListItem> months = new()
            {
                new SelectListItem { Value = "1", Text = "1" },
                new SelectListItem { Value = "2", Text = "2" },
                new SelectListItem { Value = "3", Text = "3" },
                new SelectListItem { Value = "4", Text = "4" },
                new SelectListItem { Value = "5", Text = "5" },
                new SelectListItem { Value = "6", Text = "6" },
                new SelectListItem { Value = "7", Text = "7" },
                new SelectListItem { Value = "8", Text = "8" },
                new SelectListItem { Value = "9", Text = "9" },
                new SelectListItem { Value = "10", Text = "10" },
                new SelectListItem { Value = "11", Text = "11" },
                new SelectListItem { Value = "12", Text = "12" }
            };
            ViewBag.months = months;


            List<SelectListItem> yearss = new()
            {
                new SelectListItem { Value = "2022", Text = "2022"},
                new SelectListItem { Value = "2023", Text = "2023" },
                new SelectListItem { Value = "2024", Text = "2024" },
                new SelectListItem { Value = "2025", Text = "2025" },
                new SelectListItem { Value = "2026", Text = "2026" },
                new SelectListItem { Value = "2027", Text = "2027" },
                new SelectListItem { Value = "2028", Text = "2028" },
                new SelectListItem { Value = "2029", Text = "2029" },
                new SelectListItem { Value = "2030", Text = "2030" },
                new SelectListItem { Value = "2031", Text = "2031" }
            };
            ViewBag.yearss = yearss;


            List<SelectListItem> leaveDays = new()
            {
                new SelectListItem { Value = "公假", Text = "公假" },
                new SelectListItem { Value = "事假", Text = "事假" },
                new SelectListItem { Value = "病假", Text = "病假" },
                new SelectListItem { Value = "休假(慰勞假)", Text = "休假(慰勞假)" },
                new SelectListItem { Value = "補休", Text = "補休" },
                new SelectListItem { Value = "婚假", Text = "婚假" },
                new SelectListItem { Value = "喪假", Text = "喪假" },
                new SelectListItem { Value = "延長病假", Text = "延長病假" },
                new SelectListItem { Value = "產前假", Text = "產前假" },
                new SelectListItem { Value = "陪產假", Text = "陪產假" },
                new SelectListItem { Value = "娩假", Text = "娩假" },
                new SelectListItem { Value = "流產假", Text = "流產假" },
                new SelectListItem { Value = "流產假", Text = "捐贈假" }
            };
            ViewBag.leaveDays = leaveDays;

            List<SelectListItem> daySet2 = new()
            {
                new SelectListItem { Value = "星期日", Text = "星期日"},
                new SelectListItem { Value = "星期一", Text = "星期一" },
                new SelectListItem { Value = "星期二", Text = "星期二" },
                new SelectListItem { Value = "星期三", Text = "星期三" },
                new SelectListItem { Value = "星期四", Text = "星期四" },
                new SelectListItem { Value = "星期五", Text = "星期五" },
                new SelectListItem { Value = "星期六", Text = "星期六" }
            };
            ViewBag.daySet2 = daySet2;

            //DateTime daySet2 = new DateTime(2022, 7, 30);
            //string[] weeks = new string[] { "星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六" };
            //string week = weeks[Convert.ToInt32(daySet2.DayOfWeek.ToString("d"))].ToString();
            //ViewData["week"] = week;

            return View();
        }

        [HttpPost]
        public IActionResult Index4(PunchIn test1)
        {            
            if (ModelState.IsValid)
            {
                // save vm to database, etc.
            }
            return View(test1);
        }


        // GET: PunchIns/Create
        public IActionResult Create()
        {
            List<SelectListItem> yearss = new()
            {
                new SelectListItem { Value = "2022", Text = "2022" },
                new SelectListItem { Value = "2023", Text = "2023" },
                new SelectListItem { Value = "2024", Text = "2024" }
            };
            ViewBag.yearss = yearss;

            List<SelectListItem> monthss = new()
            {
                new SelectListItem { Value = "1", Text = "1" },
                new SelectListItem { Value = "2", Text = "2" },
                new SelectListItem { Value = "3", Text = "3" },
                new SelectListItem { Value = "4", Text = "4" },
                new SelectListItem { Value = "5", Text = "5" },
                new SelectListItem { Value = "6", Text = "6" },
                new SelectListItem { Value = "7", Text = "7" },
                new SelectListItem { Value = "8", Text = "8" },
                new SelectListItem { Value = "9", Text = "9" },
                new SelectListItem { Value = "10", Text = "10" },
                new SelectListItem { Value = "11", Text = "11" },
                new SelectListItem { Value = "12", Text = "12" }
            };
            ViewBag.monthss = monthss;

            List<SelectListItem> dayss = new()
            {
                new SelectListItem { Value = "1", Text = "1" },
                new SelectListItem { Value = "2", Text = "2" },
                new SelectListItem { Value = "3", Text = "3" },
                new SelectListItem { Value = "4", Text = "4" },
                new SelectListItem { Value = "5", Text = "5" },
                new SelectListItem { Value = "6", Text = "6" },
                new SelectListItem { Value = "7", Text = "7" },
                new SelectListItem { Value = "8", Text = "8" },
                new SelectListItem { Value = "9", Text = "9" },
                new SelectListItem { Value = "10", Text = "10" },
                new SelectListItem { Value = "11", Text = "11" },
                new SelectListItem { Value = "12", Text = "12" },
                new SelectListItem { Value = "13", Text = "13" },
                new SelectListItem { Value = "14", Text = "14" },
                new SelectListItem { Value = "15", Text = "15" },
                new SelectListItem { Value = "16", Text = "16" },
                new SelectListItem { Value = "17", Text = "17" },
                new SelectListItem { Value = "18", Text = "18" },
                new SelectListItem { Value = "19", Text = "19" },
                new SelectListItem { Value = "20", Text = "20" },
                new SelectListItem { Value = "21", Text = "21" },
                new SelectListItem { Value = "22", Text = "22" },
                new SelectListItem { Value = "23", Text = "23" },
                new SelectListItem { Value = "24", Text = "24" },
                new SelectListItem { Value = "25", Text = "25" },
                new SelectListItem { Value = "26", Text = "26" },
                new SelectListItem { Value = "27", Text = "27" },
                new SelectListItem { Value = "28", Text = "28" },
                new SelectListItem { Value = "29", Text = "29" },
                new SelectListItem { Value = "30", Text = "30" },
                new SelectListItem { Value = "31", Text = "31" }
            };
            ViewBag.dayss = dayss;

           

            //    List<SelectListItem> dayss = new List<SelectListItem>();
            //    for (int i = 1; i <= DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month); i++)
            //    {
            //        dayss.Add(new SelectListItem { Value = i.ToString(), Text = i.ToString() });
            //    }
            //ViewBag.dayss = dayss;



            //int year = DateTime.Now.Year;
            ////int month = DateTime.Now.Month;

            ////ViewData["daysInJuly"] = daysInJuly;
            ////List<SelectListItem> monthsss = new List<SelectListItem>();
            //string monthsss = "";
            //for (int month = 1; month <= 12; month++)
            //{
            //    int daysmonth = System.DateTime.DaysInMonth(year, month);

            //    //monthsss.Add(new SelectListItem { Value = month.ToString(), Text = month.ToString() });
            //    monthsss = month.ToString();

            //    //monthsss = month.ToString();

            //    ViewBag.monthsss = monthsss;
            //    for (int day = 1; day <= daysmonth; day++)
            //    {
            //        //DateTime weekday = new DateTime(year, month, day);
            //        var punchIn = _context.PunchIns.Where(s => s.Day == day).ToList();
            //    }

            //}


            //List<SelectListItem> dayss = new List<SelectListItem>();
            //for (int i = 1; i <= DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month); i++)
            //{
            //    dayss.Add(new SelectListItem { Value = i.ToString(), Text = i.ToString() });
            //}
            //ViewBag.dayss = dayss;


            //int year = DateTime.Now.Year;
            ////int month = DateTime.Now.Month;
            //int daysInJuly = System.DateTime.DaysInMonth(year, 7);
            //ViewData["daysInJuly"] = daysInJuly;
            //for (int month = 1; month <= 12; month++)
            //{
            //    for (int day = 1; day <= daysInJuly; day++)
            //    {
            //        //DateTime weekday = new DateTime(year, month, day);
            //        var punchIn = _context.PunchIns.Where(s => s.Day == day).ToList();
            //    }
            //}

            // .ToList();

            return View();
        }

        // POST: PunchIns/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Pid,Year,Month,Day,PunchIn1,PunchOut,Remark2")] PunchIn punchIn)
        {
           

            if (ModelState.IsValid)
            {
                _context.Add(punchIn);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(punchIn);
        }


        public IActionResult Create2()
        {         
     //   var months = new List<SelectListItem> {
     //   new SelectListItem {Text = "1", Value = "1"},
     //   new SelectListItem {Text = "2", Value = "2"},
     //   new SelectListItem {Text = "3", Value = "3"},
     //   new SelectListItem {Text = "4", Value = "4"},
     //   new SelectListItem {Text = "5", Value = "5"},
     //   new SelectListItem {Text = "6", Value = "6"},
     //   new SelectListItem {Text = "7", Value = "7"},
     //   new SelectListItem {Text = "8", Value = "8"},
     //   new SelectListItem {Text = "9", Value = "9"},
     //   new SelectListItem {Text = "10", Value = "10"},
     //   new SelectListItem {Text = "11", Value = "11"},
     //   new SelectListItem {Text = "12", Value = "12"},
     //};
            //ViewBag.months = months;
       

            List<SelectListItem> months = new()
            {
                new SelectListItem { Value = "1", Text = "1" },
                new SelectListItem { Value = "2", Text = "2" },
                new SelectListItem { Value = "3", Text = "3" },
                new SelectListItem { Value = "4", Text = "4" },
                new SelectListItem { Value = "5", Text = "5" },
                new SelectListItem { Value = "6", Text = "6" },
                new SelectListItem { Value = "7", Text = "7" },
                new SelectListItem { Value = "8", Text = "8" },
                new SelectListItem { Value = "9", Text = "9" },
                new SelectListItem { Value = "10", Text = "10" },
                new SelectListItem { Value = "11", Text = "11" },
                new SelectListItem { Value = "12", Text = "12" }
            };
            ViewBag.months = months;


            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create2(List<PunchIn> PunchIn)
        {
            if (ModelState.IsValid)
            {

                var targetlist = PunchIn.Where(x => x.PunchIn1 != null).ToList();
                _context.AddRange(targetlist);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View();

            //if (ModelState.IsValid)
            //{
            //    _context.Add(punchIn);
            //    await _context.SaveChangesAsync();
            //    return RedirectToAction(nameof(Index));
            //}
            //return View(punchIn);
        }

        public IActionResult Create3()
        {
            List<SelectListItem> leaveDay = new()
            {
                new SelectListItem { Value = "公假", Text = "公假" },
                new SelectListItem { Value = "事假", Text = "事假" },
                new SelectListItem { Value = "病假", Text = "病假" },
                new SelectListItem { Value = "休假(慰勞假)", Text = "休假(慰勞假)" },
                new SelectListItem { Value = "補休", Text = "補休" },
                new SelectListItem { Value = "婚假", Text = "婚假" },
                new SelectListItem { Value = "喪假", Text = "喪假" },
                new SelectListItem { Value = "延長病假", Text = "延長病假" },
                new SelectListItem { Value = "產前假", Text = "產前假" },
                new SelectListItem { Value = "陪產假", Text = "陪產假" },
                new SelectListItem { Value = "娩假", Text = "娩假" },
                new SelectListItem { Value = "流產假", Text = "流產假" },
                new SelectListItem { Value = "流產假", Text = "捐贈假" }
            };
            ViewBag.leaveDay = leaveDay;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create3(List<PunchIn> PunchIn)
        {
            if (ModelState.IsValid)
            {
                var targetlist = PunchIn.Where(x => x.PunchIn1 != null).ToList();
                _context.AddRange(targetlist);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();           
        }

        public IActionResult Create4()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create4([Bind("Pid,Year,Month,Day,PunchIn1,PunchOut,Remark2")] PunchIn punchIn)
        {
            //var Pmonth = (from m in _context.PunchIns
            //              where m.Month == 2
            //              select m.Month);
            //ViewBag.ShowList = false;
            //if (Pmonth != null) 
            //{
            //    ViewBag.ShowList = true;

            //    return View(); 
            //}


            if (ModelState.IsValid)
            {
                _context.Add(punchIn);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(punchIn);
        }

        public IActionResult Create5()
        {
            List<SelectListItem> months = new()
            {
                new SelectListItem { Value = "1", Text = "1" },
                new SelectListItem { Value = "2", Text = "2" },
                new SelectListItem { Value = "3", Text = "3" },
                new SelectListItem { Value = "4", Text = "4" },
                new SelectListItem { Value = "5", Text = "5" },
                new SelectListItem { Value = "6", Text = "6" },
                new SelectListItem { Value = "7", Text = "7" },
                new SelectListItem { Value = "8", Text = "8" },
                new SelectListItem { Value = "9", Text = "9" },
                new SelectListItem { Value = "10", Text = "10" },
                new SelectListItem { Value = "11", Text = "11" },
                new SelectListItem { Value = "12", Text = "12" }
            };
            ViewBag.months = months;

           
            List<SelectListItem> yearss = new()
            {
                new SelectListItem { Value = "2022", Text = "2022"},
                new SelectListItem { Value = "2023", Text = "2023" },
                new SelectListItem { Value = "2024", Text = "2024" },
                new SelectListItem { Value = "2025", Text = "2025" },
                new SelectListItem { Value = "2026", Text = "2026" },
                new SelectListItem { Value = "2027", Text = "2027" },
                new SelectListItem { Value = "2028", Text = "2028" },
                new SelectListItem { Value = "2029", Text = "2029" },
                new SelectListItem { Value = "2030", Text = "2030" },
                new SelectListItem { Value = "2031", Text = "2031" }
            };
            ViewBag.yearss = yearss;
            //var Nowyear = DateTime.Now.Year;
            //if (ViewBag.yearss == Nowyear)
            //{
                
            //}
            
            List<SelectListItem> leaveDays = new()
            {
                new SelectListItem { Value = "公假", Text = "公假" },
                new SelectListItem { Value = "事假", Text = "事假" },
                new SelectListItem { Value = "病假", Text = "病假" },
                new SelectListItem { Value = "休假(慰勞假)", Text = "休假(慰勞假)" },
                new SelectListItem { Value = "補休", Text = "補休" },
                new SelectListItem { Value = "婚假", Text = "婚假" },
                new SelectListItem { Value = "喪假", Text = "喪假" },
                new SelectListItem { Value = "延長病假", Text = "延長病假" },
                new SelectListItem { Value = "產前假", Text = "產前假" },
                new SelectListItem { Value = "陪產假", Text = "陪產假" },
                new SelectListItem { Value = "娩假", Text = "娩假" },
                new SelectListItem { Value = "流產假", Text = "流產假" },
                new SelectListItem { Value = "流產假", Text = "捐贈假" }
            };
            ViewBag.leaveDays = leaveDays;

            List<SelectListItem> daySet2 = new()
            {
                new SelectListItem { Value = "星期日", Text = "星期日"},
                new SelectListItem { Value = "星期一", Text = "星期一" },
                new SelectListItem { Value = "星期二", Text = "星期二" },
                new SelectListItem { Value = "星期三", Text = "星期三" },
                new SelectListItem { Value = "星期四", Text = "星期四" },
                new SelectListItem { Value = "星期五", Text = "星期五" },
                new SelectListItem { Value = "星期六", Text = "星期六" }
            };
            ViewBag.daySet2 = daySet2;

            //DateTime daySet2 = new DateTime(2022, 7, 30);
            //string[] weeks = new string[] { "星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六" };
            //string week = weeks[Convert.ToInt32(daySet2.DayOfWeek.ToString("d"))].ToString();
            //ViewData["week"] = week;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create5([Bind("Pid,Year,Month,Day,PunchIn1,PunchOut,Remark2")] PunchIn punchIn)
        {
            var yearNow = DateTime.Now.Year;
            var monthNow = DateTime.Now.Month;

            DateTime daySet = new DateTime(2022, 8, 1);
            var daySet2 = daySet.DayOfWeek;
            ViewData["daySet2"] = daySet2;



            PunchIn customer = _context.PunchIns.Where(x => x.Year == yearNow && x.Month == monthNow).FirstOrDefault();
            if (customer != null)
            {
                TempData["Message"] = "非本月打卡!";
                return RedirectToAction("Create5","PunchIns");
            }

            if (ModelState.IsValid)
            {
                _context.Add(punchIn);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));               
                //_context.Add(punchIn);
                //await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
            }           
            return View(punchIn);
        }

        public IActionResult Create6()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create6(List<PunchIn> PunchIn)
        {
            if (ModelState.IsValid)
            {

                var targetlist = PunchIn.Where(x => x.PunchIn1 != null).ToList();
                _context.AddRange(targetlist);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View();

            //if (ModelState.IsValid)
            //{
            //    _context.Add(punchIn);
            //    await _context.SaveChangesAsync();
            //    return RedirectToAction(nameof(Index));
            //}
            //return View(punchIn);
        }

        public IActionResult Create7()
        {
            List<SelectListItem> leaveDays = new()
            {
                new SelectListItem { Value = "公假", Text = "公假" },
                new SelectListItem { Value = "事假", Text = "事假" },
                new SelectListItem { Value = "病假", Text = "病假" },
                new SelectListItem { Value = "休假(慰勞假)", Text = "休假(慰勞假)" },
                new SelectListItem { Value = "補休", Text = "補休" },
                new SelectListItem { Value = "婚假", Text = "婚假" },
                new SelectListItem { Value = "喪假", Text = "喪假" },
                new SelectListItem { Value = "延長病假", Text = "延長病假" },
                new SelectListItem { Value = "產前假", Text = "產前假" },
                new SelectListItem { Value = "陪產假", Text = "陪產假" },
                new SelectListItem { Value = "娩假", Text = "娩假" },
                new SelectListItem { Value = "流產假", Text = "流產假" },
                new SelectListItem { Value = "流產假", Text = "捐贈假" }
            };
            ViewBag.leaveDays = leaveDays;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create7(List<PunchIn> PunchIn)
        {
            if (ModelState.IsValid)
            {

                var targetlist = PunchIn.Where(x => x.PunchIn1 != null).ToList();
                _context.AddRange(targetlist);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(PrintPunchIn2));
            }
            //return RedirectToAction("PrintPunchIn2", "PunchIns");
            return View();
        }


        // GET: PunchIns/Edit/5
        public async Task<IActionResult> Edit(uint? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            

            List<SelectListItem> monthss = new()
            {
                new SelectListItem { Value = "1", Text = "1" },
                new SelectListItem { Value = "2", Text = "2" },
                new SelectListItem { Value = "3", Text = "3" },
                new SelectListItem { Value = "4", Text = "4" },
                new SelectListItem { Value = "5", Text = "5" },
                new SelectListItem { Value = "6", Text = "6" },
                new SelectListItem { Value = "7", Text = "7" },
                new SelectListItem { Value = "8", Text = "8" },
                new SelectListItem { Value = "9", Text = "9" },
                new SelectListItem { Value = "10", Text = "10" },
                new SelectListItem { Value = "11", Text = "11" },
                new SelectListItem { Value = "12", Text = "12" }
            };
            ViewBag.monthss = monthss;

            var punchIn = await _context.PunchIns.FindAsync(id);
            if (punchIn == null)
            {
                return NotFound();
            }
            return View(punchIn);
        }

       


            // POST: PunchIns/Edit/5
            // To protect from overposting attacks, enable the specific properties you want to bind to.
            // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(uint id, [Bind("Pid,Year,Month,Day,PunchIn1,PunchOut,Remark2")] PunchIn punchIn)
        {
          
            if (id != punchIn.Pid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(punchIn);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PunchInExists(punchIn.Pid))
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
            return View(punchIn);
        }

        // GET: PunchIns/Delete/5
        public async Task<IActionResult> Delete(uint? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var punchIn = await _context.PunchIns
                .FirstOrDefaultAsync(m => m.Pid == id);
            if (punchIn == null)
            {
                return NotFound();
            }

            return View(punchIn);
        }

        // POST: PunchIns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(uint id)
        {
            var punchIn = await _context.PunchIns.FindAsync(id);
            _context.PunchIns.Remove(punchIn);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // "DemoModelPDF"
        [HttpGet]
        public async Task<IActionResult> DemoViewAsPdf3()
        {
            //var soe = await _context.BEmployees.ToListAsync();
            return new ViewAsPdf(await _context.PunchIns.ToListAsync());
        }
        // "DemoModelPDF"

        //列印月簽到退
        public async Task<IActionResult> PrintPunchIn2()
        {
            
            return View(await _context.PunchIns.Where(s => s.Year == DateTime.Now.Year && s.Month == DateTime.Now.Month).OrderBy(s => s.Day).ToListAsync());
        }

        //列印月簽到退，新增下一頁分頁及搜尋功能
        public async Task<IActionResult> PrintPunchIn(string sortOrder,string currentFilter, string searchString, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var students = from s in _context.PunchIns
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.Month.ToString().Contains(searchString));
            }
            //若查詢無資料，會提示
            if (!students.Any())
            {
                ViewBag.message = "查無資料 ";
            }
            
            switch (sortOrder)
            {
                case "name_desc":
                    students = students.OrderByDescending(s => s.Year);
                    break;
                case "Date":
                    students = students.OrderBy(s => s.Day);
                    break;
                case "date_desc":
                    students = students.OrderByDescending(s => s.Day);
                    break;
                default:
                    students = students.OrderBy(s => s.Year);
                    break;
            }

            int pageSize = 5;
            return View(await PaginatedList<PunchIn>.CreateAsync(students.AsNoTracking(), pageNumber ?? 1, pageSize));
            //return View(await _context.PunchIns.OrderBy(s => s.Year).ThenBy(s => s.Month).ThenBy(s => s.Day).ToListAsync());
        }

      


        private bool PunchInExists(uint id)
        {
            return _context.PunchIns.Any(e => e.Pid == id);
        }
    }
}

//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.EntityFrameworkCore;
//using plan02.Models;

//namespace plan02.Controllers
//{
//    public class PunchInsController : Controller
//    {
//        private readonly PlannedStaffManagementContext _context;

//        public PunchInsController(PlannedStaffManagementContext context)
//        {
//            _context = context;
//        }

//        GET: PunchIns
//        public async Task<IActionResult> Index()
//        {
//            return View(await _context.PunchIns.ToListAsync());
//        }

//        GET: PunchIns/Details/5
//        public async Task<IActionResult> Details(uint? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var punchIn = await _context.PunchIns
//                .FirstOrDefaultAsync(m => m.Pid == id);
//            if (punchIn == null)
//            {
//                return NotFound();
//            }

//            return View(punchIn);
//        }

//        GET: PunchIns/Create
//        public IActionResult Create()
//        {
//            return View();
//        }

//        POST: PunchIns/Create
//        To protect from overposting attacks, enable the specific properties you want to bind to.
//        For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

//       [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Create([Bind("Pid,Year,Month,Day,PunchIn1,PunchOut,Remark2")] PunchIn punchIn)
//        {
//            if (ModelState.IsValid)
//            {
//                _context.Add(punchIn);
//                await _context.SaveChangesAsync();
//                return RedirectToAction(nameof(Index));
//            }
//            return View(punchIn);
//        }

//        GET: PunchIns/Edit/5
//        public async Task<IActionResult> Edit(uint? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var punchIn = await _context.PunchIns.FindAsync(id);
//            if (punchIn == null)
//            {
//                return NotFound();
//            }
//            return View(punchIn);
//        }

//        POST: PunchIns/Edit/5
//         To protect from overposting attacks, enable the specific properties you want to bind to.
//         For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Edit(uint id, [Bind("Pid,Year,Month,Day,PunchIn1,PunchOut,Remark2")] PunchIn punchIn)
//        {
//            if (id != punchIn.Pid)
//            {
//                return NotFound();
//            }

//            if (ModelState.IsValid)
//            {
//                try
//                {
//                    _context.Update(punchIn);
//                    await _context.SaveChangesAsync();
//                }
//                catch (DbUpdateConcurrencyException)
//                {
//                    if (!PunchInExists(punchIn.Pid))
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
//            return View(punchIn);
//        }

//        GET: PunchIns/Delete/5
//        public async Task<IActionResult> Delete(uint? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var punchIn = await _context.PunchIns
//                .FirstOrDefaultAsync(m => m.Pid == id);
//            if (punchIn == null)
//            {
//                return NotFound();
//            }

//            return View(punchIn);
//        }

//        POST: PunchIns/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> DeleteConfirmed(uint id)
//        {
//            var punchIn = await _context.PunchIns.FindAsync(id);
//            _context.PunchIns.Remove(punchIn);
//            await _context.SaveChangesAsync();
//            return RedirectToAction(nameof(Index));
//        }

//        private bool PunchInExists(uint id)
//        {
//            return _context.PunchIns.Any(e => e.Pid == id);
//        }
//    }
//}
