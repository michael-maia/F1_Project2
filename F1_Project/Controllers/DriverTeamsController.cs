using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using F1_Project.Data;
using F1_Project.Models;
using Microsoft.Extensions.Logging;

namespace F1_Project.Controllers
{
    public class DriverTeamsController : Controller
    {
        private readonly DBContext _context;
        private readonly ILogger<DriverTeamsController> _logger;

        public DriverTeamsController(DBContext context ,ILogger<DriverTeamsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: DriverTeams
        public async Task<IActionResult> Index(int? id)
        {
            if (id != null)
            {
                var dBContext = _context.DriverTeams.Include(d => d.Driver).Include(d => d.Team).Where(d => d.DriverId == id.Value);
                return View(await dBContext.ToListAsync());
            }
            else
            {
                var dBContext = _context.DriverTeams.Include(d => d.Driver).Include(d => d.Team);
                return View(await dBContext.ToListAsync());
            }
            
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
        public IActionResult Create(int? driverId, int? teamId)
        {
            // Como Id não é chave primária, não há auto-incremento
            IEnumerable<int> ids = _context.DriverTeams.Select(dt => dt.Id).ToList();
            ViewData["NewId"] = ids.Last() + 1;

            ViewData["DriverId"] = new SelectList(_context.Drivers, "Id", "FullName");
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "FullName");
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,InitialYear,FinalYear,DriverId,TeamId")] DriverTeam driverTeam)
        {
            if (ModelState.IsValid)
            {
                _context.Add(driverTeam);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");                
            }
            ViewData["DriverId"] = new SelectList(_context.Drivers, "Id", "FullName", driverTeam.DriverId);
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "FullName", driverTeam.TeamId);
            return View(driverTeam);
        }

        // GET: DriverTeams/Edit/5
        public IActionResult Edit(int? driverId, int? teamId)
        {
            DriverTeam driverTeam1 = _context.DriverTeams.Find(driverId, teamId);

            if (driverTeam1 == null)
            {
                return NotFound();
            }
            
            ViewData["DriverId"] = new SelectList(_context.Drivers, "Id", "FullName", driverTeam1.DriverId);
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "FullName", driverTeam1.TeamId);

            return View(driverTeam1);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind("Id,InitialYear,FinalYear,DriverId,TeamId")] DriverTeam driverTeam)
        {
            // Chaves compostas => https://www.codeproject.com/Articles/797444/ASP-NET-MVC-Edit-Primary-Key-Values-for-Composite

            if (ModelState.IsValid)
            {
                int DriverIdTemp = Convert.ToInt32(driverTeam.DriverId);
                int TeamIdTemp = Convert.ToInt32(driverTeam.TeamId);

                var services = _context.DriverTeams.Single(dt => dt.Id == driverTeam.Id);                
                _context.DriverTeams.Remove(services);
                
                _context.DriverTeams.Add(driverTeam);
                try
                {
                    _context.SaveChanges();
                }
                catch
                {
                    return RedirectToAction("Index");
                }
                _context.Entry(driverTeam).State = EntityState.Modified;              
                return RedirectToAction("Index");
            }

            ViewData["DriverId"] = new SelectList(_context.Drivers, "Id", "FullName", driverTeam.DriverId);
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "FullName", driverTeam.TeamId);
            return View(driverTeam);
        }

        // GET: DriverTeams/Delete/5
        public async Task<IActionResult> Delete(int? driverId, int? teamId)
        {
            if (driverId == null || teamId == null)
            {
                return NotFound();
            }

            var driverTeam = await _context.DriverTeams
                .Include(d => d.Driver)
                .Include(d => d.Team)
                .FirstOrDefaultAsync(m => m.DriverId == driverId && m.TeamId == teamId);
            if (driverTeam == null)
            {
                return NotFound();
            }

            return View(driverTeam);
        }

        // POST: DriverTeams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int driverId, int teamId)
        {
            var driverTeam = await _context.DriverTeams.FindAsync(driverId, teamId);
            _context.DriverTeams.Remove(driverTeam);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DriverTeamExists(int teamId, int driverId)
        {
            return _context.DriverTeams.Any(e => e.DriverId == driverId && e.TeamId == teamId);
        }
    }
}