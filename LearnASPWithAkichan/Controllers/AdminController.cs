using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LearnASPWithAkichan.Models;
using Microsoft.AspNetCore.Http;

namespace LearnASPWithAkichan.Controllers
{
    public class AdminController : Controller
    {

        string folder = @"D:\";
        string fileName = "CacKyDK.txt";


        private readonly regist_courseContext _context;
        public AdminController(regist_courseContext context)
        {
            _context = context;
        }

        [Route("/Admin")]
        public IActionResult QuanLyKyDK()
        {
            string fullPath = folder + fileName;

            string[] readText = System.IO.File.ReadAllLines(fullPath);
            List<KyDangKy> kyDangKyLst = new List<KyDangKy>();

            for (int i = 0; i < readText.Length; i++)
            {
                string[] splittedDate = readText[i].Split('-', StringSplitOptions.None);

                KyDangKy kdk = new KyDangKy();
                kdk.BeginDate = DateTime.Parse(splittedDate[0]);
                kdk.EndDate = DateTime.Parse(splittedDate[1]);
                kyDangKyLst.Add(kdk);

            }
           
            return View(kyDangKyLst);
        }


        public IActionResult ThemKyDangKy()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> TaoKyDangKy([Bind("BeginDate,EndDate")] KyDangKy kyDangKy)
        {
            if (kyDangKy != null)
            {
                string fullPath = folder + fileName;
                await using (StreamWriter writer = new StreamWriter(fullPath, true))
                {
                    writer.WriteLine(kyDangKy.BeginDate +"-"+ kyDangKy.EndDate);
                }
                return RedirectToAction(nameof(QuanLyKyDK));
            }
            TempData["notification"] = "Vô lý";
            return View("ThemKyDangKy");
        }

        //Quản lý môn học
        public async Task<IActionResult> QuanLyMonHoc()
        {
            var regist_courseContext = _context.Subjects.Include(s => s.Department);
            return View(await regist_courseContext.ToListAsync());
        }

        public IActionResult TaoMonHoc()
        {
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Id");
            return View();
        }

        // Return View chỉnh sửa môn học.
/*        public IActionResult ChinhSuaMonHoc(string id)
        {
            var subject = _context.Subjects.FirstOrDefault(x => x.Id == id);
            var department = _context.Departments.FirstOrDefault(x => x.Id == subject.DepartmentId);
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Id", subject.DepartmentId);
            if (subject != null)
            {

                return View(subject);
            }
            TempData["notification"] = "Lỗi!";
            return View();
        }*/

        // Trả về giao diện quản lý học lớp học phần
        public async Task<IActionResult> QuanLyLopHocPhan()
        {
            var regist_courseContext = _context.ClassSessions.Include(s => s.Department).Include(s => s.Subject);

            return View(await regist_courseContext.ToListAsync());
        }

        // 


        public IActionResult TaoLopLopHocPhan()
        {
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name");
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "Id", "Name");
            return View();
        }

        // Tạo lớp học phần.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> create_class_session([Bind("Id,Amount,PointClass,PointMid,PointEnd,Active,BeginDate,EndDate,CommonClass,DepartmentId,SubjectId")] ClassSession classSession)
        {
            var subject = _context.Subjects.FirstOrDefault(x =>x.Id == classSession.SubjectId);
            ClassSession class_sesion = new ClassSession()
            {
                Id = classSession.Id,
                Amount = classSession.Amount,
                Active = true,
                CommonClass = classSession.CommonClass,
                DepartmentId = classSession.DepartmentId,
                SubjectId = classSession.SubjectId,
                BeginDate = classSession.BeginDate,
                EndDate = classSession.EndDate,
                PointClass = 0, 
                PointMid = 0,
                PointEnd = 0,
                Subject = subject
            };
            _context.ClassSessions.Add(class_sesion);
            await _context.SaveChangesAsync();
            return View("QuanLyLopHocPhan");

        }


        public IActionResult ChinhSuaLopHocPhan(string id)
        {
            var class_sesion = _context.ClassSessions.FirstOrDefault(c => c.Id == id);
            if (class_sesion == null)
            {
                TempData["notification"] = "Xảy ra sự cố, vui lòng quay lại sau! ";
                return View();
            }
            else
            {
                return View(class_sesion);
            }
        }

        // tạo môn học
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSubject([Bind("Id,Name,Credits,DepartmentId")] Subject subject)
        {
            if (ModelState.IsValid)
            {
                if (!check_unique(subject.Id))
                {
                    _context.Add(subject);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["notification"] = "Môn học đã tồn tại";
                    return View("TaoMonHoc");
                }
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Id", subject.DepartmentId);
            return View(subject);
        }

        // edit môn học
        /*[HttpGet]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id)
        {
            if (ModelState.IsValid)
            {
                var subject = _context.Subjects.FirstOrDefault(x => x.Id == id);
                if (subject == null)
                {
                    TempData["notification"] = "Cập nhật thất bại";
                    return View("TaoMonHoc");

                }
                else
                {
                    subject.Name = Request.Form["Name"];
                    subject.Credits = int.Parse(Request.Form["Credits"]);
                    _context.Update(subject);
                    await _context.SaveChangesAsync();
                    return View(subject);
                }
            }
            TempData["notification"] = "Có sự cố, vui lòng quay lại sau";
            return View();
        }*/

        public async Task<IActionResult> EditMonHoc(string id)
        {
            if (id == null || _context.Subjects == null)
            {
                return NotFound();
            }

            var subject = await _context.Subjects.FindAsync(id);
            if (subject == null)
            {
                return NotFound();
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Id", subject.DepartmentId);
            return View("ChinhSuaMonHoc", subject);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditMonHoc(string id, [Bind("Id,Name,Credits,DepartmentId")] Subject subject)
        {
            if (id != subject.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(subject);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubjectExists(subject.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("QuanLyMonHoc");
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Id", subject.DepartmentId);
            return View("ChinhSuaMonHoc", subject);
            
        }

        // Edit lớp học phần
        public async Task<IActionResult> EditLHP(string id)
        {
            if (id == null || _context.ClassSessions == null)
            {
                return NotFound();
            }

            var classSession = await _context.ClassSessions.FindAsync(id);
            if (classSession == null)
            {
                return NotFound();
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Id", classSession.DepartmentId);
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "Id", "Id", classSession.SubjectId);
            return View("ChinhSuaLopHocPhan", classSession);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditLHP(string id, [Bind("Id,Amount,PointClass,PointMid,PointEnd,Active,BeginDate,EndDate,CommonClass,DepartmentId,SubjectId")] ClassSession classSession)
        {
            if (id != classSession.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(classSession);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClassSessionExists(classSession.Id))
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
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Id", classSession.DepartmentId);
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "Id", "Id", classSession.SubjectId);
            return View(classSession);
        }




        // Check unique
        public bool check_unique(string id)
        {
            return (_context.Subjects.Any(e => e.Id == id));
        }

        private bool SubjectExists(string id)
        {
            return (_context.Subjects?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public async Task<IActionResult> TimKiemMonHoc(string timKiemString)
        {

            if (!String.IsNullOrEmpty(timKiemString))
            {
                var regist_courseContext = _context.Subjects.Include(s => s.Department);
                var monHocTheoId = regist_courseContext.Where(s => s.Id.Contains(timKiemString));
                var monHocTheoTen = regist_courseContext.Where(s => s.Name.Contains(timKiemString));
                var monHocKhoa = regist_courseContext.Where(s => s.Department.Id.Contains(timKiemString));

                if (monHocTheoId.Count() > monHocTheoTen.Count() && monHocTheoId.Count() > monHocKhoa.Count())
                {
                    TempData["inputSearchValue"] = timKiemString;
                    return View("QuanLyMonHoc", await monHocTheoId.ToListAsync());

                } else if (monHocTheoTen.Count() > monHocTheoId.Count() && monHocTheoId.Count() > monHocKhoa.Count())
                {
                    TempData["inputSearchValue"] = timKiemString;
                    return View("QuanLyMonHoc", await monHocTheoTen.ToListAsync());
                }
                else
                {
                    TempData["inputSearchValue"] = timKiemString;
                    return View("QuanLyMonHoc", await monHocKhoa.ToListAsync());
                }
    
                
            }
            var regist_courseContext2 = _context.Subjects.Include(s => s.Department);
            return View("QuanLyMonHoc", await regist_courseContext2.ToListAsync());
        }

        public async Task<IActionResult> TimKiemLopHocPhan(string timKiemString)
        {

            if (!String.IsNullOrEmpty(timKiemString))
            {
                var regist_courseContext = _context.ClassSessions.Include(s => s.Department).Include(s => s.Subject);
                var monHocTheoId = regist_courseContext.Where(s => s.Id.Contains(timKiemString));
                var monHocKhoa = regist_courseContext.Where(s => s.Department.Name.Contains(timKiemString));

                if (monHocTheoId.Count() > monHocKhoa.Count())
                {
                    TempData["inputSearchValue"] = timKiemString;
                    return View("QuanLyLopHocPhan", await monHocTheoId.ToListAsync());

                }
                else
                {
                    TempData["inputSearchValue"] = timKiemString;
                    return View("QuanLyLopHocPhan", await monHocKhoa.ToListAsync());
                }


            }
            var regist_courseContext2 = _context.ClassSessions.Include(s => s.Department).Include(s => s.Subject);
            return View("QuanLyLopHocPhan", await regist_courseContext2.ToListAsync());
        }

        private bool ClassSessionExists(string id)
        {
            return (_context.ClassSessions?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }

  
}