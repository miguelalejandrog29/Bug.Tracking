using Azure.Core;
using Bug.Tracking.Api.Data;
using Bug.Tracking.Api.Data.Dto.Filter;
using Bug.Tracking.Api.Data.Dto.Request;
using Bug.Tracking.Api.Data.Dto.Response;
using Bug.Tracking.Api.Data.Entity;
using Bug.Tracking.Api.Utils;
using LinqKit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bug.Tracking.Api.Controllers
{
    [Route("api/Bug")]
    [ApiController]
    public class UserBugController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UserBugController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/UserBug
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserBug>>> GetUserBugs()
        {
            if (_context.UserBugs == null)
            {
                return NotFound();
            }
            return await _context.UserBugs.Include(x => x.User).Include(x => x.Project).ToListAsync();
        }

        // GET: api/UserBug
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<UserBug>>> GetUserBugs([FromQuery] UserBugFilter filter)
        {
            if (_context.UserBugs == null)
            {
                return NotFound();
            }

            if (!ApplicationValidations.EntityWithAtLeastOneProperty(filter))
            {
                ModelState.AddModelError("Parámetros", "Debe enviar al menos un campo.");
                return BadRequest(ModelState);
            }

            var predicate = PredicateBuilder.New<UserBug>();
            predicate.Start(x => (filter.ProjectId != 0 && x.ProjectId == filter.ProjectId));
            predicate.Or(x => (filter.UserId != 0 && x.UserId == filter.UserId));

            if (filter.StartDate != null && filter.EndDate == null)
            {
                predicate.Or(x => x.CreatedDate.Date >= filter.StartDate.Value.Date);                
            } else if (filter.StartDate == null && filter.EndDate != null)
            {
                predicate.Or(x => x.CreatedDate.Date <= filter.EndDate.Value.Date);
            }
            else if (filter.StartDate != null && filter.EndDate != null)
            {
                predicate.Or(x => x.CreatedDate.Date >= filter.StartDate.Value.Date && x.CreatedDate.Date <= filter.EndDate.Value.Date);
            }

            return await _context.UserBugs.Where(predicate)
                .Include(x => x.User).Include(x => x.Project).ToListAsync();
        }

        // GET: api/UserBug/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserBug>> GetUserBug(long id)
        {
            if (_context.UserBugs == null)
            {
                return NotFound();
            }
            var userBug = await _context.UserBugs.Where(x => x.Id == id)
                .Include(x => x.User).Include(x => x.Project).FirstOrDefaultAsync();

            if (userBug == null)
            {
                return NotFound();
            }

            return userBug;
        }

        // PUT: api/UserBug/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserBug(long id, UserBug userBug)
        {
            if (id != userBug.Id)
            {
                return BadRequest();
            }

            if (!UserExists(userBug.UserId))
            {
                ModelState.AddModelError(nameof(userBug.UserId), "No coincide con nuestro registro.");
                return BadRequest(ModelState);
            }
            if (!ProjectExists(userBug.ProjectId))
            {
                ModelState.AddModelError(nameof(userBug.ProjectId), "No coincide con nuestro registro.");
                return BadRequest(ModelState);
            }

            _context.Entry(userBug).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserBugExists(id))
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

        // POST: api/UserBug
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserBugResponse>> PostUserBug(UserBugRequest request)
        {
            if (_context.UserBugs == null)
            {
                return Problem("Entity set 'ApplicationDbContext.UserBugs'  is null.");
            }

            if (!UserExists(request.UserId))
            {
                ModelState.AddModelError(nameof(request.UserId), "No coincide con nuestro registro.");
                return BadRequest(ModelState);
            }
            if (!ProjectExists(request.ProjectId))
            {
                ModelState.AddModelError(nameof(request.ProjectId), "No coincide con nuestro registro.");
                return BadRequest(ModelState);
            }

            var operation = _context.UserBugs.Add(request.ToEntity());
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUserBug), new { id = operation.Entity.Id }, operation.Entity);
        }

        // DELETE: api/UserBug/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserBug(long id)
        {
            if (_context.UserBugs == null)
            {
                return NotFound();
            }
            var userBug = await _context.UserBugs.FindAsync(id);
            if (userBug == null)
            {
                return NotFound();
            }

            _context.UserBugs.Remove(userBug);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserBugExists(long id)
        {
            return (_context.UserBugs?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private bool UserExists(long id)
        {
            return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private bool ProjectExists(long id)
        {
            return (_context.Projects?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
