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
                              join r in _context.Subjects on e.SubjectId equals r.IdSubject
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
                                  id_placement = g.IdPlacement,
                                  name_placement = g.NamePlacement,
                                  id_movement = c.IdMovement,
                                  date_move = c.DateMove,
                                  id_company = f.IdCompany,
                                  company_name = f.CompanyName,
                                  id_empolyer = h.IdEmpolyer,
                                  full_name = h.FullName,
                                  id_subject = r.IdSubject,
                                  name_subject = r.NameSubject,
                                  serial_number = e.SerialNumber
                              }).ToList();
                // TODO utilize the above result
                result.Reverse();
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
        [HttpPut("lastMoveSet/{id}")]
        public async Task<IActionResult> PutInventory(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var lastRecord = await _context.Movements
                .OrderByDescending(m => m.IdMovement)
                .Select(s => s.IdMovement)
                .FirstOrDefaultAsync();

                //Обновление таблицы, добавление ранее созданного move_id
                SqlCommand add = new SqlCommand("UPDATE Inventory SET move_id = @move_id WHERE Id = @inventory_id", connection);
                add.Parameters.AddWithValue("@move_id", lastRecord);
                add.Parameters.AddWithValue("@inventory_id", id);
                add.ExecuteNonQuery();
                connection.Close();
            }
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutInventoryById(int id, Inventory inventory)
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


        [HttpPost]
        public async Task<ActionResult<Inventory>> PostInventory(Inventory inventory)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                inventory.DateInv = DateTime.UtcNow;

                //добавление в Inventory
                SqlCommand command = new SqlCommand("INSERT INTO Inventory ( company_id, payment_num, comment, invoice, dateInvCreate, subject_id, serial_number) VALUES (@company_id, @payment_num, @comment, @invoice, @dateInvCreate, @subject_id, @serial_number); SELECT CAST(SCOPE_IDENTITY() AS INT)", connection);

                
                command.Parameters.AddWithValue("@company_id", inventory.CompanyId);
                command.Parameters.AddWithValue("@payment_num", inventory.PaymentNum);
                command.Parameters.AddWithValue("@comment", inventory.Comment);
                command.Parameters.AddWithValue("@invoice", inventory.Invoice);
                command.Parameters.AddWithValue("@dateInvCreate", inventory.DateInv);
                command.Parameters.AddWithValue("@subject_id", inventory.SubjectId);
                command.Parameters.AddWithValue("@serial_number", inventory.SerialNumber);


                int insertedId = (int)command.ExecuteScalar();

                connection.Close();
                connection.Open();

                //добавление в Movements
                SqlCommand inventoryItemsCommand = new SqlCommand("INSERT INTO Movements ( id_inventory, date_move, placement_id, employer_id) VALUES (@id_inventory, @date_move, @placement_id, @employer_id)", connection);
                int num = 1;
                Movement movement = new()
                {
                    IdInventory = insertedId,
                    DateMove = DateTime.UtcNow,
                    PlacementId = num,
                    EmployerId = num
                    
                };
                inventoryItemsCommand.Parameters.AddWithValue("@id_inventory", movement.IdInventory);
                inventoryItemsCommand.Parameters.AddWithValue("@date_move", movement.DateMove);
                inventoryItemsCommand.Parameters.AddWithValue("@placement_id", movement.PlacementId);
                inventoryItemsCommand.Parameters.AddWithValue("@employer_id", movement.EmployerId);
                inventoryItemsCommand.ExecuteNonQuery();

                connection.Close();
                connection.Open();

                


                //получение последней записи из таблицы Movements для получения move_id (id_movement)
                var lastRecord = await _context.Movements
                .OrderByDescending(m => m.IdMovement)
                .Select(s => s.IdMovement)
                .FirstOrDefaultAsync();
                
                //Обновление таблицы, добавление ранее созданного move_id
                SqlCommand add = new SqlCommand("UPDATE Inventory SET move_id = @move_id WHERE Id = @inventory_id", connection);
                add.Parameters.AddWithValue("@move_id", lastRecord);
                add.Parameters.AddWithValue("@inventory_id", insertedId);
                add.ExecuteNonQuery();

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




    }
}
