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
    public class InventoriesController : ControllerBase
    {
        //private readonly InventarisationDbContext _context;
        InventarisationDbContext _context = new InventarisationDbContext();

        public InventoriesController(InventarisationDbContext context)
        {
            _context = context;
        }

        [Route("ConnectedTables")]
        [HttpGet]
        public object JoinStatement()
        {
            using (_context)
            {
                var result = (from e in _context.Inventories
                              join d in _context.Nomenclatures on e.NomenclatureId equals d.IdNomenclature
                              join b in _context.Workplaces on e.WorkplaceId equals b.IdWorkplace
                              join c in _context.Movements on e.MoveId equals c.IdMovement
                              join f in _context.Companies on e.CompanyId equals f.IdCompany
                              select new
                              {
                                  id = e.Id,
                                  inv_num = e.InvNum,
                                  payment_num = e.PaymentNum,
                                  comment = e.Comment,
                                  invoice = e.Invoice,
                                  nomenclature_id = e.NomenclatureId,
                                  name_device = d.NameDevice,
                                  id_workplace = b.IdWorkplace,
                                  name_workplace = b.NameWorkplace,
                                  id_movement = c.IdMovement,
                                  date_move = c.DateMove,
                                  id_company = f.IdCompany,
                                  company_name = f.CompanyName
                              }).ToList();
                // TODO utilize the above result

                return result;
            }
        }

        // GET: api/Inventories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Inventory>>> GetInventories()
        {
            return await _context.Inventories.ToListAsync();
        }

        // GET: api/Inventories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Inventory>> GetInventory(int id)
        {
            var inventory = await _context.Inventories.FindAsync(id);

            if (inventory == null)
            {
                return NotFound();
            }

            return inventory;
        }

        // PUT: api/Inventories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInventory(int id, Inventory inventory)
        {
            if (id != inventory.Id)
            {
                return BadRequest();
            }

            _context.Entry(inventory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InventoryExists(id))
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

        // POST: api/Inventories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Inventory>> PostInventory(Inventory inventory)
        {
            _context.Inventories.Add(inventory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInventory", new { id = inventory.Id }, inventory);
        }

        // DELETE: api/Inventories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInventory(int id)
        {
            var inventory = await _context.Inventories.FindAsync(id);
            if (inventory == null)
            {
                return NotFound();
            }

            _context.Inventories.Remove(inventory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InventoryExists(int id)
        {
            return _context.Inventories.Any(e => e.Id == id);
        }
    }
}
