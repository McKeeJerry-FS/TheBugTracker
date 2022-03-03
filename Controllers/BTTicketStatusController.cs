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
    public class BTTicketStatusController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BTTicketStatusController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BTTicketStatus
        public async Task<IActionResult> Index()
        {
            return View(await _context.TicketStatuses.ToListAsync());
        }

        // GET: BTTicketStatus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bTTicketStatus = await _context.TicketStatuses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bTTicketStatus == null)
            {
                return NotFound();
            }

            return View(bTTicketStatus);
        }

        // GET: BTTicketStatus/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BTTicketStatus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] BTTicketStatus bTTicketStatus)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bTTicketStatus);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bTTicketStatus);
        }

        // GET: BTTicketStatus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bTTicketStatus = await _context.TicketStatuses.FindAsync(id);
            if (bTTicketStatus == null)
            {
                return NotFound();
            }
            return View(bTTicketStatus);
        }

        // POST: BTTicketStatus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] BTTicketStatus bTTicketStatus)
        {
            if (id != bTTicketStatus.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bTTicketStatus);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BTTicketStatusExists(bTTicketStatus.Id))
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
            return View(bTTicketStatus);
        }

        // GET: BTTicketStatus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bTTicketStatus = await _context.TicketStatuses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bTTicketStatus == null)
            {
                return NotFound();
            }

            return View(bTTicketStatus);
        }

        // POST: BTTicketStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bTTicketStatus = await _context.TicketStatuses.FindAsync(id);
            _context.TicketStatuses.Remove(bTTicketStatus);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BTTicketStatusExists(int id)
        {
            return _context.TicketStatuses.Any(e => e.Id == id);
        }
    }
}
