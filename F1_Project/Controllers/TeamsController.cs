using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using F1_Project.Data;
using F1_Project.Models;
using Microsoft.AspNetCore.Hosting;
using F1_Project.Models.ViewModel;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace F1_Project.Controllers
{
    public class TeamsController : Controller
    {
        private readonly DBContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public TeamsController(DBContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        // Explicit Loading => https://docs.microsoft.com/en-us/aspnet/core/data/ef-mvc/read-related-data?view=aspnetcore-5.0#about-explicit-loading
        public async Task<IActionResult> Index(int? id)
        {
            var viewModel = new DriverIndexData();
            viewModel.Teams = await _context.Teams
                .Include(d => d.DriverTeams)
                    .ThenInclude(d => d.Driver)
                .AsNoTracking().OrderBy(d => d.FullName).ToListAsync();

            if (id != null)
            {
                ViewData["TeamId"] = id.Value;
                Team team = viewModel.Teams.Where(d => d.Id == id.Value).Single();
                viewModel.Drivers = team.DriverTeams.Select(d => d.Driver);
            }
            return View(viewModel);
        }

        // GET: Teams/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team = await _context.Teams
                .FirstOrDefaultAsync(m => m.Id == id);
            if (team == null)
            {
                return NotFound();
            }

            return View(team);
        }

        // GET: Teams/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Teams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FullName,Nationality,RaceVictories,TeamsChampionshipsVictories,DriversChampionshipsVictories,Description,Photo")] Team team, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                if(file != null)
                {
                    var linkUpload = Path.Combine(_hostEnvironment.WebRootPath, "Images");

                    using (FileStream fileStream = new FileStream(Path.Combine(linkUpload, file.FileName), FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                        team.Photo = "~/Images/" + file.FileName;
                    }
                }
                _context.Add(team);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(team);
        }

        // GET: Teams/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team = await _context.Teams.FindAsync(id);
            if (team == null)
            {
                return NotFound();
            }

            TempData["TeamPhoto"] = team.Photo;
            return View(team);
        }

        // POST: Teams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FullName,Nationality,RaceVictories,TeamsChampionshipsVictories,DriversChampionshipsVictories,Description,Photo")] Team team, IFormFile file)
        {
            if (id != team.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    var linkUpload = Path.Combine(_hostEnvironment.WebRootPath, "Images");

                    using (FileStream fileStream = new FileStream(Path.Combine(linkUpload, file.FileName), FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                        team.Photo = "~/Images/" + file.FileName;
                    }
                }
                else
                {
                    team.Photo = TempData["TeamPhoto"].ToString();
                }

                try
                {
                    _context.Update(team);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeamExists(team.Id))
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
            return View(team);
        }

        // GET: Teams/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team = await _context.Teams
                .FirstOrDefaultAsync(m => m.Id == id);
            if (team == null)
            {
                return NotFound();
            }

            return View(team);
        }

        // POST: Teams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var team = await _context.Teams.FindAsync(id);

            string teamPhoto = team.Photo;
            if (team.Photo != null)
            {
                teamPhoto = teamPhoto.Replace("~", "wwwroot");
                System.IO.File.Delete(teamPhoto);
            }

            _context.Teams.Remove(team);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeamExists(int id)
        {
            return _context.Teams.Any(e => e.Id == id);
        }
    }
}