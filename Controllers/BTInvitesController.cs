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
    public class BTInvitesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BTInvitesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BTInvites
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Invites.Include(b => b.Company).Include(b => b.Invitee).Include(b => b.Invitor).Include(b => b.Project);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: BTInvites/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bTInvite = await _context.Invites
                .Include(b => b.Company)
                .Include(b => b.Invitee)
                .Include(b => b.Invitor)
                .Include(b => b.Project)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bTInvite == null)
            {
                return NotFound();
            }

            return View(bTInvite);
        }

        // GET: BTInvites/Create
        public IActionResult Create()
        {
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Name");
            ViewData["InviteeId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["InvitorId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Name");
            return View();
        }

        // POST: BTInvites/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,InviteDate,JoinDate,CompanyToken,CompanyId,ProjectId,InvitorId,InviteeId,InviteeEmail,InviteeFirstName,InviteeLastName,IsValid")] BTInvite bTInvite)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bTInvite);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Name", bTInvite.CompanyId);
            ViewData["InviteeId"] = new SelectList(_context.Users, "Id", "Id", bTInvite.InviteeId);
            ViewData["InvitorId"] = new SelectList(_context.Users, "Id", "Id", bTInvite.InvitorId);
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Name", bTInvite.ProjectId);
            return View(bTInvite);
        }

        // GET: BTInvites/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bTInvite = await _context.Invites.FindAsync(id);
            if (bTInvite == null)
            {
                return NotFound();
            }
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Name", bTInvite.CompanyId);
            ViewData["InviteeId"] = new SelectList(_context.Users, "Id", "Id", bTInvite.InviteeId);
            ViewData["InvitorId"] = new SelectList(_context.Users, "Id", "Id", bTInvite.InvitorId);
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Name", bTInvite.ProjectId);
            return View(bTInvite);
        }

        // POST: BTInvites/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,InviteDate,JoinDate,CompanyToken,CompanyId,ProjectId,InvitorId,InviteeId,InviteeEmail,InviteeFirstName,InviteeLastName,IsValid")] BTInvite bTInvite)
        {
            if (id != bTInvite.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bTInvite);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BTInviteExists(bTInvite.Id))
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
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Name", bTInvite.CompanyId);
            ViewData["InviteeId"] = new SelectList(_context.Users, "Id", "Id", bTInvite.InviteeId);
            ViewData["InvitorId"] = new SelectList(_context.Users, "Id", "Id", bTInvite.InvitorId);
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Name", bTInvite.ProjectId);
            return View(bTInvite);
        }

        // GET: BTInvites/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bTInvite = await _context.Invites
                .Include(b => b.Company)
                .Include(b => b.Invitee)
                .Include(b => b.Invitor)
                .Include(b => b.Project)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bTInvite == null)
            {
                return NotFound();
            }

            return View(bTInvite);
        }

        // POST: BTInvites/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bTInvite = await _context.Invites.FindAsync(id);
            _context.Invites.Remove(bTInvite);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BTInviteExists(int id)
        {
            return _context.Invites.Any(e => e.Id == id);
        }
    }
}
