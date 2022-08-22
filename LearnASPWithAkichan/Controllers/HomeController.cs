using LearnASPWithAkichan.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace LearnASPWithAkichan.Controllers
{
       // [Authorize(Policy = "Student")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly regist_courseContext _db;

        public HomeController(ILogger<HomeController> logger, regist_courseContext db)
        {
            _logger = logger;
            _db = db;
        }
        
       
        public IActionResult Login()
        {
            return RedirectToAction("Index","Accounts");
        }
        /*******/
        public async Task<IActionResult> Index()
        {
            string username = HttpContext.Session.GetString("username");
            var acc = _db.Accounts.FirstOrDefault(x => x.UserName == username);
            var stu = _db.Students.FirstOrDefault(x => x.AccountId == acc.Id);
            //
            var k = _db.Departments.FirstOrDefault(x => x.Id == stu.DepartmentId);
            var sub = _db.Subjects.FirstOrDefault(x => x.DepartmentId == stu.DepartmentId);
            var listSub = from s in _db.Subjects where s.DepartmentId == k.Id select s;
            //

            TempData["checker"] = "Đăng ký";
            return View(await listSub.ToListAsync());
        }
        /*******/
        // Xem danh sách lớp học phần đã đăng ký
        public async Task<IActionResult> HocPhanDaDangKy()
        {
            string username = HttpContext.Session.GetString("username");
            var acc = _db.Accounts.FirstOrDefault(x => x.UserName == username);
            var stuID = _db.Students.FirstOrDefault(x => x.AccountId == acc.Id);
            //
            var regTest = _db.RegistClasses.Where(t => t.StudentId == stuID.Id);
            //
            return View(await regTest.ToListAsync());
        }
        //Đăng ký lớp học phần 
        public IActionResult add(string id)
        {
            string username = HttpContext.Session.GetString("username");
            var acc = _db.Accounts.FirstOrDefault(x => x.UserName == username);
            var clas = _db.ClassSessions.FirstOrDefault(x => x.Id == id);
            //
            var sub = _db.Subjects.FirstOrDefault(x => x.Id == clas.SubjectId);
            var stu = _db.Students.FirstOrDefault(x => x.AccountId == acc.Id);
            //
            if (checkClass(id) == true)
            {
                RegistClass rgclass = new RegistClass()
                {
                    StudentId = stu.Id,
                    ClassSessionId = clas.Id,
                    Status = true,
                    RegistDate = DateTime.Now,
                    Credits = sub.Credits
                };
                _db.RegistClasses.Add(rgclass);
                _db.SaveChanges();
                return RedirectToAction("HocPhanDaDangKy", "Home");
            }
            else
            {
                return RedirectToAction("DanhSachLop", "Home");
            }
            
        }
        // ---> check học phân đã đăng ký ()
        public bool checkClass(string id)
        {
            string username = HttpContext.Session.GetString("username");
            var acc = _db.Accounts.FirstOrDefault(x => x.UserName == username);
            var clas = _db.ClassSessions.FirstOrDefault(x => x.Id == id);
            //
            var sub = _db.Subjects.FirstOrDefault(x => x.Id == clas.SubjectId);
            var stu = _db.Students.FirstOrDefault(x => x.AccountId == acc.Id);
            //
            var regclass = _db.RegistClasses.FirstOrDefault(x => x.StudentId == stu.Id && x.ClassSessionId == clas.Id);
            if (regclass != null)
            {
                return false;   
            }
            else
                return true;
        }
        // ---> lớp học phần trong kế hoạch | ngoài kế hoạch
        // ---> check môn tuyên quyết
        // ---> hủy đăng ký
        /*******/
        public async Task<IActionResult> DanhSachLop(String id)
        {
            var list = from cla in _db.ClassSessions where cla.SubjectId == id select cla;
            return View(await list.ToListAsync());
            
        } 
         
        public IActionResult ThongBao()
        { 
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}