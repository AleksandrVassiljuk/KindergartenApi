using KindergartenApi.Data;
using KindergartenApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KindergartenApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KindergartenController : ControllerBase
    {
        private readonly KindergartenDbContext _context;

        public KindergartenController(KindergartenDbContext context)
        {
            _context = context;
        }

        // GET - kõikide andmete vaatamine
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Kindergarten>>> GetKindergartens()
        {
            return await _context.Kindergartens.ToListAsync();
        }

        // GET - ühe kirje vaatamine ID järgi
        [HttpGet("{id}")]
        public async Task<ActionResult<Kindergarten>> GetKindergarten(int id)
        {
            var kindergarten = await _context.Kindergartens.FindAsync(id);

            if (kindergarten == null)
            {
                return NotFound();
            }

            return kindergarten;
        }

        // POST - uue kirje lisamine
        [HttpPost]
        public async Task<ActionResult<Kindergarten>> CreateKindergarten(
            Kindergarten kindergarten)
        {
            kindergarten.CreatedAt = DateTime.Now;
            kindergarten.UpdatedAt = DateTime.Now;

            _context.Kindergartens.Add(kindergarten);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetKindergarten),
                new { id = kindergarten.Id },
                kindergarten);
        }

        // PUT - olemasoleva kirje muutmine
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateKindergarten(
            int id,
            Kindergarten kindergarten)
        {
            if (id != kindergarten.Id)
            {
                return BadRequest();
            }

            var existingKindergarten =
                await _context.Kindergartens.FindAsync(id);

            if (existingKindergarten == null)
            {
                return NotFound();
            }

            existingKindergarten.GroupName = kindergarten.GroupName;
            existingKindergarten.ChildrenCount = kindergarten.ChildrenCount;
            existingKindergarten.KindergartenName = kindergarten.KindergartenName;
            existingKindergarten.TeacherName = kindergarten.TeacherName;
            existingKindergarten.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE - kirje kustutamine
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKindergarten(int id)
        {
            var kindergarten =
                await _context.Kindergartens.FindAsync(id);

            if (kindergarten == null)
            {
                return NotFound();
            }

            _context.Kindergartens.Remove(kindergarten);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}