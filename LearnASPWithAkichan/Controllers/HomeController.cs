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
            var k = _db.Departments.FirstOrDefault(x => x.Id == stu.DepartmentId);
            var sub = _db.Subjects.FirstOrDefault(x => x.DepartmentId == stu.DepartmentId);
            var listSub = from s in _db.Subjects where s.DepartmentId == k.Id select s;
            return View(listSub.ToList());
        }
        /*******/
        // Dang ky lop hoc phan hoc phan
        public IActionResult HocPhanDaDangKy()
        {
            return View();
        }
        
        /*******/
        
        public async Task<IActionResult> DanhSachLop(String id)
        {
            var list = from cla in _db.ClassSessions where cla.SubjectId == id select cla;
            return View(list.ToList());
            //var regist_courseContext = _db.ClassSessions.Include(s => s.Department);
            //return View("DSLopHocPhan", await regist_courseContext.ToListAsync());
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