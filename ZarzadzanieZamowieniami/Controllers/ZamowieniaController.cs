using Microsoft.AspNetCore.Mvc;
using ZarzadzanieZamowieniami.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ZarzadzanieZamowieniami.Controllers
{
    public class ZamowieniaController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ZamowieniaController> _logger;

        public ZamowieniaController(ApplicationDbContext context, ILogger<ZamowieniaController> logger)
        {
            _context = context;
            _logger = logger;

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
                .Include(z => z.Klient) // Ładowanie klienta
                .Include(z => z.PozycjeZamowienia) // Ładowanie pozycji zamówienia
                    .ThenInclude(pz => pz.Produkt) // Ładowanie produktów powiązanych z pozycjami zamówienia
                .FirstOrDefaultAsync(m => m.Id == id);

            if (zamowienie == null)
            {
                return NotFound();
            }

            return View(zamowienie);
        }


        // GET: Zamowienia/Create
        // GET: Zamowienia/Create
        public IActionResult Create()
        {
            ViewData["KlientId"] = new SelectList(_context.Klienci, "Id", "Nazwisko");
            ViewData["Produkty"] = new SelectList(_context.Produkty, "Id", "Nazwa");

            return View(new Zamowienie
            {
                PozycjeZamowienia = new List<PozycjaZamowienia>() // Inicjalizuj pustą listę, zamiast dodawać pusty obiekt
            });
        }


        // POST: Zamowienia/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Zamowienie zamowienie)
        {
            // Filtruj puste pozycje zamówienia
            zamowienie.PozycjeZamowienia = zamowienie.PozycjeZamowienia
                .Where(p => p.ProduktId != 0 && p.Ilosc > 0) // Sprawdź, czy ProduktId jest ustawione i Ilość jest większa od 0
                .ToList();

            if (ModelState.IsValid)
            {
                foreach (var pozycja in zamowienie.PozycjeZamowienia)
                {
                    pozycja.ZamowienieId = zamowienie.Id;
                }
                _context.Zamowienia.Add(zamowienie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }



            if (!_context.Klienci.Any() || !_context.Produkty.Any())
            {
                _logger.LogError("Lista klientów lub produktów jest pusta. Nie można utworzyć zamówienia.");
                ModelState.AddModelError("", "Nie można utworzyć zamówienia. Brakuje klientów lub produktów.");
                return View();
            }

            ViewData["KlientId"] = new SelectList(_context.Klienci, "Id", "Nazwisko");
            ViewData["Produkty"] = _context.Produkty
                .Select(p => new { value = p.Id, text = p.Nazwa })
                .ToList();


            return View(zamowienie);
        }



        // GET: Zamowienia/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Zamowienia == null)
            {
                return NotFound();
            }

            var zamowienie = await _context.Zamowienia
                .Include(z => z.PozycjeZamowienia)
                    .ThenInclude(pz => pz.Produkt) // Ładujemy powiązane produkty
                .FirstOrDefaultAsync(z => z.Id == id);

            if (zamowienie == null)
            {
                return NotFound();
            }

            // Przygotuj listę produktów dla dropdown
            ViewData["Produkty"] = new SelectList(_context.Produkty, "Id", "Nazwa");

            // Przygotuj listę klientów dla dropdown (jeśli istnieje)
            ViewData["KlientId"] = new SelectList(_context.Klienci, "Id", "Nazwisko", zamowienie.KlientId);

            return View(zamowienie);
        }


        // POST: Zamowienia/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Zamowienie zamowienie)
        {
            if (id != zamowienie.Id)
            {
                return NotFound();
            }

            // Filtruj puste lub niepoprawne pozycje zamówienia
            zamowienie.PozycjeZamowienia = zamowienie.PozycjeZamowienia
                .Where(p => p.ProduktId != 0 && p.Ilosc > 0) // Sprawdź, czy ProduktId jest ustawione i Ilość > 0
                .ToList();

            if (ModelState.IsValid)
            {
                try
                {
                    // Pobierz aktualne pozycje zamówienia z bazy danych
                    var istniejącePozycje = _context.PozycjeZamowienia
                        .Where(p => p.ZamowienieId == zamowienie.Id)
                        .ToList();

                    // Usuwanie wszystkich pozycji z zamówienia
                    _context.PozycjeZamowienia.RemoveRange(istniejącePozycje);
                    await _context.SaveChangesAsync();

                    // Dodawanie zaktualizowanych pozycji
                    foreach (var nowaPozycja in zamowienie.PozycjeZamowienia)
                    {
                        nowaPozycja.ZamowienieId = zamowienie.Id;
                        _context.PozycjeZamowienia.Add(nowaPozycja);
                    }

                    // Aktualizacja samego zamówienia
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

            // Przygotuj dane do widoku w przypadku błędów walidacji
            ViewData["Produkty"] = new SelectList(_context.Produkty, "Id", "Nazwa");
            ViewData["KlientId"] = new SelectList(_context.Klienci, "Id", "Nazwisko", zamowienie.KlientId);
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
                .Include(z => z.PozycjeZamowienia)
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
                return Problem("Entity set 'ApplicationDbContext.Zamowienia' is null.");
            }
            var zamowienie = await _context.Zamowienia
                .Include(z => z.PozycjeZamowienia)
                .FirstOrDefaultAsync(z => z.Id == id);

            if (zamowienie != null)
            {
                // Usuwanie pozycji zamówienia
                foreach (var pozycja in zamowienie.PozycjeZamowienia)
                {
                    _context.Remove(pozycja);
                }

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
