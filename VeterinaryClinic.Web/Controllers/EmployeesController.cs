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
    public class EmployeesController : Controller
    {
        private readonly VeterinaryClinicContext _context;

        public EmployeesController(VeterinaryClinicContext context)
        {
            _context = context;
        }

        // GET: Employees
        public async Task<IActionResult> Index()
        {
            var veterinaryClinicContext = _context.Employee.Include(e => e.DocumentType).Include(e => e.Rol).Include(e => e.WorkStation);
            return View(await veterinaryClinicContext.ToListAsync());
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee
                .Include(e => e.DocumentType)
                .Include(e => e.Rol)
                .Include(e => e.WorkStation)
                .FirstOrDefaultAsync(m => m.IdEmployee == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            ViewData["IdDocumentType"] = new SelectList(_context.DocumentType, "IdDocumentType", "NameDocumentType");
            ViewData["IdRol"] = new SelectList(_context.Rol, "IdRol", "NameRol");
            ViewData["IdWorkStation"] = new SelectList(_context.WorkStation, "IdWorkStation", "NameWorkStation");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdEmployee,NameEmployee,DocumentEmployee,IdDocumentType,Idspecialty,Telphone,Email,IdRol,IdWorkStation,DateCreate,DateUpdate")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdDocumentType"] = new SelectList(_context.DocumentType, "IdDocumentType", "NameDocumentType", employee.IdDocumentType);
            ViewData["IdRol"] = new SelectList(_context.Rol, "IdRol", "NameRol", employee.IdRol);
            ViewData["IdWorkStation"] = new SelectList(_context.WorkStation, "IdWorkStation", "NameWorkStation", employee.IdWorkStation);
            return View(employee);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            ViewData["IdDocumentType"] = new SelectList(_context.DocumentType, "IdDocumentType", "NameDocumentType", employee.IdDocumentType);
            ViewData["IdRol"] = new SelectList(_context.Rol, "IdRol", "NameRol", employee.IdRol);
            ViewData["IdWorkStation"] = new SelectList(_context.WorkStation, "IdWorkStation", "NameWorkStation", employee.IdWorkStation);
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdEmployee,NameEmployee,DocumentEmployee,IdDocumentType,Idspecialty,Telphone,Email,IdRol,IdWorkStation,DateCreate,DateUpdate")] Employee employee)
        {
            if (id != employee.IdEmployee)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.IdEmployee))
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
            ViewData["IdDocumentType"] = new SelectList(_context.DocumentType, "IdDocumentType", "NameDocumentType", employee.IdDocumentType);
            ViewData["IdRol"] = new SelectList(_context.Rol, "IdRol", "NameRol", employee.IdRol);
            ViewData["IdWorkStation"] = new SelectList(_context.WorkStation, "IdWorkStation", "NameWorkStation", employee.IdWorkStation);
            return View(employee);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee
                .Include(e => e.DocumentType)
                .Include(e => e.Rol)
                .Include(e => e.WorkStation)
                .FirstOrDefaultAsync(m => m.IdEmployee == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _context.Employee.FindAsync(id);
            _context.Employee.Remove(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employee.Any(e => e.IdEmployee == id);
        }
    }
}
