using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GemBox.Document;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using plan02.Models;
using Rotativa.AspNetCore;
using Xceed.Words.NET;

namespace plan02.Controllers
{
    public class BEmployeesController : Controller
    {
        private readonly PlannedStaffManagementContext _context;
        private readonly IWebHostEnvironment environment;
        private IHostingEnvironment Environment;

        [BindProperty]
        public testModel Invoice { get; set; }

        public BEmployeesController(PlannedStaffManagementContext context, IWebHostEnvironment environment, IHostingEnvironment _env)
        {
            _context = context;
            this.environment = environment;
            Environment = _env;
            ComponentInfo.SetLicense("FREE-LIMITED-KEY");
           
            //ComponentInfo.SetLicense("HEUER-QVSKD-ODGW8-SBS7W-O2VH");
        }

        // GET: BEmployees
        public async Task<IActionResult> Index()
        {
            return View(await _context.BEmployees.Where(s => s.BType != "獎助生" && s.BType != "專任助理").ToListAsync());
        }

        public async Task<IActionResult> Index2()
        {
            return View(await _context.BEmployees.Where(s => s.BType == "獎助生").ToListAsync());
        }

        public async Task<IActionResult> Index3()
        {
            return View(await _context.BEmployees.Where(s => s.BType == "專任助理").ToListAsync());
        }

        // GET: BEmployees/Details/5
        public async Task<IActionResult> Details(uint? id)
        {
            if (id == null)
            {
                return NotFound();
            }
       
            var bEmployee = await _context.BEmployees
                .FirstOrDefaultAsync(m => m.BId == id);
            if (bEmployee == null)
            {
                return NotFound();
            }

            return View(bEmployee);
        }

        // GET: BEmployees/Create
        public IActionResult Create()
        {
            // adding dropdownlist 下拉式選單選項清單增加 助理人員類別
            List<SelectListItem> employeeTypes = new()
            {
                new SelectListItem { Value = "專任助理", Text = "專任助理" },
                new SelectListItem { Value = "獎助生", Text = "獎助生" },
                new SelectListItem { Value = "勞僱型兼任助理/臨時工", Text = "勞僱型兼任助理/臨時工" }
            };
            ViewBag.employeeTypes = employeeTypes;

            return View();
        }


        // POST: BEmployees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BId,UserName,BName,BType,BGender,BMail,BSalary,BEmploymentDate,BEmploymentDate2,BCreatetime")] BEmployee bEmployee, string btype)
        {
            if (ModelState.IsValid)
            {
                // adding different view
                var org = (from m in _context.BEmployees
                           where m.BType == btype
                           select m.BType).FirstOrDefault();
                if (org == "專任助理")
                {
                    _context.Add(bEmployee);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index3", "BEmployees");
                }
                else
                if (org == "獎助生")
                {
                    _context.Add(bEmployee);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index2", "BEmployees");
                }
                _context.Add(bEmployee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bEmployee);
        }

        // GET: BEmployees/Edit/5
        public async Task<IActionResult> Edit(uint? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // adding dropdownlist 下拉式選單選項清單增加 助理人員類別
            List<SelectListItem> employeeTypes = new()
            {
                new SelectListItem { Value = "專任助理", Text = "專任助理" },
                new SelectListItem { Value = "獎助生", Text = "獎助生" },
                new SelectListItem { Value = "勞僱型兼任助理/臨時工", Text = "勞僱型兼任助理/臨時工" }
            };

            //assigning SelectListItem to view Bag
            ViewBag.employeeTypes = employeeTypes;


            // adding dropdownlist 下拉式選單選項清單增加 性別類別
            List<SelectListItem> genderTypes = new()
            {
                new SelectListItem { Value = "男", Text = "男" },
                new SelectListItem { Value = "女", Text = "女" },
            };

            //assigning SelectListItem to view Bag
            ViewBag.genderTypes = genderTypes;

            var bEmployee = await _context.BEmployees.FindAsync(id);
            if (bEmployee == null)
            {
                return NotFound();
            }
            return View(bEmployee);
        }

        // POST: BEmployees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(uint id, [Bind("BId,UserName,BName,BType,BGender,BMail,BSalary,BEmploymentDate,BEmploymentDate2,BCreatetime")] BEmployee bEmployee, string btype)
        {
            if (id != bEmployee.BId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // adding different view
                var org = (from m in _context.BEmployees
                           where m.BType == btype
                           select m.BType).FirstOrDefault();
                if (org == "專任助理")
                {
                    _context.Update(bEmployee);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index3", "BEmployees");
                }
                else
                if (org == "獎助生")
                {
                    _context.Update(bEmployee);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index2", "BEmployees");
                }

                try
                {
                    _context.Update(bEmployee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BEmployeeExists(bEmployee.BId))
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
            return View(bEmployee);
        }

        public IActionResult Index7()
        {
            return View(new testModel());
        }

        public FileStreamResult Download_B(testModel model)
        {
            // Load template document.
            var path = Path.Combine(this.Environment.WebRootPath, "Files/agreement_template.docx");
            //下面路徑佈署到Linux會錯
            //var path = Path.Combine(this.environment.ContentRootPath, "test6.docx");
            var document = DocumentModel.Load(path);

            // Execute find and replace operations.
            //document.Content.Replace("{{BSalary}}", this.Invoice.BSalary.ToString("0000"));
            document.Content.Replace("{{BName}}", this.Invoice.BName);

            // Save document in specified file format.
            var stream = new MemoryStream();
            document.Save(stream, this.Invoice.Options);

            // Download file.
            //下面是原版套版產生之路徑
            //return File(stream, model.Options.ContentType, $"OutputFromView.{model.Format.ToLower()}");
            return File(stream, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "agreement_template.docx");
        }



        // GET: BEmployees/Delete/5
        public async Task<IActionResult> Delete(uint? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bEmployee = await _context.BEmployees
                .FirstOrDefaultAsync(m => m.BId == id);
            if (bEmployee == null)
            {
                return NotFound();
            }

            return View(bEmployee);
        }

        // POST: BEmployees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(uint id)
        {
            var bEmployee = await _context.BEmployees.FindAsync(id);
            _context.BEmployees.Remove(bEmployee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //搜尋頁
        public async Task<IActionResult> Index5(string searchString, string search2)
        {
            var movies = from m in _context.BEmployees
                         select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(s => s.BType.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(search2))
            {
                movies = movies.Where(s => s.BGender.Contains(search2));
            }
        
            return View(await movies.ToListAsync());
        }


        // "DemoModelPDF"
        //[HttpGet]
        //public async Task<IActionResult> DemoViewAsPdf()
        //{
        //    //var soe = await _context.BEmployees.ToListAsync();
        //    return new ViewAsPdf(await _context.BEmployees.ToListAsync());
        //}
        // "DemoModelPDF"

        [HttpGet]
        public IActionResult DemoViewAsPdf()
        {
            return new ViewAsPdf(_context.BEmployees.ToList());
        }

        public async Task<IActionResult> Print()
        {
            return View(await _context.BEmployees.ToListAsync());
        }

       

        private bool BEmployeeExists(uint id)
        {
            return _context.BEmployees.Any(e => e.BId == id);
        }
    }
}
