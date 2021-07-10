using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using F1_Project.Data;
using F1_Project.Models;
using F1_Project.Models.ViewModel;

namespace F1_Project
{
    public class ChampionshipsController : Controller
    {
        private readonly DBContext _context;

        public ChampionshipsController(DBContext context)
        {
            _context = context;
        }

        // GET: Championships
        public async Task<IActionResult> Index(int? id)
        {
            var viewModel = new DriverIndexData();
            viewModel.Championships = await _context.Championships
                .Include(d => d.ChampionshipTeams)
                    .ThenInclude(d => d.Team)
                .AsNoTracking().OrderBy(d => d.Year).ToListAsync();

            if (id != null)
            {
                ViewData["ChampionshipId"] = id.Value;
                Championship championship = viewModel.Championships.Where(d => d.Id == id.Value).Single();
                viewModel.Teams = championship.ChampionshipTeams.Select(d => d.Team);
            }
            return View(viewModel);
        }

        // GET: Championships/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var championship = await _context.Championships
                .FirstOrDefaultAsync(m => m.Id == id);
            if (championship == null)
            {
                return NotFound();
            }

            return View(championship);
        }

        // GET: Championships/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Championships/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Year")] Championship championship)
        {
            if (ModelState.IsValid)
            {
                _context.Add(championship);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(championship);
        }

        // GET: Championships/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var championship = await _context.Championships.FindAsync(id);
            if (championship == null)
            {
                return NotFound();
            }
            return View(championship);
        }

        // POST: Championships/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Year")] Championship championship)
        {
            if (id != championship.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(championship);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChampionshipExists(championship.Id))
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
            return View(championship);
        }

        // GET: Championships/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var championship = await _context.Championships
                .FirstOrDefaultAsync(m => m.Id == id);
            if (championship == null)
            {
                return NotFound();
            }

            return View(championship);
        }

        // POST: Championships/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var championship = await _context.Championships.FindAsync(id);
            _context.Championships.Remove(championship);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChampionshipExists(int id)
        {
            return _context.Championships.Any(e => e.Id == id);
        }
    }
}
