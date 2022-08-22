using LearnASPWithAkichan.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LearnASPWithAkichan.Controllers
{
    public class AccountsController : Controller
    {
        string folder = Environment.CurrentDirectory.ToString() + "/";
        string fileName = "CacKyDK.txt";

        private readonly regist_courseContext _context;
        public AccountsController(regist_courseContext context)
        {
            _context = context;
        }
        [Route("")]
        [Route("DangNhap")]
        public IActionResult Index()
        {
           
            if (HttpContext.Session.GetString("username") != null)
            {
                HttpContext.Session.Remove("username");
                HttpContext.Session.Remove("role");
                HttpContext.Session.Remove("trongKyDangKy");
            }

            return View();
        }

        public bool CheckTrongKyDangKy()
        {
            string fullPath = folder + fileName;

            string[] readText = System.IO.File.ReadAllLines(fullPath);
            List<KyDangKy> kyDangKyLst = new List<KyDangKy>();

            for (int i = 0; i < readText.Length; i++)
            {
                string[] splittedDate = readText[i].Split('-', StringSplitOptions.None);

                KyDangKy kdk = new KyDangKy();
                kdk.BeginDate = DateTime.Parse(splittedDate[0]);
                kdk.EndDate = DateTime.Parse(splittedDate[1]);
                kyDangKyLst.Add(kdk);

            }

            foreach (var item in kyDangKyLst)
            {
                if (DateTime.Compare(item.BeginDate,DateTime.Now) <0 && DateTime.Compare(item.EndDate, DateTime.Now) > 0)
                {
                    return true;
                }

                
            }

            return false;
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
                HttpContext.Session.SetString("trongKyDangKy", CheckTrongKyDangKy().ToString());
                
                if (account.PassWord.Equals(a.PassWord))
                {
                    if (account.Role)
                    {
                        var studentInfo = _context.Students.FirstOrDefault(s => s.AccountId == account.Id);
                        HttpContext.Session.SetString("studentInfo", studentInfo.Name);

                        if (CheckTrongKyDangKy())
                        {
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            return RedirectToAction("HocPhanDaDangKy", "Home");
                        }
                       
                    }
                    else
                    {
                        HttpContext.Session.SetString("studentInfo", "Quản trị viên");
                        return RedirectToAction("QuanLyKyDK", "Admin");
                    }
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
