using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VeterinaryClinic.Data.Context;
using VeterinaryClinic.Entities.Entities;

namespace VeterinaryClinic.Web.Controllers
{
    public class WorkStationsController : Controller
    {
        private readonly VeterinaryClinicContext _context;

        public WorkStationsController(VeterinaryClinicContext context)
        {
            _context = context;
        }

        // GET: WorkStations
        public async Task<IActionResult> Index()
        {
            return View(await _context.WorkStation.ToListAsync());
        }

        // GET: WorkStations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workStation = await _context.WorkStation
                .FirstOrDefaultAsync(m => m.IdWorkStation == id);
            if (workStation == null)
            {
                return NotFound();
            }

            return View(workStation);
        }

        // GET: WorkStations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: WorkStations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdWorkStation,NameWorkStation,DateCreate,DateUpdate")] WorkStation workStation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(workStation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(workStation);
        }

        // GET: WorkStations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workStation = await _context.WorkStation.FindAsync(id);
            if (workStation == null)
            {
                return NotFound();
            }
            return View(workStation);
        }

        // POST: WorkStations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdWorkStation,NameWorkStation,DateCreate,DateUpdate")] WorkStation workStation)
        {
            if (id != workStation.IdWorkStation)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(workStation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorkStationExists(workStation.IdWorkStation))
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
            return View(workStation);
        }

        // GET: WorkStations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workStation = await _context.WorkStation
                .FirstOrDefaultAsync(m => m.IdWorkStation == id);
            if (workStation == null)
            {
                return NotFound();
            }

            return View(workStation);
        }

        // POST: WorkStations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var workStation = await _context.WorkStation.FindAsync(id);
            _context.WorkStation.Remove(workStation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WorkStationExists(int id)
        {
            return _context.WorkStation.Any(e => e.IdWorkStation == id);
        }
    }
}
