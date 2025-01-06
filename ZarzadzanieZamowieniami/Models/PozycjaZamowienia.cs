namespace ZarzadzanieZamowieniami.Models
{
    public class PozycjaZamowienia
    {
        public int Id { get; set; }
        public int Ilosc { get; set; }
        public decimal Cena { get; set; }
        public int ZamowienieId { get; set; }
        public Zamowienie Zamowienie { get; set; }
        public int ProduktId { get; set; }
        public Produkt Produkt { get; set; }
    }
}
