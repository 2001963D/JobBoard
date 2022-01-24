using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JobBoard.Server.Data;
using JobBoard.Shared.Domain;
using JobBoard.Server.IRepository;

namespace JobBoard.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        //Refactored
        //private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitofWork;

        //public AppointmentsController(ApplicationDbContext context)
        public AppointmentsController(IUnitOfWork unitofWork)
        {
            //Refactored
            //_context = context;
            _unitofWork = unitofWork;
        }

        // GET: api/Appointments
        [HttpGet]
        //public async Task<ActionResult<IEnumerable<Appointment>>> GetAppointments()
        public async Task<IActionResult> GetAppointments()
        {
            //return await _context.Appointments.ToListAsync();
            var Appointments = await _unitofWork.Appointments.GetAll();
            return Ok(Appointments);
        }

        // GET: api/Appointments/5
        [HttpGet("{id}")]
        //public async Task<ActionResult<Appointment>> GetAppointment(int id)
        public async Task<IActionResult> GetAppointment(int id)
        {
            // var Appointment = await _context.Appointments.FindAsync(id);
            var Appointment = await _unitofWork.Appointments.Get(q => q.Id == id);

            if (Appointment == null)
            {
                return NotFound();
            }

            return Ok(Appointment);
        }

        // PUT: api/Appointments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAppointment(int id, Appointment Appointment)
        {
            if (id != Appointment.Id)
            {
                return BadRequest();
            }

            //_context.Entry(Appointment).State = EntityState.Modified;
            _unitofWork.Appointments.Update(Appointment);

            try
            {
                //await _context.SaveChangesAsync();
                await _unitofWork.Save(HttpContext);
            }
            catch (DbUpdateConcurrencyException)
            {
                //if (!AppointmentExists(id))
                if (!await AppointmentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Appointments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Appointment>> PostAppointment(Appointment Appointment)
        {
            // _context.Appointments.Add(Appointment);
            // await _context.SaveChangesAsync();
            await _unitofWork.Appointments.Insert(Appointment);
            await _unitofWork.Save(HttpContext);

            return CreatedAtAction("GetAppointment", new { id = Appointment.Id }, Appointment);
        }

        // DELETE: api/Appointments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            //var Appointment = await _context.Appointments.FindAsync(id);
            var Appointment = await _unitofWork.Appointments.Get(q => q.Id == id);
            if (Appointment == null)
            {
                return NotFound();
            }

            //_context.Appointments.Remove(Appointment);
            // await _context.SaveChangesAsync();
            await _unitofWork.Appointments.Delete(id);
            await _unitofWork.Save(HttpContext);

            return NoContent();
        }

        //private bool AppointmentExists(int id)
        private async Task<bool> AppointmentExists(int id)
        {
            //return _context.Appointments.Any(e => e.Id == id);
            var Appointment = await _unitofWork.Appointments.Get(q => q.Id == id);
            return Appointment != null;
        }
    }
}
