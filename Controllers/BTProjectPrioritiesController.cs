using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TheBugTracker.Data;
using TheBugTracker.Models;

namespace TheBugTracker.Controllers
{
    public class BTProjectPrioritiesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BTProjectPrioritiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BTProjectPriorities
        public async Task<IActionResult> Index()
        {
            return View(await _context.ProjectPriorities.ToListAsync());
        }

        // GET: BTProjectPriorities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bTProjectPriority = await _context.ProjectPriorities
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bTProjectPriority == null)
            {
                return NotFound();
            }

            return View(bTProjectPriority);
        }

        // GET: BTProjectPriorities/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BTProjectPriorities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] BTProjectPriority bTProjectPriority)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bTProjectPriority);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bTProjectPriority);
        }

        // GET: BTProjectPriorities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bTProjectPriority = await _context.ProjectPriorities.FindAsync(id);
            if (bTProjectPriority == null)
            {
                return NotFound();
            }
            return View(bTProjectPriority);
        }

        // POST: BTProjectPriorities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] BTProjectPriority bTProjectPriority)
        {
            if (id != bTProjectPriority.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bTProjectPriority);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BTProjectPriorityExists(bTProjectPriority.Id))
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
            return View(bTProjectPriority);
        }

        // GET: BTProjectPriorities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bTProjectPriority = await _context.ProjectPriorities
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bTProjectPriority == null)
            {
                return NotFound();
            }

            return View(bTProjectPriority);
        }

        // POST: BTProjectPriorities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bTProjectPriority = await _context.ProjectPriorities.FindAsync(id);
            _context.ProjectPriorities.Remove(bTProjectPriority);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BTProjectPriorityExists(int id)
        {
            return _context.ProjectPriorities.Any(e => e.Id == id);
        }
    }
}
