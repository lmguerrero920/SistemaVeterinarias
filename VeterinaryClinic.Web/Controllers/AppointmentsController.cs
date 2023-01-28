using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using VeterinaryClinic.Data.Context;
using VeterinaryClinic.Entities.Entities;
using VeterinaryClinic.Common.Enums;
using VeterinaryClinic.Common.Class;

namespace VeterinaryClinic.Web.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly VeterinaryClinicContext _context;
        public int value = 0;
        ClassStrings cls = new ClassStrings();

        public AppointmentsController(VeterinaryClinicContext context)
        {
            _context = context;
        }

        // GET: Appointments
        public async Task<IActionResult> Index()
        {
            var veterinaryClinicContext = _context.Appointment.Include(a => a.Employee).Include(a => a.Patient).Include(a => a.Room);
            return View(await veterinaryClinicContext.ToListAsync());
        }

        // GET: Appointments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointment
                .Include(a => a.Employee)
                .Include(a => a.Patient)
                .Include(a => a.Room)
                .FirstOrDefaultAsync(m => m.IdAppointment == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // GET: Appointments/Create
        public IActionResult Create()
        {
            #region Instance
            ViewData["IdEmployee"] = new SelectList(_context.Employee, "IdEmployee", "NameEmployee");
            ViewData["IdPatient"] = new SelectList(_context.Patient, "IdPatient", "NamePatient");
            ViewData["IdRoom"] = new SelectList(_context.Room, "IdRoom", "NameRoom");
            return View();
            #endregion
        }

        // POST: Appointments/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<Appointment>> CreateApi( Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    List<bool> existInArray = ValidExist( appointment);

                    if (existInArray[0].Equals(true))
                    {
                        return Problem(cls.TwoRows, null, 409);
                    }
                    else
                    {
                        value = (int)EnumSP.Insert;
                        StoreProcedure(appointment, 0);
                    }
                    //https://www.superprof.es/diccionario/matematicas/calculo/pendiente-recta.html
                    //https://phet.colorado.edu/sims/html/hookes-law/latest/hookes-law_es.html
                    //https://qastack.mx/programming/21519203/plotting-a-list-of-x-y-coordinates-in-python-matplotlib
                    //https://techmake.com/blogs/tutoriales/empezando-con-arduino-3a-sensor-de-temperatura-en-monitor-del-arduino-ide
                    //https://naylampmechatronics.com/blog/10_tutorial-de-arduino-y-sensor-ultrasonico-hc-sr04.html
                    //https://techmake.com/blogs/tutoriales/empezando-con-arduino-3a-sensor-de-temperatura-en-monitor-del-arduino-ide
                    //https://www.superprof.es/diccionario/matematicas/calculo/pendiente-recta.html
                }
                catch (Exception)
                {

                    throw;
                }

            }
            return null;
            
        }

        private void StoreProcedure(  Appointment appointment ,int id)
        {
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();

            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            switch (value)
            {

                case (int)EnumSP.Insert:
                    cmd.CommandText = cls.SpInsertAppointment;
                    ClassProperty(appointment, cmd);
                    //  ¡Stella Rojas, TE AMO!
                    break;

                case (int)EnumSP.Update:
                    //cmd.CommandText = cls.SpUpdateAnswer;
                    //ClassProperty(answerElement, cmd);
                    break;

                case (int)EnumSP.Delete: 

                    //cmd.CommandText = cls.SpDeleteAnswer;
                    //cmd.Parameters.Add(cls.IdAnswer, System.Data.SqlDbType.Int).Value = id;
                    break;

                default:
                    Console.WriteLine("");
                    break;
            }
            cmd.ExecuteNonQuery();
            conn.Close();

        }

        private void ClassProperty(object answerElement, SqlCommand cmd)
        {
            throw new NotImplementedException();
        }

        private List<bool> ValidExist(Appointment appointment)
        {

            var filterAll = _context.Appointment.ToList();
            var filter = _context.Appointment.Where(x => x.IdAppointment != appointment.IdAppointment).ToList();
            var listFilter = filter.GroupBy(n => n.NameAppointment).ToList();
            var nameProperty = appointment.NameAppointment;
            var arrayList = listFilter.ToArray();
            bool modifyInArray = false;
            bool existInArray = false;

           var findItem = filterAll.Find(n => n.IdAppointment == appointment.IdAppointment);

            if (findItem == null)
            {
                modifyInArray = false;
            }
            else if (findItem.DateEndAppointment != appointment.DateEndAppointment || findItem.DateEndAppointment
                != findItem.DateStartAppointment || findItem.Patient != findItem.Patient)
            {
                modifyInArray = true;

            }

            for (int i = 0; i < arrayList.Length; i++)
            {
                if (arrayList[i].Key.ToUpper() == nameProperty.ToUpper())
                {
                    existInArray = true;
                }

            }
            List<bool> valid = new List<bool>();
            valid.Add(existInArray);   
            valid.Add(modifyInArray);

            return valid;

        }

        // GET: Appointments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointment.FindAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }
            ViewData["IdEmployee"] = new SelectList(_context.Employee, "IdEmployee", "NameEmployee", appointment.IdEmployee);
            ViewData["IdPatient"] = new SelectList(_context.Patient, "IdPatient", "NamePatient", appointment.IdPatient);
            ViewData["IdRoom"] = new SelectList(_context.Room, "IdRoom", "NameRoom", appointment.IdRoom);
            return View(appointment);
        }

        // POST: Appointments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,  Appointment appointment)
        {
            if (id != appointment.IdAppointment)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(appointment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppointmentExists(appointment.IdAppointment))
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
            ViewData["IdEmployee"] = new SelectList(_context.Employee, "IdEmployee", "NameEmployee", appointment.IdEmployee);
            ViewData["IdPatient"] = new SelectList(_context.Patient, "IdPatient", "NamePatient", appointment.IdPatient);
            ViewData["IdRoom"] = new SelectList(_context.Room, "IdRoom", "NameRoom", appointment.IdRoom);
            return View(appointment);
        }

        // GET: Appointments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointment
                .Include(a => a.Employee)
                .Include(a => a.Patient)
                .Include(a => a.Room)
                .FirstOrDefaultAsync(m => m.IdAppointment == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // POST: Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var appointment = await _context.Appointment.FindAsync(id);
            _context.Appointment.Remove(appointment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AppointmentExists(int id)
        {
            return _context.Appointment.Any(e => e.IdAppointment == id);
        }
    }
}
