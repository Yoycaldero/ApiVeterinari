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
    public class DataUsersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DataUsersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/DataUsers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DataUser>>> GetDataUser()
        {
          if (_context.DataUser == null)
          {
              return NotFound();
          }
            return await _context.DataUser.ToListAsync();
        }

        // GET: api/DataUsers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DataUser>> GetDataUser(int id)
        {
          if (_context.DataUser == null)
          {
              return NotFound();
          }
            var dataUser = await _context.DataUser.FindAsync(id);

            if (dataUser == null)
            {
                return NotFound();
            }

            return dataUser;
        }

        // PUT: api/DataUsers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDataUser(int id, DataUser dataUser)
        {
            if (id != dataUser.Id)
            {
                return BadRequest();
            }

            _context.Entry(dataUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DataUserExists(id))
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

        // POST: api/DataUsers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("login")]
        public async Task<ActionResult<DataUser>> Login(DataUser user)
        {
            if (_context.DataUser == null)
            {
                return Problem("Entity set 'AppDbContext.DataUser' is null.");
            }

            // Find user by name and password
            var foundUser = await _context.DataUser
              .FirstOrDefaultAsync(u => u.Name == user.Name && u.Password == user.Password);

            if (foundUser == null)
            {
                // Return unauthorized or specific error message
                return Unauthorized("Invalid username or password");
            }

            // Return the found user (excluding password for security)
            foundUser.Password = "";
            return foundUser;
        }


        // DELETE: api/DataUsers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDataUser(int id)
        {
            if (_context.DataUser == null)
            {
                return NotFound();
            }
            var dataUser = await _context.DataUser.FindAsync(id);
            if (dataUser == null)
            {
                return NotFound();
            }

            _context.DataUser.Remove(dataUser);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DataUserExists(int id)
        {
            return (_context.DataUser?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
