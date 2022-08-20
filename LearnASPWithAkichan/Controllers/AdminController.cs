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
            return View();
        }

        public IActionResult QuanLyMonHoc()
        {
            return View();
        }

        public IActionResult QuanLyLopHocPhan()
        {
            return View();
        }
    }
}
