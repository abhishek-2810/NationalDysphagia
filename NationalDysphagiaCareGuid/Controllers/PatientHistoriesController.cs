using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NationalDysphagiaCareGuid.Models;

namespace NationalDysphagiaCareGuid.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientHistoriesController : ControllerBase
    {
        private readonly NationalDysphagiaCareGuidDbContext _context;

        public PatientHistoriesController(NationalDysphagiaCareGuidDbContext context)
        {
            _context = context;
        }

        // GET: api/PatientHistories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PatientHistory>>> GetPatientHistories()
        {
            return await _context.PatientHistories.ToListAsync();
        }

        // GET: api/PatientHistories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PatientHistory>> GetPatientHistory(int id)
        {
            var patientHistory = await _context.PatientHistories.FindAsync(id);

            if (patientHistory == null)
            {
                return NotFound();
            }

            return patientHistory;
        }

        // PUT: api/PatientHistories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        private async Task<IActionResult> PutPatientHistory(int id, PatientHistory patientHistory)
        {
            if (id != patientHistory.HistoryId)
            {
                return BadRequest();
            }

            _context.Entry(patientHistory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PatientHistoryExists(id))
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

        // POST: api/PatientHistories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PatientHistory>> PostPatientHistory(PatientHistory patientHistory)
        {
            _context.PatientHistories.Add(patientHistory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPatientHistory", new { id = patientHistory.HistoryId }, patientHistory);
        }

        // DELETE: api/PatientHistories/5
        [HttpDelete("{id}")]
        private async Task<IActionResult> DeletePatientHistory(int id)
        {
            var patientHistory = await _context.PatientHistories.FindAsync(id);
            if (patientHistory == null)
            {
                return NotFound();
            }

            _context.PatientHistories.Remove(patientHistory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PatientHistoryExists(int id)
        {
            return _context.PatientHistories.Any(e => e.HistoryId == id);
        }
    }
}
