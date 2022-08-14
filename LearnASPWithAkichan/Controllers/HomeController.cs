using LearnASPWithAkichan.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LearnASPWithAkichan.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;


        private Account aki = new Account();

        private List<Student> akiList = new List<Student>();

        void LoadStudent()
        {
            for (int i = 0; i < 10; i++)
            {
                Student aki = new Student();
                akiList.Add(aki);
            }
        }        

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            LoadStudent();
            ViewData["Messages"] = aki;
            ViewBag.Student = akiList;
            return View();
        }

        public IActionResult Login()
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