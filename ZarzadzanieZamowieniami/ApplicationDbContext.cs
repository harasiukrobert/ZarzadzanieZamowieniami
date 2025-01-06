using Microsoft.EntityFrameworkCore;
using ZarzadzanieZamowieniami.Models;

namespace ZarzadzanieZamowieniami
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Klient> Klienci { get; set; }
        public DbSet<Produkt> Produkty { get; set; }
        public DbSet<Zamowienie> Zamowienia { get; set; }
        public DbSet<PozycjaZamowienia> PozycjeZamowien { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
