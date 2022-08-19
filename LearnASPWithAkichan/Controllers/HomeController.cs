using LearnASPWithAkichan.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace LearnASPWithAkichan.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly regist_courseContext _db;

        public HomeController(ILogger<HomeController> logger, regist_courseContext db)
        {
            _logger = logger;
            _db = db;
        }

/*        public IActionResult Index()
        {
            return View();
        }*/

        public async Task<IActionResult> Index()
        {
            var regist_courseContext = _db.Students;
            ViewBag.Test = "Aki chan";
            return View(await regist_courseContext.ToListAsync());
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}