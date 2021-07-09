using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using F1_Project.Data;
using F1_Project.Models;

namespace F1_Project.Controllers
{
    public class DriverTeamsController : Controller
    {
        private readonly DBContext _context;

        public DriverTeamsController(DBContext context)
        {
            _context = context;
        }

        // GET: DriverTeams
        public async Task<IActionResult> Index()
        {
            var dBContext = _context.DriverTeams.Include(d => d.Driver).Include(d => d.Team);
            return View(await dBContext.ToListAsync());
        }

        // GET: DriverTeams/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var driverTeam = await _context.DriverTeams
                .Include(d => d.Driver)
                .Include(d => d.Team)
                .FirstOrDefaultAsync(m => m.DriverId == id);
            if (driverTeam == null)
            {
                return NotFound();
            }

            return View(driverTeam);
        }

        // GET: DriverTeams/Create
        public IActionResult Create()
        {
            ViewData["DriverId"] = new SelectList(_context.Drivers, "Id", "FullName");
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "FullName");
            return View();
        }

        // POST: DriverTeams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,InitialYear,FinalYear,DriverId,TeamId")] DriverTeam driverTeam)
        {
            if (ModelState.IsValid)
            {
                _context.Add(driverTeam);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DriverId"] = new SelectList(_context.Drivers, "Id", "FullName", driverTeam.DriverId);
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "FullName", driverTeam.TeamId);
            return View(driverTeam);
        }

        // GET: DriverTeams/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var driverTeam = await _context.DriverTeams.FindAsync(id);
            if (driverTeam == null)
            {
                return NotFound();
            }
            ViewData["DriverId"] = new SelectList(_context.Drivers, "Id", "FullName", driverTeam.DriverId);
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "FullName", driverTeam.TeamId);
            return View(driverTeam);
        }

        // POST: DriverTeams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,InitialYear,FinalYear,DriverId,TeamId")] DriverTeam driverTeam)
        {
            if (id != driverTeam.DriverId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(driverTeam);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DriverTeamExists(driverTeam.DriverId))
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
            ViewData["DriverId"] = new SelectList(_context.Drivers, "Id", "FullName", driverTeam.DriverId);
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "FullName", driverTeam.TeamId);
            return View(driverTeam);
        }

        // GET: DriverTeams/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var driverTeam = await _context.DriverTeams
                .Include(d => d.Driver)
                .Include(d => d.Team)
                .FirstOrDefaultAsync(m => m.DriverId == id);
            if (driverTeam == null)
            {
                return NotFound();
            }

            return View(driverTeam);
        }

        // POST: DriverTeams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var driverTeam = await _context.DriverTeams.FindAsync(id);
            _context.DriverTeams.Remove(driverTeam);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DriverTeamExists(int id)
        {
            return _context.DriverTeams.Any(e => e.DriverId == id);
        }
    }
}
