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



    }
}
