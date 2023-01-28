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
    public class PatientsController : Controller
    {
        private readonly VeterinaryClinicContext _context;

        public PatientsController(VeterinaryClinicContext context)
        {
            _context = context;
        }
        //Hola amor ¿estas por ahí?

        // GET: Patients
        public async Task<IActionResult> Index()
        {
            var veterinaryClinicContext = _context.Patient.Include(p => p.Owner).Include(p => p.Race).Include(p => p.Specie);
            return View(await veterinaryClinicContext.ToListAsync());
        }

        // GET: Patients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = await _context.Patient
                .Include(p => p.Owner)
                .Include(p => p.Race)
                .Include(p => p.Specie)
                .FirstOrDefaultAsync(m => m.IdPatient == id);
            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        // GET: Patients/Create
        public IActionResult Create()
        {
            ViewData["IdOwner"] = new SelectList(_context.Owner, "IdOwner", "NameOwner");
            ViewData["IdRace"] = new SelectList(_context.Race, "IdRace", "NameRace");
            ViewData["IdSpecie"] = new SelectList(_context.Specie, "IdSpecie", "NameSpecie");
            return View();
        }

        // POST: Patients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPatient,NamePatient,IdOwner,IdSpecie,IdRace,Age,DateRegistration")] Patient patient)
        {
            if (ModelState.IsValid)
            {
                _context.Add(patient);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdOwner"] = new SelectList(_context.Owner, "IdOwner", "NameOwner", patient.IdOwner);
            ViewData["IdRace"] = new SelectList(_context.Race, "IdRace", "NameRace", patient.IdRace);
            ViewData["IdSpecie"] = new SelectList(_context.Specie, "IdSpecie", "NameSpecie", patient.IdSpecie);
            return View(patient);
        }

        // GET: Patients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = await _context.Patient.FindAsync(id);
            if (patient == null)
            {
                return NotFound();
            }
            ViewData["IdOwner"] = new SelectList(_context.Owner, "IdOwner", "NameOwner", patient.IdOwner);
            ViewData["IdRace"] = new SelectList(_context.Race, "IdRace", "NameRace", patient.IdRace);
            ViewData["IdSpecie"] = new SelectList(_context.Specie, "IdSpecie", "NameSpecie", patient.IdSpecie);
            return View(patient);
        }

        // POST: Patients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPatient,NamePatient,IdOwner,IdSpecie,IdRace,Age,DateRegistration")] Patient patient)
        {
            if (id != patient.IdPatient)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(patient);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PatientExists(patient.IdPatient))
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
            ViewData["IdOwner"] = new SelectList(_context.Owner, "IdOwner", "NameOwner", patient.IdOwner);
            ViewData["IdRace"] = new SelectList(_context.Race, "IdRace", "NameRace", patient.IdRace);
            ViewData["IdSpecie"] = new SelectList(_context.Specie, "IdSpecie", "NameSpecie", patient.IdSpecie);
            return View(patient);
        }

        // GET: Patients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = await _context.Patient
                .Include(p => p.Owner)
                .Include(p => p.Race)
                .Include(p => p.Specie)
                .FirstOrDefaultAsync(m => m.IdPatient == id);
            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        // POST: Patients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var patient = await _context.Patient.FindAsync(id);
            _context.Patient.Remove(patient);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PatientExists(int id)
        {
            return _context.Patient.Any(e => e.IdPatient == id);
        }
    }
}
