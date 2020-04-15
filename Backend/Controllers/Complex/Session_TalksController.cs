using BackEnd.DataBase;
using DataModels.Complex;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Session_TalksController : ControllerBase
    {
        private readonly AppDbContext _context;

        public Session_TalksController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Session_Talks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Session_Talks>>> GetSession_Talks()
        {
            return await _context.Session_Talks.AsNoTracking().ToListAsync();
        }

        // GET: api/Session_Talks/5
        [HttpGet("{id_session}/{id_talk?}")]
        public async Task<ActionResult<IEnumerable<Session_Talks>>> GetSession_Talks(int id_session, int id_talk=0)
        {
            List<Session_Talks> session_Talks = new List<Session_Talks>();

            if (id_talk == 0 || id_session == 0)
                if (id_session == 0)
                    session_Talks = await _context.Session_Talks.Where(x => x.TalkID == id_talk).ToListAsync();
                else
                    session_Talks = await _context.Session_Talks.Where(x => x.SessionID == id_session).ToListAsync();
            else
            {
                var st = await _context.Session_Talks.FindAsync(id_session, id_talk);
                if (st != null)
                    session_Talks.Add(st);

            }
            if (session_Talks == null || session_Talks.Count < 1)
            {
                return NotFound();
            }

            return session_Talks;
        }

        // PUT: api/Session_Talks/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id_session}/{id_talk}")]
        public async Task<IActionResult> PutSession_Talks(int id_session, int id_talk, Session_Talks session_Talks)
        {
            if (id_session != session_Talks.SessionID && id_talk != session_Talks.TalkID)
            {
                return BadRequest();
            }

            _context.Entry(session_Talks).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Session_TalksExists(id_session, id_talk))
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

        // POST: api/Session_Talks
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Session_Talks>> PostSession_Talks(Session_Talks session_Talks)
        {
            _context.Session_Talks.Add(session_Talks);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (Session_TalksExists(session_Talks.SessionID, session_Talks.TalkID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetSession_Talks", new { id_session = session_Talks.SessionID , id_talk = session_Talks.TalkID }, session_Talks);
        }

        // DELETE: api/Session_Talks/5
        [HttpDelete("{id_session}/{id_talk}")]
        public async Task<ActionResult<Session_Talks>> DeleteSession_Talks(int id_session, int id_talk)
        {
            var session_Talks = await _context.Session_Talks.FindAsync(id_session, id_talk);
            if (session_Talks == null)
            {
                return NotFound();
            }

            _context.Session_Talks.Remove(session_Talks);
            await _context.SaveChangesAsync();

            return session_Talks;
        }

        private bool Session_TalksExists(int id_session, int id_talk)
        {
            return _context.Session_Talks.Any(e => e.SessionID == id_session && e.TalkID == id_talk);
        }
    }
}
