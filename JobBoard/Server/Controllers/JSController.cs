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
    public class JSsController : ControllerBase
    {
        //Refactored
        //private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitofWork;

        //public JSsController(ApplicationDbContext context)
        public JSsController(IUnitOfWork unitofWork)
        {
            //Refactored
            //_context = context;
            _unitofWork = unitofWork;
        }

        // GET: api/JSs
        [HttpGet]
        //public async Task<ActionResult<IEnumerable<JS>>> GetJSs()
        public async Task<IActionResult> GetJSs()
        {

            //return await _context.JSs.ToListAsync();
            var JSs = await _unitofWork.JSs.GetAll();
            return Ok(JSs);
        }

        // GET: api/JSs/5
        [HttpGet("{id}")]
        //public async Task<ActionResult<JS>> GetJS(int id)
        public async Task<IActionResult> GetJS(int id)
        {
            // var JS = await _context.JSs.FindAsync(id);
            var JS = await _unitofWork.JSs.Get(q => q.Id == id);

            if (JS == null)
            {
                return NotFound();
            }

            return Ok(JS);
        }

        // PUT: api/JSs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutJS(int id, JS JS)
        {
            if (id != JS.Id)
            {
                return BadRequest();
            }

            //_context.Entry(JS).State = EntityState.Modified;
            _unitofWork.JSs.Update(JS);

            try
            {
                //await _context.SaveChangesAsync();
                await _unitofWork.Save(HttpContext);
            }
            catch (DbUpdateConcurrencyException)
            {
                //if (!JSExists(id))
                if (!await JSExists(id))
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

        // POST: api/JSs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<JS>> PostJS(JS JS)
        {
            // _context.JSs.Add(JS);
            // await _context.SaveChangesAsync();
            await _unitofWork.JSs.Insert(JS);
            await _unitofWork.Save(HttpContext);

            return CreatedAtAction("GetJS", new { id = JS.Id }, JS);
        }

        // DELETE: api/JSs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJS(int id)
        {
            //var JS = await _context.JSs.FindAsync(id);
            var JS = await _unitofWork.JSs.Get(q => q.Id == id);
            if (JS == null)
            {
                return NotFound();
            }

            //_context.JSs.Remove(JS);
            // await _context.SaveChangesAsync();
            await _unitofWork.JSs.Delete(id);
            await _unitofWork.Save(HttpContext);

            return NoContent();
        }

        //private bool JSExists(int id)
        private async Task<bool> JSExists(int id)
        {
            //return _context.JSs.Any(e => e.Id == id);
            var JS = await _unitofWork.JSs.Get(q => q.Id == id);
            return JS != null;
        }
    }
}
