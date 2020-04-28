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
    public class Session_ChairController : ControllerBase
    {
        private readonly AppDbContext _context;

        public Session_ChairController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Session_Chair
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Session_Chair>>> GetSession_Chair()
        {
            return await _context.Session_Chair.AsNoTracking().ToListAsync();
        }

        // GET: api/Session_Chair/5
        [HttpGet("{id_session}/{id_chair?}")]
        public async Task<ActionResult<IEnumerable<Session_Chair>>> GetSession_Chair(int id_session, int id_chair = 0)
        {
            List<Session_Chair> session_Chair = new List<Session_Chair>();
            if (id_session == 0 || id_chair == 0)
                if (id_chair == 0)
                    session_Chair = await _context.Session_Chair.Where(x => x.SessionID == id_session).ToListAsync();
                else
                    session_Chair = await _context.Session_Chair.Where(x => x.PersonID == id_chair).ToListAsync();
            else
            {
                var sc = await _context.Session_Chair.FindAsync(id_session, id_chair);
                if (sc != null)
                    session_Chair.Add(sc);
            }

            if (session_Chair == null || session_Chair.Count < 1)
            {
                return NotFound();
            }

            return session_Chair;
        }

        // PUT: api/Session_Chair/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id_session}/{id_chair}")]
        public async Task<IActionResult> PutSession_Chair(int id_session, int id_chair, Session_Chair session_Chair)
        {
            if (id_session != session_Chair.SessionID || id_chair != session_Chair.PersonID)
            {
                return BadRequest();
            }

            _context.Entry(session_Chair).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Session_ChairExists(id_session, id_chair))
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

        // POST: api/Session_Chair
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Session_Chair>> PostSession_Chair(Session_Chair session_Chair)
        {
            _context.Session_Chair.Add(session_Chair);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (Session_ChairExists(session_Chair.SessionID, session_Chair.PersonID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetSession_Chair", new { id_session = session_Chair.SessionID, id_chair = session_Chair.PersonID }, session_Chair);
        }

        // DELETE: api/Session_Chair/5
        [HttpDelete("{id_session}/{id_chair}")]
        public async Task<ActionResult<Session_Chair>> DeleteSession_Chair(int id_session, int id_chair)
        {
            var session_Chair = await _context.Session_Chair.FindAsync(id_session, id_chair);
            if (session_Chair == null)
            {
                return NotFound();
            }

            _context.Session_Chair.Remove(session_Chair);
            await _context.SaveChangesAsync();

            return session_Chair;
        }

        private bool Session_ChairExists(int id_session, int id_chair)
        {
            return _context.Session_Chair.Any(e => e.SessionID == id_session && e.PersonID == id_chair);
        }
    }
}
