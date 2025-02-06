using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiVeterinaria.Context;
using ApiVeterinaria.Models;

namespace ApiVeterinaria.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataAnimalsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DataAnimalsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/DataAnimals
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DataAnimals>>> GetDataAnimals()
        {
          if (_context.DataAnimals == null)
          {
              return NotFound();
          }
            return await _context.DataAnimals.ToListAsync();
        }

        // GET: api/DataAnimals/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DataAnimals>> GetDataAnimals(int id)
        {
          if (_context.DataAnimals == null)
          {
              return NotFound();
          }
            var dataAnimals = await _context.DataAnimals.FindAsync(id);

            if (dataAnimals == null)
            {
                return NotFound();
            }

            return dataAnimals;
        }

        // PUT: api/DataAnimals/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDataAnimals(int id, DataAnimals dataAnimals)
        {
            if (id != dataAnimals.Id)
            {
                return BadRequest();
            }

            _context.Entry(dataAnimals).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DataAnimalsExists(id))
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

        // POST: api/DataAnimals
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DataAnimals>> PostDataAnimals(DataAnimals dataAnimals)
        {
          if (_context.DataAnimals == null)
          {
              return Problem("Entity set 'AppDbContext.DataAnimals'  is null.");
          }
            _context.DataAnimals.Add(dataAnimals);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDataAnimals", new { id = dataAnimals.Id }, dataAnimals);
        }

        // DELETE: api/DataAnimals/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDataAnimals(int id)
        {
            if (_context.DataAnimals == null)
            {
                return NotFound();
            }
            var dataAnimals = await _context.DataAnimals.FindAsync(id);
            if (dataAnimals == null)
            {
                return NotFound();
            }

            _context.DataAnimals.Remove(dataAnimals);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DataAnimalsExists(int id)
        {
            return (_context.DataAnimals?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
