namespace ZarzadzanieZamowieniami.Models
{
    public class Zamowienie
    {
        public int Id { get; set; }
        public DateTime DataZlozenia { get; set; }
        public string Status { get; set; }
        public int KlientId { get; set; }
        public Klient Klient { get; set; }

        public List<PozycjaZamowienia> PozycjeZamowienia { get; set; }
    }
}
