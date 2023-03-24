using InvAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

namespace InvAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class InventoriesController : ControllerBase
    {
        private readonly string _connectionString;
        InventarisationDbContext _context = new InventarisationDbContext();

        public InventoriesController(InventarisationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _connectionString = "Data Source=SV-SQL-02\\SVSQL02;Initial Catalog=InventarisationDB;User ID=api-user;Password=QFgQJkWi4t;TrustServerCertificate=True";

        }
        public ObservableCollection<InvMainClass> InventServCollection { get; set; }

        [HttpGet("getlast/")]
        public async Task<ActionResult<string>> GetLastRecord()
        {
            var lastRecord = await _context.Inventories
                .OrderByDescending(m => m.InvNum)
                .Select(s => s.InvNum)
                .FirstOrDefaultAsync();

            if (lastRecord == null)
            {
                return NotFound();
            }

            return lastRecord;
        }

        [Route("ConnectedTables")]
        [HttpGet]
        public object JoinStatement()
        {
            using (_context)
            {
                var result = (from e in _context.Inventories
                              join d in _context.Nomenclatures on e.NomenclatureId equals d.IdNomenclature                                                            
                              join c in _context.Movements on e.MoveId equals c.IdMovement
                              join g in _context.Placements on c.PlacementId equals g.IdPlacement
                              join h in _context.Employers on c.EmployerId equals h.IdEmpolyer
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
                                  id_placement = g.IdPlacement,
                                  name_placement = g.NamePlacement,
                                  id_movement = c.IdMovement,
                                  date_move = c.DateMove,
                                  id_company = f.IdCompany,
                                  company_name = f.CompanyName,
                                  full_name = h.FullName,
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
            //_context.Inventories.Add(inventory);
            //await _context.SaveChangesAsync();

            //return CreatedAtAction("GetInventory", new { id = inventory.Id }, inventory);

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                inventory.DateInv = DateTime.UtcNow;
                SqlCommand command = new SqlCommand("INSERT INTO Inventory (nomenclature_id, company_id, payment_num, comment, invoice, dateInvCreate) VALUES (@nomenclature_id, @company_id, @payment_num, @comment, @invoice, @dateInvCreate); SELECT CAST(SCOPE_IDENTITY() AS INT)", connection);

                command.Parameters.AddWithValue("@nomenclature_id", inventory.NomenclatureId);
                //command.Parameters.AddWithValue("@move_id", inventory.MoveId);
                command.Parameters.AddWithValue("@company_id", inventory.CompanyId);
                command.Parameters.AddWithValue("@payment_num", inventory.PaymentNum);
                command.Parameters.AddWithValue("@comment", inventory.Comment);
                command.Parameters.AddWithValue("@invoice", inventory.Invoice);
           
                command.Parameters.AddWithValue("@dateInvCreate", inventory.DateInv);
                await Console.Out.WriteLineAsync(inventory.DateInv.ToString());
                //string inv_num = (string)command.ExecuteScalar();
                int insertedId = (int)command.ExecuteScalar();

                

                // Do something with the inserted id
            }

            return Ok();
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


    }
}
