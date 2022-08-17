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
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

namespace LearnASPWithAkichan.Controllers
{
    public class AccountsController : Controller
    {
        private readonly registrion_course2Context _context;

        public AccountsController(registrion_course2Context context)
        {
            _context = context;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DangNhap(Account a)
        {
            if (ModelState.IsValid)
            {
                ViewBag.ThongBao = "Sai tk ";
                var acc = await _context.Accounts.FirstOrDefaultAsync(
                    x => x.Username == a.Username && x.Password == a.Password);
                if (acc != null)
                {
                    var claims = new List<Claim>();
                    if(acc.Role == 1)
                    {
                        claims.Add(new Claim(ClaimTypes.Name, "Admin"));
                        claims.Add(new Claim("Admin", "true"));
                        return RedirectToAction("Index", "Subjects");
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
                    HttpContext.Session.SetString("username", acc.Username);
                }
                else return View("Login");
            }
            return RedirectToAction("Index", "Home");

        }
        //login
        public IActionResult Login()
        {
            return View();
        }
        //Logout
        public IActionResult Logout()
        {
            return View();
        }

        // GET: Accounts
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Index()
        {
            var registrion_course2Context = _context.Accounts.Include(a => a.Student);
            return View(await registrion_course2Context.ToListAsync());
        }

        // GET: Accounts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Accounts == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts
                .Include(a => a.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // GET: Accounts/Create
        public IActionResult Create()
        {
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Id");
            return View();
        }

        // POST: Accounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Username,Password,Role,StudentId")] Account account)
        {
            if (ModelState.IsValid)
            {
                _context.Add(account);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Id", account.StudentId);
            return View(account);
        }

        // GET: Accounts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Accounts == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Id", account.StudentId);
            return View(account);
        }

        // POST: Accounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Username,Password,Role,StudentId")] Account account)
        {
            if (id != account.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(account);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccountExists(account.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Id", account.StudentId);
            return View(account);
        }

        // GET: Accounts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Accounts == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts
                .Include(a => a.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // POST: Accounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Accounts == null)
            {
                return Problem("Entity set 'registrion_course2Context.Accounts'  is null.");
            }
            var account = await _context.Accounts.FindAsync(id);
            if (account != null)
            {
                _context.Accounts.Remove(account);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AccountExists(int id)
        {
          return (_context.Accounts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
