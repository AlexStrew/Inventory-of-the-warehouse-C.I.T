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
    public class RevisionItemsController : ControllerBase
    {
        private readonly InventarisationDbContext _context;

        public RevisionItemsController(InventarisationDbContext context)
        {
            _context = context;
        }

        // GET: api/RevisionItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RevisionItem>>> GetRevisionItems()
        {
          if (_context.RevisionItems == null)
          {
              return NotFound();
          }
            return await _context.RevisionItems.ToListAsync();
        }

        // GET: api/RevisionItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RevisionItem>> GetRevisionItem(int id)
        {
          if (_context.RevisionItems == null)
          {
              return NotFound();
          }
            var revisionItem = await _context.RevisionItems.FindAsync(id);

            if (revisionItem == null)
            {
                return NotFound();
            }

            return revisionItem;
        }

        // PUT: api/RevisionItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRevisionItem(int id, RevisionItem revisionItem)
        {
            if (id != revisionItem.IdQueue)
            {
                return BadRequest();
            }

            _context.Entry(revisionItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RevisionItemExists(id))
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

        // POST: api/RevisionItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RevisionItem>> PostRevisionItem(RevisionItem revisionItem)
        {
          if (_context.RevisionItems == null)
          {
              return Problem("Entity set 'InventarisationDbContext.RevisionItems'  is null.");
          }
            _context.RevisionItems.Add(revisionItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRevisionItem", new { id = revisionItem.IdQueue }, revisionItem);
        }

        // DELETE: api/RevisionItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRevisionItem(int id)
        {
            if (_context.RevisionItems == null)
            {
                return NotFound();
            }
            var revisionItem = await _context.RevisionItems.FindAsync(id);
            if (revisionItem == null)
            {
                return NotFound();
            }

            _context.RevisionItems.Remove(revisionItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RevisionItemExists(int id)
        {
            return (_context.RevisionItems?.Any(e => e.IdQueue == id)).GetValueOrDefault();
        }
    }
}
