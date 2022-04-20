#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LesliesWarehouseAPI.Data;
using LesliesWarehouseAPI.Models;

namespace LesliesWarehouseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PunchInsController : ControllerBase
    {
        private readonly LeslieswarehouseContext _context;

        public PunchInsController(LeslieswarehouseContext context)
        {
            _context = context;
        }

        // GET: api/PunchIns
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PunchIn>>> GetPunchIns()
        {
            return await _context.PunchIns.ToListAsync();
        }

        // GET: api/PunchIns/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PunchIn>> GetPunchIn(int id)
        {
            var punchIn = await _context.PunchIns.FindAsync(id);

            if (punchIn == null)
            {
                return NotFound();
            }

            return punchIn;
        }

        // PUT: api/PunchIns/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPunchIn(int id, PunchIn punchIn)
        {
            if (id != punchIn.PunchInId)
            {
                return BadRequest();
            }

            _context.Entry(punchIn).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PunchInExists(id))
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

        // POST: api/PunchIns
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PunchIn>> PostPunchIn(PunchIn punchIn)
        {
            _context.PunchIns.Add(punchIn);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PunchInExists(punchIn.PunchInId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPunchIn", new { id = punchIn.PunchInId }, punchIn);
        }

        // DELETE: api/PunchIns/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePunchIn(int id)
        {
            var punchIn = await _context.PunchIns.FindAsync(id);
            if (punchIn == null)
            {
                return NotFound();
            }

            _context.PunchIns.Remove(punchIn);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PunchInExists(int id)
        {
            return _context.PunchIns.Any(e => e.PunchInId == id);
        }
    }
}
