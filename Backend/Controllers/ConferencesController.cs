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
    public class ConferencesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ConferencesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Conferences
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Conference>>> GetConference()
        {
            return await _context.Conference.AsNoTracking().ToListAsync();
        }

        // GET: api/Conferences/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Conference>> GetConference(int id)
        {
            var conference = await _context.Conference.FindAsync(id);

            if (conference == null)
                return NotFound();

            return conference;

        }
        // GET: api/Conferences/5/Sessions
        [HttpGet("{id_conference}/Sessions")]
        public async Task<ActionResult<IEnumerable<Session>>> GetConferenceSessions(int id_conference)
        {
            if (!ConferenceExists(id_conference))
                return BadRequest("Conference does not exists!");

            var sessions = await _context.Session.Where(x => x.ConferenceID == id_conference).ToListAsync();

            if (sessions == null || sessions.Count == 0)
                return NotFound();

            return sessions;

        }
        // PUT: api/Conferences/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutConference(int id, Conference conference)
        {
            if (id != conference.ID)
            {
                return BadRequest();
            }

            _context.Entry(conference).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConferenceExists(id))
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

        // POST: api/Conferences
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Conference>> PostConference(Conference conference)
        {
            _context.Conference.Add(conference);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetConference", new { id = conference.ID }, conference);
        }

        // DELETE: api/Conferences/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Conference>> DeleteConference(int id)
        {
            var conference = await _context.Conference.FindAsync(id);
            if (conference == null)
            {
                return NotFound();
            }

            _context.Conference.Remove(conference);
            await _context.SaveChangesAsync();

            return conference;
        }

        private bool ConferenceExists(int id)
        {
            return _context.Conference.Any(e => e.ID == id);
        }
    }
}
