using Microsoft.AspNetCore.Mvc;
using ZarzadzanieZamowieniami.Models;
using Microsoft.EntityFrameworkCore;

namespace ZarzadzanieZamowieniami.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KlienciApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public KlienciApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        //GET: api/Klienci
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Klient>>> GetKlienci()
        {
            return await _context.Klienci.ToListAsync();
        }

        //GET: api/Klienci/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Klient>> GetKlient(int id)
        {
            var klient = await _context.Klienci.FindAsync(id);

            if (klient == null)
            {
                return NotFound();
            }

            return klient;
        }

        //POST: api/Klienci
        [HttpPost]
        public async Task<ActionResult<Klient>> PostKlient(Klient klient)
        {
            _context.Klienci.Add(klient);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetKlient", new { id = klient.Id }, klient);
        }

        //PUT: api/Klienci/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKlient(int id, Klient klient)
        {
            if (id != klient.Id)
            {
                return BadRequest();
            }

            _context.Entry(klient).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KlientExists(id))
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

        //DELETE: api/Klienci/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKlient(int id)
        {
            var klient = await _context.Klienci.FindAsync(id);
            if (klient == null)
            {
                return NotFound();
            }

            _context.Klienci.Remove(klient);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool KlientExists(int id)
        {
            return _context.Klienci.Any(e => e.Id == id);
        }
    }
}