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
    public class BTTicketTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BTTicketTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BTTicketTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.TicketTypes.ToListAsync());
        }

        // GET: BTTicketTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bTTicketType = await _context.TicketTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bTTicketType == null)
            {
                return NotFound();
            }

            return View(bTTicketType);
        }

        // GET: BTTicketTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BTTicketTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] BTTicketType bTTicketType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bTTicketType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bTTicketType);
        }

        // GET: BTTicketTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bTTicketType = await _context.TicketTypes.FindAsync(id);
            if (bTTicketType == null)
            {
                return NotFound();
            }
            return View(bTTicketType);
        }

        // POST: BTTicketTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] BTTicketType bTTicketType)
        {
            if (id != bTTicketType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bTTicketType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BTTicketTypeExists(bTTicketType.Id))
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
            return View(bTTicketType);
        }

        // GET: BTTicketTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bTTicketType = await _context.TicketTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bTTicketType == null)
            {
                return NotFound();
            }

            return View(bTTicketType);
        }

        // POST: BTTicketTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bTTicketType = await _context.TicketTypes.FindAsync(id);
            _context.TicketTypes.Remove(bTTicketType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BTTicketTypeExists(int id)
        {
            return _context.TicketTypes.Any(e => e.Id == id);
        }
    }
}
