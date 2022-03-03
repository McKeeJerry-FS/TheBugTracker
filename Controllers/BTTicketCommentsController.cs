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
    public class BTTicketCommentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BTTicketCommentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BTTicketComments
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.TicketComments.Include(b => b.Ticket).Include(b => b.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: BTTicketComments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bTTicketComment = await _context.TicketComments
                .Include(b => b.Ticket)
                .Include(b => b.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bTTicketComment == null)
            {
                return NotFound();
            }

            return View(bTTicketComment);
        }

        // GET: BTTicketComments/Create
        public IActionResult Create()
        {
            ViewData["TicketId"] = new SelectList(_context.Tickets, "Id", "Description");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: BTTicketComments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Comment,Created,TicketId,UserId")] BTTicketComment bTTicketComment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bTTicketComment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TicketId"] = new SelectList(_context.Tickets, "Id", "Description", bTTicketComment.TicketId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", bTTicketComment.UserId);
            return View(bTTicketComment);
        }

        // GET: BTTicketComments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bTTicketComment = await _context.TicketComments.FindAsync(id);
            if (bTTicketComment == null)
            {
                return NotFound();
            }
            ViewData["TicketId"] = new SelectList(_context.Tickets, "Id", "Description", bTTicketComment.TicketId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", bTTicketComment.UserId);
            return View(bTTicketComment);
        }

        // POST: BTTicketComments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Comment,Created,TicketId,UserId")] BTTicketComment bTTicketComment)
        {
            if (id != bTTicketComment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bTTicketComment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BTTicketCommentExists(bTTicketComment.Id))
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
            ViewData["TicketId"] = new SelectList(_context.Tickets, "Id", "Description", bTTicketComment.TicketId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", bTTicketComment.UserId);
            return View(bTTicketComment);
        }

        // GET: BTTicketComments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bTTicketComment = await _context.TicketComments
                .Include(b => b.Ticket)
                .Include(b => b.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bTTicketComment == null)
            {
                return NotFound();
            }

            return View(bTTicketComment);
        }

        // POST: BTTicketComments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bTTicketComment = await _context.TicketComments.FindAsync(id);
            _context.TicketComments.Remove(bTTicketComment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BTTicketCommentExists(int id)
        {
            return _context.TicketComments.Any(e => e.Id == id);
        }
    }
}
