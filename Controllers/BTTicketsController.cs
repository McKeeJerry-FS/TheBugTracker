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
    public class BTTicketsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BTTicketsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BTTickets
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Tickets.Include(b => b.DeveloperUser).Include(b => b.OwnerUser).Include(b => b.Project).Include(b => b.TicketPriority).Include(b => b.TicketStatus).Include(b => b.TicketType);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: BTTickets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bTTicket = await _context.Tickets
                .Include(b => b.DeveloperUser)
                .Include(b => b.OwnerUser)
                .Include(b => b.Project)
                .Include(b => b.TicketPriority)
                .Include(b => b.TicketStatus)
                .Include(b => b.TicketType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bTTicket == null)
            {
                return NotFound();
            }

            return View(bTTicket);
        }

        // GET: BTTickets/Create
        public IActionResult Create()
        {
            ViewData["DeveloperUserId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["OwnerUserId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["ProjectID"] = new SelectList(_context.Projects, "Id", "Name");
            ViewData["TicketPriorityId"] = new SelectList(_context.TicketPriorities, "Id", "Id");
            ViewData["TicketStatusId"] = new SelectList(_context.TicketStatuses, "Id", "Id");
            ViewData["TicketTypeId"] = new SelectList(_context.TicketTypes, "Id", "Id");
            return View();
        }

        // POST: BTTickets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,Created,Updated,Archived,ProjectID,TicketTypeId,TicketPriorityId,TicketStatusId,OwnerUserId,DeveloperUserId")] BTTicket bTTicket)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bTTicket);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DeveloperUserId"] = new SelectList(_context.Users, "Id", "Id", bTTicket.DeveloperUserId);
            ViewData["OwnerUserId"] = new SelectList(_context.Users, "Id", "Id", bTTicket.OwnerUserId);
            ViewData["ProjectID"] = new SelectList(_context.Projects, "Id", "Name", bTTicket.ProjectID);
            ViewData["TicketPriorityId"] = new SelectList(_context.TicketPriorities, "Id", "Id", bTTicket.TicketPriorityId);
            ViewData["TicketStatusId"] = new SelectList(_context.TicketStatuses, "Id", "Id", bTTicket.TicketStatusId);
            ViewData["TicketTypeId"] = new SelectList(_context.TicketTypes, "Id", "Id", bTTicket.TicketTypeId);
            return View(bTTicket);
        }

        // GET: BTTickets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bTTicket = await _context.Tickets.FindAsync(id);
            if (bTTicket == null)
            {
                return NotFound();
            }
            ViewData["DeveloperUserId"] = new SelectList(_context.Users, "Id", "Id", bTTicket.DeveloperUserId);
            ViewData["OwnerUserId"] = new SelectList(_context.Users, "Id", "Id", bTTicket.OwnerUserId);
            ViewData["ProjectID"] = new SelectList(_context.Projects, "Id", "Name", bTTicket.ProjectID);
            ViewData["TicketPriorityId"] = new SelectList(_context.TicketPriorities, "Id", "Id", bTTicket.TicketPriorityId);
            ViewData["TicketStatusId"] = new SelectList(_context.TicketStatuses, "Id", "Id", bTTicket.TicketStatusId);
            ViewData["TicketTypeId"] = new SelectList(_context.TicketTypes, "Id", "Id", bTTicket.TicketTypeId);
            return View(bTTicket);
        }

        // POST: BTTickets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Created,Updated,Archived,ProjectID,TicketTypeId,TicketPriorityId,TicketStatusId,OwnerUserId,DeveloperUserId")] BTTicket bTTicket)
        {
            if (id != bTTicket.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bTTicket);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BTTicketExists(bTTicket.Id))
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
            ViewData["DeveloperUserId"] = new SelectList(_context.Users, "Id", "Id", bTTicket.DeveloperUserId);
            ViewData["OwnerUserId"] = new SelectList(_context.Users, "Id", "Id", bTTicket.OwnerUserId);
            ViewData["ProjectID"] = new SelectList(_context.Projects, "Id", "Name", bTTicket.ProjectID);
            ViewData["TicketPriorityId"] = new SelectList(_context.TicketPriorities, "Id", "Id", bTTicket.TicketPriorityId);
            ViewData["TicketStatusId"] = new SelectList(_context.TicketStatuses, "Id", "Id", bTTicket.TicketStatusId);
            ViewData["TicketTypeId"] = new SelectList(_context.TicketTypes, "Id", "Id", bTTicket.TicketTypeId);
            return View(bTTicket);
        }

        // GET: BTTickets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bTTicket = await _context.Tickets
                .Include(b => b.DeveloperUser)
                .Include(b => b.OwnerUser)
                .Include(b => b.Project)
                .Include(b => b.TicketPriority)
                .Include(b => b.TicketStatus)
                .Include(b => b.TicketType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bTTicket == null)
            {
                return NotFound();
            }

            return View(bTTicket);
        }

        // POST: BTTickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bTTicket = await _context.Tickets.FindAsync(id);
            _context.Tickets.Remove(bTTicket);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BTTicketExists(int id)
        {
            return _context.Tickets.Any(e => e.Id == id);
        }
    }
}
