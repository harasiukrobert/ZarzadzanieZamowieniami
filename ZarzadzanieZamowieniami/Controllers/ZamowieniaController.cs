using Microsoft.AspNetCore.Mvc;
using ZarzadzanieZamowieniami.Models; // Użyj nazwy swojego projektu
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ZarzadzanieZamowieniami.Controllers // Użyj nazwy swojego projektu
{
    public class ZamowieniaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ZamowieniaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Zamowienia
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Zamowienia.Include(z => z.Klient);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Zamowienia/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Zamowienia == null)
            {
                return NotFound();
            }

            var zamowienie = await _context.Zamowienia
                .Include(z => z.Klient)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (zamowienie == null)
            {
                return NotFound();
            }

            return View(zamowienie);
        }

        // GET: Zamowienia/Create
        public IActionResult Create()
        {
            ViewData["KlientId"] = new SelectList(_context.Klienci, "Id", "Nazwisko"); // Wyświetlamy nazwisko klienta na liście
            return View();
        }

        // POST: Zamowienia/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DataZlozenia,Status,KlientId")] Zamowienie zamowienie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(zamowienie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["KlientId"] = new SelectList(_context.Klienci, "Id", "Nazwisko", zamowienie.KlientId); // Wyświetlamy nazwisko klienta na liście
            return View(zamowienie);
        }

        // GET: Zamowienia/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Zamowienia == null)
            {
                return NotFound();
            }

            var zamowienie = await _context.Zamowienia.FindAsync(id);
            if (zamowienie == null)
            {
                return NotFound();
            }
            ViewData["KlientId"] = new SelectList(_context.Klienci, "Id", "Nazwisko", zamowienie.KlientId); // Wyświetlamy nazwisko klienta na liście
            return View(zamowienie);
        }

        // POST: Zamowienia/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DataZlozenia,Status,KlientId")] Zamowienie zamowienie)
        {
            if (id != zamowienie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(zamowienie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ZamowienieExists(zamowienie.Id))
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
            ViewData["KlientId"] = new SelectList(_context.Klienci, "Id", "Nazwisko", zamowienie.KlientId); // Wyświetlamy nazwisko klienta na liście
            return View(zamowienie);
        }

        // GET: Zamowienia/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Zamowienia == null)
            {
                return NotFound();
            }

            var zamowienie = await _context.Zamowienia
                .Include(z => z.Klient)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (zamowienie == null)
            {
                return NotFound();
            }

            return View(zamowienie);
        }

        // POST: Zamowienia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Zamowienia == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Zamowienia'  is null.");
            }
            var zamowienie = await _context.Zamowienia.FindAsync(id);
            if (zamowienie != null)
            {
                _context.Zamowienia.Remove(zamowienie);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ZamowienieExists(int id)
        {
            return (_context.Zamowienia?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}