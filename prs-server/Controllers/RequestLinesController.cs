using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using prs_server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace prs_server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestLinesController : ControllerBase
    {
        private readonly PrsContext _context;

        public RequestLinesController(PrsContext context)
        {
            _context = context;
        }

        private async Task <IActionResult> RecalculateRequestTotal(int requestId)
        {
            var request = await _context.Requests.FindAsync(requestId);
            if (request == null)
            {
                throw new Exception("Invalid ID");
            }
            request.Total = (from r in _context.RequestLines
                             join p in _context.Products
                             on r.ProductId equals p.Id
                             where r.RequestId == requestId
                             select new { LineTotal = r.Quantity * p.Price })
                            .Sum(x => x.LineTotal);

            request.Status = "UPDATED";
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RequestLine>>> GetRequestLines()
        {
            return await _context.RequestLines.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RequestLine>> GetRequestLine(int id)
        {
            var requestLine = await _context.RequestLines.FindAsync(id);

            if (requestLine == null)
            {
                return NotFound();
            }

            return requestLine;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, RequestLine requestLine)
        {
            if (id != requestLine.Id)
            {
                return BadRequest();
            }

            _context.Entry(requestLine).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                await RecalculateRequestTotal(requestLine.RequestId);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequestLineExists(id))
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
        public async Task<ActionResult<RequestLine>> Create(RequestLine requestLine)
        {
            _context.RequestLines.Add(requestLine);
            await _context.SaveChangesAsync();
            await RecalculateRequestTotal(requestLine.RequestId);

            return CreatedAtAction("GetRequestLine", new { id = requestLine.Id }, requestLine);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var requestLine = await _context.RequestLines.FindAsync(id);
            if (requestLine == null)
            {
                return NotFound();
            }
            _context.RequestLines.Remove(requestLine);
            await _context.SaveChangesAsync();
            await RecalculateRequestTotal(requestLine.RequestId);
            return NoContent();
        }

        private bool RequestLineExists(int id)
        {
            return _context.RequestLines.Any(e => e.Id == id);
        }
    }
}