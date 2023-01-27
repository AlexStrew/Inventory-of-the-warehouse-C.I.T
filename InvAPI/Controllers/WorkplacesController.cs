using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InvAPI.Models;
using Microsoft.AspNetCore.Authorization;

namespace InvAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class WorkplacesController : ControllerBase
    {
        private readonly InventarisationDbContext _context;

        public WorkplacesController(InventarisationDbContext context)
        {
            _context = context;
        }

        // GET: api/Workplaces
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Workplace>>> GetWorkplaces()
        {
            return await _context.Workplaces.ToListAsync();
        }

        // GET: api/Workplaces/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Workplace>> GetWorkplace(int id)
        {
            var workplace = await _context.Workplaces.FindAsync(id);

            if (workplace == null)
            {
                return NotFound();
            }

            return workplace;
        }

        // PUT: api/Workplaces/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWorkplace(int id, Workplace workplace)
        {
            if (id != workplace.IdWorkplace)
            {
                return BadRequest();
            }

            _context.Entry(workplace).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkplaceExists(id))
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

        // POST: api/Workplaces
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Workplace>> PostWorkplace(Workplace workplace)
        {
            _context.Workplaces.Add(workplace);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWorkplace", new { id = workplace.IdWorkplace }, workplace);
        }

        // DELETE: api/Workplaces/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkplace(int id)
        {
            var workplace = await _context.Workplaces.FindAsync(id);
            if (workplace == null)
            {
                return NotFound();
            }

            _context.Workplaces.Remove(workplace);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WorkplaceExists(int id)
        {
            return _context.Workplaces.Any(e => e.IdWorkplace == id);
        }
    }
}
