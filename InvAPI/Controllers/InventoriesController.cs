using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InvAPI.Models;
using Microsoft.AspNetCore.Authorization;
using System.Collections.ObjectModel;

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
        public ObservableCollection<InvMainClass> InventServCollection { get; set; }

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
        [HttpGet("getid/{id}")]
        public async Task<ActionResult<Inventory>> GetInventory(int id)
        {
            var inventory = await _context.Inventories.FindAsync(id);

            if (inventory == null)
            {
                return NotFound();
            }

            return inventory;
        }


        // GET: api/Inventories/E10371
        [HttpGet("{inv_num}")]
        public async Task<ActionResult<Inventory>> GetInventory(string inv_num)
        {
            var inventory = await _context.Inventories.SingleOrDefaultAsync(x => x.InvNum == inv_num);

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


        [HttpGet("test/")]
        public async Task<ActionResult<IEnumerable<InvMainClass>>> GetInv(int pageNumber = 1, int pageSize = 100)
        {
            if (_context.Inventories == null)
            {
                return NotFound();
            }

            using (_context)
            {
                var result = (from e in _context.Inventories
                              join d in _context.Nomenclatures on e.NomenclatureId equals d.IdNomenclature
                              join b in _context.Workplaces on e.WorkplaceId equals b.IdWorkplace
                              join c in _context.Movements on e.MoveId equals c.IdMovement
                              join f in _context.Companies on e.CompanyId equals f.IdCompany
                              select new InvMainClass
                              {
                                  Id = e.Id,
                                  invNum = e.InvNum,
                                  PaymentNum = e.PaymentNum,
                                  Comment = e.Comment,
                                  Invoice = e.Invoice,
                                  NomenclatureId = e.NomenclatureId,
                                  NameDevice = d.NameDevice,
                                  IdWorkplace = b.IdWorkplace,
                                  NameWorkplace = b.NameWorkplace,
                                  IdMovement = c.IdMovement,
                                  DateMove = c.DateMove,
                                  IdCompany = f.IdCompany,
                                  CompanyName = f.CompanyName
                              }).ToList();

                int totalCount = result.Count();
                int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
                int skip = (pageNumber - 1) * pageSize;
                var pagedData = result.Skip(skip).Take(pageSize);

                return Ok(pagedData);

            }
        }
        [HttpGet("test1/")]
        public async Task<IActionResult> GetInventoriesAndNomenclatures(int pageNumber = 1, int pageSize = 10)
        {
            var query = from i in _context.Inventories
                        join n in _context.Nomenclatures on i.NomenclatureId equals n.IdNomenclature
                        join b in _context.Workplaces on i.WorkplaceId equals b.IdWorkplace
                        join c in _context.Movements on i.MoveId equals c.IdMovement
                        join f in _context.Companies on i.CompanyId equals f.IdCompany
                        select new { i, n , b, c ,f };

            var totalCount = await query.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            var skip = (pageNumber - 1) * pageSize;

            var result = await query.Skip(skip).Take(pageSize).ToListAsync();

            var data = result.Select(x => new
            {
                Id = x.i.Id,
                invNum = x.i.InvNum,
                PaymentNum = x.i.PaymentNum,
                Comment = x.i.Comment,
                Invoice = x.i.Invoice,
                NomenclatureId = x.i.NomenclatureId,
                NameDevice = x.n.NameDevice,
                IdWorkplace = x.b.IdWorkplace,
                NameWorkplace = x.b.NameWorkplace,
                IdMovement = x.c.IdMovement,
                DateMove = x.c.DateMove,
                IdCompany = x.f.IdCompany,
                CompanyName = x.f.CompanyName
            });

            return Ok(new { TotalCount = totalCount, TotalPages = totalPages, Data = data });
        }

    }
}
