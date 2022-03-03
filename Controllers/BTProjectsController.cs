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
    public class BTProjectsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BTProjectsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BTProjects
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Projects.Include(b => b.Priority);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: BTProjects/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bTProject = await _context.Projects
                .Include(b => b.Priority)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bTProject == null)
            {
                return NotFound();
            }

            return View(bTProject);
        }

        // GET: BTProjects/Create
        public IActionResult Create()
        {
            ViewData["BTProjectPriorityId"] = new SelectList(_context.ProjectPriorities, "Id", "Id");
            return View();
        }

        // POST: BTProjects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CompanyId,Name,Description,StartDate,EndDate,BTProjectPriorityId,ImageFileName,ImageFileData,ImageContentType,Archived")] BTProject bTProject)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bTProject);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BTProjectPriorityId"] = new SelectList(_context.ProjectPriorities, "Id", "Id", bTProject.BTProjectPriorityId);
            return View(bTProject);
        }

        // GET: BTProjects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bTProject = await _context.Projects.FindAsync(id);
            if (bTProject == null)
            {
                return NotFound();
            }
            ViewData["BTProjectPriorityId"] = new SelectList(_context.ProjectPriorities, "Id", "Id", bTProject.BTProjectPriorityId);
            return View(bTProject);
        }

        // POST: BTProjects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CompanyId,Name,Description,StartDate,EndDate,BTProjectPriorityId,ImageFileName,ImageFileData,ImageContentType,Archived")] BTProject bTProject)
        {
            if (id != bTProject.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bTProject);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BTProjectExists(bTProject.Id))
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
            ViewData["BTProjectPriorityId"] = new SelectList(_context.ProjectPriorities, "Id", "Id", bTProject.BTProjectPriorityId);
            return View(bTProject);
        }

        // GET: BTProjects/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bTProject = await _context.Projects
                .Include(b => b.Priority)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bTProject == null)
            {
                return NotFound();
            }

            return View(bTProject);
        }

        // POST: BTProjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bTProject = await _context.Projects.FindAsync(id);
            _context.Projects.Remove(bTProject);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BTProjectExists(int id)
        {
            return _context.Projects.Any(e => e.Id == id);
        }
    }
}
