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
    public class WriteOffsController : ControllerBase
    {
        private readonly InventarisationDbContext _context;

        public WriteOffsController(InventarisationDbContext context)
        {
            _context = context;
        }

        // GET: api/WriteOffs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WriteOff>>> GetWriteOffs()
        {
            return await _context.WriteOffs.ToListAsync();
        }

        // GET: api/WriteOffs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WriteOff>> GetWriteOff(int id)
        {
            var writeOff = await _context.WriteOffs.FindAsync(id);

            if (writeOff == null)
            {
                return NotFound();
            }

            return writeOff;
        }

        // PUT: api/WriteOffs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWriteOff(int id, WriteOff writeOff)
        {
            if (id != writeOff.IdWriteoff)
            {
                return BadRequest();
            }

            _context.Entry(writeOff).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WriteOffExists(id))
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

        // POST: api/WriteOffs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<WriteOff>> PostWriteOff(WriteOff writeOff)
        {
            _context.WriteOffs.Add(writeOff);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWriteOff", new { id = writeOff.IdWriteoff }, writeOff);
        }

        // DELETE: api/WriteOffs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWriteOff(int id)
        {
            var writeOff = await _context.WriteOffs.FindAsync(id);
            if (writeOff == null)
            {
                return NotFound();
            }

            _context.WriteOffs.Remove(writeOff);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WriteOffExists(int id)
        {
            return _context.WriteOffs.Any(e => e.IdWriteoff == id);
        }
    }
}
