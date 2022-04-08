using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using prs_server.Models;

namespace prs_server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestsController : ControllerBase
    {
        private readonly PrsContext _context;

        public RequestsController(PrsContext context)
        {
            _context = context;
        }


        // GET api/Requests/review/id
        [HttpGet("reviews/{id}")]

        public async Task<ActionResult<IEnumerable<Request>>> GetReviews(int userId)
        {
            return await _context.Requests.Include(x => x.User).Where(x => x.Status == "REVIEW" && x.UserId != userId ).ToListAsync();
        }


        // PUT: api/Requests/5/review
        [HttpPut("review/{id}")]
        public async Task<IActionResult> Review(int id, Request request)
        {
            var req = await _context.Requests.FindAsync(request.Id);
            if (req == null)
            {
                return NotFound();
            }
            req.Status = (req.Total <= 50) ? "APPROVED" : "REVIEW";
            return await Update(id, request);
        }


        // PUT: api/Requests/5/approve
        [HttpPut("approve/{id}")]
        public async Task<IActionResult> Approve(int id, Request request)
        {
            request.Status = "APPROVED";
            return await Update(id, request);
        }

        // PUT: api/Requests/5/reject
        [HttpPut("reject/{id}")]
        public async Task<IActionResult> Reject(int id, Request request)
        {
            request.Status = "REJECTED";
            return await Update(id, request);
        }

        // GET: api/Requests
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Request>>> GetRequests()
        {
            return await _context.Requests.Include(x => x.User).ToListAsync();
        }

        // GET: api/Requests/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Request>> Get(int id)
        {
            var request = await _context.Requests.Include(x => x.User)
                                                 .Include(x => x.RequestLines)
                                                 .ThenInclude(x => x.Product)
                                                 .SingleOrDefaultAsync(x => x.Id == id);

            if (request == null)
            {
                return NotFound();
            }

            return request;
        }

        // PUT: api/Requests/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Request request)
        {
            if (id != request.Id)
            {
                return BadRequest();
            }

            _context.Entry(request).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequestExists(id))
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

        // POST: api/Requests
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Request>> Create(Request request)
        {
            _context.Requests.Add(request);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRequest", new { id = request.Id }, request);
        }

        // DELETE: api/Requests/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var request = await _context.Requests.FindAsync(id);
            if (request == null)
            {
                return NotFound();
            }

            _context.Requests.Remove(request);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RequestExists(int id)
        {
            return _context.Requests.Any(e => e.Id == id);
        }
    }
}
