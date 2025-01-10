using Microsoft.AspNetCore.Mvc;
using ZarzadzanieZamowieniami.Models; // Użyj nazwy swojego projektu
using Microsoft.EntityFrameworkCore;

namespace ZarzadzanieZamowieniami.Controllers // Użyj nazwy swojego projektu
{
    public class KlienciController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<KlienciController> _logger;

        public KlienciController(ApplicationDbContext context, ILogger<KlienciController> logger) // Update constructor
        {
            _context = context;
            _logger = logger;
        }

        // GET: Klienci
        public async Task<IActionResult> Index()
        {
            return _context.Klienci != null ?
                        View(await _context.Klienci.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Klienci'  is null.");
        }

        // GET: Klienci/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Klienci == null)
            {
                return NotFound();
            }

            var klient = await _context.Klienci
                .FirstOrDefaultAsync(m => m.Id == id);
            if (klient == null)
            {
                return NotFound();
            }

            return View(klient);
        }

        // GET: Klienci/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Klienci/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Imie,Nazwisko,Adres,Telefon,Email")] Klient klient)
        {
            if (ModelState.IsValid)
            {
                _context.Add(klient);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        _logger.LogError(error.ErrorMessage);
                    }
                }
            }
            return View(klient);
        }

        // GET: Klienci/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Klienci == null)
            {
                return NotFound();
            }

            var klient = await _context.Klienci.FindAsync(id);
            if (klient == null)
            {
                return NotFound();
            }
            return View(klient);
        }

        // POST: Klienci/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Imie,Nazwisko,Adres,Telefon,Email")] Klient klient)
        {
            if (id != klient.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(klient);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KlientExists(klient.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(klient);
        }

        // GET: Klienci/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Klienci == null)
            {
                return NotFound();
            }

            var klient = await _context.Klienci
                .FirstOrDefaultAsync(m => m.Id == id);
            if (klient == null)
            {
                return NotFound();
            }

            return View(klient);
        }

        // POST: Klienci/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Klienci == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Klienci'  is null.");
            }
            var klient = await _context.Klienci.FindAsync(id);
            if (klient != null)
            {
                _context.Klienci.Remove(klient);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KlientExists(int id)
        {
            return (_context.Klienci?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}