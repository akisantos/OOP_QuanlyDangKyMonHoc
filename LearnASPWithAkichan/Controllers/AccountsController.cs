using LearnASPWithAkichan.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LearnASPWithAkichan.Controllers
{
    public class AccountsController : Controller
    {
        private readonly regist_courseContext _context;
        public AccountsController(regist_courseContext context)
        {
            _context = context;
        }
        [Route("")]
        [Route("DangNhap")]
        public IActionResult Index()
        {
            return View();
        }


        // đăng nhập
        // parameter username && password
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("login")]
        public async Task<IActionResult> login(Account a)
        {
            var account =  _context.Accounts.FirstOrDefault(x => x.UserName == a.UserName);
            if (account == null)
            {
                TempData["notification"] = "Tài khoản không hợp lệ";
                return RedirectToAction("Index", "Accounts");
            }
            else
            {
                HttpContext.Session.SetString("username", account.UserName.ToString());
                HttpContext.Session.SetString("role", account.Role.ToString());
                if(account.PassWord.Equals(a.PassWord) && account.Role == true)
                {
                    return RedirectToAction("Index", "Home");
                }
                else if(account.PassWord.Equals(a.PassWord) && account.Role == false)
                {
                    return RedirectToAction("QuanLyKyDK","Admin");
                }
                else
                {
                    TempData["notification"] = "Sai mật khẩu, vui lòng đăng nhập lại";
                    return RedirectToAction("Index", "Accounts");
                }

            }
        }


    }
}
