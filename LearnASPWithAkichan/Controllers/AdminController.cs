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

        string folder = Environment.CurrentDirectory.ToString()+"/"; 
        string fileName = "CacKyDK.txt";
        private bool RoleCheckByHand;

        private readonly regist_courseContext _context;
        public AdminController(regist_courseContext context)
        {
            _context = context;
        }

        [Route("/Admin")]
        public IActionResult QuanLyKyDK()
        {
            if (HttpContext.Session.GetString("role") == "True")
            {
                RoleCheckByHand = true;
            }
            else
            {
                RoleCheckByHand = false;
            }
            
            if (!RoleCheckByHand)
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
                TempData["notification"] = "Thêm, xoá kỳ đăng ký";
                return View(kyDangKyLst);
            }
            
            return RedirectToAction("Index", "Home");
        }


        public IActionResult ThemKyDangKy()
        {
            if (!RoleCheckByHand)
            {
                return View();
            }

            return RedirectToAction("Index","Home");

        }

        [HttpPost]
        public async Task<IActionResult> TaoKyDangKy([Bind("BeginDate,EndDate")] KyDangKy kyDangKy)
        {
            if (!RoleCheckByHand)
            {
                if (kyDangKy != null && DateTime.Compare(kyDangKy.BeginDate, kyDangKy.EndDate) < 0)
                {
                    string fullPath = folder + fileName;
                    await using (StreamWriter writer = new StreamWriter(fullPath, true))
                    {
                        writer.WriteLine(kyDangKy.BeginDate + "-" + kyDangKy.EndDate);
                    }
                    return RedirectToAction(nameof(QuanLyKyDK));
                }
                TempData["notification"] = "Vô lý! ";
                return View("ThemKyDangKy");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public IActionResult XoaKyDangKy(KyDangKy kyDangKy)
        {
            if (!RoleCheckByHand)
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

                int j = 0;
                foreach (KyDangKy item in kyDangKyLst)
                {

                    if (item.BeginDate == kyDangKy.BeginDate && item.EndDate == kyDangKy.EndDate)
                    {


                        var tempFile = Path.GetTempFileName();
                        var linesToKeep = System.IO.File.ReadLines(fullPath).Where(l => l != readText[j]);
                        System.IO.File.WriteAllLines(tempFile, linesToKeep);
                        System.IO.File.Delete(fullPath);
                        System.IO.File.Move(tempFile, fullPath);
                        kyDangKyLst.Remove(item);
                        break;
                    }
                    j++;
                }

                return View("QuanLyKyDK", kyDangKyLst);
            }

            return RedirectToAction("Index", "Home");
        }

        //Quản lý môn học
        public async Task<IActionResult> QuanLyMonHoc()
        {
            if (!RoleCheckByHand)
            {
                var regist_courseContext = _context.Subjects.Include(s => s.Department);
                return View(await regist_courseContext.ToListAsync());
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult TaoMonHoc()
        {
            if (!RoleCheckByHand)
            {
                ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Id");
                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        // Return View chỉnh sửa môn học.


        // Trả về giao diện quản lý học lớp học phần
        public async Task<IActionResult> QuanLyLopHocPhan()
        {
            if (!RoleCheckByHand)
            {
                var regist_courseContext = _context.ClassSessions.Include(s => s.Department).Include(s => s.Subject);

                return View(await regist_courseContext.ToListAsync());
            }

            return RedirectToAction("Index", "Home");
        }

        // 


        public IActionResult TaoLopLopHocPhan()
        {

            if (!RoleCheckByHand)
            {
                ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name");
                ViewData["SubjectId"] = new SelectList(_context.Subjects, "Id", "Name");
                return View();
            }

            return RedirectToAction("Index", "Home");
        }

        // Tạo lớp học phần.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> create_class_session([Bind("Id,Amount,PointClass,PointMid,PointEnd,Active,BeginDate,EndDate,CommonClass,DepartmentId,SubjectId")] ClassSession classSession)
        {
            if (!RoleCheckByHand)
            {
                var subject = _context.Subjects.FirstOrDefault(x => x.Id == classSession.SubjectId);
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

            return RedirectToAction("Index", "Home");

        }


        

        // tạo môn học
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSubject([Bind("Id,Name,Credits,DepartmentId")] Subject subject)
        {
            if (!RoleCheckByHand)
            {
                if (ModelState.IsValid)
                {
                    if (!check_unique(subject.Id))
                    {
                        _context.Add(subject);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(QuanLyMonHoc));
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

            return RedirectToAction("Index", "Home");
        }

        // edit môn học
    

        public async Task<IActionResult> EditMonHoc(string id)
        {
            if (!RoleCheckByHand)
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
                ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name", subject.DepartmentId);
                return View("ChinhSuaMonHoc", subject);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditMonHoc(string id, [Bind("Id,Name,Credits,DepartmentId")] Subject subject)
        {
            if (!RoleCheckByHand)
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
                ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name", subject.DepartmentId);
                return View("ChinhSuaMonHoc", subject);
            }

            return RedirectToAction("Index", "Home");
        }

        // Edit lớp học phần

        public async Task<IActionResult> EditLHP(string id)
        {
            if (!RoleCheckByHand)
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

            return RedirectToAction("Index", "Home");
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditLHP(string id, [Bind("Id,Amount,PointClass,PointMid,PointEnd,Active,BeginDate,EndDate,CommonClass,DepartmentId,SubjectId")] ClassSession classSession)
        {
            if (!RoleCheckByHand)
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
                    return RedirectToAction("QuanLyLopHocPhan", "Admin");
                }
                ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name", classSession.DepartmentId);
                ViewData["SubjectId"] = new SelectList(_context.Subjects, "Id", "Name", classSession.SubjectId);
                return View("ChinhSuaLopHocPhan", classSession);
            }

            return RedirectToAction("Index", "Home");
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

            if (!RoleCheckByHand)
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

                    }
                    else if (monHocTheoTen.Count() > monHocTheoId.Count() && monHocTheoId.Count() > monHocKhoa.Count())
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
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> TimKiemLopHocPhan(string timKiemString)
        {

            if (!RoleCheckByHand)
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

            return RedirectToAction("Index", "Home");
        }

        private bool ClassSessionExists(string id)
        {
            return (_context.ClassSessions?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }

  
}