using Microsoft.AspNetCore.Mvc;

namespace LearnASPWithAkichan.Controllers
{
    public class AccountsController : Controller
    {
        [Route("")]
        [Route("DangNhap")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
