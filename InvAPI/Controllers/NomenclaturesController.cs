using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InvAPI.Models;

namespace InvAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NomenclaturesController : ControllerBase
    {
        private readonly InventarisationDbContext _context;

        public NomenclaturesController(InventarisationDbContext context)
        {
            _context = context;
        }

        // GET: api/Nomenclatures
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Nomenclature>>> GetNomenclatures()
        {
            return await _context.Nomenclatures.ToListAsync();
        }

        // GET: api/Nomenclatures/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Nomenclature>> GetNomenclature(int id)
        {
            var nomenclature = await _context.Nomenclatures.FindAsync(id);

            if (nomenclature == null)
            {
                return NotFound();
            }

            return nomenclature;
        }

        // PUT: api/Nomenclatures/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNomenclature(int id, Nomenclature nomenclature)
        {
            if (id != nomenclature.IdNomenclature)
            {
                return BadRequest();
            }

            _context.Entry(nomenclature).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NomenclatureExists(id))
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

        // POST: api/Nomenclatures
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Nomenclature>> PostNomenclature(Nomenclature nomenclature)
        {
            _context.Nomenclatures.Add(nomenclature);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNomenclature", new { id = nomenclature.IdNomenclature }, nomenclature);
        }

        // DELETE: api/Nomenclatures/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNomenclature(int id)
        {
            var nomenclature = await _context.Nomenclatures.FindAsync(id);
            if (nomenclature == null)
            {
                return NotFound();
            }

            _context.Nomenclatures.Remove(nomenclature);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NomenclatureExists(int id)
        {
            return _context.Nomenclatures.Any(e => e.IdNomenclature == id);
        }
    }
}
