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
    public class BTNotificationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BTNotificationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BTNotifications
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Notifications.Include(b => b.Recipient).Include(b => b.Sender).Include(b => b.Ticket);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: BTNotifications/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bTNotification = await _context.Notifications
                .Include(b => b.Recipient)
                .Include(b => b.Sender)
                .Include(b => b.Ticket)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bTNotification == null)
            {
                return NotFound();
            }

            return View(bTNotification);
        }

        // GET: BTNotifications/Create
        public IActionResult Create()
        {
            ViewData["RecipientId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["SenderId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["TicketId"] = new SelectList(_context.Tickets, "Id", "Description");
            return View();
        }

        // POST: BTNotifications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TicketId,Title,Message,Created,RecipientId,SenderId,Viewed")] BTNotification bTNotification)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bTNotification);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RecipientId"] = new SelectList(_context.Users, "Id", "Id", bTNotification.RecipientId);
            ViewData["SenderId"] = new SelectList(_context.Users, "Id", "Id", bTNotification.SenderId);
            ViewData["TicketId"] = new SelectList(_context.Tickets, "Id", "Description", bTNotification.TicketId);
            return View(bTNotification);
        }

        // GET: BTNotifications/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bTNotification = await _context.Notifications.FindAsync(id);
            if (bTNotification == null)
            {
                return NotFound();
            }
            ViewData["RecipientId"] = new SelectList(_context.Users, "Id", "Id", bTNotification.RecipientId);
            ViewData["SenderId"] = new SelectList(_context.Users, "Id", "Id", bTNotification.SenderId);
            ViewData["TicketId"] = new SelectList(_context.Tickets, "Id", "Description", bTNotification.TicketId);
            return View(bTNotification);
        }

        // POST: BTNotifications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TicketId,Title,Message,Created,RecipientId,SenderId,Viewed")] BTNotification bTNotification)
        {
            if (id != bTNotification.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bTNotification);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BTNotificationExists(bTNotification.Id))
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
            ViewData["RecipientId"] = new SelectList(_context.Users, "Id", "Id", bTNotification.RecipientId);
            ViewData["SenderId"] = new SelectList(_context.Users, "Id", "Id", bTNotification.SenderId);
            ViewData["TicketId"] = new SelectList(_context.Tickets, "Id", "Description", bTNotification.TicketId);
            return View(bTNotification);
        }

        // GET: BTNotifications/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bTNotification = await _context.Notifications
                .Include(b => b.Recipient)
                .Include(b => b.Sender)
                .Include(b => b.Ticket)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bTNotification == null)
            {
                return NotFound();
            }

            return View(bTNotification);
        }

        // POST: BTNotifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bTNotification = await _context.Notifications.FindAsync(id);
            _context.Notifications.Remove(bTNotification);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BTNotificationExists(int id)
        {
            return _context.Notifications.Any(e => e.Id == id);
        }
    }
}
