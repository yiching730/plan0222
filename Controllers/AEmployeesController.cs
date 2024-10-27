using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DayPilot.Web.Ui;
using GemBox.Document;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using plan02.Models;


namespace plan02.Controllers
{
    public class AEmployeesController : Controller
    {
        private readonly PlannedStaffManagementContext _context;
        private IHostingEnvironment Environment;
        private readonly IWebHostEnvironment environment;

        [BindProperty]
        public InvoiceModel Invoice { get; set; }


        public AEmployeesController(PlannedStaffManagementContext context, IHostingEnvironment _env, IWebHostEnvironment environment)
        {
            _context = context;
            Environment = _env;
            this.environment = environment;
            ComponentInfo.SetLicense("FREE-LIMITED-KEY");
        }

        // GET: AEmployees
        public async Task<IActionResult> Index()
        {
            return View(await _context.AEmployees.ToListAsync());
        }

        public async Task<IActionResult> Index2()
        {
            return View(await _context.AEmployees.ToListAsync());
        }

        public async Task<IActionResult> Index3()
        {
            return View(await _context.AEmployees.ToListAsync());
        }

        public IActionResult Index4()
        {
            return View(new InvoiceModel());
        }

        public FileStreamResult Download(InvoiceModel model)
        {
            // Load template document.
            var path = Path.Combine(this.environment.ContentRootPath, "InvoiceWithPlaceholders2.docx");
            var document = DocumentModel.Load(path);

            // Execute find and replace operations.
            document.Content.Replace("{{Number}}", this.Invoice.Number.ToString("0000"));
            document.Content.Replace("{{Date}}", this.Invoice.Date.ToString("d MMM yyyy HH:mm"));
            document.Content.Replace("{{Company}}", this.Invoice.Company);
            document.Content.Replace("{{Address}}", this.Invoice.Address);
            document.Content.Replace("{{Name}}", this.Invoice.Name);

            // Save document in specified file format.
            var stream = new MemoryStream();
            document.Save(stream, this.Invoice.Options);

            // Download file.
            return File(stream, model.Options.ContentType, $"OutputFromView.{model.Format.ToLower()}");
        }

        // GET: AEmployees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aEmployee = await _context.AEmployees
                .FirstOrDefaultAsync(m => m.AId == id);
            if (aEmployee == null)
            {
                return NotFound();
            }

            return View(aEmployee);
        }

        // GET: AEmployees/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AEmployees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AId,UserName,AName,AGender,AMail,ASalary,AEmploymentDate")] AEmployee aEmployee)
        {
            if (ModelState.IsValid)
            {
                //var firstEm = await _context.AEmployees.FirstOrDefaultAsync(m => m.AGender == "專任助理");
                ////var firstEm = await _context.AEmployees.Where(m => m.AGender == "專任助理").ToListAsync();
                //if (firstEm != null)
                //{
                //    _context.Add(aEmployee);
                //    await _context.SaveChangesAsync();
                //    return RedirectToAction(nameof(Index2));
                //}else
                _context.Add(aEmployee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(aEmployee);
        }

        // GET: AEmployees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aEmployee = await _context.AEmployees.FindAsync(id);
            if (aEmployee == null)
            {
                return NotFound();
            }
            return View(aEmployee);
        }

        // POST: AEmployees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AId,UserName,AName,AGender,AMail,ASalary,AEmploymentDate")] AEmployee aEmployee, string agender)
        {
            if (id != aEmployee.AId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // adding different view
                var org = (from m in _context.AEmployees
                           where m.AGender == agender
                           select m.AGender).FirstOrDefault();
                if (org == "專任助理")
                {
                    _context.Update(aEmployee);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index3", "AEmployees");
                } else
                if (org == "獎助生")
                {
                    _context.Update(aEmployee);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index2", "AEmployees");
                }

                try
                {
                    _context.Update(aEmployee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AEmployeeExists(aEmployee.AId))
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
            return View(aEmployee);
        }

        // GET: AEmployees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aEmployee = await _context.AEmployees
                .FirstOrDefaultAsync(m => m.AId == id);
            if (aEmployee == null)
            {
                return NotFound();
            }

            return View(aEmployee);
        }

        // POST: AEmployees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var aEmployee = await _context.AEmployees.FindAsync(id);
            _context.AEmployees.Remove(aEmployee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult temp()
        {
            return View();
        }


        //下載現有套版word
        public FileResult dword_enrp075_1()
        {
            //Build the File Path.
            string path = Path.Combine(this.Environment.WebRootPath, "Files/dword_enrp075_1.docx");

            //Read the File data into Byte Array.
            byte[] bytes = System.IO.File.ReadAllBytes(path);

            //Send the File to Download.
            return File(bytes, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "dword_enrp075_1.docx");
        }

        //上傳修改後word
        public IActionResult tword_enrp075_1()
        {
            return View();
        }

        [HttpPost]
        public IActionResult tword_enrp075_1(List<IFormFile> files)
        {
            long size = 0;
            foreach (var file in files)
            {
                var filename = ContentDispositionHeaderValue
                .Parse(file.ContentDisposition)
                .FileName
                .Trim('"');
                //這個hostingEnv.WebRootPath就是要存的地址可以改下
                filename = $"{ Environment.WebRootPath}\\{filename}";
                size = file.Length;
                using (FileStream fs = System.IO.File.Create(filename))
                {
                    file.CopyTo(fs);
                    fs.Flush();
                }
            }
            TempData["Alert"] = ("上傳成功");
            return View();
        }

        //原有轉換格式
        public IActionResult cwtp_word_enrp075_1(Models.enrp075_1 model)
        {
            // Load template document.
            var path = Path.Combine(this.environment.WebRootPath, "dword_enrp075_1.docx");
            var document = DocumentModel.Load(path);

            // Execute mail merge process.
            document.MailMerge.Execute(model);

            // Save document in specified file format.
            var stream = new MemoryStream();
            document.Save(stream, model.Options);

            // Download file.
            return File(stream, model.Options.ContentType, $"dword_enrp075_1.{model.Format.ToLower()}");
        }

        //修改後轉換格式
        public IActionResult ncwtp_word_enrp075_1(Models.enrp075_1 model)
        {
            // Load template document.
            var path = Path.Combine(this.environment.WebRootPath, "test.docx");
            var document = DocumentModel.Load(path);

            // Execute mail merge process.
            document.MailMerge.Execute(model);

            // Save document in specified file format.
            var stream = new MemoryStream();
            document.Save(stream, model.Options);

            // Download file.
            return File(stream, model.Options.ContentType, $"test.{model.Format.ToLower()}");
        }


        public FileResult DownloadFile()
        {
            //Build the File Path.
            string path = Path.Combine(this.Environment.WebRootPath, "Files/Person.xlsx");

            //Read the File data into Byte Array.
            byte[] bytes = System.IO.File.ReadAllBytes(path);

            //Send the File to Download.
            return File(bytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Person.xlsx");

        }

        //科技部約用人員聘任簽案
        public FileResult DownloadFile2()
        {
            //Build the File Path.
            string path = Path.Combine(this.Environment.WebRootPath, "Files/hireRequired.docx");

            //Read the File data into Byte Array.
            byte[] bytes = System.IO.File.ReadAllBytes(path);

            //Send the File to Download.
            return File(bytes, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "hireRequired.docx");

        }

        //專任助理契約書
        public FileResult DownloadFile3()
        {
            //Build the File Path.
            string path = Path.Combine(this.Environment.WebRootPath, "Files/contract_assistant.docx");

            //Read the File data into Byte Array.
            byte[] bytes = System.IO.File.ReadAllBytes(path);

            //Send the File to Download.
            return File(bytes, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "contract_assistant.docx");

        }

        //學習活動實施計畫
        public FileResult DownloadFile4()
        {
            //Build the File Path.
            string path = Path.Combine(this.Environment.WebRootPath, "Files/learning_activity_form.docx");

            //Read the File data into Byte Array.
            byte[] bytes = System.IO.File.ReadAllBytes(path);

            //Send the File to Download.
            return File(bytes, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "learning_activity_form.docx");

        }

        //課程學習附課程大綱/指導教授同意書影本
        public FileResult DownloadFile5()
        {
            //Build the File Path.
            string path = Path.Combine(this.Environment.WebRootPath, "Files/learning_assessment_form.docx");

            //Read the File data into Byte Array.
            byte[] bytes = System.IO.File.ReadAllBytes(path);

            //Send the File to Download.
            return File(bytes, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "learning_assessment_form.docx");

        }

        //他校同意書
        public FileResult DownloadFile6()
        {
            //Build the File Path.
            string path = Path.Combine(this.Environment.WebRootPath, "Files/agreement_for_other_school_student.docx");

            //Read the File data into Byte Array.
            byte[] bytes = System.IO.File.ReadAllBytes(path);

            //Send the File to Download.
            return File(bytes, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "agreement_for_other_school_student.docx");

        }

        //兼任助理/臨時工勞動契約
        public FileResult DownloadFile7()
        {
            //Build the File Path.
            string path = Path.Combine(this.Environment.WebRootPath, "Files/part-time-assistant-contract.docx");

            //Read the File data into Byte Array.
            byte[] bytes = System.IO.File.ReadAllBytes(path);

            //Send the File to Download.
            return File(bytes, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "part-time-assistant-contract.docx");

        }

        public IActionResult Index7()
        {
            return View();
        }


        //下載NPOI套件
        public IActionResult Index8()
        {
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Index8(IFormFile file, [Bind("UserName,AName,AGender,AMail,ASalary,AEmploymentDate")] AEmployee aEmployee)
        {
            if (file == null)
            {
                TempData["Alert"] = ("未選擇檔案!");
                TempData["Err"] = "未選擇檔案!";
                return View();
            }
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<ul>");
            foreach (Claim claim in HttpContext.User.Claims)
            {
                sb.AppendLine($@"<li> claim.Value:{claim.Value}</li>");
            }
            sb.AppendLine("</ul>");
            String Bname = HttpContext.User.Claims.FirstOrDefault(m => m.Type == ClaimTypes.Name).Value;
            ViewBag.Bname = Bname;
            ViewBag.msg = sb.ToString();

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                using (var package = new ExcelPackage(stream))
                {
                    if (file == null)
                    {
                        TempData["Alert"] = ("未選擇檔案!");
                        return View();
                    }
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    var startrow = worksheet.Dimension.Start.Row;
                    var rowcount = worksheet.Dimension.End.Row;
                    string msg = "", error = ""; int addcount = 0, errcount = 0;
                    for (startrow = 2; startrow <= rowcount; startrow++)
                    {

                        AEmployee aEmployee1 = new AEmployee()
                        {
                            UserName = worksheet.Cells[startrow, 1].Value?.ToString().Trim(),
                            AName = worksheet.Cells[startrow, 2].Value?.ToString().Trim(),
                            AGender = worksheet.Cells[startrow, 3].Value?.ToString().Trim(),
                            AMail = worksheet.Cells[startrow, 4].Value?.ToString().Trim(),
                            ASalary = Int32.Parse(worksheet.Cells[startrow, 5].Value?.ToString().Trim()),
                            AEmploymentDate = DateTime.Parse(worksheet.Cells[startrow, 6].Value?.ToString().Trim())
                   
                        };
                                                                      
                        var perid = _context.AEmployees.Where(o => o.UserName == worksheet.Cells[startrow, 1].Value.ToString().Trim()).FirstOrDefault();                                              
                        bool asstrue = false;

                        if (asstrue)
                        {
                            error += "第" + (startrow - 1) + "筆考核已存在! ";
                            errcount++;
                        }
                                      
                        else if (perid != null)
                        {
                            error += "第" + (startrow - 1) + "筆資料已存在!";
                            //TempData["Alert"] = ("匯入失敗!\\r請檢查是否重複新增相同的員工編號!");
                            errcount++;
                        }
                        else
                        {
                            _context.AEmployees.Add(aEmployee1);
                            _context.SaveChanges();                           
                            addcount++;
                            TempData["Alert"] = ("匯入成功!");
                        }
                    }
                    msg = "共匯入" + (rowcount - 1) + "筆資料!新增" + addcount + "筆，失敗" + errcount + "筆。";
                    TempData["Msg"] = msg;
                    TempData["Err"] = error;
                    return View();
                }
            }
        }


        //下載匯入計畫資料檔案格式
        public FileResult DownloadFile8()
        {
            //Build the File Path.
            string path = Path.Combine(this.Environment.WebRootPath, "Files/format-plandata.xlsx");

            //Read the File data into Byte Array.
            byte[] bytes = System.IO.File.ReadAllBytes(path);

            //Send the File to Download.
            return File(bytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "format-plandata.xlsx");

        }



        private bool AEmployeeExists(int id)
            {
                return _context.AEmployees.Any(e => e.AId == id);
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
//    public class AEmployeesController : Controller
//    {
//        private readonly PlannedStaffManagementContext _context;

//        public AEmployeesController(PlannedStaffManagementContext context)
//        {
//            _context = context;
//        }

//        // GET: AEmployees
//        public async Task<IActionResult> Index()
//        {
//            return View(await _context.AEmployees.ToListAsync());
//        }

//        // GET: AEmployees/Details/5
//        public async Task<IActionResult> Details(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var aEmployee = await _context.AEmployees
//                .FirstOrDefaultAsync(m => m.AId == id);
//            if (aEmployee == null)
//            {
//                return NotFound();
//            }

//            return View(aEmployee);
//        }

//        // GET: AEmployees/Create
//        public IActionResult Create()
//        {
//            return View();
//        }

//        // POST: AEmployees/Create
//        // To protect from overposting attacks, enable the specific properties you want to bind to.
//        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Create([Bind("AId,UserName,AName,AGender,AMail,ASalary,AEmploymentDate")] AEmployee aEmployee)
//        {
//            if (ModelState.IsValid)
//            {
//                _context.Add(aEmployee);
//                await _context.SaveChangesAsync();
//                return RedirectToAction(nameof(Index));
//            }
//            return View(aEmployee);
//        }

//        // GET: AEmployees/Edit/5
//        public async Task<IActionResult> Edit(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var aEmployee = await _context.AEmployees.FindAsync(id);
//            if (aEmployee == null)
//            {
//                return NotFound();
//            }
//            return View(aEmployee);
//        }

//        // POST: AEmployees/Edit/5
//        // To protect from overposting attacks, enable the specific properties you want to bind to.
//        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Edit(int id, [Bind("AId,UserName,AName,AGender,AMail,ASalary,AEmploymentDate")] AEmployee aEmployee)
//        {
//            if (id != aEmployee.AId)
//            {
//                return NotFound();
//            }

//            if (ModelState.IsValid)
//            {
//                try
//                {
//                    _context.Update(aEmployee);
//                    await _context.SaveChangesAsync();
//                }
//                catch (DbUpdateConcurrencyException)
//                {
//                    if (!AEmployeeExists(aEmployee.AId))
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
//            return View(aEmployee);
//        }

//        // GET: AEmployees/Delete/5
//        public async Task<IActionResult> Delete(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var aEmployee = await _context.AEmployees
//                .FirstOrDefaultAsync(m => m.AId == id);
//            if (aEmployee == null)
//            {
//                return NotFound();
//            }

//            return View(aEmployee);
//        }

//        // POST: AEmployees/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> DeleteConfirmed(int id)
//        {
//            var aEmployee = await _context.AEmployees.FindAsync(id);
//            _context.AEmployees.Remove(aEmployee);
//            await _context.SaveChangesAsync();
//            return RedirectToAction(nameof(Index));
//        }

//        private bool AEmployeeExists(int id)
//        {
//            return _context.AEmployees.Any(e => e.AId == id);
//        }
//    }
//}

