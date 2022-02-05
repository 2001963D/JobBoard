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
    public class ListingsController : ControllerBase
    {
        //Refactored
        //private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitofWork;

        //public ListingsController(ApplicationDbContext context)
        public ListingsController(IUnitOfWork unitofWork)
        {
            //Refactored
            //_context = context;
            _unitofWork = unitofWork;
        }

        // GET: api/Listings
        [HttpGet]
        //public async Task<ActionResult<IEnumerable<Listing>>> GetListings()
        public async Task<IActionResult> GetListings()
        {
            //return await _context.Listings.ToListAsync();
            var Listings = await _unitofWork.Listings.GetAll(includes: q => q.Include(x =>x.Employer).Include(x => x.Location));
            return Ok(Listings);
        }

        // GET: api/Listings/5
        [HttpGet("{id}")]
        //public async Task<ActionResult<Listing>> GetListing(int id)
        public async Task<IActionResult> GetListing(int id)
        {
            // var Listing = await _context.Listings.FindAsync(id);
            var Listing = await _unitofWork.Listings.Get(q => q.Id == id);

            if (Listing == null)
            {
                return NotFound();
            }

            return Ok(Listing);
        }

        // PUT: api/Listings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutListing(int id, Listing Listing)
        {
            if (id != Listing.Id)
            {
                return BadRequest();
            }

            //_context.Entry(Listing).State = EntityState.Modified;
            _unitofWork.Listings.Update(Listing);

            try
            {
                //await _context.SaveChangesAsync();
                await _unitofWork.Save(HttpContext);
            }
            catch (DbUpdateConcurrencyException)
            {
                //if (!ListingExists(id))
                if (!await ListingExists(id))
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

        // POST: api/Listings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Listing>> PostListing(Listing Listing)
        {
            // _context.Listings.Add(Listing);
            // await _context.SaveChangesAsync();
            await _unitofWork.Listings.Insert(Listing);
            await _unitofWork.Save(HttpContext);

            return CreatedAtAction("GetListing", new { id = Listing.Id }, Listing);
        }

        // DELETE: api/Listings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteListing(int id)
        {
            //var Listing = await _context.Listings.FindAsync(id);
            var Listing = await _unitofWork.Listings.Get(q => q.Id == id);
            if (Listing == null)
            {
                return NotFound();
            }

            //_context.Listings.Remove(Listing);
            // await _context.SaveChangesAsync();
            await _unitofWork.Listings.Delete(id);
            await _unitofWork.Save(HttpContext);

            return NoContent();
        }

        //private bool ListingExists(int id)
        private async Task<bool> ListingExists(int id)
        {
            //return _context.Listings.Any(e => e.Id == id);
            var Listing = await _unitofWork.Listings.Get(q => q.Id == id);
            return Listing != null;
        }
    }
}
