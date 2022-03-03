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
    public class BTTicketHistoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BTTicketHistoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BTTicketHistories
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.TicketHistories.Include(b => b.Ticket).Include(b => b.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: BTTicketHistories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bTTicketHistory = await _context.TicketHistories
                .Include(b => b.Ticket)
                .Include(b => b.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bTTicketHistory == null)
            {
                return NotFound();
            }

            return View(bTTicketHistory);
        }

        // GET: BTTicketHistories/Create
        public IActionResult Create()
        {
            ViewData["TicketId"] = new SelectList(_context.Tickets, "Id", "Description");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: BTTicketHistories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TicketId,Property,OldValue,NewValue,Created,Description,UserId")] BTTicketHistory bTTicketHistory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bTTicketHistory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TicketId"] = new SelectList(_context.Tickets, "Id", "Description", bTTicketHistory.TicketId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", bTTicketHistory.UserId);
            return View(bTTicketHistory);
        }

        // GET: BTTicketHistories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bTTicketHistory = await _context.TicketHistories.FindAsync(id);
            if (bTTicketHistory == null)
            {
                return NotFound();
            }
            ViewData["TicketId"] = new SelectList(_context.Tickets, "Id", "Description", bTTicketHistory.TicketId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", bTTicketHistory.UserId);
            return View(bTTicketHistory);
        }

        // POST: BTTicketHistories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TicketId,Property,OldValue,NewValue,Created,Description,UserId")] BTTicketHistory bTTicketHistory)
        {
            if (id != bTTicketHistory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bTTicketHistory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BTTicketHistoryExists(bTTicketHistory.Id))
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
            ViewData["TicketId"] = new SelectList(_context.Tickets, "Id", "Description", bTTicketHistory.TicketId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", bTTicketHistory.UserId);
            return View(bTTicketHistory);
        }

        // GET: BTTicketHistories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bTTicketHistory = await _context.TicketHistories
                .Include(b => b.Ticket)
                .Include(b => b.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bTTicketHistory == null)
            {
                return NotFound();
            }

            return View(bTTicketHistory);
        }

        // POST: BTTicketHistories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bTTicketHistory = await _context.TicketHistories.FindAsync(id);
            _context.TicketHistories.Remove(bTTicketHistory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BTTicketHistoryExists(int id)
        {
            return _context.TicketHistories.Any(e => e.Id == id);
        }
    }
}
