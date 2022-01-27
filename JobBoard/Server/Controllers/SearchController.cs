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
    public class SearchsController : ControllerBase
    {
        //Refactored
        //private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitofWork;

        //public SearchsController(ApplicationDbContext context)
        public SearchsController(IUnitOfWork unitofWork)
        {
            //Refactored
            //_context = context;
            _unitofWork = unitofWork;
        }

        // GET: api/Searchs
        [HttpGet]
        //public async Task<ActionResult<IEnumerable<Search>>> GetSearchs()
        public async Task<IActionResult> GetSearchs()
        {
            //return await _context.Searchs.ToListAsync();
            var Searchs = await _unitofWork.Searchs.GetAll();
            return Ok(Searchs);
        }

        // GET: api/Searchs/5
        [HttpGet("{id}")]
        //public async Task<ActionResult<Search>> GetSearch(int id)
        public async Task<IActionResult> GetSearch(int id)
        {
            // var Search = await _context.Searchs.FindAsync(id);
            var Search = await _unitofWork.Searchs.Get(q => q.SearchId == id);

            if (Search == null)
            {
                return NotFound();
            }

            return Ok(Search);
        }

        // PUT: api/Searchs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSearch(int id, Search Search)
        {
            if (id != Search.SearchId)
            {
                return BadRequest();
            }

            //_context.Entry(Search).State = EntityState.Modified;
            _unitofWork.Searchs.Update(Search);

            try
            {
                //await _context.SaveChangesAsync();
                await _unitofWork.Save(HttpContext);
            }
            catch (DbUpdateConcurrencyException)
            {
                //if (!SearchExists(id))
                if (!await SearchExists(id))
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

        // POST: api/Searchs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Search>> PostSearch(Search Search)
        {
            // _context.Searchs.Add(Search);
            // await _context.SaveChangesAsync();
            await _unitofWork.Searchs.Insert(Search);
            await _unitofWork.Save(HttpContext);

            return CreatedAtAction("GetSearch", new { id = Search.SearchId }, Search);
        }

        // DELETE: api/Searchs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSearch(int id)
        {
            //var Search = await _context.Searchs.FindAsync(id);
            var Search = await _unitofWork.Searchs.Get(q => q.SearchId == id);
            if (Search == null)
            {
                return NotFound();
            }

            //_context.Searchs.Remove(Search);
            // await _context.SaveChangesAsync();
            await _unitofWork.Searchs.Delete(id);
            await _unitofWork.Save(HttpContext);

            return NoContent();
        }

        //private bool SearchExists(int id)
        private async Task<bool> SearchExists(int id)
        {
            //return _context.Searchs.Any(e => e.Id == id);
            var Search = await _unitofWork.Searchs.Get(q => q.SearchId == id);
            return Search != null;
        }
    }
}
