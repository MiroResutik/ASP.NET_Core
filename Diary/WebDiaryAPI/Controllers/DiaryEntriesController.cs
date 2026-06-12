using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebDiaryAPI.Data;
using WebDiaryAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebDiaryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiaryEntriesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        
        public DiaryEntriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/<DiaryEntriesController>
        // Asyncronous data 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DiaryEntry>>> GetDiaryEntries()
        {
            return await _context.DiaryEntries.ToListAsync();

        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<DiaryEntry>> GetDiaryEntry(int id)
        {
            var diaryEntry = await _context.DiaryEntries.FindAsync(id);

            if (diaryEntry == null)
            {
                return NotFound();

            }
            return diaryEntry;
        }

        // POST api/<DiaryEntriesController>
        [HttpPost]
        public async Task<ActionResult<DiaryEntry>> PostDiaryEntry(DiaryEntry diaryEntry)
        {
            diaryEntry.DiaryEntryId = 0;

            _context.DiaryEntries.Add(diaryEntry);

            await _context.SaveChangesAsync();

            var resourceUrl = Url.Action(nameof(GetDiaryEntry), new {id=diaryEntry.DiaryEntryId});
            return Created(resourceUrl, diaryEntry);

        }

        // PUT api/<DiaryEntriesController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDiaryEntry(int id, [FromBody] DiaryEntry diaryEntry )
        {
            if (id != diaryEntry.DiaryEntryId)

            {
                // Returdns a 400 bad request respose if the IDs do not match
                return BadRequest();
            }
            // this prepares the database for data entry
            _context.Entry(diaryEntry).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {

                if (DiaryEntryExists(id))
                {
                    return NotFound();
                }else
                {
                    throw;
                }
            }

            return NoContent();
        }

        private bool DiaryEntryExists(int id)
        {
            return _context.DiaryEntries.Any(e=>e.DiaryEntryId == id);
        }

        // DELETE api/<DiaryEntriesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDiaryEntry(int id)
        {
            var diaryEntry = await _context.DiaryEntries.FindAsync(id);

            if (diaryEntry == null)
            {
                return NotFound();
            }

            _context.DiaryEntries.Remove(diaryEntry);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        
    }
}
