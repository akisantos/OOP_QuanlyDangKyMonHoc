using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LearnASPWithAkichan.Models;

namespace LearnASPWithAkichan.Controllers
{
    public class ClassSessionsController : Controller
    {
        private readonly regist_courseContext _context;

        public ClassSessionsController(regist_courseContext context)
        {
            _context = context;
        }

        // GET: ClassSessions
        public async Task<IActionResult> Index()
        {
            var regist_courseContext = _context.ClassSessions.Include(c => c.Department).Include(c => c.Subject);
            return View(await regist_courseContext.ToListAsync());
        }

        // GET: ClassSessions/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.ClassSessions == null)
            {
                return NotFound();
            }

            var classSession = await _context.ClassSessions
                .Include(c => c.Department)
                .Include(c => c.Subject)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (classSession == null)
            {
                return NotFound();
            }

            return View(classSession);
        }

        // GET: ClassSessions/Create
        public IActionResult Create()
        {
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Name", "Id");
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "Name", "Id");
            return View();
        }

        // POST: ClassSessions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Amount,PointClass,PointMid,PointEnd,Active,BeginDate,EndDate,CommonClass,DepartmentId,SubjectId")] ClassSession classSession)
        {
            if (ModelState.IsValid)
            {
                _context.Add(classSession);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Id", classSession.DepartmentId);
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "Id", "Id", classSession.SubjectId);
            return View(classSession);
        }

        // GET: ClassSessions/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.ClassSessions == null)
            {
                return NotFound();
            }

            var classSession = await _context.ClassSessions.FindAsync(id);
            if (classSession == null)
            {
                return NotFound();
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Id", classSession.DepartmentId);
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "Id", "Id", classSession.SubjectId);
            return View(classSession);
        }

        // POST: ClassSessions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Amount,PointClass,PointMid,PointEnd,Active,BeginDate,EndDate,CommonClass,DepartmentId,SubjectId")] ClassSession classSession)
        {
            if (id != classSession.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(classSession);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClassSessionExists(classSession.Id))
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
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Id", classSession.DepartmentId);
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "Id", "Id", classSession.SubjectId);
            return View(classSession);
        }

        // GET: ClassSessions/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.ClassSessions == null)
            {
                return NotFound();
            }

            var classSession = await _context.ClassSessions
                .Include(c => c.Department)
                .Include(c => c.Subject)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (classSession == null)
            {
                return NotFound();
            }

            return View(classSession);
        }

        // POST: ClassSessions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.ClassSessions == null)
            {
                return Problem("Entity set 'regist_courseContext.ClassSessions'  is null.");
            }
            var classSession = await _context.ClassSessions.FindAsync(id);
            if (classSession != null)
            {
                _context.ClassSessions.Remove(classSession);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClassSessionExists(string id)
        {
          return (_context.ClassSessions?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
