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
        public IActionResult Index()
        {
            //var subjectLst = _db._db.Subjects.ToList();
            return View();
        }

        public IActionResult Login()
        {
            return RedirectToAction("Index","Accounts");
        }

        public IActionResult HocPhanDaDangKy()
        {
            return View();
        }

        public IActionResult ThongBao()
        {
            return View();
        }

        public IActionResult DSLopHocPhan()
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