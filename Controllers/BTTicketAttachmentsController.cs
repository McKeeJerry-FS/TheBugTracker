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
    public class BTTicketAttachmentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BTTicketAttachmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BTTicketAttachments
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.TicketAttachments.Include(b => b.Ticket).Include(b => b.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: BTTicketAttachments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bTTicketAttachment = await _context.TicketAttachments
                .Include(b => b.Ticket)
                .Include(b => b.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bTTicketAttachment == null)
            {
                return NotFound();
            }

            return View(bTTicketAttachment);
        }

        // GET: BTTicketAttachments/Create
        public IActionResult Create()
        {
            ViewData["TicketId"] = new SelectList(_context.Tickets, "Id", "Description");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: BTTicketAttachments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TicketId,Created,UserId,Description,FileName,FileData,FileContentType")] BTTicketAttachment bTTicketAttachment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bTTicketAttachment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TicketId"] = new SelectList(_context.Tickets, "Id", "Description", bTTicketAttachment.TicketId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", bTTicketAttachment.UserId);
            return View(bTTicketAttachment);
        }

        // GET: BTTicketAttachments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bTTicketAttachment = await _context.TicketAttachments.FindAsync(id);
            if (bTTicketAttachment == null)
            {
                return NotFound();
            }
            ViewData["TicketId"] = new SelectList(_context.Tickets, "Id", "Description", bTTicketAttachment.TicketId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", bTTicketAttachment.UserId);
            return View(bTTicketAttachment);
        }

        // POST: BTTicketAttachments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TicketId,Created,UserId,Description,FileName,FileData,FileContentType")] BTTicketAttachment bTTicketAttachment)
        {
            if (id != bTTicketAttachment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bTTicketAttachment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BTTicketAttachmentExists(bTTicketAttachment.Id))
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
            ViewData["TicketId"] = new SelectList(_context.Tickets, "Id", "Description", bTTicketAttachment.TicketId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", bTTicketAttachment.UserId);
            return View(bTTicketAttachment);
        }

        // GET: BTTicketAttachments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bTTicketAttachment = await _context.TicketAttachments
                .Include(b => b.Ticket)
                .Include(b => b.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bTTicketAttachment == null)
            {
                return NotFound();
            }

            return View(bTTicketAttachment);
        }

        // POST: BTTicketAttachments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bTTicketAttachment = await _context.TicketAttachments.FindAsync(id);
            _context.TicketAttachments.Remove(bTTicketAttachment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BTTicketAttachmentExists(int id)
        {
            return _context.TicketAttachments.Any(e => e.Id == id);
        }
    }
}
