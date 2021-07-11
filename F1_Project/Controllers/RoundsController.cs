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
    public class RoundsController : Controller
    {
        private readonly DBContext _context;

        public RoundsController(DBContext context)
        {
            _context = context;
        }

        // GET: Rounds
        public async Task<IActionResult> Index(int? championshipId)
        {
            ViewData["ChampionshipId"] = championshipId;
            if(championshipId != null)
            {
                // Ordena primeiro pelo Id do campeonato enviado no Details do Championship
                var dBContext = _context.Rounds.Include(r => r.Championship).Include(r => r.Circuit).Where(r => r.ChampionshipId == championshipId).OrderBy(r => r.Number);                
                return View(await dBContext.ToListAsync());
            }
            else
            {
                // Ordena primeiro pelo Year do Championship e depois pelo Number da Round
                var dBContext = _context.Rounds.Include(r => r.Championship).Include(r => r.Circuit).OrderByDescending(r => r.Championship.Year).ThenBy(r => r.Number);
                return View(await dBContext.ToListAsync());
            }          
        }

        // GET: Rounds/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var round = await _context.Rounds
                .Include(r => r.Championship)
                .Include(r => r.Circuit)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (round == null)
            {
                return NotFound();
            }

            return View(round);
        }

        // GET: Rounds/Create
        public IActionResult Create(int? championshipId)
        {
            ViewData["ChampionshipId"] = new SelectList(_context.Championships, "Id", "Year");
            ViewData["CircuitId"] = new SelectList(_context.Circuits, "Id", "FullName");
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Number,ChampionshipId,CircuitId")] Round round)
        {
            if (ModelState.IsValid)
            {
                _context.Add(round);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            ViewData["ChampionshipId"] = new SelectList(_context.Championships, "Id", "Year", round.ChampionshipId);
            ViewData["CircuitId"] = new SelectList(_context.Circuits, "Id", "FullName", round.CircuitId);
            return View(round);
        }

        // GET: Rounds/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var round = await _context.Rounds.FindAsync(id);
            if (round == null)
            {
                return NotFound();
            }
            ViewData["ChampionshipId"] = new SelectList(_context.Championships, "Id", "Year", round.ChampionshipId);
            ViewData["CircuitId"] = new SelectList(_context.Circuits, "Id", "FullName", round.CircuitId);
            return View(round);
        }

        // POST: Rounds/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Number,ChampionshipId,CircuitId")] Round round)
        {
            if (id != round.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(round);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoundExists(round.Id))
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
            ViewData["ChampionshipId"] = new SelectList(_context.Championships, "Id", "Year", round.ChampionshipId);
            ViewData["CircuitId"] = new SelectList(_context.Circuits, "Id", "FullName", round.CircuitId);
            return View(round);
        }

        // GET: Rounds/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var round = await _context.Rounds
                .Include(r => r.Championship)
                .Include(r => r.Circuit)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (round == null)
            {
                return NotFound();
            }

            return View(round);
        }

        // POST: Rounds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var round = await _context.Rounds.FindAsync(id);
            _context.Rounds.Remove(round);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoundExists(int id)
        {
            return _context.Rounds.Any(e => e.Id == id);
        }
    }
}
