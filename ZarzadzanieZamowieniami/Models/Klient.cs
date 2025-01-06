namespace ZarzadzanieZamowieniami.Models
{
    public class Klient
    {
        public int Id { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string Adres { get; set; }
        public string Telefon { get; set; }
        public string Email { get; set; }

        public List<Zamowienie> Zamowienia { get; set; }
    }
}
