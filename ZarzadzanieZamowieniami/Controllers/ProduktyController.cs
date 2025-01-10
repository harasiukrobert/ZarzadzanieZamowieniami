using Microsoft.AspNetCore.Mvc;
using ZarzadzanieZamowieniami.Models; // Użyj nazwy swojego projektu
using Microsoft.EntityFrameworkCore;

namespace ZarzadzanieZamowieniami.Controllers // Użyj nazwy swojego projektu
{
    public class ProduktyController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProduktyController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Produkty
        public async Task<IActionResult> Index()
        {
            return _context.Produkty != null ?
                        View(await _context.Produkty.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Produkty'  is null.");
        }

        // GET: Produkty/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Produkty == null)
            {
                return NotFound();
            }

            var produkt = await _context.Produkty
                .FirstOrDefaultAsync(m => m.Id == id);
            if (produkt == null)
            {
                return NotFound();
            }

            return View(produkt);
        }

        // GET: Produkty/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Produkty/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nazwa,Opis,KodKreskowy,StanMagazynowy,Lokalizacja,Cena")] Produkt produkt)
        {
            if (ModelState.IsValid)
            {
                _context.Add(produkt);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(produkt);
        }

        // GET: Produkty/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Produkty == null)
            {
                return NotFound();
            }

            var produkt = await _context.Produkty.FindAsync(id);
            if (produkt == null)
            {
                return NotFound();
            }
            return View(produkt);
        }

        // POST: Produkty/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nazwa,Opis,KodKreskowy,StanMagazynowy,Lokalizacja,Cena")] Produkt produkt)
        {
            if (id != produkt.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(produkt);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProduktExists(produkt.Id))
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
            return View(produkt);
        }

        // GET: Produkty/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Produkty == null)
            {
                return NotFound();
            }

            var produkt = await _context.Produkty
                .FirstOrDefaultAsync(m => m.Id == id);
            if (produkt == null)
            {
                return NotFound();
            }

            return View(produkt);
        }

        // POST: Produkty/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Produkty == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Produkty'  is null.");
            }
            var produkt = await _context.Produkty.FindAsync(id);
            if (produkt != null)
            {
                _context.Produkty.Remove(produkt);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProduktExists(int id)
        {
            return (_context.Produkty?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}