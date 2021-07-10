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
            ViewData["ChampionshipId"] = new SelectList(_context.Championships, "Id", "Year");
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
        public IActionResult Edit(int? championshipId, int? teamId)
        {
            ChampionshipTeam championshipTeamTemp = _context.ChampionshipTeams.Find(championshipId, teamId);

            if (championshipTeamTemp == null)
            {
                return NotFound();
            }
            
            ViewData["ChampionshipId"] = new SelectList(_context.Championships, "Id", "Year", championshipTeamTemp.ChampionshipId);
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "FullName", championshipTeamTemp.TeamId);
            return View(championshipTeamTemp);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,ChampionshipId,TeamId")] ChampionshipTeam championshipTeam)
        {
            // Chaves compostas => https://www.codeproject.com/Articles/797444/ASP-NET-MVC-Edit-Primary-Key-Values-for-Composite

            if (id != championshipTeam.ChampionshipId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                int championshipIdTemp = Convert.ToInt32(championshipTeam.ChampionshipId);
                int teamIdTemp = Convert.ToInt32(championshipTeam.TeamId);

                var services = _context.ChampionshipTeams.Single(dt => dt.Id == championshipTeam.Id);
                _context.ChampionshipTeams.Remove(services);
                _context.ChampionshipTeams.Add(championshipTeam);
                try
                {
                    _context.SaveChanges();
                }
                catch
                {
                    return RedirectToAction("Index");
                }
                _context.Entry(championshipTeam).State = EntityState.Modified;
                return RedirectToAction("Index");
            }
            
            ViewData["ChampionshipId"] = new SelectList(_context.Championships, "Id", "Year", championshipTeam.ChampionshipId);
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "FullName", championshipTeam.TeamId);
            return View(championshipTeam);
        }

        // GET: ChampionshipTeams/Delete/5
        public async Task<IActionResult> Delete(int? championshipId, int? teamId)
        {
            if (championshipId == null || teamId == null)
            {
                return NotFound();
            }

            var championshipTeam = await _context.ChampionshipTeams
                .Include(c => c.Championship)
                .Include(c => c.Team)
                .FirstOrDefaultAsync(m => m.ChampionshipId == championshipId && m.TeamId == teamId);
            if (championshipTeam == null)
            {
                return NotFound();
            }

            return View(championshipTeam);
        }

        // POST: ChampionshipTeams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int championshipId, int teamId)
        {
            var championshipTeam = await _context.ChampionshipTeams.FindAsync(championshipId, teamId);
            _context.ChampionshipTeams.Remove(championshipTeam);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChampionshipTeamExists(int championshipId, int teamId)
        {
            return _context.ChampionshipTeams.Any(e => e.ChampionshipId == championshipId && e.TeamId == teamId);
        }
    }
}