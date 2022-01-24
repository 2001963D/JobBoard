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
    public class LocationsController : ControllerBase
    {
        //Refactored
        //private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitofWork;

        //public LocationsController(ApplicationDbContext context)
        public LocationsController(IUnitOfWork unitofWork)
        {
            //Refactored
            //_context = context;
            _unitofWork = unitofWork;
        }

        // GET: api/Locations
        [HttpGet]
        //public async Task<ActionResult<IEnumerable<Location>>> GetLocations()
        public async Task<IActionResult> GetLocations()
        {
            //return await _context.Locations.ToListAsync();
            var Locations = await _unitofWork.Locations.GetAll();
            return Ok(Locations);   
        }

        // GET: api/Locations/5
        [HttpGet("{id}")]
        //public async Task<ActionResult<Location>> GetLocation(int id)
        public async Task<IActionResult> GetLocation(int id)
        {
            // var Location = await _context.Locations.FindAsync(id);
            var Location = await _unitofWork.Locations.Get(q => q.LocationId == id);

            if (Location == null)
            {
                return NotFound();
            }

            return Ok(Location);
        }

        // PUT: api/Locations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLocation(int id, Location Location)
        {
            if (id != Location.LocationId)
            {
                return BadRequest();
            }

            //_context.Entry(Location).State = EntityState.Modified;
            _unitofWork.Locations.Update(Location);

            try
            {
                //await _context.SaveChangesAsync();
                await _unitofWork.Save(HttpContext);
            }
            catch (DbUpdateConcurrencyException)
            {
                //if (!LocationExists(id))
                if (!await LocationExists(id))
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

        // POST: api/Locations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Location>> PostLocation(Location Location)
        {
            // _context.Locations.Add(Location);
            // await _context.SaveChangesAsync();
            await _unitofWork.Locations.Insert(Location);
            await _unitofWork.Save(HttpContext);

            return CreatedAtAction("GetLocation", new { id = Location.LocationId }, Location);
        }

        // DELETE: api/Locations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLocation(int id)
        {
            //var Location = await _context.Locations.FindAsync(id);
            var Location = await _unitofWork.Locations.Get(q => q.LocationId == id);
            if (Location == null)
            {
                return NotFound();
            }

            //_context.Locations.Remove(Location);
            // await _context.SaveChangesAsync();
            await _unitofWork.Locations.Delete(id);
            await _unitofWork.Save(HttpContext);

            return NoContent();
        }

        //private bool LocationExists(int id)
        private async Task<bool> LocationExists(int id)
        {
            //return _context.Locations.Any(e => e.Id == id);
            var Location = await _unitofWork.Locations.Get(q => q.LocationId == id);
            return Location != null;
        }
    }
}
