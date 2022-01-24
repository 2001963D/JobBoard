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
    public class EmployersController : ControllerBase
    {
        //Refactored
        //private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitofWork;

        //public EmployersController(ApplicationDbContext context)
        public EmployersController(IUnitOfWork unitofWork)
        {
            //Refactored
            //_context = context;
            _unitofWork = unitofWork;
        }

        // GET: api/Employers
        [HttpGet]
        //public async Task<ActionResult<IEnumerable<Employer>>> GetEmployers()
        public async Task<IActionResult> GetEmployers()
        {
            //return await _context.Employers.ToListAsync();
            var Employers = await _unitofWork.Employers.GetAll();
            return Ok(Employers);
        }

        // GET: api/Employers/5
        [HttpGet("{id}")]
        //public async Task<ActionResult<Employer>> GetEmployer(int id)
        public async Task<IActionResult> GetEmployer(int id)
        {
            // var Employer = await _context.Employers.FindAsync(id);
            var Employer = await _unitofWork.Employers.Get(q => q.Id == id);

            if (Employer == null)
            {
                return NotFound();
            }

            return Ok(Employer);
        }

        // PUT: api/Employers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployer(int id, Employer Employer)
        {
            if (id != Employer.Id)
            {
                return BadRequest();
            }

            //_context.Entry(Employer).State = EntityState.Modified;
            _unitofWork.Employers.Update(Employer);

            try
            {
                //await _context.SaveChangesAsync();
                await _unitofWork.Save(HttpContext);
            }
            catch (DbUpdateConcurrencyException)
            {
                //if (!EmployerExists(id))
                if (!await EmployerExists(id))
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

        // POST: api/Employers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Employer>> PostEmployer(Employer Employer)
        {
            // _context.Employers.Add(Employer);
            // await _context.SaveChangesAsync();
            await _unitofWork.Employers.Insert(Employer);
            await _unitofWork.Save(HttpContext);

            return CreatedAtAction("GetEmployer", new { id = Employer.Id }, Employer);
        }

        // DELETE: api/Employers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployer(int id)
        {
            //var Employer = await _context.Employers.FindAsync(id);
            var Employer = await _unitofWork.Employers.Get(q => q.Id == id);
            if (Employer == null)
            {
                return NotFound();
            }

            //_context.Employers.Remove(Employer);
            // await _context.SaveChangesAsync();
            await _unitofWork.Employers.Delete(id);
            await _unitofWork.Save(HttpContext);

            return NoContent();
        }

        //private bool EmployerExists(int id)
        private async Task<bool> EmployerExists(int id)
        {
            //return _context.Employers.Any(e => e.Id == id);
            var Employer = await _unitofWork.Employers.Get(q => q.Id == id);
            return Employer != null;
        }
    }
}
