using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuickCreditApi.Models;
using QuickCreditApi.Models.Entities;

namespace QuickCreditApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAccountsController : ControllerBase
    {
        private readonly qcdbContext _context;

        public UserAccountsController(qcdbContext context)
        {
            _context = context;
        }

        // GET: api/UserAccounts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserAccount>>> GetUserAccount()
        {
            var usersList = await _context.UserAccount.ToListAsync();
            return usersList;
        }

        // GET: api/UserAccounts/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<UserAccount>> GetUserAccount(int id)
        {
            var userAccount = await _context.UserAccount.FindAsync(id);

            if (userAccount == null)
            {
                return NotFound();
            }

            return userAccount;
        }

        // PUT: api/UserAccounts/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> PutUserAccount(int id, UserAccount userAccount)
        {
            if (id != userAccount.Id)
            {
                return BadRequest();
            }

            _context.Entry(userAccount).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserAccountExists(id))
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

        // POST: api/UserAccounts
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<UserAccount>> PostUserAccount(UserAccount userAccount)
        {
            _context.UserAccount.Add(userAccount);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UserAccountExists(userAccount.Id) || UserAccountExists((userAccount.Account)))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetUserAccount", new { id = userAccount.Id }, userAccount);
        }

        // DELETE: api/UserAccounts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<UserAccount>> DeleteUserAccount(int id)
        {
            var userAccount = await _context.UserAccount.FindAsync(id);
            if (userAccount == null)
            {
                return NotFound();
            }

            _context.UserAccount.Remove(userAccount);
            await _context.SaveChangesAsync();

            return userAccount;
        }

        private bool UserAccountExists(int id)
        {
            return _context.UserAccount.Any(e => e.Id == id);
        }

        private bool UserAccountExists(string account)
        {
            return _context.UserAccount.Any(e => e.Account == account);
        }
    }
}
