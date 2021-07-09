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
    public class ChampionshipTeamsController : Controller
    {
        private readonly DBContext _context;

        public ChampionshipTeamsController(DBContext context)
        {
            _context = context;
        }

        // GET: ChampionshipTeams
        public async Task<IActionResult> Index()
        {
            var dBContext = _context.ChampionshipTeams.Include(c => c.Championship).Include(c => c.Team);
            return View(await dBContext.ToListAsync());
        }

        // GET: ChampionshipTeams/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var championshipTeam = await _context.ChampionshipTeams
                .Include(c => c.Championship)
                .Include(c => c.Team)
                .FirstOrDefaultAsync(m => m.ChampionshipId == id);
            if (championshipTeam == null)
            {
                return NotFound();
            }

            return View(championshipTeam);
        }

        // GET: ChampionshipTeams/Create
        public IActionResult Create()
        {
            ViewData["ChampionshipId"] = new SelectList(_context.Championships, "Id", "Id");
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "FullName");
            return View();
        }

        // POST: ChampionshipTeams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ChampionshipId,TeamId")] ChampionshipTeam championshipTeam)
        {
            if (ModelState.IsValid)
            {
                _context.Add(championshipTeam);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ChampionshipId"] = new SelectList(_context.Championships, "Id", "Id", championshipTeam.ChampionshipId);
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "FullName", championshipTeam.TeamId);
            return View(championshipTeam);
        }

        // GET: ChampionshipTeams/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var championshipTeam = await _context.ChampionshipTeams.FindAsync(id);
            if (championshipTeam == null)
            {
                return NotFound();
            }
            ViewData["ChampionshipId"] = new SelectList(_context.Championships, "Id", "Id", championshipTeam.ChampionshipId);
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "FullName", championshipTeam.TeamId);
            return View(championshipTeam);
        }

        // POST: ChampionshipTeams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ChampionshipId,TeamId")] ChampionshipTeam championshipTeam)
        {
            if (id != championshipTeam.ChampionshipId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(championshipTeam);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChampionshipTeamExists(championshipTeam.ChampionshipId))
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
            ViewData["ChampionshipId"] = new SelectList(_context.Championships, "Id", "Id", championshipTeam.ChampionshipId);
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "FullName", championshipTeam.TeamId);
            return View(championshipTeam);
        }

        // GET: ChampionshipTeams/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var championshipTeam = await _context.ChampionshipTeams
                .Include(c => c.Championship)
                .Include(c => c.Team)
                .FirstOrDefaultAsync(m => m.ChampionshipId == id);
            if (championshipTeam == null)
            {
                return NotFound();
            }

            return View(championshipTeam);
        }

        // POST: ChampionshipTeams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var championshipTeam = await _context.ChampionshipTeams.FindAsync(id);
            _context.ChampionshipTeams.Remove(championshipTeam);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChampionshipTeamExists(int id)
        {
            return _context.ChampionshipTeams.Any(e => e.ChampionshipId == id);
        }
    }
}
