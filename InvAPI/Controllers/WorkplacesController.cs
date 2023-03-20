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
    public class WorkplacesController : ControllerBase
    {
        private readonly InventarisationDbContext _context;

        public WorkplacesController(InventarisationDbContext context)
        {
            _context = context;
        }

        // GET: api/Workplaces
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Workplace>>> GetWorkplaces()
        {
            return await _context.Workplaces.ToListAsync();
        }

        // GET: api/Workplaces/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Workplace>> GetWorkplace(int id)
        {
            var workplace = await _context.Workplaces.FindAsync(id);

            if (workplace == null)
            {
                return NotFound();
            }

            return workplace;
        }

        // PUT: api/Workplaces/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWorkplace(int id, Workplace workplace)
        {
            if (id != workplace.IdWorkplace)
            {
                return BadRequest();
            }

            _context.Entry(workplace).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkplaceExists(id))
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

        // POST: api/Workplaces
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Workplace>> PostWorkplace(Workplace workplace)
        {
            _context.Workplaces.Add(workplace);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWorkplace", new { id = workplace.IdWorkplace }, workplace);
        }

        // DELETE: api/Workplaces/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkplace(int id)
        {
            var workplace = await _context.Workplaces.FindAsync(id);
            if (workplace == null)
            {
                return NotFound();
            }

            _context.Workplaces.Remove(workplace);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        [Route("ConnectedTables")]
        [HttpGet]
        public object JoinStatement()
        {
            using (_context)
            {
                var result = (from e in _context.Workplaces
                              join d in _context.Placements on e.PlacementIdWp equals d.IdPlacement
                              join b in _context.Inventories on e.IdInventory equals b.Id
                              join c in _context.Employers on e.EmployerId equals c.IdEmpolyer
                              select new
                              {
                                  id_workplace = e.IdWorkplace,
                                  id_inventory = e.IdInventory,                                 
                                  placement_id_wp = d.IdPlacement,
                                  name_workplace = e.NameWorkplace,
                                  name_placement = d.NamePlacement,
                                  id_empolyer = c.IdEmpolyer,
                                  full_name = c.FullName




                              }).ToList();
                // TODO utilize the above result

                return result;
            }
        }


        private bool WorkplaceExists(int id)
        {
            return _context.Workplaces.Any(e => e.IdWorkplace == id);
        }
    }
}
