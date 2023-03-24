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


        [HttpGet("/Connected/{id}")]
        public object JoinStatement()
        {
            using (_context)
            {
                var result = (from e in _context.RevisionItems
                              join d in _context.QueueLists on e.IdQueue equals d.id_list
                              join c in _context.Inventories on e.InventoryId equals c.Id
                              join g in _context.Nomenclatures on c.NomenclatureId equals g.IdNomenclature
                              
                              select new
                              {
                                  id_list = e.ListId,
                                  inv_num = c.InvNum,
                                  name_device = g.NameDevice,
                                  date_scan = e.DateScan,
                                  is_done = e.IsDone
                              }).ToList();
                // TODO utilize the above result

                return result;
            }
        }

        // GET: api/RevisionItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<RevisionItem>>> GetRevisionItem(int id)
        {
            // Исправлено: проверяем наличие элементов в таблице RevisionItems с помощью Any() вместо проверки на null
            if (!_context.RevisionItems.Any())
            {
                return NotFound();
            }

            // Исправлено: изменяем запрос для получения элемента по его идентификатору, а не по идентификатору списка
            var result = await (from e in _context.RevisionItems
                                join d in _context.QueueLists on e.IdQueue equals d.id_list
                                join c in _context.Inventories on e.InventoryId equals c.Id
                                join g in _context.Nomenclatures on c.NomenclatureId equals g.IdNomenclature
                                where e.ListId == id // фильтруем по идентификатору элемента
                                select new RevisionItem 
                                {
                                    ListId = e.ListId,
                                    InvNum = c.InvNum,
                                    NameDevice = g.NameDevice,
                                    DateScan = e.DateScan,
                                    IsDone = e.IsDone
                                }).ToListAsync();

            // Исправлено: переименовываем переменную для соответствия ее типу
            if (result == null)
            {
                return NotFound();
            }

            return result;
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
        public async Task<ActionResult<RevisionItem>> PostNomenclature(RevisionItem revision)
        {
            _context.RevisionItems.Add(revision);
            await _context.SaveChangesAsync();
            
            return CreatedAtAction("GetRevisionItems", new { id = revision.IdQueue }, revision);
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
