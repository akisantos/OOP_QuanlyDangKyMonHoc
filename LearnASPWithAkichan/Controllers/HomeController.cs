using LearnASPWithAkichan.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LearnASPWithAkichan.Controllers
{
    public class HomeController : Controller
    {
        string folder = @"D:\";
        string fileName = "CacKyDangKy.txt";

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return RedirectToAction("DangKy","Subjects");
        }
        public IActionResult Login()
        {
            return View();  
        }

        public IActionResult ThongBao()
        {
            return View();
        }

        [Authorize(Policy = "Admin")]
        public IActionResult QuanLyKyDangKy()
        {
            string fullPath = folder + fileName;
            string[] data = System.IO.File.ReadAllLines(fullPath);
            string[] thoiGianBatDau = new string[data.Length];
            string[] thoiGianKetThuc = new string[data.Length];
            for (int i = 0; i < data.Length; i++)
            {
                string[] time = data[i].Split("-");
                thoiGianBatDau[i] = time[0];
                thoiGianKetThuc[i] = time[1];

            }
            ViewBag.StartTime = thoiGianBatDau;
            ViewBag.EndTime = thoiGianKetThuc;
            return View();
        }

        [Authorize(Policy = "Admin")]
        public IActionResult TaoKyDangKy()
        {
            return View();
        }

        [HttpPost]

        [Authorize(Policy = "Admin")]
        public IActionResult Admin_TaoKyDangKy(DateTime thoiGianBatDau, DateTime thoiGianKetThuc)
        {
            string s = thoiGianBatDau + "-" + thoiGianKetThuc;
            //Vi tri Luu file Ky dang ky
            string fullPath = folder + fileName;
            using (StreamWriter writer = new StreamWriter(fullPath))
            {
                writer.WriteLine(s);

            }
            return View("QuanLyKyDangKy");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}