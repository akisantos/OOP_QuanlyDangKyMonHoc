using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LearnASPWithAkichan.Models;

namespace LearnASPWithAkichan.Controllers
{
    public class AdminController : Controller
    {
        private readonly regist_courseContext _context;
        public AdminController(regist_courseContext context)
        {
            _context = context;
        }

        [Route("/Admin")]
        public IActionResult QuanLyKyDK()
        {

            return View("Index");
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
        public IActionResult ChinhSuaMonHoc(string id)
        {
            var subject = _context.Subjects.FirstOrDefault(x => x.Id == id);
            var department = _context.Departments.FirstOrDefault(x => x.Id == subject.DepartmentId);
            if (subject != null)
            {
                TempData["department_name"] = department.Name;
                return View(subject);
            }
            return View();
        }

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
        public async Task<IActionResult> create_class_session([Bind("Id,Amount,BeginDate,EndDate,CommonClass,DepartmentId,SubjectId")] ClassSession classSession)
        {
            
            if (!ModelState.IsValid)
            {
                TempData["notification"] = "Xảy ra sự cố !";
                return View(nameof(TaoLopLopHocPhan));
            }
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
                PointEnd = 0
            };
            _context.ClassSessions.Add(class_sesion);
            await _context.SaveChangesAsync();
            return View(nameof(QuanLyLopHocPhan));

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
        public async Task<IActionResult> CreateSubject([Bind("Id,Name,Credits,Department")] Subject subject)
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

        // tạo môn học
        [HttpGet]
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
        }




        // Check unique
        public bool check_unique(string id)
        {
            return (_context.Subjects.Any(e => e.Id == id));
        }


    }
}