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
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace F1_Project.Controllers
{
    public class DriversController : Controller
    {
        private readonly DBContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public DriversController(DBContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        // Explicit Loading => https://docs.microsoft.com/en-us/aspnet/core/data/ef-mvc/read-related-data?view=aspnetcore-5.0#about-explicit-loading
        public async Task<IActionResult> Index(int? id)
        {
            var viewModel = new DriverIndexData();
            viewModel.Drivers = await _context.Drivers
                .Include(d => d.DriverTeams)
                    .ThenInclude(d => d.Team)
                .AsNoTracking().OrderBy(d => d.FullName).ToListAsync();

            if(id != null)
            {
                ViewData["DriverId"] = id.Value;                
                Driver driver = viewModel.Drivers.Where(d => d.Id == id.Value).Single();
                viewModel.Teams = driver.DriverTeams.Select(d => d.Team);
            }

            return View(viewModel);
        }

        // GET: Drivers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var driver = await _context.Drivers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (driver == null)
            {
                return NotFound();
            }

            return View(driver);
        }

        // GET: Drivers/Create
        public IActionResult Create()
        {            
            return View();
        }

        // POST: Drivers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FullName,Nationality,BirthDate,CarNumber,ChampionshipsVictories,RaceVictories,Description,Photo")] Driver driver, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                if(file != null)
                {
                    var linkUpload = Path.Combine(_hostEnvironment.WebRootPath, "Images");

                    using (FileStream fileStream = new FileStream(Path.Combine(linkUpload, file.FileName), FileMode.Create))
                    {                        
                        await file.CopyToAsync(fileStream);                        
                        driver.Photo = "~/Images/" + file.FileName;
                    }
                }

                _context.Add(driver);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(driver);
        }

        // GET: Drivers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var driver = await _context.Drivers.FindAsync(id);
            if (driver == null)
            {
                return NotFound();
            }

            TempData["DriverPhoto"] = driver.Photo;
            return View(driver);
        }

        // POST: Drivers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FullName,Nationality,BirthDate,CarNumber,ChampionshipsVictories,RaceVictories,Description,Photo")] Driver driver, IFormFile file)
        {
            if (id != driver.Id)
            {
                return NotFound();
            }

            driver.Photo = TempData["DriverPhoto"].ToString();

            if (ModelState.IsValid)
            {
                if (file != null)
                {                    
                    var linkUpload = Path.Combine(_hostEnvironment.WebRootPath, "Images");

                    using (FileStream fileStream = new FileStream(Path.Combine(linkUpload, file.FileName), FileMode.Create))
                    {                        
                        await file.CopyToAsync(fileStream);                        
                        driver.Photo = "~/Images/" + file.FileName;
                    }
                }
                else
                {
                    driver.Photo = TempData["DriverPhoto"].ToString();
                }

                try
                {
                    _context.Update(driver);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DriverExists(driver.Id))
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
            return View(driver);
        }

        // GET: Drivers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var driver = await _context.Drivers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (driver == null)
            {
                return NotFound();
            }

            return View(driver);
        }

        // POST: Drivers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var driver = await _context.Drivers.FindAsync(id);

            string driverPhoto = driver.Photo;
            driverPhoto = driverPhoto.Replace("~", "wwwroot");
            System.IO.File.Delete(driverPhoto);

            _context.Drivers.Remove(driver);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DriverExists(int id)
        {
            return _context.Drivers.Any(e => e.Id == id);
        }
    }
}
