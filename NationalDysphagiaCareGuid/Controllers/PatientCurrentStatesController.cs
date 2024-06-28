using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NationalDysphagiaCareGuid.Models;

namespace NationalDysphagiaCareGuid.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientCurrentStatesController : ControllerBase
    {
        private readonly NationalDysphagiaCareGuidDbContext _context;

        public PatientCurrentStatesController()
        {
            _context = new NationalDysphagiaCareGuidDbContext();
        }

        // GET: api/PatientCurrentStates
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PatientCurrentState>>> GetPatientCurrentStates()
        {
            return await _context.PatientCurrentStates.ToListAsync();
        }

        // GET: api/PatientCurrentStates/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PatientCurrentState>> GetPatientCurrentState(int id)
        {
            var patientCurrentState = await _context.PatientCurrentStates.FindAsync(id);

            if (patientCurrentState == null)
            {
                return NotFound();
            }

            return patientCurrentState;
        }

        // PUT: api/PatientCurrentStates/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPatientCurrentState(int id, PatientCurrentState patientCurrentState)
        {
            if (id != patientCurrentState.CurrentStateId)
            {
                return BadRequest();
            }

            _context.Entry(patientCurrentState).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PatientCurrentStateExists(id))
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

        // POST: api/PatientCurrentStates
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PatientCurrentState>> PostPatientCurrentState(PatientCurrentState patientCurrentState)
        {
            _context.PatientCurrentStates.Add(patientCurrentState);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPatientCurrentState", new { id = patientCurrentState.CurrentStateId }, patientCurrentState);
        }

        // DELETE: api/PatientCurrentStates/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatientCurrentState(int id)
        {
            var patientCurrentState = await _context.PatientCurrentStates.FindAsync(id);
            if (patientCurrentState == null)
            {
                return NotFound();
            }

            _context.PatientCurrentStates.Remove(patientCurrentState);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PatientCurrentStateExists(int id)
        {
            return _context.PatientCurrentStates.Any(e => e.CurrentStateId == id);
        }
    }
}
