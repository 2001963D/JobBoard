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
    public class EnquirysController : ControllerBase
    {
        //Refactored
        //private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitofWork;

        //public EnquirysController(ApplicationDbContext context)
        public EnquirysController(IUnitOfWork unitofWork)
        {
            //Refactored
            //_context = context;
            _unitofWork = unitofWork;
        }

        // GET: api/Enquirys
        [HttpGet]
        //public async Task<ActionResult<IEnumerable<Enquiry>>> GetEnquirys()
        public async Task<IActionResult> GetEnquirys()
        {
            //return await _context.Enquirys.ToListAsync();
            var Enquirys = await _unitofWork.Enquirys.GetAll();
            return Ok(Enquirys);
        }

        // GET: api/Enquirys/5
        [HttpGet("{id}")]
        //public async Task<ActionResult<Enquiry>> GetEnquiry(int id)
        public async Task<IActionResult> GetEnquiry(int id)
        {
            // var Enquiry = await _context.Enquirys.FindAsync(id);
            var Enquiry = await _unitofWork.Enquirys.Get(q => q.Id == id);

            if (Enquiry == null)
            {
                return NotFound();
            }

            return Ok(Enquiry);
        }

        // PUT: api/Enquirys/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEnquiry(int id, Enquiry Enquiry)
        {
            if (id != Enquiry.Id)
            {
                return BadRequest();
            }

            //_context.Entry(Enquiry).State = EntityState.Modified;
            _unitofWork.Enquirys.Update(Enquiry);

            try
            {
                //await _context.SaveChangesAsync();
                await _unitofWork.Save(HttpContext);
            }
            catch (DbUpdateConcurrencyException)
            {
                //if (!EnquiryExists(id))
                if (!await EnquiryExists(id))
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

        // POST: api/Enquirys
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Enquiry>> PostEnquiry(Enquiry Enquiry)
        {
            // _context.Enquirys.Add(Enquiry);
            // await _context.SaveChangesAsync();
            await _unitofWork.Enquirys.Insert(Enquiry);
            await _unitofWork.Save(HttpContext);

            return CreatedAtAction("GetEnquiry", new { id = Enquiry.Id }, Enquiry);
        }

        // DELETE: api/Enquirys/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEnquiry(int id)
        {
            //var Enquiry = await _context.Enquirys.FindAsync(id);
            var Enquiry = await _unitofWork.Enquirys.Get(q => q.Id == id);
            if (Enquiry == null)
            {
                return NotFound();
            }

            //_context.Enquirys.Remove(Enquiry);
            // await _context.SaveChangesAsync();
            await _unitofWork.Enquirys.Delete(id);
            await _unitofWork.Save(HttpContext);

            return NoContent();
        }

        //private bool EnquiryExists(int id)
        private async Task<bool> EnquiryExists(int id)
        {
            //return _context.Enquirys.Any(e => e.Id == id);
            var Enquiry = await _unitofWork.Enquirys.Get(q => q.Id == id);
            return Enquiry != null;
        }
    }
}
