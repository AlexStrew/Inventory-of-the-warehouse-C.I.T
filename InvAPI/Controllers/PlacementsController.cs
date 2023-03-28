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
    public class PlacementsController : ControllerBase
    {
        private readonly InventarisationDbContext _context;

        public PlacementsController(InventarisationDbContext context)
        {
            _context = context;
        }

        // GET: api/Placements
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Placement>>> GetPlacements()
        {
            return await _context.Placements.ToListAsync();
        }

        // GET: api/Placements/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Placement>> GetPlacement(int id)
        {
            var placement = await _context.Placements.FindAsync(id);

            if (placement == null)
            {
                return NotFound();
            }

            return placement;
        }

        // PUT: api/Placements/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlacement(int id, Placement placement)
        {
            if (id != placement.IdPlacement)
            {
                return BadRequest();
            }

            _context.Entry(placement).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlacementExists(id))
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

        // POST: api/Placements
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Placement>> PostPlacement(Placement placement)
        {
            _context.Placements.Add(placement);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPlacement", new { id = placement.IdPlacement }, placement);
        }

        // DELETE: api/Placements/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlacement(int id)
        {
            bool isValueUsed = await _context.Movements.AnyAsync(p => p.IdMovement == id);

            if (isValueUsed)
            {
                return BadRequest("Значение используется в другой таблице");
            }

            var placement = await _context.Placements.FindAsync(id);
            if (placement == null)
            {
                return NotFound();
            }

            _context.Placements.Remove(placement);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PlacementExists(int id)
        {
            return _context.Placements.Any(e => e.IdPlacement == id);
        }
    }
}
