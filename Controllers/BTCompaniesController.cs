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
    public class BTCompaniesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BTCompaniesController(ApplicationDbContext context) // Dependency Injection
        {
            _context = context;
        }

        // GET: BTCompanies
        public async Task<IActionResult> Index()
        {
            return View(await _context.Companies.ToListAsync());
        }

        // GET: BTCompanies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bTCompany = await _context.Companies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bTCompany == null)
            {
                return NotFound();
            }

            return View(bTCompany);
        }

        // GET: BTCompanies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BTCompanies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description")] BTCompany bTCompany)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bTCompany);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bTCompany);
        }

        // GET: BTCompanies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            BTCompany bTCompany = await _context.Companies.FindAsync(id); // Switching from "var" to BTCompany makes this more explicit
            if (bTCompany == null)
            {
                return NotFound();
            }
            return View(bTCompany);
        }

        // POST: BTCompanies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description")] BTCompany bTCompany)
        {
            if (id != bTCompany.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bTCompany);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BTCompanyExists(bTCompany.Id))
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
            return View(bTCompany);
        }

        // GET: BTCompanies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bTCompany = await _context.Companies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bTCompany == null)
            {
                return NotFound();
            }

            return View(bTCompany);
        }

        // POST: BTCompanies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bTCompany = await _context.Companies.FindAsync(id);
            _context.Companies.Remove(bTCompany);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BTCompanyExists(int id)
        {
            return _context.Companies.Any(e => e.Id == id);
        }
    }
}
