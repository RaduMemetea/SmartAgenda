using BackEnd.DataBase;
using DataModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TalksController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TalksController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Talks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Talk>>> GetTalk()
        {
            return await _context.Talk.ToListAsync();
        }

        // GET: api/Talks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Talk>> GetTalk(int id)
        {
            var talk = await _context.Talk.FindAsync(id);

            if (talk == null)
            {
                return NotFound();
            }

            return talk;
        }

        // PUT: api/Talks/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTalk(int id, Talk talk)
        {
            if (id != talk.ID)
            {
                return BadRequest();
            }

            _context.Entry(talk).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TalkExists(id))
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

        // POST: api/Talks
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Talk>> PostTalk(Talk talk)
        {
            _context.Talk.Add(talk);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTalk", new { id = talk.ID }, talk);
        }

        // DELETE: api/Talks/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Talk>> DeleteTalk(int id)
        {
            var talk = await _context.Talk.FindAsync(id);
            if (talk == null)
            {
                return NotFound();
            }

            _context.Talk.Remove(talk);
            await _context.SaveChangesAsync();

            return talk;
        }

        private bool TalkExists(int id)
        {
            return _context.Talk.Any(e => e.ID == id);
        }
    }
}
