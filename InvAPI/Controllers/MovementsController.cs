using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InvAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Data.SqlClient;

namespace InvAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MovementsController : ControllerBase
    {
        private readonly InventarisationDbContext _context;
        private readonly string _connectionString;
        public MovementsController(InventarisationDbContext context)
        {
            _context = context;
            _connectionString = "Data Source=SV-SQL-02\\SVSQL02;Initial Catalog=InventarisationDB;User ID=api-user;Password=QFgQJkWi4t;TrustServerCertificate=True";
        }

        // GET: api/Movements
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movement>>> GetMovements()
        {
            return await _context.Movements.ToListAsync();
        }

        [HttpGet("getlast/")]
        public async Task<ActionResult<int>> GetLastRecord()
        {
            var lastRecord = await _context.Movements
                .OrderByDescending(m => m.IdMovement)
                .Select(s => s.IdMovement)
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
                var result = (from m in _context.Movements
                              join p in _context.Placements on m.PlacementId equals p.IdPlacement
                              join e in _context.Employers on m.EmployerId equals e.IdEmpolyer
                              select new
                              {
                                  id_movement = m.IdMovement,
                                  date_move = m.DateMove,
                                  id_inventory = m.IdInventory,
                                  placement_id = m.PlacementId,
                                  name_placement = p.NamePlacement,
                                  planner = m.Planner,
                                  employer_id = m.EmployerId,
                                  full_name = e.FullName
                              }).ToList();
                // TODO utilize the above result
                result.Reverse();
                return result;
            }
        }
        [HttpGet("gethistory/{id}")]
        public ActionResult<List<object>> GetMovementById(int id)
        {
            var result = (from m in _context.Movements
                          join p in _context.Placements on m.PlacementId equals p.IdPlacement
                          join e in _context.Employers on m.EmployerId equals e.IdEmpolyer
                          where m.IdInventory == id
                          select new
                          {
                              id_movement = m.IdMovement,
                              date_move = m.DateMove,
                              id_inventory = m.IdInventory,
                              placement_id = m.PlacementId,
                              name_placement = p.NamePlacement,
                              planner = m.Planner,
                              employer_id = m.EmployerId,
                              full_name = e.FullName
                          }).ToList();

            if (result.Count == 0) // если запись не найдена, возвращаем статус 404 Not Found
            {
                return NotFound();
            }

            return Ok(result); // возвращаем список объектов анонимного типа
        }



        // GET: api/Movements/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Movement>> GetMovement(int id)
        {
            var movement = await _context.Movements.FindAsync(id);

            if (movement == null)
            {
                return NotFound();
            }

            return movement;
        }

        // PUT: api/Movements/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("empSetSet/")]
        public async Task<IActionResult> PutMovement(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var lastRecord = await _context.Movements
                .OrderByDescending(m => m.IdMovement)
                .Select(s => s.IdMovement)
                .FirstOrDefaultAsync();

                //Обновление таблицы, добавление ранее созданного move_id
                SqlCommand add = new SqlCommand("UPDATE Movements SET employer_id = @employer_id WHERE id_movement = @id_movement", connection);
                add.Parameters.AddWithValue("@id_movement", lastRecord);
                add.Parameters.AddWithValue("@employer_id", id);
                add.ExecuteNonQuery();
                connection.Close();
            }
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovementById(int id, Movement movement)
        {
            if (id != movement.IdMovement)
            {
                return BadRequest();
            }

            _context.Entry(movement).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovementExists(id))
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



        // POST: api/Movements
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Movement>> PostMovement(Movement movement)
        {
            _context.Movements.Add(movement);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMovement", new { id = movement.IdMovement }, movement);
        }

        // DELETE: api/Movements/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovement(int id)
        {
            bool isValueUsed = await _context.Inventories.AnyAsync(p => p.MoveId == id);

            if (isValueUsed)
            {
                return BadRequest("Значение используется в другой таблице");
            }

            var movement = await _context.Movements.FindAsync(id);
            if (movement == null)
            {
                return NotFound();
            }

            _context.Movements.Remove(movement);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MovementExists(int id)
        {
            return _context.Movements.Any(e => e.IdMovement == id);
        }
    }
}
