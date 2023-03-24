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
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class QueueListsController : ControllerBase
    {
        private readonly InventarisationDbContext _context;

        public QueueListsController(InventarisationDbContext context)
        {
            _context = context;
        }

        // GET: api/QueueLists
        [HttpGet]
        public async Task<ActionResult<IEnumerable<QueueLists>>> GetQueueLists()
        {
          if (_context.QueueLists == null)
          {
              return NotFound();
          }
            return await _context.QueueLists.ToListAsync();
        }

        // GET: api/QueueLists/5
        [HttpGet("{id}")]
        public async Task<ActionResult<QueueLists>> GetQueueLists(int id)
        {
          if (_context.QueueLists == null)
          {
              return NotFound();
          }
            var QueueLists = await _context.QueueLists.FindAsync(id);

            if (QueueLists == null)
            {
                return NotFound();
            }

            return QueueLists;
        }

        // PUT: api/QueueLists/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQueueLists(int id, QueueLists QueueLists)
        {
            if (id != QueueLists.id_list)
            {
                return BadRequest();
            }

            _context.Entry(QueueLists).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QueueListsExists(id))
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

        // POST: api/QueueLists
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<QueueLists>> PostNomenclature(QueueLists queue)
        {
            _context.QueueLists.Add(queue);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetQueueLists", new { id = queue.id_list }, queue);
        }

        // DELETE: api/QueueLists/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQueueLists(int id)
        {
            if (_context.QueueLists == null)
            {
                return NotFound();
            }
            var QueueLists = await _context.QueueLists.FindAsync(id);
            if (QueueLists == null)
            {
                return NotFound();
            }

            _context.QueueLists.Remove(QueueLists);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool QueueListsExists(int id_list)
        {
            return (_context.QueueLists?.Any(e => e.id_list == id_list)).GetValueOrDefault();
        }
    }
}
