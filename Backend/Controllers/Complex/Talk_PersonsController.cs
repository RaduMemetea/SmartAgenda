using BackEnd.DataBase;
using DataModels.Complex;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Controllers.Complex
{
    [Route("api/[controller]")]
    [ApiController]
    public class Talk_PersonsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public Talk_PersonsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Talk_Persons
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Talk_Persons>>> GetTalk_Persons()
        {
            return await _context.Talk_Persons.AsNoTracking().ToListAsync();
        }

        // GET: api/Talk_Persons/5
        [HttpGet("{id_talk}/{id_person?}")]
        public async Task<ActionResult<IEnumerable<Talk_Persons>>> GetTalk_Persons(int id_talk, int id_person=0)
        {
            List<Talk_Persons> talk_Persons = new List<Talk_Persons>();
            if (id_talk == 0 || id_person == 0)
                if (id_talk == 0)
                    talk_Persons = await _context.Talk_Persons.Where(x => x.PersonID == id_person).ToListAsync();
                else
                    talk_Persons = await _context.Talk_Persons.Where(x => x.TalkID == id_talk).ToListAsync();
            else
            {
                var tp = await _context.Talk_Persons.FindAsync(id_talk, id_person);
                if (tp != null)
                    talk_Persons.Add(tp);

            }

            if (talk_Persons == null || talk_Persons.Count < 1)
            {
                return NotFound();
            }

            return talk_Persons;
        }

        // PUT: api/Talk_Persons/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id_talk}/{id_person}")]
        public async Task<IActionResult> PutTalk_Persons(int id_talk, int id_person, Talk_Persons talk_Persons)
        {
            if (id_talk != talk_Persons.TalkID || id_person != talk_Persons.PersonID)
            {
                return BadRequest();
            }


            _context.Entry(talk_Persons).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Talk_PersonsExists(id_talk, id_person))
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

        // POST: api/Talk_Persons
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Talk_Persons>> PostTalk_Persons(Talk_Persons talk_Persons)
        {
            _context.Talk_Persons.Add(talk_Persons);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (Talk_PersonsExists(talk_Persons.TalkID, talk_Persons.PersonID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTalk_Persons", new { id_talk = talk_Persons.TalkID, id_person = talk_Persons.PersonID }, talk_Persons);
        }

        // DELETE: api/Talk_Persons/5
        [HttpDelete("{id_talk}/{id_person}")]
        public async Task<ActionResult<Talk_Persons>> DeleteTalk_Persons(int id_talk, int id_person)
        {
            var talk_Persons = await _context.Talk_Persons.FindAsync(id_talk, id_person);
            if (talk_Persons == null)
            {
                return NotFound();
            }

            _context.Talk_Persons.Remove(talk_Persons);
            await _context.SaveChangesAsync();

            return talk_Persons;
        }

        private bool Talk_PersonsExists(int id_talk, int id_person)
        {
            return _context.Talk_Persons.Any(e => e.TalkID == id_talk && e.PersonID == id_person);
        }
    }
}
