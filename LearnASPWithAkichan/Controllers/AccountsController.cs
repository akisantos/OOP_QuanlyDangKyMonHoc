using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LearnASPWithAkichan.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Account a)
        {
            if (ModelState.IsValid)
            {
                var acc = /*await*/ _context.Accounts.FirstOrDefault(x => x.UserName == a.UserName && x.PassWord == a.PassWord);
                if(acc == null)
                {
                    ViewBag.Thongbao = "Sai tk hoac";
                }
                else
                {
                    var claims = new List<Claim>();
                    if (acc.Role == false)
                    {
                        claims.Add(new Claim(ClaimTypes.Name, "Admin"));
                        claims.Add(new Claim("Admin", "false"));
                        return RedirectToAction("QuanLyKyDK", "Admin");
                    }
                    else
                    {
                        claims.Add(new Claim(ClaimTypes.Name, "Student"));
                        claims.Add(new Claim("Student", "true"));
                        return RedirectToAction("Index", "Home");
                    }
                    var Identity = new ClaimsIdentity(claims, "CookieAuth");
                    var Principal = new ClaimsPrincipal(Identity);
                    await HttpContext.SignInAsync("CookieAuth", Principal);
                    HttpContext.Session.SetString("username", acc.UserName);
                }
            }
            return RedirectToAction("Erro", "Home");

        } 
             
    }
}
