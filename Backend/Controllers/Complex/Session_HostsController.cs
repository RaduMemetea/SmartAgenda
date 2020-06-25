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
    public class Session_HostsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public Session_HostsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Session_Host
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Session_Host>>> GetSession_Host()
        {
            return await _context.Session_Host.AsNoTracking().ToListAsync();
        }

        // GET: api/Session_Host/5
        [HttpGet("{id_session}/{id_host?}")]
        public async Task<ActionResult<IEnumerable<Session_Host>>> GetSession_Host(int id_session, int id_host = 0)
        {
            List<Session_Host> session_host = new List<Session_Host>();
            if (id_session == 0 || id_host == 0)
                if (id_host == 0)
                    session_host = await _context.Session_Host.Where(x => x.SessionID == id_session).ToListAsync();
                else
                    session_host = await _context.Session_Host.Where(x => x.PersonID == id_host).ToListAsync();
            else
            {
                var sc = await _context.Session_Host.FindAsync(id_session, id_host);
                if (sc != null)
                    session_host.Add(sc);
            }

            if (session_host == null || session_host.Count < 1)
            {
                return NotFound();
            }

            return session_host;
        }

        // PUT: api/Session_Host/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id_session}/{id_host}")]
        public async Task<IActionResult> PutSession_Host(int id_session, int id_host, Session_Host session_Host)
        {
            if (id_session != session_Host.SessionID || id_host != session_Host.PersonID)
            {
                return BadRequest();
            }

            _context.Entry(session_Host).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Session_HostExists(id_session, id_host))
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

        // POST: api/Session_Host
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Session_Host>> PostSession_Host(Session_Host session_host)
        {
            _context.Session_Host.Add(session_host);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (Session_HostExists(session_host.SessionID, session_host.PersonID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetSession_Host", new { id_session = session_host.SessionID, id_Host = session_host.PersonID }, session_host);
        }

        // DELETE: api/Session_Host/5
        [HttpDelete("{id_session}/{id_host}")]
        public async Task<ActionResult<Session_Host>> DeleteSession_Host(int id_session, int id_host)
        {
            var session_host = await _context.Session_Host.FindAsync(id_session, id_host);
            if (session_host == null)
            {
                return NotFound();
            }

            _context.Session_Host.Remove(session_host);
            await _context.SaveChangesAsync();

            return session_host;
        }

        private bool Session_HostExists(int id_session, int id_host)
        {
            return _context.Session_Host.Any(e => e.SessionID == id_session && e.PersonID == id_host);
        }
    }
}
