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
    public class Conference_TagsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public Conference_TagsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Conference_Tags
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Conference_Tags>>> GetConference_Tags()
        {
            return await _context.Conference_Tags.ToListAsync();
        }

        // GET: api/Conference_Tags/5
        [HttpGet("{id_conference}/{id_tag?}" )]
        public async Task<ActionResult<IEnumerable<Conference_Tags>>> GetConference_Tags(int id_conference, string id_tag = null)
        {
            List<Conference_Tags> conference_Tags = new List<Conference_Tags>();
            if (id_conference == 0 || id_tag == null)
                if (id_conference == 0)
                    conference_Tags = await _context.Conference_Tags.Where(x => x.TagID.Equals(id_tag)).ToListAsync();
                else
                    conference_Tags = await _context.Conference_Tags.Where(x => x.ConferenceID == id_conference).ToListAsync();
            else
            {
                var tmp = await _context.Conference_Tags.FindAsync(id_conference, id_tag);
                if (tmp != null)
                    conference_Tags.Add(tmp);
            }

            if (conference_Tags == null || conference_Tags.Count == 0)
            {
                return NotFound();
            }

            return conference_Tags;
        }

        // PUT: api/Conference_Tags/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id_conference}/{id_tag}")]
        public async Task<IActionResult> PutConference_Tags(int id_conference, string id_tag, Conference_Tags conference_Tags)
        {
            if (id_conference != conference_Tags.ConferenceID || !id_tag.Equals(conference_Tags.TagID))
            {
                return BadRequest();
            }

            _context.Entry(conference_Tags).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Conference_TagsExists(id_conference, id_tag))
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

        // POST: api/Conference_Tags
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Conference_Tags>> PostConference_Tags(Conference_Tags conference_Tags)
        {
            _context.Conference_Tags.Add(conference_Tags);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (Conference_TagsExists(conference_Tags.ConferenceID, conference_Tags.TagID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetConference_Tags", new { id_conference = conference_Tags.ConferenceID, id_tag = conference_Tags.TagID }, conference_Tags);
        }

        // DELETE: api/Conference_Tags/5
        [HttpDelete("{id_conference}/{id_tag}")]
        public async Task<ActionResult<Conference_Tags>> DeleteConference_Tags(int id_conference, string id_tag)
        {
            var conference_Tags = await _context.Conference_Tags.FindAsync(id_conference, id_tag);
            if (conference_Tags == null)
            {
                return NotFound();
            }

            _context.Conference_Tags.Remove(conference_Tags);
            await _context.SaveChangesAsync();

            return conference_Tags;
        }

        private bool Conference_TagsExists(int id_conference, string id_tag)
        {
            return _context.Conference_Tags.Any(e => e.ConferenceID == id_conference && e.TagID.Equals(id_tag));
        }
    }
}
