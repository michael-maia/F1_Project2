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
using Microsoft.AspNetCore.Http;
using System.IO;

namespace F1_Project.Controllers
{
    public class CircuitsController : Controller
    {
        private readonly DBContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public CircuitsController(DBContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        // GET: Circuits
        public async Task<IActionResult> Index()
        {
            return View(await _context.Circuits.ToListAsync());
        }

        // GET: Circuits/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var circuit = await _context.Circuits
                .FirstOrDefaultAsync(m => m.Id == id);
            if (circuit == null)
            {
                return NotFound();
            }

            return View(circuit);
        }

        // GET: Circuits/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Circuits/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FullName,NumberRacesHeld,CircuitLength,NumberLaps,YearFirstRace,LapRecord,Description,Photo")] Circuit circuit, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    var linkUpload = Path.Combine(_hostEnvironment.WebRootPath, "Images");

                    using (FileStream fileStream = new FileStream(Path.Combine(linkUpload, file.FileName), FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                        circuit.Photo = "~/Images/" + file.FileName;
                    }
                }
                _context.Add(circuit);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(circuit);
        }

        // GET: Circuits/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var circuit = await _context.Circuits.FindAsync(id);
            if (circuit == null)
            {
                return NotFound();
            }
            TempData["CircuitPhoto"] = circuit.Photo;
            return View(circuit);
        }

        // POST: Circuits/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FullName,NumberRacesHeld,CircuitLength,NumberLaps,YearFirstRace,LapRecord,Description,Photo")] Circuit circuit, IFormFile file)
        {
            if (id != circuit.Id)
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
                        circuit.Photo = "~/Images/" + file.FileName;
                    }
                }
                else
                {
                    circuit.Photo = TempData["CircuitPhoto"].ToString();
                }
                try
                {
                    _context.Update(circuit);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CircuitExists(circuit.Id))
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
            return View(circuit);
        }

        // GET: Circuits/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var circuit = await _context.Circuits
                .FirstOrDefaultAsync(m => m.Id == id);
            if (circuit == null)
            {
                return NotFound();
            }

            return View(circuit);
        }

        // POST: Circuits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var circuit = await _context.Circuits.FindAsync(id);

            string circuitPhoto = circuit.Photo;
            if (circuit.Photo != null)
            {
                circuitPhoto = circuitPhoto.Replace("~", "wwwroot");
                System.IO.File.Delete(circuitPhoto);
            }

            _context.Circuits.Remove(circuit);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CircuitExists(int id)
        {
            return _context.Circuits.Any(e => e.Id == id);
        }
    }
}
