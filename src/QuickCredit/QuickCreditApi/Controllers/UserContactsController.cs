using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuickCreditApi.Models.Entities;

namespace QuickCreditApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserContactsController : ControllerBase
    {
        private readonly qcdbContext _context;

        public UserContactsController(qcdbContext context)
        {
            _context = context;
        }

        // GET: api/UserContacts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserContact>>> GetUserContact()
        {
            return await _context.UserContact.ToListAsync();
        }

        // GET: api/UserContacts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserContact>> GetUserContact(int id)
        {
            var userContact = await _context.UserContact.FindAsync(id);

            if (userContact == null)
            {
                return NotFound();
            }

            return userContact;
        }

        // PUT: api/UserContacts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserContact(int id, UserContact userContact)
        {
            if (id != userContact.Id)
            {
                return BadRequest();
            }

            _context.Entry(userContact).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserContactExists(id))
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

        // POST: api/UserContacts
        [HttpPost]
        public async Task<ActionResult<UserContact>> PostUserContact(UserContact userContact)
        {
            _context.UserContact.Add(userContact);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserContact", new { id = userContact.Id }, userContact);
        }

        // DELETE: api/UserContacts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<UserContact>> DeleteUserContact(int id)
        {
            var userContact = await _context.UserContact.FindAsync(id);
            if (userContact == null)
            {
                return NotFound();
            }

            _context.UserContact.Remove(userContact);
            await _context.SaveChangesAsync();

            return userContact;
        }

        private bool UserContactExists(int id)
        {
            return _context.UserContact.Any(e => e.Id == id);
        }
    }
}
