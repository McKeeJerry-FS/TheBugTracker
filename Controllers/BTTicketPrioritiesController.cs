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
    public class BTTicketPrioritiesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BTTicketPrioritiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BTTicketPriorities
        public async Task<IActionResult> Index()
        {
            return View(await _context.TicketPriorities.ToListAsync());
        }

        // GET: BTTicketPriorities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bTTicketPriority = await _context.TicketPriorities
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bTTicketPriority == null)
            {
                return NotFound();
            }

            return View(bTTicketPriority);
        }

        // GET: BTTicketPriorities/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BTTicketPriorities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] BTTicketPriority bTTicketPriority)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bTTicketPriority);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bTTicketPriority);
        }

        // GET: BTTicketPriorities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bTTicketPriority = await _context.TicketPriorities.FindAsync(id);
            if (bTTicketPriority == null)
            {
                return NotFound();
            }
            return View(bTTicketPriority);
        }

        // POST: BTTicketPriorities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] BTTicketPriority bTTicketPriority)
        {
            if (id != bTTicketPriority.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bTTicketPriority);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BTTicketPriorityExists(bTTicketPriority.Id))
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
            return View(bTTicketPriority);
        }

        // GET: BTTicketPriorities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bTTicketPriority = await _context.TicketPriorities
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bTTicketPriority == null)
            {
                return NotFound();
            }

            return View(bTTicketPriority);
        }

        // POST: BTTicketPriorities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bTTicketPriority = await _context.TicketPriorities.FindAsync(id);
            _context.TicketPriorities.Remove(bTTicketPriority);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BTTicketPriorityExists(int id)
        {
            return _context.TicketPriorities.Any(e => e.Id == id);
        }
    }
}
