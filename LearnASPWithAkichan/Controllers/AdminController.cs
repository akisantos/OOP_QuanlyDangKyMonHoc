using Microsoft.AspNetCore.Mvc;

namespace LearnASPWithAkichan.Controllers
{
    public class AdminController : Controller
    {
        [Route("Admin")]
        [Route("Admin/QuanLyKyDangKy")]
        public IActionResult Index()
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
