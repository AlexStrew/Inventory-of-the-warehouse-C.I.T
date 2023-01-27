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
    public class UnitsMeasurementsController : ControllerBase
    {
        private readonly InventarisationDbContext _context;

        public UnitsMeasurementsController(InventarisationDbContext context)
        {
            _context = context;
        }

        // GET: api/UnitsMeasurements
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UnitsMeasurement>>> GetUnitsMeasurements()
        {
            return await _context.UnitsMeasurements.ToListAsync();
        }

        // GET: api/UnitsMeasurements/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UnitsMeasurement>> GetUnitsMeasurement(int id)
        {
            var unitsMeasurement = await _context.UnitsMeasurements.FindAsync(id);

            if (unitsMeasurement == null)
            {
                return NotFound();
            }

            return unitsMeasurement;
        }

        // PUT: api/UnitsMeasurements/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUnitsMeasurement(int id, UnitsMeasurement unitsMeasurement)
        {
            if (id != unitsMeasurement.IdMeasure)
            {
                return BadRequest();
            }

            _context.Entry(unitsMeasurement).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UnitsMeasurementExists(id))
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

        // POST: api/UnitsMeasurements
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UnitsMeasurement>> PostUnitsMeasurement(UnitsMeasurement unitsMeasurement)
        {
            _context.UnitsMeasurements.Add(unitsMeasurement);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUnitsMeasurement", new { id = unitsMeasurement.IdMeasure }, unitsMeasurement);
        }

        // DELETE: api/UnitsMeasurements/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUnitsMeasurement(int id)
        {
            var unitsMeasurement = await _context.UnitsMeasurements.FindAsync(id);
            if (unitsMeasurement == null)
            {
                return NotFound();
            }

            _context.UnitsMeasurements.Remove(unitsMeasurement);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UnitsMeasurementExists(int id)
        {
            return _context.UnitsMeasurements.Any(e => e.IdMeasure == id);
        }
    }
}
