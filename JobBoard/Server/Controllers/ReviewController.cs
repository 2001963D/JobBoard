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
    public class ReviewsController : ControllerBase
    {
        //Refactored
        //private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitofWork;

        //public ReviewsController(ApplicationDbContext context)
        public ReviewsController(IUnitOfWork unitofWork)
        {
            //Refactored
            //_context = context;
            _unitofWork = unitofWork;
        }

        // GET: api/Reviews
        [HttpGet]
        //public async Task<ActionResult<IEnumerable<Review>>> GetReviews()
        public async Task<IActionResult> GetReviews()
        {
            //return await _context.Reviews.ToListAsync();
            var Reviews = await _unitofWork.Reviews.GetAll();
            return Ok(Reviews);
        }

        // GET: api/Reviews/5
        [HttpGet("{id}")]
        //public async Task<ActionResult<Review>> GetReview(int id)
        public async Task<IActionResult> GetReview(int id)
        {
            // var Review = await _context.Reviews.FindAsync(id);
            var Review = await _unitofWork.Reviews.GetAll(q => q.ListingId == id);

            if (Review == null)
            {
                return NotFound();
            }

            return Ok(Review);
        }

        // PUT: api/Reviews/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReview(int id, Review Review)
        {
            if (id != Review.Id)
            {
                return BadRequest();
            }

            //_context.Entry(Review).State = EntityState.Modified;
            _unitofWork.Reviews.Update(Review);

            try
            {
                //await _context.SaveChangesAsync();
                await _unitofWork.Save(HttpContext);
            }
            catch (DbUpdateConcurrencyException)
            {
                //if (!ReviewExists(id))
                if (!await ReviewExists(id))
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

        // POST: api/Reviews
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Review>> PostReview(Review Review)
        {
            // _context.Reviews.Add(Review);
            // await _context.SaveChangesAsync();
            await _unitofWork.Reviews.Insert(Review);
            await _unitofWork.Save(HttpContext);

            return CreatedAtAction("GetReview", new { id = Review.Id }, Review);
        }

        // DELETE: api/Reviews/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            //var Review = await _context.Reviews.FindAsync(id);
            var Review = await _unitofWork.Reviews.Get(q => q.Id == id);
            if (Review == null)
            {
                return NotFound();
            }

            //_context.Reviews.Remove(Review);
            // await _context.SaveChangesAsync();
            await _unitofWork.Reviews.Delete(id);
            await _unitofWork.Save(HttpContext);

            return NoContent();
        }

        //private bool ReviewExists(int id)
        private async Task<bool> ReviewExists(int id)
        {
            //return _context.Reviews.Any(e => e.Id == id);
            var Review = await _unitofWork.Reviews.Get(q => q.Id == id);
            return Review != null;
        }
    }
}
